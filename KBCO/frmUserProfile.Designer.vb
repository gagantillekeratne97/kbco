<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserProfile
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserProfile))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pbUserPicture = New System.Windows.Forms.PictureBox()
        Me.btnCamara = New System.Windows.Forms.Button()
        Me.btnBrows = New System.Windows.Forms.Button()
        Me.txtPwd = New System.Windows.Forms.TextBox()
        Me.txtRePwd = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblOldPassword = New System.Windows.Forms.Label()
        Me.txtEOldPassword = New System.Windows.Forms.TextBox()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.lblEmailAddress = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        CType(Me.pbUserPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Re type password"
        '
        'pbUserPicture
        '
        Me.pbUserPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbUserPicture.Location = New System.Drawing.Point(594, 10)
        Me.pbUserPicture.Name = "pbUserPicture"
        Me.pbUserPicture.Size = New System.Drawing.Size(230, 184)
        Me.pbUserPicture.TabIndex = 3
        Me.pbUserPicture.TabStop = False
        '
        'btnCamara
        '
        Me.btnCamara.BackgroundImage = CType(resources.GetObject("btnCamara.BackgroundImage"), System.Drawing.Image)
        Me.btnCamara.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCamara.Location = New System.Drawing.Point(830, 6)
        Me.btnCamara.Name = "btnCamara"
        Me.btnCamara.Size = New System.Drawing.Size(54, 53)
        Me.btnCamara.TabIndex = 5
        Me.btnCamara.UseVisualStyleBackColor = True
        '
        'btnBrows
        '
        Me.btnBrows.BackgroundImage = CType(resources.GetObject("btnBrows.BackgroundImage"), System.Drawing.Image)
        Me.btnBrows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnBrows.Location = New System.Drawing.Point(830, 86)
        Me.btnBrows.Name = "btnBrows"
        Me.btnBrows.Size = New System.Drawing.Size(54, 53)
        Me.btnBrows.TabIndex = 6
        Me.btnBrows.UseVisualStyleBackColor = True
        '
        'txtPwd
        '
        Me.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPwd.Location = New System.Drawing.Point(113, 93)
        Me.txtPwd.MaxLength = 40
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwd.Size = New System.Drawing.Size(475, 20)
        Me.txtPwd.TabIndex = 1
        '
        'txtRePwd
        '
        Me.txtRePwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRePwd.Location = New System.Drawing.Point(113, 119)
        Me.txtRePwd.MaxLength = 40
        Me.txtRePwd.Name = "txtRePwd"
        Me.txtRePwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtRePwd.Size = New System.Drawing.Size(475, 20)
        Me.txtRePwd.TabIndex = 2
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(110, 40)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(55, 13)
        Me.lblUsername.TabIndex = 7
        Me.lblUsername.Text = "Username"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "User ID"
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Location = New System.Drawing.Point(110, 12)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(18, 13)
        Me.lblUserID.TabIndex = 7
        Me.lblUserID.Text = "ID"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblOldPassword
        '
        Me.lblOldPassword.AutoSize = True
        Me.lblOldPassword.Location = New System.Drawing.Point(12, 70)
        Me.lblOldPassword.Name = "lblOldPassword"
        Me.lblOldPassword.Size = New System.Drawing.Size(72, 13)
        Me.lblOldPassword.TabIndex = 8
        Me.lblOldPassword.Text = "Old Password"
        '
        'txtEOldPassword
        '
        Me.txtEOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEOldPassword.Location = New System.Drawing.Point(113, 67)
        Me.txtEOldPassword.MaxLength = 40
        Me.txtEOldPassword.Name = "txtEOldPassword"
        Me.txtEOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEOldPassword.Size = New System.Drawing.Size(475, 20)
        Me.txtEOldPassword.TabIndex = 0
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Location = New System.Drawing.Point(12, 175)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(55, 13)
        Me.lblMobileNo.TabIndex = 9
        Me.lblMobileNo.Text = "Mobile No"
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.AutoSize = True
        Me.lblEmailAddress.Location = New System.Drawing.Point(12, 149)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(32, 13)
        Me.lblEmailAddress.TabIndex = 9
        Me.lblEmailAddress.Text = "Email"
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Location = New System.Drawing.Point(113, 146)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(475, 20)
        Me.txtEmail.TabIndex = 3
        '
        'txtMobile
        '
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Location = New System.Drawing.Point(113, 172)
        Me.txtMobile.MaxLength = 50
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(475, 20)
        Me.txtMobile.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(836, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Camara"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(836, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Browse"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(489, 21)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(90, 32)
        Me.btnUpdate.TabIndex = 12
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'frmUserProfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(887, 222)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.lblEmailAddress)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.txtEOldPassword)
        Me.Controls.Add(Me.lblOldPassword)
        Me.Controls.Add(Me.lblUserID)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtRePwd)
        Me.Controls.Add(Me.txtPwd)
        Me.Controls.Add(Me.btnBrows)
        Me.Controls.Add(Me.btnCamara)
        Me.Controls.Add(Me.pbUserPicture)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmUserProfile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "USER PROFILE"
        CType(Me.pbUserPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pbUserPicture As System.Windows.Forms.PictureBox
    Friend WithEvents btnCamara As System.Windows.Forms.Button
    Friend WithEvents btnBrows As System.Windows.Forms.Button
    Friend WithEvents txtPwd As System.Windows.Forms.TextBox
    Friend WithEvents txtRePwd As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblOldPassword As System.Windows.Forms.Label
    Friend WithEvents txtEOldPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents lblEmailAddress As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
End Class
