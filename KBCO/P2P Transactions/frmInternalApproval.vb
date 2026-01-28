Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmInternalApproval


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
    Private IsNegative_Internal As String = ""
    Private ApproveState As String = ""
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"

            Case "Save"

            Case "Edit"
                Me.FormEdit()
            Case "Delete"

            Case "Search"

            Case "Print"

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

        If ApproveState = "" Then
            Exit Function
        End If


        Dim Mesg As String = ""

        If ApproveState = "CANCEL" Then
            Mesg = "Do you wish to cancel this Internal?"
        ElseIf ApproveState = "APPROVE" Then
            Mesg = "Do you wish to approve this Internal?"
        Else
            Mesg = "Do you wish to reject this Internal?"
        End If

        Dim conf = MessageBox.Show(Mesg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()

            Try
                errorEvent = "Edit"

                strSQL = "UPDATE    TBL_INTERNAL_MAIN SET    IR_APP_BY =@IR_APP_BY, IR_APP_NAME =@IR_APP_NAME, IR_APP_DATE =GETDATE(), IR_APP_STATE =@IR_APP_STATE, IR_COMMENT =@IR_COMMENT, IR_STATE =@IR_STATE WHERE     (COM_ID = @COM_ID) AND (IR_NO =@IR_NO)"


                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))

                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))


                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_APP_BY", userSession)
                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_APP_NAME", userName)



                If ApproveState = "APPROVE" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_APP_STATE", "APPROVED")

                ElseIf ApproveState = "CANCEL" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_APP_STATE", "CANCELLED")
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_APP_STATE", "REJECT")
                End If
                If Trim(txtComment.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_COMMENT", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_COMMENT", Trim(txtComment.Text))
                End If
                If ApproveState = "CANCEL" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "INTERNAL CANCELLED")
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "INTERNAL PRINT PENDING")
                End If



                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


            Catch ex As Exception
                dbConnections.dReader.Close()
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()

            End Try
        End If
        If save Then

            Me.Dispose()
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
    Private Sub frmInternalApproval_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalApproval_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalApproval_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalApproval_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalApproval_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmInternalApproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        loadInternal()
        'FormClear()

    End Sub

    Private Sub frmInternalApproval_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
            MsgBox(ex.InnerException.Message)
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

        'REJECT
        If ApproveState = "REJECT" Then
            If generalValObj.isPresent(txtComment) = False Then
                Exit Function
            End If

        End If


        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        IsNegative_Internal = ""

        dgInternal.Rows.Clear()



        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()

        If IsDebtorsOutstandingHave(globalVariables.DebtorsCheckDayLimit, False) Then

        End If
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"



    Private Sub txtCurrentMR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentMR.KeyPress
        generalValObj.isDigit(e)
    End Sub





    Private Sub txtCurrentMR_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentMR.TextChanged
        If Trim(txtCurrentMR.Text) = "" Then
            dgInternal.Enabled = False
        Else
            dgInternal.Enabled = True

        End If
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()

    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '==================================================================================================================

#Region "Button Events"

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"


