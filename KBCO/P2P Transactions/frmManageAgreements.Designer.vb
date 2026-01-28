<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageAgreements
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmManageAgreements))
        Me.COLOR_RANGE_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgColor = New System.Windows.Forms.DataGridView()
        Me.COLOR_RANGE_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COLOR_RATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dgMachineList = New System.Windows.Forms.DataGridView()
        Me.SN_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PN_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M_MODEL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.M_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.txtTRAgCode = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cmbSlabMethod = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbBilPeriod = New System.Windows.Forms.ComboBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rbtnBw = New System.Windows.Forms.RadioButton()
        Me.rbtnColor = New System.Windows.Forms.RadioButton()
        Me.dgBw = New System.Windows.Forms.DataGridView()
        Me.BW_RANGE_1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BW_RANGE_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BW_RATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbtnRental = New System.Windows.Forms.RadioButton()
        Me.txtRental = New System.Windows.Forms.TextBox()
        Me.rbtnCommitment = New System.Windows.Forms.RadioButton()
        Me.rbtnActual = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAddCustomer = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtSelectedAG = New System.Windows.Forms.TextBox()
        Me.cbNewAgreement = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbIndividual = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpAPEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpAPStart = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.rbtnInvStatusAll = New System.Windows.Forms.RadioButton()
        Me.rbtnInvStatusIndividual = New System.Windows.Forms.RadioButton()
        Me.dgAgreement = New System.Windows.Forms.DataGridView()
        Me.AG_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.cbGroup = New System.Windows.Forms.CheckBox()
        Me.btnUpdateAGName = New System.Windows.Forms.Button()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtAgreementName = New System.Windows.Forms.TextBox()
        CType(Me.dgColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgMachineList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.dgBw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgAgreement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'COLOR_RANGE_1
        '
        Me.COLOR_RANGE_1.HeaderText = "Range 1"
        Me.COLOR_RANGE_1.Name = "COLOR_RANGE_1"
        Me.COLOR_RANGE_1.Width = 130
        '
        'dgColor
        '
        Me.dgColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgColor.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.COLOR_RANGE_1, Me.COLOR_RANGE_2, Me.COLOR_RATE})
        Me.dgColor.Location = New System.Drawing.Point(34, 296)
        Me.dgColor.Name = "dgColor"
        Me.dgColor.Size = New System.Drawing.Size(406, 168)
        Me.dgColor.TabIndex = 43
        '
        'COLOR_RANGE_2
        '
        Me.COLOR_RANGE_2.HeaderText = "Range 2"
        Me.COLOR_RANGE_2.Name = "COLOR_RANGE_2"
        Me.COLOR_RANGE_2.Width = 130
        '
        'COLOR_RATE
        '
        Me.COLOR_RATE.HeaderText = "Rate"
        Me.COLOR_RATE.Name = "COLOR_RATE"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.ForestGreen
        Me.Label29.Location = New System.Drawing.Point(31, 281)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(36, 13)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "Color"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(31, 86)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(101, 13)
        Me.Label28.TabIndex = 45
        Me.Label28.Text = "Black and White"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(668, 33)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(67, 13)
        Me.Label27.TabIndex = 44
        Me.Label27.Text = "Slab Method"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(8, 276)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 47
        Me.PictureBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Silver
        Me.GroupBox3.Controls.Add(Me.dgMachineList)
        Me.GroupBox3.Controls.Add(Me.btnTransfer)
        Me.GroupBox3.Controls.Add(Me.txtTRAgCode)
        Me.GroupBox3.Controls.Add(Me.PictureBox2)
        Me.GroupBox3.Controls.Add(Me.PictureBox1)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.dgColor)
        Me.GroupBox3.Controls.Add(Me.cmbSlabMethod)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.cmbBilPeriod)
        Me.GroupBox3.Controls.Add(Me.GroupBox6)
        Me.GroupBox3.Controls.Add(Me.dgBw)
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(215, 73)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(894, 470)
        Me.GroupBox3.TabIndex = 572
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Billing Details"
        '
        'dgMachineList
        '
        Me.dgMachineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgMachineList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SN_NO, Me.PN_NO, Me.M_MODEL, Me.M_LOC})
        Me.dgMachineList.Location = New System.Drawing.Point(446, 113)
        Me.dgMachineList.Name = "dgMachineList"
        Me.dgMachineList.Size = New System.Drawing.Size(442, 351)
        Me.dgMachineList.TabIndex = 50
        '
        'SN_NO
        '
        Me.SN_NO.HeaderText = "Serial No"
        Me.SN_NO.Name = "SN_NO"
        '
        'PN_NO
        '
        Me.PN_NO.HeaderText = "P No"
        Me.PN_NO.Name = "PN_NO"
        Me.PN_NO.Width = 70
        '
        'M_MODEL
        '
        Me.M_MODEL.HeaderText = "Model"
        Me.M_MODEL.Name = "M_MODEL"
        '
        'M_LOC
        '
        Me.M_LOC.HeaderText = "Location"
        Me.M_LOC.Name = "M_LOC"
        '
        'btnTransfer
        '
        Me.btnTransfer.Location = New System.Drawing.Point(661, 84)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 23)
        Me.btnTransfer.TabIndex = 49
        Me.btnTransfer.Text = "Transfer"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'txtTRAgCode
        '
        Me.txtTRAgCode.Location = New System.Drawing.Point(742, 86)
        Me.txtTRAgCode.Name = "txtTRAgCode"
        Me.txtTRAgCode.Size = New System.Drawing.Size(115, 20)
        Me.txtTRAgCode.TabIndex = 48
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 82)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'cmbSlabMethod
        '
        Me.cmbSlabMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSlabMethod.FormattingEnabled = True
        Me.cmbSlabMethod.Items.AddRange(New Object() {"--Select--", "SLAB-1", "SLAB-2", "SLAB-3"})
        Me.cmbSlabMethod.Location = New System.Drawing.Point(736, 30)
        Me.cmbSlabMethod.Name = "cmbSlabMethod"
        Me.cmbSlabMethod.Size = New System.Drawing.Size(121, 21)
        Me.cmbSlabMethod.TabIndex = 25
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(668, 62)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(67, 13)
        Me.Label24.TabIndex = 42
        Me.Label24.Text = "Billing Period"
        '
        'cmbBilPeriod
        '
        Me.cmbBilPeriod.FormattingEnabled = True
        Me.cmbBilPeriod.Items.AddRange(New Object() {"--Select--", "1", "5", "15", "25", "31"})
        Me.cmbBilPeriod.Location = New System.Drawing.Point(736, 59)
        Me.cmbBilPeriod.Name = "cmbBilPeriod"
        Me.cmbBilPeriod.Size = New System.Drawing.Size(121, 21)
        Me.cmbBilPeriod.TabIndex = 28
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rbtnBw)
        Me.GroupBox6.Controls.Add(Me.rbtnColor)
        Me.GroupBox6.Location = New System.Drawing.Point(446, 30)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(200, 45)
        Me.GroupBox6.TabIndex = 40
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Machine Type"
        '
        'rbtnBw
        '
        Me.rbtnBw.AutoSize = True
        Me.rbtnBw.Location = New System.Drawing.Point(19, 14)
        Me.rbtnBw.Name = "rbtnBw"
        Me.rbtnBw.Size = New System.Drawing.Size(104, 17)
        Me.rbtnBw.TabIndex = 26
        Me.rbtnBw.TabStop = True
        Me.rbtnBw.Text = "Black and White"
        Me.rbtnBw.UseVisualStyleBackColor = True
        '
        'rbtnColor
        '
        Me.rbtnColor.AutoSize = True
        Me.rbtnColor.Location = New System.Drawing.Point(133, 14)
        Me.rbtnColor.Name = "rbtnColor"
        Me.rbtnColor.Size = New System.Drawing.Size(49, 17)
        Me.rbtnColor.TabIndex = 27
        Me.rbtnColor.TabStop = True
        Me.rbtnColor.Text = "Color"
        Me.rbtnColor.UseVisualStyleBackColor = True
        '
        'dgBw
        '
        Me.dgBw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBw.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BW_RANGE_1, Me.BW_RANGE_2, Me.BW_RATE})
        Me.dgBw.Location = New System.Drawing.Point(34, 101)
        Me.dgBw.Name = "dgBw"
        Me.dgBw.Size = New System.Drawing.Size(406, 163)
        Me.dgBw.TabIndex = 37
        '
        'BW_RANGE_1
        '
        Me.BW_RANGE_1.HeaderText = "Range 1"
        Me.BW_RANGE_1.Name = "BW_RANGE_1"
        Me.BW_RANGE_1.Width = 130
        '
        'BW_RANGE_2
        '
        Me.BW_RANGE_2.HeaderText = "Range 2"
        Me.BW_RANGE_2.Name = "BW_RANGE_2"
        Me.BW_RANGE_2.Width = 130
        '
        'BW_RATE
        '
        Me.BW_RATE.HeaderText = "Rate"
        Me.BW_RATE.Name = "BW_RATE"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbtnRental)
        Me.GroupBox4.Controls.Add(Me.txtRental)
        Me.GroupBox4.Controls.Add(Me.rbtnCommitment)
        Me.GroupBox4.Controls.Add(Me.rbtnActual)
        Me.GroupBox4.Location = New System.Drawing.Point(20, 22)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(411, 53)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Billing Method"
        '
        'rbtnRental
        '
        Me.rbtnRental.AutoSize = True
        Me.rbtnRental.Location = New System.Drawing.Point(203, 21)
        Me.rbtnRental.Name = "rbtnRental"
        Me.rbtnRental.Size = New System.Drawing.Size(56, 17)
        Me.rbtnRental.TabIndex = 26
        Me.rbtnRental.TabStop = True
        Me.rbtnRental.Text = "Rental"
        Me.rbtnRental.UseVisualStyleBackColor = True
        '
        'txtRental
        '
        Me.txtRental.Location = New System.Drawing.Point(290, 19)
        Me.txtRental.Name = "txtRental"
        Me.txtRental.Size = New System.Drawing.Size(115, 20)
        Me.txtRental.TabIndex = 25
        '
        'rbtnCommitment
        '
        Me.rbtnCommitment.AutoSize = True
        Me.rbtnCommitment.Location = New System.Drawing.Point(19, 21)
        Me.rbtnCommitment.Name = "rbtnCommitment"
        Me.rbtnCommitment.Size = New System.Drawing.Size(82, 17)
        Me.rbtnCommitment.TabIndex = 23
        Me.rbtnCommitment.TabStop = True
        Me.rbtnCommitment.Text = "Commitment"
        Me.rbtnCommitment.UseVisualStyleBackColor = True
        '
        'rbtnActual
        '
        Me.rbtnActual.AutoSize = True
        Me.rbtnActual.Location = New System.Drawing.Point(127, 21)
        Me.rbtnActual.Name = "rbtnActual"
        Me.rbtnActual.Size = New System.Drawing.Size(55, 17)
        Me.rbtnActual.TabIndex = 24
        Me.rbtnActual.TabStop = True
        Me.rbtnActual.Text = "Actual"
        Me.rbtnActual.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(225, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 570
        Me.Label6.Text = "F2"
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.BackgroundImage = CType(resources.GetObject("btnAddCustomer.BackgroundImage"), System.Drawing.Image)
        Me.btnAddCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAddCustomer.Location = New System.Drawing.Point(250, 6)
        Me.btnAddCustomer.Name = "btnAddCustomer"
        Me.btnAddCustomer.Size = New System.Drawing.Size(30, 30)
        Me.btnAddCustomer.TabIndex = 577
        Me.btnAddCustomer.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 503)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(103, 13)
        Me.Label19.TabIndex = 576
        Me.Label19.Text = "Selected Agreement"
        '
        'txtSelectedAG
        '
        Me.txtSelectedAG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSelectedAG.Location = New System.Drawing.Point(115, 501)
        Me.txtSelectedAG.Name = "txtSelectedAG"
        Me.txtSelectedAG.ReadOnly = True
        Me.txtSelectedAG.Size = New System.Drawing.Size(87, 20)
        Me.txtSelectedAG.TabIndex = 575
        Me.txtSelectedAG.TabStop = False
        '
        'cbNewAgreement
        '
        Me.cbNewAgreement.AutoSize = True
        Me.cbNewAgreement.Location = New System.Drawing.Point(135, 528)
        Me.cbNewAgreement.Name = "cbNewAgreement"
        Me.cbNewAgreement.Size = New System.Drawing.Size(40, 17)
        Me.cbNewAgreement.TabIndex = 574
        Me.cbNewAgreement.TabStop = False
        Me.cbNewAgreement.Text = "No"
        Me.cbNewAgreement.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 529)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(126, 13)
        Me.Label18.TabIndex = 573
        Me.Label18.Text = "Save As New Agreement"
        '
        'cbIndividual
        '
        Me.cbIndividual.AutoSize = True
        Me.cbIndividual.Location = New System.Drawing.Point(101, 87)
        Me.cbIndividual.Name = "cbIndividual"
        Me.cbIndividual.Size = New System.Drawing.Size(71, 17)
        Me.cbIndividual.TabIndex = 564
        Me.cbIndividual.Text = "Individual"
        Me.cbIndividual.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(697, 551)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(20, 13)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "To"
        '
        'dtpAPEnd
        '
        Me.dtpAPEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpAPEnd.Location = New System.Drawing.Point(724, 545)
        Me.dtpAPEnd.Name = "dtpAPEnd"
        Me.dtpAPEnd.Size = New System.Drawing.Size(100, 20)
        Me.dtpAPEnd.TabIndex = 20
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(490, 551)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(91, 13)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Agreement Period"
        '
        'dtpAPStart
        '
        Me.dtpAPStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpAPStart.Location = New System.Drawing.Point(587, 547)
        Me.dtpAPStart.Name = "dtpAPStart"
        Me.dtpAPStart.Size = New System.Drawing.Size(100, 20)
        Me.dtpAPStart.TabIndex = 19
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(241, 551)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 13)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = "Inv Status"
        '
        'rbtnInvStatusAll
        '
        Me.rbtnInvStatusAll.AutoSize = True
        Me.rbtnInvStatusAll.Location = New System.Drawing.Point(438, 549)
        Me.rbtnInvStatusAll.Name = "rbtnInvStatusAll"
        Me.rbtnInvStatusAll.Size = New System.Drawing.Size(36, 17)
        Me.rbtnInvStatusAll.TabIndex = 17
        Me.rbtnInvStatusAll.TabStop = True
        Me.rbtnInvStatusAll.Text = "All"
        Me.rbtnInvStatusAll.UseVisualStyleBackColor = True
        '
        'rbtnInvStatusIndividual
        '
        Me.rbtnInvStatusIndividual.AutoSize = True
        Me.rbtnInvStatusIndividual.Location = New System.Drawing.Point(338, 549)
        Me.rbtnInvStatusIndividual.Name = "rbtnInvStatusIndividual"
        Me.rbtnInvStatusIndividual.Size = New System.Drawing.Size(70, 17)
        Me.rbtnInvStatusIndividual.TabIndex = 16
        Me.rbtnInvStatusIndividual.TabStop = True
        Me.rbtnInvStatusIndividual.Text = "Individual"
        Me.rbtnInvStatusIndividual.UseVisualStyleBackColor = True
        '
        'dgAgreement
        '
        Me.dgAgreement.AllowUserToAddRows = False
        Me.dgAgreement.AllowUserToDeleteRows = False
        Me.dgAgreement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgAgreement.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AG_ID, Me.AG_NAME})
        Me.dgAgreement.Location = New System.Drawing.Point(9, 122)
        Me.dgAgreement.Name = "dgAgreement"
        Me.dgAgreement.ReadOnly = True
        Me.dgAgreement.Size = New System.Drawing.Size(200, 359)
        Me.dgAgreement.TabIndex = 565
        '
        'AG_ID
        '
        Me.AG_ID.HeaderText = "Agreement"
        Me.AG_ID.Name = "AG_ID"
        Me.AG_ID.ReadOnly = True
        Me.AG_ID.Visible = False
        Me.AG_ID.Width = 150
        '
        'AG_NAME
        '
        Me.AG_NAME.HeaderText = "Agreement Name"
        Me.AG_NAME.Name = "AG_NAME"
        Me.AG_NAME.ReadOnly = True
        Me.AG_NAME.Width = 120
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 563
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(101, 38)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(1008, 20)
        Me.txtCustomerName.TabIndex = 561
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 560
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(101, 12)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(100, 20)
        Me.txtCustomerID.TabIndex = 559
        '
        'cbGroup
        '
        Me.cbGroup.AutoSize = True
        Me.cbGroup.Location = New System.Drawing.Point(24, 87)
        Me.cbGroup.Name = "cbGroup"
        Me.cbGroup.Size = New System.Drawing.Size(55, 17)
        Me.cbGroup.TabIndex = 562
        Me.cbGroup.Text = "Group"
        Me.cbGroup.UseVisualStyleBackColor = True
        '
        'btnUpdateAGName
        '
        Me.btnUpdateAGName.Location = New System.Drawing.Point(198, 549)
        Me.btnUpdateAGName.Name = "btnUpdateAGName"
        Me.btnUpdateAGName.Size = New System.Drawing.Size(23, 23)
        Me.btnUpdateAGName.TabIndex = 580
        Me.btnUpdateAGName.Text = "!"
        Me.btnUpdateAGName.UseVisualStyleBackColor = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 554)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(89, 13)
        Me.Label30.TabIndex = 579
        Me.Label30.Text = "Agreement Name"
        '
        'txtAgreementName
        '
        Me.txtAgreementName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAgreementName.Location = New System.Drawing.Point(101, 551)
        Me.txtAgreementName.Name = "txtAgreementName"
        Me.txtAgreementName.Size = New System.Drawing.Size(93, 20)
        Me.txtAgreementName.TabIndex = 578
        Me.txtAgreementName.TabStop = False
        '
        'frmManageAgreements
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1124, 587)
        Me.Controls.Add(Me.btnUpdateAGName)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.txtAgreementName)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.dtpAPEnd)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dtpAPStart)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.rbtnInvStatusAll)
        Me.Controls.Add(Me.btnAddCustomer)
        Me.Controls.Add(Me.rbtnInvStatusIndividual)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtSelectedAG)
        Me.Controls.Add(Me.cbNewAgreement)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.cbIndividual)
        Me.Controls.Add(Me.dgAgreement)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustomerID)
        Me.Controls.Add(Me.cbGroup)
        Me.KeyPreview = True
        Me.Name = "frmManageAgreements"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00010"
        Me.Text = "Manage Agreements"
        CType(Me.dgColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.dgMachineList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.dgBw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgAgreement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents COLOR_RANGE_1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgColor As System.Windows.Forms.DataGridView
    Friend WithEvents COLOR_RANGE_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COLOR_RATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbSlabMethod As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbBilPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnBw As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnColor As System.Windows.Forms.RadioButton
    Friend WithEvents dgBw As System.Windows.Forms.DataGridView
    Friend WithEvents BW_RANGE_1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BW_RANGE_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BW_RATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnRental As System.Windows.Forms.RadioButton
    Friend WithEvents txtRental As System.Windows.Forms.TextBox
    Friend WithEvents rbtnCommitment As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnActual As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAddCustomer As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedAG As System.Windows.Forms.TextBox
    Friend WithEvents cbNewAgreement As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cbIndividual As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpAPEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpAPStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents rbtnInvStatusAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnInvStatusIndividual As System.Windows.Forms.RadioButton
    Friend WithEvents dgAgreement As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents cbGroup As System.Windows.Forms.CheckBox
    Friend WithEvents btnTransfer As System.Windows.Forms.Button
    Friend WithEvents txtTRAgCode As System.Windows.Forms.TextBox
    Friend WithEvents dgMachineList As System.Windows.Forms.DataGridView
    Friend WithEvents SN_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PN_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M_MODEL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents M_LOC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnUpdateAGName As System.Windows.Forms.Button
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtAgreementName As System.Windows.Forms.TextBox
    Friend WithEvents AG_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
