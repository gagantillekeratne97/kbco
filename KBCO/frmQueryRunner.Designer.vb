<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQueryRunner
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
        Me.txtQuerry = New System.Windows.Forms.TextBox()
        Me.btnExt = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtQuerry
        '
        Me.txtQuerry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuerry.Location = New System.Drawing.Point(12, 12)
        Me.txtQuerry.Multiline = True
        Me.txtQuerry.Name = "txtQuerry"
        Me.txtQuerry.Size = New System.Drawing.Size(701, 159)
        Me.txtQuerry.TabIndex = 0
        '
        'btnExt
        '
        Me.btnExt.Location = New System.Drawing.Point(306, 177)
        Me.btnExt.Name = "btnExt"
        Me.btnExt.Size = New System.Drawing.Size(113, 33)
        Me.btnExt.TabIndex = 1
        Me.btnExt.Text = "Execute"
        Me.btnExt.UseVisualStyleBackColor = True
        '
        'frmQueryRunner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 219)
        Me.Controls.Add(Me.btnExt)
        Me.Controls.Add(Me.txtQuerry)
        Me.Name = "frmQueryRunner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "Y00011"
        Me.Text = "Query Runner"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtQuerry As System.Windows.Forms.TextBox
    Friend WithEvents btnExt As System.Windows.Forms.Button
End Class