#End Region



    Private Sub txtIRNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtIRNo.Validating
        loadInternal()
    End Sub


    Private Sub loadInternal()
        If Trim(txtIRNo.Text) = "" Then
            Exit Sub
        End If
        dgInternal.Rows.Clear()
        Try
            strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_CODE, TBL_INTERNAL_MAIN.CUS_LOC, TBL_INTERNAL_MAIN.ISSUED_TO, TBL_INTERNAL_MAIN.CURRENT_MR, TBL_INTERNAL_MAIN.IR_TYPE, TBL_INTERNAL_MAIN.IR_STATE, TBL_INTERNAL_MAIN.IR_PRINTED, MTBL_TECH_MASTER.TECH_NAME,MTBL_CUSTOMER_MASTER.CUS_NAME ,TBL_INTERNAL_MAIN.COMMENT  FROM         TBL_INTERNAL_MAIN INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID INNER JOIN                       MTBL_TECH_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_TECH_MASTER.COM_ID AND TBL_INTERNAL_MAIN.ISSUED_TO = MTBL_TECH_MASTER.TECH_CODE WHERE     (TBL_INTERNAL_MAIN.COM_ID = @COM_ID) AND (TBL_INTERNAL_MAIN.IR_NO = @IR_NO)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            While dbConnections.dReader.Read
                '// GET PART NAME
                txtSerial.Text = dbConnections.dReader.Item("SERIAL_NO")
                txtPNo.Text = dbConnections.dReader.Item("PN_NO")
                txtCusCode.Text = dbConnections.dReader.Item("CUS_CODE")
                txtCusName.Text = dbConnections.dReader.Item("CUS_NAME")
                txtCusAdd.Text = dbConnections.dReader.Item("CUS_LOC")
                txtCurrentMR.Text = dbConnections.dReader.Item("CURRENT_MR")
                cmbIRType.Text = dbConnections.dReader.Item("IR_TYPE")
                lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                txtTechCode.Text = dbConnections.dReader.Item("ISSUED_TO")

                If IsDBNull(dbConnections.dReader.Item("COMMENT")) Then
                    TextBox1.Text = ""
                Else
                    TextBox1.Text = dbConnections.dReader.Item("COMMENT")
                End If


            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT      SPECIAL_CASE_DESC FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID =@COM_ID)  AND (SERIAL =@SERIAL)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)

            dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", txtSerial.Text)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

             
                If IsDBNull(dbConnections.dReader.Item("SPECIAL_CASE_DESC")) Then
                    txtSpecialCase.Text = ""
                Else
                    txtSpecialCase.Text = dbConnections.dReader.Item("SPECIAL_CASE_DESC")
                End If

            End While
            dbConnections.dReader.Close()



            strSQL = "SELECT     IR_DATE, SERIAL_NO, PN, PN_DESC, PN_QTY, PN_TYPE, PN_VALUE, MR_TO_DATE, PREVIOUS_READING, CURRENT_READING, COPIES, STD_YIELD FROM         TBL_INTERNAL_ITEMS WHERE     (COM_ID =@COM_ID) AND (IR_NO = @IR_NO)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader.Item("PN_DESC"), dbConnections.dReader.Item("PN"), dbConnections.dReader.Item("PN_QTY"), dbConnections.dReader.Item("PN_TYPE"), dbConnections.dReader.Item("PN_VALUE"), dbConnections.dReader.Item("PREVIOUS_READING"), dbConnections.dReader.Item("COPIES"), (dbConnections.dReader.Item("STD_YIELD") * dbConnections.dReader.Item("PN_QTY")), dbConnections.dReader.Item("STD_YIELD"))

            End While
            dbConnections.dReader.Close()






        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub
    Private Sub populatreDatagrid(ByRef Desc As String, ByRef PN As String, ByRef Qty As String, ByRef Type As String, ByRef Value As String, ByRef PReading As String, ByRef Copies As String, ByRef TYield As String, ByRef Yield As String)
        dgInternal.ColumnCount = 9
        dgInternal.Rows.Add(Desc, PN, Qty, Type, Value, PReading, Copies, TYield, Yield)
    End Sub

    Private Sub btnPrintViewInternal_Click(sender As Object, e As EventArgs) Handles btnPrintViewInternal.Click
        frmPrintInternal.Text = Trim(txtIRNo.Text)
        frmPrintInternal.Show()
    End Sub

    Private Sub btnApprove1_Click(sender As Object, e As EventArgs) Handles btnApprove1.Click
        ApproveState = "APPROVE"
        Me.save()
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        ApproveState = "REJECT"
        Me.save()
    End Sub

    Private Sub btnCancelSO_Click(sender As Object, e As EventArgs) Handles btnCancelSO.Click
        ApproveState = "CANCEL"
        Me.save()
    End Sub

    Private Sub txtIRNo_TextChanged(sender As Object, e As EventArgs) Handles txtIRNo.TextChanged

    End Sub

    Private Sub dgInternal_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgInternal.CellFormatting

    End Sub

    Private Sub dgInternal_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgInternal.CellContentClick

    End Sub

    Private Sub btnOpenVCM_Click(sender As Object, e As EventArgs) Handles btnOpenVCM.Click
        frmRecordAdjustMaster.MdiParent = frmMDImain
        frmRecordAdjustMaster.Show()
    End Sub
End Class