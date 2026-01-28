Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Windows.Media
Imports System.Web

Public Class frmInternalDospatchQ

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
    Private Function save() As Boolean
        save = False
        If Trim(txtDispatchID.Text) = "" Then
            Exit Function
        End If

        Dim conf = MessageBox.Show("DO you wish to save this transaction?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()

            Try
                errorEvent = "Edit"

                strSQL = "UPDATE    TBL_INTERNAL_MAIN SET IR_STATE=@IR_STATE,   DISPATCH_ID=@DISPATCH_ID, DISPATCH_COMMENT=@DISPATCH_COMMENT ,DISPATCH_BY='" & userSession & "',DISPATCH_DATE=GETDATE()  WHERE     (COM_ID = @COM_ID) AND (IR_NO =@IR_NO)"


                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))

                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))


                dbConnections.sqlCommand.Parameters.AddWithValue("@DISPATCH_ID", Trim(txtDispatchID.Text))
                If Trim(txtComment.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@DISPATCH_COMMENT", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@DISPATCH_COMMENT", Trim(txtComment.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "ISSUED")

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

            MessageBox.Show("Save Successfuly.", "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return save
    End Function
#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmInternalDospatchQ_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalDospatchQ_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalDospatchQ_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalDospatchQ_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalDospatchQ_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmInternalDospatchQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmInternalDospatchQ_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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


    Private Sub populatreDatagrid(ByRef IRNo As String, ByRef BeleetaNo As String, ByRef IRDate As String, ByRef PNo As String, ByRef SN As String, ByRef CusName As String, ByRef MLoc As String, ByRef IrStatus As String)
        dgApprovedInternalQ.ColumnCount = 7
        dgApprovedInternalQ.Rows.Add(IRNo, BeleetaNo, IRDate, PNo, SN, CusName, MLoc, IrStatus)
    End Sub

    Private Sub AddtoGrid()
        Dim rowCount As Integer = 0
        errorEvent = "Add to grid()"
        dgApprovedInternalQ.Rows.Clear()
        Try
            connectionStaet()

            'changes made as at 04-07-2025
            'changes made Changing to UPLOADED TO BELEETA 
            'changes made by Gagan Tillekeratene.

            strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO,TBL_INTERNAL_MAIN.BELEETA_REFERENCE_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_LOC,   MTBL_CUSTOMER_MASTER.CUS_NAME,TBL_INTERNAL_MAIN.IR_STATE  FROM  TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.COM_ID ='" & Trim(globalVariables.selectedCompanyID) & "') AND (TBL_INTERNAL_MAIN.IR_STATE IN ('UPLOADED TO BELEETA'))"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim connectionString As String = dbConnections.cloudConnectionString
            Dim sqlConnection As New SqlConnection(connectionString)
            sqlConnection.Open()
            While dbConnections.dReader.Read
                Dim something As String = ""
                If globalVariables.selectedCompanyID = "003" Then
                    something = $"SELECT CASE WHEN EXISTS(SELECT 1 FROM TBL_INTERNAL_ITEMS WHERE IR_NO = '{dbConnections.dReader.Item("IR_NO")}' AND PN_DESC LIKE '%AOD%') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsExists"
                ElseIf globalVariables.selectedCompanyID = "001" Then
                    something = $"SELECT CASE WHEN EXISTS(SELECT 1 FROM TBL_INTERNAL_ITEMS WHERE IR_NO = '{dbConnections.dReader.Item("IR_NO")}' AND PN LIKE '%AOD%') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsExists"
                End If
                Dim sqlCommand As New SqlCommand(something, sqlConnection)
                Dim IsExists As Boolean = CBool(sqlCommand.ExecuteScalar)
                If IsExists = False Then
                    populatreDatagrid(dbConnections.dReader.Item("IR_NO"), dbConnections.dReader.Item("BELEETA_REFERENCE_NO"), CDate(dbConnections.dReader.Item("IR_DATE")).ToShortDateString, dbConnections.dReader.Item("PN_NO"), dbConnections.dReader.Item("SERIAL_NO"), dbConnections.dReader.Item("CUS_NAME"), dbConnections.dReader.Item("CUS_LOC"), dbConnections.dReader.Item("IR_STATE"))
                    rowCount = rowCount + 1
                ElseIf IsExists = True Then
                    Console.WriteLine(IsExists)
                End If
            End While
            sqlConnection.Close()
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
    Private Sub btnCreate_Click(sender As Object, e As EventArgs)
        frmInternalDispatch.MdiParent = frmMDImain
        frmInternalDispatch.Show()
    End Sub

    Private Sub btnOpenManually_Click(sender As Object, e As EventArgs) Handles btnOpenManually.Click
        frmInternalDispatch.MdiParent = frmMDImain
        frmInternalDispatch.Show()
    End Sub

    Private Sub dgHODApproval_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgApprovedInternalQ.CellDoubleClick
     
        frmInternalDispatch.MdiParent = frmMDImain
        frmInternalDispatch.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalDispatch.Show()

    End Sub

    Private Sub dgHODApproval_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgApprovedInternalQ.RowHeaderMouseDoubleClick


        frmInternalDispatch.MdiParent = frmMDImain
        frmInternalDispatch.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalDispatch.Show()

    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' API Events  ...............................................................
    '===================================================================================================================
#Region "API Events"

#End Region


    Private Sub dgApprovedInternalQ_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgApprovedInternalQ.CellClick
        Dim i = dgApprovedInternalQ.CurrentRow.Index
        txtIRNo.Text = dgApprovedInternalQ.Item(0, i).Value
    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        save()
        AddtoGrid()
    End Sub
End Class