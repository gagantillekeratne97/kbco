Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports Dapper
Imports System.IO
Imports System.Configuration
Imports System.Runtime.InteropServices.ComTypes
Imports System.Windows.Interop
Imports System.Runtime.Remoting.Messaging

Public Class frmInternalRequest

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
    Private SavedIR_NO As String
    Private IsNegative_Internal As String = "P"

    '//Get the connectionString
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If save() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                showCrystalReport()
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
        Dim success As Boolean = False
        If MessageBox.Show(SaveMessage, "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) <> vbYes Then
            Exit Function
        End If

        If isDataValid() = False Then
            Exit Function
        End If

        Dim sriLankanTimeZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time")
        Dim utcNow As DateTime = DateTime.UtcNow
        Dim sriLankaNow As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, sriLankanTimeZone)
        Dim sriLankaDate As DateTime = sriLankaNow.Date

        Dim hasDebtorsIssue As Boolean = (lblDebtors.Text = "YES")
        Dim irNo As String = GenerateIRNo()

        If String.IsNullOrEmpty(irNo) Then
            MessageBox.Show("Failed to generate IR number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If

        Dim connection As SqlConnection = Nothing
        Dim transaction As SqlTransaction = Nothing
        Try
            connection = New SqlConnection(connectionString)
            connection.Open()
            transaction = connection.BeginTransaction()
            Dim mainSql As String = "INSERT INTO TBL_INTERNAL_MAIN " &
            "(COM_ID, IR_NO, IR_DATE, SERIAL_NO, PN_NO, CUS_CODE, CUS_LOC, " &
            " ISSUED_TO, CURRENT_MR, IR_TYPE, IR_STATE, CR_BY, CR_DATE, IR_PRINTED, COMMENT) " &
            "VALUES " &
            "(@COM_ID, @IR_NO, @IR_DATE, @SERIAL_NO, @PN_NO, @CUS_CODE, @CUS_LOC, " &
            " @ISSUED_TO, @CURRENT_MR, @IR_TYPE, @IR_STATE, @CR_BY, GETDATE(), @IR_PRINTED, @COMMENT)"

            Dim irState As String = ""
            If hasDebtorsIssue Then
                irState = "PENDING APPROVAL"
            Else
                If IsNegative_Internal = "P" OrElse IsNegative_Internal = "N" Then
                    If globalVariables.selectedCompanyID = "003" Then
                        irState = "PENDING GM APPROVAL"
                    Else
                        irState = "PENDING APPROVAL"
                    End If
                Else
                    Throw New Exception("Invalid negative internal flag")
                End If
            End If
            Dim commentValue As Object = DBNull.Value
            If Trim(txtComment.Text) <> "" Then
                commentValue = Trim(txtComment.Text)
            End If
            Dim mainParams = New With {
            .COM_ID = globalVariables.selectedCompanyID,
            .IR_NO = irNo,
            .IR_DATE = sriLankaNow,
            .SERIAL_NO = Trim(txtSerial.Text),
            .PN_NO = Trim(txtPNo.Text),
            .CUS_CODE = Trim(txtCusCode.Text),
            .CUS_LOC = Trim(txtCusAdd.Text),
            .ISSUED_TO = Trim(txtTechCode.Text),
            .CURRENT_MR = Trim(txtCurrentMR.Text),
            .IR_TYPE = Trim(cmbIRType.Text),
            .IR_STATE = irState,
            .CR_BY = userSession,
            .IR_PRINTED = False,
            .COMMENT = commentValue
            }
            Dim rows As Integer = connection.Execute(mainSql, mainParams, transaction)
            If rows <> 1 Then
                Throw New Exception("Failed to insert main internal record")
            End If
            Dim itemSql As String =
            "INSERT INTO TBL_INTERNAL_ITEMS " &
            "(COM_ID, IR_NO, IR_DATE, SERIAL_NO, PN, PN_DESC, PN_QTY, PN_TYPE, " &
            " PN_VALUE, MR_TO_DATE, PREVIOUS_READING, CURRENT_READING, COPIES, STD_YIELD) " &
            "VALUES " &
            "(@COM_ID, @IR_NO, @IR_DATE, @SERIAL_NO, @PN, @PN_DESC, @PN_QTY, @PN_TYPE, " &
            " @PN_VALUE, @MR_TO_DATE, @PREVIOUS_READING, @CURRENT_READING, @COPIES, @STD_YIELD)"
            Dim currentMR As Integer = 0
            Integer.TryParse(Trim(txtCurrentMR.Text), currentMR)

            Dim row As DataGridViewRow
            For Each row In dgInternal.Rows
                Dim pnDesc As String = ""
                If row.Cells("PN_DESC").Value IsNot Nothing Then
                    pnDesc = Trim(row.Cells("PN_DESC").Value.ToString())
                End If

                If pnDesc = "" Then Continue For

                Dim pn As String = ""
                If row.Cells("IR_PN").Value IsNot Nothing Then pn = Trim(row.Cells("IR_PN").Value.ToString())

                Dim pnQty As String = ""
                If row.Cells("IR_QTY").Value IsNot Nothing Then pnQty = Trim(row.Cells("IR_QTY").Value.ToString())

                Dim pnType As String = ""
                If row.Cells("TYPE").Value IsNot Nothing Then pnType = Trim(row.Cells("TYPE").Value.ToString())

                Dim pnValue As Decimal = 0
                If row.Cells("IR_VAL").Value IsNot Nothing Then
                    Decimal.TryParse(row.Cells("IR_VAL").Value.ToString(), pnValue)
                End If

                Dim prevReading As Integer = 0
                If row.Cells("IR_P_READING").Value IsNot Nothing Then
                    Integer.TryParse(row.Cells("IR_P_READING").Value.ToString(), prevReading)
                End If

                Dim copies As Integer = 0
                If row.Cells("IR_COPIES").Value IsNot Nothing Then
                    Integer.TryParse(row.Cells("IR_COPIES").Value.ToString(), copies)
                End If

                Dim stdYield As Integer = 0
                If row.Cells("IR_YIELD").Value IsNot Nothing Then
                    Integer.TryParse(row.Cells("IR_YIELD").Value.ToString(), stdYield)
                End If

                Dim itemParams = New With {
                    .COM_ID = globalVariables.selectedCompanyID,
                    .IR_NO = irNo,
                    .IR_DATE = sriLankaDate,
                    .SERIAL_NO = Trim(txtSerial.Text),
                    .PN = pn,
                    .PN_DESC = pnDesc,
                    .PN_QTY = pnQty,
                    .PN_TYPE = pnType,
                    .PN_VALUE = pnValue,
                    .MR_TO_DATE = currentMR,
                    .PREVIOUS_READING = prevReading,
                    .CURRENT_READING = currentMR,
                    .COPIES = copies,
                    .STD_YIELD = stdYield
                }

                rows = connection.Execute(itemSql, itemParams, transaction)
                If rows <> 1 Then
                    Throw New Exception("Failed to insert item line for PN: " & pn)
                End If
            Next
            transaction.Commit()
            success = True

            txtIRNo.Text = irNo
            txtViewInternalNo.Text = irNo
        Catch ex As Exception
            If transaction IsNot Nothing Then
                transaction.Rollback()
            End If
            MessageBox.Show(ex.Message,
                        "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            success = False
        Finally
            If transaction IsNot Nothing Then Dispose()
            If connection IsNot Nothing Then
                If connection.State <> ConnectionState.Closed Then
                    connection.Close()
                End If
                connection.Dispose()
            End If
        End Try

        Return success
    End Function

    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False


        Return delete
    End Function

    Private Sub FormEdit()

        'Dim conf = MessageBox.Show("" & EditMessage & "" & txtVatTypeName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        'If conf = vbYes Then

        '    isEditClicked = True
        '    globalFunctions.globalButtonActivation(True, True, False, False, False, True)
        '    Me.saveBtnStatus()
        'End If
    End Sub

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmInternalRequest_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalRequest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalRequest_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalRequest_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmInternalRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
        bgWorkerStarup.RunWorkerAsync()
        'globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()
        'cmbPrinterList.Text = globalVariables.DefaultPrinterName
    End Sub

    Private Sub frmInternalRequest_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''all functions of the form .......................................................
    '===================================================================================================================
#Region "Functions & Subs"


    Private Sub GetLastIRInfo()
        Try
            Dim sql As String = "
            SELECT TOP 1 IR_NO, IR_DATE
            FROM TBL_INTERNAL_MAIN 
            WHERE COM_ID = @companyid
            ORDER BY IR_DATE DESC"

            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim result = connection.QueryFirstOrDefault(Of IRInfo)(
                sql, New With {.CompanyID = globalVariables.selectedCompanyID.Trim()})

                If result IsNot Nothing Then
                    lblIRNo.Text = If(result.IR_NO, String.Empty)
                    lblLInvDate.Text = If(result.IR_DATE?.ToString(), String.Empty)
                Else
                    lblIRNo.Text = String.Empty
                    lblLInvDate.Text = String.Empty
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function IsPrint_Enable() As Boolean
        Const SQL As String = "
        SELECT CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM TBL_INTERNAL_MAIN
                WHERE IR_NO = @IR_NO
                  AND COM_ID = @COM_ID
                  AND IR_STATE IN (
                      'UPLOADED TO BELEETA',
                      'PENDING DISPATCH',
                      'INTERNAL CANCELLED',
                      'INTERNAL PRINT PENDING',
                      'APPROVED'
                  )
            ) THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END"

        Try
            Using conn As New SqlConnection(connectionString)
                conn.Open()

                Dim result = conn.ExecuteScalar(Of Boolean)(
                SQL,
                New With {
                    .IR_NO = txtViewInternalNo.Text?.Trim(),
                    .COM_ID = globalVariables.selectedCompanyID
                }
            )

                Return result
            End Using

        Catch ex As Exception
            ' Consider proper logging instead of MsgBox in production
            MessageBox.Show(
            "Error checking print permission:" & vbCrLf & vbCrLf & ex.Message,
            "Database Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )

            Return False
        End Try
    End Function

    Private Sub IsNegative()
        IsNegative_Internal = "P"
        Try
            For i = 0 To dgInternal.Rows.Count - 2
                'totalSellings = totalSellings + (dgInternal.Rows(i).Cells("SalesPrice").Value)
                CurrentMRval = 0
                YieldPerItem = 0
                TotalYield = 0
                PreviousReading = 0
                ReqQty = 0
                CurrentCopies = 0
                UnitPrice = 0.0

                Dim Hasrecord As Boolean = False
                Try
                    If Not IsDBNull(dgInternal.Rows(i).Cells("IR_PN").Value) And Trim(dgInternal.Rows(i).Cells("IR_PN").Value) <> "" Then
                        If Trim(txtCurrentMR.Text) = "" Then
                            CurrentMRval = 0
                        Else
                            CurrentMRval = CInt(Trim(txtCurrentMR.Text))
                        End If



                        '// check in use row
                        If Trim(dgInternal.Rows(i).Cells("PN_DESC").Value) <> "" Then
                            If dgInternal.Rows(i).Cells("IR_QTY").Value = 0 Then
                                ReqQty = 1
                            Else
                                ReqQty = CInt(dgInternal.Rows(i).Cells("IR_QTY").Value)
                            End If
                        End If



                        '// CALCULATING YIELD
                        If IsDBNull(dgInternal.Rows(i).Cells("IR_YIELD").Value) Then
                            YieldPerItem = 0
                        Else
                            YieldPerItem = dgInternal.Rows(i).Cells("IR_YIELD").Value
                        End If

                        '// get previous reading
                        If IsDBNull(dgInternal.Item(5, i).Value) Then
                            PreviousReading = 0
                        Else
                            PreviousReading = dgInternal.Item(5, i).Value
                        End If


                        TotalYield = (YieldPerItem * ReqQty)
                        'dgInternal.Item(7, dgInternal.CurrentCell.RowIndex).Value = TotalYield
                        '// set current copies to column
                        CurrentCopies = (CurrentMRval - PreviousReading)
                        dgInternal.Rows(i).Cells("IR_COPIES").Value = CurrentCopies

                        '// check negative or positive
                        If (YieldPerItem * ReqQty) <> 0 Then
                            If TotalYield >= CurrentCopies Then
                                IsNegative_Internal = "N"
                                dgInternal.Rows(i).DefaultCellStyle.BackColor = Color.MistyRose



                            Else
                                'If IsNegative_Internal <> "N" Then
                                '    IsNegative_Internal = "P"
                                dgInternal.Rows(i).DefaultCellStyle.BackColor = Color.White
                                'End If

                            End If

                            If IsNegative_Internal = "N" Then
                                lblNPState.Text = "Negative"
                                lblNPState.ForeColor = Color.DarkRed
                            Else
                                lblNPState.Text = "Positive"
                                lblNPState.ForeColor = Color.DarkGreen
                            End If
                        End If
                    End If




                Catch ex As Exception
                    dbConnections.dReader.Close()
                    MsgBox(ex.Message)

                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function UpdateIRPrint() As Boolean
        UpdateIRPrint = False
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim updateQuery As String = "
                UPDATE TBL_INTERNAL_MAIN SET IR_STATE = @irstate,
                IR_PRINTED = @irprinted
                WHERE COM_ID = @companyid AND IR_NO = @irno"

                Dim companyId As String = globalVariables.selectedCompanyID.Trim()

                Dim result = connection.Execute(updateQuery, New With {
                    .companyid = companyId,
                    .irno = txtIRNo.Text.Trim(),
                    .irprinted = True,
                    .irstate = "PENDING DISPATCH"
                    })

                If result > 0 Then
                    MessageBox.Show("Internal Print Successful.", "Printed.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Load_IR_Info_View()
        If Trim(txtViewInternalNo.Text) = "" Then Exit Sub

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim result = connection.QueryFirstOrDefault(
                "SELECT m.CUS_CODE, c.CUS_NAME
                 FROM TBL_INTERNAL_MAIN m
                 INNER JOIN MTBL_CUSTOMER_MASTER c
                    ON m.COM_ID = c.COM_ID AND m.CUS_CODE = c.CUS_ID
                 WHERE m.IR_NO = @IRNo
                   AND m.COM_ID = @CompanyID",
                New With {
                    .IRNo = txtViewInternalNo.Text.Trim(),
                    .CompanyID = globalVariables.selectedCompanyID
                })

                If result IsNot Nothing Then
                    txtVICusCode.Text = If(result.CUS_CODE Is Nothing, String.Empty, result.CUS_CODE.ToString())
                    txtVICusName.Text = If(result.CUS_NAME Is Nothing, String.Empty, result.CUS_NAME.ToString())
                Else
                    txtVICusCode.Text = String.Empty
                    txtVICusName.Text = String.Empty
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Loading_Printer_List()
        Try
            Dim InstalledPrinters As String

            ' Find all printers installed
            For Each InstalledPrinters In
                System.Drawing.Printing.PrinterSettings.InstalledPrinters
                Me.cmbPrinterList.Items.Add(InstalledPrinters)
            Next InstalledPrinters

            ' Set the combo to the first printer in the list
            Me.cmbPrinterList.SelectedIndex = 0
            Me.cmbPrinterList.Text = globalVariables.DefaultPrinterName

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


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

    Private Function Search(ByVal searchBy As String) As Boolean
        If String.IsNullOrWhiteSpace(txtSearch.Text) Then Return False

        Try
            Dim companyID As String = globalVariables.selectedCompanyID.Trim()
            Dim searchTerm As String = txtSearch.Text.Trim()
            Dim whereClause As String = If(searchBy = "SN", "SERIAL = @SearchTerm", "P_NO = @SearchTerm")
            'Dim whereClause As String = "SERIAL = @SearchTerm"
            Dim connection As New SqlConnection(connectionString)
            connection.Open()

            Dim searchForMachineQuery As String =
            $"SELECT AG_ID, SERIAL, P_NO, CUS_ID 
                            From TBL_MACHINE_TRANSACTIONS
                            Where COM_ID = @companyid And {whereClause}"

            Dim transaction = connection.QueryFirstOrDefault(
            searchForMachineQuery, New With {
                .companyid = companyID,
                .SearchTerm = searchTerm
                })

            If transaction Is Nothing OrElse String.IsNullOrEmpty(transaction.SERIAL) Then
                Return False
            End If

            Dim selectedSN As String = transaction.SERIAL
            Dim selectedAg As String = transaction.AG_ID
            Dim selCusCode As String = transaction.CUS_ID

            Dim isBlocked = connection.QueryFirstOrDefault(
            "Select CUS_ID From TBL_BLOCK_CUSTOMER
             Where COM_ID = @CompanyID And CUS_ID = @CusID",
            New With {.CompanyID = companyID, .CusID = selCusCode})

            If isBlocked IsNot Nothing Then
                MessageBox.Show("This Is A Blocked Customer. Please contact your immediate manager.")
                FormClear()
                Return False
            End If

            Dim cusName As String = connection.QueryFirstOrDefault(Of String)(
           "Select CUS_NAME From MTBL_CUSTOMER_MASTER
             Where COM_ID = @CompanyID And CUS_ID = @CusID",
           New With {.CompanyID = companyID, .CusID = selCusCode})

            txtCusCode.Text = selCusCode
            txtCusName.Text = If(cusName, String.Empty)

            Dim machine = connection.QueryFirstOrDefault(
            "Select MACHINE_PN, SERIAL, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC,
                    M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO,
                    INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE
                From TBL_MACHINE_TRANSACTIONS
             Where COM_ID = @CompanyID And AG_ID = @AgID And SERIAL = @Serial",
            New With {.CompanyID = companyID, .AgID = selectedAg, .Serial = selectedSN})


            If machine IsNot Nothing Then
                txtSerial.Text = machine.SERIAL
                txtPNo.Text = If(machine.P_NO?.ToString(), String.Empty)
                txtTechCode.Text = If(machine.TECH_CODE?.ToString(), String.Empty)
                txtSpecialCase.Text = If(machine.SPECIAL_CASE_DESC?.ToString(), String.Empty)

                ' Location logic varies by company
                If companyID = "003" Then
                    Dim location As String = $"{machine.M_LOC1} {machine.M_LOC2} {machine.M_LOC3}".Trim()
                    txtCusAdd.Text = location
                Else
                    txtCusAdd.Text = If(machine.M_DEPT?.ToString(), String.Empty)
                End If

                ' ── Step 5: Get machine model ─────────────────────────────────
                Dim machinePN As String = machine.MACHINE_PN?.ToString().Trim()
                If Not String.IsNullOrEmpty(machinePN) Then
                    lblModel.Text = If(
                                         connection.QueryFirstOrDefault(Of String)(
                                    "Select Top 1 MACHINE_MODEL FROM MTBL_MACHINE_MASTER
                                     WHERE COM_ID = @CompanyID AND MACHINE_ID = @MachineID",
                                    New With {.CompanyID = companyID, .MachineID = machinePN}), String.Empty)
                End If

                ' ── Step 6: Get technician name ───────────────────────────────
                If Not String.IsNullOrWhiteSpace(txtTechCode.Text) Then
                    lblTechName.Text = If(
                        connection.QueryFirstOrDefault(Of String)(
                        "SELECT TECH_NAME FROM MTBL_TECH_MASTER
                         WHERE COM_ID = @CompanyID AND TECH_CODE = @TechCode",
                        New With {.CompanyID = companyID, .TechCode = txtTechCode.Text.Trim()}), "ERROR")
                End If
            End If

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Private Function GenerateIRNo() As String
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim companyID As String = globalVariables.selectedCompanyID

                ' Acquire exclusive lock per company
                Dim lockResult As Integer = connection.QueryFirstOrDefault(Of Integer)(
                "EXEC sp_getapplock 
                    @Resource = @LockName, 
                    @LockMode = 'Exclusive', 
                    @LockOwner = 'Session',
                    @LockTimeout = 5000",
                New With {.LockName = "IRNoGen_" & companyID})

                If lockResult < 0 Then
                    MessageBox.Show("Could not acquire lock to generate IR number. Please try again.")
                    'Throw New Exception("Could not acquire lock to generate IR number. Please try again.")
                End If

                Try
                    ' Get the actual max IR number (not SEQ based)
                    Dim sql As String = "
                    SELECT ISNULL(MAX(CAST(PARSENAME(REPLACE(IR_NO, '/', '.'), 1) AS INT)), 0)
                    FROM TBL_INTERNAL_MAIN 
                    WHERE COM_ID = @CompanyID
                    AND IR_NO LIKE @Pattern"

                    Dim maxID As Integer = connection.QueryFirstOrDefault(Of Integer)(
                        sql, New With {
                            .CompanyID = companyID,
                            .Pattern = companyID & "/IR/%"
                        })

                    Dim nextID As Integer = maxID + 1
                    Return $"{companyID}/IR/{nextID}"

                Finally
                    connection.Execute(
                        "EXEC sp_releaseapplock 
                        @Resource = @LockName, 
                        @LockOwner = 'Session'",
                        New With {.LockName = "IRNoGen_" & companyID})
                End Try
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    'Private Function GenerateIRNo() As String
    '    Try
    '        Using connection As New SqlConnection(connectionString)
    '            connection.Open()
    '            Dim companyID As String = globalVariables.selectedCompanyID
    '            Dim sql As String = "
    '            SELECT TOP 1 IR_NO 
    '            FROM TBL_INTERNAL_MAIN 
    '            WHERE COM_ID = @companyID
    '            ORDER BY IR_DATE DESC"

    '            Dim lastIRNo As String = connection.QueryFirstOrDefault(Of String)(
    '                sql, New With {.companyID = companyID})

    '            Dim nextID As Integer = 1
    '            If Not String.IsNullOrEmpty(lastIRNo) Then
    '                Dim parts() As String = lastIRNo.Split("/")
    '                If parts.Length >= 3 Then
    '                    Integer.TryParse(parts(2), nextID)
    '                End If
    '            End If

    '            Dim existingIDs As IEnumerable(Of Integer) =
    '                connection.Query(Of Integer)(
    '                "SELECT CAST(PARSENAME(REPLACE(IR_NO, '/', '.'), 1) AS INT)
    '                 FROM TBL_INTERNAL_MAIN
    '                 WHERE COM_ID = @CompanyID
    '                 AND IR_NO LIKE @Pattern", New With {
    '                 .CompanyID = companyID,
    '                 .Pattern = companyID & "/IR/%"
    '                 })

    '            Dim usedIDSet As New HashSet(Of Integer)(existingIDs)

    '            While usedIDSet.Contains(nextID)
    '                nextID += 1
    '            End While

    '            Return $"{companyID}/IR/{nextID}"
    '        End Using
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Function

    Private Function IsDebtorsOutstandingHave(ByRef DaysLimit As Integer, ByRef IsShowMsg As Boolean) As Boolean
        Dim hasOverDue As Boolean = False
        Dim overDueInvNo As String = String.Empty

        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim sql As String = "
                SELECT TOP 1 
                    im.INV_NO,
                    DATEDIFF(DAY, GETDATE(), im.INV_DATE) AS DaysOverdue
                FROM TBL_INVOICE_MASTER im
                LEFT JOIN TBL_RECIPTS r 
                    ON r.COM_ID = im.COM_ID 
                    AND r.INV_NO = im.INV_NO
                WHERE r.RECIPT_ID IS NULL
                    AND im.COM_ID = @CompanyId
                    AND im.CUS_ID = @CustomerId
                    AND DATEDIFF(DAY, GETDATE(), im.INV_DATE) >= @DaysLimit
                ORDER BY im.INV_DATE ASC"

                Dim result = connection.QueryFirstOrDefault(Of OverDueInvoice)(
                    sql, New With {
                    .CompanyId = globalVariables.selectedCompanyID,
                    .CustomerId = txtCusCode.Text.Trim(),
                    .DaysLimit = DaysLimit
                    })

                If result IsNot Nothing Then
                    hasOverDue = True
                    overDueInvNo = result.INV_NO
                End If

                ' Show message only if requested and there is an overdue invoice
                If IsShowMsg AndAlso hasOverDue Then
                    MessageBox.Show(
                        $"Invoice No {overDueInvNo} is not settled within {DaysLimit} days." & vbCrLf &
                        "Please settle this before processing this internal transaction.",
                        "Pending Payment Detected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtSerial) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtPNo) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtCusCode) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtCurrentMR) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtTechCode) = False Then
            Exit Function
        End If




        If dgInternal.Rows.Count = 1 Then
            MessageBox.Show("Please Add Items to proceed this transaction.", "")
            Exit Function
        End If

        If IsDebtorsOutstandingHave(globalVariables.DebtorsCheckDayLimit, True) Then

        End If


        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        GetLastIRInfo()
        IsNegative_Internal = ""
        txtIRNo.Text = GenerateIRNo()
        cmbIRType.SelectedIndex = 0
        txtSearch.Text = ""
        txtSerial.Text = ""
        lblModel.Text = ""
        txtPNo.Text = ""
        txtCusCode.Text = ""
        txtCusName.Text = ""
        txtCusAdd.Text = ""
        txtTechCode.Text = ""
        cmbIRType.SelectedIndex = 0
        txtCurrentMR.Text = ""
        lblMcRefNo.Text = globalVariables.MachineRefCode + " No"
        lblDebtors.Text = ""

        If Trim(txtCurrentMR.Text) = "" Then
            dgInternal.Enabled = False
        Else
            dgInternal.Enabled = True

        End If
        IsNegative_Internal = ""
        dgInternal.Rows.Clear()

        '// cleare report info
        cmbSelectReport.SelectedIndex = 0
        txtRCusID.Text = ""
        txtRSN.Text = ""
        txtRTechCode.Text = ""
        dtpRStartDate.Value = GetFirstDayOfMonth(Today.Date)
        dtpREndDate.Value = GetLastDayOfMonth(Today.Date)
        lblNPState.Text = ""

        txtSearch.Focus()


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
    Private Sub txtViewInternalNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtViewInternalNo.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress

    End Sub



    Private Sub txtSearch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating
        If Trim(txtSearch.Text) = "" Then
            Exit Sub
        End If
        If Search("SN") = False Then
            Search("PNO")
        End If
        bgWorkerDabtorsCheck.RunWorkerAsync()

    End Sub

    Private Sub txtCurrentMR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentMR.KeyPress
        generalValObj.isDigit(e)
    End Sub





    Private Sub txtCurrentMR_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentMR.TextChanged
        If Trim(txtCurrentMR.Text) = "" Then
            dgInternal.Enabled = False
        Else
            dgInternal.Enabled = True

        End If
        IsNegative()
    End Sub
    Private Sub txtViewInternalNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtViewInternalNo.Validating
        Load_IR_Info_View()
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

    Dim path As String
    Private Sub showCrystalReport()
        'Dim reportformObj As New frmCrystalReportViwer
        'Dim reportNamestring As String = "Report"

        'path = ""

        ''path = globalVariables.crystalReportpath & "\Reports\frmKBOInternal.rpt"
        'If globalVariables.selectedCompanyID = "003" Then
        '    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
        'Else
        '    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal.rpt"
        'End If


        'Dim manual report As New rptBank
        'Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        'Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        'Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo



        'cryRpt.Load(path)
        'cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.IR_NO} = '" & Trim(txtViewInternalNo.Text) & "'"

        'With crConnectionInfo
        '    .ServerName = selectedServerName
        '    .DatabaseName = selectedDatabaseName
        '    .UserID = "db_ab8b61_kbco_admin"
        '    .Password = "Ssg789.541351"
        'End With

        'CrTables = cryRpt.Database.Tables
        'For Each CrTable In CrTables
        '    crtableLogoninfo = CrTable.LogOnInfo
        '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
        'Next


        'cryRpt.PrintOptions.PrinterName = globalVariables.DefaultPrinterName
        ''// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
        'Try
        '    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
        '    Dim PkSize As New System.Drawing.Printing.PaperSize
        '    ObjPrinterSetting.PrinterName = globalVariables.DefaultPrinterName
        '    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
        '        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
        '            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
        '            Exit For
        '        End If
        '    Next

        '    If PkSize IsNot Nothing Then
        '        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
        '        myAppPrintOptions.PrinterName = globalVariables.DefaultPrinterName
        '        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
        '        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

        '    End If
        '    PkSize = Nothing
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        'reportformObj.CrystalReportViewer1.ShowPrintButton = False
        'reportformObj.CrystalReportViewer1.Refresh()
        'reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        'reportformObj.CrystalReportViewer1.Refresh()
        'reportformObj.Show()

        'path = ""



        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"
        Dim AdminUser As Boolean = False
        Dim path As String = ""


        'path = globalVariables.crystalReportpath & "\Reports\frmKBOInternal.rpt"
        If globalVariables.selectedCompanyID = "003" Then
            path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
        Else
            path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal.rpt"
        End If

        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
        cryRpt.Load(path)
        cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.IR_NO} = '" & Trim(txtViewInternalNo.Text) & "'"




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
        reportformObj.CrystalReportViewer1.ShowPrintButton = False
        reportformObj.CrystalReportViewer1.Refresh()
        cryRpt.Refresh()
        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.Show()

        path = ""

    End Sub

    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo


    Private Function Internal_Print() As Boolean
        If Trim(Me.Text) = "" Then
            Internal_Print = False
            Exit Function
        End If


        Try
            Dim path As String = ""
            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"
                If globalVariables.selectedCompanyID = "003" Then
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
                Else
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal.rpt"
                End If


                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.IR_NO} = '" & Trim(txtViewInternalNo.Text) & "' and {TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


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
                cryRpt.PrintOptions.PrinterName = cmbPrinterList.Text

                '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
                Try
                    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
                    Dim PkSize As New System.Drawing.Printing.PaperSize
                    ObjPrinterSetting.PrinterName = cmbPrinterList.Text
                    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
                            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                            Exit For
                        End If
                    Next

                    If PkSize IsNot Nothing Then
                        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                        myAppPrintOptions.PrinterName = cmbPrinterList.Text
                        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

                    End If
                    PkSize = Nothing



                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Dim PrtDialog = New PrintDialog
                PrtDialog.PrinterSettings.PrinterName = cmbPrinterList.Text
                cryRpt.PrintOptions.PrinterName = PrtDialog.PrinterSettings.PrinterName

                cryRpt.PrintToPrinter(1, False, 0, 0)

                path = ""
            End Using

            Internal_Print = True
        Catch ex As Exception
            Internal_Print = False
            MsgBox(ex.Message)
        Finally

        End Try
        Return Internal_Print
    End Function

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '==================================================================================================================

#Region "Button Events"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Search("SN") = False Then
            Search("PNO")
        End If
    End Sub
    Private Sub btnPrintViewInternal_Click(sender As Object, e As EventArgs) Handles btnPrintViewInternal.Click

        If IsPrint_Enable() = True Then
            If Internal_Print() Then
                UpdateIRPrint()
            End If
        End If


    End Sub

    Private Sub btnViewInternal_Click(sender As Object, e As EventArgs) Handles btnViewInternal.Click
        showCrystalReport()
    End Sub



    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        IsNegative()
    End Sub

    Private Sub btnViewBackupHistory_Click(sender As Object, e As EventArgs) Handles btnViewBackupHistory.Click
        frmBackupTonerHistory.MdiParent = frmMDImain
        frmBackupTonerHistory.lblCustomerID.Text = txtCusCode.Text
        frmBackupTonerHistory.lblSerialNo.Text = txtSerial.Text
        frmBackupTonerHistory.lblCustomerName.Text = txtCusName.Text
        frmBackupTonerHistory.Show()
    End Sub

    Private Sub btnGenarateReport_Click(sender As Object, e As EventArgs) Handles btnGenarateReport.Click
        Dim techCodeQuery As String = ""
        Dim CusCodeQuery As String = ""
        Dim RepCodeQuery As String = ""

        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"
        Dim RecordFormiula As String = ""
        path = ""
        If cmbSelectReport.Text = "Internal History Report" Then
            path = globalVariables.crystalReportpath & "\Reports\rptKBOInternalHistoryReport.rpt"
            RecordFormiula = ""
            If Trim(path) = "" Then
                Exit Sub
            End If

            'Dim manual report As New rptBank
            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
            cryRpt.Load(path)
            If RecordFormiula = "" Then
                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} ='" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED'"
            Else
                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} ='" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " + RecordFormiula
            End If

            cryRpt.SetParameterValue("Date", dtpRStartDate.Value.ToString("yyyy/MM/dd"))
            cryRpt.SetParameterValue("EDate", dtpREndDate.Value.ToString("yyyy/MM/dd"))

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
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            reportformObj.Show()

            path = ""
        ElseIf cmbSelectReport.Text = "Internal Consumption Report" Then
            path = globalVariables.crystalReportpath & "\Reports\rptInternalSalesReport.rpt"
            RecordFormiula = ""
            If Trim(txtRSN.Text) <> "" Then
                RecordFormiula = "AND {TBL_INTERNAL_MAIN.SERIAL_NO} = '" & Trim(txtRSN.Text) & "' "
            End If

            If Trim(txtRTechCode.Text) <> "" Then
                RecordFormiula = RecordFormiula + "AND {TBL_INTERNAL_MAIN.ISSUED_TO}  = '" & Trim(txtRTechCode.Text) & "' "
            End If

            If Trim(txtRCusID.Text) <> "" Then
                RecordFormiula = RecordFormiula + "AND {TBL_INTERNAL_MAIN.CUS_CODE}  = '" & Trim(txtRCusID.Text) & "'"
            End If
            If Trim(path) = "" Then
                Exit Sub
            End If

            'Dim manual report As New rptBank
            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
            cryRpt.Load(path)
            If RecordFormiula = "" Then
                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} ='" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED'"
            Else
                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} ='" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " + RecordFormiula
            End If

            cryRpt.SetParameterValue("Date", dtpRStartDate.Value.ToString("yyyy/MM/dd"))
            cryRpt.SetParameterValue("EDate", dtpREndDate.Value.ToString("yyyy/MM/dd"))

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
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            reportformObj.Show()

            path = ""
        ElseIf cmbSelectReport.Text = "Yield by Serial Report" Then
            Try
                '/=======================


                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBOYieldbySerialReport.rpt"

                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)


                If Trim(txtRCusID.Text) = "" Then
                    CusCodeQuery = ""
                Else
                    CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRCusID.Text) & "'"
                End If

                If Trim(txtRTechCode.Text) = "" Then
                    techCodeQuery = ""
                Else
                    techCodeQuery = " AND {TBL_INTERNAL_MAIN.ISSUED_TO} = '" & Trim(txtRTechCode.Text) & "'"
                End If

                If Trim(txtRRepCode.Text) = "" Then
                    RepCodeQuery = ""
                Else
                    RepCodeQuery = ""
                End If


                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_ITEMS.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND  {TBL_INTERNAL_ITEMS.IR_DATE} in cdate('" & Format(dtpRStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpREndDate.Value, "MM/dd/yyyy") & "')  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

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
                MsgBox(ex.Message)
            Finally

            End Try
        ElseIf cmbSelectReport.Text = "Internal Cosumable Utilized Report (By Model)" Then
            Try
                '/=======================

                Dim AdminUser As Boolean = False
                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBOInternal_Con_Uty_Report1.rpt"

                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)


                If Trim(txtRCusID.Text) = "" Then
                    CusCodeQuery = ""
                Else
                    CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRTechCode.Text) & "'"
                End If

                If Trim(txtRTechCode.Text) = "" Then
                    techCodeQuery = ""
                Else
                    techCodeQuery = "  AND {TBL_INTERNAL_MAIN.ISSUED_TO} ='" & Trim(txtRCusID.Text) & "'   "
                End If

                If Trim(txtRRepCode.Text) = "" Then
                    RepCodeQuery = ""
                Else
                    RepCodeQuery = ""
                End If


                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpRStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpREndDate.Value, "dd/MM/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

                cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpRStartDate.Value.ToShortDateString & "'"
                cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpREndDate.Value.ToShortDateString & "'"

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
                MsgBox(ex.Message)
            Finally

            End Try
        ElseIf cmbSelectReport.Text = "Internal Cosumable Utilized Report (By Items)" Then
            Try
                '/=======================

                Dim AdminUser As Boolean = False
                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBOInternal_Con_Uty_Report2.rpt"

                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)




                If Trim(txtRCusID.Text) = "" Then
                    CusCodeQuery = ""
                Else
                    CusCodeQuery = "AND {TBL_INTERNAL_MAIN.CUS_CODE} ='" & Trim(txtRCusID.Text) & "'"
                End If

                If Trim(txtRTechCode.Text) = "" Then
                    techCodeQuery = ""
                Else
                    techCodeQuery = " AND  {TBL_INTERNAL_MAIN.ISSUED_TO} = '" & Trim(txtRTechCode.Text) & "'"
                End If

                If Trim(txtRRepCode.Text) = "" Then
                    RepCodeQuery = ""
                Else
                    RepCodeQuery = ""
                End If
                ' {TBL_INTERNAL_MAIN.CUS_CODE} {TBL_INTERNAL_MAIN.ISSUED_TO}




                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpRStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpREndDate.Value, "dd/MM/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

                cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpRStartDate.Value.ToShortDateString & "'"
                cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpREndDate.Value.ToShortDateString & "'"

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
                MsgBox(ex.Message)
            Finally

            End Try
        ElseIf cmbSelectReport.Text = "Machine List Report" Then
            Try
                '/=======================

                Dim AdminUser As Boolean = False
                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBCOMachineList.rpt"


                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)





                cryRpt.RecordSelectionFormula = "{TBL_MACHINE_TRANSACTIONS.COM_ID}  = '" & globalVariables.selectedCompanyID & "'"




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
                MsgBox(ex.Message)
            Finally

            End Try
        ElseIf cmbSelectReport.Text = "Invoice List Report" Then

            Try
                '/=======================

                Dim AdminUser As Boolean = False
                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoiceList.rpt"


                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)

                If Trim(txtRCusID.Text) = "" Then
                    CusCodeQuery = ""
                Else
                    CusCodeQuery = " AND {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
                End If

                If Trim(txtRTechCode.Text) = "" Then
                    techCodeQuery = ""
                Else
                    techCodeQuery = ""
                End If

                If Trim(txtRRepCode.Text) = "" Then
                    RepCodeQuery = ""
                Else
                    RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
                End If
                '{TBL_INVOICE_MASTER.REP_CODE} {TBL_INVOICE_MASTER.CUS_ID}

                cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INVOICE_MASTER.INV_DATE}  in cdate('" & Format(dtpRStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpREndDate.Value, "dd/MM/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

                cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpRStartDate.Value.ToShortDateString & "'"
                cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpREndDate.Value.ToShortDateString & "'"



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
                MsgBox(ex.Message)
            Finally

            End Try
        ElseIf cmbSelectReport.Text = "Invoice List For Month" Then
            Try
                '/=======================

                Dim AdminUser As Boolean = False
                Dim path As String = ""
                path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoiceListForMonth.rpt"

                Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
                Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
                Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

                cryRpt.Load(path)






                If Trim(txtRCusID.Text) = "" Then
                    CusCodeQuery = ""
                Else
                    CusCodeQuery = " AND {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
                End If

                If Trim(txtRTechCode.Text) = "" Then
                    techCodeQuery = ""
                Else
                    techCodeQuery = ""
                End If

                If Trim(txtRRepCode.Text) = "" Then
                    RepCodeQuery = ""
                Else
                    RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
                End If

                '{TBL_INVOICE_MASTER.REP_CODE} {TBL_INVOICE_MASTER.CUS_ID}


                cryRpt.RecordSelectionFormula = "{TBL_INVOICE_DET.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INVOICE_MASTER.INV_DATE} in cdate('" & Format(dtpRStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpREndDate.Value, "dd/MM/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

                cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpRStartDate.Value.ToShortDateString & "'"
                cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpREndDate.Value.ToShortDateString & "'"

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
                MsgBox(ex.Message)
            Finally

            End Try

        End If



    End Sub


    Private Sub btnRptClear_Click(sender As Object, e As EventArgs) Handles btnRptClear.Click
        cmbSelectReport.SelectedIndex = 0
        txtRCusID.Text = ""
        txtRSN.Text = ""
        txtRTechCode.Text = ""
        dtpRStartDate.Value = GetFirstDayOfMonth(Today.Date)
        dtpREndDate.Value = GetLastDayOfMonth(Today.Date)
    End Sub

    Private Sub cmbPrinterList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPrinterList.SelectedIndexChanged
        globalVariables.DefaultPrinterName = cmbPrinterList.Text
        lblSelectedPrinter.Text = globalVariables.DefaultPrinterName
    End Sub



#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"
    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgInternal.EditingControlShowing

        If Me.dgInternal.CurrentCell.ColumnIndex = 1 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            tb.Name = "txtPN"
            AddHandler tb.KeyDown, AddressOf TextBox_KeyDown
            AddHandler tb.Validating, AddressOf TextBox_Validating

        ElseIf Me.dgInternal.CurrentCell.ColumnIndex = 2 And Not e.Control Is Nothing Then

            Dim tb2 As TextBox = CType(e.Control, TextBox)
            tb2.Name = "txtQty"

            RemoveHandler tb2.Validating, AddressOf TextBox1_Validating
            AddHandler tb2.Validating, AddressOf TextBox1_Validating


        End If


        If TypeOf e.Control Is ComboBox Then
            If dgInternal.CurrentCell.ColumnIndex = 3 Then
                Dim cb As ComboBox = TryCast(e.Control, ComboBox)

                'remove handler if it was added before
                RemoveHandler cb.SelectedIndexChanged, AddressOf ColumnCombo1SelectionChanged
                AddHandler cb.SelectedIndexChanged, AddressOf ColumnCombo1SelectionChanged

            End If
        End If
    End Sub


    Private Sub ColumnCombo1SelectionChanged(sender As Object, e As EventArgs)
        Dim sendingComboEdit = TryCast(sender, DataGridViewComboBoxEditingControl)
        Dim selectedValue As Object = sendingComboEdit.Text
        Dim isRecordhave As Boolean = False
        Try
            'strSQL = "SELECT  TOP 1  ISNULL( COPIES,0) AS 'LMR' FROM         TBL_INTERNAL_ITEMS WHERE PN_TYPE='" & selectedValue & "' and  (COM_ID = @COM_ID) AND (SERIAL_NO = '" & Trim(txtSerial.Text) & "') ORDER BY IR_DATE DESC"
            strSQL = "SELECT     TOP (1) ISNULL(TBL_INTERNAL_ITEMS.CURRENT_READING, 0) AS 'LMR'FROM         TBL_INTERNAL_ITEMS INNER JOIN  TBL_INTERNAL_MAIN ON TBL_INTERNAL_ITEMS.IR_NO = TBL_INTERNAL_MAIN.IR_NO AND TBL_INTERNAL_ITEMS.COM_ID = TBL_INTERNAL_MAIN.COM_ID WHERE     (TBL_INTERNAL_ITEMS.PN_TYPE = '" & selectedValue & "') AND (TBL_INTERNAL_ITEMS.COM_ID = @COM_ID) AND (TBL_INTERNAL_ITEMS.SERIAL_NO = '" & Trim(txtSerial.Text) & "') and  TBL_INTERNAL_MAIN.IR_STATE <> 'INTERNAL CANCELLED' ORDER BY TBL_INTERNAL_ITEMS.IR_DATE DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                isRecordhave = True

                '// Adding Previous Reading
                If IsDBNull(dbConnections.dReader.Item("LMR")) Then
                    PreviousReading = 0
                Else
                    PreviousReading = dbConnections.dReader.Item("LMR")
                End If

                dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = PreviousReading

            End While
            dbConnections.dReader.Close()

            If isRecordhave = False Then
                dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = 0
            End If
            Calculate(dgInternal.Item(8, dgInternal.CurrentCell.RowIndex).Value, CInt(txtCurrentMR.Text), dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value)
        Catch ex As Exception

        End Try


    End Sub


    'Private Sub ItemsDataGridView_CellValidating(sender As System.Object, e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgInternal.CellValidating
    '    If e.RowIndex >= 0 Then
    '        Select Case e.ColumnIndex
    '            Case 0
    '                'Dim zcell = ItemsDataGridView.Item(e.ColumnIndex, e.RowIndex).Value
    '                Dim zcell = e.FormattedValue
    '                MsgBox(zcell)
    '                If String.IsNullOrEmpty(zcell) Then
    '                    MessageBox.Show("You have left the cell empty")
    '                    e.Cancel = True
    '                End If
    '        End Select
    '    End If
    'End Sub


    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtTechCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTechCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtRSN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRSN.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtRTechCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRTechCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtRCusID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRCusID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub


    Dim CurrentMRval As Integer
    Dim YieldPerItem As Integer
    Dim TotalYield As Integer
    Dim PreviousReading As Integer = 0
    Dim ReqQty As Integer
    Dim CurrentCopies As Integer
    Dim UnitPrice As Decimal = 0
    Dim IsCalculateDRM_TON_DEV As Boolean = False
    Private Sub TextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        CurrentMRval = 0
        YieldPerItem = 0
        TotalYield = 0
        PreviousReading = 0
        ReqQty = 0
        CurrentCopies = 0
        UnitPrice = 0.0
        IsCalculateDRM_TON_DEV = False
        Dim isPreviousRedingHave As Boolean = False
        Dim Hasrecord As Boolean = False
        Try

            If Trim(txtCurrentMR.Text) = "" Then
                CurrentMRval = 0
            Else
                CurrentMRval = CInt(Trim(txtCurrentMR.Text))
            End If
            dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = 0

            If dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value = "" Then
                IsCalculateDRM_TON_DEV = False
            ElseIf dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value = "TON" Then
                IsCalculateDRM_TON_DEV = True
            ElseIf dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value = "DEV" Then
                IsCalculateDRM_TON_DEV = True
            ElseIf dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value = "DRM" Then
                IsCalculateDRM_TON_DEV = True
            Else
                IsCalculateDRM_TON_DEV = False
            End If


            If IsCalculateDRM_TON_DEV = True Then
                strSQL = " SELECT CASE WHEN EXISTS (SELECT     COM_ID FROM         TBL_INTERNAL_ITEMS WHERE   PN_TYPE='" & dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value & "' and   (SERIAL_NO ='" & Trim(txtSerial.Text) & "') AND (COM_ID = '" & globalVariables.selectedCompanyID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)


                If dbConnections.sqlCommand.ExecuteScalar Then
                    isPreviousRedingHave = True
                Else
                    isPreviousRedingHave = False
                End If

                '// First time adding will capture opening balance
                If isPreviousRedingHave = False Then

                    strSQL = "SELECT     OPENING_BALANCE FROM         TBL_INTERNAL_OPENING_BALANCE WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                    dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerial.Text))
                    dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


                    While dbConnections.dReader.Read



                        '// Adding Previous Reading
                        If IsDBNull(dbConnections.dReader.Item("OPENING_BALANCE")) Then
                            PreviousReading = 0
                        Else
                            PreviousReading = dbConnections.dReader.Item("OPENING_BALANCE")
                        End If

                        dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = PreviousReading

                    End While
                    dbConnections.dReader.Close()


                End If
            Else
                dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = 0
            End If




            strSQL = "SELECT     DAI_NAME, DAI_UNIT_PRICE, WARRANTY_YIELE ,(SELECT  TOP 1  ISNULL( COPIES,0) FROM         TBL_INTERNAL_ITEMS WHERE PN_TYPE='" & dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value & "' and  (COM_ID = @COM_ID) AND (SERIAL_NO = '" & Trim(txtSerial.Text) & "') ORDER BY IR_DATE DESC) AS 'LMR' FROM         TBL_DEVICES_AND_ITEMS WHERE     (COM_ID = @COM_ID) AND (DAI_PN = @DAI_PN) AND (DAI_ACTIVE = 1)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                Hasrecord = True
                '// GET PART NAME
                dgInternal.Item(0, dgInternal.CurrentCell.RowIndex).Value = dbConnections.dReader.Item("DAI_NAME")
                '// GET PART COST
                If IsDBNull(dbConnections.dReader.Item("DAI_UNIT_PRICE")) Then
                    UnitPrice = dbConnections.dReader.Item("DAI_UNIT_PRICE")
                Else

                    UnitPrice = dbConnections.dReader.Item("DAI_UNIT_PRICE")
                End If
                dgInternal.Item(4, dgInternal.CurrentCell.RowIndex).Value = UnitPrice
                '// GET PART YIELD
                If IsDBNull(dbConnections.dReader.Item("WARRANTY_YIELE")) Then
                    YieldPerItem = 0
                Else
                    YieldPerItem = dbConnections.dReader.Item("WARRANTY_YIELE")
                End If

            End While
            dbConnections.dReader.Close()



            strSQL = "SELECT  TOP 1  ISNULL( COPIES,0) AS 'LMR' FROM         TBL_INTERNAL_ITEMS WHERE PN_TYPE='" & dgInternal.Item(3, dgInternal.CurrentCell.RowIndex).Value & "' and  (COM_ID = @COM_ID) AND (SERIAL_NO = '" & Trim(txtSerial.Text) & "') ORDER BY IR_DATE DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                If isPreviousRedingHave = True Then
                    '// Adding Previous Reading
                    If IsDBNull(dbConnections.dReader.Item("LMR")) Then
                        PreviousReading = 0
                    Else
                        PreviousReading = dbConnections.dReader.Item("LMR")
                    End If

                    dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).Value = PreviousReading
                End If
            End While
            dbConnections.dReader.Close()








            If Hasrecord = False Then
                Exit Sub
            End If
            '// check in use row
            If dgInternal.Item(0, dgInternal.CurrentCell.RowIndex).Value.ToString() <> "" Then
                '// if null then set to qty 1
                If dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value = 0 Then
                    ReqQty = 1
                Else
                    ReqQty = CInt(dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value)
                End If
            End If
            '// set value of qty
            dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value = ReqQty



            '// CALCULATING YIELD
            dgInternal.Item(8, dgInternal.CurrentCell.RowIndex).Value = YieldPerItem

            TotalYield = (YieldPerItem * ReqQty)
            dgInternal.Item(7, dgInternal.CurrentCell.RowIndex).Value = TotalYield
            '// set current copies to column
            CurrentCopies = (CurrentMRval - PreviousReading)
            dgInternal.Item(6, dgInternal.CurrentCell.RowIndex).Value = CurrentCopies

            '// check negative or positive




            If (YieldPerItem * ReqQty) <> 0 Then

                If TotalYield >= CurrentCopies Then
                    IsNegative_Internal = "N"
                    dgInternal.Rows(dgInternal.CurrentCell.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
                Else
                    If IsNegative_Internal <> "N" Then
                        IsNegative_Internal = "P"
                    End If

                End If

                If IsNegative_Internal = "N" Then
                    lblNPState.Text = "Negative"
                    lblNPState.ForeColor = Color.DarkRed
                Else
                    lblNPState.Text = "Positive"
                    lblNPState.ForeColor = Color.DarkSeaGreen
                End If
            End If



        Catch ex As Exception
            dbConnections.dReader.Close()
            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Calculate(ByRef YieldPerItem As Integer, ByRef CurrentMRval As Integer, ByRef PreviousReading As Integer)
        '// check in use row
        If dgInternal.Item(0, dgInternal.CurrentCell.RowIndex).Value.ToString() <> "" Then
            '// if null then set to qty 1
            If dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value = 0 Then
                ReqQty = 1
            Else
                ReqQty = CInt(dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value)
            End If
        End If
        '// set value of qty
        dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value = ReqQty



        '// CALCULATING YIELD
        dgInternal.Item(8, dgInternal.CurrentCell.RowIndex).Value = YieldPerItem

        TotalYield = (YieldPerItem * ReqQty)
        dgInternal.Item(7, dgInternal.CurrentCell.RowIndex).Value = TotalYield
        '// set current copies to column
        CurrentCopies = (CurrentMRval - PreviousReading)
        dgInternal.Item(6, dgInternal.CurrentCell.RowIndex).Value = CurrentCopies

        '// check negative or positive




        If (YieldPerItem * ReqQty) <> 0 Then

            If TotalYield >= CurrentCopies Then
                IsNegative_Internal = "N"
                dgInternal.Rows(dgInternal.CurrentCell.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
            Else
                If IsNegative_Internal <> "N" Then
                    IsNegative_Internal = "P"
                End If

            End If

            If IsNegative_Internal = "N" Then
                lblNPState.Text = "Negative"
                lblNPState.ForeColor = Color.DarkRed
            Else
                lblNPState.Text = "Positive"
                lblNPState.ForeColor = Color.DarkSeaGreen
            End If
        End If
    End Sub


    Private Sub Validating_PN()

    End Sub


    Private Sub TextBox1_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)

        'changes made adding a try catch function to catch the exception and throw it
        'Changes made by Gagan 

        Try
            CurrentMRval = 0
            YieldPerItem = 0
            TotalYield = 0
            PreviousReading = 0
            ReqQty = 0
            CurrentCopies = 0
            If Trim(txtCurrentMR.Text) = "" Then
                CurrentMRval = 0
            Else
                CurrentMRval = CInt(Trim(txtCurrentMR.Text))
            End If

            YieldPerItem = CInt(dgInternal.Item(8, dgInternal.CurrentCell.RowIndex).FormattedValue)
            PreviousReading = CInt(dgInternal.Item(5, dgInternal.CurrentCell.RowIndex).FormattedValue)
            ReqQty = CInt(dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).FormattedValue)
            TotalYield = YieldPerItem * ReqQty
            CurrentCopies = CurrentMRval - PreviousReading

            dgInternal.Item(2, dgInternal.CurrentCell.RowIndex).Value = ReqQty
            dgInternal.Item(7, dgInternal.CurrentCell.RowIndex).Value = TotalYield
            dgInternal.Item(6, dgInternal.CurrentCell.RowIndex).Value = CurrentCopies

            If (YieldPerItem * ReqQty) <> 0 Then
                If (TotalYield - CurrentCopies) < 0 Then
                    dgInternal.Rows(dgInternal.CurrentCell.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
                End If

            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub


#End Region












    Private Sub dgInternal_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgInternal.CellContentClick

    End Sub

    Private Sub bgWorkerStarup_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerStarup.DoWork
        Control.CheckForIllegalCrossThreadCalls = False

        Loading_Printer_List()


        globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()
        Threading.Thread.Sleep(500)
        cmbPrinterList.Text = globalVariables.DefaultPrinterName
    End Sub

    Private Sub bgWorkerDabtorsCheck_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgWorkerDabtorsCheck.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        If selectedCompanyID = "001" Then
            If IsDebtorsOutstandingHave(globalVariables.DebtorsCheckDayLimit, False) Then
                lblDebtors.Text = "YES"
            Else
                lblDebtors.Text = "NO"
                lblDebtors.ForeColor = Color.DarkGreen
            End If
        Else
            lblDebtors.Text = "NOT CONFIGURED"
        End If

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Class OverDueInvoice
        Public Property INV_NO As String
    End Class

    Private Class IRInfo
            Public Property IR_NO As String
            Public Property IR_DATE As DateTime?
        End Class
    End Class
