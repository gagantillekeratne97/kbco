<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRecordAdjustMaster
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
        Me.btnSetSOIP = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnUpdateSIV = New System.Windows.Forms.Button()
        Me.lbl = New System.Windows.Forms.Label()
        Me.txtAmountSIV = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInvNoSIV = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInvoiceSOIP = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnCancelInvoice = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCancelReason = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCancelInvoice = New System.Windows.Forms.TextBox()
        Me.btnOpenCustomerInternalBlock = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSetSOIP
        '
        Me.btnSetSOIP.Location = New System.Drawing.Point(103, 68)
        Me.btnSetSOIP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSetSOIP.Name = "btnSetSOIP"
        Me.btnSetSOIP.Size = New System.Drawing.Size(100, 28)
        Me.btnSetSOIP.TabIndex = 0
        Me.btnSetSOIP.Text = "Change"
        Me.btnSetSOIP.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnUpdateSIV)
        Me.GroupBox1.Controls.Add(Me.lbl)
        Me.GroupBox1.Controls.Add(Me.txtAmountSIV)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtInvNoSIV)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(267, 153)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Set Invoice Value"
        '
        'btnUpdateSIV
        '
        Me.btnUpdateSIV.Location = New System.Drawing.Point(111, 94)
        Me.btnUpdateSIV.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnUpdateSIV.Name = "btnUpdateSIV"
        Me.btnUpdateSIV.Size = New System.Drawing.Size(100, 28)
        Me.btnUpdateSIV.TabIndex = 9
        Me.btnUpdateSIV.Text = "Update"
        Me.btnUpdateSIV.UseVisualStyleBackColor = True
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.Location = New System.Drawing.Point(27, 65)
        Me.lbl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(56, 17)
        Me.lbl.TabIndex = 8
        Me.lbl.Text = "Amount"
        '
        'txtAmountSIV
        '
        Me.txtAmountSIV.Location = New System.Drawing.Point(111, 62)
        Me.txtAmountSIV.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAmountSIV.Name = "txtAmountSIV"
        Me.txtAmountSIV.Size = New System.Drawing.Size(132, 22)
        Me.txtAmountSIV.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 33)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Invoice No"
        '
        'txtInvNoSIV
        '
        Me.txtInvNoSIV.Location = New System.Drawing.Point(111, 30)
        Me.txtInvNoSIV.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtInvNoSIV.Name = "txtInvNoSIV"
        Me.txtInvNoSIV.Size = New System.Drawing.Size(132, 22)
        Me.txtInvNoSIV.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtInvoiceSOIP)
        Me.GroupBox2.Controls.Add(Me.btnSetSOIP)
        Me.GroupBox2.Location = New System.Drawing.Point(291, 15)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(267, 153)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set Original Invoice Print"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 39)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Invoice No"
        '
        'txtInvoiceSOIP
        '
        Me.txtInvoiceSOIP.Location = New System.Drawing.Point(103, 36)
        Me.txtInvoiceSOIP.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtInvoiceSOIP.Name = "txtInvoiceSOIP"
        Me.txtInvoiceSOIP.Size = New System.Drawing.Size(132, 22)
        Me.txtInvoiceSOIP.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnCancelInvoice)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txtCancelReason)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.txtCancelInvoice)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 176)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(541, 153)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Cancel Invoice"
        '
        'btnCancelInvoice
        '
        Me.btnCancelInvoice.Location = New System.Drawing.Point(139, 90)
        Me.btnCancelInvoice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancelInvoice.Name = "btnCancelInvoice"
        Me.btnCancelInvoice.Size = New System.Drawing.Size(100, 28)
        Me.btnCancelInvoice.TabIndex = 9
        Me.btnCancelInvoice.Text = "Cancel"
        Me.btnCancelInvoice.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 65)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Cancel Reason"
        '
        'txtCancelReason
        '
        Me.txtCancelReason.Location = New System.Drawing.Point(139, 60)
        Me.txtCancelReason.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCancelReason.Name = "txtCancelReason"
        Me.txtCancelReason.Size = New System.Drawing.Size(395, 22)
        Me.txtCancelReason.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 33)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Invoice No"
        '
        'txtCancelInvoice
        '
        Me.txtCancelInvoice.Location = New System.Drawing.Point(139, 30)
        Me.txtCancelInvoice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCancelInvoice.Name = "txtCancelInvoice"
        Me.txtCancelInvoice.Size = New System.Drawing.Size(132, 22)
        Me.txtCancelInvoice.TabIndex = 5
        '
        'btnOpenCustomerInternalBlock
        '
        Me.btnOpenCustomerInternalBlock.Location = New System.Drawing.Point(16, 353)
        Me.btnOpenCustomerInternalBlock.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenCustomerInternalBlock.Name = "btnOpenCustomerInternalBlock"
        Me.btnOpenCustomerInternalBlock.Size = New System.Drawing.Size(245, 28)
        Me.btnOpenCustomerInternalBlock.TabIndex = 10
        Me.btnOpenCustomerInternalBlock.Text = "Internal Customer Block"
        Me.btnOpenCustomerInternalBlock.UseVisualStyleBackColor = True
        '
        'frmRecordAdjustMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 438)
        Me.Controls.Add(Me.btnOpenCustomerInternalBlock)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmRecordAdjustMaster"
        Me.Text = "frmRecordAdjustMaster"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSetSOIP As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnUpdateSIV As System.Windows.Forms.Button
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents txtAmountSIV As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInvNoSIV As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceSOIP As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelInvoice As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCancelReason As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCancelInvoice As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenCustomerInternalBlock As System.Windows.Forms.Button
End Class
