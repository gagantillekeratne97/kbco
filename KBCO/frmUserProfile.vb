Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class frmUserProfile
    Private Md5obj As New MD5
    Private errorevent As String
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
    Private UserpasswordSave As String
    '//Active form perform btn click case 
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If Save() Then FormClear()
            Case "Edit"
                'Me.FormEdit()
            Case "Delete"
                'If delete() Then FormClear()
            Case "Search"
                'SendKeys.Send("{F2}")
            Case "Print"
                'showCrystalReport()
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
    Private Function Save()
        Save = False
        Dim conf = MessageBox.Show("Do you want to change your password?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            connectionStaet()
            Try
                errorevent = "Save"
                strSQL = "UPDATE TBLU_USERHED SET  USERHED_PASSWORD =@USERHED_PASSWORD, USERHED_USERPC =@USERHED_USERPC, USERHED_PICTURE =@USERHED_PICTURE,  USERHED_EMAIL=@USERHED_EMAIL , USERHED_MOBILE=@USERHED_MOBILE  WHERE USERHED_USERCODE = '" & Trim(userSession) & "'"
                Dim PWD As String = Md5obj.EncryptPassword(Trim(txtPwd.Text))
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_PASSWORD", PWD)
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERPC", Trim(PCname))

                If Trim(txtEmail.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_EMAIL", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_EMAIL", Trim(txtEmail.Text))
                End If

                If Trim(txtMobile.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_MOBILE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_MOBILE", Trim(txtMobile.Text))
                End If


                If pbUserPicture.BackgroundImage Is Nothing Then
                    dbConnections.sqlCommand.Parameters.Add("@USERHED_PICTURE", SqlDbType.Image).Value = DBNull.Value
                Else
                    'Image converting code
                    Dim ms As New IO.MemoryStream
                    pbUserPicture.BackgroundImage.Save(ms, Imaging.ImageFormat.Jpeg)
                    Dim bytes() As Byte = ms.GetBuffer()
                    dbConnections.sqlCommand.Parameters.Add(New SqlParameter("@USERHED_PICTURE", SqlDbType.Image)).Value = bytes
                End If

                If dbConnections.sqlCommand.ExecuteNonQuery() Then Save = True Else Save = False

                If Save = True Then
                    MessageBox.Show("Update Successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorevent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                connectionClose()
            End Try
        End If
    End Function

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"

    Private Sub frmUserProfile_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))

    End Sub
    Private Sub frmUserProfile_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmUserProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblUserID.Text = userSession
        lblUsername.Text = userName
        txtEmail.Text = UserEmailAddress
        txtMobile.Text = UserMobileNo

        FormClear()
        UserImage()
        'globalFunctions.globalButtonActivation(True, True, False, False, False, False)

    End Sub

    Private Sub frmUserProfile_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmUserProfile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmUserProfile_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
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

    Private Sub UserImage()
        errorevent = "get user image"
        connectionStaet()
        Try
            strSQL = "SELECT  [USERHED_PASSWORD] ,[USERHED_PICTURE] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] =@USERHED_USERCODE"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(userSession))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                UserpasswordSave = dbConnections.dReader("USERHED_PASSWORD")
                If IsDBNull(dbConnections.dReader("USERHED_PICTURE")) Then
                Else
                    Dim x() As Byte = dbConnections.dReader("USERHED_PICTURE")
                    Dim mStream As MemoryStream = New MemoryStream(x)
                    Dim img As Image = Image.FromStream(mStream)
                    pbUserPicture.BackgroundImage = img
                    pbUserPicture.BackgroundImageLayout = ImageLayout.Zoom
                End If
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorevent, userSession, userName, DateTime.Now, ex.Message)
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
    Private Sub FormClear()
        UserImage()
        txtEOldPassword.Text = ""
        txtPwd.Text = ""
        txtRePwd.Text = ""
        txtEOldPassword.Focus()
    End Sub
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtPwd) = False Then
            Exit Function
        End If
        If Trim(txtPwd.Text) = Trim(txtRePwd.Text) = False Then
            txtPwd.Text = ""
            txtRePwd.Text = ""
            txtPwd.Focus()
            Exit Function
        End If

        If Not Trim(txtEOldPassword.Text) = Md5obj.DecryptPassword(UserpasswordSave) Then
            txtEOldPassword.Focus()
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function
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
    Private Sub btnCamara_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCamara.Click
        globalVariables.CamaraFormName = Me.Text
        frmCamera.MdiParent = frmMDImain
        frmCamera.Show()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Me.Save()
    End Sub

    Private Sub btnBrows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrows.Click
        Try
            'Set the Filter.
            OpenFileDialog1.Filter = "  All Files|*.*"
            'Clear the file name
            OpenFileDialog1.FileName = ""
            'Show it
            If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                'Get the image name
                img = OpenFileDialog1.FileName

                'Create a new Bitmap and display it
                pbUserPicture.BackgroundImage = System.Drawing.Bitmap.FromFile(img)
                pbUserPicture.BackgroundImageLayout = ImageLayout.Zoom
            End If
        Catch ex As Exception
            MessageBox.Show("Please select a image file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
#End Region




 
End Class