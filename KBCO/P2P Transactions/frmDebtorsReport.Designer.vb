<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDebtorsReport
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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDebtorsReport))
        Me.dgDebtors = New System.Windows.Forms.DataGridView()
        Me.INV_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_NO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REP_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REP_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUS_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUS_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INV_AMOUNT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BAL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.R7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OVER_DUE_DAYS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRRepCode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtRCusID = New System.Windows.Forms.TextBox()
        CType(Me.dgDebtors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgDebtors
        '
        Me.dgDebtors.AllowUserToAddRows = False
        Me.dgDebtors.AllowUserToDeleteRows = False
        Me.dgDebtors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgDebtors.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.INV_DATE, Me.INV_NO, Me.REP_CODE, Me.REP_NAME, Me.DEPT, Me.CUS_ID, Me.CUS_NAME, Me.AG_ID, Me.AG_NAME, Me.INV_AMOUNT, Me.BAL, Me.R1, Me.R2, Me.R3, Me.R4, Me.R5, Me.R6, Me.R7, Me.OVER_DUE_DAYS})
        Me.dgDebtors.Location = New System.Drawing.Point(16, 90)
        Me.dgDebtors.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgDebtors.MultiSelect = False
        Me.dgDebtors.Name = "dgDebtors"
        Me.dgDebtors.RowHeadersWidth = 51
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        Me.dgDebtors.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgDebtors.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black
        Me.dgDebtors.ShowCellErrors = False
        Me.dgDebtors.ShowCellToolTips = False
        Me.dgDebtors.ShowEditingIcon = False
        Me.dgDebtors.ShowRowErrors = False
        Me.dgDebtors.Size = New System.Drawing.Size(1593, 481)
        Me.dgDebtors.TabIndex = 0
        '
        'INV_DATE
        '
        Me.INV_DATE.HeaderText = "Inv Date"
        Me.INV_DATE.MinimumWidth = 6
        Me.INV_DATE.Name = "INV_DATE"
        Me.INV_DATE.Width = 125
        '
        'INV_NO
        '
        Me.INV_NO.HeaderText = "Inv No"
        Me.INV_NO.MinimumWidth = 6
        Me.INV_NO.Name = "INV_NO"
        Me.INV_NO.Width = 125
        '
        'REP_CODE
        '
        Me.REP_CODE.HeaderText = "Rep"
        Me.REP_CODE.MinimumWidth = 6
        Me.REP_CODE.Name = "REP_CODE"
        Me.REP_CODE.Width = 125
        '
        'REP_NAME
        '
        Me.REP_NAME.HeaderText = "Rep Name"
        Me.REP_NAME.MinimumWidth = 6
        Me.REP_NAME.Name = "REP_NAME"
        Me.REP_NAME.Width = 125
        '
        'DEPT
        '
        Me.DEPT.HeaderText = "Dept"
        Me.DEPT.MinimumWidth = 6
        Me.DEPT.Name = "DEPT"
        Me.DEPT.Width = 125
        '
        'CUS_ID
        '
        Me.CUS_ID.HeaderText = "Cus ID"
        Me.CUS_ID.MinimumWidth = 6
        Me.CUS_ID.Name = "CUS_ID"
        Me.CUS_ID.Width = 125
        '
        'CUS_NAME
        '
        Me.CUS_NAME.HeaderText = "Cus Name"
        Me.CUS_NAME.MinimumWidth = 6
        Me.CUS_NAME.Name = "CUS_NAME"
        Me.CUS_NAME.Width = 125
        '
        'AG_ID
        '
        Me.AG_ID.HeaderText = "AG ID"
        Me.AG_ID.MinimumWidth = 6
        Me.AG_ID.Name = "AG_ID"
        Me.AG_ID.Width = 125
        '
        'AG_NAME
        '
        Me.AG_NAME.HeaderText = "AG Name"
        Me.AG_NAME.MinimumWidth = 6
        Me.AG_NAME.Name = "AG_NAME"
        Me.AG_NAME.Width = 125
        '
        'INV_AMOUNT
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.INV_AMOUNT.DefaultCellStyle = DataGridViewCellStyle1
        Me.INV_AMOUNT.HeaderText = "Amount"
        Me.INV_AMOUNT.MinimumWidth = 6
        Me.INV_AMOUNT.Name = "INV_AMOUNT"
        Me.INV_AMOUNT.Width = 125
        '
        'BAL
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.BAL.DefaultCellStyle = DataGridViewCellStyle2
        Me.BAL.HeaderText = "Balance"
        Me.BAL.MinimumWidth = 6
        Me.BAL.Name = "BAL"
        Me.BAL.Width = 125
        '
        'R1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.R1.DefaultCellStyle = DataGridViewCellStyle3
        Me.R1.HeaderText = "0-15"
        Me.R1.MinimumWidth = 6
        Me.R1.Name = "R1"
        Me.R1.Width = 125
        '
        'R2
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.R2.DefaultCellStyle = DataGridViewCellStyle4
        Me.R2.HeaderText = "15-30"
        Me.R2.MinimumWidth = 6
        Me.R2.Name = "R2"
        Me.R2.Width = 125
        '
        'R3
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.R3.DefaultCellStyle = DataGridViewCellStyle5
        Me.R3.HeaderText = "30-60"
        Me.R3.MinimumWidth = 6
        Me.R3.Name = "R3"
        Me.R3.Width = 125
        '
        'R4
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.R4.DefaultCellStyle = DataGridViewCellStyle6
        Me.R4.HeaderText = "61-90"
        Me.R4.MinimumWidth = 6
        Me.R4.Name = "R4"
        Me.R4.Width = 125
        '
        'R5
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.R5.DefaultCellStyle = DataGridViewCellStyle7
        Me.R5.HeaderText = "90-180"
        Me.R5.MinimumWidth = 6
        Me.R5.Name = "R5"
        Me.R5.Width = 125
        '
        'R6
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.R6.DefaultCellStyle = DataGridViewCellStyle8
        Me.R6.HeaderText = "180-365"
        Me.R6.MinimumWidth = 6
        Me.R6.Name = "R6"
        Me.R6.Width = 125
        '
        'R7
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.R7.DefaultCellStyle = DataGridViewCellStyle9
        Me.R7.HeaderText = "365-Abv"
        Me.R7.MinimumWidth = 6
        Me.R7.Name = "R7"
        Me.R7.Width = 125
        '
        'OVER_DUE_DAYS
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        DataGridViewCellStyle10.NullValue = Nothing
        Me.OVER_DUE_DAYS.DefaultCellStyle = DataGridViewCellStyle10
        Me.OVER_DUE_DAYS.HeaderText = "Over Due Days"
        Me.OVER_DUE_DAYS.MinimumWidth = 6
        Me.OVER_DUE_DAYS.Name = "OVER_DUE_DAYS"
        Me.OVER_DUE_DAYS.Width = 125
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(600, 34)
        Me.btnProcess.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(100, 28)
        Me.btnProcess.TabIndex = 2
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnExport
        '
        Me.btnExport.BackgroundImage = CType(resources.GetObject("btnExport.BackgroundImage"), System.Drawing.Image)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.Location = New System.Drawing.Point(16, 15)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(73, 68)
        Me.btnExport.TabIndex = 3
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(389, 42)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 16)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Rep"
        '
        'txtRRepCode
        '
        Me.txtRRepCode.Location = New System.Drawing.Point(429, 37)
        Me.txtRRepCode.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRRepCode.Name = "txtRRepCode"
        Me.txtRRepCode.Size = New System.Drawing.Size(145, 22)
        Me.txtRRepCode.TabIndex = 48
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(135, 42)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(46, 16)
        Me.Label22.TabIndex = 45
        Me.Label22.Text = "Cus ID"
        '
        'txtRCusID
        '
        Me.txtRCusID.Location = New System.Drawing.Point(209, 37)
        Me.txtRCusID.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRCusID.Name = "txtRCusID"
        Me.txtRCusID.Size = New System.Drawing.Size(145, 22)
        Me.txtRCusID.TabIndex = 44
        '
        'frmDebtorsReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1625, 597)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRRepCode)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txtRCusID)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.dgDebtors)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmDebtorsReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "R00016"
        Me.Text = "Dabtors"
        CType(Me.dgDebtors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgDebtors As System.Windows.Forms.DataGridView
    Friend WithEvents INV_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents REP_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents REP_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUS_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUS_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AG_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INV_AMOUNT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BAL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents R7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OVER_DUE_DAYS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRRepCode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtRCusID As System.Windows.Forms.TextBox
End Class
