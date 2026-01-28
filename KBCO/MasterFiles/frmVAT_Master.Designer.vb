<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVAT_Master
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtVatTypeID = New System.Windows.Forms.TextBox()
        Me.txtVatTypeName = New System.Windows.Forms.TextBox()
        Me.txtVatPre = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbVAT = New System.Windows.Forms.CheckBox()
        Me.cbNBT = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvoiceHeadingName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "VAT Type ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "VAT Type Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "%"
        '
        'txtVatTypeID
        '
        Me.txtVatTypeID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVatTypeID.Location = New System.Drawing.Point(108, 12)
        Me.txtVatTypeID.MaxLength = 20
        Me.txtVatTypeID.Name = "txtVatTypeID"
        Me.txtVatTypeID.Size = New System.Drawing.Size(100, 20)
        Me.txtVatTypeID.TabIndex = 0
        '
        'txtVatTypeName
        '
        Me.txtVatTypeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVatTypeName.Location = New System.Drawing.Point(108, 38)
        Me.txtVatTypeName.MaxLength = 50
        Me.txtVatTypeName.Name = "txtVatTypeName"
        Me.txtVatTypeName.Size = New System.Drawing.Size(563, 20)
        Me.txtVatTypeName.TabIndex = 1
        '
        'txtVatPre
        '
        Me.txtVatPre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVatPre.Location = New System.Drawing.Point(108, 64)
        Me.txtVatPre.MaxLength = 3
        Me.txtVatPre.Name = "txtVatPre"
        Me.txtVatPre.Size = New System.Drawing.Size(100, 20)
        Me.txtVatPre.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(214, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "F2"
        '
        'cbVAT
        '
        Me.cbVAT.AutoSize = True
        Me.cbVAT.Checked = True
        Me.cbVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbVAT.Location = New System.Drawing.Point(108, 116)
        Me.cbVAT.Name = "cbVAT"
        Me.cbVAT.Size = New System.Drawing.Size(44, 17)
        Me.cbVAT.TabIndex = 625
        Me.cbVAT.Text = "Yes"
        Me.cbVAT.UseVisualStyleBackColor = True
        '
        'cbNBT
        '
        Me.cbNBT.AutoSize = True
        Me.cbNBT.Checked = True
        Me.cbNBT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbNBT.Location = New System.Drawing.Point(108, 90)
        Me.cbNBT.Name = "cbNBT"
        Me.cbNBT.Size = New System.Drawing.Size(44, 17)
        Me.cbNBT.TabIndex = 624
        Me.cbNBT.Text = "Yes"
        Me.cbNBT.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(28, 13)
        Me.Label11.TabIndex = 623
        Me.Label11.Text = "VAT"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 13)
        Me.Label8.TabIndex = 622
        Me.Label8.Text = "NBT"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 13)
        Me.Label5.TabIndex = 626
        Me.Label5.Text = "Invoice Heading Name"
        '
        'txtInvoiceHeadingName
        '
        Me.txtInvoiceHeadingName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInvoiceHeadingName.Location = New System.Drawing.Point(141, 143)
        Me.txtInvoiceHeadingName.MaxLength = 50
        Me.txtInvoiceHeadingName.Name = "txtInvoiceHeadingName"
        Me.txtInvoiceHeadingName.Size = New System.Drawing.Size(530, 20)
        Me.txtInvoiceHeadingName.TabIndex = 627
        '
        'frmVAT_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(683, 210)
        Me.Controls.Add(Me.txtInvoiceHeadingName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cbVAT)
        Me.Controls.Add(Me.cbNBT)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtVatPre)
        Me.Controls.Add(Me.txtVatTypeName)
        Me.Controls.Add(Me.txtVatTypeID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmVAT_Master"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00005"
        Me.Text = "VAT Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtVatTypeID As System.Windows.Forms.TextBox
    Friend WithEvents txtVatTypeName As System.Windows.Forms.TextBox
    Friend WithEvents txtVatPre As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbVAT As System.Windows.Forms.CheckBox
    Friend WithEvents cbNBT As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceHeadingName As System.Windows.Forms.TextBox
End Class
