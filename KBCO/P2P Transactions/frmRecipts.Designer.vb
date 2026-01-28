<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecipts
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbReciptType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtInvoiceAmount = New System.Windows.Forms.TextBox()
        Me.txtPaymentAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAmountInWords = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRecivedBy = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgHistory = New System.Windows.Forms.DataGridView()
        Me.RECIPT_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RECIPT_AMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RECIPT_PDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblOutstanding = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtReciptID = New System.Windows.Forms.TextBox()
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(258, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 589
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(346, 38)
        Me.txtCustomerName.Multiline = True
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(349, 45)
        Me.txtCustomerName.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(258, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 588
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(346, 12)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(100, 20)
        Me.txtCustomerID.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 591
        Me.Label3.Text = "Invoice No"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInvoiceNo.Location = New System.Drawing.Point(99, 12)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(115, 20)
        Me.txtInvoiceNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 593
        Me.Label4.Text = "Type"
        '
        'cmbReciptType
        '
        Me.cmbReciptType.FormattingEnabled = True
        Me.cmbReciptType.Items.AddRange(New Object() {"FULL PAYMENT", "ADVANCE PAYMENT"})
        Me.cmbReciptType.Location = New System.Drawing.Point(99, 64)
        Me.cmbReciptType.Name = "cmbReciptType"
        Me.cmbReciptType.Size = New System.Drawing.Size(115, 21)
        Me.cmbReciptType.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 596
        Me.Label5.Text = "Invoice Amount"
        '
        'txtInvoiceAmount
        '
        Me.txtInvoiceAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtInvoiceAmount.Location = New System.Drawing.Point(99, 38)
        Me.txtInvoiceAmount.Name = "txtInvoiceAmount"
        Me.txtInvoiceAmount.Size = New System.Drawing.Size(115, 20)
        Me.txtInvoiceAmount.TabIndex = 1
        '
        'txtPaymentAmount
        '
        Me.txtPaymentAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPaymentAmount.Location = New System.Drawing.Point(99, 91)
        Me.txtPaymentAmount.Name = "txtPaymentAmount"
        Me.txtPaymentAmount.Size = New System.Drawing.Size(115, 20)
        Me.txtPaymentAmount.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 598
        Me.Label6.Text = "Payment Amount"
        '
        'txtAmountInWords
        '
        Me.txtAmountInWords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountInWords.Location = New System.Drawing.Point(99, 117)
        Me.txtAmountInWords.Multiline = True
        Me.txtAmountInWords.Name = "txtAmountInWords"
        Me.txtAmountInWords.Size = New System.Drawing.Size(596, 36)
        Me.txtAmountInWords.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 600
        Me.Label7.Text = "Amount in word"
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.FormattingEnabled = True
        Me.cmbPaymentMethod.Items.AddRange(New Object() {"CASH", "CHEQUE"})
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(99, 159)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(115, 21)
        Me.cmbPaymentMethod.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 602
        Me.Label8.Text = "Payment Method"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChequeNo.Location = New System.Drawing.Point(99, 186)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(115, 20)
        Me.txtChequeNo.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 189)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 604
        Me.Label9.Text = "Cheque No"
        '
        'txtBankName
        '
        Me.txtBankName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankName.Location = New System.Drawing.Point(99, 212)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(115, 20)
        Me.txtBankName.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 215)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 606
        Me.Label10.Text = "Bank Name"
        '
        'txtRecivedBy
        '
        Me.txtRecivedBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecivedBy.Location = New System.Drawing.Point(99, 238)
        Me.txtRecivedBy.Name = "txtRecivedBy"
        Me.txtRecivedBy.Size = New System.Drawing.Size(115, 20)
        Me.txtRecivedBy.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 241)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 608
        Me.Label11.Text = "Received by"
        '
        'dgHistory
        '
        Me.dgHistory.AllowUserToAddRows = False
        Me.dgHistory.AllowUserToDeleteRows = False
        Me.dgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RECIPT_NO, Me.RECIPT_AMOUNT, Me.RECIPT_PDATE})
        Me.dgHistory.Location = New System.Drawing.Point(701, 6)
        Me.dgHistory.Name = "dgHistory"
        Me.dgHistory.ReadOnly = True
        Me.dgHistory.Size = New System.Drawing.Size(359, 305)
        Me.dgHistory.TabIndex = 609
        '
        'RECIPT_NO
        '
        Me.RECIPT_NO.HeaderText = "Recipt No"
        Me.RECIPT_NO.Name = "RECIPT_NO"
        Me.RECIPT_NO.ReadOnly = True
        '
        'RECIPT_AMOUNT
        '
        Me.RECIPT_AMOUNT.HeaderText = "Amount"
        Me.RECIPT_AMOUNT.Name = "RECIPT_AMOUNT"
        Me.RECIPT_AMOUNT.ReadOnly = True
        '
        'RECIPT_PDATE
        '
        Me.RECIPT_PDATE.HeaderText = "Date"
        Me.RECIPT_PDATE.Name = "RECIPT_PDATE"
        Me.RECIPT_PDATE.ReadOnly = True
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Location = New System.Drawing.Point(239, 215)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(21, 13)
        Me.lblBankName.TabIndex = 610
        Me.lblBankName.Text = "##"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.Location = New System.Drawing.Point(239, 245)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(21, 13)
        Me.lblTechName.TabIndex = 610
        Me.lblTechName.Text = "##"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(462, 212)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 611
        Me.Label12.Text = "Outstanding"
        '
        'lblOutstanding
        '
        Me.lblOutstanding.AutoSize = True
        Me.lblOutstanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutstanding.Location = New System.Drawing.Point(509, 238)
        Me.lblOutstanding.Name = "lblOutstanding"
        Me.lblOutstanding.Size = New System.Drawing.Size(45, 24)
        Me.lblOutstanding.TabIndex = 612
        Me.lblOutstanding.Text = "0.00"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label13.Location = New System.Drawing.Point(220, 215)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(19, 13)
        Me.Label13.TabIndex = 613
        Me.Label13.Text = "F2"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label14.Location = New System.Drawing.Point(220, 245)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(19, 13)
        Me.Label14.TabIndex = 614
        Me.Label14.Text = "F2"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label15.Location = New System.Drawing.Point(220, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(19, 13)
        Me.Label15.TabIndex = 615
        Me.Label15.Text = "F2"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(261, 278)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 616
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(12, 283)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 13)
        Me.Label16.TabIndex = 617
        Me.Label16.Text = "Recipt ID"
        '
        'txtReciptID
        '
        Me.txtReciptID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReciptID.Location = New System.Drawing.Point(99, 280)
        Me.txtReciptID.Name = "txtReciptID"
        Me.txtReciptID.Size = New System.Drawing.Size(115, 20)
        Me.txtReciptID.TabIndex = 618
        '
        'frmRecipts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1063, 323)
        Me.Controls.Add(Me.txtReciptID)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblOutstanding)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblTechName)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.dgHistory)
        Me.Controls.Add(Me.txtRecivedBy)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtChequeNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbPaymentMethod)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAmountInWords)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPaymentAmount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtInvoiceAmount)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbReciptType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustomerID)
        Me.KeyPreview = True
        Me.Name = "frmRecipts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00009"
        Me.Text = "Recipts"
        CType(Me.dgHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbReciptType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtPaymentAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAmountInWords As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbPaymentMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRecivedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dgHistory As System.Windows.Forms.DataGridView
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblOutstanding As System.Windows.Forms.Label
    Friend WithEvents RECIPT_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RECIPT_AMOUNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RECIPT_PDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtReciptID As System.Windows.Forms.TextBox
End Class
