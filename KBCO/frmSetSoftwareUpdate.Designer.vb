<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetSoftwareUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetSoftwareUpdate))
        Me.dgSWUpdate = New System.Windows.Forms.DataGridView()
        Me.U_CODE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_PC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_SW_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEPT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_UPDATE = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnSendUpdate = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblLastUpdateDate = New System.Windows.Forms.Label()
        Me.lblLastUpdateDatetext = New System.Windows.Forms.Label()
        Me.btnViewSWupdateStatus = New System.Windows.Forms.Button()
        Me.lblRequestby = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPurpose = New System.Windows.Forms.TextBox()
        Me.txtRequestBy = New System.Windows.Forms.TextBox()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblRefresh = New System.Windows.Forms.Label()
        Me.lblSendUpdate = New System.Windows.Forms.Label()
        Me.btnUpdateHistory = New System.Windows.Forms.Button()
        CType(Me.dgSWUpdate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgSWUpdate
        '
        Me.dgSWUpdate.AllowUserToAddRows = False
        Me.dgSWUpdate.AllowUserToDeleteRows = False
        Me.dgSWUpdate.AllowUserToOrderColumns = True
        Me.dgSWUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSWUpdate.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.U_CODE, Me.U_NAME, Me.U_PC, Me.U_SW_DATE, Me.DEPT, Me.U_UPDATE})
        Me.dgSWUpdate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgSWUpdate.Location = New System.Drawing.Point(0, 122)
        Me.dgSWUpdate.Name = "dgSWUpdate"
        Me.dgSWUpdate.Size = New System.Drawing.Size(980, 298)
        Me.dgSWUpdate.TabIndex = 0
        '
        'U_CODE
        '
        Me.U_CODE.HeaderText = "USER CODE"
        Me.U_CODE.Name = "U_CODE"
        Me.U_CODE.ReadOnly = True
        '
        'U_NAME
        '
        Me.U_NAME.HeaderText = "USER NAME"
        Me.U_NAME.Name = "U_NAME"
        Me.U_NAME.ReadOnly = True
        Me.U_NAME.Width = 300
        '
        'U_PC
        '
        Me.U_PC.HeaderText = "USER PC"
        Me.U_PC.Name = "U_PC"
        Me.U_PC.ReadOnly = True
        '
        'U_SW_DATE
        '
        Me.U_SW_DATE.HeaderText = "UPDATE RELEASE DATE"
        Me.U_SW_DATE.Name = "U_SW_DATE"
        Me.U_SW_DATE.ReadOnly = True
        Me.U_SW_DATE.Width = 200
        '
        'DEPT
        '
        Me.DEPT.HeaderText = "DEPARTMENT"
        Me.DEPT.Name = "DEPT"
        '
        'U_UPDATE
        '
        Me.U_UPDATE.HeaderText = "UPDATE"
        Me.U_UPDATE.Name = "U_UPDATE"
        '
        'btnSendUpdate
        '
        Me.btnSendUpdate.BackgroundImage = CType(resources.GetObject("btnSendUpdate.BackgroundImage"), System.Drawing.Image)
        Me.btnSendUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSendUpdate.Location = New System.Drawing.Point(908, 40)
        Me.btnSendUpdate.Name = "btnSendUpdate"
        Me.btnSendUpdate.Size = New System.Drawing.Size(60, 60)
        Me.btnSendUpdate.TabIndex = 1
        Me.btnSendUpdate.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.Location = New System.Drawing.Point(833, 50)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(40, 40)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblLastUpdateDate
        '
        Me.lblLastUpdateDate.AutoSize = True
        Me.lblLastUpdateDate.Location = New System.Drawing.Point(151, 42)
        Me.lblLastUpdateDate.Name = "lblLastUpdateDate"
        Me.lblLastUpdateDate.Size = New System.Drawing.Size(30, 13)
        Me.lblLastUpdateDate.TabIndex = 3
        Me.lblLastUpdateDate.Text = "Date"
        '
        'lblLastUpdateDatetext
        '
        Me.lblLastUpdateDatetext.AutoSize = True
        Me.lblLastUpdateDatetext.Location = New System.Drawing.Point(16, 42)
        Me.lblLastUpdateDatetext.Name = "lblLastUpdateDatetext"
        Me.lblLastUpdateDatetext.Size = New System.Drawing.Size(91, 13)
        Me.lblLastUpdateDatetext.TabIndex = 4
        Me.lblLastUpdateDatetext.Text = "Last Update Date"
        '
        'btnViewSWupdateStatus
        '
        Me.btnViewSWupdateStatus.Location = New System.Drawing.Point(886, 10)
        Me.btnViewSWupdateStatus.Name = "btnViewSWupdateStatus"
        Me.btnViewSWupdateStatus.Size = New System.Drawing.Size(82, 23)
        Me.btnViewSWupdateStatus.TabIndex = 5
        Me.btnViewSWupdateStatus.Text = "View Status"
        Me.btnViewSWupdateStatus.UseVisualStyleBackColor = True
        '
        'lblRequestby
        '
        Me.lblRequestby.AutoSize = True
        Me.lblRequestby.Location = New System.Drawing.Point(16, 16)
        Me.lblRequestby.Name = "lblRequestby"
        Me.lblRequestby.Size = New System.Drawing.Size(62, 13)
        Me.lblRequestby.TabIndex = 6
        Me.lblRequestby.Text = "Request By"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Purpose of Update"
        '
        'txtPurpose
        '
        Me.txtPurpose.Location = New System.Drawing.Point(154, 65)
        Me.txtPurpose.MaxLength = 150
        Me.txtPurpose.Name = "txtPurpose"
        Me.txtPurpose.Size = New System.Drawing.Size(651, 20)
        Me.txtPurpose.TabIndex = 9
        '
        'txtRequestBy
        '
        Me.txtRequestBy.Location = New System.Drawing.Point(154, 13)
        Me.txtRequestBy.MaxLength = 50
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.Size = New System.Drawing.Size(481, 20)
        Me.txtRequestBy.TabIndex = 11
        '
        'cmbDepartment
        '
        Me.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(154, 95)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(178, 21)
        Me.cmbDepartment.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Select user by Department"
        '
        'lblRefresh
        '
        Me.lblRefresh.AutoSize = True
        Me.lblRefresh.Location = New System.Drawing.Point(833, 92)
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(44, 13)
        Me.lblRefresh.TabIndex = 14
        Me.lblRefresh.Text = "Refresh"
        '
        'lblSendUpdate
        '
        Me.lblSendUpdate.AutoSize = True
        Me.lblSendUpdate.Location = New System.Drawing.Point(904, 100)
        Me.lblSendUpdate.Name = "lblSendUpdate"
        Me.lblSendUpdate.Size = New System.Drawing.Size(70, 13)
        Me.lblSendUpdate.TabIndex = 15
        Me.lblSendUpdate.Text = "Send Update"
        '
        'btnUpdateHistory
        '
        Me.btnUpdateHistory.Location = New System.Drawing.Point(761, 10)
        Me.btnUpdateHistory.Name = "btnUpdateHistory"
        Me.btnUpdateHistory.Size = New System.Drawing.Size(112, 23)
        Me.btnUpdateHistory.TabIndex = 16
        Me.btnUpdateHistory.Text = "Update History"
        Me.btnUpdateHistory.UseVisualStyleBackColor = True
        '
        'frmSetSoftwareUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 420)
        Me.Controls.Add(Me.btnUpdateHistory)
        Me.Controls.Add(Me.lblSendUpdate)
        Me.Controls.Add(Me.lblRefresh)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbDepartment)
        Me.Controls.Add(Me.txtRequestBy)
        Me.Controls.Add(Me.txtPurpose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblRequestby)
        Me.Controls.Add(Me.btnViewSWupdateStatus)
        Me.Controls.Add(Me.lblLastUpdateDatetext)
        Me.Controls.Add(Me.lblLastUpdateDate)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnSendUpdate)
        Me.Controls.Add(Me.dgSWUpdate)
        Me.KeyPreview = True
        Me.Name = "frmSetSoftwareUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00010"
        Me.Text = "Set Software Update"
        CType(Me.dgSWUpdate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgSWUpdate As System.Windows.Forms.DataGridView
    Friend WithEvents btnSendUpdate As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents lblLastUpdateDate As System.Windows.Forms.Label
    Friend WithEvents lblLastUpdateDatetext As System.Windows.Forms.Label
    Friend WithEvents btnViewSWupdateStatus As System.Windows.Forms.Button
    Friend WithEvents lblRequestby As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPurpose As System.Windows.Forms.TextBox
    Friend WithEvents txtRequestBy As System.Windows.Forms.TextBox
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents U_CODE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_PC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_SW_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEPT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_UPDATE As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents lblRefresh As System.Windows.Forms.Label
    Friend WithEvents lblSendUpdate As System.Windows.Forms.Label
    Friend WithEvents btnUpdateHistory As System.Windows.Forms.Button
End Class
