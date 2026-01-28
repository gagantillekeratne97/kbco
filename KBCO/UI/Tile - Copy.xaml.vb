Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.IO
Imports System.ComponentModel
Imports System.Reflection
Imports System.Data.SqlClient

Partial Public Class Tile2
    Private strSQL As String = ""
    Public Property ExecutablePath() As String

    Private Sub Tile_PreviewMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Me.PreviewMouseDoubleClick
        TreeClick(sender.tag.ToString, e)
    End Sub



    Private Sub TreeClick(ByVal sendertag As String, ByVal e As EventArgs)




        Dim ItemName As String = ""
        Dim formRealname As String = ""
        connectionStaet()
        Try
            'strSQL = "SELECT [MENUITEM_MENUFORMNAME],[MENUITEM_MENUNAME] FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM] WHERE [MENUITEM_MENUTAG] ='" & sendertag & "' AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            strSQL = "SELECT [MENUITEM_MENUFORMNAME],[MENUITEM_MENUNAME] FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM] WHERE [MENUITEM_MENUTAG] ='" & sendertag & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                ItemName = dbConnections.dReader.Item("MENUITEM_MENUFORMNAME")
                formRealname = dbConnections.dReader.Item("MENUITEM_MENUNAME")
            End While
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show(GenaralErrorMessage + vbCrLf + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try

        Try
            'converting string value to object and call as a form
            Dim frm As New Form
            Dim formName As String = ItemName
            formName = [Assembly].GetEntryAssembly.GetName.Name & "." & formName
            frm = DirectCast([Assembly].GetEntryAssembly.CreateInstance(formName), Form)

            ' block duplicateing form code
            For Each f In frmMDImain.MdiChildren
                If f.Name = frm.Name Then
                    'the form is already open
                    Exit Sub
                End If
            Next
            'check form opend through MDI or not
            OpenThroughMDI = True
            frm.Text = formRealname
            frm.MdiParent = frmMDImain
            'frm.BackColor = Color.SteelBlue
            FormResize(frmMDImain, frm)
            frmMDImain.mainmenuPenal.Height = 0


            frm.BackColor = System.Drawing.Color.Silver
            frm.Show()

            frm.BringToFront()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRemoveFavorits_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveFavorits.Click
        Dim conf = MessageBox.Show("Do you want to remove this button to Favorites?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            Try
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.MENUITEM_FAVORITS WHERE     (MENUITEM_MENUTAG = '" & Me.Tag & "') AND (USERHED_USERCODE = '" & userSession & "') AND COM_ID='" & globalVariables.selectedCompanyID & "'"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.ExecuteNonQuery()

                MessageBox.Show("Successfully removed form favorites and will affect on next K-Bridge System startup.", "Removed", MessageBoxButton.OK, MessageBoxImage.Information)
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            End Try
        End If
    End Sub
End Class
