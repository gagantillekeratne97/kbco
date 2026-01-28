Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class frmManageMenuItems
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
                ' showCrystalReport()
        End Select
    End Sub
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"
    Private Sub createNew()
        'If Not canCreate Then
        '    MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save() As Boolean
        save = False
        'If Not canCreate Then
        '    MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Function
        'End If

        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()
            Try
                If isEditClicked Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE    " & selectedDatabaseName & ".dbo.MENUITEM SET  MENUITEM_MENUNAME =@MENUITEM_MENUNAME, MENUITEM_MENURIGHT =@MENUITEM_MENURIGHT, MENUITEM_MENUFORMNAME =@MENUITEM_MENUFORMNAME, MENUITEM_MENUDATE =GETDATE(), MENUITEM_MENUPARENT =@MENUITEM_MENUPARENT, MENUITEM_MENULEVEL =@MENUITEM_MENULEVEL, COM_ID = @COM_ID WHERE     (MENUITEM_MENUTAG =@MENUITEM_MENUTAG)"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.MENUITEM (MENUITEM_MENUTAG, MENUITEM_MENUNAME, MENUITEM_MENURIGHT, MENUITEM_MENUFORMNAME, MENUITEM_MENUDATE, MENUITEM_MENUPARENT, MENUITEM_MENULEVEL,COM_ID) VALUES     (@MENUITEM_MENUTAG, @MENUITEM_MENUNAME, @MENUITEM_MENURIGHT, @MENUITEM_MENUFORMNAME, GETDATE(), @MENUITEM_MENUPARENT, @MENUITEM_MENULEVEL,@COM_ID)"
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUTAG", Trim(txtMenuTag.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUNAME", Trim(txtMenuName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENURIGHT", Trim(txtMenuRights.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUFORMNAME", Trim(txtFormName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUPARENT", Trim(txtMenuParentCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENULEVEL", Trim(txtMenuLevel.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtMenuTag.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else
                        MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                End Select
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
            End Try
        End If
        Return save
    End Function

    Private Function delete() As Boolean
        delete = False
        'If Not canDelete Then
        '    MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Function
        'End If
        connectionStaet()
        Try
            errorEvent = "Delete"
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtMenuName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.MENUITEM WHERE     (MENUITEM_MENUTAG = @MENUITEM_MENUTAG) AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUTAG", Trim(txtMenuTag.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
        Return delete
    End Function

    Private Sub FormEdit()
        'If Not canModify Then
        '    MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If
        Dim conf = MessageBox.Show("" & EditMessage & "" & txtMenuName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            Isenable(False)
            isEditClicked = True
            globalFunctions.globalButtonActivation(True, True, False, False, False, False)
            Me.saveBtnStatus()
        End If
    End Sub
#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmManageMenuItems_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmManageMenuItems_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmManageMenuItems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmManageMenuItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmManageMenuItems_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmManageMenuItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
    End Sub

    Private Sub frmManageMenuItems_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = "read User Permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'  and COM_ID= '" & globalVariables.selectedCompanyID & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)

            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
            'dbConnections.sqlConnection.Close()
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
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

    Private Sub Isenable(ByRef enablestate As Boolean)
        txtMenuName.ReadOnly = enablestate
        txtMenuRights.ReadOnly = enablestate
        txtFormName.ReadOnly = enablestate
        txtMenuParentCode.ReadOnly = enablestate
        txtMenuLevel.ReadOnly = enablestate
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        'If generalValObj.isPresent(txtLocDesc) = False Then
        '    Exit Function
        'End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtMenuTag.Text = ""
        txtMenuName.Text = ""
        txtMenuRights.Text = ""
        txtMenuParentCode.Text = ""
        txtMenuLevel.Text = ""
        txtFormName.Text = ""
        txtMenuTag.Enabled = True
        txtMenuTag.Focus()
        isEditClicked = False
        '//Set enability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"


    Private Sub txtMenuTag_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMenuTag.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If

        connectionStaet()
        Try
            strSQL = "SELECT      MENUITEM_MENUNAME, MENUITEM_MENURIGHT, MENUITEM_MENUFORMNAME, MENUITEM_MENUPARENT, MENUITEM_MENULEVEL FROM         " & selectedDatabaseName & ".dbo.MENUITEM WHERE     (MENUITEM_MENUTAG = @MENUITEM_MENUTAG) AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUTAG", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtMenuName.Text = dbConnections.dReader.Item("MENUITEM_MENUNAME")
                txtMenuRights.Text = dbConnections.dReader.Item("MENUITEM_MENURIGHT")
                txtFormName.Text = dbConnections.dReader.Item("MENUITEM_MENUFORMNAME")
                txtMenuParentCode.Text = dbConnections.dReader.Item("MENUITEM_MENUPARENT")
                txtMenuLevel.Text = dbConnections.dReader.Item("MENUITEM_MENULEVEL")

            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtMenuTag.Enabled = False
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

#End Region




    Private Sub txtMenuTag_TextChanged(sender As Object, e As EventArgs) Handles txtMenuTag.TextChanged

    End Sub
End Class