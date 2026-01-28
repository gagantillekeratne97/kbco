<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServistaSearch
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpSearchBy = New System.Windows.Forms.GroupBox()
        Me.rdoMiddleLetters = New System.Windows.Forms.RadioButton()
        Me.rdoStarting = New System.Windows.Forms.RadioButton()
        Me.dgSearch = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.searchStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.cmbSearchFrom = New System.Windows.Forms.ComboBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.searchInfo = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpSearchBy.SuspendLayout()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpSearchBy
        '
        Me.grpSearchBy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSearchBy.Controls.Add(Me.rdoMiddleLetters)
        Me.grpSearchBy.Controls.Add(Me.rdoStarting)
        Me.grpSearchBy.Location = New System.Drawing.Point(442, 28)
        Me.grpSearchBy.Name = "grpSearchBy"
        Me.grpSearchBy.Size = New System.Drawing.Size(223, 46)
        Me.grpSearchBy.TabIndex = 14
        Me.grpSearchBy.TabStop = False
        Me.grpSearchBy.Text = "Search by"
        '
        'rdoMiddleLetters
        '
        Me.rdoMiddleLetters.AutoSize = True
        Me.rdoMiddleLetters.Location = New System.Drawing.Point(118, 22)
        Me.rdoMiddleLetters.Name = "rdoMiddleLetters"
        Me.rdoMiddleLetters.Size = New System.Drawing.Size(87, 17)
        Me.rdoMiddleLetters.TabIndex = 1
        Me.rdoMiddleLetters.Text = "Middle letters"
        Me.rdoMiddleLetters.UseVisualStyleBackColor = True
        '
        'rdoStarting
        '
        Me.rdoStarting.AutoSize = True
        Me.rdoStarting.Checked = True
        Me.rdoStarting.Location = New System.Drawing.Point(7, 22)
        Me.rdoStarting.Name = "rdoStarting"
        Me.rdoStarting.Size = New System.Drawing.Size(92, 17)
        Me.rdoStarting.TabIndex = 0
        Me.rdoStarting.TabStop = True
        Me.rdoStarting.Text = "Starting letters"
        Me.rdoStarting.UseVisualStyleBackColor = True
        '
        'dgSearch
        '
        Me.dgSearch.AllowUserToAddRows = False
        Me.dgSearch.AllowUserToDeleteRows = False
        Me.dgSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSearch.BackgroundColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSearch.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSearch.Location = New System.Drawing.Point(11, 94)
        Me.dgSearch.MultiSelect = False
        Me.dgSearch.Name = "dgSearch"
        Me.dgSearch.ReadOnly = True
        Me.dgSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgSearch.Size = New System.Drawing.Size(722, 403)
        Me.dgSearch.TabIndex = 15
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(279, 64)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Location = New System.Drawing.Point(59, 66)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(206, 20)
        Me.txtSearch.TabIndex = 9
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(11, 70)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(41, 13)
        Me.lblSearch.TabIndex = 10
        Me.lblSearch.Text = "Search"
        '
        'searchStatusStrip
        '
        Me.searchStatusStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.searchStatusStrip.Location = New System.Drawing.Point(0, 0)
        Me.searchStatusStrip.Name = "searchStatusStrip"
        Me.searchStatusStrip.Size = New System.Drawing.Size(744, 22)
        Me.searchStatusStrip.SizingGrip = False
        Me.searchStatusStrip.TabIndex = 8
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.Location = New System.Drawing.Point(289, 32)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(65, 23)
        Me.btnPrevious.TabIndex = 13
        Me.btnPrevious.Text = "< Previous"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(222, 32)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(65, 23)
        Me.btnNext.TabIndex = 12
        Me.btnNext.Text = "Next  >"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'cmbSearchFrom
        '
        Me.cmbSearchFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSearchFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchFrom.FormattingEnabled = True
        Me.cmbSearchFrom.Location = New System.Drawing.Point(59, 33)
        Me.cmbSearchFrom.Name = "cmbSearchFrom"
        Me.cmbSearchFrom.Size = New System.Drawing.Size(136, 21)
        Me.cmbSearchFrom.TabIndex = 17
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(12, 38)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 16
        Me.lblFrom.Text = "From"
        '
        'searchInfo
        '
        Me.searchInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning
        Me.searchInfo.ToolTipTitle = "Invalid search criteria"
        '
        'frmServistaSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 501)
        Me.Controls.Add(Me.grpSearchBy)
        Me.Controls.Add(Me.dgSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.searchStatusStrip)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.cmbSearchFrom)
        Me.Controls.Add(Me.lblFrom)
        Me.Name = "frmServistaSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Servista Search"
        Me.grpSearchBy.ResumeLayout(False)
        Me.grpSearchBy.PerformLayout()
        CType(Me.dgSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpSearchBy As System.Windows.Forms.GroupBox
    Friend WithEvents rdoMiddleLetters As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStarting As System.Windows.Forms.RadioButton
    Friend WithEvents dgSearch As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents searchStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents cmbSearchFrom As System.Windows.Forms.ComboBox
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents searchInfo As System.Windows.Forms.ToolTip
End Class
