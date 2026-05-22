<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemMasterUpload
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
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnUploadFile = New System.Windows.Forms.Button()
        Me.btnDownloadTemplateFile = New System.Windows.Forms.Button()
        Me.dgItemView = New System.Windows.Forms.DataGridView()
        Me.btnSaveToSystem = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.dgItemView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtFilePath
        '
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(12, 39)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(436, 26)
        Me.txtFilePath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Upload Item File"
        '
        'btnUploadFile
        '
        Me.btnUploadFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUploadFile.Location = New System.Drawing.Point(482, 13)
        Me.btnUploadFile.Name = "btnUploadFile"
        Me.btnUploadFile.Size = New System.Drawing.Size(161, 89)
        Me.btnUploadFile.TabIndex = 2
        Me.btnUploadFile.Text = "Upload File"
        Me.btnUploadFile.UseVisualStyleBackColor = True
        '
        'btnDownloadTemplateFile
        '
        Me.btnDownloadTemplateFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDownloadTemplateFile.Location = New System.Drawing.Point(659, 13)
        Me.btnDownloadTemplateFile.Name = "btnDownloadTemplateFile"
        Me.btnDownloadTemplateFile.Size = New System.Drawing.Size(161, 89)
        Me.btnDownloadTemplateFile.TabIndex = 3
        Me.btnDownloadTemplateFile.Text = "Download Template File"
        Me.btnDownloadTemplateFile.UseVisualStyleBackColor = True
        '
        'dgItemView
        '
        Me.dgItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgItemView.Location = New System.Drawing.Point(13, 203)
        Me.dgItemView.Name = "dgItemView"
        Me.dgItemView.RowHeadersWidth = 51
        Me.dgItemView.RowTemplate.Height = 24
        Me.dgItemView.Size = New System.Drawing.Size(1413, 493)
        Me.dgItemView.TabIndex = 4
        '
        'btnSaveToSystem
        '
        Me.btnSaveToSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveToSystem.Location = New System.Drawing.Point(840, 13)
        Me.btnSaveToSystem.Name = "btnSaveToSystem"
        Me.btnSaveToSystem.Size = New System.Drawing.Size(161, 89)
        Me.btnSaveToSystem.TabIndex = 5
        Me.btnSaveToSystem.Text = "Save to System"
        Me.btnSaveToSystem.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(13, 124)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1413, 60)
        Me.ProgressBar1.TabIndex = 6
        '
        'frmItemMasterUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1438, 708)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnSaveToSystem)
        Me.Controls.Add(Me.dgItemView)
        Me.Controls.Add(Me.btnDownloadTemplateFile)
        Me.Controls.Add(Me.btnUploadFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFilePath)
        Me.Name = "frmItemMasterUpload"
        Me.Tag = "A00018"
        Me.Text = "ITEM MASTER UPLOAD"
        CType(Me.dgItemView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnUploadFile As Button
    Friend WithEvents btnDownloadTemplateFile As Button
    Friend WithEvents dgItemView As DataGridView
    Friend WithEvents btnSaveToSystem As Button
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
