Public Class frmPDFViwer

    Private Sub frmPDFViwer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub frmPDFViwer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim conf = MessageBox.Show("Are you sure you want to exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbNo Then
            e.Cancel = True

        End If

    End Sub

End Class