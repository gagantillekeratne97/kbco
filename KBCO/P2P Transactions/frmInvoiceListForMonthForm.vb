Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text

Public Class frmInvoiceListForMonthForm
    Public startDate As String = ""
    Public endDate As String = ""
    Private Sub frmInvoiceListForMonthForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub ToCsV(dGV As DataGridView, filename As String)
        Dim stOutput As String = ""
        ' Export titles:
        Dim sHeaders As String = ""

        For j As Integer = 0 To dGV.Columns.Count - 1
            sHeaders = sHeaders.ToString() & Convert.ToString(dGV.Columns(j).HeaderText) & vbTab
        Next
        stOutput += sHeaders & vbCr & vbLf
        ' Export data.
        For i As Integer = 0 To dGV.RowCount - 1
            Dim stLine As String = ""
            For j As Integer = 0 To dGV.Rows(i).Cells.Count - 1
                stLine = stLine.ToString() & Convert.ToString(dGV.Rows(i).Cells(j).Value) & vbTab
            Next
            stOutput += stLine & vbCr & vbLf
        Next
        Dim utf16 As Encoding = Encoding.GetEncoding(1254)
        Dim output As Byte() = utf16.GetBytes(stOutput)
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)
        bw.Write(output, 0, output.Length)
        'write the encoded file
        bw.Flush()
        bw.Close()
        fs.Close()

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (*.xls)|*.xls"
        'sfd.FileName = "" & Trim(txtRepCode.Text) & "SOBackup.xls"
        If sfd.ShowDialog() = DialogResult.OK Then
            ToCsV(dgInvoiceLists, sfd.FileName)
        End If
    End Sub
End Class