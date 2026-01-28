<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalFinanceUpload
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
        Me.beleeta_reference = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_IR_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_PNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_SN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_CUS_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_M_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_STATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(112, 13)
        Me.btnOpenManually.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(147, 28)
        Me.btnOpenManually.TabIndex = 4
        Me.btnOpenManually.Text = "Open Manually"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(4, 13)
        Me.btnCreate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(100, 28)
        Me.btnCreate.TabIndex = 3
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'dgApprovedInternalQ
        '
        Me.dgApprovedInternalQ.AllowUserToAddRows = False
        Me.dgApprovedInternalQ.AllowUserToDeleteRows = False
        Me.dgApprovedInternalQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgApprovedInternalQ.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_IR_NO, Me.beleeta_reference, Me.DG_IR_DATE, Me.DG_PNO, Me.DG_SN, Me.DG_CUS_NAME, Me.DG_M_LOC, Me.IR_STATE})
        Me.dgApprovedInternalQ.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgApprovedInternalQ.Location = New System.Drawing.Point(0, 87)
        Me.dgApprovedInternalQ.Margin = New System.Windows.Forms.Padding(4)
        Me.dgApprovedInternalQ.Name = "dgApprovedInternalQ"
        Me.dgApprovedInternalQ.ReadOnly = True
        Me.dgApprovedInternalQ.RowHeadersWidth = 51
        Me.dgApprovedInternalQ.Size = New System.Drawing.Size(1615, 614)
        Me.dgApprovedInternalQ.TabIndex = 2
        '
        'DG_IR_NO
        '
        Me.DG_IR_NO.HeaderText = "IR No"
        Me.DG_IR_NO.MinimumWidth = 6
        Me.DG_IR_NO.Name = "DG_IR_NO"
        Me.DG_IR_NO.ReadOnly = True
        Me.DG_IR_NO.Width = 125
        '
        'beleeta_reference
        '
        Me.beleeta_reference.HeaderText = "BEL_NO"
        Me.beleeta_reference.MinimumWidth = 6
        Me.beleeta_reference.Name = "beleeta_reference"
        Me.beleeta_reference.ReadOnly = True
        Me.beleeta_reference.Width = 125
        '
        'DG_IR_DATE
        '
        Me.DG_IR_DATE.HeaderText = "IR Date"
        Me.DG_IR_DATE.MinimumWidth = 6
        Me.DG_IR_DATE.Name = "DG_IR_DATE"
        Me.DG_IR_DATE.ReadOnly = True
        Me.DG_IR_DATE.Width = 125
        '
        'DG_PNO
        '
        Me.DG_PNO.HeaderText = "PNo"
        Me.DG_PNO.MinimumWidth = 6
        Me.DG_PNO.Name = "DG_PNO"
        Me.DG_PNO.ReadOnly = True
        Me.DG_PNO.Width = 125
        '
        'DG_SN
        '
        Me.DG_SN.HeaderText = "Serial No"
        Me.DG_SN.MinimumWidth = 6
        Me.DG_SN.Name = "DG_SN"
        Me.DG_SN.ReadOnly = True
        Me.DG_SN.Width = 130
        '
        'DG_CUS_NAME
        '
        Me.DG_CUS_NAME.HeaderText = "Customer Name"
        Me.DG_CUS_NAME.MinimumWidth = 6
        Me.DG_CUS_NAME.Name = "DG_CUS_NAME"
        Me.DG_CUS_NAME.ReadOnly = True
        Me.DG_CUS_NAME.Width = 300
        '
        'DG_M_LOC
        '
        Me.DG_M_LOC.HeaderText = "Machine Location"
        Me.DG_M_LOC.MinimumWidth = 6
        Me.DG_M_LOC.Name = "DG_M_LOC"
        Me.DG_M_LOC.ReadOnly = True
        Me.DG_M_LOC.Width = 300
        '
        'IR_STATE
        '
        Me.IR_STATE.HeaderText = "Status"
        Me.IR_STATE.MinimumWidth = 6
        Me.IR_STATE.Name = "IR_STATE"
        Me.IR_STATE.ReadOnly = True
        Me.IR_STATE.Width = 200
        '
        'frmInternalFinanceUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1615, 701)
        Me.Controls.Add(Me.btnOpenManually)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.dgApprovedInternalQ)
        Me.Name = "frmInternalFinanceUpload"
        Me.Tag = "Y00012"
        Me.Text = "frmInternalFinanceUpload"
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnOpenManually As Button
    Friend WithEvents btnCreate As Button
    Friend WithEvents dgApprovedInternalQ As DataGridView
    Friend WithEvents DG_IR_NO As DataGridViewTextBoxColumn
    Friend WithEvents beleeta_reference As DataGridViewTextBoxColumn
    Friend WithEvents DG_IR_DATE As DataGridViewTextBoxColumn
    Friend WithEvents DG_PNO As DataGridViewTextBoxColumn
    Friend WithEvents DG_SN As DataGridViewTextBoxColumn
    Friend WithEvents DG_CUS_NAME As DataGridViewTextBoxColumn
    Friend WithEvents DG_M_LOC As DataGridViewTextBoxColumn
    Friend WithEvents IR_STATE As DataGridViewTextBoxColumn
End Class
