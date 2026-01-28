Imports System.Collections.Generic
Public Class Metrolizer2
    Private wrapPanelX As Double = 0

    Public Sub DisplayTiles(ByRef metroStackPanel As Windows.Controls.StackPanel)


        Dim di As Dictionary(Of String, String()) = New IconsAndPaths().GetIconsAndPathsFavorit()

        Dim letter2 As String = ""
        Dim coll2 = di.Where(Function(k) k.Key.StartsWith(letter2, True, Nothing))
        If (coll2.Count > 0) Then
            AddTiles(coll2, metroStackPanel, letter2)
        End If
       
    End Sub

    Private Sub AddTiles(ByVal coll As IEnumerable(Of KeyValuePair(Of String, String())), ByRef metroStackPanel As Windows.Controls.StackPanel, ByVal letter As String)
        Dim tileWrapPanel2 As New Windows.Controls.WrapPanel
        tileWrapPanel2.Orientation = Orientation.Vertical
        tileWrapPanel2.Margin = New Windows.Thickness(0, 0, 20, 0)
        ' 3 tiles height-wise
        tileWrapPanel2.Height = (110 * 1) + (6 * 1)

        For Each kvp In coll
            Dim newTile As New Tile2
            newTile.ExecutablePath = kvp.Value(1)
            newTile.TileIcon.Source = New Windows.Media.Imaging.BitmapImage(New Uri(kvp.Value(0), UriKind.Absolute))
            Dim NameOnly As String() = Nothing
            NameOnly = kvp.Key.ToString.Split(":")
            newTile.TileTxtBlck.Text = NameOnly(1)
            newTile.Tag = NameOnly(0)
            newTile.Margin = New Windows.Thickness(0, 0, 6, 6)
            tileWrapPanel2.Children.Add(newTile)
        Next

        metroStackPanel.Children.Add(tileWrapPanel2)
    End Sub

   
End Class
