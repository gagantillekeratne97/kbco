Imports System.Data.SqlClient

Public Class frmQueryRunner
    Dim strSQL As String = ""
    Private Sub btnExt_Click(sender As Object, e As EventArgs) Handles btnExt.Click
        ExQuery()
    End Sub

    Private Function ExQuery() As Boolean
        If Trim(txtQuerry.Text) = "" Then
            ExQuery = False
            Exit Function
        End If
        Try
            strSQL = Trim(txtQuerry.Text)
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            If dbConnections.sqlCommand.ExecuteNonQuery() Then ExQuery = True Else ExQuery = False
            If ExQuery = True Then
                MessageBox.Show("Sucuessfully Executed")
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return ExQuery
    End Function

   
End Class