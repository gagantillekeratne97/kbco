Partial Class Something
    Inherits System.Windows.Forms.Form

    ' ... Other generated code

    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label

    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents lblInvoiceNumber As System.Windows.Forms.Label
    Friend WithEvents lblInvoiceDate As System.Windows.Forms.Label

    Friend WithEvents txtAddressLine1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine1 As System.Windows.Forms.Label

    Friend WithEvents txtAddressLine2 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine2 As System.Windows.Forms.Label

    Friend WithEvents txtAddressLine3 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine3 As System.Windows.Forms.Label

    Friend WithEvents txtInvoiceAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblInvoiceAmount As System.Windows.Forms.Label

    Friend WithEvents txtTaxAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblTaxAmount As System.Windows.Forms.Label

    Friend WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalAmount As System.Windows.Forms.Label



    ' ... Other generated code

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.lblCustomerID = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.lblInvoiceNumber = New System.Windows.Forms.Label()
        Me.lblInvoiceDate = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.lblAddressLine1 = New System.Windows.Forms.Label()
        Me.txtAddressLine1 = New System.Windows.Forms.TextBox()
        Me.lblAddressLine2 = New System.Windows.Forms.Label()
        Me.txtAddressLine2 = New System.Windows.Forms.TextBox()
        Me.lblAddressLine3 = New System.Windows.Forms.Label()
        Me.txtAddressLine3 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.lblInvoiceAmount = New System.Windows.Forms.Label()
        Me.txtInvoiceAmount = New System.Windows.Forms.TextBox()
        Me.lblTaxAmount = New System.Windows.Forms.Label()
        Me.txtTaxAmount = New System.Windows.Forms.TextBox()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Location = New System.Drawing.Point(12, 9)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(85, 13)
        Me.lblCustomerName.TabIndex = 0
        Me.lblCustomerName.Text = "Customer Name:"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Location = New System.Drawing.Point(150, 9)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(200, 20)
        Me.txtCustomerName.TabIndex = 1
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = True
        Me.lblCustomerID.Location = New System.Drawing.Point(12, 35)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(68, 13)
        Me.lblCustomerID.TabIndex = 2
        Me.lblCustomerID.Text = "Customer ID:"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.Location = New System.Drawing.Point(150, 35)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(200, 20)
        Me.txtCustomerID.TabIndex = 3
        '
        'lblInvoiceNumber
        '
        Me.lblInvoiceNumber.AutoSize = True
        Me.lblInvoiceNumber.Location = New System.Drawing.Point(422, 12)
        Me.lblInvoiceNumber.Name = "lblInvoiceNumber"
        Me.lblInvoiceNumber.Size = New System.Drawing.Size(83, 13)
        Me.lblInvoiceNumber.TabIndex = 0
        Me.lblInvoiceNumber.Text = "Credit Note No: "
        '
        'lblInvoiceDate
        '
        Me.lblInvoiceDate.AutoSize = True
        Me.lblInvoiceDate.Location = New System.Drawing.Point(422, 38)
        Me.lblInvoiceDate.Name = "lblInvoiceDate"
        Me.lblInvoiceDate.Size = New System.Drawing.Size(92, 13)
        Me.lblInvoiceDate.TabIndex = 2
        Me.lblInvoiceDate.Text = "Credit Note Date: "
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(560, 39)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(200, 20)
        Me.TextBox1.TabIndex = 7
        '
        'lblAddressLine1
        '
        Me.lblAddressLine1.AutoSize = True
        Me.lblAddressLine1.Location = New System.Drawing.Point(12, 61)
        Me.lblAddressLine1.Name = "lblAddressLine1"
        Me.lblAddressLine1.Size = New System.Drawing.Size(80, 13)
        Me.lblAddressLine1.TabIndex = 0
        Me.lblAddressLine1.Text = "Address Line 1:"
        '
        'txtAddressLine1
        '
        Me.txtAddressLine1.Location = New System.Drawing.Point(150, 61)
        Me.txtAddressLine1.Name = "txtAddressLine1"
        Me.txtAddressLine1.Size = New System.Drawing.Size(200, 20)
        Me.txtAddressLine1.TabIndex = 1
        '
        'lblAddressLine2
        '
        Me.lblAddressLine2.AutoSize = True
        Me.lblAddressLine2.Location = New System.Drawing.Point(12, 87)
        Me.lblAddressLine2.Name = "lblAddressLine2"
        Me.lblAddressLine2.Size = New System.Drawing.Size(80, 13)
        Me.lblAddressLine2.TabIndex = 2
        Me.lblAddressLine2.Text = "Address Line 2:"
        '
        'txtAddressLine2
        '
        Me.txtAddressLine2.Location = New System.Drawing.Point(150, 87)
        Me.txtAddressLine2.Name = "txtAddressLine2"
        Me.txtAddressLine2.Size = New System.Drawing.Size(200, 20)
        Me.txtAddressLine2.TabIndex = 3
        '
        'lblAddressLine3
        '
        Me.lblAddressLine3.AutoSize = True
        Me.lblAddressLine3.Location = New System.Drawing.Point(12, 113)
        Me.lblAddressLine3.Name = "lblAddressLine3"
        Me.lblAddressLine3.Size = New System.Drawing.Size(80, 13)
        Me.lblAddressLine3.TabIndex = 4
        Me.lblAddressLine3.Text = "Address Line 3:"
        '
        'txtAddressLine3
        '
        Me.txtAddressLine3.Location = New System.Drawing.Point(150, 113)
        Me.txtAddressLine3.Name = "txtAddressLine3"
        Me.txtAddressLine3.Size = New System.Drawing.Size(200, 20)
        Me.txtAddressLine3.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 268)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(926, 184)
        Me.DataGridView1.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(422, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Invoices"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(422, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Invoice Date:"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(560, 12)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(200, 20)
        Me.TextBox3.TabIndex = 13
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(560, 113)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 139)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Reason : "
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(150, 139)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(200, 67)
        Me.RichTextBox1.TabIndex = 16
        Me.RichTextBox1.Text = ""
        '
        'lblInvoiceAmount
        '
        Me.lblInvoiceAmount.AutoSize = True
        Me.lblInvoiceAmount.Location = New System.Drawing.Point(600, 463)
        Me.lblInvoiceAmount.Name = "lblInvoiceAmount"
        Me.lblInvoiceAmount.Size = New System.Drawing.Size(84, 13)
        Me.lblInvoiceAmount.TabIndex = 0
        Me.lblInvoiceAmount.Text = "Invoice Amount:"
        '
        'txtInvoiceAmount
        '
        Me.txtInvoiceAmount.Location = New System.Drawing.Point(738, 463)
        Me.txtInvoiceAmount.Name = "txtInvoiceAmount"
        Me.txtInvoiceAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtInvoiceAmount.TabIndex = 1
        '
        'lblTaxAmount
        '
        Me.lblTaxAmount.AutoSize = True
        Me.lblTaxAmount.Location = New System.Drawing.Point(600, 489)
        Me.lblTaxAmount.Name = "lblTaxAmount"
        Me.lblTaxAmount.Size = New System.Drawing.Size(67, 13)
        Me.lblTaxAmount.TabIndex = 2
        Me.lblTaxAmount.Text = "Tax Amount:"
        '
        'txtTaxAmount
        '
        Me.txtTaxAmount.Location = New System.Drawing.Point(738, 489)
        Me.txtTaxAmount.Name = "txtTaxAmount"
        Me.txtTaxAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtTaxAmount.TabIndex = 3
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.Location = New System.Drawing.Point(600, 515)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(73, 13)
        Me.lblTotalAmount.TabIndex = 4
        Me.lblTotalAmount.Text = "Total Amount:"
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.Location = New System.Drawing.Point(738, 515)
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(200, 20)
        Me.txtTotalAmount.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label23.Location = New System.Drawing.Point(356, 12)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(19, 13)
        Me.Label23.TabIndex = 672
        Me.Label23.Text = "F2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(356, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 673
        Me.Label4.Text = "F2"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(560, 182)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(200, 20)
        Me.TextBox2.TabIndex = 677
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(560, 155)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(200, 21)
        Me.ComboBox2.TabIndex = 676
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(422, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 674
        Me.Label5.Text = "Department"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(422, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 675
        Me.Label6.Text = "HOD"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(560, 87)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(200, 21)
        Me.ComboBox1.TabIndex = 678
        '
        'frmCreditNote
        '
        Me.ClientSize = New System.Drawing.Size(945, 554)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.lblInvoiceAmount)
        Me.Controls.Add(Me.txtInvoiceAmount)
        Me.Controls.Add(Me.lblTaxAmount)
        Me.Controls.Add(Me.txtTaxAmount)
        Me.Controls.Add(Me.lblTotalAmount)
        Me.Controls.Add(Me.txtTotalAmount)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.lblAddressLine1)
        Me.Controls.Add(Me.txtAddressLine1)
        Me.Controls.Add(Me.lblAddressLine2)
        Me.Controls.Add(Me.txtAddressLine2)
        Me.Controls.Add(Me.lblAddressLine3)
        Me.Controls.Add(Me.txtAddressLine3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.lblInvoiceNumber)
        Me.Controls.Add(Me.lblInvoiceDate)
        Me.Controls.Add(Me.lblCustomerName)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.lblCustomerID)
        Me.Controls.Add(Me.txtCustomerID)
        Me.Name = "frmCreditNote"
        Me.Text = "frmCreditNote"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox1 As ComboBox

    ' ... Other generated code

End Class
