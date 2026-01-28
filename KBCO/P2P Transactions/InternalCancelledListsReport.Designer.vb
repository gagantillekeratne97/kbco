<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InternalCancelledListsReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InternalCancelledListsReport))
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtRCusID = New System.Windows.Forms.TextBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.dgCancelledIR = New System.Windows.Forms.DataGridView()
        Me.btnExport = New System.Windows.Forms.Button()
        CType(Me.dgCancelledIR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(135, 47)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(43, 16)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "IR NO"
        '
        'txtRCusID
        '
        Me.txtRCusID.Location = New System.Drawing.Point(186, 45)
        Me.txtRCusID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRCusID.Name = "txtRCusID"
        Me.txtRCusID.Size = New System.Drawing.Size(145, 22)
        Me.txtRCusID.TabIndex = 53
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(397, 42)
        Me.btnProcess.Margin = New System.Windows.Forms.Padding(4)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(100, 28)
        Me.btnProcess.TabIndex = 51
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'dgCancelledIR
        '
        Me.dgCancelledIR.AllowUserToAddRows = False
        Me.dgCancelledIR.AllowUserToDeleteRows = False
        Me.dgCancelledIR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCancelledIR.Location = New System.Drawing.Point(16, 95)
        Me.dgCancelledIR.Margin = New System.Windows.Forms.Padding(4)
        Me.dgCancelledIR.MultiSelect = False
        Me.dgCancelledIR.Name = "dgCancelledIR"
        Me.dgCancelledIR.RowHeadersWidth = 51
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        Me.dgCancelledIR.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgCancelledIR.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgCancelledIR.ShowCellErrors = False
        Me.dgCancelledIR.ShowCellToolTips = False
        Me.dgCancelledIR.ShowEditingIcon = False
        Me.dgCancelledIR.ShowRowErrors = False
        Me.dgCancelledIR.Size = New System.Drawing.Size(1593, 481)
        Me.dgCancelledIR.TabIndex = 50
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
        'InternalCancelledListsReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1625, 597)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtRCusID)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.dgCancelledIR)
        Me.Name = "InternalCancelledListsReport"
        Me.Text = "InternalCancelledListsReport"
        CType(Me.dgCancelledIR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label22 As Label
    Friend WithEvents txtRCusID As TextBox
    Friend WithEvents btnExport As Button
    Friend WithEvents btnProcess As Button
    Friend WithEvents dgCancelledIR As DataGridView
End Class
