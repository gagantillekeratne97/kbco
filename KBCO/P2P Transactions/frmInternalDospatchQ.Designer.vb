<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInternalDospatchQ
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
        Me.btnOpenManually = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIRNo = New System.Windows.Forms.TextBox()
        Me.txtDispatchID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.DG_IR_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BeleetaNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_IR_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_PNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_SN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_CUS_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_M_LOC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IR_STATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgApprovedInternalQ
        '
        Me.dgApprovedInternalQ.AllowUserToAddRows = False
        Me.dgApprovedInternalQ.AllowUserToDeleteRows = False
        Me.dgApprovedInternalQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgApprovedInternalQ.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_IR_NO, Me.BeleetaNo, Me.DG_IR_DATE, Me.DG_PNO, Me.DG_SN, Me.DG_CUS_NAME, Me.DG_M_LOC, Me.IR_STATE})
        Me.dgApprovedInternalQ.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgApprovedInternalQ.Location = New System.Drawing.Point(0, 90)
        Me.dgApprovedInternalQ.Margin = New System.Windows.Forms.Padding(4)
        Me.dgApprovedInternalQ.Name = "dgApprovedInternalQ"
        Me.dgApprovedInternalQ.ReadOnly = True
        Me.dgApprovedInternalQ.RowHeadersWidth = 51
        Me.dgApprovedInternalQ.Size = New System.Drawing.Size(1657, 482)
        Me.dgApprovedInternalQ.TabIndex = 2
        '
        'btnOpenManually
        '
        Me.btnOpenManually.Location = New System.Drawing.Point(16, 16)
        Me.btnOpenManually.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOpenManually.Name = "btnOpenManually"
        Me.btnOpenManually.Size = New System.Drawing.Size(147, 28)
        Me.btnOpenManually.TabIndex = 3
        Me.btnOpenManually.Text = "Open Manually"
        Me.btnOpenManually.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(256, 22)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "IR No"
        '
        'txtIRNo
        '
        Me.txtIRNo.Location = New System.Drawing.Point(335, 18)
        Me.txtIRNo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIRNo.Name = "txtIRNo"
        Me.txtIRNo.Size = New System.Drawing.Size(132, 22)
        Me.txtIRNo.TabIndex = 14
        '
        'txtDispatchID
        '
        Me.txtDispatchID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDispatchID.Location = New System.Drawing.Point(617, 18)
        Me.txtDispatchID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDispatchID.Name = "txtDispatchID"
        Me.txtDispatchID.Size = New System.Drawing.Size(133, 22)
        Me.txtDispatchID.TabIndex = 355
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(505, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 16)
        Me.Label3.TabIndex = 354
        Me.Label3.Text = "Dispatch No"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(836, 18)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(459, 63)
        Me.txtComment.TabIndex = 352
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(760, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 353
        Me.Label2.Text = "Comment"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(1304, 18)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(77, 64)
        Me.btnUpdate.TabIndex = 356
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'DG_IR_NO
        '
        Me.DG_IR_NO.HeaderText = "IR No"
        Me.DG_IR_NO.MinimumWidth = 6
        Me.DG_IR_NO.Name = "DG_IR_NO"
        Me.DG_IR_NO.ReadOnly = True
        Me.DG_IR_NO.Width = 125
        '
        'BeleetaNo
        '
        Me.BeleetaNo.HeaderText = "Beeleta No"
        Me.BeleetaNo.MinimumWidth = 6
        Me.BeleetaNo.Name = "BeleetaNo"
        Me.BeleetaNo.ReadOnly = True
        Me.BeleetaNo.Width = 125
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
        'frmInternalDospatchQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1657, 572)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.txtDispatchID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtIRNo)
        Me.Controls.Add(Me.dgApprovedInternalQ)
        Me.Controls.Add(Me.btnOpenManually)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInternalDospatchQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmInternalDospatchQ"
        CType(Me.dgApprovedInternalQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgApprovedInternalQ As System.Windows.Forms.DataGridView
    Friend WithEvents btnOpenManually As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIRNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDispatchID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents DG_IR_NO As DataGridViewTextBoxColumn
    Friend WithEvents BeleetaNo As DataGridViewTextBoxColumn
    Friend WithEvents DG_IR_DATE As DataGridViewTextBoxColumn
    Friend WithEvents DG_PNO As DataGridViewTextBoxColumn
    Friend WithEvents DG_SN As DataGridViewTextBoxColumn
    Friend WithEvents DG_CUS_NAME As DataGridViewTextBoxColumn
    Friend WithEvents DG_M_LOC As DataGridViewTextBoxColumn
    Friend WithEvents IR_STATE As DataGridViewTextBoxColumn
End Class
