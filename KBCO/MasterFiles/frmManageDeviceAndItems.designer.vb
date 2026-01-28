<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageDeviceAndItems
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
        Me.lblPartNo = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.lblUnitPrice = New System.Windows.Forms.Label()
        Me.txtpartname = New System.Windows.Forms.TextBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtUnitPrice = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbActiveState = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbVatAvailable = New System.Windows.Forms.CheckBox()
        Me.lblVatAvailable = New System.Windows.Forms.Label()
        Me.dtGRNDate = New System.Windows.Forms.DateTimePicker()
        Me.lblGRNDate = New System.Windows.Forms.Label()
        Me.lblItemClass = New System.Windows.Forms.Label()
        Me.cmbItemClass = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtQtyOnHand = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtWarrantyYiele = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblPartNo
        '
        Me.lblPartNo.AutoSize = True
        Me.lblPartNo.Location = New System.Drawing.Point(12, 15)
        Me.lblPartNo.Name = "lblPartNo"
        Me.lblPartNo.Size = New System.Drawing.Size(43, 13)
        Me.lblPartNo.TabIndex = 0
        Me.lblPartNo.Text = "Part No"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 68)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(35, 13)
        Me.lblName.TabIndex = 1
        Me.lblName.Text = "Name"
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Location = New System.Drawing.Point(12, 94)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(60, 13)
        Me.lblDesc.TabIndex = 2
        Me.lblDesc.Text = "Description"
        '
        'lblUnitPrice
        '
        Me.lblUnitPrice.AutoSize = True
        Me.lblUnitPrice.Location = New System.Drawing.Point(12, 120)
        Me.lblUnitPrice.Name = "lblUnitPrice"
        Me.lblUnitPrice.Size = New System.Drawing.Size(55, 13)
        Me.lblUnitPrice.TabIndex = 3
        Me.lblUnitPrice.Text = "Unit Plrice"
        '
        'txtpartname
        '
        Me.txtpartname.Location = New System.Drawing.Point(91, 64)
        Me.txtpartname.MaxLength = 50
        Me.txtpartname.Name = "txtpartname"
        Me.txtpartname.Size = New System.Drawing.Size(561, 20)
        Me.txtpartname.TabIndex = 2
        '
        'txtPartNo
        '
        Me.txtPartNo.Location = New System.Drawing.Point(91, 12)
        Me.txtPartNo.MaxLength = 20
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.Size = New System.Drawing.Size(227, 20)
        Me.txtPartNo.TabIndex = 0
        '
        'txtUnitPrice
        '
        Me.txtUnitPrice.Location = New System.Drawing.Point(91, 116)
        Me.txtUnitPrice.MaxLength = 20
        Me.txtUnitPrice.Name = "txtUnitPrice"
        Me.txtUnitPrice.Size = New System.Drawing.Size(227, 20)
        Me.txtUnitPrice.TabIndex = 4
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(91, 90)
        Me.txtDesc.MaxLength = 50
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(561, 20)
        Me.txtDesc.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Active state"
        '
        'cbActiveState
        '
        Me.cbActiveState.AutoSize = True
        Me.cbActiveState.Checked = True
        Me.cbActiveState.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActiveState.Location = New System.Drawing.Point(91, 141)
        Me.cbActiveState.Name = "cbActiveState"
        Me.cbActiveState.Size = New System.Drawing.Size(56, 17)
        Me.cbActiveState.TabIndex = 5
        Me.cbActiveState.Text = "Active"
        Me.cbActiveState.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(324, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "F2"
        '
        'cbVatAvailable
        '
        Me.cbVatAvailable.AutoSize = True
        Me.cbVatAvailable.Checked = True
        Me.cbVatAvailable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbVatAvailable.Location = New System.Drawing.Point(249, 141)
        Me.cbVatAvailable.Name = "cbVatAvailable"
        Me.cbVatAvailable.Size = New System.Drawing.Size(69, 17)
        Me.cbVatAvailable.TabIndex = 6
        Me.cbVatAvailable.Text = "Available"
        Me.cbVatAvailable.UseVisualStyleBackColor = True
        '
        'lblVatAvailable
        '
        Me.lblVatAvailable.AutoSize = True
        Me.lblVatAvailable.Location = New System.Drawing.Point(170, 143)
        Me.lblVatAvailable.Name = "lblVatAvailable"
        Me.lblVatAvailable.Size = New System.Drawing.Size(73, 13)
        Me.lblVatAvailable.TabIndex = 21
        Me.lblVatAvailable.Text = "VAT available"
        '
        'dtGRNDate
        '
        Me.dtGRNDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtGRNDate.Location = New System.Drawing.Point(91, 39)
        Me.dtGRNDate.Name = "dtGRNDate"
        Me.dtGRNDate.Size = New System.Drawing.Size(227, 20)
        Me.dtGRNDate.TabIndex = 1
        '
        'lblGRNDate
        '
        Me.lblGRNDate.AutoSize = True
        Me.lblGRNDate.Location = New System.Drawing.Point(12, 45)
        Me.lblGRNDate.Name = "lblGRNDate"
        Me.lblGRNDate.Size = New System.Drawing.Size(57, 13)
        Me.lblGRNDate.TabIndex = 23
        Me.lblGRNDate.Text = "GRN Date"
        '
        'lblItemClass
        '
        Me.lblItemClass.AutoSize = True
        Me.lblItemClass.Location = New System.Drawing.Point(12, 178)
        Me.lblItemClass.Name = "lblItemClass"
        Me.lblItemClass.Size = New System.Drawing.Size(55, 13)
        Me.lblItemClass.TabIndex = 24
        Me.lblItemClass.Text = "Item Class"
        '
        'cmbItemClass
        '
        Me.cmbItemClass.FormattingEnabled = True
        Me.cmbItemClass.Items.AddRange(New Object() {"None", "Stock item", "Serialized Stock item", "Service"})
        Me.cmbItemClass.Location = New System.Drawing.Point(91, 175)
        Me.cmbItemClass.Name = "cmbItemClass"
        Me.cmbItemClass.Size = New System.Drawing.Size(227, 21)
        Me.cmbItemClass.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 205)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Qty on Hand"
        '
        'txtQtyOnHand
        '
        Me.txtQtyOnHand.Location = New System.Drawing.Point(91, 202)
        Me.txtQtyOnHand.MaxLength = 20
        Me.txtQtyOnHand.Name = "txtQtyOnHand"
        Me.txtQtyOnHand.Size = New System.Drawing.Size(227, 20)
        Me.txtQtyOnHand.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 179
        Me.Label5.Text = "Warranty Yiele"
        '
        'txtWarrantyYiele
        '
        Me.txtWarrantyYiele.Location = New System.Drawing.Point(91, 228)
        Me.txtWarrantyYiele.Name = "txtWarrantyYiele"
        Me.txtWarrantyYiele.Size = New System.Drawing.Size(227, 20)
        Me.txtWarrantyYiele.TabIndex = 10
        '
        'frmManageDeviceAndItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(666, 262)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtWarrantyYiele)
        Me.Controls.Add(Me.txtQtyOnHand)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbItemClass)
        Me.Controls.Add(Me.lblItemClass)
        Me.Controls.Add(Me.lblGRNDate)
        Me.Controls.Add(Me.dtGRNDate)
        Me.Controls.Add(Me.lblVatAvailable)
        Me.Controls.Add(Me.cbVatAvailable)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbActiveState)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtUnitPrice)
        Me.Controls.Add(Me.txtPartNo)
        Me.Controls.Add(Me.txtpartname)
        Me.Controls.Add(Me.lblUnitPrice)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblPartNo)
        Me.KeyPreview = True
        Me.Name = "frmManageDeviceAndItems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00003"
        Me.Text = "Manage Device & Items"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPartNo As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents lblUnitPrice As System.Windows.Forms.Label
    Friend WithEvents txtpartname As System.Windows.Forms.TextBox
    Friend WithEvents txtPartNo As System.Windows.Forms.TextBox
    Friend WithEvents txtUnitPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbActiveState As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbVatAvailable As System.Windows.Forms.CheckBox
    Friend WithEvents lblVatAvailable As System.Windows.Forms.Label
    Friend WithEvents dtGRNDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblGRNDate As System.Windows.Forms.Label
    Friend WithEvents lblItemClass As System.Windows.Forms.Label
    Friend WithEvents cmbItemClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtQtyOnHand As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtWarrantyYiele As System.Windows.Forms.TextBox
End Class
