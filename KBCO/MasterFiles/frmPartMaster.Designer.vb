<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartMaster
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPartCost = New System.Windows.Forms.TextBox()
        Me.txtPartName = New System.Windows.Forms.TextBox()
        Me.txtPartID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtYield = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAvQty = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbIsVat = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(270, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "F2"
        '
        'txtPartCost
        '
        Me.txtPartCost.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPartCost.Location = New System.Drawing.Point(94, 64)
        Me.txtPartCost.MaxLength = 50
        Me.txtPartCost.Name = "txtPartCost"
        Me.txtPartCost.Size = New System.Drawing.Size(166, 20)
        Me.txtPartCost.TabIndex = 2
        '
        'txtPartName
        '
        Me.txtPartName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPartName.Location = New System.Drawing.Point(94, 38)
        Me.txtPartName.MaxLength = 50
        Me.txtPartName.Name = "txtPartName"
        Me.txtPartName.Size = New System.Drawing.Size(633, 20)
        Me.txtPartName.TabIndex = 1
        '
        'txtPartID
        '
        Me.txtPartID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPartID.Location = New System.Drawing.Point(94, 12)
        Me.txtPartID.MaxLength = 20
        Me.txtPartID.Name = "txtPartID"
        Me.txtPartID.Size = New System.Drawing.Size(166, 20)
        Me.txtPartID.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Part Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Part ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Part Cost"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Yield"
        '
        'txtYield
        '
        Me.txtYield.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtYield.Location = New System.Drawing.Point(94, 90)
        Me.txtYield.MaxLength = 50
        Me.txtYield.Name = "txtYield"
        Me.txtYield.Size = New System.Drawing.Size(166, 20)
        Me.txtYield.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Availavle Qty"
        '
        'txtAvQty
        '
        Me.txtAvQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAvQty.Location = New System.Drawing.Point(94, 116)
        Me.txtAvQty.MaxLength = 50
        Me.txtAvQty.Name = "txtAvQty"
        Me.txtAvQty.Size = New System.Drawing.Size(166, 20)
        Me.txtAvQty.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Is VAT"
        '
        'cbIsVat
        '
        Me.cbIsVat.AutoSize = True
        Me.cbIsVat.Location = New System.Drawing.Point(94, 145)
        Me.cbIsVat.Name = "cbIsVat"
        Me.cbIsVat.Size = New System.Drawing.Size(42, 17)
        Me.cbIsVat.TabIndex = 5
        Me.cbIsVat.Text = "NO"
        Me.cbIsVat.UseVisualStyleBackColor = True
        '
        'frmPartMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(752, 178)
        Me.Controls.Add(Me.cbIsVat)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtAvQty)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtYield)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPartCost)
        Me.Controls.Add(Me.txtPartName)
        Me.Controls.Add(Me.txtPartID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmPartMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00003"
        Me.Text = "Part Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPartCost As System.Windows.Forms.TextBox
    Friend WithEvents txtPartName As System.Windows.Forms.TextBox
    Friend WithEvents txtPartID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtYield As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAvQty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbIsVat As System.Windows.Forms.CheckBox
End Class
