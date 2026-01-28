Imports System.Data.SqlClient

Public Class frmUserPermission
    Private errorevent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights of current loged user
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    '// User rights for the selected user
    Private canaccess1 As Boolean
    Private canCreate1 As Boolean
    Private canDelete1 As Boolean
    Private canModify1 As Boolean

    Dim generalValObj As New generalValidation
    Const WMCLOSE As String = "WmClose"
    Private img As String
    Private IsAllUser As Boolean = False

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
                If deleteRecord() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'showCrystalReport()
        End Select
    End Sub

    Dim CanAccessRecord As Boolean
    Dim CanCreateRecord As Boolean
    Dim CanDeleteRecord As Boolean
    Dim CanEditRecord As Boolean
    Dim MenutagName As String
    Dim Menuright As String

    Dim count As String
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

    Private Function save() As Boolean
        save = False
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbNo Then
            Exit Function
        End If
        If isDataValid() = False Then
            Exit Function
        End If
        Try
            connectionStaet()
            Me.Cursor = Cursors.WaitCursor
            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
            dbConnections.sqlCommand.Transaction = dbConnections.sqlTransaction
            '//Delete all permissions before saving the new changes
            If Me.Text = "USER PERMISSION" Then

                strSQL = "DELETE FROM TBLU_USERDET WHERE USERDET_USERCODE = '" & Trim(txtUserID.Text) & "' AND COM_ID = '" & Trim(txtComID.Text) & "'"
            Else

                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERGROUPDET WHERE UG_ID ='" & Trim(txtUserID.Text) & "' AND COM_ID = '" & Trim(txtComID.Text) & "'"
            End If
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.sqlCommand.ExecuteNonQuery()

            For Each row As DataGridViewRow In dgUserPermission.Rows
                Dim permissionsSelected As String = Me.generatePermissionChars(row.Index)
                If Not permissionsSelected = "" Then
                    If Me.Text = "USER PERMISSION" Then
                        errorevent = "UPSave"
                        strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLU_USERDET (USERDET_USERCODE, USERDET_MENUTAG, USERDET_MENURIGHT, DTS_DATE,USERDET_USERGROUP, COM_ID)VALUES     ('" & Trim(txtUserID.Text) & "', '" & dgUserPermission.Rows(row.Index).Cells(0).Value & "', '" & permissionsSelected & "', getdate(),'" & Trim(cmbUserGroup.SelectedItem.ToString) & "', '" & Trim(txtComID.Text) & "')"
                    Else
                        errorevent = "UGSave"
                        strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLU_USERGROUPDET (UG_ID, UG_MENUTAG,UG_MENUNAME, UG_MENURIGHT, CR_DATE, COM_ID) VALUES     ('" & Trim(txtUserID.Text) & "', '" & dgUserPermission.Rows(row.Index).Cells(0).Value & "','" & dgUserPermission.Rows(row.Index).Cells(1).Value & "',  '" & permissionsSelected & "', getdate(), '" & Trim(txtComID.Text) & "')"
                    End If
                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                End If
            Next
            dbConnections.sqlTransaction.Commit()
            save = True

        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Function

    Private Sub FormEdit()
        If Not canModify Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show("" & EditMessage & "" & txtUserName.Text & " permission?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            dgUserPermission.Enabled = True
            dgUserPermission.Focus()
            isEditClicked = True
            cbAccess.Enabled = True
            cbCreate.Enabled = True
            cbDelete.Enabled = True
            cbEdit.Enabled = True
            cmbUserGroup.Enabled = True
            globalFunctions.globalButtonActivation(True, True, False, False, False, True)
            Me.saveBtnStatus()
        End If
    End Sub
    Private Sub createNew()
        Dim conf = MessageBox.Show("" & CreateNewMessgae & "", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub
    Private Function deleteRecord() As Boolean
        errorevent = "Delete"
        deleteRecord = False
        If Not canDelete Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If
        connectionStaet()
        Dim IDbackup As String = Trim(txtUserID.Text)
        Dim IDdesc As String = Trim(txtUserName.Text)
        Try
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtUserName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                If Me.Text = "USER PERMISSION" Then
                    strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERDET WHERE USERDET_USERCODE =@USERDET_USERCODE AND COM_ID = '" & Trim(txtComID.Text) & "'"
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERDET_USERCODE", Trim(txtUserID.Text))
                Else
                    strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERGROUPDET WHERE UG_ID =@UG_ID AND COM_ID = '" & Trim(txtComID.Text) & "'"
                    dbConnections.sqlCommand.Parameters.AddWithValue("@UG_ID", Trim(txtUserID.Text))
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then deleteRecord = True Else deleteRecord = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
        Return deleteRecord
    End Function

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmUserPermission_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        Try
            errorevent = "User permission read"
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)

            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub

    Private Sub frmUserPermission_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmUserPermission_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmUserPermission_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmUserPermission_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmUserPermission_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmUserPermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        If Not Me.Text = "USER PERMISSION" Then
            lblUserID.Text = "UG ID"
            lblUserName.Text = "UG Name"
            Me.Tag = "Y00006"
            cmbUserGroup.Visible = False
            lblUserGroup.Visible = False
        End If
        AddGroupsToCMB()
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

    Private Sub getMenuItemstoGrid()
        Try
            errorevent = "get menu item to grid"
            connectionStaet()
            strSQL = "SELECT  MENUITEM_MENUTAG,MENUITEM_MENUNAME FROM MENUITEM WHERE COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            dgUserPermission.Rows.Clear()
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader("MENUITEM_MENUTAG"), dbConnections.dReader("MENUITEM_MENUNAME"), False, False, False, False)
            End While

            dbConnections.sqlCommand.CommandText = ""
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByVal MenuTag As String, ByVal MenuName As String, ByVal Access As Boolean, ByVal Create As Boolean, ByVal Edit As Boolean, ByVal Delete As Boolean)
        dgUserPermission.ColumnCount = 6
        dgUserPermission.Rows.Add(MenuTag, MenuName, Access, Create, Edit, Delete)
    End Sub


    '//Creates the permission chars that represent the rights. Used during save
    Private Function generatePermissionChars(ByVal rowIndex As Integer) As String
        Dim appendedPermissionChars As String = ""
        If dgUserPermission.Rows(rowIndex).Cells(2).EditedFormattedValue Then appendedPermissionChars += "A"
        If dgUserPermission.Rows(rowIndex).Cells(3).EditedFormattedValue Then appendedPermissionChars += "C"
        If dgUserPermission.Rows(rowIndex).Cells(4).EditedFormattedValue Then appendedPermissionChars += "M"
        If dgUserPermission.Rows(rowIndex).Cells(5).EditedFormattedValue Then appendedPermissionChars += "D"
        Return appendedPermissionChars
    End Function
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtUserName) = False Then
            Exit Function
        End If

        If Me.Text = "USER PERMISSION" Then
            If cmbUserGroup.Text = "" Then
                cmbUserGroup.Focus()
                Exit Function
            End If
        End If


        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtUserID.Text = ""
        txtUserName.Text = ""
        dgUserPermission.DataSource = Nothing
        dgUserPermission.Refresh()
        txtUserID.Enabled = True
        txtUserName.Enabled = True
        isEditClicked = False

        cbAccess.Checked = False
        cbCreate.Checked = False
        cbDelete.Checked = False
        cbEdit.Checked = False

        cbAccess.Enabled = False
        cbCreate.Enabled = False
        cbDelete.Enabled = False
        cbEdit.Enabled = False
        cmbUserGroup.Enabled = False
        '//Set enability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        getMenuItemstoGrid()
        Me.saveBtnStatus()
        txtUserID.Focus()
    End Sub
    Private Sub AddGroupsToCMB()
        connectionStaet()
        errorevent = "get groups to cmb"
        Try
            strSQL = "SELECT UG_NAME FROM TBLU_USERGROUPHED WHERE COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                cmbUserGroup.Items.Add(dbConnections.dReader.Item("UG_NAME"))
            End While
            dbConnections.dReader.Close()

        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub AddingGroupPermissionToGrid()
        ClearGridCheckBocClear()
        errorevent = "adding user permission to grid"
        Dim Menutag As String
        Dim MenuName As String
        Dim rights As String
        Try
            connectionStaet()
            strSQL = "SELECT    TBLU_USERGROUPDET.UG_MENURIGHT AS 'MENURIGHT', TBLU_USERGROUPDET.UG_MENUTAG AS 'MENUTAG', TBLU_USERGROUPDET.UG_MENUNAME AS 'MENUNAME' FROM TBLU_USERGROUPDET INNER JOIN TBLU_USERGROUPHED ON TBLU_USERGROUPDET.UG_ID = TBLU_USERGROUPHED.UG_ID WHERE (TBLU_USERGROUPHED.UG_NAME = '" & Trim(cmbUserGroup.SelectedItem.ToString) & "') AND TBLU_USERGROUPHED.COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read

                Menutag = Trim(dbConnections.dReader("MENUTAG"))
                MenuName = Trim(dbConnections.dReader("MENUNAME"))
                rights = Trim(dbConnections.dReader("MENURIGHT"))

                If InStr(1, rights, "A") Then canaccess1 = True Else canaccess1 = False
                If InStr(1, rights, "C") Then canCreate1 = True Else canCreate1 = False
                If InStr(1, rights, "M") Then canModify1 = True Else canModify1 = False
                If InStr(1, rights, "D") Then canDelete1 = True Else canDelete1 = False

                For y As Integer = 0 To dgUserPermission.Rows.Count - 1

                    If dgUserPermission.Rows(y).Cells(0).Value = Menutag Then
                        dgUserPermission.Rows(y).Cells(1).Value = MenuName
                        dgUserPermission.Rows(y).Cells(2).Value = canaccess1
                        dgUserPermission.Rows(y).Cells(3).Value = canCreate1
                        dgUserPermission.Rows(y).Cells(4).Value = canModify1
                        dgUserPermission.Rows(y).Cells(5).Value = canDelete1
                    End If
                Next
            End While
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X6) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X6", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub ClearGridCheckBocClear()
        For y As Integer = 0 To dgUserPermission.Rows.Count - 1
            dgUserPermission.Rows(y).Cells(2).Value = False
            dgUserPermission.Rows(y).Cells(3).Value = False
            dgUserPermission.Rows(y).Cells(4).Value = False
            dgUserPermission.Rows(y).Cells(5).Value = False
        Next
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"
    Private Sub txtUserID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtUserID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserID.Validating
        errorevent = "Reading information"
        Dim Menutag As String
        Dim MenuName As String
        Dim rights As String
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If

        connectionStaet()
        Try
            If Me.Text = "USER PERMISSION" Then
                strSQL = "SELECT [USERHED_USERCODE],[USERHED_TITLE] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] ='" & Trim(txtUserID.Text) & "'"
            Else
                strSQL = "SELECT [UG_NAME] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERGROUPHED] WHERE  [UG_ID]='" & Trim(txtUserID.Text) & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            End If
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                If Me.Text = "USER PERMISSION" Then
                    txtUserName.Text = dbConnections.dReader.Item("USERHED_TITLE")
                Else
                    txtUserName.Text = dbConnections.dReader.Item("UG_NAME")
                End If

                sender.Enabled = False
                'txtUserName.Enabled = False
                cbAccess.Enabled = False
                cbCreate.Enabled = False
                cbDelete.Enabled = False
                cbEdit.Enabled = False
                cmbUserGroup.Enabled = False
            End While
            dbConnections.dReader.Close()
            strSQL = ""
            dbConnections.sqlCommand.CommandText = ""

            If hasRecords = False Then
                If Me.Text = "USER PERMISSION" Then
                    MessageBox.Show("Please type the correct user code. Please retype the user code  or use F2", "Incorrect user name", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtUserID.Text = ""
                    txtUserID.Focus()
                    Exit Sub
                Else

                End If

            End If
            If Me.Text = "USER PERMISSION" Then
                strSQL = "SELECT TBLU_USERDET.USERDET_MENURIGHT, MENUITEM.MENUITEM_MENUTAG, MENUITEM.MENUITEM_MENUNAME FROM TBLU_USERDET INNER JOIN MENUITEM ON TBLU_USERDET.USERDET_MENUTAG = MENUITEM.MENUITEM_MENUTAG INNER JOIN TBLU_USERHED ON TBLU_USERDET.USERDET_USERCODE = TBLU_USERHED.USERHED_USERCODE WHERE USERDET_USERCODE = '" & Trim(txtUserID.Text) & "' AND MENUITEM.COM_ID = '" & globalVariables.selectedCompanyID & "'"
            Else
                strSQL = "SELECT  [UG_MENURIGHT],[UG_MENUTAG],[UG_MENUNAME] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERGROUPDET] WHERE [UG_ID] ='" & Trim(txtUserID.Text) & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            End If

            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If Me.Text = "USER PERMISSION" Then
                    Menutag = Trim(dbConnections.dReader("MENUITEM_MENUTAG"))
                    MenuName = Trim(dbConnections.dReader("MENUITEM_MENUNAME"))
                    rights = Trim(dbConnections.dReader("USERDET_MENURIGHT"))
                Else
                    Menutag = Trim(dbConnections.dReader("UG_MENUTAG"))
                    MenuName = Trim(dbConnections.dReader("UG_MENUNAME"))
                    rights = Trim(dbConnections.dReader("UG_MENURIGHT"))
                End If

                If InStr(1, rights, "A") Then canaccess1 = True Else canaccess1 = False
                If InStr(1, rights, "C") Then canCreate1 = True Else canCreate1 = False
                If InStr(1, rights, "M") Then canModify1 = True Else canModify1 = False
                If InStr(1, rights, "D") Then canDelete1 = True Else canDelete1 = False

                For y As Integer = 0 To dgUserPermission.Rows.Count - 1

                    If dgUserPermission.Rows(y).Cells(0).Value = Menutag Then
                        dgUserPermission.Rows(y).Cells(1).Value = MenuName
                        dgUserPermission.Rows(y).Cells(2).Value = canaccess1
                        dgUserPermission.Rows(y).Cells(3).Value = canCreate1
                        dgUserPermission.Rows(y).Cells(4).Value = canModify1
                        dgUserPermission.Rows(y).Cells(5).Value = canDelete1
                    End If
                Next
            End While
            dbConnections.sqlCommand.CommandText = ""

            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
                dgUserPermission.Enabled = False
            Else
                '//New user permissions
                txtUserID.Enabled = False
                cbAccess.Enabled = True
                cbCreate.Enabled = True
                cbDelete.Enabled = True
                cbEdit.Enabled = True
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If
            dgUserPermission.Sort(dgUserPermission.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
            dgUserPermission.Refresh()
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X7) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X7", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '===================================================================================================================
#Region "Button Events"
    Private Sub CheckboxChange(ByRef cb As CheckBox, ByRef cellValu As Integer)
        For y As Integer = 0 To dgUserPermission.Rows.Count - 1

            If cb.Checked = True Then
                dgUserPermission.Rows(y).Cells(cellValu).Value = True
            Else
                dgUserPermission.Rows(y).Cells(cellValu).Value = False
            End If
        Next
    End Sub

    Private Sub cbAccess_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAccess.CheckedChanged
        CheckboxChange(sender, 2)
    End Sub

    Private Sub cbCreate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCreate.CheckedChanged
        CheckboxChange(sender, 3)
    End Sub

    Private Sub cbDelete_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDelete.CheckedChanged
        CheckboxChange(sender, 5)
    End Sub

    Private Sub cbEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbEdit.CheckedChanged
        CheckboxChange(sender, 4)
    End Sub

    Private Sub cmbUserGroup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUserGroup.TextChanged
        AddingGroupPermissionToGrid()
    End Sub

#End Region


End Class