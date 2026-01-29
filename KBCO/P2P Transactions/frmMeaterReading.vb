Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Resources
Imports System.Text
Imports CrystalDecisions.[Shared].Json
Imports System.Net.Http
Imports System.Configuration
Imports Newtonsoft.Json
Imports iTextSharp.text
Imports System.DirectoryServices.ActiveDirectory
Imports System.Threading.Tasks
Imports Dapper
Imports System.DirectoryServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Web.Configuration

Public Class frmMeaterReading


    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Dim generalValObj As New generalValidation
    Const WMCLOSE As String = "WmClose"
    Private _lastFormSize As Integer
    Dim SelectedAgreement As String
    Dim MR_ID As String
    Dim viewPreviousData As Boolean = False
    Dim is_FirstRecord As Boolean = True '// reading first record of the dgbw
    Dim SelectedVAT As Integer = 0
    Dim MeterReadingNo As String = ""

    Dim connectionString As String =
    ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                Me.save(True)
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                delete()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'showCrystalReport()
        End Select
    End Sub

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

    Private Sub createNew()
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save(ByRef msgNeed As Boolean) As Boolean
        ' Confirm with user if needed
        If msgNeed Then
            Dim result = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result <> DialogResult.Yes Then Return False
        End If

        ' Validate data first (fail fast)
        If Not isDataValid() Then Return False

        ' Generate MR ID
        Dim mrId As String = GenarateMRNo() ' Use optimized version
        If String.IsNullOrEmpty(mrId) Then
            MessageBox.Show("Failed to generate MR ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Using transaction = connection.BeginTransaction()
                    Try
                        ' ================================================================
                        ' STEP 1: UPSERT METER READING MASTER (1 query instead of 2)
                        ' ================================================================
                        Dim masterSql As String = "
                        MERGE TBL_METER_READING_MASTER AS target
                        USING (SELECT @COM_ID AS COM_ID, @CUS_ID AS CUS_ID, @AG_ID AS AG_ID, 
                                      @PERIOD_START AS PERIOD_START, @PERIOD_END AS PERIOD_END) AS source
                        ON (target.COM_ID = source.COM_ID 
                            AND target.CUS_ID = source.CUS_ID 
                            AND target.AG_ID = source.AG_ID
                            AND target.PERIOD_START = source.PERIOD_START 
                            AND target.PERIOD_END = source.PERIOD_END)
                        WHEN MATCHED THEN
                            UPDATE SET 
                                INV_ADD1 = @INV_ADD1, 
                                INV_ADD2 = @INV_ADD2, 
                                INV_ADD3 = @INV_ADD3, 
                                IS_NBT = @IS_NBT,
                                IS_VAT = @IS_VAT, 
                                RENTAL_VAL = @RENTAL_VAL, 
                                ADJUSTMENT = @ADJUSTMENT
                        WHEN NOT MATCHED THEN
                            INSERT (COM_ID, MR_ID, CUS_ID, AG_ID, PERIOD_START, PERIOD_END, 
                                    INV_ADD1, INV_ADD2, INV_ADD3, CR_BY, CR_DATE, 
                                    IS_NBT, IS_VAT, RENTAL_VAL, ADJUSTMENT)
                            VALUES (@COM_ID, @MR_ID, @CUS_ID, @AG_ID, @PERIOD_START, @PERIOD_END, 
                                    @INV_ADD1, @INV_ADD2, @INV_ADD3, @CR_BY, GETDATE(), 
                                    @IS_NBT, @IS_VAT, @RENTAL_VAL, @ADJUSTMENT);
                    "

                        connection.Execute(masterSql, New With {
                            .COM_ID = globalVariables.selectedCompanyID,
                            .MR_ID = mrId,
                            .CUS_ID = txtCustomerID.Text.Trim(),
                            .AG_ID = txtSelectedAG.Text.Trim(),
                            .PERIOD_START = dtpStart.Value.ToString("yyyy-MM-dd"),
                            .PERIOD_END = dtpEnd.Value.ToString("yyyy-MM-dd"),
                            .INV_ADD1 = txLocation1.Text.Trim(),
                            .INV_ADD2 = txtLocation2.Text.Trim(),
                            .INV_ADD3 = txtLocation3.Text.Trim(),
                            .IS_NBT = cbNBT.CheckState,
                            .IS_VAT = cbVAT.CheckState,
                            .RENTAL_VAL = If(String.IsNullOrWhiteSpace(txtRental.Text), DBNull.Value, CDbl(txtRental.Text.Trim())),
                            .ADJUSTMENT = If(String.IsNullOrWhiteSpace(txtAdujstment.Text), DBNull.Value, CDbl(txtAdujstment.Text.Trim())),
                            .CR_BY = userSession
                        }, transaction)

                        ' ================================================================
                        ' STEP 2: BATCH UPSERT METER READING DETAILS (HUGE PERFORMANCE GAIN!)
                        ' ================================================================
                        ' Instead of loop with 2 queries per row (EXISTS + INSERT/UPDATE)
                        ' We do 1 batch MERGE for all rows!

                        Dim meterReadings = GetMeterReadingFromGrid() ' Extract data once

                        If meterReadings.Count > 0 Then
                            Dim detailSql As String = "
                            MERGE TBL_TBL_METER_READING_DET AS target
                            USING (SELECT @COM_ID AS COM_ID, @CUS_ID AS CUS_ID, @AG_ID AS AG_ID,
                                          @PERIOD_START AS PERIOD_START, @PERIOD_END AS PERIOD_END,
                                          @SERIAL_NO AS SERIAL_NO) AS source
                            ON (target.COM_ID = source.COM_ID 
                                AND target.CUS_ID = source.CUS_ID
                                AND target.AG_ID = source.AG_ID 
                                AND target.PERIOD_START = source.PERIOD_START
                                AND target.PERIOD_END = source.PERIOD_END 
                                AND target.SERIAL_NO = source.SERIAL_NO)
                            WHEN MATCHED THEN
                                UPDATE SET 
                                    START_MR = @START_MR,
                                    END_MR = @END_MR,
                                    P_NO = @P_NO,
                                    MR_MAKE_MODEL = @MR_MAKE_MODEL,
                                    M_LOC = @M_LOC,
                                    COPIES = @COPIES,
                                    WAISTAGE = @WAISTAGE,
                                    CUS_NAME = @CUS_NAME
                            WHEN NOT MATCHED THEN
                                INSERT (COM_ID, PERIOD_START, PERIOD_END, SERIAL_NO, MR_ID, AG_ID, 
                                        CUS_ID, P_NO, MR_MAKE_MODEL, M_LOC, START_MR, END_MR, 
                                        COPIES, WAISTAGE, CUS_NAME)
                                VALUES (@COM_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @MR_ID, @AG_ID,
                                        @CUS_ID, @P_NO, @MR_MAKE_MODEL, @M_LOC, @START_MR, @END_MR,
                                        @COPIES, @WAISTAGE, @CUS_NAME);
                        "

                            ' Batch execute - Dapper handles this efficiently
                            connection.Execute(detailSql, meterReadings.Select(Function(mr) New With {
                                .COM_ID = globalVariables.selectedCompanyID,
                                .CUS_ID = txtCustomerID.Text.Trim(),
                                .AG_ID = txtSelectedAG.Text.Trim(),
                                .PERIOD_START = dtpStart.Value.ToString("yyyy-MM-dd"),
                                .PERIOD_END = dtpEnd.Value.ToString("yyyy-MM-dd"),
                                .SERIAL_NO = mr.SerialNo,
                                .MR_ID = mrId,
                                .P_NO = mr.PNo,
                                .MR_MAKE_MODEL = mr.MakeModel,
                                .M_LOC = mr.Location,
                                .START_MR = mr.StartMR,
                                .END_MR = mr.EndMR,
                                .COPIES = mr.Copies,
                                .WAISTAGE = mr.Waistage,
                                .CUS_NAME = txtCustomerName.Text.Trim()
                            }), transaction)

                            ' ================================================================
                            ' STEP 3: BATCH UPDATE MACHINE TRANSACTIONS
                            ' ================================================================
                            ' Instead of loop with update per row, batch update!
                            Dim updateMachineSql As String = "
                            UPDATE TBL_MACHINE_TRANSACTIONS 
                            SET SMR_ADUJESTED_STATUS = 'UPDATED'
                            WHERE COM_ID = @COM_ID 
                            AND SERIAL IN @Serials
                        "

                            connection.Execute(updateMachineSql, New With {
                                .COM_ID = globalVariables.selectedCompanyID,
                                .Serials = meterReadings.Select(Function(mr) mr.SerialNo).ToArray()
                            }, transaction)
                        End If

                        ' ================================================================
                        ' STEP 4: UPDATE AGREEMENT (1 query)
                        ' ================================================================
                        Dim billingMethod As String = ""
                        If rbtnCommitment.Checked Then billingMethod = "COMMITMENT"
                        If rbtnActual.Checked Then billingMethod = "ACTUAL"
                        If rbtnRental.Checked Then billingMethod = "RENTAL"

                        Dim invStatus As String = ""
                        If rbtnInvStatusAll.Checked Then invStatus = "ALL"
                        If rbtnInvStatusIndividual.Checked Then invStatus = "INDIVIDUAL"

                        Dim agreementSql As String = "
                        UPDATE TBL_CUS_AGREEMENT 
                        SET CUS_CODE = @CUS_CODE,
                            BILLING_METHOD = @BILLING_METHOD,
                            SLAB_METHOD = @SLAB_METHOD,
                            BILLING_PERIOD = @BILLING_PERIOD,
                            MD_BY = @MD_BY,
                            MD_DATE = GETDATE(),
                            INV_STATUS = @INV_STATUS,
                            MACHINE_TYPE = @MACHINE_TYPE,
                            AG_RENTAL_PRICE = @AG_RENTAL_PRICE
                        WHERE COM_ID = @COM_ID 
                        AND AG_ID = @AG_ID
                    "

                        connection.Execute(agreementSql, New With {
                            .COM_ID = globalVariables.selectedCompanyID,
                            .AG_ID = txtSelectedAG.Text.Trim(),
                            .CUS_CODE = txtCustomerID.Text.Trim(),
                            .BILLING_METHOD = billingMethod,
                            .SLAB_METHOD = txtSlabMethod.Text.Trim(),
                            .BILLING_PERIOD = txtBilPeriod.Text.Trim(),
                            .MD_BY = userSession,
                            .INV_STATUS = invStatus,
                            .MACHINE_TYPE = "BW",
                            .AG_RENTAL_PRICE = If(String.IsNullOrWhiteSpace(txtRental.Text), DBNull.Value, CDbl(txtRental.Text.Trim()))
                        }, transaction)

                        ' ================================================================
                        ' STEP 5: HANDLE BW COMMITMENTS (Batch operations)
                        ' ================================================================
                        If dgBw.Rows.Count > 0 Then
                            Dim bwCommitments = GetBWCommitmentsFromGrid()

                            If bwCommitments.Count > 0 Then
                                ' Delete existing commitments
                                connection.Execute("
                                DELETE FROM TBL_AG_BW_COMMITMENT 
                                WHERE COM_ID = @COM_ID 
                                AND CUS_ID = @CUS_ID 
                                AND AG_CODE = @AG_CODE
                            ", New With {
                                    .COM_ID = globalVariables.selectedCompanyID,
                                    .CUS_ID = txtCustomerID.Text.Trim(),
                                    .AG_CODE = txtSelectedAG.Text.Trim()
                                }, transaction)

                                ' Batch insert new commitments
                                Dim bwInsertSql As String = "
                                INSERT INTO TBL_AG_BW_COMMITMENT 
                                    (COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE)
                                VALUES (@COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE)
                            "

                                connection.Execute(bwInsertSql, bwCommitments.Select(Function(bw) New With {
                                    .COM_ID = globalVariables.selectedCompanyID,
                                    .CUS_ID = txtCustomerID.Text.Trim(),
                                    .AG_CODE = txtSelectedAG.Text.Trim(),
                                    .BW_RANGE_1 = bw.Range1,
                                    .BW_RANGE_2 = bw.Range2,
                                    .BW_RATE = bw.Rate
                                }), transaction)
                            End If
                        End If

                        ' Commit transaction
                        transaction.Commit()

                        If msgNeed Then
                            MessageBox.Show("Transaction Saved Successfully.", "Saved.",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        Return True

                    Catch ex As Exception
                        transaction.Rollback()
                        inputErrorLog(Me.Text, $"{globalVariables.selectedCompanyID}-{Me.Tag}X1",
                                    "Save", userSession, userName, DateTime.Now, ex.Message)
                        MessageBox.Show($"Error code({globalVariables.selectedCompanyID}-{Me.Tag}X1) {GenaralErrorMessage}{ex.Message}",
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End Try
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Function GetMeterReadingFromGrid() As List(Of MeterReadingData)
        Dim readings As New List(Of MeterReadingData)

        For Each row As DataGridViewRow In dgMR.Rows
            ' Skip empty rows
            If row.Cells("SN").Value Is Nothing OrElse
               String.IsNullOrWhiteSpace(row.Cells("SN").Value.ToString()) Then
                Continue For
            End If

            Dim SerialNo As String = row.Cells("SN").Value.ToString()
            Dim PNo As String = If(row.Cells("P_NO").Value?.ToString(), "0")
            Dim MakeModel As String = If(row.Cells("MR_MAKE").Value?.ToString(), "")
            Dim Location As String = If(row.Cells("M_LOC").Value?.ToString(), "")
            Dim StartMR As Integer
            Integer.TryParse(If(row.Cells("START_MR").Value, "").ToString(), StartMR)

            Dim EndMR As Integer
            Integer.TryParse(If(row.Cells("END_MR").Value, "").ToString(), EndMR)


            Dim Copies As Integer
            Integer.TryParse(If(row.Cells("MR_COPIES").Value, "").ToString(), Copies)

            Dim waistage As Integer
            Integer.TryParse(If(row.Cells("WAISTAGE").Value, "").ToString(), waistage)

            readings.Add(New MeterReadingData With {
                .SerialNo = row.Cells("SN").Value.ToString(),
                .PNo = If(row.Cells("P_NO").Value?.ToString(), "0"),
                .MakeModel = If(row.Cells("MR_MAKE").Value?.ToString(), ""),
                .Location = If(row.Cells("M_LOC").Value?.ToString(), ""),
                .StartMR = If(Integer.TryParse(
                  If(row.Cells("START_MR").Value, "").ToString(),
                  Nothing),
                   Convert.ToInt32(row.Cells("START_MR").Value),
                   0),
                .EndMR = If(Integer.TryParse(
                  If(row.Cells("END_MR").Value, "").ToString(),
                  Nothing),
                   Convert.ToInt32(row.Cells("END_MR").Value),
                   0),
                .Copies = If(Integer.TryParse(
                  If(row.Cells("MR_COPIES").Value, "").ToString(),
                  Nothing),
                   Convert.ToInt32(row.Cells("MR_COPIES").Value),
                   0),
                .Waistage = If(Integer.TryParse(
                  If(row.Cells("WAISTAGE").Value, "").ToString(),
                  Nothing),
                   Convert.ToInt32(row.Cells("WAISTAGE").Value),
                   0)
            })
        Next

        Return readings
    End Function

    Private Function GetBWCommitmentsFromGrid() As List(Of BWCommitmentData)
        Dim commitments As New List(Of BWCommitmentData)

        For Each row As DataGridViewRow In dgBw.Rows
            ' Skip empty rows
            If row.Cells("BW_RANGE_1").Value Is Nothing Then
                Continue For
            End If

            commitments.Add(New BWCommitmentData With {
            .Range1 = Convert.ToInt32(row.Cells("BW_RANGE_1").Value),
            .Range2 = Convert.ToInt32(row.Cells("BW_RANGE_2").Value),
            .Rate = Convert.ToDecimal(row.Cells("BW_RATE").Value)
        })
        Next

        Return commitments
    End Function

    'Private Function save(ByRef MsgNeed As Boolean) As Boolean
    '    save = False
    '    Dim IsEdit As Boolean = False
    '    If MsgNeed = True Then
    '        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '        If conf = vbYes Then

    '        Else
    '            Exit Function
    '        End If
    '    End If

    '    GenarateMRNo()
    '    MR_ID = MeterReadingNo
    '    MessageBox.Show(MR_ID)
    '    Try
    '        If isDataValid() = False Then
    '            Exit Function
    '        End If

    '        connectionStaet()

    '        dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction


    '        errorEvent = "Save"
    '        strSQL = "SELECT CASE WHEN EXISTS (SELECT  COM_ID FROM  TBL_METER_READING_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '        If dbConnections.sqlCommand.ExecuteScalar Then
    '            IsEdit = True
    '            strSQL = "UPDATE    TBL_METER_READING_MASTER SET     INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, IS_NBT=@IS_NBT ,IS_VAT=@IS_VAT, RENTAL_VAL=@RENTAL_VAL,ADJUSTMENT=@ADJUSTMENT WHERE     (COM_ID = @COM_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (CUS_ID = @CUS_ID) AND (AG_ID =@AG_ID)"
    '        Else
    '            IsEdit = False
    '            strSQL = "INSERT INTO TBL_METER_READING_MASTER (COM_ID, MR_ID, CUS_ID, AG_ID, PERIOD_START, PERIOD_END, INV_ADD1, INV_ADD2, INV_ADD3, CR_BY, CR_DATE,IS_NBT ,IS_VAT,RENTAL_VAL,ADJUSTMENT) VALUES     (@COM_ID, @MR_ID, @CUS_ID, @AG_ID, @PERIOD_START, @PERIOD_END, @INV_ADD1, @INV_ADD2, @INV_ADD3, '" & userSession & "', GETDATE(),@IS_NBT ,@IS_VAT,@RENTAL_VAL,@ADJUSTMENT)"
    '        End If

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

    '        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value.ToString("yyyy/MM/dd"))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value.ToString("yyyy/MM/dd"))

    '        If IsEdit = False Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@MR_ID", Trim(MeterReadingNo))
    '        End If

    '        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@IS_NBT", cbNBT.CheckState)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@IS_VAT", cbVAT.CheckState)

    '        If Trim(txtRental.Text) = "" Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", DBNull.Value)
    '        Else
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", CDbl(Trim(txtRental.Text)))
    '        End If


    '        'ADJUSTMENT
    '        If Trim(txtAdujstment.Text) = "" Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@ADJUSTMENT", DBNull.Value)
    '        Else
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@ADJUSTMENT", CDbl(Trim(txtAdujstment.Text)))
    '        End If

    '        If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

    '        For Each row As DataGridViewRow In dgMR.Rows

    '            If dgMR.Rows(row.Index).Cells(0).Value <> "" Then
    '                dbConnections.sqlCommand.Parameters.Clear()

    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT     P_NO FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (SERIAL_NO = '" & Trim(dgMR.Rows(row.Index).Cells("SN").Value) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                If dbConnections.sqlCommand.ExecuteScalar Then
    '                    IsEdit = True
    '                    strSQL = "UPDATE    TBL_TBL_METER_READING_DET SET START_MR=@START_MR,END_MR=@END_MR,  P_NO =@P_NO, MR_MAKE_MODEL =@MR_MAKE_MODEL, M_LOC =@M_LOC,  COPIES =@COPIES, WAISTAGE =@WAISTAGE, CUS_NAME =@CUS_NAME  WHERE     (COM_ID = @COM_ID) AND (PERIOD_START = @PERIOD_START) AND (PERIOD_END = @PERIOD_END) AND (CUS_ID = @CUS_ID) AND (SERIAL_NO = @SERIAL_NO)"
    '                Else
    '                    IsEdit = False
    '                    strSQL = "INSERT INTO TBL_TBL_METER_READING_DET (COM_ID, PERIOD_START, PERIOD_END, SERIAL_NO, MR_ID, AG_ID, CUS_ID, P_NO, MR_MAKE_MODEL, M_LOC, START_MR, END_MR, COPIES, WAISTAGE, CUS_NAME) VALUES     (@COM_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @MR_ID, @AG_ID, @CUS_ID, @P_NO, @MR_MAKE_MODEL, @M_LOC, @START_MR, @END_MR, @COPIES, @WAISTAGE, @CUS_NAME)"
    '                End If

    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value.ToString("yyyy/MM/dd"))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value.ToString("yyyy/MM/dd"))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", dgMR.Rows(row.Index).Cells("SN").Value)
    '                If IsEdit = False Then
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@MR_ID", MR_ID)
    '                End If

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@P_NO", dgMR.Rows(row.Index).Cells("P_NO").Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@MR_MAKE_MODEL", dgMR.Rows(row.Index).Cells("MR_MAKE").Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC", dgMR.Rows(row.Index).Cells("M_LOC").Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", dgMR.Rows(row.Index).Cells("START_MR").Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@END_MR", dgMR.Rows(row.Index).Cells("END_MR").Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COPIES", dgMR.Rows(row.Index).Cells("MR_COPIES").Value)
    '                If IsDBNull(dgMR.Rows(row.Index).Cells("WAISTAGE").Value) Then
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
    '                Else
    '                    If dgMR.Rows(row.Index).Cells("WAISTAGE").Value = Nothing Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", dgMR.Rows(row.Index).Cells("WAISTAGE").Value)
    '                    End If

    '                End If

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NAME", Trim(txtCustomerName.Text))



    '                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

    '                strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET              SMR_ADUJESTED_STATUS ='UPDATED' WHERE     (SERIAL = @SERIAL) AND (COM_ID = @COM_ID)"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(dgMR.Rows(row.Index).Cells("SN").Value))
    '                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


    '            End If

    '        Next

    '        '// Agrremtne Update 

    '        strSQL = "UPDATE  TBL_CUS_AGREEMENT SET  CUS_CODE =@CUS_CODE,  BILLING_METHOD =@BILLING_METHOD, SLAB_METHOD =@SLAB_METHOD, BILLING_PERIOD =@BILLING_PERIOD, MD_BY ='" & userSession & "', MD_DATE =GETDATE() , INV_STATUS=@INV_STATUS , MACHINE_TYPE=@MACHINE_TYPE ,AG_RENTAL_PRICE=@AG_RENTAL_PRICE  WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID)"


    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))

    '        If rbtnCommitment.Checked = True Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "COMMITMENT")
    '        End If
    '        If rbtnActual.Checked = True Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "ACTUAL")
    '        End If

    '        If rbtnRental.Checked = True Then

    '            dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "RENTAL")
    '        End If

    '        If Trim(txtRental.Text) = "" Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", DBNull.Value)
    '        Else
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", CDbl(Trim(txtRental.Text)))
    '        End If
    '        If rbtnInvStatusAll.Checked = True Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "ALL")
    '        End If
    '        If rbtnInvStatusIndividual.Checked = True Then
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "INDIVIDUAL")
    '        End If

    '        dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "BW")



    '        dbConnections.sqlCommand.Parameters.AddWithValue("@SLAB_METHOD", Trim(txtSlabMethod.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_PERIOD", Trim(txtBilPeriod.Text))

    '        If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


    '        'BW_RANGE_1
    '        'BW_RANGE_2
    '        'BW_RATE

    '        'COLOR_RANGE_1
    '        'COLOR_RANGE_2
    '        'COLOR_RATE
    '        'If IsNewAgreement = True Then '//  commitments will save only for new agreement creation
    '        If dgBw.Rows.Count > 0 Then


    '            strSQL = "DELETE FROM TBL_AG_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '            dbConnections.sqlCommand.ExecuteNonQuery()

    '            'Data grid Data feeding code

    '            For Each row As DataGridViewRow In dgBw.Rows

    '                If dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value <> Nothing Then
    '                    dbConnections.sqlCommand.Parameters.Clear()

    '                    strSQL = "INSERT   INTO            TBL_AG_BW_COMMITMENT(COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE) VALUES     (@COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE)"
    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(SelectedAgreement))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_1", dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_2", dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RATE", dgBw.Rows(row.Index).Cells("BW_RATE").Value)

    '                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
    '                End If


    '            Next
    '        End If


    '        dbConnections.sqlTransaction.Commit()
    '        If MsgNeed = True Then
    '            MessageBox.Show("Transaction Saved Successfully.", "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If


    '    Catch ex As Exception
    '        dbConnections.sqlTransaction.Rollback()
    '        inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '        MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally
    '        dbConnections.dReader.Close()
    '        connectionClose()

    '    End Try


    '    Return save
    'End Function

    Private Async Function GenerateInvoice() As Threading.Tasks.Task(Of Boolean)
        Try
            Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/generate-invoice"
            Dim rental As Double = 0
            Dim Adjustmentval As Double = 0

            Dim request As New InvoiceRequestModel()

            request.CompanyID = globalVariables.selectedCompanyID
            request.AgreementID = Trim(txtSelectedAG.Text)
            request.CustomerID = Trim(txtCustomerID.Text)
            request.PeriodStart = dtpStart.Value
            request.PeriodEnd = dtpEnd.Value
            request.InvoiceNo = ""
            Dim InvDateTime As DateTime = dtpInvoiceDate.Value
            InvDateTime = InvDateTime.Add(Today.Date.TimeOfDay)
            request.InvoiceDate = InvDateTime
            request.Address1 = Trim(txLocation1.Text)
            request.Address2 = Trim(txtLocation2.Text)
            request.Address3 = Trim(txtLocation3.Text)
            Dim Billing_Method As String = ""
            If rbtnActual.Checked = True Then
                Billing_Method = "Actual"
            End If
            If rbtnCommitment.Checked = True Then
                Billing_Method = "Commitment"
            End If
            If rbtnRental.Checked = True Then
                Billing_Method = "Rental"
            End If

            Dim Inv_Status As String = ""
            If rbtnInvStatusAll.Checked = True Then
                Inv_Status = "All"
            End If
            If rbtnInvStatusIndividual.Checked = True Then
                Inv_Status = "Individual"
            End If
            request.BillingMethod = Billing_Method
            request.InvoiceStatus = Inv_Status
            request.VATType = lblVatType.Text
            request.IsPrinted = False
            request.UserSession = userSession
            request.UserName = userName
            request.IsNBT = cbNBT.CheckState
            request.IsVAT = cbVAT.CheckState
            request.NBT2Percent = NBT2
            request.VATPercent = VAT
            If Trim(txtRental.Text) = "" Then
                rental = 0
                request.Rental = rental
            Else
                request.Rental = CDbl(Trim(txtRental.Text))
                rental = CDbl(Trim(txtRental.Text))
            End If

            If Trim(txtAdujstment.Text) = "" Then
                request.Adjustment = Nothing
                Adjustmentval = 0
            Else
                request.Adjustment = CDbl(Trim(txtAdujstment.Text))
                Adjustmentval = CDbl(Trim(txtAdujstment.Text))
            End If
            request.InvoiceValue = (CDbl(Trim(txtInvoiceValue.Text) + rental + Adjustmentval))
            request.RepCode = Trim(txtRepCode.Text)

            'Meter Reading 
            request.MeterReadings = New List(Of MeterReading)
            For Each row As DataGridViewRow In dgMR.Rows
                If row.IsNewRow Then Continue For

                Dim reading As New MeterReading()

                reading.SerialNo = If(IsDBNull(row.Cells("SN").Value) OrElse row.Cells("SN").Value Is Nothing,
                              "", row.Cells("SN").Value.ToString())

                reading.MakeModel = If(IsDBNull(row.Cells("MR_MAKE").Value) OrElse row.Cells("MR_MAKE").Value Is Nothing,
                               "", row.Cells("MR_MAKE").Value.ToString())

                reading.Location = If(IsDBNull(row.Cells("M_LOC").Value) OrElse row.Cells("M_LOC").Value Is Nothing,
                              "", row.Cells("M_LOC").Value.ToString())

                reading.StartReading = If(IsDBNull(row.Cells("START_MR").Value) OrElse Not IsNumeric(row.Cells("START_MR").Value),
                                  0, CDbl(row.Cells("START_MR").Value))

                reading.EndReading = If(IsDBNull(row.Cells("END_MR").Value) OrElse Not IsNumeric(row.Cells("END_MR").Value),
                                0, CDbl(row.Cells("END_MR").Value))

                reading.Copies = If(IsDBNull(row.Cells("MR_COPIES").Value) OrElse Not IsNumeric(row.Cells("MR_COPIES").Value),
                            0, CInt(row.Cells("MR_COPIES").Value))

                reading.Wastage = If(IsDBNull(row.Cells("WAISTAGE").Value) OrElse Not IsNumeric(row.Cells("WAISTAGE").Value),
                             Nothing, CDbl(row.Cells("WAISTAGE").Value))

                reading.ProductNo = If(IsDBNull(row.Cells("P_NO").Value) OrElse row.Cells("P_NO").Value Is Nothing,
                               "", row.Cells("P_NO").Value.ToString())

                request.MeterReadings.Add(reading)
            Next


            'Adding commitments 
            request.BwCommitments = New List(Of BwCommitment)
            For Each row As DataGridViewRow In dgBw.Rows
                If row.Index <> (dgBw.RowCount - 1) Then
                    Dim bwcommitment As New BwCommitment With {
                        .Rate = dgBw.Rows(row.Index).Cells("BW_RATE").Value.ToString(),
                        .Range1 = dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value.ToString(),
                        .Range2 = dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value.ToString()
                    }

                    If dgBw.Rows(row.Index).Cells("COMMI_BREAKUP").Value = Nothing Then
                        bwcommitment.CopyBreakup = DBNull.Value.ToString()
                    Else
                        bwcommitment.CopyBreakup = dgBw.Rows(row.Index).Cells("COMMI_BREAKUP").Value.ToString()
                    End If
                    request.BwCommitments.Add(bwcommitment)
                End If
            Next
            Dim jsonData As String = JsonConvert.SerializeObject(request)
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")

            Using client As New HttpClient()
                Try
                    Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
                    If response.IsSuccessStatusCode Then
                        Return True
                    Else
                        MessageBox.Show("API Error: " & response.StatusCode.ToString())
                        Return False
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                    Return False
                End Try
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Async Function Process_Invoice() As Threading.Tasks.Task(Of Boolean)
        Dim processed As Boolean = Await GenerateInvoice()
    End Function

    'Private Function Process_Invoice() As Boolean
    '    Process_Invoice = False
    '    Dim InvoiceNo As String = ""
    '    Dim IsEdit As Boolean = False
    '    Dim rental As Double = 0
    '    Dim Adjustmentval As Double = 0
    '    Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '    If conf = vbYes Then
    '        If isDataValid() = False Then
    '            Exit Function
    '        End If

    '        InvoiceNo = Genarate_INV_NO()
    '        Try
    '            If InvoiceNo IsNot "" Then
    '                connectionStaet()

    '                errorEvent = "Save"
    '                strSQL = " SELECT CASE WHEN EXISTS (SELECT     COM_ID FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') and INV_STATUS_T <> 'CANCELLED') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                If dbConnections.sqlCommand.ExecuteScalar Then
    '                    IsEdit = True
    '                    strSQL = "UPDATE    TBL_INVOICE_MASTER SET   INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, BILLING_METHOD =@BILLING_METHOD, INV_STATUS =@INV_STATUS, VAT_TYPE =@VAT_TYPE, INV_PRINTED =@INV_PRINTED, INV_BY =@INV_BY, INV_BY_NAME =@INV_BY_NAME, INV_HEADDING =@INV_HEADDING,IS_NBT=@IS_NBT ,IS_VAT=@IS_VAT,RENTAL_VAL=@RENTAL_VAL,INV_VAL=@INV_VAL,REP_CODE=@REP_CODE ,ADJUSTMENT=@ADJUSTMENT WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID) AND (CUS_ID =@CUS_ID) AND (INV_PERIOD_START = @INV_PERIOD_START) AND (INV_PERIOD_END = @INV_PERIOD_END)"
    '                    MessageBox.Show("Invoice already processed.", "Invalid attempt.", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '                    Exit Function

    '                Else
    '                    IsEdit = False
    '                    strSQL = "INSERT INTO TBL_INVOICE_MASTER (COM_ID, AG_ID, CUS_ID, INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, INV_STATUS, VAT_TYPE, INV_PRINTED, INV_BY, INV_BY_NAME, INV_HEADDING,IS_NBT,IS_VAT,RENTAL_VAL,INV_VAL,REP_CODE,ADJUSTMENT, VAT_P, NBT2_P) VALUES     (@COM_ID, @AG_ID, @CUS_ID, @INV_PERIOD_START, @INV_PERIOD_END, @INV_NO, @INV_DATE, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @INV_STATUS, @VAT_TYPE, @INV_PRINTED, @INV_BY, @INV_BY_NAME, @INV_HEADDING,@IS_NBT ,@IS_VAT,@RENTAL_VAL,@INV_VAL,@REP_CODE,@ADJUSTMENT, @VAT_P, @NBT2_P)"
    '                End If

    '                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)


    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PERIOD_START", dtpStart.Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PERIOD_END", dtpEnd.Value)


    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(InvoiceNo))

    '                Dim InvDateTome As DateTime = dtpInvoiceDate.Value
    '                InvDateTome = InvDateTome.Add(Today.Date.TimeOfDay)


    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_DATE", InvDateTome)

    '                'INV_DATE


    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))

    '                Dim Billing_Method As String = ""
    '                If rbtnActual.Checked = True Then
    '                    Billing_Method = "Actual"
    '                End If
    '                If rbtnCommitment.Checked = True Then
    '                    Billing_Method = "Commitment"
    '                End If
    '                If rbtnRental.Checked = True Then
    '                    Billing_Method = "Rental"
    '                End If

    '                Dim Inv_Status As String = ""
    '                If rbtnInvStatusAll.Checked = True Then
    '                    Inv_Status = "All"
    '                End If
    '                If rbtnInvStatusIndividual.Checked = True Then
    '                    Inv_Status = "Individual"
    '                End If

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", Billing_Method)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", Inv_Status)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_TYPE", Trim(lblVatType.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PRINTED", False)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_BY", userSession)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_BY_NAME", userName)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_HEADDING", "")
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_NBT", cbNBT.CheckState)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_VAT", cbVAT.CheckState)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_P", VAT)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@NBT2_P", NBT2)
    '                If Trim(txtRental.Text) = "" Then
    '                    rental = 0
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", DBNull.Value)
    '                Else
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", CDbl(Trim(txtRental.Text)))
    '                    rental = CDbl(Trim(txtRental.Text))
    '                End If

    '                If Trim(txtAdujstment.Text) = "" Then
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@ADJUSTMENT", DBNull.Value)
    '                    Adjustmentval = 0
    '                Else
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@ADJUSTMENT", CDbl(Trim(txtAdujstment.Text)))
    '                    Adjustmentval = CDbl(Trim(txtAdujstment.Text))
    '                End If

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_VAL", (CDbl(Trim(txtInvoiceValue.Text) + rental + Adjustmentval)))

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRepCode.Text))




    '                If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False


    '                For Each row As DataGridViewRow In dgMR.Rows

    '                    If dgMR.Rows(row.Index).Cells(0).Value <> "" Then
    '                        dbConnections.sqlCommand.Parameters.Clear()

    '                        If IsEdit = True Then
    '                            strSQL = "UPDATE    TBL_INVOICE_DET SET P_NO=@P_NO, MAKE_MODEL =@MAKE_MODEL, INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, BILLING_METHOD =@BILLING_METHOD, M_LOC =@M_LOC, START_MR =@START_MR,    END_MR =@END_MR, INV_COPIES =@INV_COPIES, WAISTAGE =@WAISTAGE WHERE     (COM_ID =@COM_ID) AND (AG_ID = @AG_ID) AND (CUS_ID = @CUS_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (SERIAL_NO =@SERIAL_NO)"
    '                        Else
    '                            strSQL = "INSERT INTO TBL_INVOICE_DET (COM_ID, AG_ID, CUS_ID, PERIOD_START, PERIOD_END, SERIAL_NO, INV_NO, MAKE_MODEL, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, M_LOC, START_MR, END_MR, INV_COPIES, WAISTAGE,P_NO) VALUES     (@COM_ID, @AG_ID, @CUS_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @INV_NO, @MAKE_MODEL, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @M_LOC, @START_MR, @END_MR, @INV_COPIES, @WAISTAGE,@P_NO) "
    '                        End If

    '                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", dgMR.Rows(row.Index).Cells("SN").Value)
    '                        If IsEdit = False Then
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", InvoiceNo)
    '                        End If

    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@MAKE_MODEL", dgMR.Rows(row.Index).Cells("MR_MAKE").Value)

    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))


    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", Billing_Method)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC", dgMR.Rows(row.Index).Cells("M_LOC").Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", dgMR.Rows(row.Index).Cells("START_MR").Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@END_MR", dgMR.Rows(row.Index).Cells("END_MR").Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_COPIES", dgMR.Rows(row.Index).Cells("MR_COPIES").Value)
    '                        If IsDBNull(dgMR.Rows(row.Index).Cells("WAISTAGE").Value) Then
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
    '                        Else
    '                            If dgMR.Rows(row.Index).Cells("WAISTAGE").Value = Nothing Then
    '                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
    '                            Else
    '                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", dgMR.Rows(row.Index).Cells("WAISTAGE").Value)
    '                            End If
    '                        End If

    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@P_NO", dgMR.Rows(row.Index).Cells("P_NO").Value)
    '                        If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False
    '                    End If

    '                Next


    '                strSQL = "UPDATE    TBL_METER_READING_MASTER SET  DATA_PROCESSED=1, DATA_PROCESSED_DATE=GETDATE(), DATA_PROCESSED_BY=@DATA_PROCESSED_BY  WHERE     (COM_ID = @COM_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (CUS_ID = @CUS_ID) AND (AG_ID =@AG_ID)"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)

    '                dbConnections.sqlCommand.Parameters.AddWithValue("@DATA_PROCESSED_BY", userSession)

    '                If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False




    '                If dgBw.Rows.Count > 0 Then


    '                    strSQL = "DELETE FROM TBL_INV_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_CODE = '" & Trim(txtSelectedAG.Text) & "') AND (INV_NO = '" & InvoiceNo & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "')"
    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    dbConnections.sqlCommand.ExecuteNonQuery()

    '                    'Data grid Data feeding code

    '                    For Each row As DataGridViewRow In dgBw.Rows

    '                        If row.Index <> (dgBw.RowCount - 1) Then

    '                            dbConnections.sqlCommand.Parameters.Clear()

    '                            strSQL = "INSERT INTO TBL_INV_BW_COMMITMENT                       (INV_NO, COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE, BW_COPY_BREAKUP) VALUES     (@INV_NO, @COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE, @BW_COPY_BREAKUP)"
    '                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", InvoiceNo)
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(txtSelectedAG.Text))
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_1", dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value)
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_2", dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RATE", dgBw.Rows(row.Index).Cells("BW_RATE").Value)
    '                            If dgBw.Rows(row.Index).Cells("COMMI_BREAKUP").Value = Nothing Then
    '                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_COPY_BREAKUP", DBNull.Value)
    '                            Else
    '                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_COPY_BREAKUP", dgBw.Rows(row.Index).Cells("COMMI_BREAKUP").Value)
    '                            End If

    '                            If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False


    '                        End If


    '                    Next
    '                End If


    '                dbConnections.sqlTransaction.Commit()

    '                txtInvoiceNo.Text = InvoiceNo

    '                MessageBox.Show("Invoiced Process Completed.", "Completed.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            End If
    '        Catch ex As Exception
    '            dbConnections.sqlTransaction.Rollback()
    '            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            dbConnections.dReader.Close()
    '            connectionClose()

    '        End Try
    '    End If

    '    Return Process_Invoice
    'End Function





    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False

        Try

        Catch ex As Exception
            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            connectionClose()

        End Try
        Return delete
    End Function

    Private Sub FormEdit()


    End Sub

