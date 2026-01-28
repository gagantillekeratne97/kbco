<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReturnMachine
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReturnMachine))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblRepName = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtRepCode = New System.Windows.Forms.TextBox()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAgreementID = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 565
        Me.Label3.Text = "Sn or Pno"
        '
        'btnSearch
        '
        Me.btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSearch.Location = New System.Drawing.Point(229, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(30, 30)
        Me.btnSearch.TabIndex = 564
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(98, 12)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(125, 20)
        Me.txtSearch.TabIndex = 563
        Me.txtSearch.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 562
        Me.Label2.Text = "Customer Name"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(98, 68)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.Size = New System.Drawing.Size(839, 20)
        Me.txtCustomerName.TabIndex = 560
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 561
        Me.Label1.Text = "Customer ID"
        '
        'txtCustomerID
        '
        Me.txtCustomerID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerID.Location = New System.Drawing.Point(98, 42)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(100, 20)
        Me.txtCustomerID.TabIndex = 559
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
        Me.GroupBox1.Location = New System.Drawing.Point(13, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(924, 198)
        Me.GroupBox1.TabIndex = 568
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Machine Detials"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(214, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 13)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Start M/R Color"
        '
        'txtStartMRC
        '
        Me.txtStartMRC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStartMRC.Location = New System.Drawing.Point(311, 148)
        Me.txtStartMRC.Name = "txtStartMRC"
        Me.txtStartMRC.Size = New System.Drawing.Size(100, 20)
        Me.txtStartMRC.TabIndex = 42
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 176)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(62, 13)
        Me.Label22.TabIndex = 41
        Me.Label22.Text = "Book Value"
        '
        'txtBookValue
        '
        Me.txtBookValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBookValue.Location = New System.Drawing.Point(103, 173)
        Me.txtBookValue.Name = "txtBookValue"
        Me.txtBookValue.Size = New System.Drawing.Size(100, 20)
        Me.txtBookValue.TabIndex = 40
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(744, 126)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(22, 13)
        Me.Label26.TabIndex = 39
        Me.Label26.Text = "Tel"
        '
        'txtTel
        '
        Me.txtTel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTel.Location = New System.Drawing.Point(772, 123)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(131, 20)
        Me.txtTel.TabIndex = 14
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(476, 126)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(44, 13)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "Contact"
        '
        'txtContact
        '
        Me.txtContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtContact.Location = New System.Drawing.Point(544, 123)
        Me.txtContact.Name = "txtContact"
        Me.txtContact.Size = New System.Drawing.Size(175, 20)
        Me.txtContact.TabIndex = 13
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 151)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(54, 13)
        Me.Label23.TabIndex = 35
        Me.Label23.Text = "Start M/R"
        '
        'txtStartMR
        '
        Me.txtStartMR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtStartMR.Location = New System.Drawing.Point(103, 147)
        Me.txtStartMR.Name = "txtStartMR"
        Me.txtStartMR.Size = New System.Drawing.Size(100, 20)
        Me.txtStartMR.TabIndex = 18
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblMachineName.Location = New System.Drawing.Point(211, 16)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(80, 13)
        Me.lblMachineName.TabIndex = 33
        Me.lblMachineName.Text = "Machine Model"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 125)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Installation"
        '
        'dtpInstallationDate
        '
        Me.dtpInstallationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInstallationDate.Location = New System.Drawing.Point(103, 121)
        Me.dtpInstallationDate.Name = "dtpInstallationDate"
        Me.dtpInstallationDate.Size = New System.Drawing.Size(100, 20)
        Me.dtpInstallationDate.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(476, 100)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 13)
        Me.Label11.TabIndex = 23
        Me.Label11.Text = "Department"
        '
        'txtDept
        '
        Me.txtDept.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDept.Location = New System.Drawing.Point(544, 97)
        Me.txtDept.Name = "txtDept"
        Me.txtDept.Size = New System.Drawing.Size(359, 20)
        Me.txtDept.TabIndex = 12
        '
        'txtMLocation3
        '
        Me.txtMLocation3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation3.Location = New System.Drawing.Point(544, 71)
        Me.txtMLocation3.Name = "txtMLocation3"
        Me.txtMLocation3.Size = New System.Drawing.Size(359, 20)
        Me.txtMLocation3.TabIndex = 11
        '
        'txtMLocation2
        '
        Me.txtMLocation2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation2.Location = New System.Drawing.Point(544, 45)
        Me.txtMLocation2.Name = "txtMLocation2"
        Me.txtMLocation2.Size = New System.Drawing.Size(359, 20)
        Me.txtMLocation2.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(476, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "M Location"
        '
        'txtMLocation1
        '
        Me.txtMLocation1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMLocation1.Location = New System.Drawing.Point(544, 19)
        Me.txtMLocation1.Name = "txtMLocation1"
        Me.txtMLocation1.Size = New System.Drawing.Size(359, 20)
        Me.txtMLocation1.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Special Case"
        '
        'txtSpecialCase
        '
        Me.txtSpecialCase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpecialCase.Location = New System.Drawing.Point(148, 92)
        Me.txtSpecialCase.Name = "txtSpecialCase"
        Me.txtSpecialCase.Size = New System.Drawing.Size(285, 20)
        Me.txtSpecialCase.TabIndex = 8
        '
        'cbSpecialCase
        '
        Me.cbSpecialCase.AutoSize = True
        Me.cbSpecialCase.Location = New System.Drawing.Point(103, 94)
        Me.cbSpecialCase.Name = "cbSpecialCase"
        Me.cbSpecialCase.Size = New System.Drawing.Size(40, 17)
        Me.cbSpecialCase.TabIndex = 7
        Me.cbSpecialCase.Text = "No"
        Me.cbSpecialCase.UseVisualStyleBackColor = True
        '
        'lblMachineStartCode
        '
        Me.lblMachineStartCode.AutoSize = True
        Me.lblMachineStartCode.Location = New System.Drawing.Point(6, 69)
        Me.lblMachineStartCode.Name = "lblMachineStartCode"
        Me.lblMachineStartCode.Size = New System.Drawing.Size(31, 13)
        Me.lblMachineStartCode.TabIndex = 14
        Me.lblMachineStartCode.Text = "P No"
        '
        'txtPno
        '
        Me.txtPno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPno.Location = New System.Drawing.Point(103, 66)
        Me.txtPno.Name = "txtPno"
        Me.txtPno.Size = New System.Drawing.Size(100, 20)
        Me.txtPno.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Serial No"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSerialNo.Location = New System.Drawing.Point(103, 40)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(208, 20)
        Me.txtSerialNo.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Machine PN"
        '
        'txtMachinePN
        '
        Me.txtMachinePN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMachinePN.Location = New System.Drawing.Point(103, 13)
        Me.txtMachinePN.Name = "txtMachinePN"
        Me.txtMachinePN.Size = New System.Drawing.Size(100, 20)
        Me.txtMachinePN.TabIndex = 4
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
        Me.GroupBox2.Location = New System.Drawing.Point(13, 313)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(457, 91)
        Me.GroupBox2.TabIndex = 569
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tech/Sales Details "
        '
        'lblRepName
        '
        Me.lblRepName.AutoSize = True
        Me.lblRepName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblRepName.Location = New System.Drawing.Point(209, 48)
        Me.lblRepName.Name = "lblRepName"
        Me.lblRepName.Size = New System.Drawing.Size(58, 13)
        Me.lblRepName.TabIndex = 20
        Me.lblRepName.Text = "Rep Name"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 48)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(55, 13)
        Me.Label21.TabIndex = 18
        Me.Label21.Text = "Rep Code"
        '
        'txtRepCode
        '
        Me.txtRepCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRepCode.Location = New System.Drawing.Point(103, 45)
        Me.txtRepCode.Name = "txtRepCode"
        Me.txtRepCode.Size = New System.Drawing.Size(100, 20)
        Me.txtRepCode.TabIndex = 22
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTechName.Location = New System.Drawing.Point(209, 22)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(91, 13)
        Me.lblTechName.TabIndex = 16
        Me.lblTechName.Text = "Technician Name"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 22)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 13)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Technician Code"
        '
        'txtTechCode
        '
        Me.txtTechCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTechCode.Location = New System.Drawing.Point(103, 19)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(100, 20)
        Me.txtTechCode.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(489, 301)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 571
        Me.Label8.Text = "Comment"
        '
        'txtComment
        '
        Me.txtComment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComment.Location = New System.Drawing.Point(557, 298)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(359, 69)
        Me.txtComment.TabIndex = 570
        '
        'btnReturn
        '
        Me.btnReturn.Location = New System.Drawing.Point(841, 373)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(75, 23)
        Me.btnReturn.TabIndex = 572
        Me.btnReturn.Text = "Return"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(236, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 13)
        Me.Label5.TabIndex = 574
        Me.Label5.Text = "Agreement ID"
        '
        'txtAgreementID
        '
        Me.txtAgreementID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAgreementID.Location = New System.Drawing.Point(324, 42)
        Me.txtAgreementID.Name = "txtAgreementID"
        Me.txtAgreementID.Size = New System.Drawing.Size(100, 20)
        Me.txtAgreementID.TabIndex = 573
        '
        'frmReturnMachine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(948, 419)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtAgreementID)
        Me.Controls.Add(Me.btnReturn)
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
        Me.KeyPreview = True
        Me.Name = "frmReturnMachine"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00013"
        Me.Text = "Machine Return"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtBookValue As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtStartMR As System.Windows.Forms.TextBox
    Friend WithEvents lblMachineName As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpInstallationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDept As System.Windows.Forms.TextBox
    Friend WithEvents txtMLocation3 As System.Windows.Forms.TextBox
    Friend WithEvents txtMLocation2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtMLocation1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSpecialCase As System.Windows.Forms.TextBox
    Friend WithEvents cbSpecialCase As System.Windows.Forms.CheckBox
    Friend WithEvents lblMachineStartCode As System.Windows.Forms.Label
    Friend WithEvents txtPno As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSerialNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMachinePN As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRepName As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtRepCode As System.Windows.Forms.TextBox
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAgreementID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStartMRC As System.Windows.Forms.TextBox
End Class
