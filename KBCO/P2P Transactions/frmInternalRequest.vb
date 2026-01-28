Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

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
        save = False
        Dim srilankaTimeZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time")
        Dim utcNow As DateTime = DateTime.UtcNow
        Dim sriLankaTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, srilankaTimeZone)
        Dim DebtorsOutHave As Boolean = False
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            Try

                'If IsDebtorsOutstandingHave(globalVariables.DebtorsCheckDayLimit, False) Then
                '    DebtorsOutHave = True
                'End If
                If lblDebtors.Text = "YES" Then
                    DebtorsOutHave = True
                Else
                    DebtorsOutHave = False
                End If

                SavedIR_NO = GenarateIRNo()

                connectionStaet()
                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
                errorEvent = "Save"
                strSQL = "INSERT INTO TBL_INTERNAL_MAIN  ( COM_ID, IR_NO, IR_DATE, SERIAL_NO, PN_NO, CUS_CODE, CUS_LOC, ISSUED_TO, CURRENT_MR, IR_TYPE, IR_STATE, CR_BY, CR_DATE,IR_PRINTED,COMMENT) VALUES     (@COM_ID, @IR_NO, @IR_DATE, @SERIAL_NO, @PN_NO, @CUS_CODE, @CUS_LOC, @ISSUED_TO, @CURRENT_MR, @IR_TYPE, @IR_STATE, '" & userSession & "', GETDATE(),@IR_PRINTED,@COMMENT)"

                'Changes made to IR_DATE on 2025-07-02
                'Changes made IR_DATE TO sriLankaTime function 
                'Changes made by Gagan Tillekeratne.

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_DATE", sriLankaTime.ToString("yyyy-MM-dd HH:mm:ss"))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", Trim(txtSerial.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@PN_NO", Trim(txtPNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCusCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_LOC", Trim(txtCusAdd.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@ISSUED_TO", Trim(txtTechCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR", Trim(txtCurrentMR.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_TYPE", Trim(cmbIRType.Text))

                If DebtorsOutHave = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING APPROVAL")
                Else
                    If IsNegative_Internal = "P" Then
                        If globalVariables.selectedCompanyID = "003" Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING GM APPROVAL")
                        Else
                            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING APPROVAL")
                        End If
                    ElseIf IsNegative_Internal = "N" Then
                        If globalVariables.selectedCompanyID = "003" Then
                            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING GM APPROVAL")
                        Else
                            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING APPROVAL")
                        End If
                    Else
                        Exit Function
                    End If
                End If
                'COMMENT
                If Trim(txtComment.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@COMMENT", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@COMMENT", Trim(txtComment.Text))
                End If
                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", SavedIR_NO)
                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_PRINTED", False)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                For Each row As DataGridViewRow In dgInternal.Rows

                    If Trim(dgInternal.Rows(row.Index).Cells("PN_DESC").Value) <> "" Then
                        dbConnections.sqlCommand.Parameters.Clear()

                        strSQL = "INSERT INTO TBL_INTERNAL_ITEMS  (COM_ID, IR_NO, IR_DATE, SERIAL_NO, PN, PN_DESC, PN_QTY, PN_TYPE, PN_VALUE, MR_TO_DATE, PREVIOUS_READING, CURRENT_READING, COPIES, STD_YIELD) VALUES     (@COM_ID, @IR_NO, @IR_DATE, @SERIAL_NO, @PN, @PN_DESC, @PN_QTY, @PN_TYPE, @PN_VALUE, @MR_TO_DATE, @PREVIOUS_READING, @CURRENT_READING, @COPIES, @STD_YIELD)"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)

                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", SavedIR_NO)

                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL_NO", Trim(txtSerial.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@IR_DATE", Convert.ToDateTime(sriLankaTime.ToString("yyyy-MM-dd")))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PN", Trim(dgInternal.Rows(row.Index).Cells("IR_PN").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PN_DESC", Trim(dgInternal.Rows(row.Index).Cells("PN_DESC").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PN_QTY", Trim(dgInternal.Rows(row.Index).Cells("IR_QTY").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PN_TYPE", Trim(dgInternal.Rows(row.Index).Cells("TYPE").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PN_VALUE", CDbl(dgInternal.Rows(row.Index).Cells("IR_VAL").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@MR_TO_DATE", CInt(txtCurrentMR.Text))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@PREVIOUS_READING", dgInternal.Rows(row.Index).Cells("IR_P_READING").Value)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_READING", (Trim(txtCurrentMR.Text)))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COPIES", CInt(dgInternal.Rows(row.Index).Cells("IR_COPIES").Value))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@STD_YIELD", CInt(dgInternal.Rows(row.Index).Cells("IR_YIELD").Value))


                        If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                    End If
                Next
                dbConnections.sqlTransaction.Commit()


                txtIRNo.Text = SavedIR_NO
                txtViewInternalNo.Text = SavedIR_NO
                '' If IsNegative_Internal = "P" Then
                'frmPrintInternal.Text = Trim(txtIRNo.Text)
                'frmPrintInternal.Show()
                '' End If


                If save = True Then

                    AuditDelete(Me.Text, userSession, userName, txtIRNo.Text, txtCusName.Text + "(Saved)")

                End If
            Catch ex As SqlException
                dbConnections.sqlTransaction.Rollback()
                inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

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
            Dim isRecordHave As Boolean = False
            strSQL = "SELECT   TOP 1  IR_NO, IR_DATE FROM TBL_INTERNAL_MAIN WHERE     (COM_ID = '" & Trim(globalVariables.selectedCompanyID) & "') ORDER BY IR_DATE DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                isRecordHave = True
                If IsDBNull(dbConnections.dReader.Item("IR_NO")) Then
                    lblIRNo.Text = ""
                Else
                    lblIRNo.Text = dbConnections.dReader.Item("IR_NO")
                End If

                If IsDBNull(dbConnections.dReader.Item("IR_DATE")) Then
                    lblLInvDate.Text = ""
                Else
                    lblLInvDate.Text = dbConnections.dReader.Item("IR_DATE")
                End If


            End While

            If isRecordHave = False Then
                lblIRNo.Text = ""

                lblLInvDate.Text = ""
            End If
            dbConnections.dReader.Close()
        Catch ex As Exception
            dbConnections.dReader.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function IsPrint_Enable() As Boolean

        'Changes made to the SQL Query on 2025-07-01
        'Change made adding IR_STATE = 'UPLOADED TO BELEETA'
        'Change made by Gagan Tillekeratne.

        IsPrint_Enable = False
        Try
            strSQL = "SELECT CASE
                WHEN EXISTS (
                    SELECT IR_NO 
                    FROM TBL_INTERNAL_MAIN 
                    WHERE 
                        (IR_STATE = 'UPLOADED TO BELEETA' OR 
                         IR_STATE = 'PENDING DISPATCH' OR 
                         IR_STATE = 'INTERNAL CANCELLED' OR 
			             IR_STATE = 'INTERNAL PRINT PENDING' OR
			             IR_STATE = 'APPROVED') 
                        AND IR_NO = @IR_NO
                        AND COM_ID = @COM_ID
                ) 
                THEN CAST(1 AS BIT) 
                ELSE CAST(0 AS BIT) 
            END
            "
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtViewInternalNo.Text))
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsPrint_Enable = True

            Else
                IsPrint_Enable = False
                MessageBox.Show("This Internal is in pending approval stage.", "Pending Approval.", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return IsPrint_Enable
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
            connectionStaet()

            errorEvent = "Save"
            strSQL = "UPDATE    TBL_INTERNAL_MAIN  SET              IR_STATE =@IR_STATE, IR_PRINTED =@IR_PRINTED WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (IR_NO = @IR_NO)"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING DISPATCH")
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_PRINTED", True)
            If dbConnections.sqlCommand.ExecuteNonQuery() Then UpdateIRPrint = True Else UpdateIRPrint = False

            If UpdateIRPrint = True Then
                MessageBox.Show("Internal Print Successful.", "Printed.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return UpdateIRPrint
    End Function

    Private Sub Load_IR_Info_View()
        If Trim(txtViewInternalNo.Text) = "" Then
            Exit Sub
        End If
        Try
            strSQL = "SELECT     TBL_INTERNAL_MAIN.COM_ID, TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.CUS_CODE, MTBL_CUSTOMER_MASTER.CUS_NAME FROM         TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.IR_NO = @IR_NO) AND (TBL_INTERNAL_MAIN.COM_ID = @COM_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtViewInternalNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                '// GET PART NAME
                txtVICusCode.Text = dbConnections.dReader.Item("CUS_CODE")
                txtVICusName.Text = dbConnections.dReader.Item("CUS_NAME")
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            dbConnections.dReader.Close()
            MsgBox(ex.Message)
        Finally

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


    '// search by  = bySN,ByPN/ByAG
    Private Function Search(ByRef SearchBy As String) As Boolean
        Search = False

        If Trim(txtSearch.Text) = "" Then
            Exit Function
        End If
        Dim SelectedSN As String = ""
        Dim SelectedPNo As String = ""
        Dim SelectedAg As String = ""
        Dim SelCusCode As String = ""
        Dim SelectedPartNo As String = ""

        Try


            If SearchBy = "SN" Then
                strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (SERIAL = '" & Trim(txtSearch.Text) & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    Search = True
                    SelectedSN = dbConnections.dReader.Item("SERIAL")
                    If IsDBNull(dbConnections.dReader.Item("P_NO")) Then
                        SelectedPNo = ""
                    Else
                        SelectedPNo = dbConnections.dReader.Item("P_NO")
                    End If

                    SelectedAg = dbConnections.dReader.Item("AG_ID")
                    SelCusCode = dbConnections.dReader.Item("CUS_ID")

                End While
                dbConnections.dReader.Close()
            Else
                strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (P_NO = '" & Trim(txtSearch.Text) & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    SelectedSN = dbConnections.dReader.Item("SERIAL")
                    If IsDBNull(dbConnections.dReader.Item("P_NO")) Then
                        SelectedPNo = ""
                    Else
                        SelectedPNo = dbConnections.dReader.Item("P_NO")
                    End If
                    SelectedAg = dbConnections.dReader.Item("AG_ID")
                    SelCusCode = dbConnections.dReader.Item("CUS_ID")

                End While
                dbConnections.dReader.Close()
            End If



            If SelectedSN = "" Then
                Exit Function
            End If


            txtCusCode.Text = SelCusCode


            dbConnections.sqlCommand.Parameters.Clear()

            strSQL = "SELECT     CUS_NAME FROM      MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID) AND (CUS_ID = @CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", SelCusCode)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                txtCusName.Text = dbConnections.dReader.Item("CUS_NAME")

            End While
            dbConnections.dReader.Close()


            dbConnections.sqlCommand.Parameters.Clear()


            '// loading machine Info

            strSQL = "SELECT      MACHINE_PN,SERIAL, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE,M_DEPT FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID =@COM_ID) AND (AG_ID =@AG_ID) AND (SERIAL =@SERIAL)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", SelectedAg)
            dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", SelectedSN)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                txtSerial.Text = dbConnections.dReader.Item("SERIAL")
                SelectedPartNo = dbConnections.dReader.Item("MACHINE_PN")

                If IsDBNull(dbConnections.dReader.Item("P_NO")) Then
                    txtPNo.Text = ""
                Else
                    txtPNo.Text = dbConnections.dReader.Item("P_NO")
                End If
                If globalVariables.selectedCompanyID = "003" Then
                    Dim Location As String = dbConnections.dReader.Item("M_LOC1")

                    If Not IsDBNull(dbConnections.dReader.Item("M_LOC2")) Then
                        Location = Location + " " + dbConnections.dReader.Item("M_LOC2")
                    End If

                    If Not IsDBNull(dbConnections.dReader.Item("M_LOC3")) Then
                        Location = Location + " " + dbConnections.dReader.Item("M_LOC3")
                    End If
                    txtCusAdd.Text = Location
                Else
                    txtCusAdd.Text = dbConnections.dReader.Item("M_DEPT")
                End If


                If IsDBNull(dbConnections.dReader.Item("TECH_CODE")) Then
                    txtTechCode.Text = ""
                Else
                    txtTechCode.Text = dbConnections.dReader.Item("TECH_CODE")
                End If

                If IsDBNull(dbConnections.dReader.Item("SPECIAL_CASE_DESC")) Then
                    txtSpecialCase.Text = ""
                Else
                    txtSpecialCase.Text = dbConnections.dReader.Item("SPECIAL_CASE_DESC")
                End If

            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT   top 1  MACHINE_MODEL FROM         MTBL_MACHINE_MASTER WHERE     (COM_ID = @COM_ID) AND (MACHINE_ID =@MACHINE_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(SelectedPartNo))
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                lblModel.Text = ""
            Else
                lblModel.Text = dbConnections.sqlCommand.ExecuteScalar
            End If




            '// load Tech Name
            If Trim(txtTechCode.Text) <> "" Then
                strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


                While dbConnections.dReader.Read
                    If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
                        lblTechName.Text = "ERROR"
                    Else
                        lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                    End If


                End While
                dbConnections.dReader.Close()
            End If




            strSQL = "SELECT      CUS_ID, CUS_NAME, REASON_FOR_BLOCK FROM         TBL_BLOCK_CUSTOMER WHERE     (COM_ID = @COM_ID) AND (CUS_ID = @CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCusCode.Text))
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(selectedCompanyID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim IsBlockedUser As Boolean = False
            While dbConnections.dReader.Read
                IsBlockedUser = True
            End While
            dbConnections.dReader.Close()

            If IsBlockedUser = True Then
                MessageBox.Show("This Is A Blocked Customer. Please contact your immediate manager.")
                FormClear()
            End If

        Catch ex As Exception
            dbConnections.dReader.Close()
            MsgBox(ex.Message)
        End Try



        Return Search
    End Function

    Private Function GenarateIRNo() As String

        GenarateIRNo = ""

        errorEvent = "Reading information"
        connectionStaet()


        Try
            strSQL = "SELECT top 1  IR_NO FROM         TBL_INTERNAL_MAIN WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') order by IR_DATE desc"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GenarateIRNo = globalVariables.selectedCompanyID & "/" & "IR" & "/" & 1
            Else
                Dim IRCodeSplit() As String
                Dim NoRecordFound As Boolean = False
                Dim IRID As Integer = 0
                IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                IRID = IRCodeSplit(2)
                Do Until NoRecordFound = True
                    strSQL = "SELECT CASE WHEN EXISTS (SELECT     IR_NO  FROM         TBL_INTERNAL_MAIN WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (IR_NO = '" & globalVariables.selectedCompanyID & "/IR/" & IRID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteScalar = False Then
                        NoRecordFound = True
                    Else
                        IRID = IRID + 1
                    End If
                Loop

                If NoRecordFound = True Then
                    GenarateIRNo = IRCodeSplit(0) & "/IR/" & IRID
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


    Private Function IsDebtorsOutstandingHave(ByRef DaysLimit As Integer, ByRef IsShowMsg As Boolean) As Boolean
        IsDebtorsOutstandingHave = False
        Dim TransactionDays As Integer = 0
        Dim ReadingInvNo As String = ""
        Try
            strSQL = "SELECT     TBL_INVOICE_MASTER.INV_NO, TBL_RECIPTS.RECIPT_ID, TBL_INVOICE_MASTER.INV_DATE, DATEDIFF(DAY, GETDATE(), TBL_INVOICE_MASTER.INV_DATE) AS Expr1, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER LEFT OUTER JOIN  TBL_RECIPTS ON TBL_INVOICE_MASTER.COM_ID = TBL_RECIPTS.COM_ID AND TBL_INVOICE_MASTER.INV_NO = TBL_RECIPTS.INV_NO WHERE     (TBL_RECIPTS.RECIPT_ID IS NULL) and TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "' and TBL_INVOICE_MASTER.CUS_ID = '" & Trim(txtCusCode.Text) & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("Expr1")) Then
                    TransactionDays = 0
                Else
                    TransactionDays = dbConnections.dReader.Item("Expr1")
                End If

                ReadingInvNo = dbConnections.dReader.Item("INV_NO")

                If TransactionDays >= DaysLimit Then
                    IsDebtorsOutstandingHave = True
                    Exit While
                End If

            End While
            dbConnections.dReader.Close()

            If IsShowMsg = True Then
                If IsDebtorsOutstandingHave = True Then
                    MessageBox.Show("Invoice No " & ReadingInvNo & " is not settled in " & DaysLimit & ". Please settle this before processing this internal.", "Pending Payment detected.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return IsDebtorsOutstandingHave
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
        txtIRNo.Text = GenarateIRNo()
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
End Class