#End Region

    Private Sub frmMeaterReading_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmMeaterReading_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmMeaterReading_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMeaterReading_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmMeaterReading_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmMeaterReading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
        cmbSearchCol.Items.Clear()
        cmbSearchCol.Items.Add("Serial")
        cmbSearchCol.Items.Add("MRef")
        cmbSearchCol.Items.Add("Loc")
        cmbSearchCol.Items.Add("Model")
        cmbSearchCol.SelectedIndex = 0
    End Sub

    Private Sub frmMeaterReading_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X3) " + PermissionReadingErrorMessgae, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            connectionClose()
        End Try

    End Sub
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''all functions of the form .......................................................
    '===================================================================================================================
#Region "Functions & Subs"
    Public Function IsFormClosing() As Boolean
        Dim stackTrace As System.Diagnostics.StackTrace = New System.Diagnostics.StackTrace
        For Each sf As System.Diagnostics.StackFrame In stackTrace.GetFrames
            If (sf.GetMethod.Name = WMCLOSE) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub saveBtnStatus()
        If frmMDImain.tsbtnSave.Enabled Then btnStatus(0) = True Else btnStatus(0) = False
        If frmMDImain.tsbtnNew.Enabled Then btnStatus(1) = True Else btnStatus(1) = False
        If frmMDImain.tsbtnEdit.Enabled Then btnStatus(2) = True Else btnStatus(2) = False
        If frmMDImain.tsbtnDelete.Enabled Then btnStatus(3) = True Else btnStatus(3) = False
        If frmMDImain.tsbtnPrint.Enabled Then btnStatus(4) = True Else btnStatus(4) = False
    End Sub

    Private Function Genarate_INV_NO() As String
        Genarate_INV_NO = ""
        errorEvent = "Generating Invoice No."
        connectionStaet()

        Try
            Dim cmd As New SqlCommand("GenerateInvoiceNumber", dbConnections.sqlConnection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@CompanyID", globalVariables.selectedCompanyID)
            Dim outputParam As New SqlParameter("@NextInvoiceNo", SqlDbType.VarChar, 100)
            outputParam.Direction = ParameterDirection.Output
            cmd.Parameters.Add(outputParam)

            cmd.ExecuteNonQuery()

            Genarate_INV_NO = outputParam.Value.ToString()

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X10) " & GenaralErrorMessage & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Function

    'Private Function Genarate_INV_NO() As String

    '    Genarate_INV_NO = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()


    '    Try
    '        strSQL = "SELECT     MAX(INV_NO) as 'max' FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"


    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            Genarate_INV_NO = globalVariables.selectedCompanyID & "/" & "INV" & "/" & 1
    '        Else
    '            Dim IRCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim IRID As Integer = 0
    '            IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            IRID = IRCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT     INV_NO  FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (INV_NO = '" & globalVariables.selectedCompanyID & "/INV/" & IRID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    IRID = IRID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                Genarate_INV_NO = IRCodeSplit(0) & "/INV/" & IRID
    '            Else
    '                Exit Function
    '            End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally

    '        connectionClose()
    '    End Try
    'End Function

    'Private Async Function GenerateMeterReadingNo() As Threading.Tasks.Task(Of String)
    '    errorEvent = "Reading information"
    '    'Dim apiUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/meterreading/generatemeterreadingno?companyID={globalVariables.selectedCompanyID}"
    '    Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/generatemeterreadingno?companyID={globalVariables.selectedCompanyID}"
    '    Using client As New HttpClient
    '        Dim apiurlResponse As HttpResponseMessage = Await client.GetAsync(apiUrl)
    '        If apiurlResponse.IsSuccessStatusCode Then
    '            Dim json As String = Await apiurlResponse.Content.ReadAsStringAsync()
    '            Dim generateMRNo As String = JsonConvert.DeserializeObject(json)
    '            MeterReadingNo = generateMRNo
    '            Return generateMRNo
    '        End If
    '    End Using
    'End Function

    'Private Function GenarateMRNo()
    '    GenarateMRNo = ""
    '    GenarateMRNo = GenerateMeterReadingNo().ToString()
    'End Function

    Private Function GenarateMRNo() As String
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim parameters As New DynamicParameters()
                parameters.Add("@CompanyID", globalVariables.selectedCompanyID)
                parameters.Add("@NextMRID", dbType:=DbType.String, direction:=ParameterDirection.Output, size:=100)

                connection.Execute("GenerateMRID", parameters, commandType:=CommandType.StoredProcedure, commandTimeout:=30)

                Return parameters.Get(Of String)("@NextMRID")
            End Using

        Catch ex As Exception
            MessageBox.Show($"Error code({Me.Tag}X10) {GenaralErrorMessage}{ex.Message}",
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, $"{Me.Tag}X10", "Generating MR No", userSession, userName, DateTime.Now, ex.Message)
            Return String.Empty
        End Try
    End Function


    'Private Function GenarateMRNo() As String

    '    GenarateMRNo = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()


    '    Try
    '        strSQL = "SELECT     MAX(MR_ID) as 'max' FROM         TBL_METER_READING_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            GenarateMRNo = globalVariables.selectedCompanyID & "/" & "MR" & "/" & 1
    '        Else
    '            Dim IRCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim IRID As Integer = 0
    '            IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            IRID = IRCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT    MR_ID FROM         TBL_METER_READING_MASTER WHERE COM_ID  = '" & globalVariables.selectedCompanyID & "' AND MR_ID = '" & globalVariables.selectedCompanyID & "/" & "MR" & "/" & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    IRID = IRID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                GenarateMRNo = IRCodeSplit(0) & "/MR/" & IRID
    '            Else
    '                Exit Function
    '            End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally

    '        connectionClose()
    '    End Try
    'End Function


    'Private Function Genarate_INV_NO() As String

    '    Genarate_INV_NO = ""
    '    Dim preFixComName As String = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()

    '    Try
    '        strSQL = "SELECT     COM_PRE_FIX_NAME FROM         L_TBL_COMPANIES WHERE    (COM_ID = '" & globalVariables.selectedCompanyID & "')"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            preFixComName = globalVariables.selectedCompanyID
    '        Else
    '            preFixComName = dbConnections.sqlCommand.ExecuteScalar
    '        End If



    '        strSQL = "SELECT     MAX(INV_NO) as 'max' FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            Genarate_INV_NO = preFixComName & "/" & "INV" & "/" & 1
    '        Else
    '            Dim IRCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim IRID As Integer = 0
    '            IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            IRID = IRCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT    INV_NO FROM         TBL_INVOICE_MASTER WHERE COM_ID  = '" & globalVariables.selectedCompanyID & "' AND INV_NO = '" & preFixComName & "/" & "INV" & "/" & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    IRID = IRID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                Genarate_INV_NO = IRCodeSplit(0) & "/INV/" & IRID
    '            Else
    '                Exit Function
    '            End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally

    '        connectionClose()
    '    End Try
    'End Function


    Dim sqlCMD As New SqlCommand

    Public Async Function LoadSelectedAgreementOptimizedAsync() As Task
        If String.IsNullOrWhiteSpace(txtSelectedAG.Text) Then Exit Function

        Dim agId As String = txtSelectedAG.Text.Trim()
        Dim customerId As String = txtCustomerID.Text.Trim()
        Dim companyId As String = globalVariables.selectedCompanyID

        dgMR.Rows.Clear()

        dbConnections.sqlConnection.Close()

        Try
            Using connection As New SqlConnection(connectionString)
                Await connection.OpenAsync()
                Dim batchSql As String = "
                -- Query 1: Billing Period
                SELECT ISNULL(BILLING_PERIOD, 1) AS BillingPeriod 
                FROM TBL_CUS_AGREEMENT 
                WHERE AG_ID = @AgId AND COM_ID = @CompanyId;
                
                -- Query 2: Last Billing Date
                SELECT ISNULL(MAX(PERIOD_START), GETDATE()) AS LastBillingDate 
                FROM TBL_TBL_METER_READING_DET 
                WHERE COM_ID = @CompanyId AND AG_ID = @AgId;
                
                -- Query 3: Customer/Location Info
                IF @CompanyId = '003'
                BEGIN
                    SELECT CUS_NAME, CUS_ADD1, CUS_ADD2 
                    FROM MTBL_CUSTOMER_MASTER 
                    WHERE COM_ID = @CompanyId AND CUS_ID = @CustomerId;
                END
                ELSE
                BEGIN
                    SELECT TOP 1 M_LOC1 AS CUS_NAME, M_LOC2 AS CUS_ADD1, M_LOC3 AS CUS_ADD2 
                    FROM TBL_MACHINE_TRANSACTIONS 
                    WHERE COM_ID = @CompanyId AND AG_ID = @AgId;
                END
                
                -- Query 4: Check if Invoiced
                SELECT CASE 
                    WHEN EXISTS (
                        SELECT 1 FROM TBL_INVOICE_MASTER 
                        WHERE COM_ID = @CompanyId 
                        AND AG_ID = @AgId 
                        AND INV_PERIOD_START = @PeriodStart 
                        AND INV_PERIOD_END = @PeriodEnd
                    ) THEN CAST(1 AS BIT) 
                    ELSE CAST(0 AS BIT) 
                END AS IsInvoiced;
                
                -- Query 5: Machine Data with Last Reading (OPTIMIZED)
                SELECT 
                    MM.MACHINE_MODEL,
                    MT.SERIAL,
                    MT.P_NO,
                    MT.M_DEPT,
                    MT.MACHINE_PN,
                    ISNULL(LR.END_MR, 0) AS L_READING,
                    MT.START_MR,
                    MT.SMR_ADUJESTED_STATUS
                FROM TBL_MACHINE_TRANSACTIONS MT
                INNER JOIN MTBL_MACHINE_MASTER MM 
                    ON MT.COM_ID = MM.COM_ID AND MT.MACHINE_PN = MM.MACHINE_ID
                OUTER APPLY (
                    SELECT TOP 1 END_MR 
                    FROM TBL_TBL_METER_READING_DET 
                    WHERE SERIAL_NO = MT.SERIAL AND COM_ID = @CompanyId
                    ORDER BY TRANS_ID DESC
                ) LR
                WHERE MT.COM_ID = @CompanyId 
                AND MT.CUS_ID = @CustomerId 
                AND MT.AG_ID = @AgId
                ORDER BY MT.P_NO DESC;
                
                -- Query 6: Existing Meter Readings for Period
                SELECT 
                    SERIAL_NO,
                    START_MR,
                    END_MR,
                    COPIES,
                    WAISTAGE
                FROM TBL_TBL_METER_READING_DET
                WHERE COM_ID = @CompanyId 
                AND AG_ID = @AgId 
                AND CUS_ID = @CustomerId
                AND PERIOD_START = @PeriodStart 
                AND PERIOD_END = @PeriodEnd;
                
                -- Query 7: Adjustment
                SELECT ADJUSTMENT 
                FROM TBL_METER_READING_MASTER
                WHERE COM_ID = @CompanyId 
                AND AG_ID = @AgId 
                AND CUS_ID = @CustomerId
                AND PERIOD_START = @PeriodStart 
                AND PERIOD_END = @PeriodEnd;
                
                -- Query 8: Agreement Details
                SELECT 
                    CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, 
                    AG_PERIOD_START, AG_PERIOD_END, INV_STATUS, MACHINE_TYPE,
                    AG_RENTAL_PRICE, REP_CODE
                FROM TBL_CUS_AGREEMENT
                WHERE COM_ID = @CompanyId 
                AND CUS_CODE = @CustomerId 
                AND AG_ID = @AgId;
            "

                Dim parameters As New DynamicParameters()
                parameters.Add("@AgId", agId)
                parameters.Add("@CustomerId", customerId)
                parameters.Add("@CompanyId", companyId)
                parameters.Add("@PeriodStart", dtpStart.Value.ToString("yyyy-MM-dd"))
                parameters.Add("@PeriodEnd", dtpEnd.Value.ToString("yyyy-MM-dd"))

                Using multi = Await connection.QueryMultipleAsync(batchSql, parameters)
                    Dim billingPeriod As Integer = Await multi.ReadFirstOrDefaultAsync(Of Integer)()
                    Dim lastBillingDate As DateTime = Await multi.ReadFirstAsync(Of DateTime)()
                    Dim locationInfo = Await multi.ReadFirstOrDefaultAsync()

                    If locationInfo IsNot Nothing Then
                        txLocation1.Text = If(locationInfo.CUS_NAME, "")
                        txtLocation2.Text = If(locationInfo.CUS_ADD1, "")
                        txtLocation3.Text = If(locationInfo.CUS_ADD2, "")
                    End If

                    Dim isInvoiced As Boolean = Await multi.ReadFirstOrDefaultAsync(Of Boolean)()
                    Dim machines = (Await multi.ReadAsync(Of MachineData)()).ToList()
                    Dim existingReadings = (Await multi.ReadAsync(Of ExistingReading)()).ToList()
                    Dim readingsDict = existingReadings.ToDictionary(Function(r) r.SERIAL_NO)
                    Dim adjustment = Await multi.ReadFirstOrDefaultAsync()
                    txtAdujstment.Text = If(adjustment?.ADJUSTMENT, "")
                    ' Read Query 8: Agreement Details
                    Dim agreementDetails = Await multi.ReadFirstOrDefaultAsync()
                    PopulateAgreementDetails(agreementDetails)
                    For Each machine In machines
                        Dim lastMR As Integer = machine.L_READING
                        Dim endReading As String = ""
                        Dim waistage As String = ""
                        Dim copies As Integer = 0

                        Dim existingReading As ExistingReading = Nothing
                        If readingsDict.TryGetValue(machine.SERIAL, existingReading) Then
                            lastMR = If(existingReading.START_MR, machine.L_READING)
                            endReading = If(existingReading.END_MR?.ToString(), "")
                            waistage = If(existingReading.WAISTAGE?.ToString(), "")
                            copies = If(existingReading.COPIES, 0)
                        Else
                            ' No existing reading - check for reset or use machine START_MR
                            If machine.SMR_ADUJESTED_STATUS = "PENDING CAPTURE" Then
                                lastMR = If(machine.START_MR, lastMR)
                            ElseIf lastMR = 0 Then
                                lastMR = If(machine.START_MR, 0)
                            End If
                        End If

                        Dim pNo As String = If(String.IsNullOrEmpty(machine.P_NO), "0", machine.P_NO)
                        populatreDatagrid(
                                            machine.MACHINE_MODEL,
                                            machine.SERIAL,
                                            pNo,
                                            machine.M_DEPT,
                                            lastMR,
                                            endReading,
                                            copies,
                                            waistage
                                        )
                    Next
                End Using
                Dim AdjustmentsQuery As String = "
                -- Query 1: Get Adjustment
                SELECT ADJUSTMENT 
                FROM TBL_METER_READING_MASTER
                WHERE COM_ID = @COM_ID 
                AND AG_ID = @AG_ID 
                AND CUS_ID = @CUS_ID
                AND PERIOD_START = @PERIOD_START 
                AND PERIOD_END = @PERIOD_END;
                
                -- Query 2: Get Agreement Details
                SELECT 
                    CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, 
                    AG_PERIOD_START, AG_PERIOD_END, INV_STATUS, MACHINE_TYPE,
                    AG_RENTAL_PRICE, REP_CODE
                FROM TBL_CUS_AGREEMENT
                WHERE COM_ID = @COM_ID 
                AND CUS_CODE = @CUS_ID 
                AND AG_ID = @AG_ID;"
                Dim parametersAdjustment As New DynamicParameters()
                parametersAdjustment.Add("@AG_ID", txtSelectedAG.Text.Trim())
                parametersAdjustment.Add("@CUS_ID", txtCustomerID.Text.Trim())
                parametersAdjustment.Add("@COM_ID", globalVariables.selectedCompanyID.Trim())
                parametersAdjustment.Add("@PERIOD_START", dtpStart.Value.ToString("yyyy-MM-dd"))
                parametersAdjustment.Add("@PERIOD_END", dtpEnd.Value.ToString("yyyy-MM-dd"))
                Using multi = connection.QueryMultiple(AdjustmentsQuery, parametersAdjustment)
                    Dim adjustmentResult = multi.ReadFirstOrDefault(Of AdjustmentData)()
                    If adjustmentResult Is Nothing Then
                        txtAdujstment.Text = ""
                    Else
                        txtAdujstment.Text = adjustmentResult.ADJUSTMENT
                    End If
                    Dim agreement = multi.ReadFirstOrDefault(Of AgreementData)()
                    If agreement IsNot Nothing Then
                        PopulateAgreementDetails(agreement)
                    End If
                End Using
            End Using
            LoadCommitments(txtSelectedAG.Text.Trim(), txtCustomerID.Text.Trim())
            CalculateInvoiceValue()
            GetLastInvInfo(txtCustomerID.Text.Trim(), txtSelectedAG.Text.Trim())
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    '// New Populate Agreement details function 
    Public Sub PopulateAgreementDetails(agreementDetails As Object)
        If agreementDetails Is Nothing Then Return

        rbtnActual.Checked = False
        rbtnCommitment.Checked = False
        rbtnRental.Checked = False

        Dim billingMethod As String = agreementDetails.BILLING_METHOD
        If Not String.IsNullOrEmpty(billingMethod) Then
            Select Case billingMethod.ToUpper()
                Case "COMMITMENT"
                    rbtnCommitment.Checked = True
                Case "ACTUAL"
                    rbtnActual.Checked = True
                Case "RENTAL"
                    rbtnRental.Checked = True
            End Select
        End If

        ' Other fields
        txtSlabMethod.Text = If(agreementDetails.SLAB_METHOD, "")
        txtBilPeriod.Text = If(agreementDetails.BILLING_PERIOD?.ToString(), "")

        ' Invoice Status
        rbtnInvStatusAll.Checked = False
        rbtnInvStatusIndividual.Checked = False

        Dim invStatus As String = agreementDetails.INV_STATUS
        If Not String.IsNullOrEmpty(invStatus) Then
            If invStatus.ToUpper() = "ALL" Then
                rbtnInvStatusAll.Checked = True
            ElseIf invStatus.ToUpper() = "INDIVIDUAL" Then
                rbtnInvStatusIndividual.Checked = True
            End If
        End If

        txtRental.Text = If(agreementDetails.AG_RENTAL_PRICE IsNot Nothing,
                            Format(CDbl(agreementDetails.AG_RENTAL_PRICE), "0.00"), "")
        txtRepCode.Text = If(agreementDetails.REP_CODE, "")
    End Sub

    'Private Sub LoadSelectedAgreement()
    '    If Trim(txtSelectedAG.Text) = "" Then
    '        Exit Sub
    '    End If
    '    Dim MkeModel As String
    '    Dim BillingPeriod As Integer = 1
    '    Dim LastBillingDate As DateTime
    '    Dim IsPrevousRecordLoading As Boolean = False
    '    Dim lastMR As Integer = 0
    '    Dim EndReading As String = ""
    '    Dim Waistage As String = ""
    '    Dim Copies As Integer = 0
    '    Dim SN As String
    '    Dim PNo As String
    '    Dim MLoc As String
    '    Dim IsMRhave As Boolean = False

    '    dgMR.Rows.Clear()
    '    Try

    '        '// get billing period            

    '        dbConnections.sqlCommand.Parameters.Clear()
    '        strSQL = "SELECT    ISNULL( BILLING_PERIOD,1) FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID = '" & Trim(txtSelectedAG.Text) & "') and (COM_ID = '" & globalVariables.selectedCompanyID & "')"
    '        BillingPeriod = dbConnections.sqlConnection.Query(Of Integer)(strSQL).FirstOrDefault()

    '        '// get last billing date range
    '        strSQL = "SELECT   ISNULL( MAX(PERIOD_START),GETDATE()) AS Expr1  FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "')"
    '        LastBillingDate = dbConnections.sqlConnection.Query(Of DateTime)(strSQL).FirstOrDefault()
    '        'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        'LastBillingDate = dbConnections.sqlCommand.ExecuteScalar

    '        If globalVariables.selectedCompanyID = "003" Then
    '            'dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2 FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID)"
    '            Dim customerInfo As CustomerLocationVM = dbConnections.sqlConnection.Query(Of CustomerLocationVM)(strSQL, New With {.COM_ID = globalVariables.selectedCompanyID, .CUS_ID = Trim(txtCustomerID.Text)}).FirstOrDefault()
    '            'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            'dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '            'dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '            'dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
    '                    txLocation1.Text = ""
    '                Else
    '                    txLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
    '                End If



    '                If IsDBNull(dbConnections.dReader.Item("CUS_ADD1")) Then
    '                    txtLocation2.Text = ""
    '                Else
    '                    txtLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
    '                End If
    '                If IsDBNull(dbConnections.dReader.Item("CUS_ADD2")) Then
    '                    txtLocation3.Text = ""
    '                Else
    '                    txtLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
    '                End If

    '            End While
    '            dbConnections.dReader.Close()
    '        Else '// this will take machine location as invoice address

    '            dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT     TOP (1) M_LOC1, M_LOC2, M_LOC3 FROM         TBL_MACHINE_TRANSACTIONS  WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID)"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read

    '                txLocation1.Text = dbConnections.dReader.Item("M_LOC1")
    '                txtLocation2.Text = dbConnections.dReader.Item("M_LOC2")
    '                txtLocation3.Text = dbConnections.dReader.Item("M_LOC3")
    '            End While
    '            dbConnections.dReader.Close()

    '        End If


    '        dbConnections.sqlCommand.Parameters.Clear()

    '        dbConnections.sqlCommand.Parameters.Clear()
    '        Dim IsInvoiced As Boolean = False
    '        strSQL = "SELECT CASE WHEN EXISTS (SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If dbConnections.sqlCommand.ExecuteScalar Then
    '            IsInvoiced = True
    '        Else
    '            IsInvoiced = False
    '        End If

    '        strSQL = "SELECT     MTBL_MACHINE_MASTER.MACHINE_MAKE, MTBL_MACHINE_MASTER.MACHINE_MODEL, TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO, TBL_MACHINE_TRANSACTIONS.M_DEPT, (SELECT  TOP 1  isnull( END_MR,0) FROM         TBL_TBL_METER_READING_DET WHERE     (SERIAL_NO = TBL_MACHINE_TRANSACTIONS.SERIAL) AND (COM_ID = '" & globalVariables.selectedCompanyID & "') ORDER BY TRANS_ID  DESC) as 'L_READING' FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN MTBL_MACHINE_MASTER ON TBL_MACHINE_TRANSACTIONS.COM_ID = MTBL_MACHINE_MASTER.COM_ID AND TBL_MACHINE_TRANSACTIONS.MACHINE_PN = MTBL_MACHINE_MASTER.MACHINE_ID WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.CUS_ID = @CUS_ID) AND (TBL_MACHINE_TRANSACTIONS.AG_ID =@AG_ID) ORDER BY TBL_MACHINE_TRANSACTIONS.P_NO DESC"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))


    '        Dim da As New SqlDataAdapter(sqlCommand)

    '        Dim ds As New DataSet()

    '        da.Fill(ds)

    '        Dim IsHasPreviousRecord As Boolean = False
    '        Dim Reset_CapturedMR As Double = 0
    '        Dim IsReset As Boolean = False
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            'ds.Tables(0).Rows(i).Item(0)
    '            SN = ds.Tables(0).Rows(i).Item(2)
    '            If IsDBNull(ds.Tables(0).Rows(i).Item(3)) Then
    '                PNo = ""
    '            Else
    '                PNo = ds.Tables(0).Rows(i).Item(3)
    '            End If
    '            IsReset = False
    '            EndReading = ""
    '            If IsDBNull(ds.Tables(0).Rows(i).Item("L_READING")) Then
    '                lastMR = 0
    '            Else
    '                lastMR = ds.Tables(0).Rows(i).Item("L_READING")
    '            End If


    '            Waistage = ""
    '            Copies = 0
    '            IsMRhave = False
    '            IsHasPreviousRecord = False
    '            MLoc = ds.Tables(0).Rows(i).Item(4)
    '            MkeModel = ds.Tables(0).Rows(i).Item(1)
    '            dbConnections.dReader.Close()
    '            strSQL = "SELECT     START_MR, END_MR, COPIES, WAISTAGE FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (SERIAL_NO = '" & SN & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                IsHasPreviousRecord = True
    '                If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                    If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
    '                        lastMR = 0
    '                    Else
    '                        lastMR = ds.Tables(0).Rows(i).Item(5)
    '                    End If
    '                Else
    '                    lastMR = dbConnections.dReader.Item("START_MR")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("END_MR")) Then
    '                    EndReading = ""
    '                Else
    '                    EndReading = dbConnections.dReader.Item("END_MR")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("WAISTAGE")) Then
    '                    Waistage = ""
    '                Else
    '                    Waistage = dbConnections.dReader.Item("WAISTAGE")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("COPIES")) Then
    '                    Copies = 0
    '                Else
    '                    Copies = dbConnections.dReader.Item("COPIES")
    '                End If

    '            End While
    '            dbConnections.dReader.Close()

    '            'If IsMRhave = False Then

    '            If IsHasPreviousRecord = False Then



    '                '// get First meter reading  form master transaction
    '                strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL) AND (SMR_ADUJESTED_STATUS='PENDING CAPTURE')"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                While dbConnections.dReader.Read
    '                    IsMRhave = True
    '                    IsReset = True
    '                    If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                        Reset_CapturedMR = lastMR
    '                    Else
    '                        Reset_CapturedMR = dbConnections.dReader.Item("START_MR")
    '                    End If

    '                End While
    '                dbConnections.dReader.Close()

    '                If IsReset = True Then
    '                    lastMR = Reset_CapturedMR
    '                Else
    '                    If lastMR = 0 Then

    '                        '// get First meter reading  form master transaction
    '                        strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
    '                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                        While dbConnections.dReader.Read
    '                            IsMRhave = True
    '                            If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                                lastMR = lastMR
    '                            Else
    '                                lastMR = dbConnections.dReader.Item("START_MR")
    '                            End If

    '                        End While
    '                        dbConnections.dReader.Close()
    '                    Else

    '                        lastMR = lastMR

    '                    End If


    '                End If

    '                'If lastMR = 0 Or IsReset = True Then

    '                '    If IsReset = True Then
    '                '        lastMR = Reset_CapturedMR
    '                '    End If
    '                '    If lastMR = 0 Then
    '                '        lastMR = lastMR
    '                '    End If

    '                'Else
    '                '    '// get First meter reading  form master transaction
    '                '    strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
    '                '    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                '    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                '    dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                '    dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                '    While dbConnections.dReader.Read
    '                '        IsMRhave = True
    '                '        If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                '            lastMR = lastMR
    '                '        Else
    '                '            lastMR = dbConnections.dReader.Item("START_MR")
    '                '        End If

    '                '    End While
    '                '    dbConnections.dReader.Close()

    '                '    If IsMRhave = False Then
    '                '        If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
    '                '            lastMR = 0
    '                '        Else
    '                '            lastMR = ds.Tables(0).Rows(i).Item(5)
    '                '        End If

    '                '    End If
    '                'End If



    '            End If

    '            'End If
    '            If PNo = "" Then
    '                PNo = "0"
    '            End If


    '            populatreDatagrid(MkeModel, SN, PNo, MLoc, lastMR, EndReading, Copies, Waistage)


    '        Next


    '        dbConnections.sqlCommand.Parameters.Clear()
    '        strSQL = "SELECT     ADJUSTMENT FROM         TBL_METER_READING_MASTER WHERE     (COM_ID = '" & Trim(globalVariables.selectedCompanyID) & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("ADJUSTMENT")) Then
    '                txtAdujstment.Text = ""
    '            Else
    '                txtAdujstment.Text = dbConnections.dReader.Item("ADJUSTMENT")
    '            End If

    '        End While
    '        dbConnections.dReader.Close()

    '        Try
    '            dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE,AG_RENTAL_PRICE,REP_CODE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read


    '                If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
    '                    rbtnActual.Checked = False
    '                    rbtnCommitment.Checked = False
    '                    rbtnRental.Checked = False
    '                Else
    '                    rbtnActual.Checked = False
    '                    rbtnCommitment.Checked = False
    '                    rbtnRental.Checked = False

    '                    If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
    '                        rbtnCommitment.Checked = True
    '                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
    '                        rbtnActual.Checked = True
    '                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "RENTAL" Then
    '                        rbtnRental.Checked = True
    '                    Else
    '                        rbtnActual.Checked = False
    '                        rbtnCommitment.Checked = False
    '                        rbtnRental.Checked = False
    '                    End If

    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
    '                    txtSlabMethod.Text = ""
    '                Else
    '                    txtSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
    '                    txtBilPeriod.Text = ""
    '                Else
    '                    txtBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("INV_STATUS")) Then
    '                    rbtnInvStatusAll.Checked = False
    '                    rbtnInvStatusIndividual.Checked = False
    '                Else
    '                    If dbConnections.dReader.Item("INV_STATUS") = "ALL" Then
    '                        rbtnInvStatusAll.Checked = True
    '                    ElseIf dbConnections.dReader.Item("INV_STATUS") = "INDIVIDUAL" Then
    '                        rbtnInvStatusIndividual.Checked = True
    '                    Else
    '                        rbtnInvStatusAll.Checked = False
    '                        rbtnInvStatusIndividual.Checked = False
    '                    End If

    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("AG_RENTAL_PRICE")) Then
    '                    txtRental.Text = ""
    '                Else
    '                    txtRental.Text = Format(dbConnections.dReader.Item("AG_RENTAL_PRICE"), "0.00")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("REP_CODE")) Then
    '                    txtRepCode.Text = ""
    '                Else
    '                    txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")
    '                End If


    '            End While
    '            dbConnections.dReader.Close()
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try

    '        LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))



    '        CalculateInvoiceValue()
    '        GetLastInvInfo(Trim(txtCustomerID.Text), Trim(txtSelectedAG.Text))

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try


    'End Sub


    'Private Async Sub LoadSelectedAgreement()
    '    If Trim(txtSelectedAG.Text) = "" Then
    '        Exit Sub
    '    End If
    '    Dim MkeModel As String
    '    Dim BillingPeriod As Integer = 1
    '    Dim LastBillingDate As DateTime
    '    Dim IsPrevousRecordLoading As Boolean = False
    '    Dim lastMR As Integer = 0
    '    Dim EndReading As String = ""
    '    Dim Waistage As String = ""
    '    Dim Copies As Integer = 0
    '    Dim SN As String
    '    Dim PNo As String
    '    Dim MLoc As String
    '    Dim IsMRhave As Boolean = False

    '    dgMR.Rows.Clear()

    '    Try
    '        Using client As New HttpClient()
    '            'Get the billing Period
    '            Dim apiUrl01 As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getbillingperiod?companyID={globalVariables.selectedCompanyID}&AgreementID={Trim(txtSelectedAG.Text)}"
    '            Dim apiUrlResponse As HttpResponseMessage = Await client.GetAsync(apiUrl01)
    '            If apiUrlResponse.IsSuccessStatusCode Then
    '                Dim json As String = Await apiUrlResponse.Content.ReadAsStringAsync()
    '                Dim serializeJson01 As String = JsonConvert.DeserializeObject(json)
    '                BillingPeriod = serializeJson01
    '            End If

    '            'Get the last billing Period
    '            Dim apiUrl02 As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getlastbillingdate?companyID={globalVariables.selectedCompanyID}&AgreementID={txtSelectedAG.Text}"
    '            Dim apiUrl02Response As HttpResponseMessage = Await client.GetAsync(apiUrl02)
    '            If apiUrl02Response.IsSuccessStatusCode Then
    '                Dim json As String = Await apiUrl02Response.Content.ReadAsStringAsync()
    '                Dim serializeJson02 As String = JsonConvert.DeserializeObject(json)
    '                LastBillingDate = serializeJson02
    '            End If

    '            'Check for company id 003 
    '            If globalVariables.selectedCompanyID = "003" Then
    '                'Get customer details information 
    '                Dim customerDetailsApi As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getcustomerlocationinfo?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}"
    '                Dim customerDetailsResponse As HttpResponseMessage = Await client.GetAsync(customerDetailsApi)
    '                If customerDetailsResponse.IsSuccessStatusCode Then
    '                    Dim json As String = Await customerDetailsResponse.Content.ReadAsStringAsync()
    '                    Dim data As List(Of CustomerLocationVM) = JsonConvert.DeserializeObject(Of List(Of CustomerLocationVM))(json)
    '                    For Each item As CustomerLocationVM In data
    '                        txLocation1.Text = item.customerName
    '                        txtLocation2.Text = item.customerAddress01
    '                        txtLocation3.Text = item.customerAddress02
    '                    Next
    '                End If
    '            Else
    '                Dim machineLocationAPI As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getmachinelocation?AgreementID={txtSelectedAG.Text}&companyID={globalVariables.selectedCompanyID}"
    '                Dim machineLocationResponse As HttpResponseMessage = Await client.GetAsync(machineLocationAPI)
    '                If machineLocationResponse.IsSuccessStatusCode Then
    '                    Dim json As String = Await machineLocationResponse.Content.ReadAsStringAsync()
    '                    Dim data As List(Of MachineLocationVM) = JsonConvert.DeserializeObject(Of List(Of MachineLocationVM))(json)
    '                    For Each item As MachineLocationVM In data
    '                        txLocation1.Text = item.MachineLocation1
    '                        txtLocation2.Text = item.MachineLocation2
    '                        txtLocation3.Text = item.MachineLocation3
    '                    Next
    '                End If
    '            End If

    '            'Check for is invoiced
    '            Dim IsInvoiced As Boolean = False
    '            Dim query As String = "SELECT CASE WHEN EXISTS (SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '            dbConnections.sqlCommand = New SqlCommand(query, dbConnections.sqlConnection)
    '            If dbConnections.sqlCommand.ExecuteScalar Then
    '                IsInvoiced = True
    '            Else
    '                IsInvoiced = False
    '            End If

    '            'get machine transactions info 
    '            Dim IsHasPreviousRecord As Boolean = False
    '            Dim Reset_CapturedMR As Double = 0
    '            Dim IsReset As Boolean = False
    '            Dim machineTransactionsApi As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getmachinereadingmaster?companyID={globalVariables.selectedCompanyID}&customerID={txtCustomerID.Text}&agreementID={txtSelectedAG.Text}"
    '            Dim machineTransactionsResponse As HttpResponseMessage = Await client.GetAsync(machineTransactionsApi)
    '            If machineTransactionsResponse.IsSuccessStatusCode Then
    '                Dim json As String = Await machineTransactionsResponse.Content.ReadAsStringAsync()
    '                Dim data As List(Of MachineTransactionsVM) = JsonConvert.DeserializeObject(Of List(Of MachineTransactionsVM))(json)
    '                For Each item As MachineTransactionsVM In data
    '                    SN = item.SERIAL
    '                    PNo = item.P_NO
    '                    IsReset = False
    '                    EndReading = ""
    '                    lastMR = item.LAST_MR

    '                    Waistage = ""
    '                    Copies = 0
    '                    IsMRhave = False
    '                    IsHasPreviousRecord = False
    '                    MLoc = item.M_DEPT
    '                    MkeModel = $"{item.MACHINE_MAKE} - {item.MACHINE_MODEL}"

    '                    Dim tablemeterreadingAPI As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getmeterwaistage?customerID={txtCustomerID.Text}&AgreementID={txtSelectedAG.Text}&serialNo={SN}&periodStart={dtpStart.Value.ToString("yyyy/MM/dd")}&periodEnd={dtpEnd.Value.ToString("yyyy/MM/dd")}&companyID={globalVariables.selectedCompanyID}"
    '                    Dim tablemeterreadingResponse As HttpResponseMessage = Await client.GetAsync(tablemeterreadingAPI)
    '                    If tablemeterreadingResponse.IsSuccessStatusCode Then
    '                        Dim tableMeterreadingJson As String = Await tablemeterreadingResponse.Content.ReadAsStringAsync()
    '                        Dim tableMeterreadingDeserialized As List(Of MachineReadingDetailsInformation) = JsonConvert.DeserializeObject(Of List(Of MachineReadingDetailsInformation))(tableMeterreadingJson)
    '                        For Each tableMeterReadingItem As MachineReadingDetailsInformation In tableMeterreadingDeserialized
    '                            IsHasPreviousRecord = True
    '                            lastMR = tableMeterReadingItem.START_MR
    '                            EndReading = tableMeterReadingItem.END_MR
    '                            Waistage = tableMeterReadingItem.WAISTAGE
    '                            Copies = tableMeterReadingItem.COPIES

    '                            If IsHasPreviousRecord = False Then
    '                                Dim firstmeterReadingApi As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getfirstmeterreading?companyID={globalVariables.selectedCompanyID}&serialNo={SN}"
    '                                Dim firstmeterreadingResponse As HttpResponseMessage = Await client.GetAsync(firstmeterReadingApi)
    '                                If firstmeterreadingResponse.IsSuccessStatusCode Then
    '                                    Dim firstMeterReadingJson As String = Await machineTransactionsResponse.Content.ReadAsStringAsync()
    '                                    Dim firstMeterReadingSerialize As String = JsonConvert.DeserializeObject(firstMeterReadingJson)
    '                                    IsMRhave = True
    '                                    IsReset = True
    '                                    Reset_CapturedMR = firstMeterReadingSerialize
    '                                End If
    '                            End If

    '                            If PNo = "" Then
    '                                PNo = "0"
    '                            End If
    '                        Next
    '                    End If
    '                    populatreDatagrid(MkeModel, SN, PNo, MLoc, lastMR, EndReading, Copies, Waistage)
    '                Next

    '                'Get adjustments  
    '                Dim getadjustmentsAPI As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getadjustment?companyID={globalVariables.selectedCompanyID}&agreementID={txtSelectedAG.Text}&customerID={txtCustomerID.Text}&periodStart={dtpStart.Value.ToString("yyyy/MM/dd")}&periodEnd={dtpEnd.Value.ToString("yyyy/MM/dd")}"
    '                Dim getadjustmentsResponse As HttpResponseMessage = Await client.GetAsync(getadjustmentsAPI)
    '                If getadjustmentsResponse.IsSuccessStatusCode Then
    '                    Dim adjustmentsJson As String = Await getadjustmentsResponse.Content.ReadAsStringAsync()
    '                    Dim adjustmentsSerialize As String = JsonConvert.DeserializeObject(adjustmentsJson)
    '                    txtAdujstment.Text = adjustmentsSerialize
    '                End If

    '                Dim customerAgreementAPI As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/customeragreement?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}&AgreementID={txtSelectedAG.Text}"
    '                Dim customerAgreementResponse As HttpResponseMessage = Await client.GetAsync(customerAgreementAPI)
    '                If customerAgreementResponse.IsSuccessStatusCode Then
    '                    Dim customerAgreementJson As String = Await customerAgreementResponse.Content.ReadAsStringAsync()
    '                    Dim customerAgreementData As List(Of CustomerAgreementVM) = JsonConvert.DeserializeObject(Of List(Of CustomerAgreementVM))(customerAgreementJson)
    '                    For Each customerDataItem As CustomerAgreementVM In customerAgreementData
    '                        If IsDBNull(customerDataItem.BillingPeriod) Then
    '                            rbtnActual.Checked = False
    '                            rbtnCommitment.Checked = False
    '                            rbtnRental.Checked = False
    '                        Else
    '                            rbtnActual.Checked = False
    '                            rbtnCommitment.Checked = False
    '                            rbtnRental.Checked = False

    '                            If customerDataItem.BillingPeriod = "COMMITMENT" Then
    '                                rbtnCommitment.Checked = True
    '                            ElseIf customerDataItem.BillingPeriod = "ACTUAL" Then
    '                                rbtnActual.Checked = True
    '                            ElseIf customerDataItem.BillingPeriod = "RENTAL" Then
    '                                rbtnRental.Checked = True
    '                            Else
    '                                rbtnActual.Checked = False
    '                                rbtnCommitment.Checked = False
    '                                rbtnRental.Checked = False
    '                            End If
    '                        End If

    '                        If IsDBNull(customerDataItem.SlabMethod) Then
    '                            txtSlabMethod.Text = ""
    '                        Else
    '                            txtSlabMethod.Text = customerDataItem.SlabMethod
    '                        End If

    '                        If IsDBNull(customerDataItem.BillingPeriod) Then
    '                            txtBilPeriod.Text = ""
    '                        Else
    '                            txtBilPeriod.Text = customerDataItem.BillingPeriod
    '                        End If

    '                        If IsDBNull(customerDataItem.InvoiceStatus) Then
    '                            rbtnInvStatusAll.Checked = False
    '                            rbtnInvStatusIndividual.Checked = False
    '                        Else
    '                            If customerDataItem.InvoiceStatus = "ALL" Then
    '                                rbtnInvStatusAll.Checked = True
    '                            ElseIf customerDataItem.InvoiceStatus = "INDIVIDUAL" Then
    '                                rbtnInvStatusIndividual.Checked = True
    '                            Else
    '                                rbtnInvStatusAll.Checked = False
    '                                rbtnInvStatusIndividual.Checked = False
    '                            End If

    '                        End If

    '                        If IsDBNull(customerDataItem.AgRentalPrice) Then
    '                            txtRental.Text = ""
    '                        Else
    '                            txtRental.Text = Format(customerDataItem.AgRentalPrice, "0.00")
    '                        End If

    '                        If IsDBNull(customerDataItem.RepCode) Then
    '                            txtRepCode.Text = ""
    '                        Else
    '                            txtRepCode.Text = customerDataItem.RepCode
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Try
    '        LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))
    '        'CalculateInvoiceValue()
    '        GetLastInvInfo(Trim(txtCustomerID.Text), Trim(txtSelectedAG.Text))
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Sub LoadSelectedAgreement()

    '    If Trim(txtSelectedAG.Text) = "" Then
    '        Exit Sub
    '    End If
    '    Dim MkeModel As String
    '    Dim BillingPeriod As Integer = 1
    '    Dim LastBillingDate As DateTime
    '    Dim IsPrevousRecordLoading As Boolean = False
    '    Dim lastMR As Integer = 0
    '    Dim EndReading As String = ""
    '    Dim Waistage As String = ""
    '    Dim Copies As Integer = 0
    '    Dim SN As String
    '    Dim PNo As String
    '    Dim MLoc As String
    '    Dim IsMRhave As Boolean = False

    '    dgMR.Rows.Clear()
    '    Try
    '        '// get billing period

    '        dbConnections.sqlCommand = New SqlCommand("GetBillingPeriod", dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure

    '        dbConnections.sqlCommand.Parameters.Add("@AG_ID", SqlDbType.NVarChar).Value = Trim(txtSelectedAG.Text)
    '        dbConnections.sqlCommand.Parameters.Add("@COM_ID", SqlDbType.NVarChar).Value = globalVariables.selectedCompanyID

    '        BillingPeriod = dbConnections.sqlCommand.ExecuteScalar()

    '        ' Optional: Handle DBNull
    '        If IsDBNull(BillingPeriod) Then
    '            BillingPeriod = 1
    '        End If

    '        'dbConnections.sqlCommand.Parameters.Clear()
    '        'strSQL = "SELECT    ISNULL( BILLING_PERIOD,1) FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID = '" & Trim(txtSelectedAG.Text) & "') and (COM_ID = '" & globalVariables.selectedCompanyID & "')"
    '        'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        'BillingPeriod = dbConnections.sqlCommand.ExecuteScalar

    '        '// get last billing date range

    '        Dim cmd As New SqlCommand("GetLastBillingDate", dbConnections.sqlConnection)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        cmd.Parameters.Add("@COM_ID", SqlDbType.NVarChar).Value = globalVariables.selectedCompanyID
    '        cmd.Parameters.Add("@AG_ID", SqlDbType.NVarChar).Value = Trim(txtSelectedAG.Text)

    '        LastBillingDate = cmd.ExecuteScalar()

    '        ' Optional: Ensure it's a DateTime
    '        If Not IsDBNull(LastBillingDate) Then
    '            LastBillingDate = CDate(LastBillingDate)
    '        Else
    '            LastBillingDate = DateTime.Now
    '        End If


    '        'strSQL = "SELECT   ISNULL( MAX(PERIOD_START),GETDATE()) AS Expr1  FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "')"
    '        'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        'LastBillingDate = dbConnections.sqlCommand.ExecuteScalar

    '        If globalVariables.selectedCompanyID = "003" Then
    '            dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2 FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID)"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
    '                    txLocation1.Text = ""
    '                Else
    '                    txLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
    '                End If



    '                If IsDBNull(dbConnections.dReader.Item("CUS_ADD1")) Then
    '                    txtLocation2.Text = ""
    '                Else
    '                    txtLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
    '                End If
    '                If IsDBNull(dbConnections.dReader.Item("CUS_ADD2")) Then
    '                    txtLocation3.Text = ""
    '                Else
    '                    txtLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
    '                End If

    '            End While
    '            dbConnections.dReader.Close()
    '        Else '// this will take machine location as invoice address
    '            dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT TOP (1) M_LOC1, M_LOC2, M_LOC3 FROM         TBL_MACHINE_TRANSACTIONS  WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID)"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read

    '                txLocation1.Text = dbConnections.dReader.Item("M_LOC1")
    '                txtLocation2.Text = dbConnections.dReader.Item("M_LOC2")
    '                txtLocation3.Text = dbConnections.dReader.Item("M_LOC3")
    '            End While
    '            dbConnections.dReader.Close()

    '        End If


    '        dbConnections.sqlCommand.Parameters.Clear()

    '        dbConnections.sqlCommand.Parameters.Clear()
    '        Dim IsInvoiced As Boolean = False
    '        strSQL = "SELECT CASE WHEN EXISTS (SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If dbConnections.sqlCommand.ExecuteScalar Then
    '            IsInvoiced = True
    '        Else
    '            IsInvoiced = False
    '        End If

    '        strSQL = "SELECT     MTBL_MACHINE_MASTER.MACHINE_MAKE, MTBL_MACHINE_MASTER.MACHINE_MODEL, TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO, TBL_MACHINE_TRANSACTIONS.M_DEPT, (SELECT  TOP 1  isnull( END_MR,0) FROM         TBL_TBL_METER_READING_DET WHERE     (SERIAL_NO = TBL_MACHINE_TRANSACTIONS.SERIAL) AND (COM_ID = '" & globalVariables.selectedCompanyID & "') ORDER BY TRANS_ID  DESC) as 'L_READING' FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN MTBL_MACHINE_MASTER ON TBL_MACHINE_TRANSACTIONS.COM_ID = MTBL_MACHINE_MASTER.COM_ID AND TBL_MACHINE_TRANSACTIONS.MACHINE_PN = MTBL_MACHINE_MASTER.MACHINE_ID WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.CUS_ID = @CUS_ID) AND (TBL_MACHINE_TRANSACTIONS.AG_ID =@AG_ID) ORDER BY TBL_MACHINE_TRANSACTIONS.P_NO DESC"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))


    '        Dim da As New SqlDataAdapter(sqlCommand)

    '        Dim ds As New DataSet()

    '        da.Fill(ds)

    '        Dim IsHasPreviousRecord As Boolean = False
    '        Dim Reset_CapturedMR As Double = 0
    '        Dim IsReset As Boolean = False
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            'ds.Tables(0).Rows(i).Item(0)
    '            SN = ds.Tables(0).Rows(i).Item(2)
    '            If IsDBNull(ds.Tables(0).Rows(i).Item(3)) Then
    '                PNo = ""
    '            Else
    '                PNo = ds.Tables(0).Rows(i).Item(3)
    '            End If
    '            IsReset = False
    '            EndReading = ""
    '            If IsDBNull(ds.Tables(0).Rows(i).Item("L_READING")) Then
    '                lastMR = 0
    '            Else
    '                lastMR = ds.Tables(0).Rows(i).Item("L_READING")
    '            End If


    '            Waistage = ""
    '            Copies = 0
    '            IsMRhave = False
    '            IsHasPreviousRecord = False
    '            MLoc = ds.Tables(0).Rows(i).Item(4)
    '            MkeModel = ds.Tables(0).Rows(i).Item(1)
    '            dbConnections.dReader.Close()
    '            strSQL = "SELECT     START_MR, END_MR, COPIES, WAISTAGE FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (SERIAL_NO = '" & SN & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                IsHasPreviousRecord = True
    '                If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                    If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
    '                        lastMR = 0
    '                    Else
    '                        lastMR = ds.Tables(0).Rows(i).Item(5)
    '                    End If
    '                Else
    '                    lastMR = dbConnections.dReader.Item("START_MR")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("END_MR")) Then
    '                    EndReading = ""
    '                Else
    '                    EndReading = dbConnections.dReader.Item("END_MR")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("WAISTAGE")) Then
    '                    Waistage = ""
    '                Else
    '                    Waistage = dbConnections.dReader.Item("WAISTAGE")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("COPIES")) Then
    '                    Copies = 0
    '                Else
    '                    Copies = dbConnections.dReader.Item("COPIES")
    '                End If

    '            End While
    '            dbConnections.dReader.Close()

    '            'If IsMRhave = False Then

    '            If IsHasPreviousRecord = False Then



    '                '// get First meter reading  form master transaction
    '                strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL) AND (SMR_ADUJESTED_STATUS='PENDING CAPTURE')"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                While dbConnections.dReader.Read
    '                    IsMRhave = True
    '                    IsReset = True
    '                    If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                        Reset_CapturedMR = lastMR
    '                    Else
    '                        Reset_CapturedMR = dbConnections.dReader.Item("START_MR")
    '                    End If

    '                End While
    '                dbConnections.dReader.Close()

    '                If IsReset = True Then
    '                    lastMR = Reset_CapturedMR
    '                Else
    '                    If lastMR = 0 Then

    '                        '// get First meter reading  form master transaction
    '                        strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
    '                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                        While dbConnections.dReader.Read
    '                            IsMRhave = True
    '                            If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                                lastMR = lastMR
    '                            Else
    '                                lastMR = dbConnections.dReader.Item("START_MR")
    '                            End If

    '                        End While
    '                        dbConnections.dReader.Close()
    '                    Else

    '                        lastMR = lastMR

    '                    End If


    '                End If

    '                'If lastMR = 0 Or IsReset = True Then

    '                '    If IsReset = True Then
    '                '        lastMR = Reset_CapturedMR
    '                '    End If
    '                '    If lastMR = 0 Then
    '                '        lastMR = lastMR
    '                '    End If

    '                'Else
    '                '    '// get First meter reading  form master transaction
    '                '    strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
    '                '    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                '    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                '    dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
    '                '    dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                '    While dbConnections.dReader.Read
    '                '        IsMRhave = True
    '                '        If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
    '                '            lastMR = lastMR
    '                '        Else
    '                '            lastMR = dbConnections.dReader.Item("START_MR")
    '                '        End If

    '                '    End While
    '                '    dbConnections.dReader.Close()

    '                '    If IsMRhave = False Then
    '                '        If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
    '                '            lastMR = 0
    '                '        Else
    '                '            lastMR = ds.Tables(0).Rows(i).Item(5)
    '                '        End If

    '                '    End If
    '                'End If



    '            End If

    '            'End If
    '            If PNo = "" Then
    '                PNo = "0"
    '            End If


    '            populatreDatagrid(MkeModel, SN, PNo, MLoc, lastMR, EndReading, Copies, Waistage)


    '        Next


    '        dbConnections.sqlCommand.Parameters.Clear()
    '        strSQL = "SELECT     ADJUSTMENT FROM         TBL_METER_READING_MASTER WHERE     (COM_ID = '" & Trim(globalVariables.selectedCompanyID) & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("ADJUSTMENT")) Then
    '                txtAdujstment.Text = ""
    '            Else
    '                txtAdujstment.Text = dbConnections.dReader.Item("ADJUSTMENT")
    '            End If

    '        End While
    '        dbConnections.dReader.Close()

    '        Try
    '            dbConnections.sqlCommand.Parameters.Clear()
    '            strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE,AG_RENTAL_PRICE,REP_CODE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read


    '                If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
    '                    rbtnActual.Checked = False
    '                    rbtnCommitment.Checked = False
    '                    rbtnRental.Checked = False
    '                Else
    '                    rbtnActual.Checked = False
    '                    rbtnCommitment.Checked = False
    '                    rbtnRental.Checked = False

    '                    If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
    '                        rbtnCommitment.Checked = True
    '                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
    '                        rbtnActual.Checked = True
    '                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "RENTAL" Then
    '                        rbtnRental.Checked = True
    '                    Else
    '                        rbtnActual.Checked = False
    '                        rbtnCommitment.Checked = False
    '                        rbtnRental.Checked = False
    '                    End If

    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
    '                    txtSlabMethod.Text = ""
    '                Else
    '                    txtSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
    '                    txtBilPeriod.Text = ""
    '                Else
    '                    txtBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("INV_STATUS")) Then
    '                    rbtnInvStatusAll.Checked = False
    '                    rbtnInvStatusIndividual.Checked = False
    '                Else
    '                    If dbConnections.dReader.Item("INV_STATUS") = "ALL" Then
    '                        rbtnInvStatusAll.Checked = True
    '                    ElseIf dbConnections.dReader.Item("INV_STATUS") = "INDIVIDUAL" Then
    '                        rbtnInvStatusIndividual.Checked = True
    '                    Else
    '                        rbtnInvStatusAll.Checked = False
    '                        rbtnInvStatusIndividual.Checked = False
    '                    End If

    '                End If


    '                If IsDBNull(dbConnections.dReader.Item("AG_RENTAL_PRICE")) Then
    '                    txtRental.Text = ""
    '                Else
    '                    txtRental.Text = Format(dbConnections.dReader.Item("AG_RENTAL_PRICE"), "0.00")
    '                End If

    '                If IsDBNull(dbConnections.dReader.Item("REP_CODE")) Then
    '                    txtRepCode.Text = ""
    '                Else
    '                    txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")
    '                End If


    '            End While
    '            dbConnections.dReader.Close()
    '        Catch ex As Exception
    '            MsgBox(ex.InnerException.Message)
    '        End Try

    '        LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))



    '        CalculateInvoiceValue()
    '        GetLastInvInfo(Trim(txtCustomerID.Text), Trim(txtSelectedAG.Text))

    '    Catch ex As Exception
    '        MsgBox(ex.InnerException.Message)
    '    End Try


    'End Sub

    Private Function GetResetMRIfPending(SN As String) As Integer?
        Dim cmd As New SqlCommand("
        SELECT START_MR FROM TBL_MACHINE_TRANSACTIONS 
        WHERE COM_ID = @COM_ID AND SERIAL = @SERIAL AND SMR_ADUJESTED_STATUS = 'PENDING CAPTURE'", dbConnections.sqlConnection)
        cmd.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
        cmd.Parameters.AddWithValue("@SERIAL", SN)

        Dim result = cmd.ExecuteScalar()
        If result IsNot Nothing AndAlso Not IsDBNull(result) Then
            Return Convert.ToInt32(result)
        End If
        Return Nothing
    End Function

    Private Sub LoadCommitments(ByRef AG_ID As String, ByRef CUS_ID As String)
        dgBw.Rows.Clear()
        Try
            '//SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)
            strSQL = "
            SELECT BW_RANGE_1 AS Range1, BW_RANGE_2 AS Range2, BW_RATE AS BWRate
            FROM TBL_AG_BW_COMMITMENT 
            WHERE (COM_ID = @COM_ID) AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim result = connection.Query(Of CommitmentVM)(strSQL, New With {
                    .COM_ID = globalVariables.selectedCompanyID,
                    .CUS_ID = Trim(CUS_ID),
                    .AG_CODE = Trim(AG_ID)
                })
                For Each item In result
                    populatreDatagrid_BW_Commitment(item.Range1, item.Range2, item.BWRate, 0)
                Next
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'dgBw.Rows.Clear()
        'Try
        '    strSQL = "SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
        '    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
        '    dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

        '    While dbConnections.dReader.Read

        '        populatreDatagrid_BW_Commitment(dbConnections.dReader.Item("BW_RANGE_1"), dbConnections.dReader.Item("BW_RANGE_2"), dbConnections.dReader.Item("BW_RATE"), 0)
        '    End While
        '    dbConnections.dReader.Close()
        'Catch ex As Exception
        '    MsgBox(ex.InnerException.Message)
        'End Try
    End Sub

    'Private Sub LoadCommitments(ByRef AG_ID As String, ByRef CUS_ID As String)
    '    dgBw.Rows.Clear()
    '    Try
    '        strSQL = "SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read

    '            populatreDatagrid_BW_Commitment(dbConnections.dReader.Item("BW_RANGE_1"), dbConnections.dReader.Item("BW_RANGE_2"), dbConnections.dReader.Item("BW_RATE"), 0)
    '        End While
    '        dbConnections.dReader.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.InnerException.Message)
    '    End Try



    'End Sub

    Private Sub populatreDatagrid_BW_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal, ByRef CopyCount As Integer)
        dgBw.ColumnCount = 4
        dgBw.Rows.Add(Range1, Range2, Rate, CopyCount)
    End Sub

    Private Sub populatreDatagrid(ByRef Make As String, ByRef SN As String, ByRef PNO As Integer, ByRef Location As String, ByRef StartReading As String, ByRef EndReading As String, ByRef Copies As String, ByRef Waistage As String)
        dgMR.ColumnCount = 8
        dgMR.Rows.Add(Make, SN, PNO, Location, StartReading, EndReading, Copies, Waistage)
    End Sub

    Private Sub populatreDatagrAgreements(ByRef AG_ID As String, ByRef AG_NAME As String, ByRef Image As Image)
        dgAgreement.ColumnCount = 3
        dgAgreement.Rows.Add(AG_ID, AG_NAME, Image)
    End Sub


    Private Sub CalculateInvoiceValue()
        Dim CopyCount As Integer = 0
        Dim Waistage As Integer = 0
        Dim InvCopyCount As Integer = 0
        Dim CommitmentBreakup As Integer = 0
        '// Actual Calculation variables
        Dim ARate As Decimal = 0


        '// Commitment Calculation variables
        '// Slab 2
        Dim isCapturedRange As Boolean = False
        Dim CapturedLastRate As Double = 0
        '// Master variables
        Dim InvoiceValue As Double
        Dim NBTval As Double
        Dim VATVal As Double
        Dim NetValue As Double
        Dim BwNBT As Double
        Dim BwVAT As Double
        Dim AdjustMntVal As Double = 0
        Dim SelectedVAT As Integer = 0
        Dim SelectedNBT As Double = 0


        SelectedVAT = GetSelectedVATP()
        SelectedNBT = GetSelectedNBT2P()



        '// Rental
        Dim rental_Val As Double = 0

        Try
            For Each row As DataGridViewRow In dgMR.Rows
                If dgMR.Rows(row.Index).Cells("MR_COPIES").Value <> Nothing Then
                    CopyCount = CopyCount + dgMR.Rows(row.Index).Cells("MR_COPIES").Value
                End If

                If dgMR.Rows(row.Index).Cells("WAISTAGE").Value <> Nothing Then
                    Waistage = Waistage + dgMR.Rows(row.Index).Cells("WAISTAGE").Value
                End If

            Next

            txtTotalCopies.Text = CopyCount
            txtTotalWaistage.Text = Waistage
            InvCopyCount = (CopyCount - Waistage)
            txtInvCopies.Text = InvCopyCount
            Dim CopyCountVal As Integer = 0 '// this user to crystal report bug fixign


            If Trim(txtAdujstment.Text) <> "" Then
                AdjustMntVal = CDbl(Trim(txtAdujstment.Text))
            End If

            For Each row2 As DataGridViewRow In dgBw.Rows
                dgBw.Item(3, row2.Index).Value = Nothing
            Next




            If rbtnActual.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then
                    '// slab confirm
                    ARate = dgBw.Item(2, 0).Value


                    InvoiceValue = (InvCopyCount * ARate)
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * SelectedNBT)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * SelectedVAT)
                    Else
                        VATVal = 0
                    End If
                    InvoiceValue = InvoiceValue + AdjustMntVal
                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)
                    dgBw.Item(3, 0).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database
                ElseIf Trim(txtSlabMethod.Text) = "SLAB-2" Then
                    '// slab confirm
                    CommitmentBreakup = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value <> Nothing Then



                            If (InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value) Then



                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                                dgBw.Item(3, row.Index).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database

                                If row.Index > 0 Then
                                    dgBw.Item(3, row.Index - 1).Value = Nothing

                                End If
                                isCapturedRange = True
                                Exit For
                            Else
                                CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                isCapturedRange = False
                            End If

                            If isCapturedRange = False Then

                                InvoiceValue = CapturedLastRate * InvCopyCount


                                dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                If row.Index > 0 Then
                                    dgBw.Item(3, row.Index - 1).Value = Nothing

                                End If
                            End If

                        End If
                    Next
                    InvoiceValue = InvoiceValue + AdjustMntVal

                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * SelectedNBT)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * SelectedVAT)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)


                End If


            End If

            If rbtnCommitment.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then
                    Dim TotalCopies As Integer = InvCopyCount
                    Dim Diffreance As Integer = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        ''dgMR.Rows(row.Index).Cells("MR_COPIES").Value

                        If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value <> Nothing Then

                            If TotalCopies <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then
                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)

                                dgBw.Item(3, 0).Value = dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value  '// use to crystal report counting bug using bug fix from database
                            Else
                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * TotalCopies)
                                dgBw.Item(3, 0).Value = TotalCopies  '// use to crystal report counting bug using bug fix from database
                            End If

                        End If


                    Next
                    InvoiceValue = InvoiceValue + AdjustMntVal

                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * SelectedNBT)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * SelectedVAT)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)

                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)

                ElseIf Trim(txtSlabMethod.Text) = "SLAB-2" Then
                    '// slab confirm
                    'For Each row2 As DataGridViewRow In dgBw.Rows
                    '    dgBw.Item(3, row2.Index).Value = Nothing
                    'Next
                    Dim IsFirstRecord As Boolean = True
                    Dim CurrentCopyCoyunt As Integer = InvCopyCount
                    Dim LastReadIndex As Integer = 0
                    Dim x As Integer = 0
                    Dim Diffrence As Integer = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        Diffrence = 0

                        'If row.Index = dgBw.RowCount - 2 Then

                        'Else
                        '    Diffrence = (dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value - dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value) + 1
                        'End If
                        Diffrence = (dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value - dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value) + 1
                        If dgBw.Rows(row.Index).Cells("BW_RATE").Value <> Nothing Then
                            If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value = 0 Then

                                InvoiceValue = InvoiceValue + (InvCopyCount * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database

                            ElseIf row.Index = 0 Then

                                If CurrentCopyCoyunt <= Diffrence Then
                                    InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                    dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    Exit For
                                Else
                                    InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                    dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                                End If

                            ElseIf CurrentCopyCoyunt <= Diffrence Then
                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If

                            ElseIf row.Index = dgBw.RowCount - 2 Then

                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                'CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence


                            ElseIf CurrentCopyCoyunt >= Diffrence Then
                                InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                            Else

                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If




                            End If

                        End If

                    Next
                    InvoiceValue = InvoiceValue + AdjustMntVal
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * SelectedNBT)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * SelectedVAT)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)


                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)


                ElseIf Trim(txtSlabMethod.Text) = "SLAB-3" Then
                    '// slab confirmed
                    'For Each row2 As DataGridViewRow In dgBw.Rows
                    '    dgBw.Item(3, row2.Index).Value = Nothing
                    'Next                    
                    is_FirstRecord = True
                    Dim RowCount As Integer = dgBw.RowCount - 2
                    For Each row As DataGridViewRow In dgBw.Rows
                        Dim something As String = dgBw.Rows(row.Index).Cells("BW_RATE").Value

                        Console.WriteLine(something)

                        If something <> Nothing Then
                            If is_FirstRecord = True Then
                                If InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then

                                    InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                                    dgBw.Item(3, row.Index).Value = dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value  '// use to crystal report counting bug using bug fix from database
                                    isCapturedRange = True
                                    is_FirstRecord = False
                                    Exit For
                                Else
                                    isCapturedRange = False
                                    CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                    is_FirstRecord = False
                                End If

                            Else

                                If row.Index = RowCount Then

                                    InvoiceValue = (dgBw.Rows(RowCount).Cells("BW_RATE").Value * InvCopyCount)
                                    dgBw.Item(3, RowCount).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                    isCapturedRange = True
                                    Exit For

                                Else
                                    If InvCopyCount > dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then
                                        isCapturedRange = False
                                        CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                    Else
                                        InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                                        dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                        isCapturedRange = True
                                        Exit For
                                    End If
                                End If
                            End If
                        End If
                    Next


                    If isCapturedRange = False Then
                        InvoiceValue = (dgBw.Rows(RowCount).Cells("BW_RATE").Value * InvCopyCount)
                        dgBw.Item(3, RowCount).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                    End If

                    InvoiceValue = InvoiceValue + AdjustMntVal
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * SelectedNBT)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * SelectedVAT)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)

                End If
            End If

            If rbtnRental.Checked = True Then
                InvoiceValue = 0

                dgBw.Item(3, 0).Value = Nothing


                Dim ExceedCommitment As Integer = 0
                Dim ExceededCopyValue As Double = 0
                Dim RentalNBT As Double = 0
                Dim RentalVAT As Double = 0

                If Trim(txtRental.Text) = "" Then
                    rental_Val = 0
                    txtRental.Text = 0
                Else
                    rental_Val = CDbl(txtRental.Text)
                    txtRental.Text = rental_Val.ToString("N2")
                End If
                If dgBw.Item(1, 0).Value = 1 Then '// Actual Rentals 

                    ExceedCommitment = InvCopyCount
                    InvoiceValue = (ExceedCommitment * dgBw.Item(2, 0).Value)
                ElseIf InvCopyCount <= dgBw.Item(1, 0).Value Then
                    InvoiceValue = 0
                Else
                    ExceedCommitment = (InvCopyCount - dgBw.Item(1, 0).Value)
                    InvoiceValue = (ExceedCommitment * dgBw.Item(2, 0).Value)
                End If

                'InvoiceValue = (InvCopyCount * dgBw.Item(2, 0).Value)
                dgBw.Item(3, 0).Value = ExceedCommitment
                txtTotalCopies.Text = ExceedCommitment

                InvoiceValue = InvoiceValue + AdjustMntVal
                If cbNBT.Checked = True Then
                    BwNBT = (InvoiceValue / 100) * SelectedNBT
                Else
                    BwNBT = 0
                End If

                If cbVAT.Checked = True Then
                    BwVAT = ((InvoiceValue + BwNBT) / 100) * SelectedVAT
                Else
                    BwVAT = 0
                End If



                txtInvoiceValue.Text = (InvoiceValue).ToString("N2")



                If cbNBT.Checked = True Then
                    RentalNBT = (rental_Val / 100) * SelectedNBT
                Else
                    RentalNBT = 0
                End If

                If cbVAT.Checked = True Then
                    RentalVAT = ((rental_Val + RentalNBT) / 100) * SelectedVAT
                Else
                    RentalVAT = 0
                End If



                txtNBT.Text = (BwNBT + RentalNBT).ToString("N2")
                txtVAT.Text = (BwVAT + RentalVAT).ToString("N2")

                NetValue = (InvoiceValue + BwNBT + BwVAT) + (rental_Val + RentalNBT + RentalVAT)
                txtNetValue.Text = NetValue.ToString("N2")
            End If


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Exit Sub
    End Sub


    Private Sub Calculate_Net_Value(ByRef InvoiceValue As Double, ByRef NBTval As Double, ByRef VATVal As Double, ByRef NetValue As Double)
        txtInvoiceValue.Text = InvoiceValue.ToString("N2")
        txtNBT.Text = NBTval.ToString("N2")
        txtVAT.Text = VATVal.ToString("N2")
        txtNetValue.Text = NetValue.ToString("N2")
    End Sub
    Private Sub IsInvoiced()
        Try
            Dim RM As New Resources.ResourceManager("KBCO.Resources", System.Reflection.Assembly.GetExecutingAssembly)
            Dim startDate As String = dtpStart.Value.ToString("yyyy/MM/dd")
            Dim endDate As String = dtpEnd.Value.ToString("yyyy/MM/dd")

            ' Step 1: Fetch all invoice master records
            Dim invoiceData As New Dictionary(Of String, Boolean) ' AG_ID -> INV_PRINTED
            Dim strSQL As String = "SELECT AG_ID, INV_PRINTED FROM TBL_INVOICE_MASTER " &
                               "WHERE INV_PERIOD_START = @StartDate AND INV_PERIOD_END = @EndDate"
            Using cmd As New SqlCommand(strSQL, dbConnections.sqlConnection)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim agId = rdr("AG_ID").ToString()
                        Dim isPrinted = Not IsDBNull(rdr("INV_PRINTED")) AndAlso Convert.ToBoolean(rdr("INV_PRINTED"))
                        If Not invoiceData.ContainsKey(agId) Then
                            invoiceData.Add(agId, isPrinted)
                        End If
                    End While
                End Using
            End Using

            ' Step 2: Fetch all meter reading master records
            Dim meterData As New HashSet(Of String) ' AG_IDs that have meter readings
            strSQL = "SELECT AG_ID FROM TBL_METER_READING_MASTER " &
                 "WHERE PERIOD_START = @StartDate AND PERIOD_END = @EndDate"
            Using cmd As New SqlCommand(strSQL, dbConnections.sqlConnection)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim agId = rdr("AG_ID").ToString()
                        If Not meterData.Contains(agId) Then
                            meterData.Add(agId)
                        End If
                    End While
                End Using
            End Using

            ' Step 3: Apply logic row-by-row
            For Each row As DataGridViewRow In dgAgreement.Rows
                Dim agId = row.Cells(0).Value?.ToString()
                If String.IsNullOrEmpty(agId) Then Continue For

                Dim status As String = "NOT SAVED"

                If invoiceData.ContainsKey(agId) Then
                    status = If(invoiceData(agId), "PRINTED", "INVOICED")
                ElseIf meterData.Contains(agId) Then
                    status = "SAVED"
                End If

                ' Step 4: Set image based on status
                Select Case status
                    Case "PRINTED"
                        row.Cells(2).Value = RM.GetObject("B1")
                    Case "INVOICED"
                        row.Cells(2).Value = RM.GetObject("O1")
                    Case "SAVED"
                        row.Cells(2).Value = RM.GetObject("B2")
                    Case Else
                        row.Cells(2).Value = RM.GetObject("C1")
                End Select
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub IsInvoiced()
    '    Dim IsCaptured As Boolean = False
    '    Dim InvStatus As String = ""
    '    Dim RM As ResourceManager
    '    RM = New Resources.ResourceManager("KBCO.Resources", System.Reflection.Assembly.GetExecutingAssembly)
    '    Try
    '        For Each row As DataGridViewRow In dgAgreement.Rows

    '            ' there are 4 stages as 
    '            '1 Not Saved 
    '            '2 Saved
    '            '3 Invoiced
    '            '4 Invoice Printed
    '            IsCaptured = False

    '            strSQL = "SELECT     INV_PRINTED FROM         TBL_INVOICE_MASTER WHERE     (AG_ID = '" & dgAgreement.Rows(row.Index).Cells(0).Value & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '                InvStatus = ""
    '            Else
    '                If dbConnections.sqlCommand.ExecuteScalar = True Then
    '                    InvStatus = "PRINTED"
    '                    IsCaptured = True
    '                Else
    '                    InvStatus = ""
    '                End If
    '            End If

    '            If IsCaptured = False Then
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT AG_ID FROM TBL_INVOICE_MASTER WHERE (AG_ID = '" & dgAgreement.Rows(row.Index).Cells(0).Value & "') AND (INV_PERIOD_START =  '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                If dbConnections.sqlCommand.ExecuteScalar = True Then
    '                    InvStatus = "INVOICED"
    '                    IsCaptured = True
    '                Else
    '                    InvStatus = ""
    '                End If
    '            End If



    '            If IsCaptured = False Then
    '                '// Not Saved 
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT AG_ID FROM TBL_METER_READING_MASTER WHERE (AG_ID = '" & dgAgreement.Rows(row.Index).Cells(0).Value & "') AND (PERIOD_START =  '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                If dbConnections.sqlCommand.ExecuteScalar = True Then
    '                    InvStatus = "SAVED"
    '                Else
    '                    InvStatus = "NOT SAVED"
    '                End If

    '            End If






    '            If InvStatus = "PRINTED" Then
    '                dgAgreement.Rows(row.Index).Cells(2).Value = RM.GetObject("B1")
    '            ElseIf InvStatus = "INVOICED" Then
    '                dgAgreement.Rows(row.Index).Cells(2).Value = RM.GetObject("O1")
    '            ElseIf InvStatus = "SAVED" Then
    '                dgAgreement.Rows(row.Index).Cells(2).Value = RM.GetObject("B2")
    '            Else
    '                dgAgreement.Rows(row.Index).Cells(2).Value = RM.GetObject("C1")
    '            End If





    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.InnerException.Message)
    '    End Try

    'End Sub

    Private Sub GetLastInvInfo(ByRef CusID As String, ByRef AgID As String)
        Try
            Dim isRecordHave As Boolean = False
            strSQL = "SELECT     TOP (1) INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE FROM    TBL_INVOICE_MASTER WHERE     (COM_ID ='" & globalVariables.selectedCompanyID & "') AND (CUS_ID =@CUS_ID) AND (AG_ID = @AG_ID) ORDER BY INV_DATE DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CusID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AgID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                isRecordHave = True
                If IsDBNull(dbConnections.dReader.Item("INV_NO")) Then
                    lblInvoiceNo.Text = ""
                Else
                    lblInvoiceNo.Text = dbConnections.dReader.Item("INV_NO")
                End If
                If IsDBNull(dbConnections.dReader.Item("INV_PERIOD_START")) Then
                    lblSDate.Text = ""
                Else
                    lblSDate.Text = dbConnections.dReader.Item("INV_PERIOD_START")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_PERIOD_END")) Then
                    lblEDate.Text = ""
                Else
                    lblEDate.Text = dbConnections.dReader.Item("INV_PERIOD_END")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_DATE")) Then
                    lblLInvDate.Text = ""
                Else
                    lblLInvDate.Text = dbConnections.dReader.Item("INV_DATE")
                End If


            End While

            If isRecordHave = False Then
                lblInvoiceNo.Text = ""
                lblSDate.Text = ""
                lblEDate.Text = ""
                lblLInvDate.Text = ""
            End If
            dbConnections.dReader.Close()
        Catch ex As Exception
            dbConnections.dReader.Close()
            '/MsgBox(ex.InnerException.Message)
        End Try
    End Sub


    Private Function GetSelectedVATP() As Double
        GetSelectedVATP = globalVariables.VAT

        If dtpEnd.Value > DateSerial(2019, 11, 10) Then
            GetSelectedVATP = globalVariables.VAT
        Else
            GetSelectedVATP = 18
        End If

        Return GetSelectedVATP
    End Function

    Private Function GetSelectedNBT2P() As Double
        GetSelectedNBT2P = globalVariables.NBT2

        If dtpEnd.Value > DateSerial(2019, 11, 10) Then
            GetSelectedNBT2P = globalVariables.NBT2
        Else
            GetSelectedNBT2P = 2.5
        End If

        Return GetSelectedNBT2P
    End Function


#End Region

#Region "Validation"

    Dim IsErrorHave As Boolean = False
    Private Function isDataValid()
        isDataValid = False

        ErrorProvider1.Clear()



        strSQL = "SELECT CASE WHEN EXISTS (SELECT     COM_ID FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (INV_STATUS_T <> 'CANCELLED')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
        If dbConnections.sqlCommand.ExecuteScalar Then
            MessageBox.Show("You don't have privilege to preform this action after processed.", "Already Processed.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If



        If generalValObj.isPresent(txtCustomerID) = False Then
            IsErrorHave = True
        End If
        If generalValObj.isPresent(txtCustomerName) = False Then
            IsErrorHave = True
        End If




        If generalValObj.isPresent(txLocation1) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtLocation2) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtLocation3) = False Then
            IsErrorHave = True
        End If





        For Each row As DataGridViewRow In dgMR.Rows
            If dgMR.Rows(row.Index).Cells(0).Value <> "" Then
                If dgMR.Rows(row.Index).Cells("MR_COPIES").Value < 0 Then
                    MessageBox.Show("Serial " & dgMR.Rows(row.Index).Cells("SN").Value & " Copy Count is minus.", "Minus Value Detected.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Function
                End If
            End If
        Next





        If IsErrorHave = True Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()

        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        txtSelectedAG.Text = ""
        txLocation1.Text = ""
        txtLocation2.Text = ""
        txtLocation3.Text = ""
        dgMR.Columns(2).HeaderText = globalVariables.MachineRefCode + " No"
        dgAgreement.Rows.Clear()
        txtCustomerID.Focus()
        txtRepCode.Text = ""
        lblInvoiceNo.Text = ""
        lblSDate.Text = ""
        lblEDate.Text = ""
        lblLInvDate.Text = ""
        txtRepCode.Text = ""
        'Dim CurrentDate As DateTime = Today.Date
        'Dim dateVal As Integer
        'Dim MonthVal As Integer
        'Dim YearVal As Integer
        'Dim NewStartDate As DateTime
        'Dim NewEndDate As DateTime
        'dateVal = 1
        'MonthVal = CurrentDate.Month
        'YearVal = CurrentDate.Year

        'NewStartDate = CDate(dateVal & "/" & MonthVal & "/" & YearVal)
        'NewEndDate = NewStartDate.AddMonths(1)
        'dtpStart.Value = NewStartDate
        'dtpEnd.Value = NewEndDate

        dtpEnd.Value = Today.Date
        dtpStart.Value = Today.Date.AddMonths(-1)


        dgMR.Rows.Clear()
        dgBw.Rows.Clear()
        txtSelectedAG.Text = ""
        rbtnActual.Checked = False
        rbtnCommitment.Checked = False
        rbtnInvStatusAll.Checked = False
        rbtnInvStatusIndividual.Checked = False
        txtSlabMethod.Text = ""
        txtBilPeriod.Text = ""
        txtTotalCopies.Text = ""
        txtTotalWaistage.Text = ""
        txtInvCopies.Text = ""
        txtInvoiceValue.Text = ""
        txtNBT.Text = ""
        txtVAT.Text = ""
        cbNBT.Checked = False
        cbVAT.Checked = False
        txtNetValue.Text = ""
        lblVatType.Text = "##"
        lblVatTypeName.Text = "##"



        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()
    End Sub



#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

    Private Sub txtCustomerID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Async Sub txtCustomerID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCustomerID.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        Try
            strSQL = $"
            SELECT MTBL_CUSTOMER_MASTER.CUS_NAME, 
            MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, 
            MTBL_VAT_MASTER.VAT_DESC, 
            MTBL_VAT_MASTER.IS_NBT, 
            MTBL_VAT_MASTER.IS_VAT 
            FROM 
            MTBL_CUSTOMER_MASTER 
            INNER JOIN  
            MTBL_VAT_MASTER ON 
            MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID 
            AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID 
            WHERE 
            (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
            Dim customerResult As CustomerInformation = dbConnections.sqlConnection.QuerySingleOrDefault(Of CustomerInformation)(strSQL, New With {.COM_ID = globalVariables.selectedCompanyID, .CUS_ID = Trim(txtCustomerID.Text)})
            txtCustomerName.Text = customerResult.CUS_NAME
            lblVatType.Text = customerResult.VAT_TYPE_ID
            lblVatTypeName.Text = customerResult.VAT_DESC
            If IsDBNull(customerResult.IS_NBT) Then
                cbNBT.Checked = False
            Else
                If customerResult.IS_NBT Then
                    cbNBT.Checked = True
                Else
                    cbNBT.Checked = False
                End If
            End If

            If IsDBNull(customerResult.IS_VAT) Then
                cbVAT.Checked = False
            Else
                If customerResult.IS_VAT Then
                    cbVAT.Checked = True
                Else
                    cbVAT.Checked = False
                End If
            End If
            dgAgreement.Rows.Clear()
            strSQL = "SELECT AG_ID,AG_NAME FROM TBL_CUS_AGREEMENT WHERE (COM_ID = @COM_ID) AND (CUS_CODE =@CUS_CODE) and (MACHINE_TYPE = 'BW')"
            Dim agreementLists As List(Of AgreementInformationVM) = dbConnections.sqlConnection.Query(Of AgreementInformationVM)(strSQL, New With {.COM_ID = globalVariables.selectedCompanyID, .CUS_CODE = Trim(txtCustomerID.Text)})
            Dim hasRecord As Boolean = False
            Dim AgreementName As String

            For Each item As AgreementInformationVM In agreementLists
                hasRecord = True
                If IsDBNull(item.AG_NAME) Then
                    AgreementName = item.AG_ID
                Else
                    AgreementName = item.AG_NAME
                End If
                populatreDatagrAgreements(item.AG_ID, AgreementName, Nothing)
                IsInvoiced()
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    '//Dim apiUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/meterreading/getcustomerinformation?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}"
    '    Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getcustomerinformation?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}"
    '    Using client As New HttpClient()
    '        Try
    '            Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
    '            Dim rowCount As Integer = 0
    '            If response.IsSuccessStatusCode Then
    '                Dim json As String = Await response.Content.ReadAsStringAsync()
    '                Dim data As List(Of CustomerInformation) = JsonConvert.DeserializeObject(Of List(Of CustomerInformation))(json)
    '                For Each item As CustomerInformation In data
    '                    txtCustomerName.Text = item.CUS_NAME
    '                    lblVatType.Text = item.VAT_TYPE_ID
    '                    lblVatTypeName.Text = item.VAT_DESC
    '                    cbNBT.Checked = item.IS_NBT
    '                    cbVAT.Checked = item.IS_VAT
    '                Next
    '            End If

    '            '//     apiUrl = $"{dbconnections.kbcoAPIEndPoint}/api/meterreading/getagreementinformation?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}"
    '            apiUrl = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/getagreementinformation?companyID={globalVariables.selectedCompanyID}&customerCode={txtCustomerID.Text}"
    '            Dim something As HttpResponseMessage = Await client.GetAsync(apiUrl)
    '            rowCount = 0
    '            If something.IsSuccessStatusCode Then
    '                Dim json As String = Await something.Content.ReadAsStringAsync()
    '                Dim data As List(Of AgreementInformationVM) = JsonConvert.DeserializeObject(Of List(Of AgreementInformationVM))(json)
    '                dgAgreement.Rows.Clear()
    '                dgAgreement.DataSource = Nothing
    '                For Each item As AgreementInformationVM In data
    '                    dgAgreement.Rows.Add(item.AG_ID, item.AG_NAME, Nothing)
    '                Next
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try

    '    End Using
    'End Sub

    'Private Sub txtCustomerID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCustomerID.Validating
    '    errorEvent = "Reading information"
    '    If IsFormClosing() Then Exit Sub
    '    If Not isFormFocused Then Exit Sub
    '    If Trim(sender.Text) = "" Then
    '        e.Cancel = True
    '        Exit Sub
    '    End If
    '    connectionStaet()
    '    Try
    '        strSQL = "SELECT     MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read

    '            txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
    '            lblVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
    '            lblVatTypeName.Text = dbConnections.dReader.Item("VAT_DESC")

    '            If IsDBNull(dbConnections.dReader.Item("IS_NBT")) Then
    '                cbNBT.Checked = False
    '            Else
    '                If dbConnections.dReader.Item("IS_NBT") Then
    '                    cbNBT.Checked = True
    '                Else
    '                    cbNBT.Checked = False
    '                End If
    '            End If


    '            If IsDBNull(dbConnections.dReader.Item("IS_VAT")) Then
    '                cbVAT.Checked = False
    '            Else
    '                If dbConnections.dReader.Item("IS_VAT") Then
    '                    cbVAT.Checked = True
    '                Else
    '                    cbVAT.Checked = False
    '                End If
    '            End If
    '        End While
    '        dbConnections.dReader.Close()

    '        Dim AgreementName As String = ""
    '        dgAgreement.Rows.Clear()
    '        dgAgreement.SuspendLayout()
    '        strSQL = "SELECT     AG_ID,AG_NAME FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = @COM_ID) AND (CUS_CODE =@CUS_CODE) and (MACHINE_TYPE = 'BW')"
    '        'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        'dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        'dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
    '        'dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        dgAgreement.AutoGenerateColumns = True

    '        Dim dt As New DataTable()
    '        dt.Columns.Add("AG_ID", GetType(String))
    '        dt.Columns.Add("AG_NAME", GetType(String))
    '        dt.Columns.Add("ST", GetType(Image)) ' Optional column for images

    '        Dim hasRecords As Boolean = False

    '        Using cmd As New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            cmd.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '            cmd.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))

    '            Using reader As SqlDataReader = cmd.ExecuteReader()
    '                While reader.Read()
    '                    Dim agID = reader("AG_ID").ToString()
    '                    Dim agName = If(IsDBNull(reader("AG_NAME")), agID, reader("AG_NAME").ToString())
    '                    dt.Rows.Add(agID, agName, Nothing) ' Add rows to DataTable
    '                End While
    '            End Using
    '        End Using

    '        ' Bind the DataTable to the DataGridView
    '        dgAgreement.DataSource = dt


    '        ' Set image layout and row height
    '        Dim imageCol As DataGridViewImageColumn = DirectCast(dgAgreement.Columns("ST"), DataGridViewImageColumn)
    '        imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom
    '        dgAgreement.RowTemplate.Height = 50

    '        dgAgreement.Columns(0).HeaderText = "Agreement ID"
    '        dgAgreement.Columns(1).HeaderText = "Agreement Name"
    '        'While dbConnections.dReader.Read
    '        '    hasRecords = True

    '        '    'dgAgreement.Rows.Add(dbConnections.dReader.Item("AG_ID"))

    '        '    If IsDBNull(dbConnections.dReader.Item("AG_NAME")) Then
    '        '        AgreementName = dbConnections.dReader.Item("AG_ID")
    '        '    Else
    '        '        AgreementName = dbConnections.dReader.Item("AG_NAME")
    '        '    End If
    '        '    populatreDatagrAgreements(dbConnections.dReader.Item("AG_ID"), AgreementName, Nothing)
    '        'End While
    '        dgAgreement.ResumeLayout()

    '        IsInvoiced()
    '        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
    '        Me.saveBtnStatus()

    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally
    '        dbConnections.dReader.Close()
    '        connectionClose()
    '    End Try

    'End Sub






