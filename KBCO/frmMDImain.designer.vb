<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMDImain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMDImain))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tslbl_Newkey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_Savekey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_Editkey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_Deletekey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_Printkey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslbl_Searchkey = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblServerName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.tsbtnNew = New System.Windows.Forms.ToolStripButton()
        Me.tsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnSearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnLogin = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnPermissionUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbtnUpdate = New System.Windows.Forms.ToolStripButton()
        Me.tslblNewUpdate = New System.Windows.Forms.ToolStripLabel()
        Me.systemtray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.mainmenuPenal = New System.Windows.Forms.Panel()
        Me.ElementHost1 = New System.Windows.Forms.Integration.ElementHost()
        Me.btnMainMenu = New System.Windows.Forms.Button()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.mainmenuPenal.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.BackColor = System.Drawing.Color.LightGray
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslbl_Newkey, Me.tslbl_Savekey, Me.tslbl_Editkey, Me.tslbl_Deletekey, Me.tslbl_Printkey, Me.tslbl_Searchkey, Me.tslblServerName})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 580)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip.Size = New System.Drawing.Size(1104, 26)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'tslbl_Newkey
        '
        Me.tslbl_Newkey.Name = "tslbl_Newkey"
        Me.tslbl_Newkey.Size = New System.Drawing.Size(64, 20)
        Me.tslbl_Newkey.Text = "New(F4)"
        '
        'tslbl_Savekey
        '
        Me.tslbl_Savekey.Name = "tslbl_Savekey"
        Me.tslbl_Savekey.Size = New System.Drawing.Size(65, 20)
        Me.tslbl_Savekey.Text = "Save(F9)"
        '
        'tslbl_Editkey
        '
        Me.tslbl_Editkey.Name = "tslbl_Editkey"
        Me.tslbl_Editkey.Size = New System.Drawing.Size(68, 20)
        Me.tslbl_Editkey.Text = "Edit(F10)"
        '
        'tslbl_Deletekey
        '
        Me.tslbl_Deletekey.Name = "tslbl_Deletekey"
        Me.tslbl_Deletekey.Size = New System.Drawing.Size(86, 20)
        Me.tslbl_Deletekey.Text = "Delete(F12)"
        '
        'tslbl_Printkey
        '
        Me.tslbl_Printkey.Name = "tslbl_Printkey"
        Me.tslbl_Printkey.Size = New System.Drawing.Size(72, 20)
        Me.tslbl_Printkey.Text = "Print(F11)"
        '
        'tslbl_Searchkey
        '
        Me.tslbl_Searchkey.Margin = New System.Windows.Forms.Padding(1, 3, 0, 2)
        Me.tslbl_Searchkey.Name = "tslbl_Searchkey"
        Me.tslbl_Searchkey.Size = New System.Drawing.Size(78, 21)
        Me.tslbl_Searchkey.Text = "Search(F2)"
        '
        'tslblServerName
        '
        Me.tslblServerName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.tslblServerName.Name = "tslblServerName"
        Me.tslblServerName.Size = New System.Drawing.Size(159, 20)
        Me.tslblServerName.Text = "ServerName\Database"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStrip
        '
        Me.ToolStrip.BackColor = System.Drawing.Color.RoyalBlue
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.tsbtnNew, Me.ToolStripSeparator2, Me.tsbtnSave, Me.ToolStripSeparator3, Me.tsbtnEdit, Me.ToolStripSeparator4, Me.tsbtnDelete, Me.ToolStripSeparator5, Me.tsbtnPrint, Me.ToolStripSeparator6, Me.tsbtnSearch, Me.ToolStripSeparator7, Me.tsbtnLogin, Me.ToolStripSeparator8, Me.tsbtnHelp, Me.ToolStripSeparator9, Me.tsbtnPermissionUpdate, Me.ToolStripSeparator10, Me.tsbtnUpdate, Me.tslblNewUpdate})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ToolStrip.Size = New System.Drawing.Size(1104, 39)
        Me.ToolStrip.TabIndex = 6
        Me.ToolStrip.Text = "ToolStrip"
        '
        'tsbtnNew
        '
        Me.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnNew.Image = CType(resources.GetObject("tsbtnNew.Image"), System.Drawing.Image)
        Me.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnNew.Name = "tsbtnNew"
        Me.tsbtnNew.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnNew.Text = "New (F4)"
        '
        'tsbtnSave
        '
        Me.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnSave.Image = CType(resources.GetObject("tsbtnSave.Image"), System.Drawing.Image)
        Me.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnSave.Name = "tsbtnSave"
        Me.tsbtnSave.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnSave.Text = "Save (F9)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnEdit
        '
        Me.tsbtnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnEdit.Image = CType(resources.GetObject("tsbtnEdit.Image"), System.Drawing.Image)
        Me.tsbtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnEdit.Name = "tsbtnEdit"
        Me.tsbtnEdit.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnEdit.Text = "Edit (F10)"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnDelete
        '
        Me.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnDelete.Image = CType(resources.GetObject("tsbtnDelete.Image"), System.Drawing.Image)
        Me.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnDelete.Name = "tsbtnDelete"
        Me.tsbtnDelete.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnDelete.Text = "Delete (F12)"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnPrint
        '
        Me.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnPrint.Image = CType(resources.GetObject("tsbtnPrint.Image"), System.Drawing.Image)
        Me.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnPrint.Name = "tsbtnPrint"
        Me.tsbtnPrint.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnPrint.Text = "Print (F11)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnSearch
        '
        Me.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnSearch.Image = CType(resources.GetObject("tsbtnSearch.Image"), System.Drawing.Image)
        Me.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnSearch.Name = "tsbtnSearch"
        Me.tsbtnSearch.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnSearch.Text = "Search (F2)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnLogin
        '
        Me.tsbtnLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnLogin.Image = CType(resources.GetObject("tsbtnLogin.Image"), System.Drawing.Image)
        Me.tsbtnLogin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnLogin.Name = "tsbtnLogin"
        Me.tsbtnLogin.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnLogin.Text = "Log-out"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnHelp
        '
        Me.tsbtnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnHelp.Image = CType(resources.GetObject("tsbtnHelp.Image"), System.Drawing.Image)
        Me.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnHelp.Name = "tsbtnHelp"
        Me.tsbtnHelp.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnHelp.Text = "Help"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnPermissionUpdate
        '
        Me.tsbtnPermissionUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnPermissionUpdate.Image = CType(resources.GetObject("tsbtnPermissionUpdate.Image"), System.Drawing.Image)
        Me.tsbtnPermissionUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnPermissionUpdate.Name = "tsbtnPermissionUpdate"
        Me.tsbtnPermissionUpdate.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnPermissionUpdate.Text = "User Rights Update"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 39)
        '
        'tsbtnUpdate
        '
        Me.tsbtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbtnUpdate.Image = CType(resources.GetObject("tsbtnUpdate.Image"), System.Drawing.Image)
        Me.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbtnUpdate.Name = "tsbtnUpdate"
        Me.tsbtnUpdate.Size = New System.Drawing.Size(36, 36)
        Me.tsbtnUpdate.Text = "System Update"
        '
        'tslblNewUpdate
        '
        Me.tslblNewUpdate.ForeColor = System.Drawing.Color.Red
        Me.tslblNewUpdate.Name = "tslblNewUpdate"
        Me.tslblNewUpdate.Size = New System.Drawing.Size(154, 36)
        Me.tslblNewUpdate.Text = "New update available"
        '
        'systemtray
        '
        Me.systemtray.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.systemtray.Name = "ContextMenuStrip1"
        Me.systemtray.Size = New System.Drawing.Size(61, 4)
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "K-Bidge 6.0"
        Me.NotifyIcon1.Visible = True
        '
        'mainmenuPenal
        '
        Me.mainmenuPenal.Controls.Add(Me.ElementHost1)
        Me.mainmenuPenal.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.mainmenuPenal.Location = New System.Drawing.Point(0, 580)
        Me.mainmenuPenal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.mainmenuPenal.Name = "mainmenuPenal"
        Me.mainmenuPenal.Size = New System.Drawing.Size(1104, 0)
        Me.mainmenuPenal.TabIndex = 19
        '
        'ElementHost1
        '
        Me.ElementHost1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ElementHost1.Location = New System.Drawing.Point(0, 0)
        Me.ElementHost1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ElementHost1.Name = "ElementHost1"
        Me.ElementHost1.Size = New System.Drawing.Size(1104, 0)
        Me.ElementHost1.TabIndex = 0
        Me.ElementHost1.Text = "ElementHost1"
        Me.ElementHost1.Child = Nothing
        '
        'btnMainMenu
        '
        Me.btnMainMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(104, Byte), Integer), CType(CType(7, Byte), Integer))
        Me.btnMainMenu.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnMainMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMainMenu.Location = New System.Drawing.Point(0, 554)
        Me.btnMainMenu.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnMainMenu.Name = "btnMainMenu"
        Me.btnMainMenu.Size = New System.Drawing.Size(1104, 26)
        Me.btnMainMenu.TabIndex = 20
        Me.btnMainMenu.Text = "^^ Main Menu ^^"
        Me.btnMainMenu.UseVisualStyleBackColor = False
        '
        'frmMDImain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(1104, 606)
        Me.ContextMenuStrip = Me.systemtray
        Me.Controls.Add(Me.btnMainMenu)
        Me.Controls.Add(Me.mainmenuPenal)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.StatusStrip)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmMDImain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = " K-Bridge Main Menu"
        Me.TransparencyKey = System.Drawing.Color.White
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.mainmenuPenal.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tslbl_Newkey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslbl_Savekey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_Editkey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_Printkey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tslbl_Deletekey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsbtnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtnLogin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslbl_Searchkey As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsbtnUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents tslblNewUpdate As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsbtnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents systemtray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents mainmenuPenal As System.Windows.Forms.Panel
    Friend WithEvents ElementHost1 As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents btnMainMenu As System.Windows.Forms.Button
    Friend WithEvents tslblServerName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsbtnPermissionUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator

End Class
