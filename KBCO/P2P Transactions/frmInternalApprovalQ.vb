Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmInternalApprovalQ


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
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"

            Case "Save"

            Case "Edit"

            Case "Delete"

            Case "Search"

            Case "Print"

        End Select
    End Sub

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmInternalApprovalQ_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalApprovalQ_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalApprovalQ_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalApprovalQ_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalApprovalQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmInternalApprovalQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmInternalApprovalQ_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
        FormClear()
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


    Private Sub populatreDatagrid(ByRef IRNo As String, ByRef IRDate As String, ByRef PNo As String, ByRef SN As String, ByRef CusName As String, ByRef MLoc As String, ByRef IRStatus As String)
        dgApprovedInternalQ.ColumnCount = 7
        dgApprovedInternalQ.Rows.Add(IRNo, IRDate, PNo, SN, CusName, MLoc, IRStatus)
    End Sub

    Private Sub AddtoGrid()
        Dim rowCount As Integer = 0
        errorEvent = "Add to grid()"
        dgApprovedInternalQ.Rows.Clear()
        Try
            connectionStaet()

            strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_LOC,   MTBL_CUSTOMER_MASTER.CUS_NAME,TBL_INTERNAL_MAIN.IR_STATE FROM  TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.COM_ID ='" & Trim(globalVariables.selectedCompanyID) & "') AND ((TBL_INTERNAL_MAIN.IR_STATE = 'PENDING APPROVAL') OR (TBL_INTERNAL_MAIN.IR_STATE = 'PENDING GM APPROVAL'))"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader.Item("IR_NO"), CDate(dbConnections.dReader.Item("IR_DATE")).ToShortDateString, dbConnections.dReader.Item("PN_NO"), dbConnections.dReader.Item("SERIAL_NO"), dbConnections.dReader.Item("CUS_NAME"), dbConnections.dReader.Item("CUS_LOC"), dbConnections.dReader.Item("IR_STATE"))

                rowCount = rowCount + 1
            End While

        Catch ex As Exception
            dbConnections.dReader.Close()
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub


#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False



        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()

        dgApprovedInternalQ.Rows.Clear()
        AddtoGrid()
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '===================================================================================================================
#Region "Button Events"

#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' API Events  ...............................................................
    '===================================================================================================================
#Region "API Events"

#End Region

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        frmInternalApproval.MdiParent = frmMDImain
        frmInternalApproval.Show()
    End Sub

    Private Sub btnOpenManually_Click(sender As Object, e As EventArgs) Handles btnOpenManually.Click
        frmInternalApproval.MdiParent = frmMDImain
        frmInternalApproval.Show()
    End Sub


    Private Sub dgApprovedInternalQ_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgApprovedInternalQ.CellMouseDoubleClick

        frmInternalApproval.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalApproval.MdiParent = frmMDImain
        frmInternalApproval.Show()

    End Sub

    Private Sub dgApprovedInternalQ_ColumnHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgApprovedInternalQ.ColumnHeaderMouseDoubleClick



        frmInternalApproval.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalApproval.MdiParent = frmMDImain
        frmInternalApproval.Show()

    End Sub


End Class