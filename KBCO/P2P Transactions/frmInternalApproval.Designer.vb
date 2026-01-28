<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalApproval
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInternalApproval))
        Me.btnPrintViewInternal = New System.Windows.Forms.Button()
        Me.TOT_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_COPIES = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_P_READING = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_VAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TYPE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_PN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PN_DESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgInternal = New System.Windows.Forms.DataGridView()
        Me.IR_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblNPState = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbIRType = New System.Windows.Forms.ComboBox()
        Me.txtCurrentMR = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTechName = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtIRNo = New System.Windows.Forms.TextBox()
        Me.txtCusAdd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCusCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMcRefNo = New System.Windows.Forms.Label()
        Me.txtSerial = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnCancelSO = New System.Windows.Forms.Button()
        Me.btnApprove1 = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOpenVCM = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtSpecialCase = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrintViewInternal
        '
        Me.btnPrintViewInternal.Location = New System.Drawing.Point(772, 11)
        Me.btnPrintViewInternal.Name = "btnPrintViewInternal"
        Me.btnPrintViewInternal.Size = New System.Drawing.Size(75, 23)
        Me.btnPrintViewInternal.TabIndex = 38
        Me.btnPrintViewInternal.Text = "Print"
        Me.btnPrintViewInternal.UseVisualStyleBackColor = True
        '
        'TOT_YIELD
        '
        Me.TOT_YIELD.HeaderText = "Total Yield"
        Me.TOT_YIELD.Name = "TOT_YIELD"
        Me.TOT_YIELD.ReadOnly = True
        '
        'IR_COPIES
        '
        Me.IR_COPIES.HeaderText = "Copies"
        Me.IR_COPIES.Name = "IR_COPIES"
        Me.IR_COPIES.ReadOnly = True
        Me.IR_COPIES.Width = 110
        '
        'IR_P_READING
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.IR_P_READING.DefaultCellStyle = DataGridViewCellStyle3
        Me.IR_P_READING.HeaderText = "P.Reading"
        Me.IR_P_READING.Name = "IR_P_READING"
        Me.IR_P_READING.ReadOnly = True
        Me.IR_P_READING.Width = 110
        '
        'IR_VAL
        '
        Me.IR_VAL.HeaderText = "Value"
        Me.IR_VAL.Name = "IR_VAL"
        Me.IR_VAL.ReadOnly = True
        Me.IR_VAL.Width = 110
        '
        'TYPE
        '
        Me.TYPE.HeaderText = "Type"
        Me.TYPE.Name = "TYPE"
        Me.TYPE.ReadOnly = True
        Me.TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TYPE.Width = 110
        '
        'IR_QTY
        '
        Me.IR_QTY.HeaderText = "Qty"
        Me.IR_QTY.Name = "IR_QTY"
        Me.IR_QTY.ReadOnly = True
        '
        'IR_PN
        '
        Me.IR_PN.HeaderText = "Part No"
        Me.IR_PN.Name = "IR_PN"
        Me.IR_PN.ReadOnly = True
        Me.IR_PN.Width = 150
        '
        'PN_DESC
        '
        Me.PN_DESC.HeaderText = "Description"
        Me.PN_DESC.Name = "PN_DESC"
        Me.PN_DESC.ReadOnly = True
        Me.PN_DESC.Width = 300
        '
        'dgInternal
        '
        Me.dgInternal.AllowUserToAddRows = False
        Me.dgInternal.AllowUserToDeleteRows = False
        Me.dgInternal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInternal.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PN_DESC, Me.IR_PN, Me.IR_QTY, Me.TYPE, Me.IR_VAL, Me.IR_P_READING, Me.IR_COPIES, Me.TOT_YIELD, Me.IR_YIELD})
        Me.dgInternal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgInternal.Location = New System.Drawing.Point(3, 16)
        Me.dgInternal.Name = "dgInternal"
        Me.dgInternal.ReadOnly = True
        Me.dgInternal.Size = New System.Drawing.Size(1201, 225)
        Me.dgInternal.TabIndex = 0
        '
        'IR_YIELD
        '
        Me.IR_YIELD.HeaderText = "Yield"
        Me.IR_YIELD.Name = "IR_YIELD"
        Me.IR_YIELD.ReadOnly = True
        Me.IR_YIELD.Width = 110
        '
        'lblNPState
        '
        Me.lblNPState.AutoSize = True
        Me.lblNPState.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPState.Location = New System.Drawing.Point(673, 11)
        Me.lblNPState.Name = "lblNPState"
        Me.lblNPState.Size = New System.Drawing.Size(80, 25)
        Me.lblNPState.TabIndex = 39
        Me.lblNPState.Text = "Positive"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgInternal)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 255)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1207, 244)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Internal Items"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.txtSpecialCase)
        Me.GroupBox1.Controls.Add(Me.Label3)
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
        Me.GroupBox1.Location = New System.Drawing.Point(11, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(656, 238)
        Me.GroupBox1.TabIndex = 36
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "IR Info"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(466, 132)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "IR Type"
        '
        'cmbIRType
        '
        Me.cmbIRType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbIRType.FormattingEnabled = True
        Me.cmbIRType.Items.AddRange(New Object() {"Standard", "Backup"})
        Me.cmbIRType.Location = New System.Drawing.Point(516, 129)
        Me.cmbIRType.Name = "cmbIRType"
        Me.cmbIRType.Size = New System.Drawing.Size(121, 21)
        Me.cmbIRType.TabIndex = 10
        '
        'txtCurrentMR
        '
        Me.txtCurrentMR.BackColor = System.Drawing.Color.PaleGreen
        Me.txtCurrentMR.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentMR.Location = New System.Drawing.Point(495, 41)
        Me.txtCurrentMR.Name = "txtCurrentMR"
        Me.txtCurrentMR.Size = New System.Drawing.Size(144, 24)
        Me.txtCurrentMR.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(492, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Current Reading"
        '
        'lblTechName
        '
        Me.lblTechName.AutoSize = True
        Me.lblTechName.Location = New System.Drawing.Point(280, 132)
        Me.lblTechName.Name = "lblTechName"
        Me.lblTechName.Size = New System.Drawing.Size(60, 13)
        Me.lblTechName.TabIndex = 26
        Me.lblTechName.Text = "TechName"
        '
        'txtTechCode
        '
        Me.txtTechCode.Location = New System.Drawing.Point(109, 129)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(100, 20)
        Me.txtTechCode.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 132)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Issued To"
        '
        'txtIRNo
        '
        Me.txtIRNo.Location = New System.Drawing.Point(109, 25)
        Me.txtIRNo.Name = "txtIRNo"
        Me.txtIRNo.Size = New System.Drawing.Size(100, 20)
        Me.txtIRNo.TabIndex = 0
        '
        'txtCusAdd
        '
        Me.txtCusAdd.Location = New System.Drawing.Point(109, 103)
        Me.txtCusAdd.Name = "txtCusAdd"
        Me.txtCusAdd.Size = New System.Drawing.Size(528, 20)
        Me.txtCusAdd.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IR No"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 106)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Customer Location"
        '
        'txtCusName
        '
        Me.txtCusName.Location = New System.Drawing.Point(307, 77)
        Me.txtCusName.Name = "txtCusName"
        Me.txtCusName.Size = New System.Drawing.Size(330, 20)
        Me.txtCusName.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(219, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Customer Name"
        '
        'txtCusCode
        '
        Me.txtCusCode.Location = New System.Drawing.Point(109, 77)
        Me.txtCusCode.Name = "txtCusCode"
        Me.txtCusCode.Size = New System.Drawing.Size(100, 20)
        Me.txtCusCode.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Customer Code"
        '
        'txtPNo
        '
        Me.txtPNo.Location = New System.Drawing.Point(256, 51)
        Me.txtPNo.Name = "txtPNo"
        Me.txtPNo.Size = New System.Drawing.Size(107, 20)
        Me.txtPNo.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Serial No"
        '
        'lblMcRefNo
        '
        Me.lblMcRefNo.AutoSize = True
        Me.lblMcRefNo.Location = New System.Drawing.Point(219, 54)
        Me.lblMcRefNo.Name = "lblMcRefNo"
        Me.lblMcRefNo.Size = New System.Drawing.Size(31, 13)
        Me.lblMcRefNo.TabIndex = 15
        Me.lblMcRefNo.Text = "P No"
        '
        'txtSerial
        '
        Me.txtSerial.Location = New System.Drawing.Point(109, 51)
        Me.txtSerial.Name = "txtSerial"
        Me.txtSerial.Size = New System.Drawing.Size(100, 20)
        Me.txtSerial.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.DarkRed
        Me.Label15.Location = New System.Drawing.Point(39, 506)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(407, 13)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "*If internal is negative or customer have debtors outstanding will proceed to app" & _
    "roval."
        '
        'btnCancelSO
        '
        Me.btnCancelSO.BackColor = System.Drawing.Color.Crimson
        Me.btnCancelSO.Location = New System.Drawing.Point(687, 159)
        Me.btnCancelSO.Name = "btnCancelSO"
        Me.btnCancelSO.Size = New System.Drawing.Size(122, 33)
        Me.btnCancelSO.TabIndex = 337
        Me.btnCancelSO.Text = "Cancel SO"
        Me.btnCancelSO.UseVisualStyleBackColor = False
        '
        'btnApprove1
        '
        Me.btnApprove1.BackgroundImage = CType(resources.GetObject("btnApprove1.BackgroundImage"), System.Drawing.Image)
        Me.btnApprove1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnApprove1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnApprove1.Location = New System.Drawing.Point(687, 48)
        Me.btnApprove1.Name = "btnApprove1"
        Me.btnApprove1.Size = New System.Drawing.Size(122, 47)
        Me.btnApprove1.TabIndex = 335
        Me.btnApprove1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApprove1.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.BackgroundImage = CType(resources.GetObject("btnReject.BackgroundImage"), System.Drawing.Image)
        Me.btnReject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReject.Location = New System.Drawing.Point(687, 101)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(122, 47)
        Me.btnReject.TabIndex = 336
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(837, 65)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(360, 127)
        Me.txtComment.TabIndex = 338
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(834, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 339
        Me.Label2.Text = "Comment"
        '
        'btnOpenVCM
        '
        Me.btnOpenVCM.Location = New System.Drawing.Point(1054, 11)
        Me.btnOpenVCM.Name = "btnOpenVCM"
        Me.btnOpenVCM.Size = New System.Drawing.Size(161, 23)
        Me.btnOpenVCM.TabIndex = 340
        Me.btnOpenVCM.Text = "Open Value Change Master"
        Me.btnOpenVCM.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Black
        Me.TextBox1.Location = New System.Drawing.Point(109, 181)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(528, 47)
        Me.TextBox1.TabIndex = 38
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(8, 184)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(51, 13)
        Me.Label27.TabIndex = 39
        Me.Label27.Text = "Comment"
        '
        'txtSpecialCase
        '
        Me.txtSpecialCase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpecialCase.ForeColor = System.Drawing.Color.Red
        Me.txtSpecialCase.Location = New System.Drawing.Point(109, 155)
        Me.txtSpecialCase.Name = "txtSpecialCase"
        Me.txtSpecialCase.Size = New System.Drawing.Size(231, 20)
        Me.txtSpecialCase.TabIndex = 36
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Special Case"
        '
        'frmInternalApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1222, 531)
        Me.Controls.Add(Me.btnOpenVCM)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnCancelSO)
        Me.Controls.Add(Me.btnApprove1)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.btnPrintViewInternal)
        Me.Controls.Add(Me.lblNPState)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label15)
        Me.KeyPreview = True
        Me.Name = "frmInternalApproval"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00011"
        Me.Text = "Internal Approval"
        CType(Me.dgInternal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnPrintViewInternal As System.Windows.Forms.Button
    Friend WithEvents TOT_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_COPIES As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_P_READING As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_VAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TYPE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_PN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PN_DESC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgInternal As System.Windows.Forms.DataGridView
    Friend WithEvents IR_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblNPState As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbIRType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCurrentMR As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblTechName As System.Windows.Forms.Label
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtIRNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCusAdd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCusName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCusCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblMcRefNo As System.Windows.Forms.Label
    Friend WithEvents txtSerial As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnCancelSO As System.Windows.Forms.Button
    Friend WithEvents btnApprove1 As System.Windows.Forms.Button
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenVCM As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtSpecialCase As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
