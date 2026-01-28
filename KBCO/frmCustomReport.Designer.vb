<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomReport
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
        Me.lblSqlQuery = New System.Windows.Forms.Label()
        Me.txtSql = New System.Windows.Forms.RichTextBox()
        Me.btnGenarate = New System.Windows.Forms.Button()
        Me.dgReport = New System.Windows.Forms.DataGridView()
        Me.btnExport2 = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.txtReportNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbColFind = New System.Windows.Forms.ComboBox()
        Me.txtFindKeyWord = New System.Windows.Forms.TextBox()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.cbRowNumbers = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.dgReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSqlQuery
        '
        Me.lblSqlQuery.AutoSize = True
        Me.lblSqlQuery.Location = New System.Drawing.Point(451, 15)
        Me.lblSqlQuery.Name = "lblSqlQuery"
        Me.lblSqlQuery.Size = New System.Drawing.Size(59, 13)
        Me.lblSqlQuery.TabIndex = 0
        Me.lblSqlQuery.Text = "SQL Query"
        '
        'txtSql
        '
        Me.txtSql.Location = New System.Drawing.Point(454, 29)
        Me.txtSql.Name = "txtSql"
        Me.txtSql.Size = New System.Drawing.Size(714, 100)
        Me.txtSql.TabIndex = 1
        Me.txtSql.Text = ""
        '
        'btnGenarate
        '
        Me.btnGenarate.Location = New System.Drawing.Point(263, 18)
        Me.btnGenarate.Name = "btnGenarate"
        Me.btnGenarate.Size = New System.Drawing.Size(75, 38)
        Me.btnGenarate.TabIndex = 2
        Me.btnGenarate.Text = "Genarate"
        Me.btnGenarate.UseVisualStyleBackColor = True
        '
        'dgReport
        '
        Me.dgReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgReport.Location = New System.Drawing.Point(12, 173)
        Me.dgReport.Name = "dgReport"
        Me.dgReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgReport.Size = New System.Drawing.Size(1163, 318)
        Me.dgReport.TabIndex = 3
        '
        'btnExport2
        '
        Me.btnExport2.Location = New System.Drawing.Point(354, 18)
        Me.btnExport2.Name = "btnExport2"
        Me.btnExport2.Size = New System.Drawing.Size(91, 23)
        Me.btnExport2.TabIndex = 11
        Me.btnExport2.Text = "Export to Excel"
        Me.btnExport2.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(1067, 3)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(101, 23)
        Me.btnCreate.TabIndex = 12
        Me.btnCreate.Text = "Create Report"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'txtReportName
        '
        Me.txtReportName.Location = New System.Drawing.Point(86, 41)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Size = New System.Drawing.Size(157, 20)
        Me.txtReportName.TabIndex = 1
        '
        'txtReportNo
        '
        Me.txtReportNo.Location = New System.Drawing.Point(86, 15)
        Me.txtReportNo.Name = "txtReportNo"
        Me.txtReportNo.Size = New System.Drawing.Size(100, 20)
        Me.txtReportNo.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Report Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Report No"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStartDate.Location = New System.Drawing.Point(86, 113)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(115, 20)
        Me.dtpStartDate.TabIndex = 17
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEndDate.Location = New System.Drawing.Point(305, 113)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(123, 20)
        Me.dtpEndDate.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Start Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(230, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "End Date"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label28.Location = New System.Drawing.Point(192, 18)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(19, 13)
        Me.Label28.TabIndex = 174
        Me.Label28.Text = "F2"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(86, 67)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(342, 40)
        Me.txtDesc.TabIndex = 175
        Me.txtDesc.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 176
        Me.Label6.Text = "Description"
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(86, 139)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(252, 20)
        Me.txtFilter.TabIndex = 177
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 142)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 178
        Me.Label1.Text = "Filter"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(504, 140)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 179
        Me.Label7.Text = "Column to Find"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(761, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 179
        Me.Label8.Text = "Find Key word"
        '
        'cmbColFind
        '
        Me.cmbColFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColFind.FormattingEnabled = True
        Me.cmbColFind.Location = New System.Drawing.Point(587, 136)
        Me.cmbColFind.Name = "cmbColFind"
        Me.cmbColFind.Size = New System.Drawing.Size(168, 21)
        Me.cmbColFind.TabIndex = 180
        '
        'txtFindKeyWord
        '
        Me.txtFindKeyWord.Location = New System.Drawing.Point(841, 136)
        Me.txtFindKeyWord.Name = "txtFindKeyWord"
        Me.txtFindKeyWord.Size = New System.Drawing.Size(276, 20)
        Me.txtFindKeyWord.TabIndex = 181
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(1121, 135)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(54, 23)
        Me.btnFind.TabIndex = 182
        Me.btnFind.Text = "Find"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(841, 153)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(276, 10)
        Me.ProgressBar1.TabIndex = 183
        '
        'BackgroundWorker1
        '
        '
        'cbRowNumbers
        '
        Me.cbRowNumbers.AutoSize = True
        Me.cbRowNumbers.Location = New System.Drawing.Point(454, 144)
        Me.cbRowNumbers.Name = "cbRowNumbers"
        Me.cbRowNumbers.Size = New System.Drawing.Size(42, 17)
        Me.cbRowNumbers.TabIndex = 184
        Me.cbRowNumbers.Text = "NO"
        Me.cbRowNumbers.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(345, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 13)
        Me.Label9.TabIndex = 185
        Me.Label9.Text = "Show Row Numbers"
        '
        'frmCustomReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1187, 514)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cbRowNumbers)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.txtFindKeyWord)
        Me.Controls.Add(Me.cmbColFind)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpEndDate)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.txtReportName)
        Me.Controls.Add(Me.txtReportNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.btnExport2)
        Me.Controls.Add(Me.dgReport)
        Me.Controls.Add(Me.btnGenarate)
        Me.Controls.Add(Me.txtSql)
        Me.Controls.Add(Me.lblSqlQuery)
        Me.KeyPreview = True
        Me.Name = "frmCustomReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "R00015"
        Me.Text = "Custome Report Genarator"
        CType(Me.dgReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSqlQuery As System.Windows.Forms.Label
    Friend WithEvents txtSql As System.Windows.Forms.RichTextBox
    Friend WithEvents btnGenarate As System.Windows.Forms.Button
    Friend WithEvents dgReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport2 As System.Windows.Forms.Button
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents txtReportName As System.Windows.Forms.TextBox
    Friend WithEvents txtReportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.RichTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbColFind As System.Windows.Forms.ComboBox
    Friend WithEvents txtFindKeyWord As System.Windows.Forms.TextBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents cbRowNumbers As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
