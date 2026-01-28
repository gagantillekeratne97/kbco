<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalPrintView
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbIRType = New System.Windows.Forms.ComboBox()
        Me.txtCurrentMR = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIRNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCusAdd = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCusCode = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBeleetaRefNo = New System.Windows.Forms.TextBox()
        Me.lblDispatchNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMcRefNo = New System.Windows.Forms.Label()
        Me.txtSerial = New System.Windows.Forms.TextBox()
        Me.lblNPState = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgInternal = New System.Windows.Forms.DataGridView()
        Me.PN_DESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_PN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_VAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_P_READING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_COPIES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TOT_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPrintViewInternal = New System.Windows.Forms.Button()
        Me.lblSelectedPrinter = New System.Windows.Forms.Label()
        Me.cmbPrinterList = New System.Windows.Forms.ComboBox()
        Me.btnUploadToBeleeta = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(621, 162)
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
        Me.cmbIRType.Location = New System.Drawing.Point(688, 159)
        Me.cmbIRType.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbIRType.Name = "cmbIRType"
        Me.cmbIRType.Size = New System.Drawing.Size(160, 24)
        Me.cmbIRType.TabIndex = 10
        '
        'txtCurrentMR
        '
        Me.txtCurrentMR.BackColor = System.Drawing.Color.PaleGreen
        Me.txtCurrentMR.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentMR.Location = New System.Drawing.Point(660, 50)
        Me.txtCurrentMR.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCurrentMR.Name = "txtCurrentMR"
        Me.txtCurrentMR.Size = New System.Drawing.Size(191, 29)
        Me.txtCurrentMR.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(656, 31)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 16)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Current Reading"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.Location = New System.Drawing.Point(373, 162)
        Me.lblTechName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(75, 16)
        Me.lblTechName.TabIndex = 26
        Me.lblTechName.Text = "TechName"
        '
        'txtTechCode
        '
        Me.txtTechCode.Location = New System.Drawing.Point(145, 159)
        Me.txtTechCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(132, 22)
        Me.txtTechCode.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 162)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 16)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Issued To"
        '
        'txtIRNo
        '
        Me.txtIRNo.Location = New System.Drawing.Point(145, 31)
        Me.txtIRNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIRNo.Name = "txtIRNo"
        Me.txtIRNo.Size = New System.Drawing.Size(132, 22)
        Me.txtIRNo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 36)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IR No"
        '
        'txtCusAdd
        '
        Me.txtCusAdd.Location = New System.Drawing.Point(145, 127)
        Me.txtCusAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCusAdd.Name = "txtCusAdd"
        Me.txtCusAdd.Size = New System.Drawing.Size(703, 22)
        Me.txtCusAdd.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(11, 130)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 16)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Customer Location"
        '
        'txtCusName
        '
        Me.txtCusName.Location = New System.Drawing.Point(409, 95)
        Me.txtCusName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCusName.Name = "txtCusName"
        Me.txtCusName.Size = New System.Drawing.Size(439, 22)
        Me.txtCusName.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(292, 98)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Customer Name"
        '
        'txtCusCode
        '
        Me.txtCusCode.Location = New System.Drawing.Point(145, 95)
        Me.txtCusCode.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCusCode.Name = "txtCusCode"
        Me.txtCusCode.Size = New System.Drawing.Size(132, 22)
        Me.txtCusCode.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbIRType)
        Me.GroupBox1.Controls.Add(Me.txtCurrentMR)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lblTechName)
        Me.GroupBox1.Controls.Add(Me.txtTechCode)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtIRNo)
        Me.GroupBox1.Controls.Add(Me.txtCusAdd)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCusName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCusCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblMcRefNo)
        Me.GroupBox1.Controls.Add(Me.txtSerial)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 13)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(875, 235)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "IR Info"
        '
        'txtBeleetaRefNo
        '
        Me.txtBeleetaRefNo.Location = New System.Drawing.Point(1010, 72)
        Me.txtBeleetaRefNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBeleetaRefNo.Name = "txtBeleetaRefNo"
        Me.txtBeleetaRefNo.Size = New System.Drawing.Size(141, 22)
        Me.txtBeleetaRefNo.TabIndex = 29
        Me.txtBeleetaRefNo.Visible = False
        '
        'lblDispatchNo
        '
        Me.lblDispatchNo.AutoSize = True
        Me.lblDispatchNo.Location = New System.Drawing.Point(903, 75)
        Me.lblDispatchNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDispatchNo.Name = "lblDispatchNo"
        Me.lblDispatchNo.Size = New System.Drawing.Size(99, 16)
        Me.lblDispatchNo.TabIndex = 30
        Me.lblDispatchNo.Text = "Beleeta Ref No"
        Me.lblDispatchNo.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 98)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Customer Code"
        '
        'txtPNo
        '
        Me.txtPNo.Location = New System.Drawing.Point(341, 63)
        Me.txtPNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPNo.Name = "txtPNo"
        Me.txtPNo.Size = New System.Drawing.Size(141, 22)
        Me.txtPNo.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 66)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 16)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Serial No"
        '
        'lblMcRefNo
        '
        Me.lblMcRefNo.AutoSize = True
        Me.lblMcRefNo.Location = New System.Drawing.Point(292, 66)
        Me.lblMcRefNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMcRefNo.Name = "lblMcRefNo"
        Me.lblMcRefNo.Size = New System.Drawing.Size(37, 16)
        Me.lblMcRefNo.TabIndex = 15
        Me.lblMcRefNo.Text = "P No"
        '
        'txtSerial
        '
        Me.txtSerial.Location = New System.Drawing.Point(145, 63)
        Me.txtSerial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.Size = New System.Drawing.Size(132, 22)
        Me.txtSerial.TabIndex = 3
        '
        'lblNPState
        '
        Me.lblNPState.AutoSize = True
        Me.lblNPState.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPState.Location = New System.Drawing.Point(923, 32)
        Me.lblNPState.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNPState.Name = "lblNPState"
        Me.lblNPState.Size = New System.Drawing.Size(103, 29)
        Me.lblNPState.TabIndex = 34
        Me.lblNPState.Text = "Positive"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgInternal)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 256)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(1609, 391)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Internal Items"
        '
        'dgInternal
        '
        Me.dgInternal.AllowUserToAddRows = False
        Me.dgInternal.AllowUserToDeleteRows = False
        Me.dgInternal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInternal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PN_DESC, Me.IR_PN, Me.IR_QTY, Me.TYPE, Me.IR_VAL, Me.IR_P_READING, Me.IR_COPIES, Me.TOT_YIELD, Me.IR_YIELD})
        Me.dgInternal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgInternal.Location = New System.Drawing.Point(4, 19)
        Me.dgInternal.Margin = New System.Windows.Forms.Padding(4)
        Me.dgInternal.Name = "dgInternal"
        Me.dgInternal.ReadOnly = True
        Me.dgInternal.RowHeadersWidth = 51
        Me.dgInternal.Size = New System.Drawing.Size(1601, 368)
        Me.dgInternal.TabIndex = 0
        '
        'PN_DESC
        '
        Me.PN_DESC.HeaderText = "Description"
        Me.PN_DESC.MinimumWidth = 6
        Me.PN_DESC.Name = "PN_DESC"
        Me.PN_DESC.ReadOnly = True
        Me.PN_DESC.Width = 300
        '
        'IR_PN
        '
        Me.IR_PN.HeaderText = "Part No"
        Me.IR_PN.MinimumWidth = 6
        Me.IR_PN.Name = "IR_PN"
        Me.IR_PN.ReadOnly = True
        Me.IR_PN.Width = 150
        '
        'IR_QTY
        '
        Me.IR_QTY.HeaderText = "Qty"
        Me.IR_QTY.MinimumWidth = 6
        Me.IR_QTY.Name = "IR_QTY"
        Me.IR_QTY.ReadOnly = True
        Me.IR_QTY.Width = 125
        '
        'TYPE
        '
        Me.TYPE.HeaderText = "Type"
        Me.TYPE.MinimumWidth = 6
        Me.TYPE.Name = "TYPE"
        Me.TYPE.ReadOnly = True
        Me.TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TYPE.Width = 110
        '
        'IR_VAL
        '
        Me.IR_VAL.HeaderText = "Value"
        Me.IR_VAL.MinimumWidth = 6
        Me.IR_VAL.Name = "IR_VAL"
        Me.IR_VAL.ReadOnly = True
        Me.IR_VAL.Width = 110
        '
        'IR_P_READING
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.IR_P_READING.DefaultCellStyle = DataGridViewCellStyle7
        Me.IR_P_READING.HeaderText = "P.Reading"
        Me.IR_P_READING.MinimumWidth = 6
        Me.IR_P_READING.Name = "IR_P_READING"
        Me.IR_P_READING.ReadOnly = True
        Me.IR_P_READING.Width = 110
        '
        'IR_COPIES
        '
        Me.IR_COPIES.HeaderText = "Copies"
        Me.IR_COPIES.MinimumWidth = 6
        Me.IR_COPIES.Name = "IR_COPIES"
        Me.IR_COPIES.ReadOnly = True
        Me.IR_COPIES.Width = 110
        '
        'TOT_YIELD
        '
        Me.TOT_YIELD.HeaderText = "Total Yield"
        Me.TOT_YIELD.MinimumWidth = 6
        Me.TOT_YIELD.Name = "TOT_YIELD"
        Me.TOT_YIELD.ReadOnly = True
        Me.TOT_YIELD.Width = 125
        '
        'IR_YIELD
        '
        Me.IR_YIELD.HeaderText = "Yield"
        Me.IR_YIELD.MinimumWidth = 6
        Me.IR_YIELD.Name = "IR_YIELD"
        Me.IR_YIELD.ReadOnly = True
        Me.IR_YIELD.Width = 110
        '
        'btnPrintViewInternal
        '
        Me.btnPrintViewInternal.Location = New System.Drawing.Point(902, 105)
        Me.btnPrintViewInternal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrintViewInternal.Name = "btnPrintViewInternal"
        Me.btnPrintViewInternal.Size = New System.Drawing.Size(100, 28)
        Me.btnPrintViewInternal.TabIndex = 33
        Me.btnPrintViewInternal.Text = "Print"
        Me.btnPrintViewInternal.UseVisualStyleBackColor = True
        '
        'lblSelectedPrinter
        '
        Me.lblSelectedPrinter.AutoSize = True
        Me.lblSelectedPrinter.Location = New System.Drawing.Point(902, 175)
        Me.lblSelectedPrinter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSelectedPrinter.Name = "lblSelectedPrinter"
        Me.lblSelectedPrinter.Size = New System.Drawing.Size(118, 16)
        Me.lblSelectedPrinter.TabIndex = 37
        Me.lblSelectedPrinter.Text = "Printer no selected"
        '
        'cmbPrinterList
        '
        Me.cmbPrinterList.FormattingEnabled = True
        Me.cmbPrinterList.Location = New System.Drawing.Point(902, 140)
        Me.cmbPrinterList.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbPrinterList.Name = "cmbPrinterList"
        Me.cmbPrinterList.Size = New System.Drawing.Size(160, 24)
        Me.cmbPrinterList.TabIndex = 36
        '
        'btnUploadToBeleeta
        '
        Me.btnUploadToBeleeta.Location = New System.Drawing.Point(902, 105)
        Me.btnUploadToBeleeta.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUploadToBeleeta.Name = "btnUploadToBeleeta"
        Me.btnUploadToBeleeta.Size = New System.Drawing.Size(196, 28)
        Me.btnUploadToBeleeta.TabIndex = 38
        Me.btnUploadToBeleeta.Text = "Upload to beleeta"
        Me.btnUploadToBeleeta.UseVisualStyleBackColor = True
        Me.btnUploadToBeleeta.Visible = False
        '
        'frmInternalPrintView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1630, 654)
        Me.Controls.Add(Me.txtBeleetaRefNo)
        Me.Controls.Add(Me.lblDispatchNo)
        Me.Controls.Add(Me.btnUploadToBeleeta)
        Me.Controls.Add(Me.lblSelectedPrinter)
        Me.Controls.Add(Me.cmbPrinterList)
        Me.Controls.Add(Me.btnPrintViewInternal)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblNPState)
        Me.Controls.Add(Me.GroupBox2)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInternalPrintView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00006"
        Me.Text = "Internal Print View"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbIRType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCurrentMR As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtIRNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCusAdd As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCusName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCusCode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblMcRefNo As System.Windows.Forms.Label
    Friend WithEvents txtSerial As System.Windows.Forms.TextBox
    Friend WithEvents lblNPState As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgInternal As System.Windows.Forms.DataGridView
    Friend WithEvents btnPrintViewInternal As System.Windows.Forms.Button
    Friend WithEvents PN_DESC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_PN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TYPE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_VAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_P_READING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_COPIES As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TOT_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblSelectedPrinter As System.Windows.Forms.Label
    Friend WithEvents cmbPrinterList As System.Windows.Forms.ComboBox
    Friend WithEvents txtBeleetaRefNo As TextBox
    Friend WithEvents lblDispatchNo As Label
    Friend WithEvents btnUploadToBeleeta As Button
End Class
