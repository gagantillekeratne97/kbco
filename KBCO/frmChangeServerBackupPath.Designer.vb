<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeServerBackupPath
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
        Me.dgPOImagePaths = New System.Windows.Forms.DataGridView()
        Me.REF_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PO_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COST_SHEET_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblServerAddress = New System.Windows.Forms.Label()
        Me.txtNewServerAddress = New System.Windows.Forms.TextBox()
        Me.btnChange = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.dgLocalPurchase = New System.Windows.Forms.DataGridView()
        Me.REF_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QUOT_IMAGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ATT_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.dgNashua_PO = New System.Windows.Forms.DataGridView()
        Me.dgCN = New System.Windows.Forms.DataGridView()
        Me.NREF_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N_PO_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CN_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CN_ATT_PATH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgPOImagePaths, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgLocalPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.dgNashua_PO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgPOImagePaths
        '
        Me.dgPOImagePaths.AllowUserToAddRows = False
        Me.dgPOImagePaths.AllowUserToDeleteRows = False
        Me.dgPOImagePaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgPOImagePaths.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.REF_ID, Me.PO_PATH, Me.COST_SHEET_PATH, Me.INV_PATH})
        Me.dgPOImagePaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPOImagePaths.Location = New System.Drawing.Point(3, 3)
        Me.dgPOImagePaths.Name = "dgPOImagePaths"
        Me.dgPOImagePaths.ReadOnly = True
        Me.dgPOImagePaths.Size = New System.Drawing.Size(917, 367)
        Me.dgPOImagePaths.TabIndex = 0
        '
        'REF_ID
        '
        Me.REF_ID.HeaderText = "Ref ID"
        Me.REF_ID.Name = "REF_ID"
        Me.REF_ID.ReadOnly = True
        Me.REF_ID.Width = 150
        '
        'PO_PATH
        '
        Me.PO_PATH.HeaderText = "PO Image path"
        Me.PO_PATH.Name = "PO_PATH"
        Me.PO_PATH.ReadOnly = True
        Me.PO_PATH.Width = 250
        '
        'COST_SHEET_PATH
        '
        Me.COST_SHEET_PATH.HeaderText = "Cost Sheet Path"
        Me.COST_SHEET_PATH.Name = "COST_SHEET_PATH"
        Me.COST_SHEET_PATH.ReadOnly = True
        Me.COST_SHEET_PATH.Width = 250
        '
        'INV_PATH
        '
        Me.INV_PATH.HeaderText = "Invoice Path"
        Me.INV_PATH.Name = "INV_PATH"
        Me.INV_PATH.ReadOnly = True
        Me.INV_PATH.Width = 250
        '
        'lblServerAddress
        '
        Me.lblServerAddress.AutoSize = True
        Me.lblServerAddress.Location = New System.Drawing.Point(13, 26)
        Me.lblServerAddress.Name = "lblServerAddress"
        Me.lblServerAddress.Size = New System.Drawing.Size(104, 13)
        Me.lblServerAddress.TabIndex = 1
        Me.lblServerAddress.Text = "New Server Address"
        '
        'txtNewServerAddress
        '
        Me.txtNewServerAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewServerAddress.Location = New System.Drawing.Point(123, 23)
        Me.txtNewServerAddress.Name = "txtNewServerAddress"
        Me.txtNewServerAddress.Size = New System.Drawing.Size(686, 20)
        Me.txtNewServerAddress.TabIndex = 2
        '
        'btnChange
        '
        Me.btnChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChange.Location = New System.Drawing.Point(834, 21)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(75, 23)
        Me.btnChange.TabIndex = 3
        Me.btnChange.Text = "Change"
        Me.btnChange.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 50)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(931, 399)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgPOImagePaths)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(923, 373)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Sales Work Flow"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgLocalPurchase)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(923, 373)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Local Purchase"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'dgLocalPurchase
        '
        Me.dgLocalPurchase.AllowUserToAddRows = False
        Me.dgLocalPurchase.AllowUserToDeleteRows = False
        Me.dgLocalPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLocalPurchase.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.REF_NO, Me.QUOT_IMAGE, Me.ATT_PATH})
        Me.dgLocalPurchase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgLocalPurchase.Location = New System.Drawing.Point(3, 3)
        Me.dgLocalPurchase.Name = "dgLocalPurchase"
        Me.dgLocalPurchase.ReadOnly = True
        Me.dgLocalPurchase.Size = New System.Drawing.Size(917, 367)
        Me.dgLocalPurchase.TabIndex = 0
        '
        'REF_NO
        '
        Me.REF_NO.HeaderText = "Ref No"
        Me.REF_NO.Name = "REF_NO"
        Me.REF_NO.ReadOnly = True
        '
        'QUOT_IMAGE
        '
        Me.QUOT_IMAGE.HeaderText = "Quot Image Path"
        Me.QUOT_IMAGE.Name = "QUOT_IMAGE"
        Me.QUOT_IMAGE.ReadOnly = True
        Me.QUOT_IMAGE.Width = 300
        '
        'ATT_PATH
        '
        Me.ATT_PATH.HeaderText = "Attachment Path"
        Me.ATT_PATH.Name = "ATT_PATH"
        Me.ATT_PATH.ReadOnly = True
        Me.ATT_PATH.Width = 300
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.dgNashua_PO)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(923, 373)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Nashua SO"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.dgCN)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(923, 373)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Credit Notes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'dgNashua_PO
        '
        Me.dgNashua_PO.AllowUserToAddRows = False
        Me.dgNashua_PO.AllowUserToDeleteRows = False
        Me.dgNashua_PO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgNashua_PO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NREF_NO, Me.N_PO_PATH})
        Me.dgNashua_PO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgNashua_PO.Location = New System.Drawing.Point(3, 3)
        Me.dgNashua_PO.Name = "dgNashua_PO"
        Me.dgNashua_PO.ReadOnly = True
        Me.dgNashua_PO.Size = New System.Drawing.Size(917, 367)
        Me.dgNashua_PO.TabIndex = 1
        '
        'dgCN
        '
        Me.dgCN.AllowUserToAddRows = False
        Me.dgCN.AllowUserToDeleteRows = False
        Me.dgCN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCN.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CN_NO, Me.CN_ATT_PATH})
        Me.dgCN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgCN.Location = New System.Drawing.Point(3, 3)
        Me.dgCN.Name = "dgCN"
        Me.dgCN.ReadOnly = True
        Me.dgCN.Size = New System.Drawing.Size(917, 367)
        Me.dgCN.TabIndex = 1
        '
        'NREF_NO
        '
        Me.NREF_NO.HeaderText = "Ref No"
        Me.NREF_NO.Name = "NREF_NO"
        Me.NREF_NO.ReadOnly = True
        '
        'N_PO_PATH
        '
        Me.N_PO_PATH.HeaderText = "Attachment Path"
        Me.N_PO_PATH.Name = "N_PO_PATH"
        Me.N_PO_PATH.ReadOnly = True
        Me.N_PO_PATH.Width = 300
        '
        'CN_NO
        '
        Me.CN_NO.HeaderText = "CN NO"
        Me.CN_NO.Name = "CN_NO"
        Me.CN_NO.ReadOnly = True
        '
        'CN_ATT_PATH
        '
        Me.CN_ATT_PATH.HeaderText = "Attachment Path"
        Me.CN_ATT_PATH.Name = "CN_ATT_PATH"
        Me.CN_ATT_PATH.ReadOnly = True
        Me.CN_ATT_PATH.Width = 300
        '
        'frmChangeServerBackupPath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(963, 449)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.txtNewServerAddress)
        Me.Controls.Add(Me.lblServerAddress)
        Me.Name = "frmChangeServerBackupPath"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00009"
        Me.Text = "Change Server Backup Path"
        CType(Me.dgPOImagePaths, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgLocalPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.dgNashua_PO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgPOImagePaths As System.Windows.Forms.DataGridView
    Friend WithEvents REF_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PO_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COST_SHEET_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblServerAddress As System.Windows.Forms.Label
    Friend WithEvents txtNewServerAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnChange As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgLocalPurchase As System.Windows.Forms.DataGridView
    Friend WithEvents REF_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QUOT_IMAGE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ATT_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents dgNashua_PO As System.Windows.Forms.DataGridView
    Friend WithEvents NREF_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents N_PO_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents dgCN As System.Windows.Forms.DataGridView
    Friend WithEvents CN_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CN_ATT_PATH As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
