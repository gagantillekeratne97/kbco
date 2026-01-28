<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageMenuItems
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
        Me.lblMenuTag = New System.Windows.Forms.Label()
        Me.lblMenuName = New System.Windows.Forms.Label()
        Me.lblMenuRights = New System.Windows.Forms.Label()
        Me.lblFormName = New System.Windows.Forms.Label()
        Me.lblMenuParent = New System.Windows.Forms.Label()
        Me.lblMenuLevel = New System.Windows.Forms.Label()
        Me.txtMenuTag = New System.Windows.Forms.TextBox()
        Me.txtMenuName = New System.Windows.Forms.TextBox()
        Me.txtMenuRights = New System.Windows.Forms.TextBox()
        Me.txtFormName = New System.Windows.Forms.TextBox()
        Me.txtMenuParentCode = New System.Windows.Forms.TextBox()
        Me.txtMenuLevel = New System.Windows.Forms.TextBox()
        Me.Lable1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblMenuTag
        '
        Me.lblMenuTag.AutoSize = True
        Me.lblMenuTag.Location = New System.Drawing.Point(11, 15)
        Me.lblMenuTag.Name = "lblMenuTag"
        Me.lblMenuTag.Size = New System.Drawing.Size(53, 13)
        Me.lblMenuTag.TabIndex = 0
        Me.lblMenuTag.Text = "MenuTag"
        '
        'lblMenuName
        '
        Me.lblMenuName.AutoSize = True
        Me.lblMenuName.Location = New System.Drawing.Point(11, 40)
        Me.lblMenuName.Name = "lblMenuName"
        Me.lblMenuName.Size = New System.Drawing.Size(65, 13)
        Me.lblMenuName.TabIndex = 0
        Me.lblMenuName.Text = "Menu Name"
        '
        'lblMenuRights
        '
        Me.lblMenuRights.AutoSize = True
        Me.lblMenuRights.Location = New System.Drawing.Point(11, 66)
        Me.lblMenuRights.Name = "lblMenuRights"
        Me.lblMenuRights.Size = New System.Drawing.Size(67, 13)
        Me.lblMenuRights.TabIndex = 0
        Me.lblMenuRights.Text = "Menu Rights"
        '
        'lblFormName
        '
        Me.lblFormName.AutoSize = True
        Me.lblFormName.Location = New System.Drawing.Point(11, 92)
        Me.lblFormName.Name = "lblFormName"
        Me.lblFormName.Size = New System.Drawing.Size(61, 13)
        Me.lblFormName.TabIndex = 0
        Me.lblFormName.Text = "Form Name"
        '
        'lblMenuParent
        '
        Me.lblMenuParent.AutoSize = True
        Me.lblMenuParent.Location = New System.Drawing.Point(11, 118)
        Me.lblMenuParent.Name = "lblMenuParent"
        Me.lblMenuParent.Size = New System.Drawing.Size(96, 13)
        Me.lblMenuParent.TabIndex = 0
        Me.lblMenuParent.Text = "Menu Parent Code"
        '
        'lblMenuLevel
        '
        Me.lblMenuLevel.AutoSize = True
        Me.lblMenuLevel.Location = New System.Drawing.Point(11, 144)
        Me.lblMenuLevel.Name = "lblMenuLevel"
        Me.lblMenuLevel.Size = New System.Drawing.Size(63, 13)
        Me.lblMenuLevel.TabIndex = 1
        Me.lblMenuLevel.Text = "Menu Level"
        '
        'txtMenuTag
        '
        Me.txtMenuTag.Location = New System.Drawing.Point(115, 12)
        Me.txtMenuTag.MaxLength = 6
        Me.txtMenuTag.Name = "txtMenuTag"
        Me.txtMenuTag.Size = New System.Drawing.Size(124, 20)
        Me.txtMenuTag.TabIndex = 0
        '
        'txtMenuName
        '
        Me.txtMenuName.Location = New System.Drawing.Point(115, 38)
        Me.txtMenuName.MaxLength = 30
        Me.txtMenuName.Name = "txtMenuName"
        Me.txtMenuName.Size = New System.Drawing.Size(339, 20)
        Me.txtMenuName.TabIndex = 1
        '
        'txtMenuRights
        '
        Me.txtMenuRights.Location = New System.Drawing.Point(115, 64)
        Me.txtMenuRights.MaxLength = 4
        Me.txtMenuRights.Name = "txtMenuRights"
        Me.txtMenuRights.Size = New System.Drawing.Size(124, 20)
        Me.txtMenuRights.TabIndex = 2
        '
        'txtFormName
        '
        Me.txtFormName.Location = New System.Drawing.Point(115, 90)
        Me.txtFormName.MaxLength = 30
        Me.txtFormName.Name = "txtFormName"
        Me.txtFormName.Size = New System.Drawing.Size(339, 20)
        Me.txtFormName.TabIndex = 3
        '
        'txtMenuParentCode
        '
        Me.txtMenuParentCode.Location = New System.Drawing.Point(115, 116)
        Me.txtMenuParentCode.MaxLength = 6
        Me.txtMenuParentCode.Name = "txtMenuParentCode"
        Me.txtMenuParentCode.Size = New System.Drawing.Size(124, 20)
        Me.txtMenuParentCode.TabIndex = 4
        '
        'txtMenuLevel
        '
        Me.txtMenuLevel.Location = New System.Drawing.Point(115, 142)
        Me.txtMenuLevel.MaxLength = 5
        Me.txtMenuLevel.Name = "txtMenuLevel"
        Me.txtMenuLevel.Size = New System.Drawing.Size(124, 20)
        Me.txtMenuLevel.TabIndex = 5
        '
        'Lable1
        '
        Me.Lable1.AutoSize = True
        Me.Lable1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Lable1.Location = New System.Drawing.Point(245, 15)
        Me.Lable1.Name = "Lable1"
        Me.Lable1.Size = New System.Drawing.Size(19, 13)
        Me.Lable1.TabIndex = 3
        Me.Lable1.Text = "F2"
        '
        'frmManageMenuItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 177)
        Me.Controls.Add(Me.Lable1)
        Me.Controls.Add(Me.txtMenuLevel)
        Me.Controls.Add(Me.txtMenuParentCode)
        Me.Controls.Add(Me.txtFormName)
        Me.Controls.Add(Me.txtMenuRights)
        Me.Controls.Add(Me.txtMenuName)
        Me.Controls.Add(Me.txtMenuTag)
        Me.Controls.Add(Me.lblMenuLevel)
        Me.Controls.Add(Me.lblMenuParent)
        Me.Controls.Add(Me.lblFormName)
        Me.Controls.Add(Me.lblMenuRights)
        Me.Controls.Add(Me.lblMenuName)
        Me.Controls.Add(Me.lblMenuTag)
        Me.KeyPreview = True
        Me.Name = "frmManageMenuItems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Manage Menu Items"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblMenuTag As System.Windows.Forms.Label
    Friend WithEvents lblMenuName As System.Windows.Forms.Label
    Friend WithEvents lblMenuRights As System.Windows.Forms.Label
    Friend WithEvents lblFormName As System.Windows.Forms.Label
    Friend WithEvents lblMenuParent As System.Windows.Forms.Label
    Friend WithEvents lblMenuLevel As System.Windows.Forms.Label
    Friend WithEvents txtMenuTag As System.Windows.Forms.TextBox
    Friend WithEvents txtMenuName As System.Windows.Forms.TextBox
    Friend WithEvents txtMenuRights As System.Windows.Forms.TextBox
    Friend WithEvents txtFormName As System.Windows.Forms.TextBox
    Friend WithEvents txtMenuParentCode As System.Windows.Forms.TextBox
    Friend WithEvents txtMenuLevel As System.Windows.Forms.TextBox
    Friend WithEvents Lable1 As System.Windows.Forms.Label
End Class
