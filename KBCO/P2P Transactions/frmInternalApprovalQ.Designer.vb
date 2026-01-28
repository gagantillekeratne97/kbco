<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalApprovalQ
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
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.dgApprovedInternalQ = New System.Windows.Forms.DataGridView()
        Me.DG_IR_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_IR_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_PNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_SN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_CUS_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_M_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_STATUS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(94, 7)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(110, 23)
        Me.btnOpenManually.TabIndex = 4
        Me.btnOpenManually.Text = "Open Manually"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(13, 7)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 3
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'dgApprovedInternalQ
        '
        Me.dgApprovedInternalQ.AllowUserToAddRows = False
        Me.dgApprovedInternalQ.AllowUserToDeleteRows = False
        Me.dgApprovedInternalQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgApprovedInternalQ.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_IR_NO, Me.DG_IR_DATE, Me.DG_PNO, Me.DG_SN, Me.DG_CUS_NAME, Me.DG_M_LOC, Me.IR_STATUS})
        Me.dgApprovedInternalQ.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgApprovedInternalQ.Location = New System.Drawing.Point(0, 57)
        Me.dgApprovedInternalQ.Name = "dgApprovedInternalQ"
        Me.dgApprovedInternalQ.ReadOnly = True
        Me.dgApprovedInternalQ.Size = New System.Drawing.Size(1255, 423)
        Me.dgApprovedInternalQ.TabIndex = 2
        '
        'DG_IR_NO
        '
        Me.DG_IR_NO.HeaderText = "IR No"
        Me.DG_IR_NO.Name = "DG_IR_NO"
        Me.DG_IR_NO.ReadOnly = True
        '
        'DG_IR_DATE
        '
        Me.DG_IR_DATE.HeaderText = "IR Date"
        Me.DG_IR_DATE.Name = "DG_IR_DATE"
        Me.DG_IR_DATE.ReadOnly = True
        '
        'DG_PNO
        '
        Me.DG_PNO.HeaderText = "PNo"
        Me.DG_PNO.Name = "DG_PNO"
        Me.DG_PNO.ReadOnly = True
        '
        'DG_SN
        '
        Me.DG_SN.HeaderText = "Serial No"
        Me.DG_SN.Name = "DG_SN"
        Me.DG_SN.ReadOnly = True
        Me.DG_SN.Width = 130
        '
        'DG_CUS_NAME
        '
        Me.DG_CUS_NAME.HeaderText = "Customer Name"
        Me.DG_CUS_NAME.Name = "DG_CUS_NAME"
        Me.DG_CUS_NAME.ReadOnly = True
        Me.DG_CUS_NAME.Width = 300
        '
        'DG_M_LOC
        '
        Me.DG_M_LOC.HeaderText = "Machine Location"
        Me.DG_M_LOC.Name = "DG_M_LOC"
        Me.DG_M_LOC.ReadOnly = True
        Me.DG_M_LOC.Width = 300
        '
        'IR_STATUS
        '
        Me.IR_STATUS.HeaderText = "Status"
        Me.IR_STATUS.Name = "IR_STATUS"
        Me.IR_STATUS.ReadOnly = True
        Me.IR_STATUS.Width = 200
        '
        'frmInternalApprovalQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1255, 480)
        Me.Controls.Add(Me.btnOpenManually)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.dgApprovedInternalQ)
        Me.KeyPreview = True
        Me.Name = "frmInternalApprovalQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00011"
        Me.Text = "Internal Approval Queue"
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOpenManually As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents dgApprovedInternalQ As System.Windows.Forms.DataGridView
    Friend WithEvents DG_IR_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_IR_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_PNO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_SN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_CUS_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_M_LOC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IR_STATUS As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
