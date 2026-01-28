<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPDFViwer
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
        Me.wbPDF = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'wbPDF
        '
        Me.wbPDF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbPDF.Location = New System.Drawing.Point(0, 0)
        Me.wbPDF.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbPDF.Name = "wbPDF"
        Me.wbPDF.ScriptErrorsSuppressed = True
        Me.wbPDF.Size = New System.Drawing.Size(1009, 513)
        Me.wbPDF.TabIndex = 0
        '
        'frmPDFViwer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 513)
        Me.Controls.Add(Me.wbPDF)
        Me.Name = "frmPDFViwer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PDF Viwer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents wbPDF As System.Windows.Forms.WebBrowser
End Class
