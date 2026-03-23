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
        Me.Label2.Location = New System.Drawing.Point(335, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 16)
        Me.Label2.TabIndex = 593
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(451, 54)
        Me.txtCustomerName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerName.Multiline = True
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(889, 24)
        Me.txtCustomerName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 592
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(133, 54)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(152, 22)
        Me.txtCustomerID.TabIndex = 1
        '
        'txtReciptID
        '
        Me.txtReciptID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReciptID.Location = New System.Drawing.Point(1307, 9)
        Me.txtReciptID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReciptID.Name = "txtReciptID"
        Me.txtReciptID.Size = New System.Drawing.Size(152, 22)
        Me.txtReciptID.TabIndex = 644
        Me.txtReciptID.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(1229, 14)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(62, 16)
        Me.Label16.TabIndex = 643
        Me.Label16.Text = "Recipt ID"
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.BackgroundImage = CType(resources.GetObject("btnPrint.BackgroundImage"), System.Drawing.Image)
        Me.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnPrint.Location = New System.Drawing.Point(1380, 143)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(67, 62)
        Me.btnPrint.TabIndex = 642
        Me.btnPrint.TabStop = False
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label14.Location = New System.Drawing.Point(295, 225)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(22, 16)
        Me.Label14.TabIndex = 640
        Me.Label14.Text = "F2"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label13.Location = New System.Drawing.Point(295, 192)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(22, 16)
        Me.Label13.TabIndex = 639
        Me.Label13.Text = "F2"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.ForeColor = System.Drawing.Color.Red
        Me.lblTechName.Location = New System.Drawing.Point(320, 225)
        Me.lblTechName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(21, 16)
        Me.lblTechName.TabIndex = 638
        Me.lblTechName.Text = "##"
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.ForeColor = System.Drawing.Color.Red
        Me.lblBankName.Location = New System.Drawing.Point(320, 192)
        Me.lblBankName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(21, 16)
        Me.lblBankName.TabIndex = 637
        Me.lblBankName.Text = "##"
        '
        'txtRecivedBy
        '
        Me.txtRecivedBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRecivedBy.Location = New System.Drawing.Point(133, 220)
        Me.txtRecivedBy.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRecivedBy.Name = "txtRecivedBy"
        Me.txtRecivedBy.Size = New System.Drawing.Size(152, 22)
        Me.txtRecivedBy.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 225)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 16)
        Me.Label11.TabIndex = 636
        Me.Label11.Text = "Received by"
        '
        'txtBankID
        '
        Me.txtBankID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBankID.Location = New System.Drawing.Point(133, 188)
        Me.txtBankID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBankID.Name = "txtBankID"
        Me.txtBankID.Size = New System.Drawing.Size(152, 22)
        Me.txtBankID.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 192)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 16)
        Me.Label10.TabIndex = 635
        Me.Label10.Text = "Bank ID"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtChequeNo.Location = New System.Drawing.Point(133, 156)
        Me.txtChequeNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(152, 22)
        Me.txtChequeNo.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 160)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 16)
        Me.Label9.TabIndex = 634
        Me.Label9.Text = "Cheque No"
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.FormattingEnabled = True
        Me.cmbPaymentMethod.Items.AddRange(New Object() {"CASH", "CHEQUE", "DIRECT BANK", "SVAT"})
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(133, 123)
        Me.cmbPaymentMethod.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(152, 24)
        Me.cmbPaymentMethod.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 127)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(108, 16)
        Me.Label8.TabIndex = 633
        Me.Label8.Text = "Payment Method"
        '
        'txtAmountInWords
        '
        Me.txtAmountInWords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAmountInWords.Location = New System.Drawing.Point(580, 190)
        Me.txtAmountInWords.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAmountInWords.Multiline = True
        Me.txtAmountInWords.Name = "txtAmountInWords"
        Me.txtAmountInWords.Size = New System.Drawing.Size(760, 46)
        Me.txtAmountInWords.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(465, 193)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(97, 16)
        Me.Label7.TabIndex = 632
        Me.Label7.Text = "Amount in word"
        '
        'txtPaymentAmount
        '
        Me.txtPaymentAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPaymentAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentAmount.Location = New System.Drawing.Point(580, 86)
        Me.txtPaymentAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPaymentAmount.Name = "txtPaymentAmount"
        Me.txtPaymentAmount.Size = New System.Drawing.Size(200, 26)
        Me.txtPaymentAmount.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(427, 90)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 20)
        Me.Label6.TabIndex = 631
        Me.Label6.Text = "Payment Amount"
        '
        'cmbReciptType
        '
        Me.cmbReciptType.FormattingEnabled = True
        Me.cmbReciptType.Items.AddRange(New Object() {"FULL PAYMENT", "ADVANCE PAYMENT", "PARTLY PAYMENT"})
        Me.cmbReciptType.Location = New System.Drawing.Point(133, 90)
        Me.cmbReciptType.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbReciptType.Name = "cmbReciptType"
        Me.cmbReciptType.Size = New System.Drawing.Size(152, 24)
        Me.cmbReciptType.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 94)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 16)
        Me.Label4.TabIndex = 629
        Me.Label4.Text = "Type"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(509, 364)
        Me.WebBrowser1.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(27, 25)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(27, 25)
        Me.WebBrowser1.TabIndex = 645
        '
        'dgGrid
        '
        Me.dgGrid.AllowUserToAddRows = False
        Me.dgGrid.AllowUserToDeleteRows = False
        Me.dgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.INV_NO, Me.INV_DATE, Me.AG_ID, Me.AG_NAME, Me.INV_LOC, Me.INV_VAL, Me.PAY_VAL, Me.CHECK, Me.BAL})
        Me.dgGrid.Location = New System.Drawing.Point(20, 276)
        Me.dgGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.dgGrid.Name = "dgGrid"
        Me.dgGrid.RowHeadersWidth = 51
        Me.dgGrid.Size = New System.Drawing.Size(1427, 279)
        Me.dgGrid.TabIndex = 646
        '
        'INV_NO
        '
        Me.INV_NO.HeaderText = "Invoice No"
        Me.INV_NO.MinimumWidth = 6
        Me.INV_NO.Name = "INV_NO"
        Me.INV_NO.ReadOnly = True
        Me.INV_NO.Width = 125
        '
        'INV_DATE
        '
        Me.INV_DATE.HeaderText = "INV Date"
        Me.INV_DATE.MinimumWidth = 6
        Me.INV_DATE.Name = "INV_DATE"
        Me.INV_DATE.ReadOnly = True
        Me.INV_DATE.Width = 120
        '
        'AG_ID
        '
        Me.AG_ID.HeaderText = "AG ID"
        Me.AG_ID.MinimumWidth = 6
        Me.AG_ID.Name = "AG_ID"
        Me.AG_ID.ReadOnly = True
        Me.AG_ID.Visible = False
        Me.AG_ID.Width = 125
        '
        'AG_NAME
        '
        Me.AG_NAME.HeaderText = "AG Name"
        Me.AG_NAME.MinimumWidth = 6
        Me.AG_NAME.Name = "AG_NAME"
        Me.AG_NAME.ReadOnly = True
        Me.AG_NAME.Width = 120
        '
        'INV_LOC
        '
        Me.INV_LOC.HeaderText = "Location"
        Me.INV_LOC.MinimumWidth = 6
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
        Me.INV_VAL.MinimumWidth = 6
        Me.INV_VAL.Name = "INV_VAL"
        Me.INV_VAL.ReadOnly = True
        Me.INV_VAL.Width = 125
        '
        'PAY_VAL
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.PAY_VAL.DefaultCellStyle = DataGridViewCellStyle2
        Me.PAY_VAL.HeaderText = "Payment Value"
        Me.PAY_VAL.MinimumWidth = 6
        Me.PAY_VAL.Name = "PAY_VAL"
        Me.PAY_VAL.Width = 120
        '
        'CHECK
        '
        Me.CHECK.HeaderText = "Check"
        Me.CHECK.MinimumWidth = 6
        Me.CHECK.Name = "CHECK"
        Me.CHECK.Width = 125
        '
        'BAL
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.BAL.DefaultCellStyle = DataGridViewCellStyle3
        Me.BAL.HeaderText = "Balance"
        Me.BAL.MinimumWidth = 6
        Me.BAL.Name = "BAL"
        Me.BAL.ReadOnly = True
        Me.BAL.Width = 125
        '
        'txtReciptTotal
        '
        Me.txtReciptTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReciptTotal.Location = New System.Drawing.Point(751, 562)
        Me.txtReciptTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReciptTotal.Name = "txtReciptTotal"
        Me.txtReciptTotal.ReadOnly = True
        Me.txtReciptTotal.Size = New System.Drawing.Size(152, 22)
        Me.txtReciptTotal.TabIndex = 647
        Me.txtReciptTotal.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(635, 566)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 648
        Me.Label3.Text = "Recipt Total"
        '
        'txtBalanceTotal
        '
        Me.txtBalanceTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBalanceTotal.Location = New System.Drawing.Point(1037, 562)
        Me.txtBalanceTotal.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBalanceTotal.Name = "txtBalanceTotal"
        Me.txtBalanceTotal.ReadOnly = True
        Me.txtBalanceTotal.Size = New System.Drawing.Size(152, 22)
        Me.txtBalanceTotal.TabIndex = 649
        Me.txtBalanceTotal.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(932, 566)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 16)
        Me.Label12.TabIndex = 650
        Me.Label12.Text = "Balance Total"
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.SystemColors.Control
        Me.btnView.BackgroundImage = CType(resources.GetObject("btnView.BackgroundImage"), System.Drawing.Image)
        Me.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnView.Location = New System.Drawing.Point(1380, 54)
        Me.btnView.Margin = New System.Windows.Forms.Padding(4)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(67, 62)
        Me.btnView.TabIndex = 651
        Me.btnView.TabStop = False
        Me.btnView.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(580, 244)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(760, 24)
        Me.txtRemarks.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(507, 247)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 16)
        Me.Label15.TabIndex = 653
        Me.Label15.Text = "Remarks"
        '
        'txtFindIncoice
        '
        Me.txtFindIncoice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFindIncoice.Location = New System.Drawing.Point(133, 15)
        Me.txtFindIncoice.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFindIncoice.Name = "txtFindIncoice"
        Me.txtFindIncoice.Size = New System.Drawing.Size(152, 22)
        Me.txtFindIncoice.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 20)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 16)
        Me.Label5.TabIndex = 655
        Me.Label5.Text = "Find Invoice"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSearch.Location = New System.Drawing.Point(295, 9)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(40, 37)
        Me.btnSearch.TabIndex = 656
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtOutStanding
        '
        Me.txtOutStanding.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOutStanding.Location = New System.Drawing.Point(1295, 562)
        Me.txtOutStanding.Margin = New System.Windows.Forms.Padding(4)
        Me.txtOutStanding.Name = "txtOutStanding"
        Me.txtOutStanding.ReadOnly = True
        Me.txtOutStanding.Size = New System.Drawing.Size(152, 22)
        Me.txtOutStanding.TabIndex = 657
        Me.txtOutStanding.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(1201, 566)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(78, 16)
        Me.Label17.TabIndex = 658
        Me.Label17.Text = "Outstanding"
        '
        'txtAPAmount
        '
        Me.txtAPAmount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAPAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAPAmount.Location = New System.Drawing.Point(580, 121)
        Me.txtAPAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAPAmount.Name = "txtAPAmount"
        Me.txtAPAmount.Size = New System.Drawing.Size(200, 26)
        Me.txtAPAmount.TabIndex = 9
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(351, 124)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(205, 20)
        Me.Label18.TabIndex = 660
        Me.Label18.Text = "Advance Payment Amount"
        '
        'txtBFOutstanding
        '
        Me.txtBFOutstanding.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBFOutstanding.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtBFOutstanding.ForeColor = System.Drawing.Color.Red
        Me.txtBFOutstanding.Location = New System.Drawing.Point(580, 155)
        Me.txtBFOutstanding.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBFOutstanding.Name = "txtBFOutstanding"
        Me.txtBFOutstanding.Size = New System.Drawing.Size(200, 26)
        Me.txtBFOutstanding.TabIndex = 10
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label19.Location = New System.Drawing.Point(435, 155)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(131, 20)
        Me.Label19.TabIndex = 662
        Me.Label19.Text = "B/F Outstanding"
        '
        'cbAPUse
        '
        Me.cbAPUse.AutoSize = True
        Me.cbAPUse.Location = New System.Drawing.Point(789, 126)
        Me.cbAPUse.Margin = New System.Windows.Forms.Padding(4)
        Me.cbAPUse.Name = "cbAPUse"
        Me.cbAPUse.Size = New System.Drawing.Size(39, 20)
        Me.cbAPUse.TabIndex = 663
        Me.cbAPUse.Text = "N"
        Me.cbAPUse.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Location = New System.Drawing.Point(343, 561)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(40, 37)
        Me.Button2.TabIndex = 669
        Me.Button2.TabStop = False
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtFind
        '
        Me.txtFind.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFind.Location = New System.Drawing.Point(181, 567)
        Me.txtFind.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(152, 22)
        Me.txtFind.TabIndex = 667
        Me.txtFind.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(31, 571)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(136, 16)
        Me.Label20.TabIndex = 668
        Me.Label20.Text = "Find Invoice From List"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(1395, 208)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(33, 16)
        Me.Label21.TabIndex = 670
        Me.Label21.Text = "Print"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(1393, 114)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(36, 16)
        Me.Label22.TabIndex = 670
        Me.Label22.Text = "View"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label23.Location = New System.Drawing.Point(291, 58)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(22, 16)
        Me.Label23.TabIndex = 671
        Me.Label23.Text = "F2"
        '
        'lblReciptDate
        '
        Me.lblReciptDate.AutoSize = True
        Me.lblReciptDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReciptDate.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblReciptDate.Location = New System.Drawing.Point(1024, 11)
        Me.lblReciptDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReciptDate.Name = "lblReciptDate"
        Me.lblReciptDate.Size = New System.Drawing.Size(45, 20)
        Me.lblReciptDate.TabIndex = 672
        Me.lblReciptDate.Text = "Date"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(805, 9)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(98, 20)
        Me.Label25.TabIndex = 672
        Me.Label25.Text = "Recipt Date"
        '
        'frmReciptMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1476, 599)
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
        Me.Margin = New System.Windows.Forms.Padding(4)
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
