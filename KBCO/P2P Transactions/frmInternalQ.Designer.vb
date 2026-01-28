<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalQ
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
        Me.dgApprovedInternalQ = New System.Windows.Forms.DataGridView()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.lblGMApproval = New System.Windows.Forms.Label()
        Me.pbGmApproval = New System.Windows.Forms.PictureBox()
        Me.pbSoapprovedbyAccounts = New System.Windows.Forms.PictureBox()
        Me.lblSOapprovedByAccounts = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtInternalSearching = New System.Windows.Forms.TextBox()
        Me.DG_IR_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_IR_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_PNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InternalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_SN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BeleetaRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_CUS_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_M_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_STATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbGmApproval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSoapprovedbyAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgApprovedInternalQ
        '
        Me.dgApprovedInternalQ.AllowUserToAddRows = False
        Me.dgApprovedInternalQ.AllowUserToDeleteRows = False
        Me.dgApprovedInternalQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgApprovedInternalQ.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_IR_NO, Me.DG_IR_DATE, Me.DG_PNO, Me.InternalCost, Me.DG_SN, Me.BeleetaRefNo, Me.DG_CUS_NAME, Me.DG_M_LOC, Me.IR_STATE})
        Me.dgApprovedInternalQ.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgApprovedInternalQ.Location = New System.Drawing.Point(0, 51)
        Me.dgApprovedInternalQ.Margin = New System.Windows.Forms.Padding(4)
        Me.dgApprovedInternalQ.Name = "dgApprovedInternalQ"
        Me.dgApprovedInternalQ.ReadOnly = True
        Me.dgApprovedInternalQ.RowHeadersWidth = 51
        Me.dgApprovedInternalQ.Size = New System.Drawing.Size(1657, 521)
        Me.dgApprovedInternalQ.TabIndex = 0
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(17, 16)
        Me.btnCreate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(100, 28)
        Me.btnCreate.TabIndex = 1
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(125, 16)
        Me.btnOpenManually.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(147, 28)
        Me.btnOpenManually.TabIndex = 1
        Me.btnOpenManually.Text = "Open Manually"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'lblGMApproval
        '
        Me.lblGMApproval.AutoSize = True
        Me.lblGMApproval.Location = New System.Drawing.Point(1053, 21)
        Me.lblGMApproval.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGMApproval.Name = "lblGMApproval"
        Me.lblGMApproval.Size = New System.Drawing.Size(167, 16)
        Me.lblGMApproval.TabIndex = 26
        Me.lblGMApproval.Text = "PENDING GM APPROVAL"
        '
        'pbGmApproval
        '
        Me.pbGmApproval.BackColor = System.Drawing.Color.LightCoral
        Me.pbGmApproval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbGmApproval.Location = New System.Drawing.Point(1015, 15)
        Me.pbGmApproval.Margin = New System.Windows.Forms.Padding(4)
        Me.pbGmApproval.Name = "pbGmApproval"
        Me.pbGmApproval.Size = New System.Drawing.Size(30, 28)
        Me.pbGmApproval.TabIndex = 25
        Me.pbGmApproval.TabStop = False
        '
        'pbSoapprovedbyAccounts
        '
        Me.pbSoapprovedbyAccounts.BackColor = System.Drawing.Color.LightSeaGreen
        Me.pbSoapprovedbyAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbSoapprovedbyAccounts.Location = New System.Drawing.Point(315, 15)
        Me.pbSoapprovedbyAccounts.Margin = New System.Windows.Forms.Padding(4)
        Me.pbSoapprovedbyAccounts.Name = "pbSoapprovedbyAccounts"
        Me.pbSoapprovedbyAccounts.Size = New System.Drawing.Size(30, 28)
        Me.pbSoapprovedbyAccounts.TabIndex = 28
        Me.pbSoapprovedbyAccounts.TabStop = False
        '
        'lblSOapprovedByAccounts
        '
        Me.lblSOapprovedByAccounts.AutoSize = True
        Me.lblSOapprovedByAccounts.Location = New System.Drawing.Point(353, 21)
        Me.lblSOapprovedByAccounts.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSOapprovedByAccounts.Name = "lblSOapprovedByAccounts"
        Me.lblSOapprovedByAccounts.Size = New System.Drawing.Size(182, 16)
        Me.lblSOapprovedByAccounts.TabIndex = 27
        Me.lblSOapprovedByAccounts.Text = "INTERNAL PRINT PENDING"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(577, 15)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 28)
        Me.PictureBox1.TabIndex = 30
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(616, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 16)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "PENDING APPROVAL"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(831, 21)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 16)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "PENDING DISPATCH"
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Thistle
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Location = New System.Drawing.Point(792, 15)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(30, 28)
        Me.PictureBox2.TabIndex = 31
        Me.PictureBox2.TabStop = False
        '
        'txtInternalSearching
        '
        Me.txtInternalSearching.Location = New System.Drawing.Point(1248, 19)
        Me.txtInternalSearching.Name = "txtInternalSearching"
        Me.txtInternalSearching.Size = New System.Drawing.Size(281, 22)
        Me.txtInternalSearching.TabIndex = 34
        '
        'DG_IR_NO
        '
        Me.DG_IR_NO.HeaderText = "IR No"
        Me.DG_IR_NO.MinimumWidth = 6
        Me.DG_IR_NO.Name = "DG_IR_NO"
        Me.DG_IR_NO.ReadOnly = True
        Me.DG_IR_NO.Width = 125
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
        'InternalCost
        '
        Me.InternalCost.HeaderText = "IR Cost"
        Me.InternalCost.MinimumWidth = 6
        Me.InternalCost.Name = "InternalCost"
        Me.InternalCost.ReadOnly = True
        Me.InternalCost.Width = 125
        '
        'DG_SN
        '
        Me.DG_SN.HeaderText = "Serial No"
        Me.DG_SN.MinimumWidth = 6
        Me.DG_SN.Name = "DG_SN"
        Me.DG_SN.ReadOnly = True
        Me.DG_SN.Width = 130
        '
        'BeleetaRefNo
        '
        Me.BeleetaRefNo.HeaderText = "Beleeta Ref No"
        Me.BeleetaRefNo.MinimumWidth = 6
        Me.BeleetaRefNo.Name = "BeleetaRefNo"
        Me.BeleetaRefNo.ReadOnly = True
        Me.BeleetaRefNo.Width = 125
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
        'frmInternalQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(1657, 572)
        Me.Controls.Add(Me.txtInternalSearching)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbSoapprovedbyAccounts)
        Me.Controls.Add(Me.lblSOapprovedByAccounts)
        Me.Controls.Add(Me.lblGMApproval)
        Me.Controls.Add(Me.pbGmApproval)
        Me.Controls.Add(Me.btnOpenManually)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.dgApprovedInternalQ)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInternalQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "A00006"
        Me.Text = "Internal Queue"
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbGmApproval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSoapprovedbyAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgApprovedInternalQ As System.Windows.Forms.DataGridView
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnOpenManually As System.Windows.Forms.Button
    Friend WithEvents lblGMApproval As System.Windows.Forms.Label
    Friend WithEvents pbGmApproval As System.Windows.Forms.PictureBox
    Friend WithEvents pbSoapprovedbyAccounts As System.Windows.Forms.PictureBox
    Friend WithEvents lblSOapprovedByAccounts As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents txtInternalSearching As TextBox
    Friend WithEvents DG_IR_NO As DataGridViewTextBoxColumn
    Friend WithEvents DG_IR_DATE As DataGridViewTextBoxColumn
    Friend WithEvents DG_PNO As DataGridViewTextBoxColumn
    Friend WithEvents InternalCost As DataGridViewTextBoxColumn
    Friend WithEvents DG_SN As DataGridViewTextBoxColumn
    Friend WithEvents BeleetaRefNo As DataGridViewTextBoxColumn
    Friend WithEvents DG_CUS_NAME As DataGridViewTextBoxColumn
    Friend WithEvents DG_M_LOC As DataGridViewTextBoxColumn
    Friend WithEvents IR_STATE As DataGridViewTextBoxColumn
End Class
