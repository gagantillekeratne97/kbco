<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerMaster
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
        Me.lblVatTypeVal = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbBillingPeriod = New System.Windows.Forms.ComboBox()
        Me.cbNBT2 = New System.Windows.Forms.CheckBox()
        Me.cbNBT1 = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.txtSvatNo = New System.Windows.Forms.TextBox()
        Me.txtVatNo = New System.Windows.Forms.TextBox()
        Me.txtVatType = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtContactNo = New System.Windows.Forms.TextBox()
        Me.txtAdd2 = New System.Windows.Forms.TextBox()
        Me.txtCusID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAdd1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblVatTypeVal
        '
        Me.lblVatTypeVal.AutoSize = True
        Me.lblVatTypeVal.ForeColor = System.Drawing.Color.Brown
        Me.lblVatTypeVal.Location = New System.Drawing.Point(251, 197)
        Me.lblVatTypeVal.Name = "lblVatTypeVal"
        Me.lblVatTypeVal.Size = New System.Drawing.Size(27, 13)
        Me.lblVatTypeVal.TabIndex = 68
        Me.lblVatTypeVal.Text = "N/A"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Location = New System.Drawing.Point(207, 197)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(19, 13)
        Me.Label16.TabIndex = 67
        Me.Label16.Text = "F2"
        '
        'cmbBillingPeriod
        '
        Me.cmbBillingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBillingPeriod.FormattingEnabled = True
        Me.cmbBillingPeriod.Items.AddRange(New Object() {"1", "5", "15", "25", "31"})
        Me.cmbBillingPeriod.Location = New System.Drawing.Point(101, 323)
        Me.cmbBillingPeriod.Name = "cmbBillingPeriod"
        Me.cmbBillingPeriod.Size = New System.Drawing.Size(121, 21)
        Me.cmbBillingPeriod.TabIndex = 12
        '
        'cbNBT2
        '
        Me.cbNBT2.AutoSize = True
        Me.cbNBT2.Location = New System.Drawing.Point(101, 300)
        Me.cbNBT2.Name = "cbNBT2"
        Me.cbNBT2.Size = New System.Drawing.Size(40, 17)
        Me.cbNBT2.TabIndex = 11
        Me.cbNBT2.Text = "No"
        Me.cbNBT2.UseVisualStyleBackColor = True
        '
        'cbNBT1
        '
        Me.cbNBT1.AutoSize = True
        Me.cbNBT1.Location = New System.Drawing.Point(101, 274)
        Me.cbNBT1.Name = "cbNBT1"
        Me.cbNBT1.Size = New System.Drawing.Size(40, 17)
        Me.cbNBT1.TabIndex = 10
        Me.cbNBT1.Text = "No"
        Me.cbNBT1.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(11, 93)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(84, 13)
        Me.Label15.TabIndex = 63
        Me.Label15.Text = "Billing Address 2"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(11, 119)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 13)
        Me.Label14.TabIndex = 62
        Me.Label14.Text = "Contact No"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 145)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 13)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Fax"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(11, 171)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Email"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(11, 197)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "VAT Type"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 223)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "VAT No"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(11, 250)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "S-VAT No"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 275)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "NBT 1"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 301)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 55
        Me.Label7.Text = "NBT 2"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 327)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "Billing Period"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 353)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Discount"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Billing Address 1"
        '
        'txtDiscount
        '
        Me.txtDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiscount.Location = New System.Drawing.Point(101, 350)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(100, 20)
        Me.txtDiscount.TabIndex = 13
        '
        'txtSvatNo
        '
        Me.txtSvatNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSvatNo.Location = New System.Drawing.Point(101, 246)
        Me.txtSvatNo.Name = "txtSvatNo"
        Me.txtSvatNo.Size = New System.Drawing.Size(197, 20)
        Me.txtSvatNo.TabIndex = 9
        '
        'txtVatNo
        '
        Me.txtVatNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVatNo.Location = New System.Drawing.Point(101, 220)
        Me.txtVatNo.Name = "txtVatNo"
        Me.txtVatNo.Size = New System.Drawing.Size(197, 20)
        Me.txtVatNo.TabIndex = 8
        '
        'txtVatType
        '
        Me.txtVatType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVatType.Location = New System.Drawing.Point(101, 194)
        Me.txtVatType.Name = "txtVatType"
        Me.txtVatType.Size = New System.Drawing.Size(100, 20)
        Me.txtVatType.TabIndex = 7
        '
        'txtEmail
        '
        Me.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtEmail.Location = New System.Drawing.Point(101, 168)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(467, 20)
        Me.txtEmail.TabIndex = 6
        '
        'txtFax
        '
        Me.txtFax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFax.Location = New System.Drawing.Point(101, 142)
        Me.txtFax.MaxLength = 50
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(467, 20)
        Me.txtFax.TabIndex = 5
        '
        'txtContactNo
        '
        Me.txtContactNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContactNo.Location = New System.Drawing.Point(101, 116)
        Me.txtContactNo.MaxLength = 50
        Me.txtContactNo.Name = "txtContactNo"
        Me.txtContactNo.Size = New System.Drawing.Size(467, 20)
        Me.txtContactNo.TabIndex = 4
        '
        'txtAdd2
        '
        Me.txtAdd2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAdd2.Location = New System.Drawing.Point(101, 90)
        Me.txtAdd2.MaxLength = 100
        Me.txtAdd2.Name = "txtAdd2"
        Me.txtAdd2.Size = New System.Drawing.Size(712, 20)
        Me.txtAdd2.TabIndex = 3
        '
        'txtCusID
        '
        Me.txtCusID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCusID.Location = New System.Drawing.Point(101, 12)
        Me.txtCusID.MaxLength = 20
        Me.txtCusID.Name = "txtCusID"
        Me.txtCusID.Size = New System.Drawing.Size(100, 20)
        Me.txtCusID.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(207, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "F2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "ID"
        '
        'txtAdd1
        '
        Me.txtAdd1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAdd1.Location = New System.Drawing.Point(101, 64)
        Me.txtAdd1.MaxLength = 100
        Me.txtAdd1.Name = "txtAdd1"
        Me.txtAdd1.Size = New System.Drawing.Size(712, 20)
        Me.txtAdd1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Name"
        '
        'txtCusName
        '
        Me.txtCusName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCusName.Location = New System.Drawing.Point(101, 38)
        Me.txtCusName.Name = "txtCusName"
        Me.txtCusName.Size = New System.Drawing.Size(712, 20)
        Me.txtCusName.TabIndex = 1
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(11, 379)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(39, 13)
        Me.Label17.TabIndex = 70
        Me.Label17.Text = "PO No"
        '
        'txtPONo
        '
        Me.txtPONo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPONo.Location = New System.Drawing.Point(101, 376)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.Size = New System.Drawing.Size(100, 20)
        Me.txtPONo.TabIndex = 69
        '
        'frmCustomerMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(837, 420)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtPONo)
        Me.Controls.Add(Me.lblVatTypeVal)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cmbBillingPeriod)
        Me.Controls.Add(Me.cbNBT2)
        Me.Controls.Add(Me.cbNBT1)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.txtSvatNo)
        Me.Controls.Add(Me.txtVatNo)
        Me.Controls.Add(Me.txtVatType)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.txtContactNo)
        Me.Controls.Add(Me.txtAdd2)
        Me.Controls.Add(Me.txtCusID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAdd1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCusName)
        Me.KeyPreview = True
        Me.Name = "frmCustomerMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00001"
        Me.Text = "Customer Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblVatTypeVal As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbBillingPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents cbNBT2 As System.Windows.Forms.CheckBox
    Friend WithEvents cbNBT1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtSvatNo As System.Windows.Forms.TextBox
    Friend WithEvents txtVatNo As System.Windows.Forms.TextBox
    Friend WithEvents txtVatType As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents txtContactNo As System.Windows.Forms.TextBox
    Friend WithEvents txtAdd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCusID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAdd1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCusName As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
End Class
