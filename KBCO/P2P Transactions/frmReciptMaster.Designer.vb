<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReciptMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReciptMaster))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.txtReciptID = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.txtRecivedBy = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtBankID = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtAmountInWords = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPaymentAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbReciptType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.dgGrid = New System.Windows.Forms.DataGridView()
        Me.INV_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_VAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PAY_VAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CHECK = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtReciptTotal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBalanceTotal = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnView = New System.Windows.Forms.Button()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFindIncoice = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtOutStanding = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtAPAmount = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtBFOutstanding = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cbAPUse = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblReciptDate = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(251, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 593
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(338, 44)
        Me.txtCustomerName.Multiline = True
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(668, 20)
        Me.txtCustomerName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 592
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(100, 44)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(115, 20)
        Me.txtCustomerID.TabIndex = 1
        '
        'txtReciptID
        '
        Me.txtReciptID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReciptID.Location = New System.Drawing.Point(980, 7)
        Me.txtReciptID.Name = "txtReciptID"
        Me.txtReciptID.Size = New System.Drawing.Size(115, 20)
        Me.txtReciptID.TabIndex = 644
        Me.txtReciptID.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(922, 11)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(52, 13)
        Me.Label16.TabIndex = 643
        Me.Label16.Text = "Recipt ID"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnPrint.Location = New System.Drawing.Point(1035, 116)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(50, 50)
        Me.btnPrint.TabIndex = 642
        Me.btnPrint.TabStop = False
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label14.Location = New System.Drawing.Point(221, 183)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(19, 13)
        Me.Label14.TabIndex = 640
        Me.Label14.Text = "F2"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label13.Location = New System.Drawing.Point(221, 156)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(19, 13)
        Me.Label13.TabIndex = 639
        Me.Label13.Text = "F2"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.ForeColor = System.Drawing.Color.Red
        Me.lblTechName.Location = New System.Drawing.Point(240, 183)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(21, 13)
        Me.lblTechName.TabIndex = 638
        Me.lblTechName.Text = "##"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.ForeColor = System.Drawing.Color.Red
        Me.lblBankName.Location = New System.Drawing.Point(240, 156)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(21, 13)
        Me.lblBankName.TabIndex = 637
        Me.lblBankName.Text = "##"
        '
        'txtRecivedBy
        '
        Me.txtRecivedBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecivedBy.Location = New System.Drawing.Point(100, 179)
        Me.txtRecivedBy.Name = "txtRecivedBy"
        Me.txtRecivedBy.Size = New System.Drawing.Size(115, 20)
        Me.txtRecivedBy.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 183)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 636
        Me.Label11.Text = "Received by"
        '
        'txtBankID
        '
        Me.txtBankID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankID.Location = New System.Drawing.Point(100, 153)
        Me.txtBankID.Name = "txtBankID"
        Me.txtBankID.Size = New System.Drawing.Size(115, 20)
        Me.txtBankID.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 156)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 635
        Me.Label10.Text = "Bank ID"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChequeNo.Location = New System.Drawing.Point(100, 127)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(115, 20)
        Me.txtChequeNo.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 634
        Me.Label9.Text = "Cheque No"
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.FormattingEnabled = True
        Me.cmbPaymentMethod.Items.AddRange(New Object() {"CASH", "CHEQUE", "DIRECT BANK", "SVAT"})
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(100, 100)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(115, 21)
        Me.cmbPaymentMethod.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 103)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 13)
        Me.Label8.TabIndex = 633
        Me.Label8.Text = "Payment Method"
        '
        'txtAmountInWords
        '
        Me.txtAmountInWords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountInWords.Location = New System.Drawing.Point(435, 154)
        Me.txtAmountInWords.Multiline = True
        Me.txtAmountInWords.Name = "txtAmountInWords"
        Me.txtAmountInWords.Size = New System.Drawing.Size(571, 38)
        Me.txtAmountInWords.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(349, 157)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 632
        Me.Label7.Text = "Amount in word"
        '
        'txtPaymentAmount
        '
        Me.txtPaymentAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPaymentAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentAmount.Location = New System.Drawing.Point(435, 70)
        Me.txtPaymentAmount.Name = "txtPaymentAmount"
        Me.txtPaymentAmount.Size = New System.Drawing.Size(151, 22)
        Me.txtPaymentAmount.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(320, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 16)
        Me.Label6.TabIndex = 631
        Me.Label6.Text = "Payment Amount"
        '
        'cmbReciptType
        '
        Me.cmbReciptType.FormattingEnabled = True
        Me.cmbReciptType.Items.AddRange(New Object() {"FULL PAYMENT", "ADVANCE PAYMENT", "PARTLY PAYMENT"})
        Me.cmbReciptType.Location = New System.Drawing.Point(100, 73)
        Me.cmbReciptType.Name = "cmbReciptType"
        Me.cmbReciptType.Size = New System.Drawing.Size(115, 21)
        Me.cmbReciptType.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 76)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 629
        Me.Label4.Text = "Type"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(382, 296)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.TabIndex = 645
        '
        'dgGrid
        '
        Me.dgGrid.AllowUserToAddRows = False
        Me.dgGrid.AllowUserToDeleteRows = False
        Me.dgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.INV_NO, Me.INV_DATE, Me.AG_ID, Me.AG_NAME, Me.INV_LOC, Me.INV_VAL, Me.PAY_VAL, Me.CHECK, Me.BAL})
        Me.dgGrid.Location = New System.Drawing.Point(15, 224)
        Me.dgGrid.Name = "dgGrid"
        Me.dgGrid.Size = New System.Drawing.Size(1070, 227)
        Me.dgGrid.TabIndex = 646
        '
        'INV_NO
        '
        Me.INV_NO.HeaderText = "Invoice No"
        Me.INV_NO.Name = "INV_NO"
        Me.INV_NO.ReadOnly = True
        '
        'INV_DATE
        '
        Me.INV_DATE.HeaderText = "INV Date"
        Me.INV_DATE.Name = "INV_DATE"
        Me.INV_DATE.ReadOnly = True
        Me.INV_DATE.Width = 120
        '
        'AG_ID
        '
        Me.AG_ID.HeaderText = "AG ID"
        Me.AG_ID.Name = "AG_ID"
        Me.AG_ID.ReadOnly = True
        Me.AG_ID.Visible = False
        '
        'AG_NAME
        '
        Me.AG_NAME.HeaderText = "AG Name"
        Me.AG_NAME.Name = "AG_NAME"
        Me.AG_NAME.ReadOnly = True
        Me.AG_NAME.Width = 120
        '
        'INV_LOC
        '
        Me.INV_LOC.HeaderText = "Location"
        Me.INV_LOC.Name = "INV_LOC"
        Me.INV_LOC.ReadOnly = True
        Me.INV_LOC.Width = 200
        '
        'INV_VAL
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.INV_VAL.DefaultCellStyle = DataGridViewCellStyle1
        Me.INV_VAL.HeaderText = "Invoice Value"
        Me.INV_VAL.Name = "INV_VAL"
        Me.INV_VAL.ReadOnly = True
        '
        'PAY_VAL
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.PAY_VAL.DefaultCellStyle = DataGridViewCellStyle2
        Me.PAY_VAL.HeaderText = "Payment Value"
        Me.PAY_VAL.Name = "PAY_VAL"
        Me.PAY_VAL.Width = 120
        '
        'CHECK
        '
        Me.CHECK.HeaderText = "Check"
        Me.CHECK.Name = "CHECK"
        '
        'BAL
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.BAL.DefaultCellStyle = DataGridViewCellStyle3
        Me.BAL.HeaderText = "Balance"
        Me.BAL.Name = "BAL"
        Me.BAL.ReadOnly = True
        '
        'txtReciptTotal
        '
        Me.txtReciptTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReciptTotal.Location = New System.Drawing.Point(563, 457)
        Me.txtReciptTotal.Name = "txtReciptTotal"
        Me.txtReciptTotal.ReadOnly = True
        Me.txtReciptTotal.Size = New System.Drawing.Size(115, 20)
        Me.txtReciptTotal.TabIndex = 647
        Me.txtReciptTotal.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(476, 460)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 648
        Me.Label3.Text = "Recipt Total"
        '
        'txtBalanceTotal
        '
        Me.txtBalanceTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBalanceTotal.Location = New System.Drawing.Point(778, 457)
        Me.txtBalanceTotal.Name = "txtBalanceTotal"
        Me.txtBalanceTotal.ReadOnly = True
        Me.txtBalanceTotal.Size = New System.Drawing.Size(115, 20)
        Me.txtBalanceTotal.TabIndex = 649
        Me.txtBalanceTotal.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(699, 460)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 13)
        Me.Label12.TabIndex = 650
        Me.Label12.Text = "Balance Total"
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.SystemColors.Control
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.Location = New System.Drawing.Point(1035, 44)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(50, 50)
        Me.btnView.TabIndex = 651
        Me.btnView.TabStop = False
        Me.btnView.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(435, 198)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(571, 20)
        Me.txtRemarks.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(380, 201)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 13)
        Me.Label15.TabIndex = 653
        Me.Label15.Text = "Remarks"
        '
        'txtFindIncoice
        '
        Me.txtFindIncoice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFindIncoice.Location = New System.Drawing.Point(100, 12)
        Me.txtFindIncoice.Name = "txtFindIncoice"
        Me.txtFindIncoice.Size = New System.Drawing.Size(115, 20)
        Me.txtFindIncoice.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 655
        Me.Label5.Text = "Find Invoice"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSearch.Location = New System.Drawing.Point(221, 7)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 30)
        Me.btnSearch.TabIndex = 656
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtOutStanding
        '
        Me.txtOutStanding.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOutStanding.Location = New System.Drawing.Point(971, 457)
        Me.txtOutStanding.Name = "txtOutStanding"
        Me.txtOutStanding.ReadOnly = True
        Me.txtOutStanding.Size = New System.Drawing.Size(115, 20)
        Me.txtOutStanding.TabIndex = 657
        Me.txtOutStanding.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(901, 460)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 13)
        Me.Label17.TabIndex = 658
        Me.Label17.Text = "Outstanding"
        '
        'txtAPAmount
        '
        Me.txtAPAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAPAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPAmount.Location = New System.Drawing.Point(435, 98)
        Me.txtAPAmount.Name = "txtAPAmount"
        Me.txtAPAmount.Size = New System.Drawing.Size(151, 22)
        Me.txtAPAmount.TabIndex = 9
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(263, 101)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(165, 16)
        Me.Label18.TabIndex = 660
        Me.Label18.Text = "Advance Payment Amount"
        '
        'txtBFOutstanding
        '
        Me.txtBFOutstanding.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBFOutstanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtBFOutstanding.ForeColor = System.Drawing.Color.Red
        Me.txtBFOutstanding.Location = New System.Drawing.Point(435, 126)
        Me.txtBFOutstanding.Name = "txtBFOutstanding"
        Me.txtBFOutstanding.Size = New System.Drawing.Size(151, 22)
        Me.txtBFOutstanding.TabIndex = 10
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label19.Location = New System.Drawing.Point(326, 126)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(102, 16)
        Me.Label19.TabIndex = 662
        Me.Label19.Text = "B/F Outstanding"
        '
        'cbAPUse
        '
        Me.cbAPUse.AutoSize = True
        Me.cbAPUse.Location = New System.Drawing.Point(592, 102)
        Me.cbAPUse.Name = "cbAPUse"
        Me.cbAPUse.Size = New System.Drawing.Size(34, 17)
        Me.cbAPUse.TabIndex = 663
        Me.cbAPUse.Text = "N"
        Me.cbAPUse.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Location = New System.Drawing.Point(257, 456)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(30, 30)
        Me.Button2.TabIndex = 669
        Me.Button2.TabStop = False
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtFind
        '
        Me.txtFind.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFind.Location = New System.Drawing.Point(136, 461)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(115, 20)
        Me.txtFind.TabIndex = 667
        Me.txtFind.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(23, 464)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(110, 13)
        Me.Label20.TabIndex = 668
        Me.Label20.Text = "Find Invoice From List"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(1046, 169)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(28, 13)
        Me.Label21.TabIndex = 670
        Me.Label21.Text = "Print"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(1045, 93)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(30, 13)
        Me.Label22.TabIndex = 670
        Me.Label22.Text = "View"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label23.Location = New System.Drawing.Point(218, 47)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(19, 13)
        Me.Label23.TabIndex = 671
        Me.Label23.Text = "F2"
        '
        'lblReciptDate
        '
        Me.lblReciptDate.AutoSize = True
        Me.lblReciptDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReciptDate.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblReciptDate.Location = New System.Drawing.Point(767, 9)
        Me.lblReciptDate.Name = "lblReciptDate"
        Me.lblReciptDate.Size = New System.Drawing.Size(36, 16)
        Me.lblReciptDate.TabIndex = 672
        Me.lblReciptDate.Text = "Date"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(680, 9)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(78, 16)
        Me.Label25.TabIndex = 672
        Me.Label25.Text = "Recipt Date"
        '
        'frmReciptMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1107, 487)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.lblReciptDate)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtFind)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.cbAPUse)
        Me.Controls.Add(Me.txtBFOutstanding)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtAPAmount)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtOutStanding)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtFindIncoice)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.txtBalanceTotal)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtReciptTotal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgGrid)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.txtReciptID)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblTechName)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.txtRecivedBy)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtBankID)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtChequeNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbPaymentMethod)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtAmountInWords)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPaymentAmount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbReciptType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustomerID)
        Me.KeyPreview = True
        Me.Name = "frmReciptMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00009"
        Me.Text = "frmReciptMaster"
        CType(Me.dgGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents txtReciptID As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents txtRecivedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtBankID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbPaymentMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAmountInWords As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPaymentAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbReciptType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents dgGrid As System.Windows.Forms.DataGridView
    Friend WithEvents txtReciptTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBalanceTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFindIncoice As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtOutStanding As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAPAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtBFOutstanding As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cbAPUse As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtFind As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents INV_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_LOC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_VAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PAY_VAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CHECK As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents BAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblReciptDate As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
End Class
