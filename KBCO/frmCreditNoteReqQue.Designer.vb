<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditNoteReqQue
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 98)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(776, 340)
        Me.DataGridView1.TabIndex = 680
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(233, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 682
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cmbDepartment
        '
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Items.AddRange(New Object() {"None", "Marketing", "Technical", "Consumables", "Finance", "Outright" & Global.Microsoft.VisualBasic.ChrW(9), "Doculine"})
        Me.cmbDepartment.Location = New System.Drawing.Point(12, 12)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(202, 21)
        Me.cmbDepartment.TabIndex = 681
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(148, 39)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(149, 30)
        Me.Button1.TabIndex = 683
        Me.Button1.Text = "HOD Rejected List"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 39)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(130, 30)
        Me.Button2.TabIndex = 684
        Me.Button2.Text = "HOD Approved List"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(303, 39)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(149, 30)
        Me.Button3.TabIndex = 685
        Me.Button3.Text = "Pending Approval"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'frmCreditNoteReqQue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.cmbDepartment)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmCreditNoteReqQue"
        Me.Tag = "A00016"
        Me.Text = "frmCreditNoteReqQue"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnSearch As Button
    Friend WithEvents cmbDepartment As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