#End Region




    Private Async Sub dgAgreement_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAgreement.CellClick
        Try
            txtSelectedAG.Text = dgAgreement.Item(0, e.RowIndex).Value
            Await LoadSelectedAgreementOptimizedAsync()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"

    'Private Sub dgMR_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgMR.CellFormatting

    '    dgMR.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)

    'End Sub


    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgMR.EditingControlShowing

        If Me.dgMR.CurrentCell.ColumnIndex = 5 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            tb.Name = "txtEndReading"

            AddHandler tb.Validating, AddressOf TextBox_Validating
        ElseIf Me.dgMR.CurrentCell.ColumnIndex = 7 And Not e.Control Is Nothing Then
            Dim txtWaistage As TextBox = CType(e.Control, TextBox)
            txtWaistage.Name = "txtWaistage"
            AddHandler txtWaistage.Validating, AddressOf txtWaistage_Validating

        End If

    End Sub




    Dim CurrentMRval As Integer
    Dim YieldPerItem As Integer
    Dim TotalYield As Integer
    Dim PreviousReading As Integer = 0
    Dim ReqQty As Integer
    Dim CurrentCopies As Integer



    Private Sub TextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(4, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value)
            End If


            AllCopieseThisMOnth = (EndReading - StartReading)


            'If rbtnRental.Checked = True Then

            '    If AllCopieseThisMOnth <= dgBw.Item(1, 0).Value Then
            '        AllCopieseThisMOnth = 0
            '    Else

            '        AllCopieseThisMOnth = (AllCopieseThisMOnth - dgBw.Item(1, 0).Value)
            '    End If

            'End If






            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try
        Exit Sub
    End Sub

    Private Sub txtWaistage_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(4, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value)
            End If

            AllCopieseThisMOnth = (EndReading - StartReading)

            If rbtnRental.Checked = True Then

                If AllCopieseThisMOnth <= dgBw.Item(1, 0).Value Then
                    AllCopieseThisMOnth = 0
                Else

                    AllCopieseThisMOnth = (AllCopieseThisMOnth - dgBw.Item(1, 0).Value)
                End If

            End If

            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try
        Exit Sub
    End Sub

