Imports System.IO
Imports System.Data.SqlClient
Imports System.Text
Imports System.Data
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports System.Windows.Controls.Primitives
Imports System.Net.NetworkInformation
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Windows.Media

Public Class Login
    Private Shared DES As New TripleDESCryptoServiceProvider
    Private Shared MD5 As New MD5CryptoServiceProvider
    Private isUserSelected As Boolean = False
    Private loginMode As String = "DefaultMode"
    Private strSQL As String
    Dim selectedUserName As String
    Dim hashedPassed As String
    Private MD5obj As New MD5
    Dim CompanyCout As Integer = 0


    Private Sub Login_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown

        If e.Key = 8 Then
            capsLOCK() '//Check for caps lock
        ElseIf e.Key = 13 Then
            If isUserSelected Then
                uProfiles.Children.Clear()
                txtPassword.Clear()
                lblPasswordPlaceholder.Visibility = Windows.Visibility.Hidden
                txtPassword.Visibility = Windows.Visibility.Hidden
                btnLogin.Visibility = Windows.Visibility.Hidden
                btnSwitchUser.Visibility = Windows.Visibility.Visible
                Me.getProfileInformation("SELECT USERHED_PICTURE, USERHED_TITLE, USERHED_USERCODE FROM TBLU_USERHED WHERE USERHED_ACTIVEUSER=1 AND USERHED_USERPC='" & Me.getPCName & "'") '//Calls the local function get profile information
                isUserSelected = False
                capsLOCK()
            End If
        End If
    End Sub


    Private Function initializeLogin() As Boolean
        capsLOCK() '//Check for caps lock
        Dim profileInit As Boolean = Me.getProfileInformation("SELECT USERHED_PICTURE, USERHED_TITLE, USERHED_USERCODE FROM TBLU_USERHED WHERE USERHED_ACTIVEUSER=1 AND USERHED_USERPC='" & Me.getPCName() & "'") '//Calls the local function get profile information
        Return profileInit
    End Function

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Me.initializeLogin()
        lblSoftwareName.Content = "K-Bridge Creative Outsource (v" & KBridgeVersion & ")"
    End Sub

    '//Local function for retrieving the profile info
    Private Function getProfileInformation(ByVal selectString As String) As Boolean
        Try
            dbConnections.sqlCommand = New SqlCommand(selectString, dbConnections.sqlConnection)
            dbConnections.sqlAdapter = New SqlDataAdapter(dbConnections.sqlCommand)
            dbConnections.dset = New DataSet
            dbConnections.sqlAdapter.Fill(dbConnections.dset, "tmp_profileInformation")

            Dim rowCount As Integer = dbConnections.dset.Tables("tmp_profileInformation").Rows.Count - 1
            If rowCount < 0 Then
                MessageBox.Show("There are no users assigned to this computer. Please enter your user name and password to login to the system", "No users assigned", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                loginMode = "SwitchUserMode"
                activateSwitchUserMode()
                btnSwitchUser.Visibility = Windows.Visibility.Collapsed
                Return False
            Else
                While rowCount >= 0
                    If IsDBNull(dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(0)) Then
                        Dim profileDummy As New Image
                        Dim uri As New Uri("Images/backgroundImages/profileDummy.png", UriKind.Relative)
                        profileDummy.Source = New BitmapImage(uri)
                        Me.addProfileInfoToWindow(profileDummy, dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(1), dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(2))
                    Else
                        Dim imgSource1() As Byte = DirectCast(dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(0), Byte())
                        Dim stream As MemoryStream = New MemoryStream
                        stream.Write(imgSource1, 0, imgSource1.Length - 1)
                        stream.Seek(0, SeekOrigin.Begin)
                        Dim bitmap As New BitmapImage
                        bitmap.BeginInit()
                        bitmap.StreamSource = stream
                        bitmap.EndInit()
                        Dim ProfileImage As New Image
                        ProfileImage.Source = bitmap
                        Me.addProfileInfoToWindow(ProfileImage, dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(1), dbConnections.dset.Tables("tmp_profileInformation").Rows(rowCount).Item(2))
                    End If
                    rowCount -= 1
                End While
                Return True
            End If
        Catch ex As SqlException
            MessageBox.Show("Unable to fetch user information." & vbNewLine & ex.Message, "Database connection error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            dbConnections.dset.Dispose()
        End Try
    End Function

    '//Local function for getting the local machines mac address
    Private Function getPCName() As String
        Dim ipproperties As IPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties()
        Return ipproperties.HostName
    End Function

    Dim uProfilesStackPanel As StackPanel
    '//Local function for preparing the layout required to display the user profile information
    Private Sub addProfileInfoToWindow(ByVal UserImage As Image, ByVal userName As String, ByVal userCode As String)
        '//Adding borders to the profiles
        Dim UserImageborder As New Border
        '//Locating style resource from the xaml
        UserImageborder.Style = FindResource("userProfBorders")
        UserImageborder.Child = UserImage

        '//Adding stack panel for displaying the user name and the image
        uProfilesStackPanel = New StackPanel
        uProfilesStackPanel.Style = FindResource("profileInfoStackPanel")
        uProfilesStackPanel.Children.Add(UserImageborder)

        Dim userNameTextBlock As New TextBlock
        userNameTextBlock.Foreground = New SolidColorBrush(Colors.WhiteSmoke) '// changing color of the fonts
        userNameTextBlock.FontSize = 17
        userNameTextBlock.Style = FindResource("userProfLabels")

        userNameTextBlock.Background = Brushes.Black
        userNameTextBlock.TextAlignment = Windows.TextAlignment.Center

        userNameTextBlock.Text = userName
        userNameTextBlock.Name = userCode
        uProfilesStackPanel.Name = userCode
        uProfilesStackPanel.Children.Add(userNameTextBlock)
        uProfiles.Children.Add(uProfilesStackPanel)
        AddHandler uProfilesStackPanel.MouseLeftButtonUp, AddressOf UserAccountSelected
    End Sub

    Private Sub UserAccountSelected(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If Not isUserSelected Then
            uProfiles.Children.Clear()
            btnSwitchUser.Visibility = Windows.Visibility.Hidden
            lblPasswordPlaceholder.Visibility = Windows.Visibility.Visible
            txtPassword.Visibility = Windows.Visibility.Visible
            btnLogin.Visibility = Windows.Visibility.Visible
            Me.getProfileInformation("SELECT USERHED_PICTURE, USERHED_TITLE, USERHED_USERCODE FROM TBLU_USERHED WHERE USERHED_USERCODE='" & sender.Name.ToString & "'")
            isUserSelected = True
            capsLOCK()
            txtPassword.Focus()
        End If
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnLogin.Click
        Dim decryptedpassword As String
        Try
            If loginMode = "DefaultMode" Then
                selectedUserName = uProfilesStackPanel.Children.OfType(Of TextBlock).FirstOrDefault().Name.ToString  '//Referring the text block in the stack-panel
            Else
                GetUserCode()
            End If
            strSQL = "SELECT [USERHED_PASSWORD] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] =@TxtUserCode AND [USERHED_ACTIVEUSER] ='1'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection) '//compare location with DB
            hashedPassed = MD5obj.EncryptPassword(txtPassword.Password)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TxtUserCode", selectedUserName)
            decryptedpassword = MD5obj.DecryptPassword(dbConnections.sqlCommand.ExecuteScalar)
            If Trim(txtPassword.Password = decryptedpassword) Then '//If 1 then the user name and password is valid
                userSession = selectedUserName
                txtPassword.Password = ""

                strSQL = "SELECT COUNT(COM_ID) FROM TBLU_COMPANY_DET WHERE     (USERHED_USERCODE = '" & userSession & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                    CompanyCout = 0
                Else
                    CompanyCout = dbConnections.sqlCommand.ExecuteScalar
                End If

                If CompanyCout = 1 Then

                    strSQL = "SELECT COM_ID FROM TBLU_COMPANY_DET WHERE     (USERHED_USERCODE = '" & userSession & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    If Not IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                        selectedCompanyID = dbConnections.sqlCommand.ExecuteScalar
                        frmMDImain.Show()
                        frmLoginWinForm.Hide()
                    End If

                Else
                    frmSelectLoginCompany1.Show()
                    frmLoginWinForm.Hide()
                End If


            Else '//If 0 the user name and password is invalid
                MessageBox.Show("The user name or password you entered is incorrect." & vbNewLine & "Please check the caps-lock as the password is case-sensitive", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Clear()
                If loginMode = "DefaultMode" Then
                    txtPassword.Focus()
                Else
                    lblPasswordPlaceholder.Visibility = Windows.Visibility.Visible
                    txtUsername.Focus()
                    txtUsername.SelectAll()
                End If
            End If

        Catch ex As SqlException
            MessageBox.Show("Unable to establish a connection with the database." & vbNewLine & "Please check your connection and re-try..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub capsLOCK()
        If System.Windows.Forms.Control.IsKeyLocked(Keys.CapsLock) And (isUserSelected Or loginMode = "SwitchUserMode") Then
            lblCapsLockInfo.Visibility = Windows.Visibility.Visible
        Else
            lblCapsLockInfo.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Dim closeConfirm = MessageBox.Show("Are you sure you want to exit ?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If closeConfirm = vbYes Then
            dbConnections.DisconnectFromDB()
            End
        End If
    End Sub

    Private Sub activateSwitchUserMode()
        uProfiles.Visibility = Windows.Visibility.Hidden
        lblPasswordPlaceholder.Visibility = Windows.Visibility.Visible
        lblUsernamePlaceholder.Visibility = Windows.Visibility.Visible
        txtPassword.Visibility = Windows.Visibility.Visible
        txtUsername.Visibility = Windows.Visibility.Visible
        txtUsername.Focus()
        btnLogin.Visibility = Windows.Visibility.Visible
        loginMode = "SwitchUserMode"
        capsLOCK()
        btnSwitchUser.Content = "Show users"
    End Sub

    Private Sub btnSwitchUser_Click(ByVal sender As System.Windows.Controls.Button, ByVal e As System.Windows.RoutedEventArgs) Handles btnSwitchUser.Click
        txtUsername.Clear()
        txtPassword.Clear()
        If sender.Content = "Switch user" Then
            Me.activateSwitchUserMode() '//Calls the local function
        ElseIf sender.Content = "Show users" Then
            lblPasswordPlaceholder.Visibility = Windows.Visibility.Hidden
            lblUsernamePlaceholder.Visibility = Windows.Visibility.Hidden
            uProfiles.Visibility = Windows.Visibility.Visible
            txtPassword.Visibility = Windows.Visibility.Hidden
            txtUsername.Visibility = Windows.Visibility.Hidden
            btnLogin.Visibility = Windows.Visibility.Hidden
            loginMode = "DefaultMode"
            capsLOCK()
            sender.Content = "Switch user"
        End If
    End Sub

    Private Sub btnAdvanceSettings_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdvanceSettings.Click
        frmSettings.Show()
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As PasswordBox, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtPassword.KeyDown
        If e.Key = 6 Then
            btnLogin.Focus()

        Else
            If sender.Password = "" Then
                lblPasswordPlaceholder.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub txtUsername_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtUsername.KeyDown
        If e.Key = 6 Then
            txtPassword.Focus()
        Else
            If txtUsername.Text = "" Then
                lblUsernamePlaceholder.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub txtUsername_LostFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles txtUsername.LostFocus
        If txtUsername.Text = "" Then
            lblUsernamePlaceholder.Visibility = Windows.Visibility.Visible
        Else
            lblUsernamePlaceholder.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub txtPassword_LostFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles txtPassword.LostFocus
        If txtPassword.Password = "" AndAlso txtPassword.IsVisible Then
            lblPasswordPlaceholder.Visibility = Windows.Visibility.Visible
        Else
            lblPasswordPlaceholder.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub GetUserCode()
        connectionStaet()
        Try
            strSQL = "SELECT  [USERHED_USERCODE] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_TITLE]='" & Trim(txtUsername.Text) & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            selectedUserName = dbConnections.sqlCommand.ExecuteScalar

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub

End Class
