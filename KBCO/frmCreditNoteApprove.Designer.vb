<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditNoteApprove
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreditNoteApprove))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtCNNo = New System.Windows.Forms.TextBox()
        Me.txtReqRepCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCusCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMcRefNo = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.btnApprove1 = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txtInvoiceSubTotal = New System.Windows.Forms.TextBox()
        Me.txtInvoiceTotal = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.txtVATValue = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSSCL = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtReason)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.txtCNNo)
        Me.GroupBox1.Controls.Add(Me.txtReqRepCode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCusName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtCusCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceDate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblMcRefNo)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(656, 192)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Credit Note Info"
        '
        'txtReason
        '
        Me.txtReason.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.Color.Black
        Me.txtReason.Location = New System.Drawing.Point(109, 129)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(528, 47)
        Me.txtReason.TabIndex = 38
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(8, 132)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(44, 13)
        Me.Label27.TabIndex = 39
        Me.Label27.Text = "Reason"
        '
        'txtCNNo
        '
        Me.txtCNNo.Location = New System.Drawing.Point(109, 25)
        Me.txtCNNo.Name = "txtCNNo"
        Me.txtCNNo.Size = New System.Drawing.Size(100, 20)
        Me.txtCNNo.TabIndex = 0
        '
        'txtReqRepCode
        '
        Me.txtReqRepCode.Location = New System.Drawing.Point(109, 103)
        Me.txtReqRepCode.Name = "txtReqRepCode"
        Me.txtReqRepCode.Size = New System.Drawing.Size(202, 20)
        Me.txtReqRepCode.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CN No"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 106)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Req Rep Code"
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
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.Location = New System.Drawing.Point(256, 51)
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.ReadOnly = True
        Me.txtInvoiceDate.Size = New System.Drawing.Size(140, 20)
        Me.txtInvoiceDate.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Invoice No"
        '
        'lblMcRefNo
        '
        Me.lblMcRefNo.AutoSize = True
        Me.lblMcRefNo.Location = New System.Drawing.Point(219, 54)
        Me.lblMcRefNo.Name = "lblMcRefNo"
        Me.lblMcRefNo.Size = New System.Drawing.Size(30, 13)
        Me.lblMcRefNo.TabIndex = 15
        Me.lblMcRefNo.Text = "Date"
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(109, 51)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(100, 20)
        Me.txtInvoiceNo.TabIndex = 3
        '
        'btnApprove1
        '
        Me.btnApprove1.BackgroundImage = CType(resources.GetObject("btnApprove1.BackgroundImage"), System.Drawing.Image)
        Me.btnApprove1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnApprove1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnApprove1.Location = New System.Drawing.Point(683, 58)
        Me.btnApprove1.Name = "btnApprove1"
        Me.btnApprove1.Size = New System.Drawing.Size(122, 47)
        Me.btnApprove1.TabIndex = 337
        Me.btnApprove1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnApprove1.UseVisualStyleBackColor = True
        '
        'btnReject
        '
        Me.btnReject.BackgroundImage = CType(resources.GetObject("btnReject.BackgroundImage"), System.Drawing.Image)
        Me.btnReject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReject.Location = New System.Drawing.Point(683, 111)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(122, 47)
        Me.btnReject.TabIndex = 338
        Me.btnReject.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 210)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(793, 128)
        Me.DataGridView1.TabIndex = 339
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(541, 434)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(44, 17)
        Me.CheckBox1.TabIndex = 348
        Me.CheckBox1.Text = "Yes"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'txtInvoiceSubTotal
        '
        Me.txtInvoiceSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtInvoiceSubTotal.Location = New System.Drawing.Point(610, 354)
        Me.txtInvoiceSubTotal.Name = "txtInvoiceSubTotal"
        Me.txtInvoiceSubTotal.Size = New System.Drawing.Size(197, 23)
        Me.txtInvoiceSubTotal.TabIndex = 347
        '
        'txtInvoiceTotal
        '
        Me.txtInvoiceTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtInvoiceTotal.Location = New System.Drawing.Point(610, 500)
        Me.txtInvoiceTotal.Name = "txtInvoiceTotal"
        Me.txtInvoiceTotal.Size = New System.Drawing.Size(197, 23)
        Me.txtInvoiceTotal.TabIndex = 346
        '
        'TextBox6
        '
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.TextBox6.Location = New System.Drawing.Point(610, 466)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(197, 23)
        Me.TextBox6.TabIndex = 345
        '
        'txtVATValue
        '
        Me.txtVATValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtVATValue.Location = New System.Drawing.Point(610, 434)
        Me.txtVATValue.Name = "txtVATValue"
        Me.txtVATValue.Size = New System.Drawing.Size(197, 23)
        Me.txtVATValue.TabIndex = 344
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label13.Location = New System.Drawing.Point(480, 434)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 17)
        Me.Label13.TabIndex = 343
        Me.Label13.Text = "VAT"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(522, 466)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 17)
        Me.Label12.TabIndex = 342
        Me.Label12.Text = "Discount"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(545, 500)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 17)
        Me.Label11.TabIndex = 341
        Me.Label11.Text = "Total"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(516, 354)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 17)
        Me.Label10.TabIndex = 340
        Me.Label10.Text = "Sub Total"
        '
        'txtSSCL
        '
        Me.txtSSCL.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtSSCL.Location = New System.Drawing.Point(610, 395)
        Me.txtSSCL.Name = "txtSSCL"
        Me.txtSSCL.Size = New System.Drawing.Size(197, 23)
        Me.txtSSCL.TabIndex = 350
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(511, 395)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 349
        Me.Label2.Text = "SSCL TAX"
        '
        'frmCreditNoteApprove
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 542)
        Me.Controls.Add(Me.txtSSCL)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtInvoiceSubTotal)
        Me.Controls.Add(Me.txtInvoiceTotal)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.txtVATValue)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnApprove1)
        Me.Controls.Add(Me.btnReject)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmCreditNoteApprove"
        Me.Tag = "A00016"
        Me.Text = "frmCreditNoteApprove"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtReason As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents txtCNNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtCusName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtCusCode As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtInvoiceDate As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents lblMcRefNo As Label
    Friend WithEvents btnApprove1 As Button
    Friend WithEvents btnReject As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents txtInvoiceSubTotal As TextBox
    Friend WithEvents txtInvoiceTotal As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents txtVATValue As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtReqRepCode As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtSSCL As TextBox
    Friend WithEvents Label2 As Label
End Class
