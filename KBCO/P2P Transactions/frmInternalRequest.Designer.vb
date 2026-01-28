<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalRequest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInternalRequest))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIRNo = New System.Windows.Forms.TextBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.txtSerial = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPNo = New System.Windows.Forms.TextBox()
        Me.lblMcRefNo = New System.Windows.Forms.Label()
        Me.txtCusCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCusAdd = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSpecialCase = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbIRType = New System.Windows.Forms.ComboBox()
        Me.txtCurrentMR = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgInternal = New System.Windows.Forms.DataGridView()
        Me.PN_DESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_PN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TYPE = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IR_VAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_P_READING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_COPIES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TOT_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnViewInternal = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtViewInternalNo = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblSelectedPrinter = New System.Windows.Forms.Label()
        Me.cmbPrinterList = New System.Windows.Forms.ComboBox()
        Me.btnPrintViewInternal = New System.Windows.Forms.Button()
        Me.txtVICusName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtVICusCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNPState = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnViewBackupHistory = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtRRepCode = New System.Windows.Forms.TextBox()
        Me.btnRptClear = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtRCusID = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRTechCode = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtRSN = New System.Windows.Forms.TextBox()
        Me.dtpREndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpRStartDate = New System.Windows.Forms.DateTimePicker()
        Me.btnGenarateReport = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbSelectReport = New System.Windows.Forms.ComboBox()
        Me.lblLInvDate = New System.Windows.Forms.Label()
        Me.lblIRNo = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblDebtors = New System.Windows.Forms.Label()
        Me.bgWorkerStarup = New System.ComponentModel.BackgroundWorker()
        Me.bgWorkerDabtorsCheck = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IR No"
        '
        'txtIRNo
        '
        Me.txtIRNo.Location = New System.Drawing.Point(148, 23)
        Me.txtIRNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtIRNo.Name = "txtIRNo"
        Me.txtIRNo.Size = New System.Drawing.Size(132, 22)
        Me.txtIRNo.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(148, 55)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(223, 22)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.TabStop = False
        '
        'txtSerial
        '
        Me.txtSerial.Location = New System.Drawing.Point(148, 87)
        Me.txtSerial.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.Size = New System.Drawing.Size(132, 22)
        Me.txtSerial.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 91)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Serial No"
        '
        'txtPNo
        '
        Me.txtPNo.Location = New System.Drawing.Point(344, 87)
        Me.txtPNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPNo.Name = "txtPNo"
        Me.txtPNo.Size = New System.Drawing.Size(141, 22)
        Me.txtPNo.TabIndex = 4
        '
        'lblMcRefNo
        '
        Me.lblMcRefNo.AutoSize = True
        Me.lblMcRefNo.Location = New System.Drawing.Point(295, 91)
        Me.lblMcRefNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMcRefNo.Name = "lblMcRefNo"
        Me.lblMcRefNo.Size = New System.Drawing.Size(37, 16)
        Me.lblMcRefNo.TabIndex = 15
        Me.lblMcRefNo.Text = "P No"
        '
        'txtCusCode
        '
        Me.txtCusCode.Location = New System.Drawing.Point(148, 119)
        Me.txtCusCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCusCode.Name = "txtCusCode"
        Me.txtCusCode.Size = New System.Drawing.Size(132, 22)
        Me.txtCusCode.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 123)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Customer Code"
        '
        'txtCusName
        '
        Me.txtCusName.Location = New System.Drawing.Point(412, 119)
        Me.txtCusName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCusName.Name = "txtCusName"
        Me.txtCusName.Size = New System.Drawing.Size(439, 22)
        Me.txtCusName.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(295, 123)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Customer Name"
        '
        'txtCusAdd
        '
        Me.txtCusAdd.Location = New System.Drawing.Point(148, 151)
        Me.txtCusAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCusAdd.Name = "txtCusAdd"
        Me.txtCusAdd.Size = New System.Drawing.Size(703, 22)
        Me.txtCusAdd.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 155)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 16)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Customer Location"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtComment)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.txtSpecialCase)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.lblModel)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbIRType)
        Me.GroupBox1.Controls.Add(Me.txtCurrentMR)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lblTechName)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtTechCode)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtIRNo)
        Me.GroupBox1.Controls.Add(Me.txtCusAdd)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCusName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.txtCusCode)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblMcRefNo)
        Me.GroupBox1.Controls.Add(Me.txtSerial)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(867, 311)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "IR Info"
        '
        'txtComment
        '
        Me.txtComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComment.ForeColor = System.Drawing.Color.Black
        Me.txtComment.Location = New System.Drawing.Point(148, 246)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(703, 57)
        Me.txtComment.TabIndex = 34
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(13, 250)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 16)
        Me.Label27.TabIndex = 35
        Me.Label27.Text = "Comment"
        '
        'txtSpecialCase
        '
        Me.txtSpecialCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpecialCase.ForeColor = System.Drawing.Color.Red
        Me.txtSpecialCase.Location = New System.Drawing.Point(148, 214)
        Me.txtSpecialCase.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSpecialCase.Name = "txtSpecialCase"
        Me.txtSpecialCase.Size = New System.Drawing.Size(307, 23)
        Me.txtSpecialCase.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 218)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 16)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Special Case"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(13, 59)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(78, 16)
        Me.Label26.TabIndex = 31
        Me.Label26.Text = "S/N or P No"
        '
        'lblModel
        '
        Me.lblModel.AutoSize = True
        Me.lblModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModel.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblModel.Location = New System.Drawing.Point(500, 91)
        Me.lblModel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.Size = New System.Drawing.Size(116, 17)
        Me.lblModel.TabIndex = 30
        Me.lblModel.Text = "Machine Model"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Location = New System.Drawing.Point(380, 59)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(22, 16)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "F2"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(624, 187)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 16)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "IR Type"
        '
        'cmbIRType
        '
        Me.cmbIRType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIRType.FormattingEnabled = True
        Me.cmbIRType.Items.AddRange(New Object() {"Standard", "Backup"})
        Me.cmbIRType.Location = New System.Drawing.Point(691, 183)
        Me.cmbIRType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbIRType.Name = "cmbIRType"
        Me.cmbIRType.Size = New System.Drawing.Size(160, 24)
        Me.cmbIRType.TabIndex = 10
        '
        'txtCurrentMR
        '
        Me.txtCurrentMR.BackColor = System.Drawing.Color.PaleGreen
        Me.txtCurrentMR.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentMR.Location = New System.Drawing.Point(660, 43)
        Me.txtCurrentMR.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCurrentMR.Name = "txtCurrentMR"
        Me.txtCurrentMR.Size = New System.Drawing.Size(191, 29)
        Me.txtCurrentMR.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(656, 23)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 16)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Current Reading"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.Location = New System.Drawing.Point(376, 187)
        Me.lblTechName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(75, 16)
        Me.lblTechName.TabIndex = 26
        Me.lblTechName.Text = "TechName"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label11.Location = New System.Drawing.Point(313, 187)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 16)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "F2"
        '
        'txtTechCode
        '
        Me.txtTechCode.Location = New System.Drawing.Point(148, 183)
        Me.txtTechCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(132, 22)
        Me.txtTechCode.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 187)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 16)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Issued To"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSearch.Location = New System.Drawing.Point(432, 48)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(40, 37)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgInternal)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 334)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(1597, 297)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Internal Items"
        '
        'dgInternal
        '
        Me.dgInternal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInternal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PN_DESC, Me.IR_PN, Me.IR_QTY, Me.TYPE, Me.IR_VAL, Me.IR_P_READING, Me.IR_COPIES, Me.TOT_YIELD, Me.IR_YIELD})
        Me.dgInternal.Location = New System.Drawing.Point(8, 23)
        Me.dgInternal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgInternal.Name = "dgInternal"
        Me.dgInternal.RowHeadersWidth = 51
        Me.dgInternal.Size = New System.Drawing.Size(1571, 256)
        Me.dgInternal.TabIndex = 0
        '
        'PN_DESC
        '
        Me.PN_DESC.HeaderText = "Description"
        Me.PN_DESC.MinimumWidth = 6
        Me.PN_DESC.Name = "PN_DESC"
        Me.PN_DESC.Width = 230
        '
        'IR_PN
        '
        Me.IR_PN.HeaderText = "Part No"
        Me.IR_PN.MinimumWidth = 6
        Me.IR_PN.Name = "IR_PN"
        Me.IR_PN.Width = 120
        '
        'IR_QTY
        '
        Me.IR_QTY.HeaderText = "Qty"
        Me.IR_QTY.MinimumWidth = 6
        Me.IR_QTY.Name = "IR_QTY"
        Me.IR_QTY.Width = 125
        '
        'TYPE
        '
        Me.TYPE.HeaderText = "Type"
        Me.TYPE.Items.AddRange(New Object() {"--Select--", "TON", "DRM", "DEV", "OTH", "STA", "INK", "MAS", "HR", "PR", "BLA", "HRG"})
        Me.TYPE.MinimumWidth = 6
        Me.TYPE.Name = "TYPE"
        Me.TYPE.Width = 110
        '
        'IR_VAL
        '
        Me.IR_VAL.HeaderText = "Value"
        Me.IR_VAL.MinimumWidth = 6
        Me.IR_VAL.Name = "IR_VAL"
        Me.IR_VAL.Width = 110
        '
        'IR_P_READING
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.IR_P_READING.DefaultCellStyle = DataGridViewCellStyle2
        Me.IR_P_READING.HeaderText = "P.Reading"
        Me.IR_P_READING.MinimumWidth = 6
        Me.IR_P_READING.Name = "IR_P_READING"
        Me.IR_P_READING.Width = 110
        '
        'IR_COPIES
        '
        Me.IR_COPIES.HeaderText = "Copies"
        Me.IR_COPIES.MinimumWidth = 6
        Me.IR_COPIES.Name = "IR_COPIES"
        Me.IR_COPIES.Width = 110
        '
        'TOT_YIELD
        '
        Me.TOT_YIELD.HeaderText = "Total Yield"
        Me.TOT_YIELD.MinimumWidth = 6
        Me.TOT_YIELD.Name = "TOT_YIELD"
        Me.TOT_YIELD.Width = 125
        '
        'IR_YIELD
        '
        Me.IR_YIELD.HeaderText = "Yield"
        Me.IR_YIELD.MinimumWidth = 6
        Me.IR_YIELD.Name = "IR_YIELD"
        Me.IR_YIELD.Width = 110
        '
        'btnViewInternal
        '
        Me.btnViewInternal.Location = New System.Drawing.Point(203, 128)
        Me.btnViewInternal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnViewInternal.Name = "btnViewInternal"
        Me.btnViewInternal.Size = New System.Drawing.Size(100, 28)
        Me.btnViewInternal.TabIndex = 25
        Me.btnViewInternal.Text = "View"
        Me.btnViewInternal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 34)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 16)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Internal No"
        '
        'txtViewInternalNo
        '
        Me.txtViewInternalNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtViewInternalNo.Location = New System.Drawing.Point(117, 31)
        Me.txtViewInternalNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtViewInternalNo.Name = "txtViewInternalNo"
        Me.txtViewInternalNo.Size = New System.Drawing.Size(140, 22)
        Me.txtViewInternalNo.TabIndex = 27
        Me.txtViewInternalNo.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblSelectedPrinter)
        Me.GroupBox3.Controls.Add(Me.cmbPrinterList)
        Me.GroupBox3.Controls.Add(Me.btnPrintViewInternal)
        Me.GroupBox3.Controls.Add(Me.txtVICusName)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtVICusCode)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtViewInternalNo)
        Me.GroupBox3.Controls.Add(Me.btnViewInternal)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(891, 15)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(443, 196)
        Me.GroupBox3.TabIndex = 28
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "View Internal"
        '
        'lblSelectedPrinter
        '
        Me.lblSelectedPrinter.AutoSize = True
        Me.lblSelectedPrinter.Location = New System.Drawing.Point(16, 164)
        Me.lblSelectedPrinter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSelectedPrinter.Name = "lblSelectedPrinter"
        Me.lblSelectedPrinter.Size = New System.Drawing.Size(118, 16)
        Me.lblSelectedPrinter.TabIndex = 35
        Me.lblSelectedPrinter.Text = "Printer no selected"
        '
        'cmbPrinterList
        '
        Me.cmbPrinterList.FormattingEnabled = True
        Me.cmbPrinterList.Location = New System.Drawing.Point(16, 129)
        Me.cmbPrinterList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPrinterList.Name = "cmbPrinterList"
        Me.cmbPrinterList.Size = New System.Drawing.Size(160, 24)
        Me.cmbPrinterList.TabIndex = 34
        '
        'btnPrintViewInternal
        '
        Me.btnPrintViewInternal.Location = New System.Drawing.Point(323, 128)
        Me.btnPrintViewInternal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnPrintViewInternal.Name = "btnPrintViewInternal"
        Me.btnPrintViewInternal.Size = New System.Drawing.Size(100, 28)
        Me.btnPrintViewInternal.TabIndex = 33
        Me.btnPrintViewInternal.Text = "Print"
        Me.btnPrintViewInternal.UseVisualStyleBackColor = True
        '
        'txtVICusName
        '
        Me.txtVICusName.Location = New System.Drawing.Point(117, 95)
        Me.txtVICusName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVICusName.Name = "txtVICusName"
        Me.txtVICusName.Size = New System.Drawing.Size(309, 22)
        Me.txtVICusName.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(4, 98)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 16)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Customer Name"
        '
        'txtVICusCode
        '
        Me.txtVICusCode.Location = New System.Drawing.Point(117, 63)
        Me.txtVICusCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVICusCode.Name = "txtVICusCode"
        Me.txtVICusCode.Size = New System.Drawing.Size(153, 22)
        Me.txtVICusCode.TabIndex = 29
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(4, 66)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(100, 16)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Customer Code"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(267, 34)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 16)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "F2"
        '
        'lblNPState
        '
        Me.lblNPState.AutoSize = True
        Me.lblNPState.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPState.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblNPState.Location = New System.Drawing.Point(1076, 218)
        Me.lblNPState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNPState.Name = "lblNPState"
        Me.lblNPState.Size = New System.Drawing.Size(111, 29)
        Me.lblNPState.TabIndex = 29
        Me.lblNPState.Text = "Positive"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.DarkRed
        Me.Label15.Location = New System.Drawing.Point(11, 634)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(508, 16)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "*If internal is negative or customer have debtors outstanding will proceed to app" &
    "roval."
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(1233, 219)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 28)
        Me.btnRefresh.TabIndex = 31
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnViewBackupHistory
        '
        Me.btnViewBackupHistory.Location = New System.Drawing.Point(899, 218)
        Me.btnViewBackupHistory.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnViewBackupHistory.Name = "btnViewBackupHistory"
        Me.btnViewBackupHistory.Size = New System.Drawing.Size(169, 28)
        Me.btnViewBackupHistory.TabIndex = 34
        Me.btnViewBackupHistory.Text = "Backup Toner History"
        Me.btnViewBackupHistory.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.Controls.Add(Me.txtRRepCode)
        Me.GroupBox4.Controls.Add(Me.btnRptClear)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.txtRCusID)
        Me.GroupBox4.Controls.Add(Me.Label21)
        Me.GroupBox4.Controls.Add(Me.txtRTechCode)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtRSN)
        Me.GroupBox4.Controls.Add(Me.dtpREndDate)
        Me.GroupBox4.Controls.Add(Me.dtpRStartDate)
        Me.GroupBox4.Controls.Add(Me.btnGenarateReport)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.cmbSelectReport)
        Me.GroupBox4.Location = New System.Drawing.Point(1347, 15)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(267, 277)
        Me.GroupBox4.TabIndex = 35
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Reports"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(9, 202)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(33, 16)
        Me.Label28.TabIndex = 35
        Me.Label28.Text = "Rep"
        '
        'txtRRepCode
        '
        Me.txtRRepCode.Location = New System.Drawing.Point(84, 197)
        Me.txtRRepCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRRepCode.Name = "txtRRepCode"
        Me.txtRRepCode.Size = New System.Drawing.Size(145, 22)
        Me.txtRRepCode.TabIndex = 34
        '
        'btnRptClear
        '
        Me.btnRptClear.Location = New System.Drawing.Point(23, 238)
        Me.btnRptClear.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRptClear.Name = "btnRptClear"
        Me.btnRptClear.Size = New System.Drawing.Size(100, 28)
        Me.btnRptClear.TabIndex = 33
        Me.btnRptClear.Text = "Clear"
        Me.btnRptClear.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label25.Location = New System.Drawing.Point(232, 180)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(22, 16)
        Me.Label25.TabIndex = 32
        Me.Label25.Text = "F2"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label24.Location = New System.Drawing.Point(232, 156)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(22, 16)
        Me.Label24.TabIndex = 31
        Me.Label24.Text = "F2"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label23.Location = New System.Drawing.Point(232, 87)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(22, 16)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "F2"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(9, 180)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(30, 16)
        Me.Label22.TabIndex = 21
        Me.Label22.Text = "Cus"
        '
        'txtRCusID
        '
        Me.txtRCusID.Location = New System.Drawing.Point(84, 175)
        Me.txtRCusID.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRCusID.Name = "txtRCusID"
        Me.txtRCusID.Size = New System.Drawing.Size(145, 22)
        Me.txtRCusID.TabIndex = 20
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(9, 156)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(38, 16)
        Me.Label21.TabIndex = 19
        Me.Label21.Text = "Tech"
        '
        'txtRTechCode
        '
        Me.txtRTechCode.Location = New System.Drawing.Point(84, 151)
        Me.txtRTechCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRTechCode.Name = "txtRTechCode"
        Me.txtRTechCode.Size = New System.Drawing.Size(145, 22)
        Me.txtRTechCode.TabIndex = 18
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(9, 133)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(31, 16)
        Me.Label20.TabIndex = 17
        Me.Label20.Text = "End"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 110)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(34, 16)
        Me.Label19.TabIndex = 16
        Me.Label19.Text = "Start"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 86)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(63, 16)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "Serial No"
        '
        'txtRSN
        '
        Me.txtRSN.Location = New System.Drawing.Point(84, 81)
        Me.txtRSN.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRSN.Name = "txtRSN"
        Me.txtRSN.Size = New System.Drawing.Size(145, 22)
        Me.txtRSN.TabIndex = 14
        '
        'dtpREndDate
        '
        Me.dtpREndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpREndDate.Location = New System.Drawing.Point(84, 128)
        Me.dtpREndDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpREndDate.Name = "dtpREndDate"
        Me.dtpREndDate.Size = New System.Drawing.Size(145, 22)
        Me.dtpREndDate.TabIndex = 4
        '
        'dtpRStartDate
        '
        Me.dtpRStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRStartDate.Location = New System.Drawing.Point(84, 105)
        Me.dtpRStartDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpRStartDate.Name = "dtpRStartDate"
        Me.dtpRStartDate.Size = New System.Drawing.Size(145, 22)
        Me.dtpRStartDate.TabIndex = 3
        '
        'btnGenarateReport
        '
        Me.btnGenarateReport.Location = New System.Drawing.Point(131, 238)
        Me.btnGenarateReport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGenarateReport.Name = "btnGenarateReport"
        Me.btnGenarateReport.Size = New System.Drawing.Size(100, 28)
        Me.btnGenarateReport.TabIndex = 2
        Me.btnGenarateReport.Text = "Generate"
        Me.btnGenarateReport.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 25)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(89, 16)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "Select Report"
        '
        'cmbSelectReport
        '
        Me.cmbSelectReport.FormattingEnabled = True
        Me.cmbSelectReport.Items.AddRange(New Object() {"None", "Internal History Report", "Internal Consumption Report", "Yield by Serial Report", "Internal Cosumable Utilized Report (By Model)", "Internal Cosumable Utilized Report (By Items)", "Machine List Report", "Invoice List Report", "Invoice List For Month"})
        Me.cmbSelectReport.Location = New System.Drawing.Point(13, 48)
        Me.cmbSelectReport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbSelectReport.Name = "cmbSelectReport"
        Me.cmbSelectReport.Size = New System.Drawing.Size(244, 24)
        Me.cmbSelectReport.TabIndex = 0
        '
        'lblLInvDate
        '
        Me.lblLInvDate.AutoSize = True
        Me.lblLInvDate.ForeColor = System.Drawing.Color.Crimson
        Me.lblLInvDate.Location = New System.Drawing.Point(1127, 265)
        Me.lblLInvDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLInvDate.Name = "lblLInvDate"
        Me.lblLInvDate.Size = New System.Drawing.Size(36, 16)
        Me.lblLInvDate.TabIndex = 663
        Me.lblLInvDate.Text = "Date"
        '
        'lblIRNo
        '
        Me.lblIRNo.AutoSize = True
        Me.lblIRNo.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblIRNo.Location = New System.Drawing.Point(996, 265)
        Me.lblIRNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIRNo.Name = "lblIRNo"
        Me.lblIRNo.Size = New System.Drawing.Size(20, 16)
        Me.lblIRNo.TabIndex = 658
        Me.lblIRNo.Text = "IR"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(907, 265)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(72, 16)
        Me.Label30.TabIndex = 657
        Me.Label30.Text = "Last IR info"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(907, 302)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(108, 16)
        Me.Label29.TabIndex = 664
        Me.Label29.Text = "Debtor Customer"
        '
        'lblDebtors
        '
        Me.lblDebtors.AutoSize = True
        Me.lblDebtors.ForeColor = System.Drawing.Color.Red
        Me.lblDebtors.Location = New System.Drawing.Point(1029, 303)
        Me.lblDebtors.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDebtors.Name = "lblDebtors"
        Me.lblDebtors.Size = New System.Drawing.Size(55, 16)
        Me.lblDebtors.TabIndex = 665
        Me.lblDebtors.Text = "Debtors"
        '
        'bgWorkerStarup
        '
        '
        'bgWorkerDabtorsCheck
        '
        '
        'frmInternalRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1629, 654)
        Me.Controls.Add(Me.lblDebtors)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.lblLInvDate)
        Me.Controls.Add(Me.lblIRNo)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.btnViewBackupHistory)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lblNPState)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmInternalRequest"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00006"
        Me.Text = "Internal Reques"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIRNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents txtSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPNo As System.Windows.Forms.TextBox
    Friend WithEvents lblMcRefNo As System.Windows.Forms.Label
    Friend WithEvents txtCusCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCusName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCusAdd As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCurrentMR As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgInternal As System.Windows.Forms.DataGridView
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbIRType As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnViewInternal As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtViewInternalNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrintViewInternal As System.Windows.Forms.Button
    Friend WithEvents txtVICusName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtVICusCode As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblNPState As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnViewBackupHistory As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnGenarateReport As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbSelectReport As System.Windows.Forms.ComboBox
    Friend WithEvents dtpREndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRSN As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtRCusID As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRTechCode As System.Windows.Forms.TextBox
    Friend WithEvents btnRptClear As System.Windows.Forms.Button
    Friend WithEvents lblModel As System.Windows.Forms.Label
    Friend WithEvents cmbPrinterList As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectedPrinter As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents PN_DESC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_PN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TYPE As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents IR_VAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_P_READING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_COPIES As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TOT_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtSpecialCase As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblLInvDate As System.Windows.Forms.Label
    Friend WithEvents lblIRNo As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtRRepCode As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblDebtors As System.Windows.Forms.Label
    Friend WithEvents bgWorkerStarup As System.ComponentModel.BackgroundWorker
    Friend WithEvents bgWorkerDabtorsCheck As System.ComponentModel.BackgroundWorker
End Class
