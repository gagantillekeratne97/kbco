<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddUser
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUser_Type = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblPictures = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtReTypePassword = New System.Windows.Forms.TextBox()
        Me.cbActiveUser = New System.Windows.Forms.CheckBox()
        Me.pbUserImage = New System.Windows.Forms.PictureBox()
        Me.btnBrows = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtUser_ID = New System.Windows.Forms.TextBox()
        Me.btn_Camara = New System.Windows.Forms.Button()
        Me.lblUserPosition = New System.Windows.Forms.Label()
        Me.lblNextUserID = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.cmbUserGroup = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbShowPwd = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbAssignToPC = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblUserPCName = New System.Windows.Forms.Label()
        Me.txtPCName = New System.Windows.Forms.TextBox()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.cbMachineTracerLogin = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dgCompanies = New System.Windows.Forms.DataGridView()
        Me.CHECK = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.COM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COM_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.pbUserImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Name"
        '
        'lblUser_Type
        '
        Me.lblUser_Type.AutoSize = True
        Me.lblUser_Type.Location = New System.Drawing.Point(14, 15)
        Me.lblUser_Type.Name = "lblUser_Type"
        Me.lblUser_Type.Size = New System.Drawing.Size(43, 13)
        Me.lblUser_Type.TabIndex = 1
        Me.lblUser_Type.Text = "User ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Password"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 171)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "User Active State"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 119)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Re Type Password"
        '
        'lblPictures
        '
        Me.lblPictures.AutoSize = True
        Me.lblPictures.Location = New System.Drawing.Point(620, 80)
        Me.lblPictures.Name = "lblPictures"
        Me.lblPictures.Size = New System.Drawing.Size(40, 13)
        Me.lblPictures.TabIndex = 5
        Me.lblPictures.Text = "Picture"
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Location = New System.Drawing.Point(117, 38)
        Me.txtUserName.MaxLength = 40
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(383, 20)
        Me.txtUserName.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Location = New System.Drawing.Point(117, 90)
        Me.txtPassword.MaxLength = 40
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(383, 20)
        Me.txtPassword.TabIndex = 3
        '
        'txtReTypePassword
        '
        Me.txtReTypePassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReTypePassword.Location = New System.Drawing.Point(117, 116)
        Me.txtReTypePassword.MaxLength = 40
        Me.txtReTypePassword.Name = "txtReTypePassword"
        Me.txtReTypePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtReTypePassword.Size = New System.Drawing.Size(383, 20)
        Me.txtReTypePassword.TabIndex = 4
        '
        'cbActiveUser
        '
        Me.cbActiveUser.AutoSize = True
        Me.cbActiveUser.Checked = True
        Me.cbActiveUser.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActiveUser.Location = New System.Drawing.Point(117, 169)
        Me.cbActiveUser.Name = "cbActiveUser"
        Me.cbActiveUser.Size = New System.Drawing.Size(56, 17)
        Me.cbActiveUser.TabIndex = 6
        Me.cbActiveUser.Text = "Active"
        Me.cbActiveUser.UseVisualStyleBackColor = True
        '
        'pbUserImage
        '
        Me.pbUserImage.BackColor = System.Drawing.SystemColors.Control
        Me.pbUserImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbUserImage.Location = New System.Drawing.Point(522, 9)
        Me.pbUserImage.Name = "pbUserImage"
        Me.pbUserImage.Size = New System.Drawing.Size(229, 163)
        Me.pbUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbUserImage.TabIndex = 9
        Me.pbUserImage.TabStop = False
        '
        'btnBrows
        '
        Me.btnBrows.Location = New System.Drawing.Point(659, 181)
        Me.btnBrows.Name = "btnBrows"
        Me.btnBrows.Size = New System.Drawing.Size(92, 23)
        Me.btnBrows.TabIndex = 9
        Me.btnBrows.Text = "Browse"
        Me.btnBrows.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'txtUser_ID
        '
        Me.txtUser_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUser_ID.Location = New System.Drawing.Point(117, 12)
        Me.txtUser_ID.Name = "txtUser_ID"
        Me.txtUser_ID.Size = New System.Drawing.Size(100, 20)
        Me.txtUser_ID.TabIndex = 0
        '
        'btn_Camara
        '
        Me.btn_Camara.Location = New System.Drawing.Point(522, 181)
        Me.btn_Camara.Name = "btn_Camara"
        Me.btn_Camara.Size = New System.Drawing.Size(92, 23)
        Me.btn_Camara.TabIndex = 8
        Me.btn_Camara.Text = "Camera"
        Me.btn_Camara.UseVisualStyleBackColor = True
        '
        'lblUserPosition
        '
        Me.lblUserPosition.AutoSize = True
        Me.lblUserPosition.Location = New System.Drawing.Point(14, 67)
        Me.lblUserPosition.Name = "lblUserPosition"
        Me.lblUserPosition.Size = New System.Drawing.Size(68, 13)
        Me.lblUserPosition.TabIndex = 12
        Me.lblUserPosition.Text = "User position"
        '
        'lblNextUserID
        '
        Me.lblNextUserID.AutoSize = True
        Me.lblNextUserID.Location = New System.Drawing.Point(265, 15)
        Me.lblNextUserID.Name = "lblNextUserID"
        Me.lblNextUserID.Size = New System.Drawing.Size(117, 13)
        Me.lblNextUserID.TabIndex = 13
        Me.lblNextUserID.Text = "Next available user ID -"
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblID.Location = New System.Drawing.Point(382, 15)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(18, 13)
        Me.lblID.TabIndex = 14
        Me.lblID.Text = "ID"
        '
        'cmbUserGroup
        '
        Me.cmbUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUserGroup.FormattingEnabled = True
        Me.cmbUserGroup.Location = New System.Drawing.Point(117, 63)
        Me.cmbUserGroup.Name = "cmbUserGroup"
        Me.cmbUserGroup.Size = New System.Drawing.Size(121, 21)
        Me.cmbUserGroup.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(219, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "F2"
        '
        'cbShowPwd
        '
        Me.cbShowPwd.AutoSize = True
        Me.cbShowPwd.Enabled = False
        Me.cbShowPwd.Location = New System.Drawing.Point(355, 67)
        Me.cbShowPwd.Name = "cbShowPwd"
        Me.cbShowPwd.Size = New System.Drawing.Size(48, 17)
        Me.cbShowPwd.TabIndex = 16
        Me.cbShowPwd.Text = "Hide"
        Me.cbShowPwd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(265, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Show password"
        '
        'cbAssignToPC
        '
        Me.cbAssignToPC.AutoSize = True
        Me.cbAssignToPC.Location = New System.Drawing.Point(117, 193)
        Me.cbAssignToPC.Name = "cbAssignToPC"
        Me.cbAssignToPC.Size = New System.Drawing.Size(44, 17)
        Me.cbAssignToPC.TabIndex = 7
        Me.cbAssignToPC.Text = "Yes"
        Me.cbAssignToPC.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 194)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Assign to a PC"
        '
        'lblUserPCName
        '
        Me.lblUserPCName.AutoSize = True
        Me.lblUserPCName.Location = New System.Drawing.Point(14, 145)
        Me.lblUserPCName.Name = "lblUserPCName"
        Me.lblUserPCName.Size = New System.Drawing.Size(77, 13)
        Me.lblUserPCName.TabIndex = 20
        Me.lblUserPCName.Text = "User PC Name"
        '
        'txtPCName
        '
        Me.txtPCName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPCName.Location = New System.Drawing.Point(117, 142)
        Me.txtPCName.MaxLength = 50
        Me.txtPCName.Name = "txtPCName"
        Me.txtPCName.Size = New System.Drawing.Size(383, 20)
        Me.txtPCName.TabIndex = 5
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Location = New System.Drawing.Point(14, 222)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(55, 13)
        Me.lblMobileNo.TabIndex = 21
        Me.lblMobileNo.Text = "Mobile No"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(14, 248)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(35, 13)
        Me.lblEmail.TabIndex = 21
        Me.lblEmail.Text = "E-mail"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileNo.Location = New System.Drawing.Point(117, 219)
        Me.txtMobileNo.MaxLength = 20
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(383, 20)
        Me.txtMobileNo.TabIndex = 22
        '
        'txtEmail
        '
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Location = New System.Drawing.Point(117, 245)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(383, 20)
        Me.txtEmail.TabIndex = 22
        '
        'cbMachineTracerLogin
        '
        Me.cbMachineTracerLogin.AutoSize = True
        Me.cbMachineTracerLogin.Location = New System.Drawing.Point(385, 193)
        Me.cbMachineTracerLogin.Name = "cbMachineTracerLogin"
        Me.cbMachineTracerLogin.Size = New System.Drawing.Size(40, 17)
        Me.cbMachineTracerLogin.TabIndex = 23
        Me.cbMachineTracerLogin.Text = "No"
        Me.cbMachineTracerLogin.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(189, 194)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(188, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Assign to Machine Tracer system login"
        '
        'dgCompanies
        '
        Me.dgCompanies.AllowUserToAddRows = False
        Me.dgCompanies.AllowUserToDeleteRows = False
        Me.dgCompanies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCompanies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CHECK, Me.COM_ID, Me.COM_NAME})
        Me.dgCompanies.Location = New System.Drawing.Point(117, 281)
        Me.dgCompanies.Name = "dgCompanies"
        Me.dgCompanies.Size = New System.Drawing.Size(579, 150)
        Me.dgCompanies.TabIndex = 25
        '
        'CHECK
        '
        Me.CHECK.HeaderText = "Check"
        Me.CHECK.Name = "CHECK"
        Me.CHECK.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CHECK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.CHECK.Width = 80
        '
        'COM_ID
        '
        Me.COM_ID.HeaderText = "ID"
        Me.COM_ID.Name = "COM_ID"
        Me.COM_ID.Width = 50
        '
        'COM_NAME
        '
        Me.COM_NAME.HeaderText = "Company Name"
        Me.COM_NAME.Name = "COM_NAME"
        Me.COM_NAME.Width = 400
        '
        'frmAddUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(762, 455)
        Me.Controls.Add(Me.dgCompanies)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cbMachineTracerLogin)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtMobileNo)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.txtPCName)
        Me.Controls.Add(Me.lblUserPCName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbAssignToPC)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbShowPwd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbUserGroup)
        Me.Controls.Add(Me.lblID)
        Me.Controls.Add(Me.lblNextUserID)
        Me.Controls.Add(Me.lblUserPosition)
        Me.Controls.Add(Me.btn_Camara)
        Me.Controls.Add(Me.txtUser_ID)
        Me.Controls.Add(Me.btnBrows)
        Me.Controls.Add(Me.cbActiveUser)
        Me.Controls.Add(Me.txtReTypePassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.lblPictures)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblUser_Type)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbUserImage)
        Me.KeyPreview = True
        Me.Name = "frmAddUser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00001"
        Me.Text = "Add Login Users"
        CType(Me.pbUserImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUser_Type As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPictures As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtReTypePassword As System.Windows.Forms.TextBox
    Friend WithEvents cbActiveUser As System.Windows.Forms.CheckBox
    Friend WithEvents pbUserImage As System.Windows.Forms.PictureBox
    Friend WithEvents btnBrows As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtUser_ID As System.Windows.Forms.TextBox
    Friend WithEvents btn_Camara As System.Windows.Forms.Button
    Friend WithEvents lblUserPosition As System.Windows.Forms.Label
    Friend WithEvents lblNextUserID As System.Windows.Forms.Label
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents cmbUserGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbShowPwd As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbAssignToPC As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblUserPCName As System.Windows.Forms.Label
    Friend WithEvents txtPCName As System.Windows.Forms.TextBox
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents cbMachineTracerLogin As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dgCompanies As System.Windows.Forms.DataGridView
    Friend WithEvents CHECK As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents COM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COM_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
