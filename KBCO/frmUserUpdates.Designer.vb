<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserUpdates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserUpdates))
        Me.dgUserPermission = New System.Windows.Forms.DataGridView()
        Me.MenuTag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Access = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Create = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Edit = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Delete = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.pbImage = New System.Windows.Forms.PictureBox()
        Me.pgBar = New System.Windows.Forms.ProgressBar()
        Me.btnUpdateSystem = New System.Windows.Forms.Button()
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgUserPermission
        '
        Me.dgUserPermission.AllowUserToAddRows = False
        Me.dgUserPermission.AllowUserToDeleteRows = False
        Me.dgUserPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgUserPermission.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MenuTag, Me.MenuName, Me.Access, Me.Create, Me.Edit, Me.Delete})
        Me.dgUserPermission.Location = New System.Drawing.Point(3, 81)
        Me.dgUserPermission.Name = "dgUserPermission"
        Me.dgUserPermission.ReadOnly = True
        Me.dgUserPermission.Size = New System.Drawing.Size(626, 232)
        Me.dgUserPermission.TabIndex = 0
        Me.dgUserPermission.Visible = False
        '
        'MenuTag
        '
        Me.MenuTag.HeaderText = "MenuTag"
        Me.MenuTag.Name = "MenuTag"
        Me.MenuTag.ReadOnly = True
        '
        'MenuName
        '
        Me.MenuName.HeaderText = "MenuName"
        Me.MenuName.Name = "MenuName"
        Me.MenuName.ReadOnly = True
        '
        'Access
        '
        Me.Access.HeaderText = "Access"
        Me.Access.Name = "Access"
        Me.Access.ReadOnly = True
        '
        'Create
        '
        Me.Create.HeaderText = "Create"
        Me.Create.Name = "Create"
        Me.Create.ReadOnly = True
        '
        'Edit
        '
        Me.Edit.HeaderText = "Edit"
        Me.Edit.Name = "Edit"
        Me.Edit.ReadOnly = True
        '
        'Delete
        '
        Me.Delete.HeaderText = "Delete"
        Me.Delete.Name = "Delete"
        Me.Delete.ReadOnly = True
        '
        'pbImage
        '
        Me.pbImage.Image = CType(resources.GetObject("pbImage.Image"), System.Drawing.Image)
        Me.pbImage.Location = New System.Drawing.Point(565, 0)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(77, 65)
        Me.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbImage.TabIndex = 1
        Me.pbImage.TabStop = False
        '
        'pgBar
        '
        Me.pgBar.Location = New System.Drawing.Point(9, 36)
        Me.pgBar.Name = "pgBar"
        Me.pgBar.Size = New System.Drawing.Size(554, 23)
        Me.pgBar.TabIndex = 2
        '
        'btnUpdateSystem
        '
        Me.btnUpdateSystem.Location = New System.Drawing.Point(9, 7)
        Me.btnUpdateSystem.Name = "btnUpdateSystem"
        Me.btnUpdateSystem.Size = New System.Drawing.Size(109, 23)
        Me.btnUpdateSystem.TabIndex = 3
        Me.btnUpdateSystem.Text = "Update System"
        Me.btnUpdateSystem.UseVisualStyleBackColor = True
        '
        'frmUserUpdates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 67)
        Me.Controls.Add(Me.btnUpdateSystem)
        Me.Controls.Add(Me.pgBar)
        Me.Controls.Add(Me.pbImage)
        Me.Controls.Add(Me.dgUserPermission)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUserUpdates"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Updates"
        CType(Me.dgUserPermission, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgUserPermission As System.Windows.Forms.DataGridView
    Friend WithEvents pbImage As System.Windows.Forms.PictureBox
    Friend WithEvents MenuTag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenuName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Access As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Create As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Edit As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents pgBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnUpdateSystem As System.Windows.Forms.Button
End Class
