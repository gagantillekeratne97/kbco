<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBank
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
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtBankID = New System.Windows.Forms.TextBox()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblBank_ID = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtBankName
        '
        Me.txtBankName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBankName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.Location = New System.Drawing.Point(77, 38)
        Me.txtBankName.MaxLength = 40
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(560, 20)
        Me.txtBankName.TabIndex = 7
        '
        'txtBankID
        '
        Me.txtBankID.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBankID.Location = New System.Drawing.Point(77, 12)
        Me.txtBankID.MaxLength = 50
        Me.txtBankID.Name = "txtBankID"
        Me.txtBankID.Size = New System.Drawing.Size(161, 20)
        Me.txtBankID.TabIndex = 6
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Location = New System.Drawing.Point(8, 41)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(63, 13)
        Me.lblBankName.TabIndex = 5
        Me.lblBankName.Text = "Bank Name"
        '
        'lblBank_ID
        '
        Me.lblBank_ID.AutoSize = True
        Me.lblBank_ID.Location = New System.Drawing.Point(8, 15)
        Me.lblBank_ID.Name = "lblBank_ID"
        Me.lblBank_ID.Size = New System.Drawing.Size(46, 13)
        Me.lblBank_ID.TabIndex = 4
        Me.lblBank_ID.Text = "Bank ID"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(240, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "F2"
        '
        'frmBank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 75)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.txtBankID)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.lblBank_ID)
        Me.KeyPreview = True
        Me.Name = "frmBank"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "M00003"
        Me.Text = "Bank"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents txtBankID As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblBank_ID As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
