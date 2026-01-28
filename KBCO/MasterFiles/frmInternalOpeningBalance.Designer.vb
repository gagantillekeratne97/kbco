<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalOpeningBalance
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtYield = New System.Windows.Forms.TextBox()
        Me.txtSN = New System.Windows.Forms.TextBox()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblBank_ID = New System.Windows.Forms.Label()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(360, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "F2"
        '
        'txtYield
        '
        Me.txtYield.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYield.Location = New System.Drawing.Point(107, 49)
        Me.txtYield.MaxLength = 40
        Me.txtYield.Name = "txtYield"
        Me.txtYield.Size = New System.Drawing.Size(243, 20)
        Me.txtYield.TabIndex = 20
        '
        'txtSN
        '
        Me.txtSN.Location = New System.Drawing.Point(107, 23)
        Me.txtSN.MaxLength = 100
        Me.txtSN.Name = "txtSN"
        Me.txtSN.Size = New System.Drawing.Size(243, 20)
        Me.txtSN.TabIndex = 19
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Location = New System.Drawing.Point(14, 52)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(89, 13)
        Me.lblBankName.TabIndex = 18
        Me.lblBankName.Text = "Opening Balance"
        '
        'lblBank_ID
        '
        Me.lblBank_ID.AutoSize = True
        Me.lblBank_ID.Location = New System.Drawing.Point(14, 26)
        Me.lblBank_ID.Name = "lblBank_ID"
        Me.lblBank_ID.Size = New System.Drawing.Size(33, 13)
        Me.lblBank_ID.TabIndex = 17
        Me.lblBank_ID.Text = "Serial"
        '
        'txtPN
        '
        Me.txtPN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPN.Location = New System.Drawing.Point(107, 75)
        Me.txtPN.MaxLength = 40
        Me.txtPN.Name = "txtPN"
        Me.txtPN.Size = New System.Drawing.Size(243, 20)
        Me.txtPN.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(22, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "PN"
        '
        'frmInternalOpeningBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 121)
        Me.Controls.Add(Me.txtPN)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtYield)
        Me.Controls.Add(Me.txtSN)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.lblBank_ID)
        Me.KeyPreview = True
        Me.Name = "frmInternalOpeningBalance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "N00001"
        Me.Text = "Manage Previous Internal reading"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtYield As System.Windows.Forms.TextBox
    Friend WithEvents txtSN As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblBank_ID As System.Windows.Forms.Label
    Friend WithEvents txtPN As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
