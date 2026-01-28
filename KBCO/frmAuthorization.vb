Imports System.Data.SqlClient
Public Class frmAuthorization
    Private errorEvent As String
    Private strSQL As String
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Private AuUserCode As String
    Private MD5obj As New MD5
    Private UserConfirm As Boolean = False

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmAuthorization_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub
    Private Sub frmAuthorization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''all functions of the form .......................................................
    '===================================================================================================================
#Region "Functions & Subs"
    Private Sub Authorizing()
        If Not Me.CheckPassword Then
            Exit Sub
        End If
        errorEvent = "read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & AuUserCode & "' AND USERDET_MENUTAG='" & globalVariables.AuthorizingFormTag & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True

            If globalVariables.AuthorizationType = "Save" Then
                If canCreate Then
                    globalVariables.UserAuthorize = True
                End If

            ElseIf globalVariables.AuthorizationType = "Edit" Then
                If canModify Then
                    globalVariables.UserAuthorize = True
                End If

            ElseIf globalVariables.AuthorizationType = "Delete" Then
                If canDelete Then
                    globalVariables.UserAuthorize = True
                End If
            Else
                MessageBox.Show("Authorization username or password incorrect. Please try again.", "Authorization fail", MessageBoxButtons.OK, MessageBoxIcon.Information)
                globalVariables.UserAuthorize = False
            End If
            FormClear()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub


    Private Sub GetUserCode()
        errorEvent = "Get Log UserCode"
        connectionStaet()
        Try
            strSQL = "SELECT [USERHED_USERCODE] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_TITLE] ='" & Trim(txtAuthorizedUser.Text) & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            AuUserCode = Trim(dbConnections.sqlCommand.ExecuteScalar)

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function CheckPassword() As Boolean
        CheckPassword = False
        Dim decryptedpassword As String
        Try
            connectionStaet()
            strSQL = "SELECT [USERHED_PASSWORD] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] =@TxtUserCode AND [USERHED_ACTIVEUSER] ='1'"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection) '//compare location with DB

            dbConnections.sqlCommand.Parameters.AddWithValue("@TxtUserCode", AuUserCode)
        decryptedpassword = MD5obj.DecryptPassword(dbConnections.sqlCommand.ExecuteScalar)

            If Trim(Trim(txtPassword.Text) = decryptedpassword) Then
                CheckPassword = True
            End If
            Return CheckPassword
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Function

    Private Sub FormClear()
        txtAuthorizedUser.Text = ""
        txtPassword.Text = ""
        txtAuthorizedUser.Focus()
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"
    Private Sub txtAuthorizedUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAuthorizedUser.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
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
    Private Sub btnAuthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthorize.Click
        Me.GetUserCode()
        Me.Authorizing()
    End Sub
    Private Sub btnDeauthorize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeauthorize.Click
        Me.Hide()
    End Sub
#End Region


End Class