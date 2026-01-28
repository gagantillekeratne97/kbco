<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysConnectedUsers
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
        Me.dgOnlineUsers = New System.Windows.Forms.DataGridView()
        Me.PCNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetLibrary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IPAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblConnectedUsers = New System.Windows.Forms.Label()
        Me.lblUserCount = New System.Windows.Forms.Label()
        CType(Me.dgOnlineUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgOnlineUsers
        '
        Me.dgOnlineUsers.AllowUserToAddRows = False
        Me.dgOnlineUsers.AllowUserToDeleteRows = False
        Me.dgOnlineUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgOnlineUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgOnlineUsers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PCNAME, Me.NetLibrary, Me.NetAddress, Me.IPAddress})
        Me.dgOnlineUsers.Location = New System.Drawing.Point(0, 41)
        Me.dgOnlineUsers.Name = "dgOnlineUsers"
        Me.dgOnlineUsers.ReadOnly = True
        Me.dgOnlineUsers.Size = New System.Drawing.Size(648, 445)
        Me.dgOnlineUsers.TabIndex = 0
        '
        'PCNAME
        '
        Me.PCNAME.HeaderText = "PC-NAME"
        Me.PCNAME.Name = "PCNAME"
        Me.PCNAME.ReadOnly = True
        Me.PCNAME.Width = 150
        '
        'NetLibrary
        '
        Me.NetLibrary.HeaderText = "Net-Library"
        Me.NetLibrary.Name = "NetLibrary"
        Me.NetLibrary.ReadOnly = True
        Me.NetLibrary.Width = 150
        '
        'NetAddress
        '
        Me.NetAddress.HeaderText = "Net Address"
        Me.NetAddress.Name = "NetAddress"
        Me.NetAddress.ReadOnly = True
        Me.NetAddress.Width = 150
        '
        'IPAddress
        '
        Me.IPAddress.HeaderText = "IP-Address"
        Me.IPAddress.Name = "IPAddress"
        Me.IPAddress.ReadOnly = True
        Me.IPAddress.Width = 150
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(12, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblConnectedUsers
        '
        Me.lblConnectedUsers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConnectedUsers.AutoSize = True
        Me.lblConnectedUsers.Location = New System.Drawing.Point(457, 17)
        Me.lblConnectedUsers.Name = "lblConnectedUsers"
        Me.lblConnectedUsers.Size = New System.Drawing.Size(121, 13)
        Me.lblConnectedUsers.TabIndex = 2
        Me.lblConnectedUsers.Text = "Connected User Count :"
        '
        'lblUserCount
        '
        Me.lblUserCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUserCount.AutoSize = True
        Me.lblUserCount.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lblUserCount.Location = New System.Drawing.Point(584, 17)
        Me.lblUserCount.Name = "lblUserCount"
        Me.lblUserCount.Size = New System.Drawing.Size(57, 13)
        Me.lblUserCount.TabIndex = 3
        Me.lblUserCount.Text = "UserCount"
        '
        'frmSysConnectedUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 486)
        Me.Controls.Add(Me.lblUserCount)
        Me.Controls.Add(Me.lblConnectedUsers)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgOnlineUsers)
        Me.Name = "frmSysConnectedUsers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00008"
        Me.Text = "Online Users"
        CType(Me.dgOnlineUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgOnlineUsers As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents PCNAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetLibrary As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NetAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IPAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblConnectedUsers As System.Windows.Forms.Label
    Friend WithEvents lblUserCount As System.Windows.Forms.Label
End Class
