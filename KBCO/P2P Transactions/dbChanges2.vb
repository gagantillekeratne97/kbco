Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Resources
Imports System.Net.Http
Imports Newtonsoft.Json


Public Class dbChanges2



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
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                Me.save()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                Me.delete()
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
    Private Function save() As Boolean
        save = False
        Dim IsEdit As Boolean = False
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If

            MR_ID = GenarateMRNo()
            Try


                connectionStaet()

                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction


                errorEvent = "Save"
                strSQL = "SELECT CASE WHEN EXISTS (SELECT  COM_ID FROM  TBL_METER_READING_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                If dbConnections.sqlCommand.ExecuteScalar Then
                    IsEdit = True
                    strSQL = "UPDATE    TBL_METER_READING_MASTER SET     INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3,IS_NBT=@IS_NBT ,IS_VAT=@IS_VAT,RENTAL_VAL=@RENTAL_VAL WHERE     (COM_ID = @COM_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (CUS_ID = @CUS_ID) AND (AG_ID =@AG_ID)"
                Else
                    IsEdit = False
                    strSQL = "INSERT INTO TBL_METER_READING_MASTER (COM_ID, MR_ID, CUS_ID, AG_ID, PERIOD_START, PERIOD_END, INV_ADD1, INV_ADD2, INV_ADD3, CR_BY, CR_DATE,IS_NBT,IS_VAT,RENTAL_VAL) VALUES     (@COM_ID, @MR_ID, @CUS_ID, @AG_ID, @PERIOD_START, @PERIOD_END, @INV_ADD1, @INV_ADD2, @INV_ADD3, '" & userSession & "', GETDATE(),@IS_NBT, @IS_VAT,@RENTAL_VAL)"
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)

                If IsEdit = False Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MR_ID", Trim(MR_ID))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_NBT", cbNBT.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_VAT", cbVAT.CheckState)
                If Trim(txtRental.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", CDbl(Trim(txtRental.Text)))
                End If

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                For Each row As DataGridViewRow In dgMR.Rows

                    If dgMR.Rows(row.Index).Cells(0).Value <> "" Then
                        dbConnections.sqlCommand.Parameters.Clear()


                        strSQL = "SELECT CASE WHEN EXISTS (SELECT     P_NO FROM         TBL_TBL_METER_READING_DET_COLOR WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (SERIAL_NO = '" & Trim(dgMR.Rows(row.Index).Cells("SN").Value) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        If dbConnections.sqlCommand.ExecuteScalar Then
                            IsEdit = True
                            strSQL = "UPDATE    TBL_TBL_METER_READING_DET_COLOR SET  P_NO =@P_NO, MR_MAKE_MODEL =@MR_MAKE_MODEL, M_LOC =@M_LOC, END_MR=@END_MR, COPIES =@COPIES, WAISTAGE =@WAISTAGE, CUS_NAME =@CUS_NAME , C_START_MR=@C_START_MR, C_END_MR=@C_END_MR, C_COPIES=@C_COPIES, C_WAISTAGE=@C_WAISTAGE WHERE     (COM_ID = @COM_ID) AND (PERIOD_START = @PERIOD_START) AND (PERIOD_END = @PERIOD_END) AND (CUS_ID = @CUS_ID) AND (SERIAL_NO = @SERIAL_NO)"
                        Else
                            IsEdit = False
                            strSQL = "INSERT INTO TBL_TBL_METER_READING_DET_COLOR (COM_ID, PERIOD_START, PERIOD_END, SERIAL_NO, MR_ID, AG_ID, CUS_ID, P_NO, MR_MAKE_MODEL, M_LOC, START_MR, END_MR, COPIES, WAISTAGE, CUS_NAME,C_START_MR, C_END_MR, C_COPIES, C_WAISTAGE) VALUES     (@COM_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @MR_ID, @AG_ID, @CUS_ID, @P_NO, @MR_MAKE_MODEL, @M_LOC, @START_MR, @END_MR, @COPIES, @WAISTAGE, @CUS_NAME,@C_START_MR, @C_END_MR, @C_COPIES, @C_WAISTAGE)"
                        End If




                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", dgMR.Rows(row.Index).Cells("SN").Value)
                        If IsEdit = False Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@MR_ID", MR_ID)
                        End If

                        dbConnections.sqlCommand.Parameters.AddWithValue("@P_NO", dgMR.Rows(row.Index).Cells("P_NO").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@MR_MAKE_MODEL", dgMR.Rows(row.Index).Cells("MR_MAKE").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC", dgMR.Rows(row.Index).Cells("M_LOC").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", dgMR.Rows(row.Index).Cells("START_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@END_MR", dgMR.Rows(row.Index).Cells("END_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COPIES", dgMR.Rows(row.Index).Cells("MR_COPIES").Value)
                        If IsDBNull(dgMR.Rows(row.Index).Cells("WAISTAGE").Value) Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
                        Else
                            If dgMR.Rows(row.Index).Cells("WAISTAGE").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", dgMR.Rows(row.Index).Cells("WAISTAGE").Value)
                            End If

                        End If

                        'C_START_MR, C_END_MR, C_COPIES, C_WAISTAGE



                        'C_START_MR
                        'C_END_MR
                        'C_COPIES
                        'C_WAISTAGE

                        dbConnections.sqlCommand.Parameters.AddWithValue("@C_START_MR", dgMR.Rows(row.Index).Cells("C_START_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@C_END_MR", dgMR.Rows(row.Index).Cells("C_END_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@C_COPIES", dgMR.Rows(row.Index).Cells("C_COPIES").Value)
                        If IsDBNull(dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value) Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@C_WAISTAGE", 0)
                        Else
                            If dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@C_WAISTAGE", 0)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@C_WAISTAGE", dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value)
                            End If

                        End If




                        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NAME", Trim(txtCustomerName.Text))

                        If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                    End If


                Next


                dbConnections.sqlTransaction.Commit()

                MessageBox.Show("Transaction Saved Successfully.", "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                dbConnections.sqlTransaction.Rollback()
                inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Finally
                dbConnections.dReader.Close()
                connectionClose()

            End Try
        End If

        Return save
    End Function



    Private Function Process_Invoice() As Boolean
        Process_Invoice = False
        Dim InvoiceNo As String = ""
        Dim IsEdit As Boolean = False
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If

            InvoiceNo = Genarate_INV_NO()
            Try




                connectionStaet()

                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction


                errorEvent = "Save"
                strSQL = "SELECT CASE WHEN EXISTS (SELECT     COM_ID FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(txtSelectedAG.Text) & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END =  '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                If dbConnections.sqlCommand.ExecuteScalar Then
                    IsEdit = True
                    strSQL = "UPDATE    TBL_INVOICE_MASTER SET              INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, BILLING_METHOD =@BILLING_METHOD, INV_STATUS =@INV_STATUS, VAT_TYPE =@VAT_TYPE, INV_PRINTED =@INV_PRINTED, INV_BY =@INV_BY, INV_BY_NAME =@INV_BY_NAME, INV_HEADDING =@INV_HEADDING,INV_TRANSACTION_STATUS=@INV_TRANSACTION_STATUS,IS_NBT=@IS_NBT,IS_VAT=@IS_VAT, RENTAL_VAL=@RENTAL_VAL,INV_VAL=@INV_VAL,REP_CODE=@REP_CODE WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID) AND (CUS_ID =@CUS_ID) AND (INV_PERIOD_START = @INV_PERIOD_START) AND (INV_PERIOD_END = @INV_PERIOD_END)"
                Else
                    IsEdit = False
                    strSQL = "INSERT INTO TBL_INVOICE_MASTER (COM_ID, AG_ID, CUS_ID, INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, INV_STATUS, VAT_TYPE, INV_PRINTED, INV_BY, INV_BY_NAME, INV_HEADDING,INV_TRANSACTION_STATUS,IS_NBT,IS_VAT,RENTAL_VAL,INV_VAL,REP_CODE) VALUES     (@COM_ID, @AG_ID, @CUS_ID, @INV_PERIOD_START, @INV_PERIOD_END, @INV_NO, @INV_DATE, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @INV_STATUS, @VAT_TYPE, @INV_PRINTED, @INV_BY, @INV_BY_NAME, @INV_HEADDING,@INV_TRANSACTION_STATUS,@IS_NBT,@IS_VAT,@RENTAL_VAL,@INV_VAL,@REP_CODE)"
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)


                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PERIOD_START", dtpStart.Value)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PERIOD_END", dtpEnd.Value)

                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(InvoiceNo))

                Dim InvDateTome As DateTime = dtpInvoiceDate.Value
                InvDateTome = InvDateTome.Add(Today.Date.TimeOfDay)


                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_DATE", InvDateTome)

                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_NBT", cbNBT.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_VAT", cbVAT.CheckState)
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

                dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", Billing_Method)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", Inv_Status)
                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_TYPE", Trim(lblVatType.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PRINTED", False)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_BY", userSession)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_BY_NAME", userName)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_HEADDING", "")
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_TRANSACTION_STATUS", "INVOICED")
                If Trim(txtRental.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RENTAL_VAL", CDbl(Trim(txtRental.Text)))
                End If

                Dim ColVal As Double = 0
                Dim BwVal As Double = 0
                Dim rentalval As Double = 0

                If Trim(txtInvoiceValue.Text) = "" Then
                    BwVal = 0
                Else
                    BwVal = CDbl(Trim(txtInvoiceValue.Text))
                End If

                If Trim(txtColorInvoiceValue.Text) = "" Then
                    ColVal = 0
                Else
                    ColVal = CDbl(Trim(txtColorInvoiceValue.Text))
                End If

                If Trim(txtRental.Text) = "" Then
                    rentalval = 0
                Else
                    rentalval = CDbl(Trim(txtRental.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_VAL", (BwVal + ColVal + rentalval))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRepCode.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False


                For Each row As DataGridViewRow In dgMR.Rows

                    If dgMR.Rows(row.Index).Cells(0).Value <> "" Then
                        dbConnections.sqlCommand.Parameters.Clear()


                        'strSQL = "SELECT CASE WHEN EXISTS (SELECT     P_NO FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (SERIAL_NO = '" & Trim(dgMR.Rows(row.Index).Cells("SN").Value) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                        'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        'If dbConnections.sqlCommand.ExecuteScalar Then
                        '    IsEdit = True
                        '    strSQL = "UPDATE    TBL_INVOICE_DET SET P_NO=@P_NO MAKE_MODEL =@MAKE_MODEL, INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, BILLING_METHOD =, M_LOC =@M_LOC, START_MR =@START_MR,    END_MR =@END_MR, INV_COPIES =@INV_COPIES, WAISTAGE =@WAISTAGE WHERE     (COM_ID =@COM_ID) AND (AG_ID = @AG_ID) AND (CUS_ID = @CUS_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (SERIAL_NO =@SERIAL_NO)"
                        'Else
                        '    IsEdit = False
                        '    strSQL = "INSERT INTO TBL_INVOICE_DET (COM_ID, AG_ID, CUS_ID, PERIOD_START, PERIOD_END, SERIAL_NO, INV_NO, MAKE_MODEL, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, M_LOC, START_MR, END_MR, INV_COPIES, WAISTAGE,P_NO) VALUES     (@COM_ID, @AG_ID, @CUS_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @INV_NO, @MAKE_MODEL, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @M_LOC, @START_MR, @END_MR, @INV_COPIES, @WAISTAGE,@P_NO) "
                        'End If
                        If IsEdit = True Then
                            strSQL = "UPDATE    TBL_INVOICE_DET SET P_NO=@P_NO ,MAKE_MODEL =@MAKE_MODEL, INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, BILLING_METHOD =@BILLING_METHOD, M_LOC =@M_LOC, START_MR =@START_MR,    END_MR =@END_MR, INV_COPIES =@INV_COPIES, WAISTAGE =@WAISTAGE , COLOR_START_MR=@COLOR_START_MR, COLOR_END_MR=@COLOR_END_MR, COLOR_INV_COPIES@COLOR_INV_COPIES, COLOR_WAISTAGE=@COLOR_WAISTAGE WHERE     (COM_ID =@COM_ID) AND (AG_ID = @AG_ID) AND (CUS_ID = @CUS_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (SERIAL_NO =@SERIAL_NO)"
                        Else
                            strSQL = "INSERT INTO TBL_INVOICE_DET (COM_ID, AG_ID, CUS_ID, PERIOD_START, PERIOD_END, SERIAL_NO, INV_NO, MAKE_MODEL, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, M_LOC, START_MR, END_MR, INV_COPIES, WAISTAGE,P_NO,COLOR_START_MR, COLOR_END_MR, COLOR_INV_COPIES, COLOR_WAISTAGE) VALUES     (@COM_ID, @AG_ID, @CUS_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @INV_NO, @MAKE_MODEL, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @M_LOC, @START_MR, @END_MR, @INV_COPIES, @WAISTAGE,@P_NO,@COLOR_START_MR, @COLOR_END_MR, @COLOR_INV_COPIES, @COLOR_WAISTAGE) "
                        End If


                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", dgMR.Rows(row.Index).Cells("SN").Value)
                        If IsEdit = False Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", InvoiceNo)
                        End If

                        dbConnections.sqlCommand.Parameters.AddWithValue("@MAKE_MODEL", dgMR.Rows(row.Index).Cells("MR_MAKE").Value)

                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD1", Trim(txLocation1.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD2", Trim(txtLocation2.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_ADD3", Trim(txtLocation3.Text))


                        dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", Billing_Method)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC", dgMR.Rows(row.Index).Cells("M_LOC").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", dgMR.Rows(row.Index).Cells("START_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@END_MR", dgMR.Rows(row.Index).Cells("END_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_COPIES", dgMR.Rows(row.Index).Cells("MR_COPIES").Value)
                        If IsDBNull(dgMR.Rows(row.Index).Cells("WAISTAGE").Value) Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
                        Else
                            If dgMR.Rows(row.Index).Cells("WAISTAGE").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", 0)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@WAISTAGE", dgMR.Rows(row.Index).Cells("WAISTAGE").Value)
                            End If
                        End If

                        '// color mr
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_START_MR", dgMR.Rows(row.Index).Cells("C_START_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_END_MR", dgMR.Rows(row.Index).Cells("C_END_MR").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_INV_COPIES", dgMR.Rows(row.Index).Cells("C_COPIES").Value)

                        If IsDBNull(dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value) Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_WAISTAGE", 0)
                        Else
                            If dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_WAISTAGE", 0)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_WAISTAGE", dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value)
                            End If
                        End If

                        dbConnections.sqlCommand.Parameters.AddWithValue("@P_NO", dgMR.Rows(row.Index).Cells("P_NO").Value)

                        If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False
                    End If

                Next


                strSQL = "UPDATE    TBL_METER_READING_MASTER SET  DATA_PROCESSED=1, DATA_PROCESSED_DATE=GETDATE(), DATA_PROCESSED_BY=@DATA_PROCESSED_BY  WHERE     (COM_ID = @COM_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (CUS_ID = @CUS_ID) AND (AG_ID =@AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_START", dtpStart.Value)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PERIOD_END", dtpEnd.Value)

                dbConnections.sqlCommand.Parameters.AddWithValue("@DATA_PROCESSED_BY", userSession)

                If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False



                If dgBw.Rows.Count > 0 Then


                    strSQL = "DELETE FROM TBL_INV_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_CODE = '" & Trim(txtSelectedAG.Text) & "') AND (INV_NO = '" & InvoiceNo & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()

                    'Data grid Data feeding code

                    For Each row As DataGridViewRow In dgBw.Rows

                        If row.Index <> (dgBw.RowCount - 1) Then
                            dbConnections.sqlCommand.Parameters.Clear()

                            strSQL = "INSERT INTO TBL_INV_BW_COMMITMENT  (INV_NO, COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE, BW_COPY_BREAKUP) VALUES     (@INV_NO, @COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE, @BW_COPY_BREAKUP)"
                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", InvoiceNo)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(txtSelectedAG.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_1", dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_2", dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RATE", dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                            If dgBw.Rows(row.Index).Cells("COMMI_BREAKUP_B").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_COPY_BREAKUP", DBNull.Value)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_COPY_BREAKUP", dgBw.Rows(row.Index).Cells("COMMI_BREAKUP_B").Value)
                            End If

                            If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False
                        End If


                    Next
                End If


                If dgColor.Rows.Count > 0 Then


                    strSQL = "DELETE FROM TBL_INV_COLOR_COMMITMENT WHERE    (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_CODE = '" & Trim(txtSelectedAG.Text) & "') AND (INV_NO = '" & InvoiceNo & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()

                    'Data grid Data feeding code

                    For Each row As DataGridViewRow In dgColor.Rows
                        If row.Index <> (dgColor.RowCount - 1) Then


                            dbConnections.sqlCommand.Parameters.Clear()

                            strSQL = "INSERT INTO TBL_INV_COLOR_COMMITMENT (INV_NO, COM_ID, CUS_ID, AG_CODE, COLOR_RANGE_1, COLOR_RANGE_2, COLOR_RATE, COLOR_COPY_BREAKUP) VALUES     (@INV_NO, @COM_ID, @CUS_ID, @AG_CODE, @COLOR_RANGE_1, @COLOR_RANGE_2, @COLOR_RATE, @COLOR_COPY_BREAKUP)"
                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", InvoiceNo)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(txtSelectedAG.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_1", dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_2", dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RATE", dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                            If dgColor.Rows(row.Index).Cells("COMMI_BREAKUP_C").Value = Nothing Then
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_COPY_BREAKUP", DBNull.Value)
                            Else
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_COPY_BREAKUP", dgColor.Rows(row.Index).Cells("COMMI_BREAKUP_C").Value)
                            End If
                            If dbConnections.sqlCommand.ExecuteNonQuery() Then Process_Invoice = True Else Process_Invoice = False
                        End If
                    Next
                End If

                txtInvoiceNo.Text = InvoiceNo


                '// Agreement Update

                strSQL = "UPDATE  TBL_CUS_AGREEMENT SET  CUS_CODE =@CUS_CODE,  BILLING_METHOD =@BILLING_METHOD, SLAB_METHOD =@SLAB_METHOD, BILLING_PERIOD =@BILLING_PERIOD, MD_BY ='" & userSession & "', MD_DATE =GETDATE() , INV_STATUS=@INV_STATUS , MACHINE_TYPE=@MACHINE_TYPE , AG_RENTAL_PRICE=@AG_RENTAL_PRICE  WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID)"


                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))

                If rbtnCommitment.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "COMMITMENT")
                End If
                If rbtnActual.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "ACTUAL")
                End If

                If rbtnRental.Checked = True Then

                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "RENTAL")
                End If

                If Trim(txtRental.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", CDbl(Trim(txtRental.Text)))
                End If
                If rbtnInvStatusAll.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "ALL")
                End If
                If rbtnInvStatusIndividual.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "INDIVIDUAL")
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "COLOR")


                dbConnections.sqlCommand.Parameters.AddWithValue("@SLAB_METHOD", Trim(txtSlabMethod.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_PERIOD", Trim(txtBilPeriod.Text))

                dbConnections.sqlCommand.ExecuteNonQuery()


                'BW_RANGE_1
                'BW_RANGE_2
                'BW_RATE

                'COLOR_RANGE_1
                'COLOR_RANGE_2
                'COLOR_RATE
                'If IsNewAgreement = True Then '//  commitments will save only for new agreement creation
                If dgBw.Rows.Count > 0 Then


                    strSQL = "DELETE FROM TBL_AG_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()

                    'Data grid Data feeding code

                    For Each row As DataGridViewRow In dgBw.Rows

                        If dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value <> Nothing Then
                            dbConnections.sqlCommand.Parameters.Clear()

                            strSQL = "INSERT   INTO            TBL_AG_BW_COMMITMENT(COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE) VALUES     (@COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE)"
                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(SelectedAgreement))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_1", dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_2", dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RATE", dgBw.Rows(row.Index).Cells("BW_RATE").Value)

                            dbConnections.sqlCommand.ExecuteNonQuery()
                        End If


                    Next
                End If

                If dgColor.Rows.Count > 0 Then


                    strSQL = "DELETE FROM TBL_AG_COLOR_COMMITMENT  WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()

                    'Data grid Data feeding code

                    For Each row As DataGridViewRow In dgColor.Rows
                        If dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value <> 0 Then


                            dbConnections.sqlCommand.Parameters.Clear()

                            strSQL = "INSERT INTO TBL_AG_COLOR_COMMITMENT    (COM_ID, CUS_ID, AG_CODE, COLOR_RANGE_1, COLOR_RANGE_2, COLOR_RATE) VALUES     (@COM_ID, @CUS_ID, @AG_CODE, @COLOR_RANGE_1, @COLOR_RANGE_2, @COLOR_RATE)"
                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(SelectedAgreement))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_1", dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_2", dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value)
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RATE", dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)

                            dbConnections.sqlCommand.ExecuteNonQuery()
                        End If
                    Next
                End If
                'End If





                dbConnections.sqlTransaction.Commit()

                MessageBox.Show("Invoiced Process Completed.", "Completed.", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                dbConnections.sqlTransaction.Rollback()
                inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Finally
                dbConnections.dReader.Close()
                connectionClose()

            End Try
        End If

        Return Process_Invoice
    End Function





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
        AddtoGrid()
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

    Private Async Function GenerateMeterReadingNo() As Threading.Tasks.Task(Of String)
        errorEvent = "Reading information"
        Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/meterreading/generatemeterreadingno?companyID={globalVariables.selectedCompanyID}"
        Using client As New HttpClient
            Dim apiurlResponse As HttpResponseMessage = Await client.GetAsync(apiUrl)
            If apiurlResponse.IsSuccessStatusCode Then
                Dim json As String = Await apiurlResponse.Content.ReadAsStringAsync()
                Dim generateMRNo As String = JsonConvert.DeserializeObject(json)

                Return generateMRNo
            End If
        End Using
    End Function

    Private Function GenarateMRNo() As String
        GenarateMRNo = ""
        GenarateMRNo = GenerateMeterReadingNo().ToString()
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
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT    INV_NO FROM         TBL_INVOICE_MASTER WHERE COM_ID  = '" & globalVariables.selectedCompanyID & "' AND INV_NO = '" & globalVariables.selectedCompanyID & "/" & "INV" & "/" & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
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

    Private Function Genarate_INV_NO() As String

        Genarate_INV_NO = ""

        errorEvent = "Reading information"
        connectionStaet()


        Try
            strSQL = "SELECT     MAX(INV_NO) as 'max' FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"


            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                Genarate_INV_NO = globalVariables.selectedCompanyID & "/" & "INV" & "/" & 1
            Else
                Dim IRCodeSplit() As String
                Dim NoRecordFound As Boolean = False
                Dim IRID As Integer = 0
                IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                IRID = IRCodeSplit(2)
                Do Until NoRecordFound = True
                    strSQL = "SELECT CASE WHEN EXISTS (SELECT     INV_NO  FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (INV_NO = '" & globalVariables.selectedCompanyID & "/INV/" & IRID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteScalar = False Then
                        NoRecordFound = True
                    Else
                        IRID = IRID + 1
                    End If
                Loop

                If NoRecordFound = True Then
                    Genarate_INV_NO = IRCodeSplit(0) & "/INV/" & IRID
                Else
                    Exit Function
                End If

            End If


        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally

            connectionClose()
        End Try
    End Function


    Dim sqlCMD As New SqlCommand

    Private Sub LoadSelectedAgreement(ByRef InvoiceNo As String)


        Dim MkeModel As String
        Dim SN As String
        Dim PNo As String
        Dim MLoc As String

        Dim BillingPeriod As Integer = 1
        Dim LastBillingDate As DateTime
        Dim IsPrevousRecordLoading As Boolean = False
        Dim lastMR As Integer = 0
        Dim LastMRColor As Integer = 0

        Dim EndReading As String = ""
        Dim ColorEndReading As String = ""
        Dim Waistage As String = ""
        Dim ColorWaistage As String = ""
        Dim Copies As Integer = 0
        Dim ColorCopies As Integer = 0

        dgMR.Rows.Clear()
        Dim IsMRhave As Boolean = False


        Dim AGCode As String = ""
        Dim CUCode As String = ""

        '// Get Ag ID from INvoice
        dbConnections.sqlCommand.Parameters.Clear()
        strSQL = "SELECT     AG_ID,CUS_ID,INV_PERIOD_START,INV_PERIOD_END FROM         TBL_INVOICE_MASTER WHERE     (INV_NO = '" & Trim(InvoiceNo) & "') and COM_ID =@COM_ID"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

        While dbConnections.dReader.Read
            AGCode = dbConnections.dReader.Item("AG_ID")
            CUCode = dbConnections.dReader.Item("CUS_ID")
            dtpStart.Value = dbConnections.dReader.Item("INV_PERIOD_START")
            dtpEnd.Value = dbConnections.dReader.Item("INV_PERIOD_END")
        End While
        dbConnections.dReader.Close()


        strSQL = "SELECT     MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", CUCode)
        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


        While dbConnections.dReader.Read

            txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
            lblVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
            lblVatTypeName.Text = dbConnections.dReader.Item("VAT_DESC")

            If IsDBNull(dbConnections.dReader.Item("IS_NBT")) Then
                cbNBT.Checked = False
            Else
                If dbConnections.dReader.Item("IS_NBT") Then
                    cbNBT.Checked = True
                Else
                    cbNBT.Checked = False
                End If
            End If


            If IsDBNull(dbConnections.dReader.Item("IS_VAT")) Then
                cbVAT.Checked = False
            Else
                If dbConnections.dReader.Item("IS_VAT") Then
                    cbVAT.Checked = True
                Else
                    cbVAT.Checked = False
                End If
            End If

        End While
        dbConnections.dReader.Close()

        Try

            '// get billing period
            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT    ISNULL( BILLING_PERIOD,1) FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID = '" & Trim(AGCode) & "') and (COM_ID = '" & globalVariables.selectedCompanyID & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            BillingPeriod = dbConnections.sqlCommand.ExecuteScalar

            '// get last billing date range
            strSQL = "SELECT   ISNULL( MAX(PERIOD_START),GETDATE()) AS Expr1  FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            LastBillingDate = dbConnections.sqlCommand.ExecuteScalar

            'dbConnections.sqlCommand.Parameters.Clear()
            'strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2 FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID)"
            'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            'dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
            'dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
            'dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            'While dbConnections.dReader.Read

            '    txLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
            '    txtLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
            '    txtLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
            'End While
            'dbConnections.dReader.Close()


            If globalVariables.selectedCompanyID = "003" Then
                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2 FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read

                    txLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
                    txtLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
                    txtLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
                End While
                dbConnections.dReader.Close()
            Else '// this will take machine location as invoice address

                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     TOP (1) M_LOC1, M_LOC2, M_LOC3 FROM         TBL_MACHINE_TRANSACTIONS  WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read

                    txLocation1.Text = dbConnections.dReader.Item("M_LOC1")
                    txtLocation2.Text = dbConnections.dReader.Item("M_LOC2")
                    txtLocation3.Text = dbConnections.dReader.Item("M_LOC3")
                End While
                dbConnections.dReader.Close()

            End If

            dbConnections.sqlCommand.Parameters.Clear()
            Dim IsInvoiced As Boolean = False
            strSQL = "SELECT CASE WHEN EXISTS (SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsInvoiced = True
            Else
                IsInvoiced = False
            End If

            strSQL = "SELECT  DISTINCT   MTBL_MACHINE_MASTER.MACHINE_MAKE, MTBL_MACHINE_MASTER.MACHINE_MODEL, TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO,  TBL_MACHINE_TRANSACTIONS.M_DEPT, (SELECT     TOP (1) ISNULL(END_MR, 0) AS Expr1  FROM          TBL_TBL_METER_READING_DET_COLOR WHERE      (SERIAL_NO = TBL_MACHINE_TRANSACTIONS.SERIAL) AND (COM_ID = '" & globalVariables.selectedCompanyID & "')  ORDER BY TRANS_ID DESC) AS 'L_READING', (SELECT     TOP (1) ISNULL(C_END_MR, 0) AS Expr2  FROM          TBL_TBL_METER_READING_DET_COLOR AS TBL_TBL_METER_READING_DET_COLOR_1  WHERE      (SERIAL_NO = TBL_MACHINE_TRANSACTIONS.SERIAL) AND (COM_ID = '" & globalVariables.selectedCompanyID & "')  ORDER BY TRANS_ID DESC) AS 'L_COLOR_READING', TBL_CUS_AGREEMENT.MACHINE_TYPE FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN  MTBL_MACHINE_MASTER ON TBL_MACHINE_TRANSACTIONS.COM_ID = MTBL_MACHINE_MASTER.COM_ID AND  TBL_MACHINE_TRANSACTIONS.MACHINE_PN = MTBL_MACHINE_MASTER.MACHINE_ID INNER JOIN TBL_CUS_AGREEMENT ON TBL_MACHINE_TRANSACTIONS.COM_ID = TBL_CUS_AGREEMENT.COM_ID WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.CUS_ID = @CUS_ID) AND (TBL_MACHINE_TRANSACTIONS.AG_ID = @AG_ID) and TBL_CUS_AGREEMENT.MACHINE_TYPE = 'COLOR' ORDER BY TBL_MACHINE_TRANSACTIONS.P_NO"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))


            Dim da As New SqlDataAdapter(sqlCommand)

            Dim ds As New DataSet()

            da.Fill(ds)


            For i = 0 To ds.Tables(0).Rows.Count - 1
                'ds.Tables(0).Rows(i).Item(0)
                SN = ds.Tables(0).Rows(i).Item(2)
                If IsDBNull(ds.Tables(0).Rows(i).Item(3)) Then
                    PNo = ""
                Else
                    PNo = ds.Tables(0).Rows(i).Item(3)
                End If

                MLoc = ds.Tables(0).Rows(i).Item(4)
                MkeModel = ds.Tables(0).Rows(i).Item(1)
                dbConnections.dReader.Close()
                strSQL = "SELECT     START_MR, END_MR, COPIES, WAISTAGE, C_START_MR, C_END_MR, C_COPIES, C_WAISTAGE FROM         TBL_TBL_METER_READING_DET_COLOR WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "') AND (SERIAL_NO = '" & SN & "') AND (CUS_ID = '" & Trim(CUCode) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    IsMRhave = True
                    If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
                        If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
                            lastMR = 0
                        Else
                            lastMR = ds.Tables(0).Rows(i).Item(5)
                        End If
                    Else
                        lastMR = dbConnections.dReader.Item("START_MR")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("C_START_MR")) Then
                        If IsDBNull(ds.Tables(0).Rows(i).Item(6)) Then
                            LastMRColor = 0
                        Else
                            LastMRColor = ds.Tables(0).Rows(i).Item(6)
                        End If
                    Else
                        LastMRColor = dbConnections.dReader.Item("C_START_MR")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("END_MR")) Then
                        EndReading = ""
                    Else
                        EndReading = dbConnections.dReader.Item("END_MR")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("C_END_MR")) Then
                        ColorEndReading = ""
                    Else
                        ColorEndReading = dbConnections.dReader.Item("C_END_MR")
                    End If

                    If IsDBNull(dbConnections.dReader.Item("WAISTAGE")) Then
                        Waistage = ""
                    Else
                        Waistage = dbConnections.dReader.Item("WAISTAGE")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("C_WAISTAGE")) Then
                        ColorWaistage = ""
                    Else
                        ColorWaistage = dbConnections.dReader.Item("C_WAISTAGE")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("COPIES")) Then
                        Copies = 0
                    Else
                        Copies = dbConnections.dReader.Item("COPIES")
                    End If

                    If IsDBNull(dbConnections.dReader.Item("C_COPIES")) Then
                        ColorCopies = 0
                    Else
                        ColorCopies = dbConnections.dReader.Item("C_COPIES")
                    End If


                End While
                dbConnections.dReader.Close()

                If IsMRhave = False Then
                    If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
                        '// get First meter reading  form master transaction
                        strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
                        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                        While dbConnections.dReader.Read
                            If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
                                lastMR = 0
                            Else
                                lastMR = dbConnections.dReader.Item("START_MR")
                            End If

                        End While

                        dbConnections.dReader.Close()

                    Else
                        lastMR = ds.Tables(0).Rows(i).Item(5)
                    End If

                    If IsDBNull(ds.Tables(0).Rows(i).Item(6)) Then

                        '// get First meter reading  form master transaction
                        strSQL = "SELECT     START_MR_COLOR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
                        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                        While dbConnections.dReader.Read
                            If IsDBNull(dbConnections.dReader.Item("START_MR_COLOR")) Then
                                LastMRColor = 0
                            Else
                                LastMRColor = dbConnections.dReader.Item("START_MR_COLOR")
                            End If

                        End While

                        dbConnections.dReader.Close()

                    Else
                        LastMRColor = ds.Tables(0).Rows(i).Item(6)
                    End If
                End If

                If PNo = "" Then
                    PNo = "0"
                End If
                populatreDatagrid(MkeModel, SN, PNo, MLoc, lastMR, EndReading, Copies, Waistage, LastMRColor, ColorEndReading, ColorCopies, ColorWaistage)


            Next





            Try
                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE,AG_RENTAL_PRICE,REP_CODE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read





                    If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                        rbtnRental.Checked = False
                    Else
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                        rbtnRental.Checked = False

                        If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
                            rbtnCommitment.Checked = True
                        ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
                            rbtnActual.Checked = True
                        ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "RENTAL" Then
                            rbtnRental.Checked = True
                        Else
                            rbtnActual.Checked = False
                            rbtnCommitment.Checked = False
                            rbtnRental.Checked = False
                        End If

                    End If

                    If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
                        txtSlabMethod.Text = ""
                    Else
                        txtSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
                    End If


                    If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
                        txtBilPeriod.Text = ""
                    Else
                        txtBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
                    End If


                    If IsDBNull(dbConnections.dReader.Item("INV_STATUS")) Then
                        rbtnInvStatusAll.Checked = False
                        rbtnInvStatusIndividual.Checked = False
                    Else
                        If dbConnections.dReader.Item("INV_STATUS") = "ALL" Then
                            rbtnInvStatusAll.Checked = True
                        ElseIf dbConnections.dReader.Item("INV_STATUS") = "INDIVIDUAL" Then
                            rbtnInvStatusIndividual.Checked = True
                        Else
                            rbtnInvStatusAll.Checked = False
                            rbtnInvStatusIndividual.Checked = False
                        End If

                    End If

                    If IsDBNull(dbConnections.dReader.Item("AG_RENTAL_PRICE")) Then
                        txtRental.Text = ""
                    Else
                        txtRental.Text = Format(dbConnections.dReader.Item("AG_RENTAL_PRICE"), "0.00")
                    End If


                    If IsDBNull(dbConnections.dReader.Item("REP_CODE")) Then
                        txtRepCode.Text = ""
                    Else
                        txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")
                    End If

                End While
                dbConnections.dReader.Close()
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            End Try

            LoadCommitments(Trim(AGCode), Trim(CUCode))



            CalculateInvoiceValue()

            GetLastInvInfo(Trim(CUCode), Trim(AGCode))

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try


    End Sub

    Private Sub LoadCommitments(ByRef AG_ID As String, ByRef CUS_ID As String)
        dgBw.Rows.Clear()
        Try
            strSQL = "SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                populatreDatagrid_BW_Commitment(dbConnections.dReader.Item("BW_RANGE_1"), dbConnections.dReader.Item("BW_RANGE_2"), dbConnections.dReader.Item("BW_RATE"), Nothing)
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        dgColor.Rows.Clear()
        Try
            strSQL = "SELECT     COLOR_RANGE_1, COLOR_RANGE_2, COLOR_RATE FROM         TBL_AG_COLOR_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                populatreDatagrid_Color_Commitment(dbConnections.dReader.Item("COLOR_RANGE_1"), dbConnections.dReader.Item("COLOR_RANGE_2"), dbConnections.dReader.Item("COLOR_RATE"), Nothing)
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

    End Sub

    Private Sub populatreDatagrid_BW_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal, ByRef CopyCount As Integer)
        dgBw.ColumnCount = 4
        dgBw.Rows.Add(Range1, Range2, Rate, CopyCount)
    End Sub
    Private Sub populatreDatagrid_Color_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal, ByRef CopyCount As Integer)
        dgColor.ColumnCount = 4
        dgColor.Rows.Add(Range1, Range2, Rate, CopyCount)
    End Sub

    Private Sub populatreDatagrid(ByRef Make As String, ByRef SN As String, ByRef PNO As String, ByRef Location As String, ByRef StartReading As String, ByRef EndReading As String, ByRef Copies As String, ByRef Waistage As String, ByRef CStartReding As String, ByRef CEndReading As String, ByRef CCopies As String, ByRef CWaistage As String)
        dgMR.ColumnCount = 12
        dgMR.Rows.Add(Make, SN, PNO, Location, StartReading, EndReading, Copies, Waistage, CStartReding, CEndReading, CCopies, CWaistage)
    End Sub



    Private Sub CalculateInvoiceValue()
        Dim CopyCount As Integer = 0
        Dim Waistage As Integer = 0


        Dim InvCopyCount As Integer = 0

        '// Actual Calculation variables
        Dim ARate As Double = 0


        '// Commitment Calculation variables
        '// Slab 2
        Dim isCapturedRange As Boolean = False
        Dim CapturedLastRate As Double = 0
        '// Master variables
        Dim InvoiceValue As Double
        Dim ColorInvoiceValue As Double
        Dim NBTval As Double
        Dim VATVal As Double
        Dim NetValue As Double
        Dim ColorNBT As Double
        Dim BwNBT As Double
        Dim COlorVAT As Double
        Dim BwVAT As Double

        '// COlor Variables
        Dim ColorCopyCount As Integer = 0
        Dim ColorWaistage As Integer = 0
        Dim InvColorCopyCount As Integer = 0

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

                If dgMR.Rows(row.Index).Cells("C_COPIES").Value <> Nothing Then
                    ColorCopyCount = ColorCopyCount + dgMR.Rows(row.Index).Cells("C_COPIES").Value
                End If

                If dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value <> Nothing Then
                    ColorWaistage = ColorWaistage + dgMR.Rows(row.Index).Cells("C_WAISTAGE").Value
                End If

            Next

            For Each row2 As DataGridViewRow In dgBw.Rows
                dgBw.Item(3, row2.Index).Value = Nothing
            Next
            For Each row2 As DataGridViewRow In dgBw.Rows
                dgColor.Item(3, row2.Index).Value = Nothing
            Next

            txtTotalCopies.Text = CopyCount
            txtTotalWaistage.Text = Waistage
            InvCopyCount = (CopyCount - Waistage)
            txtInvCopies.Text = InvCopyCount

            txtTotalColorCopies.Text = ColorCopyCount
            txtTotalColorWaistage.Text = ColorWaistage
            InvColorCopyCount = (ColorCopyCount - ColorWaistage)
            txtColorInvCopies.Text = InvColorCopyCount

            If rbtnActual.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then
                    dgBw.Item(3, 0).Value = Nothing
                    dgColor.Item(3, 0).Value = Nothing
                    '// BW
                    ARate = dgBw.Item(2, 0).Value
                    InvoiceValue = (InvCopyCount * ARate)
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If


                    BwNBT = NBTval

                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    BwVAT = VATVal


                    txtInvoiceValue.Text = InvoiceValue.ToString("N2")
                    dgBw.Item(3, 0).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database


                    '// color

                    ARate = dgColor.Item(2, 0).Value
                    ColorInvoiceValue = (InvColorCopyCount * ARate)

                    If cbNBT.Checked = True Then
                        NBTval = ((ColorInvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If

                    ColorNBT = NBTval

                    If cbVAT.Checked = True Then
                        VATVal = (((ColorInvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    COlorVAT = VATVal

                    txtColorInvoiceValue.Text = ColorInvoiceValue
                    dgColor.Item(3, 0).Value = InvColorCopyCount '// use to crystal report counting bug using bug fix from database
                    txtNBT.Text = (BwNBT + ColorNBT).ToString("N2")
                    txtVAT.Text = (BwVAT + COlorVAT).ToString("N2")

                    If Trim(txtRental.Text) = "" Then
                        rental_Val = 0
                        txtRental.Text = 0
                    Else
                        rental_Val = CDbl(txtRental.Text)
                        txtRental.Text = rental_Val.ToString("N2")
                    End If
                    '           bW                              + Color                            + rental
                    NetValue = (InvoiceValue + BwNBT + BwVAT) + (ColorInvoiceValue + ColorNBT + COlorVAT) + rental_Val

                    txtNetValue.Text = NetValue.ToString("N2")
                End If


            End If

            If rbtnCommitment.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then

                    InvoiceValue = 0
                    is_FirstRecord = True

                    For Each row As DataGridViewRow In dgBw.Rows
                        If dgBw.Rows(row.Index).Cells("BW_RATE").Value <> Nothing Then

                            If (InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value) Then

                                If is_FirstRecord = True Then
                                    InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                                    dgBw.Item(3, row.Index).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database
                                    If row.Index > 0 Then
                                        dgBw.Item(3, row.Index - 1).Value = Nothing
                                    End If
                                Else
                                    InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
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



                    If cbNBT.Checked = True Then
                        BwNBT = ((InvoiceValue / 100) * 2)
                    Else
                        BwNBT = 0
                    End If

                    If cbVAT.Checked = True Then
                        BwVAT = (((InvoiceValue + BwNBT) / 100) * 15)
                    Else
                        BwVAT = 0
                    End If

                    txtInvoiceValue.Text = InvoiceValue.ToString("N2")

                    '/// for color

                    ColorInvoiceValue = 0
                    is_FirstRecord = True
                    For Each row As DataGridViewRow In dgColor.Rows
                        If dgColor.Rows(row.Index).Cells("COLOR_RATE").Value <> Nothing Then

                            If (InvColorCopyCount <= dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value) Then

                                If is_FirstRecord = True Then

                                    ColorInvoiceValue = (dgColor.Rows(row.Index).Cells("COLOR_RATE").Value * dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value)
                                    dgColor.Item(3, row.Index).Value = InvColorCopyCount '// use to crystal report counting bug using bug fix from database
                                    If row.Index > 0 Then
                                        dgColor.Item(3, row.Index - 1).Value = Nothing
                                    End If
                                Else
                                    ColorInvoiceValue = (dgColor.Rows(row.Index).Cells("COLOR_RATE").Value * InvColorCopyCount)
                                End If

                                isCapturedRange = True
                                Exit For
                            Else
                                CapturedLastRate = dgColor.Rows(row.Index).Cells("COLOR_RATE").Value
                                isCapturedRange = False
                            End If

                            If isCapturedRange = False Then
                                ColorInvoiceValue = CapturedLastRate * InvColorCopyCount
                                dgColor.Item(3, row.Index).Value = InvColorCopyCount  '// use to crystal report counting bug using bug fix from database
                                If row.Index > 0 Then
                                    dgColor.Item(3, row.Index - 1).Value = Nothing
                                End If
                            End If

                        End If
                    Next


                    If cbNBT.Checked = True Then
                        ColorNBT = ((ColorInvoiceValue / 100) * 2)
                    Else
                        ColorNBT = 0
                    End If
                    If cbVAT.Checked = True Then
                        COlorVAT = (((ColorInvoiceValue + ColorNBT) / 100) * 15)
                    Else
                        COlorVAT = 0
                    End If

                    txtColorInvoiceValue.Text = ColorInvoiceValue.ToString("N2")
                    txtNBT.Text = (BwNBT + ColorNBT).ToString("N2")
                    txtVAT.Text = (BwVAT + COlorVAT).ToString("N2")
                    If Trim(txtRental.Text) = "" Then
                        rental_Val = 0
                        txtRental.Text = 0
                    Else
                        rental_Val = CDbl(txtRental.Text)
                        txtRental.Text = rental_Val.ToString("N2")
                    End If
                    '           bW                              + Color                            + rental
                    NetValue = (InvoiceValue + BwNBT + BwVAT) + (ColorInvoiceValue + ColorNBT + COlorVAT) + rental_Val

                    txtNetValue.Text = NetValue.ToString("N2")

                End If

                If Trim(txtSlabMethod.Text) = "SLAB-2" Then
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


                    If cbNBT.Checked = True Then
                        BwNBT = ((InvoiceValue / 100) * 2)
                    Else
                        BwNBT = 0
                    End If

                    If cbVAT.Checked = True Then
                        BwVAT = (((InvoiceValue + BwNBT) / 100) * 15)
                    Else
                        BwVAT = 0
                    End If

                    txtInvoiceValue.Text = InvoiceValue.ToString("N2")


                    '/// Need Bw calculated values 
                    '// here 


                    '// Color Copy calculation slab 2
                    IsFirstRecord = True
                    CurrentCopyCoyunt = InvColorCopyCount

                    LastReadIndex = 0
                    x = 0
                    Diffrence = 0
                    For Each row As DataGridViewRow In dgColor.Rows
                        Diffrence = 0

                        'If row.Index = dgBw.RowCount - 2 Then

                        'Else
                        '    Diffrence = (dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value - dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value) + 1
                        'End If
                        Diffrence = (dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value - dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value) + 1
                        If dgColor.Rows(row.Index).Cells("COLOR_RATE").Value <> Nothing Then
                            If dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value = 0 Then

                                ColorInvoiceValue = ColorInvoiceValue + (InvCopyCount * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                dgColor.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database

                            ElseIf row.Index = 0 Then

                                If CurrentCopyCoyunt <= Diffrence Then
                                    ColorInvoiceValue = ColorInvoiceValue + (Diffrence * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                    dgColor.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    Exit For
                                Else
                                    ColorInvoiceValue = ColorInvoiceValue + (Diffrence * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                    dgColor.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                                End If

                            ElseIf CurrentCopyCoyunt <= Diffrence Then
                                ColorInvoiceValue = ColorInvoiceValue + (CurrentCopyCoyunt * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                dgColor.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If

                            ElseIf row.Index = dgColor.RowCount - 2 Then

                                ColorInvoiceValue = ColorInvoiceValue + (CurrentCopyCoyunt * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                dgColor.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                'CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence


                            ElseIf CurrentCopyCoyunt >= Diffrence Then
                                ColorInvoiceValue = ColorInvoiceValue + (Diffrence * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                dgColor.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                            Else

                                ColorInvoiceValue = ColorInvoiceValue + (CurrentCopyCoyunt * dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)
                                dgColor.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If




                            End If

                        End If

                    Next

                    If cbNBT.Checked = True Then
                        ColorNBT = ((ColorInvoiceValue / 100) * 2)
                    Else
                        ColorNBT = 0
                    End If
                    If cbVAT.Checked = True Then
                        COlorVAT = (((ColorInvoiceValue + ColorNBT) / 100) * 15)
                    Else
                        COlorVAT = 0
                    End If

                    txtColorInvoiceValue.Text = ColorInvoiceValue.ToString("N2")
                    txtNBT.Text = (BwNBT + ColorNBT).ToString("N2")
                    txtVAT.Text = (BwVAT + COlorVAT).ToString("N2")
                    If Trim(txtRental.Text) = "" Then
                        rental_Val = 0
                        txtRental.Text = 0
                    Else
                        rental_Val = CDbl(txtRental.Text)
                        txtRental.Text = rental_Val.ToString("N2")
                    End If
                    '           bW                              + Color                            + rental
                    NetValue = (InvoiceValue + BwNBT + BwVAT) + (ColorInvoiceValue + ColorNBT + COlorVAT) + rental_Val

                    txtNetValue.Text = NetValue.ToString("N2")


                    'Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)

                    'NetValue = (InvoiceValue + BwNBT + BwVAT) + (ColorInvoiceValue + ColorNBT + COlorVAT) + rental_Val

                    'txtNetValue.Text = NetValue.ToString("N2")
                End If



            End If

            If rbtnRental.Checked = True Then
                InvoiceValue = 0
                ColorInvoiceValue = 0
                dgBw.Item(3, 0).Value = Nothing
                dgColor.Item(3, 0).Value = Nothing

                Dim ExceedCommitment As Integer = 0
                Dim ExceededCopyValue As Double = 0
                Dim RentalNBT As Double = 0
                Dim RentalVAT As Double = 0


                ExceedCommitment = InvCopyCount - dgBw.Item(1, 0).Value

                If Trim(txtRental.Text) = "" Then
                    rental_Val = 0
                    txtRental.Text = 0
                Else
                    rental_Val = CDbl(txtRental.Text)
                    txtRental.Text = rental_Val.ToString("N2")
                End If

                If InvCopyCount <= dgBw.Item(1, 0).Value Then
                    InvoiceValue = 0
                    dgBw.Item(3, 0).Value = 0
                Else

                    InvoiceValue = (ExceedCommitment * dgBw.Item(2, 0).Value)
                    dgBw.Item(3, 0).Value = ExceedCommitment
                End If


                'InvoiceValue = (InvCopyCount * dgBw.Item(2, 0).Value)


                If InvColorCopyCount <= dgColor.Item(1, 0).Value Then
                    ColorInvoiceValue = 0
                    dgColor.Item(3, 0).Value = 0
                Else

                    ColorInvoiceValue = (ExceedCommitment * dgColor.Item(2, 0).Value)
                    dgColor.Item(3, 0).Value = ExceedCommitment
                End If


                'ColorInvoiceValue = (InvColorCopyCount * dgColor.Item(2, 0).Value)


                If cbNBT.Checked = True Then
                    BwNBT = (InvoiceValue / 100) * 2
                Else
                    BwNBT = 0
                End If

                If cbVAT.Checked = True Then
                    BwVAT = ((InvoiceValue + BwNBT) / 100) * 15
                Else
                    BwVAT = 0
                End If



                txtInvoiceValue.Text = (InvoiceValue).ToString("N2")

                If cbNBT.Checked = True Then
                    ColorNBT = (ColorInvoiceValue / 100) * 2
                Else
                    ColorNBT = 0
                End If

                If cbVAT.Checked = True Then
                    COlorVAT = ((ColorInvoiceValue + ColorNBT) / 100) * 15
                Else
                    COlorVAT = 0
                End If


                txtColorInvoiceValue.Text = (ColorInvoiceValue).ToString("N2")

                If cbNBT.Checked = True Then
                    RentalNBT = (rental_Val / 100) * 2
                Else
                    RentalNBT = 0
                End If

                If cbVAT.Checked = True Then
                    RentalVAT = ((rental_Val + RentalNBT) / 100) * 15
                Else
                    RentalVAT = 0
                End If



                txtNBT.Text = (BwNBT + ColorNBT + RentalNBT).ToString("N2")
                txtVAT.Text = (BwVAT + COlorVAT + RentalVAT).ToString("N2")

                NetValue = (InvoiceValue + BwNBT + BwVAT) + (ColorInvoiceValue + ColorNBT + COlorVAT) + (rental_Val + RentalNBT + RentalVAT)
                txtNetValue.Text = NetValue.ToString("N2")
            End If


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

    End Sub





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
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub

#End Region

#Region "Validation"

    Dim IsErrorHave As Boolean = False
    Private Function isDataValid()
        isDataValid = False

        ErrorProvider1.Clear()





        If generalValObj.isPresent(txtCustomerID) = False Then
            IsErrorHave = True
        End If
        If generalValObj.isPresent(txtCustomerName) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtRepCode) = False Then
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
                If dgMR.Rows(row.Index).Cells("MR_COPIES").Value <> Nothing Then
                    If dgMR.Rows(row.Index).Cells("MR_COPIES").Value < 0 Then
                        MessageBox.Show("Serial " & dgMR.Rows(row.Index).Cells("SN").Value & " Copy Count is minus.", "Minus Value Detected.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Function
                    End If
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

        txtCustomerID.Focus()
        dtpEnd.Value = Today.Date
        dtpStart.Value = Today.Date.AddMonths(-1)


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

        dgMR.Rows.Clear()
        dgBw.Rows.Clear()
        dgColor.Rows.Clear()
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
        txtColorInvCopies.Text = ""
        txtInvoiceValue.Text = ""
        txtColorInvoiceValue.Text = ""
        txtNBT.Text = ""
        txtVAT.Text = ""
        cbNBT.Checked = False
        cbVAT.Checked = False
        txtNetValue.Text = ""
        lblVatType.Text = "##"
        lblVatTypeName.Text = "##"

        txtTotalCopies.Text = ""
        txtTotalColorCopies.Text = ""
        txtTotalWaistage.Text = ""
        txtTotalColorWaistage.Text = ""
        txtRental.Text = ""

        lblInvoiceNo.Text = ""
        lblSDate.Text = ""
        lblEDate.Text = ""
        lblLInvDate.Text = ""

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



    Private Sub txtCustomerID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCustomerID.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT     MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
                lblVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
                lblVatTypeName.Text = dbConnections.dReader.Item("VAT_DESC")

                If IsDBNull(dbConnections.dReader.Item("IS_NBT")) Then
                    cbNBT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_NBT") Then
                        cbNBT.Checked = True
                    Else
                        cbNBT.Checked = False
                    End If
                End If


                If IsDBNull(dbConnections.dReader.Item("IS_VAT")) Then
                    cbVAT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_VAT") Then
                        cbVAT.Checked = True
                    Else
                        cbVAT.Checked = False
                    End If
                End If

            End While
            dbConnections.dReader.Close()




            globalFunctions.globalButtonActivation(True, True, False, False, False, False)
            Me.saveBtnStatus()

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try

    End Sub






#End Region






    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"
    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgMR.EditingControlShowing

        If Me.dgMR.CurrentCell.ColumnIndex = 5 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            tb.Name = "txtEndReading"

            AddHandler tb.Validating, AddressOf TextBox_Validating
        ElseIf Me.dgMR.CurrentCell.ColumnIndex = 7 And Not e.Control Is Nothing Then
            Dim txtWaistage As TextBox = CType(e.Control, TextBox)
            txtWaistage.Name = "txtWaistage"
            AddHandler txtWaistage.Validating, AddressOf txtWaistage_Validating


        ElseIf Me.dgMR.CurrentCell.ColumnIndex = 9 And Not e.Control Is Nothing Then
            Dim txtColorEndReading As TextBox = CType(e.Control, TextBox)
            txtColorEndReading.Name = "txtColorEndReading"
            AddHandler txtColorEndReading.Validating, AddressOf txtColorEndReading_Validating

        ElseIf Me.dgMR.CurrentCell.ColumnIndex = 11 And Not e.Control Is Nothing Then
            Dim txtColorWaistage As TextBox = CType(e.Control, TextBox)
            txtColorWaistage.Name = "txtColorWaistage"
            AddHandler txtColorWaistage.Validating, AddressOf txtColorWaistage_Validating


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


            If rbtnRental.Checked = True Then

                If AllCopieseThisMOnth <= dgBw.Item(1, 0).Value Then
                    AllCopieseThisMOnth = 0
                Else

                    AllCopieseThisMOnth = (AllCopieseThisMOnth - dgBw.Item(1, 0).Value)
                End If

            End If

            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth
            AllCopieseThisMOnth = 0
            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try

    End Sub

    Private Sub txtColorWaistage_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(8, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(9, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(9, dgMR.CurrentCell.RowIndex).Value)
            End If

            AllCopieseThisMOnth = (EndReading - StartReading)


            If rbtnRental.Checked = True Then

                If AllCopieseThisMOnth <= dgColor.Item(1, 0).Value Then
                    AllCopieseThisMOnth = 0
                Else

                    AllCopieseThisMOnth = (AllCopieseThisMOnth - dgColor.Item(1, 0).Value)
                End If

            End If


            dgMR.Item(10, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try

    End Sub


    Private Sub txtColorEndReading_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(8, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(9, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(9, dgMR.CurrentCell.RowIndex).Value)
            End If

            AllCopieseThisMOnth = (EndReading - StartReading)


            If rbtnRental.Checked = True Then

                If AllCopieseThisMOnth <= dgColor.Item(1, 0).Value Then
                    AllCopieseThisMOnth = 0
                Else

                    AllCopieseThisMOnth = (AllCopieseThisMOnth - dgColor.Item(1, 0).Value)
                End If

            End If


            dgMR.Item(10, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth
            AllCopieseThisMOnth = 0
            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try

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

            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try

    End Sub


#End Region


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

    Private Sub btnProcessInvoice_Click(sender As Object, e As EventArgs) Handles btnProcessInvoice.Click
        Process_Invoice()
    End Sub



    'Private Sub dgMR_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgMR.CellFormatting
    '    dgMR.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)
    'End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Try

            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"

                Dim path As String = globalVariables.crystalReportpath & "\Reports\rptKBOInvoiceColorBreakup.rpt"

                'Dim manual report As New rptBank

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

                cryRpt.PrintOptions.PrinterName = globalVariables.DefaultPrinterName

                '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
                Try
                    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
                    Dim PkSize As New System.Drawing.Printing.PaperSize
                    ObjPrinterSetting.PrinterName = globalVariables.DefaultPrinterName
                    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "Letter" Then
                            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                            Exit For
                        End If
                    Next

                    If PkSize IsNot Nothing Then
                        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                        myAppPrintOptions.PrinterName = globalVariables.DefaultPrinterName
                        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

                    End If
                    PkSize = Nothing



                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try



                'cryRpt.PrintOptions.PrinterName = "EPSON LQ-310 ESC/P2 (Copy 1)"
                cryRpt.PrintToPrinter(1, False, 0, 0)
                'reportformObj.CrystalReportViewer1.Refresh()
                'reportformObj.CrystalReportViewer1.ReportSource = cryRpt
                'reportformObj.CrystalReportViewer1.Refresh()
                'reportformObj.Show()
            End Using


            Try
                Dim strSQL As String
                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET              INV_PRINTED =@INV_PRINTED WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PRINTED", True)
                dbConnections.sqlCommand.ExecuteNonQuery()

                dbConnections.sqlTransaction.Commit()
            Catch ex As Exception

            End Try



        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub


    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click



        '/=======================
        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"
        Dim AdminUser As Boolean = False
        Dim path As String = ""
        path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoiceColorBreakup.rpt"


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
        reportformObj.Show()

    End Sub


    Private Sub txtInvoiceNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtInvoiceNo.Validating
        LoadSelectedAgreement(txtInvoiceNo.Text)
    End Sub

    Private Sub txtInvoiceNo_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceNo.TextChanged

    End Sub

    Private Sub dgAgreement_CellContextMenuStripChanged(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Save1()
    End Sub


    Private Function Save1() As Boolean

        Dim GrandTotal As Double = 0
        Dim Rental As Double = 0

        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                Rental = 0
                LoadSelectedAgreement(Trim(DataGridView1.Rows(row.Index).Cells(0).Value))
                dbConnections.sqlCommand.Parameters.Clear()
                If Trim(txtRental.Text) = "" Then
                    Rental = 0
                Else
                    Rental = CDbl(txtRental.Text)
                End If
                Threading.Thread.Sleep(200)
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET  INV_VAL ='" & (CDbl(txtInvoiceValue.Text) + CDbl(txtColorInvoiceValue.Text) + Rental) & "' WHERE     (INV_NO = @INV_NO)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(DataGridView1.Rows(row.Index).Cells(0).Value))

                If dbConnections.sqlCommand.ExecuteNonQuery() Then Save1 = True Else Save1 = False
            Next

            If Save1 Then
                MsgBox("Saved")

            End If
        Catch ex As Exception

        End Try

        Return Save1
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProgressBar1.Maximum = DataGridView1.RowCount 'Set Max Length
        ProgressBar1.Step = 1 'Set Step
        ProgressBar1.Value = 0 'Set Begin value
        Save1()
    End Sub



    Private Sub AddtoGrid()
        Dim rowCount As Integer = 0
        Dim INVList As New List(Of String)
        DataGridView1.Rows.Clear()
        Try
            connectionStaet()

            strSQL = "SELECT     TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT FROM         TBL_INVOICE_MASTER INNER JOIN                       TBL_CUS_AGREEMENT ON TBL_INVOICE_MASTER.COM_ID = TBL_CUS_AGREEMENT.COM_ID AND TBL_INVOICE_MASTER.AG_ID = TBL_CUS_AGREEMENT.AG_ID AND  TBL_INVOICE_MASTER.CUS_ID = TBL_CUS_AGREEMENT.CUS_CODE WHERE  TBL_CUS_AGREEMENT.MACHINE_TYPE = 'COLOR' AND TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                If INVList.Contains(dbConnections.dReader.Item("INV_NO")) Then

                Else
                    populatreDatagrid(dbConnections.dReader.Item("INV_NO"), dbConnections.dReader.Item("IS_VAT"), dbConnections.dReader.Item("IS_NBT"))

                    INVList.Add(dbConnections.dReader.Item("INV_NO"))
                End If




                rowCount = rowCount + 1
            End While


            ProgressBar1.Maximum = DataGridView1.RowCount 'Set Max Length
            ProgressBar1.Step = 1 'Set Step
            ProgressBar1.Value = 0 'Set Begin value
        Catch ex As Exception
            dbConnections.dReader.Close()


        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef INV As String, ByRef vat As Boolean, ByRef nbt As Boolean)
        DataGridView1.ColumnCount = 3
        DataGridView1.Rows.Add(INV, vat, nbt)
    End Sub
End Class