#End Region


    Private Sub btnProcessInvoice_Click(sender As Object, e As EventArgs) Handles btnProcessInvoice.Click
        'If save(False) Then
        '    Process_Invoice()
        'End If
        Process_Invoice()
    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub

    Private Sub cbNBT_CheckedChanged(sender As Object, e As EventArgs) Handles cbNBT.CheckedChanged
        If cbNBT.Checked = True Then
            cbNBT.Text = "Yes"
        Else
            cbNBT.Text = "No"
        End If
        CalculateInvoiceValue()
    End Sub

    Private Sub cbVAT_CheckedChanged(sender As Object, e As EventArgs) Handles cbVAT.CheckedChanged
        If cbVAT.Checked = True Then
            cbVAT.Text = "Yes"
        Else
            cbVAT.Text = "No"
        End If
        CalculateInvoiceValue()
    End Sub


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        frmPrintInvoice.Text = Trim(txtInvoiceNo.Text)
        frmPrintInvoice.Show()
    End Sub

    Private Sub dtpEnd_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpEnd.Validating
        IsInvoiced()
    End Sub




    Private Sub dgMR_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgMR.CellContentClick

    End Sub

    Private Sub dgMR_CellToolTipTextChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgMR.CellToolTipTextChanged

    End Sub

    Private Sub dgAgreement_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAgreement.CellContentClick

    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        If Trim(txtInvoiceNo.Text) = "" Then
            Exit Sub
        End If

        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoice.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.INV_NO} = '" & Trim(txtInvoiceNo.Text) & "' and {TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"




            With crConnectionInfo
                .ServerName = selectedServerName
                .DatabaseName = selectedDatabaseName
                .UserID = "db_ab8b61_kbco_admin"
                .Password = "Ssg789.541351"
            End With

            CrTables = cryRpt.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        dbChnges.MdiParent = frmMDImain
        dbChnges.Show()
    End Sub


    Private Sub txtAdujstment_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtAdujstment.Validating
        CalculateInvoiceValue()
    End Sub

    Private Sub btnToExce_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToCsV(dGV As DataGridView, filename As String)
        Dim stOutput As String = ""
        ' Export titles:
        Dim sHeaders As String = ""

        For j As Integer = 0 To dGV.Columns.Count - 1
            sHeaders = sHeaders.ToString() & Convert.ToString(dGV.Columns(j).HeaderText) & vbTab
        Next
        stOutput += sHeaders & vbCr & vbLf
        ' Export data.
        For i As Integer = 0 To dGV.RowCount - 1
            Dim stLine As String = ""
            For j As Integer = 0 To dGV.Rows(i).Cells.Count - 1
                stLine = stLine.ToString() & Convert.ToString(dGV.Rows(i).Cells(j).Value) & vbTab
            Next
            stOutput += stLine & vbCr & vbLf
        Next
        Dim utf16 As Encoding = Encoding.GetEncoding(1254)
        Dim output As Byte() = utf16.GetBytes(stOutput)
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)
        bw.Write(output, 0, output.Length)
        'write the encoded file
        bw.Flush()
        bw.Close()
        fs.Close()

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (*.xls)|*.xls"
        'sfd.FileName = "" & Trim(txtRepCode.Text) & "SOBackup.xls"
        If sfd.ShowDialog() = DialogResult.OK Then
            ToCsV(dgMR, sfd.FileName)
        End If
    End Sub


    Private Sub bgworkerSNSerch_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgworkerSNSerch.DoWork
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        Dim Current_RowIndex As Integer = 0


        cmbSearchCol.Items.Add("Serial")
        cmbSearchCol.Items.Add("MRef")
        cmbSearchCol.Items.Add("Loc")
        cmbSearchCol.Items.Add("Model")
        Dim SelectedColomID As String = ""
        If cmbSearchCol.Text = "Serial" Then
            SelectedColomID = "SN"
        ElseIf cmbSearchCol.Text = "MRef" Then

            SelectedColomID = "P_NO"
        ElseIf cmbSearchCol.Text = "Loc" Then
            SelectedColomID = "M_LOC"
        ElseIf cmbSearchCol.Text = "Model" Then
            SelectedColomID = "MR_MAKE"
        Else

            SelectedColomID = ""
        End If


        If Trim(SelectedColomID) <> "" Then
            For i = Current_RowIndex To dgMR.RowCount
                If IsDBNull(dgMR.Rows(i).Cells("" & SelectedColomID & "").Value) = False Then

                    If dgMR.RowCount = i + 1 Then
                        Current_RowIndex = 0

                        MessageBox.Show("No records found.", "No records.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If


                    If dgMR.Rows(i).Cells("" & SelectedColomID & "").Value.ToString = Trim(txtSearchtext.Text) Then
                        dgMR.Rows(i).Selected = True
                        dgMR.FirstDisplayedScrollingRowIndex = i
                        Current_RowIndex = i + 1
                        Exit For
                    End If


                End If


                Current_RowIndex = i
            Next

        End If



    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        bgworkerSNSerch.RunWorkerAsync()

    End Sub

    Private Sub txtSelectedAG_TextChanged(sender As Object, e As EventArgs) Handles txtSelectedAG.TextChanged

    End Sub
End Class