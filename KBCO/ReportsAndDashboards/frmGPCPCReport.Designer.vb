<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGPCPCReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGPCPCReport))
        Me.dgCPC = New System.Windows.Forms.DataGridView()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTechCode = New System.Windows.Forms.TextBox()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.DEPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TECH_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TECH_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COPY_VOL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INTERNALS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CPC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgCPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgCPC
        '
        Me.dgCPC.AllowUserToAddRows = False
        Me.dgCPC.AllowUserToDeleteRows = False
        Me.dgCPC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCPC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DEPT, Me.TECH_CODE, Me.TECH_NAME, Me.COPY_VOL, Me.INTERNALS, Me.CPC})
        Me.dgCPC.Location = New System.Drawing.Point(12, 63)
        Me.dgCPC.Name = "dgCPC"
        Me.dgCPC.ReadOnly = True
        Me.dgCPC.Size = New System.Drawing.Size(699, 361)
        Me.dgCPC.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(74, 25)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(60, 13)
        Me.Label22.TabIndex = 53
        Me.Label22.Text = "Tech Code"
        '
        'txtTechCode
        '
        Me.txtTechCode.Location = New System.Drawing.Point(140, 21)
        Me.txtTechCode.Name = "txtTechCode"
        Me.txtTechCode.Size = New System.Drawing.Size(110, 20)
        Me.txtTechCode.TabIndex = 52
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.Location = New System.Drawing.Point(12, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(55, 55)
        Me.btnExport.TabIndex = 51
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(616, 19)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 50
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(444, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "End Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(263, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Start Date"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(502, 21)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(108, 20)
        Me.dtpEndDate.TabIndex = 54
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(324, 21)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(108, 20)
        Me.dtpStartDate.TabIndex = 55
        '
        'DEPT
        '
        Me.DEPT.HeaderText = "Dept"
        Me.DEPT.Name = "DEPT"
        Me.DEPT.ReadOnly = True
        '
        'TECH_CODE
        '
        Me.TECH_CODE.HeaderText = "Tech Code"
        Me.TECH_CODE.Name = "TECH_CODE"
        Me.TECH_CODE.ReadOnly = True
        '
        'TECH_NAME
        '
        Me.TECH_NAME.HeaderText = "Tech Name"
        Me.TECH_NAME.Name = "TECH_NAME"
        Me.TECH_NAME.ReadOnly = True
        Me.TECH_NAME.Width = 150
        '
        'COPY_VOL
        '
        Me.COPY_VOL.HeaderText = "Copy Volume"
        Me.COPY_VOL.Name = "COPY_VOL"
        Me.COPY_VOL.ReadOnly = True
        '
        'INTERNALS
        '
        Me.INTERNALS.HeaderText = "Internals"
        Me.INTERNALS.Name = "INTERNALS"
        Me.INTERNALS.ReadOnly = True
        '
        'CPC
        '
        Me.CPC.HeaderText = "CPC"
        Me.CPC.Name = "CPC"
        Me.CPC.ReadOnly = True
        '
        'frmGPCPCReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 436)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtTechCode)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.dgCPC)
        Me.Name = "frmGPCPCReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Cost Per Copy Report"
        CType(Me.dgCPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgCPC As System.Windows.Forms.DataGridView
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtTechCode As System.Windows.Forms.TextBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents DEPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TECH_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TECH_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COPY_VOL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INTERNALS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CPC As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
