<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackupTonerHistory
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
        Me.dgBTH = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lblSerialNo = New System.Windows.Forms.Label()
        Me.lblCustomerID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BAK_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_IR_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_PN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_PN_DESC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_VALUE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAK_YIELD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgBTH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgBTH
        '
        Me.dgBTH.AllowUserToAddRows = False
        Me.dgBTH.AllowUserToDeleteRows = False
        Me.dgBTH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBTH.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BAK_DATE, Me.BAK_IR_NO, Me.BAK_PN, Me.BAK_PN_DESC, Me.BAK_QTY, Me.BAK_VALUE, Me.BAK_YIELD})
        Me.dgBTH.Location = New System.Drawing.Point(12, 88)
        Me.dgBTH.Name = "dgBTH"
        Me.dgBTH.ReadOnly = True
        Me.dgBTH.Size = New System.Drawing.Size(1022, 330)
        Me.dgBTH.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Customer Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Serial No "
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.Location = New System.Drawing.Point(118, 57)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(27, 13)
        Me.lblCustomerName.TabIndex = 1
        Me.lblCustomerName.Text = "xxxx"
        '
        'lblSerialNo
        '
        Me.lblSerialNo.AutoSize = True
        Me.lblSerialNo.Location = New System.Drawing.Point(118, 7)
        Me.lblSerialNo.Name = "lblSerialNo"
        Me.lblSerialNo.Size = New System.Drawing.Size(27, 13)
        Me.lblSerialNo.TabIndex = 1
        Me.lblSerialNo.Text = "xxxx"
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = True
        Me.lblCustomerID.Location = New System.Drawing.Point(118, 33)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(27, 13)
        Me.lblCustomerID.TabIndex = 2
        Me.lblCustomerID.Text = "xxxx"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Customer ID"
        '
        'BAK_DATE
        '
        Me.BAK_DATE.HeaderText = "Date"
        Me.BAK_DATE.Name = "BAK_DATE"
        Me.BAK_DATE.ReadOnly = True
        Me.BAK_DATE.Width = 150
        '
        'BAK_IR_NO
        '
        Me.BAK_IR_NO.HeaderText = "IR No"
        Me.BAK_IR_NO.Name = "BAK_IR_NO"
        Me.BAK_IR_NO.ReadOnly = True
        Me.BAK_IR_NO.Width = 120
        '
        'BAK_PN
        '
        Me.BAK_PN.HeaderText = "P/N"
        Me.BAK_PN.Name = "BAK_PN"
        Me.BAK_PN.ReadOnly = True
        '
        'BAK_PN_DESC
        '
        Me.BAK_PN_DESC.HeaderText = "P/N Desc"
        Me.BAK_PN_DESC.Name = "BAK_PN_DESC"
        Me.BAK_PN_DESC.ReadOnly = True
        Me.BAK_PN_DESC.Width = 300
        '
        'BAK_QTY
        '
        Me.BAK_QTY.HeaderText = "Qty"
        Me.BAK_QTY.Name = "BAK_QTY"
        Me.BAK_QTY.ReadOnly = True
        '
        'BAK_VALUE
        '
        Me.BAK_VALUE.HeaderText = "Value"
        Me.BAK_VALUE.Name = "BAK_VALUE"
        Me.BAK_VALUE.ReadOnly = True
        '
        'BAK_YIELD
        '
        Me.BAK_YIELD.HeaderText = "Std. Yield"
        Me.BAK_YIELD.Name = "BAK_YIELD"
        Me.BAK_YIELD.ReadOnly = True
        '
        'frmBackupTonerHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 440)
        Me.Controls.Add(Me.lblCustomerID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblSerialNo)
        Me.Controls.Add(Me.lblCustomerName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgBTH)
        Me.KeyPreview = True
        Me.Name = "frmBackupTonerHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Backup Toner History"
        CType(Me.dgBTH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgBTH As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lblSerialNo As System.Windows.Forms.Label
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents BAK_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_IR_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_PN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_PN_DESC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_VALUE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAK_YIELD As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
