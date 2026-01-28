<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectLoginCompany1
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
        Me.dgCompanies = New System.Windows.Forms.DataGridView()
        Me.COM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COM_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgCompanies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgCompanies
        '
        Me.dgCompanies.AllowUserToAddRows = False
        Me.dgCompanies.AllowUserToDeleteRows = False
        Me.dgCompanies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCompanies.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.COM_ID, Me.COM_NAME})
        Me.dgCompanies.Location = New System.Drawing.Point(12, 12)
        Me.dgCompanies.Name = "dgCompanies"
        Me.dgCompanies.ReadOnly = True
        Me.dgCompanies.Size = New System.Drawing.Size(558, 285)
        Me.dgCompanies.TabIndex = 0
        '
        'COM_ID
        '
        Me.COM_ID.HeaderText = "ID"
        Me.COM_ID.Name = "COM_ID"
        Me.COM_ID.ReadOnly = True
        '
        'COM_NAME
        '
        Me.COM_NAME.HeaderText = "Company"
        Me.COM_NAME.Name = "COM_NAME"
        Me.COM_NAME.ReadOnly = True
        Me.COM_NAME.Width = 400
        '
        'frmSelectLoginCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(581, 308)
        Me.Controls.Add(Me.dgCompanies)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmSelectLoginCompany"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Company"
        CType(Me.dgCompanies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgCompanies As System.Windows.Forms.DataGridView
    Friend WithEvents COM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COM_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
