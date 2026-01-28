<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserPermission
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
        Me.lblUserID = New System.Windows.Forms.Label()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.dgUserPermission = New System.Windows.Forms.DataGridView()
        Me.MenuTag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Access = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Create = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Edit = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Delete = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cbAccess = New System.Windows.Forms.CheckBox()
        Me.cbCreate = New System.Windows.Forms.CheckBox()
        Me.cbEdit = New System.Windows.Forms.CheckBox()
        Me.cbDelete = New System.Windows.Forms.CheckBox()
        Me.cmbUserGroup = New System.Windows.Forms.ComboBox()
        Me.lblUserGroup = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtComID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Location = New System.Drawing.Point(15, 15)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(43, 13)
        Me.lblUserID.TabIndex = 0
        Me.lblUserID.Text = "User ID"
        '
        'txtUserID
        '
        Me.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserID.Location = New System.Drawing.Point(83, 12)
        Me.txtUserID.MaxLength = 4
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(100, 20)
        Me.txtUserID.TabIndex = 1
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(15, 44)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(60, 13)
        Me.lblUserName.TabIndex = 2
        Me.lblUserName.Text = "User Name"
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Enabled = False
        Me.txtUserName.Location = New System.Drawing.Point(83, 41)
        Me.txtUserName.MaxLength = 20
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(392, 20)
        Me.txtUserName.TabIndex = 2
        '
        'dgUserPermission
        '
        Me.dgUserPermission.AllowUserToAddRows = False
        Me.dgUserPermission.AllowUserToDeleteRows = False
        Me.dgUserPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgUserPermission.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenuTag, Me.MenuName, Me.Access, Me.Create, Me.Edit, Me.Delete})
        Me.dgUserPermission.Location = New System.Drawing.Point(0, 67)
        Me.dgUserPermission.Name = "dgUserPermission"
        Me.dgUserPermission.Size = New System.Drawing.Size(760, 535)
        Me.dgUserPermission.TabIndex = 3
        '
        'MenuTag
        '
        Me.MenuTag.HeaderText = "MenuTag"
        Me.MenuTag.Name = "MenuTag"
        Me.MenuTag.Width = 125
        '
        'MenuName
        '
        Me.MenuName.HeaderText = "MenuName"
        Me.MenuName.Name = "MenuName"
        Me.MenuName.Width = 327
        '
        'Access
        '
        Me.Access.HeaderText = "Access"
        Me.Access.Name = "Access"
        Me.Access.Width = 60
        '
        'Create
        '
        Me.Create.HeaderText = "Create"
        Me.Create.Name = "Create"
        Me.Create.Width = 60
        '
        'Edit
        '
        Me.Edit.HeaderText = "Edit"
        Me.Edit.Name = "Edit"
        Me.Edit.Width = 60
        '
        'Delete
        '
        Me.Delete.HeaderText = "Delete"
        Me.Delete.Name = "Delete"
        Me.Delete.Width = 60
        '
        'cbAccess
        '
        Me.cbAccess.AutoSize = True
        Me.cbAccess.Location = New System.Drawing.Point(500, 44)
        Me.cbAccess.Name = "cbAccess"
        Me.cbAccess.Size = New System.Drawing.Size(61, 17)
        Me.cbAccess.TabIndex = 4
        Me.cbAccess.Text = "Access"
        Me.cbAccess.UseVisualStyleBackColor = True
        '
        'cbCreate
        '
        Me.cbCreate.AutoSize = True
        Me.cbCreate.Location = New System.Drawing.Point(560, 44)
        Me.cbCreate.Name = "cbCreate"
        Me.cbCreate.Size = New System.Drawing.Size(57, 17)
        Me.cbCreate.TabIndex = 5
        Me.cbCreate.Text = "Create"
        Me.cbCreate.UseVisualStyleBackColor = True
        '
        'cbEdit
        '
        Me.cbEdit.AutoSize = True
        Me.cbEdit.Location = New System.Drawing.Point(622, 44)
        Me.cbEdit.Name = "cbEdit"
        Me.cbEdit.Size = New System.Drawing.Size(44, 17)
        Me.cbEdit.TabIndex = 6
        Me.cbEdit.Text = "Edit"
        Me.cbEdit.UseVisualStyleBackColor = True
        '
        'cbDelete
        '
        Me.cbDelete.AutoSize = True
        Me.cbDelete.Location = New System.Drawing.Point(679, 44)
        Me.cbDelete.Name = "cbDelete"
        Me.cbDelete.Size = New System.Drawing.Size(57, 17)
        Me.cbDelete.TabIndex = 7
        Me.cbDelete.Text = "Delete"
        Me.cbDelete.UseVisualStyleBackColor = True
        '
        'cmbUserGroup
        '
        Me.cmbUserGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUserGroup.FormattingEnabled = True
        Me.cmbUserGroup.Location = New System.Drawing.Point(354, 11)
        Me.cmbUserGroup.Name = "cmbUserGroup"
        Me.cmbUserGroup.Size = New System.Drawing.Size(121, 21)
        Me.cmbUserGroup.TabIndex = 8
        '
        'lblUserGroup
        '
        Me.lblUserGroup.AutoSize = True
        Me.lblUserGroup.Location = New System.Drawing.Point(285, 14)
        Me.lblUserGroup.Name = "lblUserGroup"
        Me.lblUserGroup.Size = New System.Drawing.Size(61, 13)
        Me.lblUserGroup.TabIndex = 9
        Me.lblUserGroup.Text = "User Group"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(185, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "F2"
        '
        'txtComID
        '
        Me.txtComID.Location = New System.Drawing.Point(640, 6)
        Me.txtComID.Name = "txtComID"
        Me.txtComID.Size = New System.Drawing.Size(100, 20)
        Me.txtComID.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(573, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Company ID"
        '
        'frmUserPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 602)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtComID)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblUserGroup)
        Me.Controls.Add(Me.cmbUserGroup)
        Me.Controls.Add(Me.cbDelete)
        Me.Controls.Add(Me.cbEdit)
        Me.Controls.Add(Me.cbCreate)
        Me.Controls.Add(Me.cbAccess)
        Me.Controls.Add(Me.dgUserPermission)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.lblUserID)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmUserPermission"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00005"
        Me.Text = "User Permission"
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents dgUserPermission As System.Windows.Forms.DataGridView
    Friend WithEvents MenuTag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenuName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Access As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Create As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cbAccess As System.Windows.Forms.CheckBox
    Friend WithEvents cbCreate As System.Windows.Forms.CheckBox
    Friend WithEvents cbEdit As System.Windows.Forms.CheckBox
    Friend WithEvents cbDelete As System.Windows.Forms.CheckBox
    Friend WithEvents cmbUserGroup As System.Windows.Forms.ComboBox
    Friend WithEvents lblUserGroup As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtComID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
