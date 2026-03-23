<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMachineDispose
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMachineDispose))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAgreementID = New System.Windows.Forms.TextBox()
        Me.btnDispose = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblRepName = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRepCode = New System.Windows.Forms.TextBox()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtStartMRC = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtBookValue = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtContact = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtStartMR = New System.Windows.Forms.TextBox()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpInstallationDate = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtMLocation3 = New System.Windows.Forms.TextBox()
        Me.txtMLocation2 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMLocation1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSpecialCase = New System.Windows.Forms.TextBox()
        Me.cbSpecialCase = New System.Windows.Forms.CheckBox()
        Me.lblMachineStartCode = New System.Windows.Forms.Label()
        Me.txtPno = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSerialNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMachinePN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(316, 61)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 16)
        Me.Label5.TabIndex = 588
        Me.Label5.Text = "Agreement ID"
        '
        'txtAgreementID
        '
        Me.txtAgreementID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAgreementID.Location = New System.Drawing.Point(433, 58)
        Me.txtAgreementID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAgreementID.Name = "txtAgreementID"
        Me.txtAgreementID.Size = New System.Drawing.Size(132, 22)
        Me.txtAgreementID.TabIndex = 587
        '
        'btnDispose
        '
        Me.btnDispose.Location = New System.Drawing.Point(1068, 465)
        Me.btnDispose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDispose.Name = "btnDispose"
        Me.btnDispose.Size = New System.Drawing.Size(154, 28)
        Me.btnDispose.TabIndex = 586
        Me.btnDispose.Text = "Dispose Machine"
        Me.btnDispose.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(627, 376)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 16)
        Me.Label8.TabIndex = 585
        Me.Label8.Text = "Dispose Reason"
        '
        'txtComment
        '
        Me.txtComment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComment.Location = New System.Drawing.Point(744, 373)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(477, 84)
        Me.txtComment.TabIndex = 584
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Silver
        Me.GroupBox2.Controls.Add(Me.lblRepName)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtRepCode)
        Me.GroupBox2.Controls.Add(Me.lblTechName)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.txtTechCode)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 391)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(609, 112)
        Me.GroupBox2.TabIndex = 583
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tech/Sales Details "
        '
        'lblRepName
        '
        Me.lblRepName.AutoSize = True
        Me.lblRepName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblRepName.Location = New System.Drawing.Point(279, 59)
        Me.lblRepName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRepName.Name = "lblRepName"
        Me.lblRepName.Size = New System.Drawing.Size(73, 16)
        Me.lblRepName.TabIndex = 20
        Me.lblRepName.Text = "Rep Name"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(11, 59)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(69, 16)
        Me.Label21.TabIndex = 18
        Me.Label21.Text = "Rep Code"
        '
        'txtRepCode
        '
        Me.txtRepCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRepCode.Location = New System.Drawing.Point(137, 55)
        Me.txtRepCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRepCode.Name = "txtRepCode"
        Me.txtRepCode.Size = New System.Drawing.Size(132, 22)
        Me.txtRepCode.TabIndex = 22
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTechName.Location = New System.Drawing.Point(279, 27)
        Me.lblTechName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(113, 16)
        Me.lblTechName.TabIndex = 16
        Me.lblTechName.Text = "Technician Name"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 27)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(109, 16)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Technician Code"
        '
        'txtTechCode
        '
        Me.txtTechCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechCode.Location = New System.Drawing.Point(137, 23)
        Me.txtTechCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(132, 22)
        Me.txtTechCode.TabIndex = 21
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Silver
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtStartMRC)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtBookValue)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.txtTel)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtContact)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtStartMR)
        Me.GroupBox1.Controls.Add(Me.lblMachineName)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.dtpInstallationDate)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtDept)
        Me.GroupBox1.Controls.Add(Me.txtMLocation3)
        Me.GroupBox1.Controls.Add(Me.txtMLocation2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtMLocation1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtSpecialCase)
        Me.GroupBox1.Controls.Add(Me.cbSpecialCase)
        Me.GroupBox1.Controls.Add(Me.lblMachineStartCode)
        Me.GroupBox1.Controls.Add(Me.txtPno)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtSerialNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtMachinePN)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 122)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1232, 244)
        Me.GroupBox1.TabIndex = 582
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Machine Detials"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(285, 187)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 16)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Start M/R Color"
        '
        'txtStartMRC
        '
        Me.txtStartMRC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStartMRC.Location = New System.Drawing.Point(415, 182)
        Me.txtStartMRC.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStartMRC.Name = "txtStartMRC"
        Me.txtStartMRC.Size = New System.Drawing.Size(132, 22)
        Me.txtStartMRC.TabIndex = 42
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(8, 217)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(77, 16)
        Me.Label22.TabIndex = 41
        Me.Label22.Text = "Book Value"
        '
        'txtBookValue
        '
        Me.txtBookValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBookValue.Location = New System.Drawing.Point(137, 213)
        Me.txtBookValue.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBookValue.Name = "txtBookValue"
        Me.txtBookValue.Size = New System.Drawing.Size(132, 22)
        Me.txtBookValue.TabIndex = 40
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(992, 155)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(27, 16)
        Me.Label26.TabIndex = 39
        Me.Label26.Text = "Tel"
        '
        'txtTel
        '
        Me.txtTel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTel.Location = New System.Drawing.Point(1029, 151)
        Me.txtTel.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(173, 22)
        Me.txtTel.TabIndex = 14
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(635, 155)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(52, 16)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "Contact"
        '
        'txtContact
        '
        Me.txtContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContact.Location = New System.Drawing.Point(725, 151)
        Me.txtContact.Margin = New System.Windows.Forms.Padding(4)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(232, 22)
        Me.txtContact.TabIndex = 13
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(8, 186)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(62, 16)
        Me.Label23.TabIndex = 35
        Me.Label23.Text = "Start M/R"
        '
        'txtStartMR
        '
        Me.txtStartMR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStartMR.Location = New System.Drawing.Point(137, 181)
        Me.txtStartMR.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStartMR.Name = "txtStartMR"
        Me.txtStartMR.Size = New System.Drawing.Size(132, 22)
        Me.txtStartMR.TabIndex = 18
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMachineName.Location = New System.Drawing.Point(281, 20)
        Me.lblMachineName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(99, 16)
        Me.lblMachineName.TabIndex = 33
        Me.lblMachineName.Text = "Machine Model"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 154)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 16)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Installation"
        '
        'dtpInstallationDate
        '
        Me.dtpInstallationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInstallationDate.Location = New System.Drawing.Point(137, 149)
        Me.dtpInstallationDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpInstallationDate.Name = "dtpInstallationDate"
        Me.dtpInstallationDate.Size = New System.Drawing.Size(132, 22)
        Me.dtpInstallationDate.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(635, 123)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 16)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Department"
        '
        'txtDept
        '
        Me.txtDept.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDept.Location = New System.Drawing.Point(725, 119)
        Me.txtDept.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDept.Name = "txtDept"
        Me.txtDept.Size = New System.Drawing.Size(477, 22)
        Me.txtDept.TabIndex = 12
        '
        'txtMLocation3
        '
        Me.txtMLocation3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation3.Location = New System.Drawing.Point(725, 87)
        Me.txtMLocation3.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMLocation3.Name = "txtMLocation3"
        Me.txtMLocation3.Size = New System.Drawing.Size(477, 22)
        Me.txtMLocation3.TabIndex = 11
        '
        'txtMLocation2
        '
        Me.txtMLocation2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation2.Location = New System.Drawing.Point(725, 55)
        Me.txtMLocation2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMLocation2.Name = "txtMLocation2"
        Me.txtMLocation2.Size = New System.Drawing.Size(477, 22)
        Me.txtMLocation2.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(635, 27)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 16)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "M Location"
        '
        'txtMLocation1
        '
        Me.txtMLocation1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation1.Location = New System.Drawing.Point(725, 23)
        Me.txtMLocation1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMLocation1.Name = "txtMLocation1"
        Me.txtMLocation1.Size = New System.Drawing.Size(477, 22)
        Me.txtMLocation1.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 118)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Special Case"
        '
        'txtSpecialCase
        '
        Me.txtSpecialCase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpecialCase.Location = New System.Drawing.Point(197, 113)
        Me.txtSpecialCase.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSpecialCase.Name = "txtSpecialCase"
        Me.txtSpecialCase.Size = New System.Drawing.Size(379, 22)
        Me.txtSpecialCase.TabIndex = 8
        '
        'cbSpecialCase
        '
        Me.cbSpecialCase.AutoSize = True
        Me.cbSpecialCase.Location = New System.Drawing.Point(137, 116)
        Me.cbSpecialCase.Margin = New System.Windows.Forms.Padding(4)
        Me.cbSpecialCase.Name = "cbSpecialCase"
        Me.cbSpecialCase.Size = New System.Drawing.Size(47, 20)
        Me.cbSpecialCase.TabIndex = 7
        Me.cbSpecialCase.Text = "No"
        Me.cbSpecialCase.UseVisualStyleBackColor = True
        '
        'lblMachineStartCode
        '
        Me.lblMachineStartCode.AutoSize = True
        Me.lblMachineStartCode.Location = New System.Drawing.Point(8, 85)
        Me.lblMachineStartCode.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMachineStartCode.Name = "lblMachineStartCode"
        Me.lblMachineStartCode.Size = New System.Drawing.Size(37, 16)
        Me.lblMachineStartCode.TabIndex = 14
        Me.lblMachineStartCode.Text = "P No"
        '
        'txtPno
        '
        Me.txtPno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPno.Location = New System.Drawing.Point(137, 81)
        Me.txtPno.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPno.Name = "txtPno"
        Me.txtPno.Size = New System.Drawing.Size(132, 22)
        Me.txtPno.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 53)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 16)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Serial No"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSerialNo.Location = New System.Drawing.Point(137, 49)
        Me.txtSerialNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(276, 22)
        Me.txtSerialNo.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 21)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Machine PN"
        '
        'txtMachinePN
        '
        Me.txtMachinePN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMachinePN.Location = New System.Drawing.Point(137, 16)
        Me.txtMachinePN.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMachinePN.Name = "txtMachinePN"
        Me.txtMachinePN.Size = New System.Drawing.Size(132, 22)
        Me.txtMachinePN.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 24)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 16)
        Me.Label3.TabIndex = 581
        Me.Label3.Text = "Sn or Pno"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSearch.Location = New System.Drawing.Point(306, 13)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(40, 37)
        Me.btnSearch.TabIndex = 580
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(132, 21)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(165, 22)
        Me.txtSearch.TabIndex = 579
        Me.txtSearch.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 93)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 16)
        Me.Label2.TabIndex = 578
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(132, 90)
        Me.txtCustomerName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(1117, 22)
        Me.txtCustomerName.TabIndex = 576
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 61)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 577
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(132, 58)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(132, 22)
        Me.txtCustomerID.TabIndex = 575
        '
        'frmMachineDispose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1264, 516)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAgreementID)
        Me.Controls.Add(Me.btnDispose)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustomerName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCustomerID)
        Me.Name = "frmMachineDispose"
        Me.Tag = "A00017"
        Me.Text = "RETURN MACHINE DISPOSE"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label5 As Label
    Friend WithEvents txtAgreementID As TextBox
    Friend WithEvents btnDispose As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents txtComment As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblRepName As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtRepCode As TextBox
    Friend WithEvents lblTechName As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents txtTechCode As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtStartMRC As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txtBookValue As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txtTel As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents txtContact As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents txtStartMR As TextBox
    Friend WithEvents lblMachineName As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents dtpInstallationDate As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents txtDept As TextBox
    Friend WithEvents txtMLocation3 As TextBox
    Friend WithEvents txtMLocation2 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtMLocation1 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtSpecialCase As TextBox
    Friend WithEvents cbSpecialCase As CheckBox
    Friend WithEvents lblMachineStartCode As Label
    Friend WithEvents txtPno As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtSerialNo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtMachinePN As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtCustomerName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCustomerID As TextBox
End Class
