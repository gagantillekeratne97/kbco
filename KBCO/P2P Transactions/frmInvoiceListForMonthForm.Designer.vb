<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoiceListForMonthForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceListForMonthForm))
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dgInvoiceLists = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        CType(Me.dgInvoiceLists, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(226, 44)
        Me.txtInvoiceNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(145, 22)
        Me.txtInvoiceNo.TabIndex = 53
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(394, 42)
        Me.btnProcess.Margin = New System.Windows.Forms.Padding(4)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(100, 28)
        Me.btnProcess.TabIndex = 51
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(135, 47)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(71, 16)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "Invoice No"
        '
        'dgInvoiceLists
        '
        Me.dgInvoiceLists.AllowUserToAddRows = False
        Me.dgInvoiceLists.AllowUserToDeleteRows = False
        Me.dgInvoiceLists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgInvoiceLists.Location = New System.Drawing.Point(16, 95)
        Me.dgInvoiceLists.Margin = New System.Windows.Forms.Padding(4)
        Me.dgInvoiceLists.MultiSelect = False
        Me.dgInvoiceLists.Name = "dgInvoiceLists"
        Me.dgInvoiceLists.RowHeadersWidth = 51
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgInvoiceLists.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgInvoiceLists.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgInvoiceLists.ShowCellErrors = False
        Me.dgInvoiceLists.ShowCellToolTips = False
        Me.dgInvoiceLists.ShowEditingIcon = False
        Me.dgInvoiceLists.ShowRowErrors = False
        Me.dgInvoiceLists.Size = New System.Drawing.Size(1593, 481)
        Me.dgInvoiceLists.TabIndex = 50
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.Location = New System.Drawing.Point(16, 20)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(73, 68)
        Me.btnExport.TabIndex = 52
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'frmInvoiceListForMonthForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1625, 597)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.dgInvoiceLists)
        Me.Name = "frmInvoiceListForMonthForm"
        Me.Text = "Invoice List for Month Form"
        CType(Me.dgInvoiceLists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents btnProcess As Button
    Friend WithEvents Label22 As Label
    Friend WithEvents dgInvoiceLists As DataGridView
End Class
