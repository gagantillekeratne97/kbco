Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmUserGroups
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
    Private Sub createNew()
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save() As Boolean
        save = False
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If

        Dim conf = MessageBox.Show("" & SaveMessage & "", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()
            Try
                If isEditClicked Then
                    errorevent = "Edit"
                    strSQL = "UPDATE " & selectedDatabaseName & ".dbo.TBLU_USERGROUPHED SET UG_NAME =@UG_NAME,MD_BY ='" & userSession & "', MD_DATE =GETDATE(), COM_ID=@COM_ID  WHERE UG_ID=@UG_ID"
                Else
                    errorevent = "Save"
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLU_USERGROUPHED  (UG_ID, UG_NAME, CR_BY, CR_DATE,COM_ID) VALUES     (@UG_ID, @UG_NAME, '" & userSession & "', GETDATE(),@COM_ID)"
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@UG_ID", Trim(txtUserID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@UG_NAME", Trim(txtGroupName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)


                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtUserID.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else

                End Select
            Catch ex As Exception
                dbConnections.dReader.Close()
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorevent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
            End Try
        End If
        Return save
    End Function

    Private Function delete() As Boolean
        errorevent = "Delete"
        delete = False
        If Not canDelete Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If
        connectionStaet()
        Dim IDbackup As String = Trim(txtUserID.Text)
        Dim IDdesc As String = Trim(txtGroupName.Text)
        Try
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtGroupName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERGROUPHED WHERE UG_ID =@UG_ID AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@UG_ID", Trim(txtUserID.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
            End If

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
        Return delete
    End Function

    Private Sub FormEdit()
        If Not canModify Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show("" & EditMessage & "" & txtGroupName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            txtGroupName.Enabled = True
            txtGroupName.Focus()
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

    Private Sub frmUserGroups_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmUserGroups_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmUserGroups_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmUserGroups_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmUserGroups_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmUserGroupsb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
    End Sub

    Private Sub frmUserGroups_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorevent = "Accesses user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)

            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
            'dbConnections.sqlConnection.Close()
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorevent, userSession, userName, DateTime.Now, ex.Message)
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
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtUserID) = False Then
            Exit Function
        End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtUserID.Text = ""
        txtGroupName.Text = ""
        txtGroupName.Enabled = True
        txtUserID.Enabled = True
        txtUserID.Focus()
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
    Private Sub txtUserID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserID.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtUserID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress
        generalValObj.isLetterOrDigit(e)
    End Sub
    Private Sub txtUserID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserID.Validating
        errorevent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT UG_NAME FROM " & selectedDatabaseName & ".dbo.TBLU_USERGROUPHED WHERE UG_ID =@UG_ID AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@UG_ID", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtGroupName.Text = dbConnections.dReader.Item("UG_NAME")

                sender.Enabled = False
                txtGroupName.Enabled = False

            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtUserID.Enabled = False
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(UG00X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "UG00X4", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub txtGroupName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGroupName.KeyDown
        KeyPresSoundDisable(e)
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()
        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"

        Dim path As String = globalVariables.crystalReportpath & "\Reports\rptGroupReport.rpt"

        'Dim manualreport As New rptBank
        Dim cryRpt As New ReportDocument
        cryRpt.Load(path)

        globalFunctions.generateReportTemplate(cryRpt, Me.Text)
        Dim CrTables As Tables
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo


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
        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.Show()
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '===================================================================================================================
#Region "Button Events"

#End Region



End Class