<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBlockCustomers
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCusName = New System.Windows.Forms.TextBox()
        Me.txtCusID = New System.Windows.Forms.TextBox()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.lblBank_ID = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label6.Location = New System.Drawing.Point(538, 26)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 17)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "F2"
        '
        'txtCusName
        '
        Me.txtCusName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCusName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCusName.Location = New System.Drawing.Point(122, 55)
        Me.txtCusName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCusName.MaxLength = 40
        Me.txtCusName.Name = "txtCusName"
        Me.txtCusName.Size = New System.Drawing.Size(710, 23)
        Me.txtCusName.TabIndex = 20
        '
        'txtCusID
        '
        Me.txtCusID.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCusID.Location = New System.Drawing.Point(122, 23)
        Me.txtCusID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCusID.MaxLength = 3
        Me.txtCusID.Name = "txtCusID"
        Me.txtCusID.Size = New System.Drawing.Size(408, 22)
        Me.txtCusID.TabIndex = 19
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Location = New System.Drawing.Point(30, 58)
        Me.lblBankName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(73, 17)
        Me.lblBankName.TabIndex = 18
        Me.lblBankName.Text = "Cus Name"
        '
        'lblBank_ID
        '
        Me.lblBank_ID.AutoSize = True
        Me.lblBank_ID.Location = New System.Drawing.Point(30, 26)
        Me.lblBank_ID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBank_ID.Name = "lblBank_ID"
        Me.lblBank_ID.Size = New System.Drawing.Size(49, 17)
        Me.lblBank_ID.TabIndex = 17
        Me.lblBank_ID.Text = "Cus ID"
        '
        'txtReason
        '
        Me.txtReason.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReason.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.Location = New System.Drawing.Point(122, 110)
        Me.txtReason.Margin = New System.Windows.Forms.Padding(4)
        Me.txtReason.MaxLength = 3000
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(710, 120)
        Me.txtReason.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(135, 17)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Reason for Blocking"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(435, 238)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(397, 17)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "This Will Block adding Internals to blocked Customers"
        '
        'frmBlockCustomers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 270)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCusName)
        Me.Controls.Add(Me.txtCusID)
        Me.Controls.Add(Me.lblBankName)
        Me.Controls.Add(Me.lblBank_ID)
        Me.KeyPreview = True
        Me.Name = "frmBlockCustomers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00011"
        Me.Text = "Customer Blocking"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCusName As System.Windows.Forms.TextBox
    Friend WithEvents txtCusID As System.Windows.Forms.TextBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblBank_ID As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
