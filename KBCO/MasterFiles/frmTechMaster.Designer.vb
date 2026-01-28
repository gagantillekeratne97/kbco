<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTechMaster
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
        Me.cbActive = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTechArea = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTechMobile = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTechSageCode = New System.Windows.Forms.TextBox()
        Me.txtTechName = New System.Windows.Forms.TextBox()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Location = New System.Drawing.Point(109, 145)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(42, 17)
        Me.cbActive.TabIndex = 5
        Me.cbActive.Text = "NO"
        Me.cbActive.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Is Active"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Tech Area"
        '
        'txtTechArea
        '
        Me.txtTechArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechArea.Location = New System.Drawing.Point(109, 116)
        Me.txtTechArea.MaxLength = 50
        Me.txtTechArea.Name = "txtTechArea"
        Me.txtTechArea.Size = New System.Drawing.Size(166, 20)
        Me.txtTechArea.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Tech Mobile"
        '
        'txtTechMobile
        '
        Me.txtTechMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechMobile.Location = New System.Drawing.Point(109, 90)
        Me.txtTechMobile.MaxLength = 50
        Me.txtTechMobile.Name = "txtTechMobile"
        Me.txtTechMobile.Size = New System.Drawing.Size(166, 20)
        Me.txtTechMobile.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Tech Sage Code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(283, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "F2"
        '
        'txtTechSageCode
        '
        Me.txtTechSageCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechSageCode.Location = New System.Drawing.Point(109, 64)
        Me.txtTechSageCode.MaxLength = 50
        Me.txtTechSageCode.Name = "txtTechSageCode"
        Me.txtTechSageCode.Size = New System.Drawing.Size(166, 20)
        Me.txtTechSageCode.TabIndex = 2
        '
        'txtTechName
        '
        Me.txtTechName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechName.Location = New System.Drawing.Point(109, 38)
        Me.txtTechName.MaxLength = 50
        Me.txtTechName.Name = "txtTechName"
        Me.txtTechName.Size = New System.Drawing.Size(633, 20)
        Me.txtTechName.TabIndex = 1
        '
        'txtTechCode
        '
        Me.txtTechCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechCode.Location = New System.Drawing.Point(109, 12)
        Me.txtTechCode.MaxLength = 50
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(166, 20)
        Me.txtTechCode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Tech Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Tech Code"
        '
        'frmTechMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(747, 175)
        Me.Controls.Add(Me.cbActive)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTechArea)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtTechMobile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTechSageCode)
        Me.Controls.Add(Me.txtTechName)
        Me.Controls.Add(Me.txtTechCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmTechMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00004"
        Me.Text = "Technician Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTechArea As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTechMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTechSageCode As System.Windows.Forms.TextBox
    Friend WithEvents txtTechName As System.Windows.Forms.TextBox
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
