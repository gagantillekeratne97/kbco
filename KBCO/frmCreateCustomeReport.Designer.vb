<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateCustomeReport
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtReportNo = New System.Windows.Forms.TextBox()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.RichTextBox()
        Me.txtQuery = New System.Windows.Forms.RichTextBox()
        Me.txtRequestBy = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbActiveReport = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Report No"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Report Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 210)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Description"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Query"
        '
        'txtReportNo
        '
        Me.txtReportNo.Location = New System.Drawing.Point(88, 12)
        Me.txtReportNo.Name = "txtReportNo"
        Me.txtReportNo.Size = New System.Drawing.Size(100, 20)
        Me.txtReportNo.TabIndex = 1
        '
        'txtReportName
        '
        Me.txtReportName.Location = New System.Drawing.Point(88, 38)
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Size = New System.Drawing.Size(157, 20)
        Me.txtReportName.TabIndex = 1
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(88, 233)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(937, 96)
        Me.txtDesc.TabIndex = 2
        Me.txtDesc.Text = ""
        '
        'txtQuery
        '
        Me.txtQuery.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuery.Location = New System.Drawing.Point(88, 90)
        Me.txtQuery.Name = "txtQuery"
        Me.txtQuery.Size = New System.Drawing.Size(910, 137)
        Me.txtQuery.TabIndex = 2
        Me.txtQuery.Text = ""
        '
        'txtRequestBy
        '
        Me.txtRequestBy.Location = New System.Drawing.Point(88, 64)
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.Size = New System.Drawing.Size(157, 20)
        Me.txtRequestBy.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Request by"
        '
        'cbActiveReport
        '
        Me.cbActiveReport.AutoSize = True
        Me.cbActiveReport.Checked = True
        Me.cbActiveReport.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActiveReport.Location = New System.Drawing.Point(327, 41)
        Me.cbActiveReport.Name = "cbActiveReport"
        Me.cbActiveReport.Size = New System.Drawing.Size(44, 17)
        Me.cbActiveReport.TabIndex = 5
        Me.cbActiveReport.Text = "Yes"
        Me.cbActiveReport.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(251, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Active Report"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.DarkRed
        Me.Label7.Location = New System.Drawing.Point(413, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(213, 26)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Please start date and end date sql injection " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "as @startDate and @endDate"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(403, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 18)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Importent"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label28.Location = New System.Drawing.Point(194, 15)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(19, 13)
        Me.Label28.TabIndex = 174
        Me.Label28.Text = "F2"
        '
        'frmCreateCustomeReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 341)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cbActiveReport)
        Me.Controls.Add(Me.txtRequestBy)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtQuery)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtReportName)
        Me.Controls.Add(Me.txtReportNo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmCreateCustomeReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "R00015"
        Me.Text = "Create Custome Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtReportNo As System.Windows.Forms.TextBox
    Friend WithEvents txtReportName As System.Windows.Forms.TextBox
    Friend WithEvents txtDesc As System.Windows.Forms.RichTextBox
    Friend WithEvents txtQuery As System.Windows.Forms.RichTextBox
    Friend WithEvents txtRequestBy As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbActiveReport As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
End Class
