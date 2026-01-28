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

Partial Public Class Tile
    Private strSQL As String = ""
    Public Property ExecutablePath() As String
    Public MenuTag As String = ""




    Private Sub Tile_PreviewMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Me.PreviewMouseDoubleClick
        TreeClick(sender.tag.ToString, e)


    End Sub



    Private Sub TreeClick(ByVal sendertag As String, ByVal e As EventArgs)

        Dim ItemName As String = ""
        Dim formRealname As String = ""
        connectionStaet()
        Try
            'strSQL = "SELECT MENUITEM_MENUFORMNAME, MENUITEM_MENUNAME FROM MENUITEM WHERE MENUITEM_MENUTAG ='" & sendertag & "'  AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            strSQL = "SELECT MENUITEM_MENUFORMNAME, MENUITEM_MENUNAME FROM MENUITEM WHERE MENUITEM_MENUTAG ='" & sendertag & "' "
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
            'FormResize(frmMDImain, frm)
            frmMDImain.mainmenuPenal.Height = 0



            frm.BackColor = System.Drawing.Color.Silver
            frm.Show()

            frm.BringToFront()
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub

    Private Sub btnFavorits_Click(sender As Object, e As RoutedEventArgs) Handles btnFavorits.Click

        If CheckFavoritsButtonCount(userSession) = True Then
            MessageBox.Show("You have reach maximum favorites buttons.", "Favorites", MessageBoxButton.OK, MessageBoxImage.Warning)
            Exit Sub
        End If

        If CheckFavoritsButtonExsist(Me.Tag) = True Then
            MessageBox.Show("This Button is already in the favorites.", "Favorites", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub
        Else
            AddToFavorits(Me.Tag, TileTxtBlck.Text)

        End If



    End Sub


    Public Function CheckFavoritsButtonExsist(ByRef MenuItemTag As String) As Boolean
        Try
            strSQL = "SELECT count( [MENUITEM_MENUTAG]) FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM_FAVORITS] WHERE [USERHED_USERCODE] = '" & userSession & "' AND MENUITEM_MENUTAG ='" & MenuItemTag & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            If dbConnections.sqlCommand.ExecuteScalar() = 0 Then
                CheckFavoritsButtonExsist = False
            Else
                CheckFavoritsButtonExsist = True
            End If

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Return CheckFavoritsButtonExsist
    End Function


    Public Function CheckFavoritsButtonCount(ByRef UserCode As String) As Boolean
        Try
            strSQL = "SELECT  count([USERHED_USERCODE]) AS 'COUNT_FAV'  FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM_FAVORITS] WHERE [USERHED_USERCODE] ='" & UserCode & "'AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            If dbConnections.sqlCommand.ExecuteScalar() > 6 Then
                CheckFavoritsButtonCount = True
            Else
                CheckFavoritsButtonCount = False
            End If

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Return CheckFavoritsButtonCount
    End Function


    Public Sub AddToFavorits(ByRef MenuTag As String, ByRef MenuName As String)
        Dim conf = MessageBox.Show("Do you want to add this button to Favorits?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            connectionStaet()
            Try
                strSQL = "INSERT INTO  " & selectedDatabaseName & ".dbo.MENUITEM_FAVORITS(MENUITEM_MENUTAG, MENUITEM_MENUNAME, USERHED_USERCODE,COM_ID ) VALUES     (@MENUITEM_MENUTAG, @MENUITEM_MENUNAME, @USERHED_USERCODE, '" & globalVariables.selectedCompanyID & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUTAG", Trim(MenuTag))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MENUITEM_MENUNAME", Trim(MenuName))
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", userSession)
                dbConnections.sqlCommand.ExecuteNonQuery()
                MessageBox.Show("Successfully added to favorites and will affect next K-Bridge System restart.", "Add", MessageBoxButton.OK, MessageBoxImage.Information)
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            Finally
                connectionClose()

            End Try
        End If
    End Sub


End Class
