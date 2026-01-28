Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class frmAddUser
    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Const WMCLOSE As String = "WmClose"
    Private generalValObj As New generalValidation
    Private PCname As String = System.Net.Dns.GetHostName
    Private img As String
    Private MD5obj As New MD5
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If Save() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'showCrystalReport()
        End Select
    End Sub

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"
    'Save Record
    Private Function Save()
        Dim IDbackup As String = Trim(txtUser_ID.Text)
        Dim IDdesc As String = Trim(txtUserName.Text)
        Save = False
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If

        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()
            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
            Try
                If isEditClicked Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE TBLU_USERHED SET USERHED_MOBILE=@USERHED_MOBILE,USERHED_EMAIL=@USERHED_EMAIL, USERHED_USERCODE =@USERHED_USERCODE, USERHED_TITLE =@USERHED_TITLE, USERHED_PASSWORD =@USERHED_PASSWORD, USERHED_ACTIVEUSER ='" & cbActiveUser.CheckState & "', USERHED_USERPC =@USERHED_USERPC, USERHED_PICTURE =@USERHED_PICTURE ,USERHED_POSITION =@USERHED_POSITION,USERHED_MT_SYS_LOGIN=@USERHED_MT_SYS_LOGIN WHERE USERHED_USERCODE = '" & Trim(txtUser_ID.Text) & "'"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO TBLU_USERHED(USERHED_USERCODE, USERHED_TITLE, USERHED_PASSWORD, USERHED_ACTIVEUSER, USERHED_USERPC, USERHED_PICTURE,USERHED_POSITION,USERHED_MOBILE,USERHED_EMAIL,USERHED_MT_SYS_LOGIN)VALUES (@USERHED_USERCODE,@USERHED_TITLE,@USERHED_PASSWORD,'" & cbActiveUser.CheckState & "',@USERHED_USERPC,@USERHED_PICTURE,@USERHED_POSITION,@USERHED_MOBILE,@USERHED_EMAIL,@USERHED_MT_SYS_LOGIN)"
                End If
                Dim PWD As String = MD5obj.EncryptPassword(Trim(txtPassword.Text))
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtUser_ID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_TITLE", Trim(txtUserName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_PASSWORD", PWD)
                If cbAssignToPC.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERPC", Trim(txtPCName.Text))
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERPC", DBNull.Value)
                End If

                If pbUserImage.BackgroundImage Is Nothing Then
                    dbConnections.sqlCommand.Parameters.Add("@USERHED_PICTURE", SqlDbType.Image).Value = DBNull.Value
                Else
                    'Image converting code
                    Dim ms As New IO.MemoryStream
                    pbUserImage.BackgroundImage.Save(ms, Imaging.ImageFormat.Jpeg)
                    Dim bytes() As Byte = ms.GetBuffer()
                    dbConnections.sqlCommand.Parameters.Add(New SqlParameter("@USERHED_PICTURE", SqlDbType.Image)).Value = bytes
                End If

                If Trim(txtMobileNo.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_MOBILE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_MOBILE", Trim(txtMobileNo.Text))
                End If

                If Trim(txtEmail.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_EMAIL", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_EMAIL", Trim(txtEmail.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_POSITION", Trim(cmbUserGroup.SelectedItem.ToString))
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_MT_SYS_LOGIN", cbMachineTracerLogin.CheckState)





                If dbConnections.sqlCommand.ExecuteNonQuery() Then Save = True Else Save = False


                If Save = True Then
                    dbConnections.sqlCommand.Parameters.Clear()
                    strSQL = "DELETE FROM TBLU_COMPANY_DET WHERE     (USERHED_USERCODE = '" & Trim(txtUser_ID.Text) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    If dbConnections.sqlCommand.ExecuteNonQuery() Then Save = True Else Save = False

                    For Each row As DataGridViewRow In dgCompanies.Rows

                        If dgCompanies.Rows(row.Index).Cells("CHECK").Value = True Then

                            dbConnections.sqlCommand.Parameters.Clear()
                            strSQL = "INSERT INTO TBLU_COMPANY_DET (USERHED_USERCODE, COM_ID) VALUES     (@USERHED_USERCODE, @COM_ID)"
                            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)

                            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtUser_ID.Text))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(dgCompanies.Rows(row.Index).Cells("COM_ID").Value))

                            dbConnections.sqlCommand.ExecuteNonQuery()
                        End If
                    Next
                End If




                dbConnections.sqlTransaction.Commit()

             
            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtUserName.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else

                End Select
            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                connectionClose()
                Me.NextUserID()
            End Try
        End If
    End Function

    'new record
    Private Sub createNew()
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    'Edit Record
    Private Sub FormEdit()
        If Not canModify Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim conf = MessageBox.Show("" & EditMessage & ": " & txtUserName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            txtUser_ID.Enabled = False
            FormtextBoxEnableState(True)
            cbShowPwd.Enabled = True
            isEditClicked = True
            txtUserName.Focus()
            globalFunctions.globalButtonActivation(True, True, False, False, False, True)
            Me.saveBtnStatus()

        End If
    End Sub

    Private Function delete() As Boolean
        Dim IDbackup As String = Trim(txtUser_ID.Text)
        Dim IDdesc As String = Trim(txtUserName.Text)
        errorEvent = "Delete"
        delete = False
        If Not canDelete Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If
        connectionStaet()
        Try
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtUserName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
                dbConnections.sqlCommand.Transaction = dbConnections.sqlTransaction
                'delete user form user table
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERHED WHERE USERHED_USERCODE =@USERHED_USERCODE"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtUser_ID.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False

                'delete users permission
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBLU_USERDET WHERE USERDET_USERCODE =@USERDET_USERCODE"
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERDET_USERCODE", Trim(txtUser_ID.Text))
                dbConnections.sqlCommand.CommandText = strSQL
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
                dbConnections.sqlTransaction.Commit()

              
            End If
        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try

    End Function

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"

    Private Sub frmAddUser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & " " & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub frmAddUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        globalFunctions.globalButtonActivation(True, True, True, True, True, True)
        ' Me.NextUserID()
        Me.AddGroupsToCMB()
    End Sub

    Private Sub frmAddUser_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmAddUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmAddUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmAddUser_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        errorEvent = "read User Permission"
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
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

    Private Sub FormtextBoxEnableState(ByRef enablestate As Boolean)
        txtPassword.Enabled = enablestate
        txtUserName.Enabled = enablestate
        txtUser_ID.Enabled = enablestate
        txtPassword.Enabled = enablestate
        txtReTypePassword.Enabled = enablestate
        pbUserImage.Enabled = enablestate
        cmbUserGroup.Enabled = enablestate
        cbAssignToPC.Enabled = enablestate
        txtPCName.Enabled = enablestate
        cbActiveUser.Enabled = enablestate
        txtEmail.Enabled = enablestate
        txtMobileNo.Enabled = enablestate
    End Sub

    Private Sub NextUserID()
        errorEvent = "NextuserID"
        Try
            connectionStaet()
            strSQL = "SELECT  max([USERHED_USERCODE]) FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED]"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim UserID As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            Dim Num As String
            'UserID = UserID.Replace("U", "")
            Num = IncrementStringID(UserID)
            txtUser_ID.Text = Num
            lblID.Text = Num
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub
    Public Function IncrementStringID(ByVal Sender As String) As String

        Dim Index As Integer

        For Item As Integer = Sender.Length - 1 To 0 Step -1
            Select Case Sender.Substring(Item, 1)
                Case "0" To "9"

                Case Else
                    Index = Item
                    Exit For
            End Select
        Next

        If Index = Sender.Length - 1 Then
            Return Sender & "1" '  Optionally throw an exception ?
        Else
            Dim x As Integer = Index + 1
            Dim value As Integer = Integer.Parse(Sender.Substring(x)) + 1
            Return Sender.Substring(0, x) & value.ToString()
        End If

    End Function
    Private Sub AddGroupsToCMB()
        connectionStaet()
        errorEvent = "AddGroups to CMB"
        Try
            strSQL = "SELECT [UG_NAME] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERGROUPHED]"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                cmbUserGroup.Items.Add(dbConnections.dReader.Item("UG_NAME"))
            End While


        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub


    Private Sub LoadCompanies()
        connectionStaet()
        errorEvent = "AddGroups to CMB"
        Try
            dgCompanies.Rows.Clear()
            strSQL = "SELECT     COM_ID, COM_NAME FROM         L_TBL_COMPANIES"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            While dbConnections.dReader.Read
                populatreDatagrid(False, dbConnections.dReader.Item("COM_ID"), dbConnections.dReader.Item("COM_NAME"))
            End While


        Catch ex As Exception
            dbConnections.dReader.Close()
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef Check As Boolean, ByRef ComID As String, ByRef ComName As String)
        dgCompanies.ColumnCount = 3
        dgCompanies.Rows.Add(Check, ComID, ComName)
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtPassword) = False Then
            Exit Function
        End If
        If cmbUserGroup.Text = "" Then
            Exit Function
        End If
        If generalValObj.isPresent(txtReTypePassword) = False Then
            Exit Function
        End If
        If Trim(txtPassword.Text) = Trim(txtReTypePassword.Text) = False Then
            txtPassword.Text = ""
            txtReTypePassword.Text = ""
            txtPassword.Focus()
            Exit Function
        End If
        Dim IsChecked As Boolean = False
        For Each row As DataGridViewRow In dgCompanies.Rows
            If dgCompanies.Rows(row.Index).Cells("CHECK").Value = True Then
                IsChecked = True
            End If
        Next

        If IsChecked = False Then
            MessageBox.Show("Please select one or more company to create this user account.", "Select Company", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtUser_ID.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtReTypePassword.Text = ""
        cmbUserGroup.Text = ""
        cbActiveUser.Checked = True
        FormtextBoxEnableState(True)
        pbUserImage.BackgroundImage = Nothing
        img = ""
        txtUser_ID.Focus()
        cbAssignToPC.Checked = True
        txtUser_ID.Enabled = True
        cbShowPwd.Checked = False
        isEditClicked = False

        txtPCName.Text = ""
        txtEmail.Text = ""
        txtMobileNo.Text = ""
        LoadCompanies()
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"
    Private Sub txtUser_ID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUser_ID.Validating
        If IsFormClosing() Then Exit Sub
        'If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT  [USERHED_USERCODE],[USERHED_TITLE],[USERHED_PASSWORD],[USERHED_ACTIVEUSER],[USERHED_PICTURE],[USERHED_POSITION],[USERHED_USERPC],[USERHED_EMAIL] ,[USERHED_MOBILE],[USERHED_MT_SYS_LOGIN] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] =@USERHED_USERCODE"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtUserName.Text = dbConnections.dReader.Item("USERHED_TITLE")
                txtPassword.Text = ""
                cbActiveUser.Checked = dbConnections.dReader.Item("USERHED_ACTIVEUSER")
                If IsDBNull(dbConnections.dReader("USERHED_PICTURE")) Then
                Else
                    Dim x() As Byte = dbConnections.dReader("USERHED_PICTURE")
                    Dim mStream As MemoryStream = New MemoryStream(x)
                    Dim img As Image = Image.FromStream(mStream)
                    pbUserImage.BackgroundImage = img
                    pbUserImage.BackgroundImageLayout = ImageLayout.Zoom
                End If
                If IsDBNull(dbConnections.dReader.Item("USERHED_POSITION")) Then
                    cmbUserGroup.Text = "None"
                Else
                    cmbUserGroup.Text = dbConnections.dReader.Item("USERHED_POSITION")
                End If

                txtPassword.Text = dbConnections.dReader.Item("USERHED_PASSWORD")
                If IsDBNull(dbConnections.dReader.Item("USERHED_USERPC")) Then
                    txtPCName.Text = ""
                Else
                    txtPCName.Text = dbConnections.dReader.Item("USERHED_USERPC")
                End If

                If IsDBNull(dbConnections.dReader.Item("USERHED_EMAIL")) Then
                    txtEmail.Text = ""
                Else
                    txtEmail.Text = dbConnections.dReader.Item("USERHED_EMAIL")
                End If

                If IsDBNull(dbConnections.dReader.Item("USERHED_MOBILE")) Then
                    txtMobileNo.Text = ""
                Else
                    txtMobileNo.Text = dbConnections.dReader.Item("USERHED_MOBILE")
                End If

                If IsDBNull(dbConnections.dReader.Item("USERHED_MT_SYS_LOGIN")) Then
                    cbMachineTracerLogin.Checked = False
                Else
                    If dbConnections.dReader.Item("USERHED_MT_SYS_LOGIN") Then
                        cbMachineTracerLogin.Checked = True
                    Else
                        cbMachineTracerLogin.Checked = False
                    End If

                End If
                sender.Enabled = False

            End While

            dbConnections.dReader.Close()


            strSQL = "SELECT COM_ID FROM TBLU_COMPANY_DET WHERE     (USERHED_USERCODE = @USERHED_USERCODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtUser_ID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read



                If Not IsDBNull(dbConnections.dReader.Item("COM_ID")) Then
                    For Each row As DataGridViewRow In dgCompanies.Rows
                        If dbConnections.dReader.Item("COM_ID") = dgCompanies.Rows(row.Index).Cells("COM_ID").ToString() Then
                            dgCompanies.Rows(row.Index).Cells("CHECK").Value = True
                        Else
                            dgCompanies.Rows(row.Index).Cells("CHECK").Value = False
                        End If

                    Next
                End If
            End While

            dbConnections.dReader.Close()



            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
                FormtextBoxEnableState(False)
            Else
                '//New user permissions
                txtUser_ID.Enabled = False
                txtPCName.Text = PCname
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X6) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X6", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub txtUser_ID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUser_ID.KeyDown
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

    Private Sub cbShowPwd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowPwd.CheckedChanged
        If cbShowPwd.Checked = True Then
            cbShowPwd.Text = "Show"
            txtPassword.Text = MD5obj.DecryptPassword(Trim(txtPassword.Text))
            txtPassword.PasswordChar = Nothing
        Else
            cbShowPwd.Text = "Hide"
            txtPassword.Text = MD5obj.EncryptPassword(Trim(txtPassword.Text))
            txtPassword.PasswordChar = "*"
        End If
    End Sub
    Private Sub frm_Camara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Camara.Click
        globalVariables.CamaraFormName = Me.Text
        frmCamera.MdiParent = frmMDImain
        frmCamera.Show()
    End Sub

    Private Sub btnBrows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrows.Click
        Try
            'Set the Filter.
            OpenFileDialog1.Filter = "All Files|*.*"

            'Clear the file name
            OpenFileDialog1.FileName = ""
            'Show it
            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                'Get the image name
                img = OpenFileDialog1.FileName

                'Create a new Bitmap and display it
                pbUserImage.BackgroundImage = System.Drawing.Bitmap.FromFile(img)
                pbUserImage.BackgroundImageLayout = ImageLayout.Zoom
            End If
        Catch ex As Exception
            MessageBox.Show("Please select a image file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cbActiveUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbActiveUser.CheckedChanged
        If cbActiveUser.CheckState = CheckState.Checked Then
            cbActiveUser.Text = "Active"
        Else
            cbActiveUser.Text = "Inactive"
        End If
    End Sub

    Private Sub btnNextAvaibleUID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NextUserID()
    End Sub
    Private Sub pbUserImage_BackgroundImageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbUserImage.BackgroundImageChanged
        lblPictures.Visible = False
    End Sub
    Private Sub lblID_Click(sender As Object, e As EventArgs) Handles lblID.Click
        txtUser_ID.Text = sender.text
    End Sub
#End Region




    Private Sub cbMachineTracerLogin_CheckedChanged(sender As Object, e As EventArgs) Handles cbMachineTracerLogin.CheckedChanged
        If cbMachineTracerLogin.Checked = True Then
            cbMachineTracerLogin.Text = "Yes"
        Else
            cbMachineTracerLogin.Text = "No"
        End If
    End Sub

    Private Sub txtUser_ID_TextChanged(sender As Object, e As EventArgs) Handles txtUser_ID.TextChanged

    End Sub
End Class