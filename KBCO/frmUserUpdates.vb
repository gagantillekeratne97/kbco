Imports System.Data.SqlClient
Public Class frmUserUpdates
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
    Private UserDataExits As Boolean
    Const WMCLOSE As String = "WmClose"
    Private img As String
    Private IsAllUser As Boolean = False

    '//Active form perform btn click case 
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                'Me.createNew()
            Case "Save"
                If save() Then FormClear()
            Case "Edit"
                'Me.FormEdit()
            Case "Delete"
                'If deleteRecord() Then FormClear()
            Case "Search"
                'SendKeys.Send("{F2}")
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

    Dim MenuItemsDT As DataTable
    Dim workCol As New DataColumn
    Dim MenuItems As New DataSet("MI")
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"
    Private Function save() As Boolean
        errorevent = "Save"
        Dim rowcount As Integer = dgUserPermission.RowCount
        Dim Value1 As Double = rowcount * 100
        Dim value2 As Double
        Dim countvalue As Integer = 1
        save = False
        Dim conf = MessageBox.Show("Do you want to update your system? " & vbNewLine & "Before updating your system  save all your current work." & vbNewLine & "If not saved your work click 'No' button." & vbNewLine & "To proceed update click 'Yes' button.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbNo Then
            Exit Function
        End If
        Try
            connectionStaet()
            Me.Cursor = Cursors.WaitCursor
            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
            dbConnections.sqlCommand.Transaction = dbConnections.sqlTransaction
            '//Delete all permissions before saving the new changes
            strSQL = "DELETE FROM TBLU_USERDET WHERE USERDET_USERCODE = '" & Trim(userSession) & "' AND COM_ID='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.sqlCommand.ExecuteNonQuery()
            For Each row As DataGridViewRow In dgUserPermission.Rows
                'progress bar adding values using exsisting loop
                value2 = ((Value1 / rowcount) * 1)
                pgBar.Value = value2
                Dim permissionsSelected As String = Me.generatePermissionChars(row.Index)
                If Not permissionsSelected = "" Then
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLU_USERDET (USERDET_USERCODE, USERDET_MENUTAG, USERDET_MENURIGHT, DTS_DATE,USERDET_USERGROUP,COM_ID)VALUES     ('" & Trim(userSession) & "', '" & dgUserPermission.Rows(row.Index).Cells(0).Value & "', '" & permissionsSelected & "', getdate(),'" & UsergroupName & "', '" & globalVariables.selectedCompanyID & "')"
                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                End If
                countvalue += 1
            Next
            dbConnections.sqlTransaction.Commit()
            save = True
            If save = True Then
            End If
        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            MessageBox.Show("System updated. System will restart now", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dbConnections.sqlConnection.Close()
            Application.Restart()
        End Try
    End Function

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"


    Private Sub frmUserUpdates_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmUserUpdates_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmUserUpdates_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmUserUpdates_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmUserUpdates_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False

    End Sub

    Private Sub frmUserUpdates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        getMenuItemstoGrid()
        AddingGroupPermissionToGrid()
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
        errorevent = "get menu item to grid"
        Try
            connectionStaet()
            strSQL = "SELECT  [MENUITEM_MENUTAG],[MENUITEM_MENUNAME] FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM] WHERE COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            dgUserPermission.Rows.Clear()
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader("MENUITEM_MENUTAG"), dbConnections.dReader("MENUITEM_MENUNAME"), False, False, False, False)
            End While
            dbConnections.dReader.Close()
            dbConnections.sqlCommand.CommandText = ""
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorevent, userSession, userName, DateTime.Now, ex.Message)
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


    Private Sub FormClear()
        dgUserPermission.DataSource = Nothing
        dgUserPermission.Refresh()
        '//Set enability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        getMenuItemstoGrid()
        Me.saveBtnStatus()
    End Sub

    Private Sub AddingGroupPermissionToGrid()
        ClearGridCheckBocClear()
        errorevent = "adding groups to grid"
        Dim Menutag As String
        Dim MenuName As String
        Dim rights As String
        Try
            connectionStaet()
            strSQL = "SELECT    TBLU_USERGROUPDET.UG_MENURIGHT AS 'MENURIGHT', TBLU_USERGROUPDET.UG_MENUTAG AS 'MENUTAG', TBLU_USERGROUPDET.UG_MENUNAME AS 'MENUNAME' FROM TBLU_USERGROUPDET INNER JOIN TBLU_USERGROUPHED ON TBLU_USERGROUPDET.UG_ID = TBLU_USERGROUPHED.UG_ID WHERE (TBLU_USERGROUPHED.UG_NAME = '" & UsergroupName & "') AND TBLU_USERGROUPHED.COM_ID = '" & globalVariables.selectedCompanyID & "'"
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
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorevent, userSession, userName, DateTime.Now, ex.Message)
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


    Private Sub TestDataaset()
        ClearGridCheckBocClear()
        errorevent = "adding groups to grid"
        Dim Menutag As String
        Dim MenuName As String
        Dim rights As String
        Try
            connectionStaet()
            strSQL = "SELECT    TBLU_USERGROUPDET.UG_MENURIGHT AS 'MENURIGHT', TBLU_USERGROUPDET.UG_MENUTAG AS 'MENUTAG', TBLU_USERGROUPDET.UG_MENUNAME AS 'MENUNAME' FROM TBLU_USERGROUPDET INNER JOIN TBLU_USERGROUPHED ON TBLU_USERGROUPDET.UG_ID = TBLU_USERGROUPHED.UG_ID WHERE (TBLU_USERGROUPHED.UG_NAME = '" & UsergroupName & "')"
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

            'dbConnections.ServistasqlAdeptor = New SqlDataAdapter("Select * From Authors", dbConnections.sqlConnection)

            'Dim dsPubs As New DataSet("Pubs")
            'daAuthors.FillSchema(dsPubs, SchemaType.Source, "Authors")
            'daAuthors.Fill(dsPubs, "Authors")

            'Dim tblAuthors As DataTable
            'tblAuthors = dsPubs.Tables("Authors")
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorevent, userSession, userName, DateTime.Now, ex.Message)
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
    Private Sub btnUpdateSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSystem.Click
        Me.save()
    End Sub

#End Region



End Class