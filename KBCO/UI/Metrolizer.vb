Imports System.Collections.Generic

Public Class Metrolizer

    Private wrapPanelX As Double = 0

    Public Sub DisplayTiles(ByRef metroStackPanel As Windows.Controls.StackPanel)
        Dim alphabet() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", _
                                    "j", "k", "l", "m", "n", "o", "p", "q", "r", _
                                    "s", "t", "u", "v", "w", "x", "y", "z"}
        Dim numbers() As String = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}
        Dim di As Dictionary(Of String, String()) = New IconsAndPaths().GetIconsAndPaths()

        For Each s As String In alphabet
            Dim letter As String = s
            Dim coll = di.Where(Function(k) k.Key.StartsWith(letter, True, Nothing))
            If (coll.Count > 0) Then
                AddTiles(coll, metroStackPanel, letter)
            End If
        Next

        For Each s As String In numbers
            Dim letter As String = s
            Dim coll = di.Where(Function(k) k.Key.StartsWith(letter, True, Nothing))
            If (coll.Count > 0) Then
                AddTiles(coll, metroStackPanel, letter)
            End If
        Next
    End Sub

    Private Sub AddTiles(ByVal coll As IEnumerable(Of KeyValuePair(Of String, String())), ByRef metroStackPanel As Windows.Controls.StackPanel, ByVal letter As String)
        Dim tileWrapPanel As New Windows.Controls.WrapPanel
        tileWrapPanel.Orientation = Orientation.Vertical
        tileWrapPanel.Margin = New Windows.Thickness(0, 0, 20, 0)
        ' 3 tiles height-wise
        tileWrapPanel.Height = (110 * 3) + (6 * 3)

        For Each kvp As KeyValuePair(Of String, String()) In coll
            Dim newTile As New Tile
            newTile.ExecutablePath = kvp.Value(1)
            newTile.TileIcon.Source = New Windows.Media.Imaging.BitmapImage(New Uri(kvp.Value(0), UriKind.Absolute))
            Dim NameOnly As String() = Nothing

            NameOnly = kvp.Key.ToString.Split(":")
            newTile.TileTxtBlck.Text = NameOnly(1)
            newTile.Tag = NameOnly(0)
            newTile.Margin = New Windows.Thickness(0, 0, 6, 6)
            tileWrapPanel.Children.Add(newTile)
        Next

        WrapPanelLocation(letter, tileWrapPanel)
        metroStackPanel.Children.Add(tileWrapPanel)
    End Sub

    ''' <summary>
    ''' Determines the probable location of a WrapPanel that is added
    ''' to MetroStackPanel (assuming that MetroStackPanel was
    ''' like a Canvas).
    ''' </summary>
    ''' <param name="letter">The alphabetical letter representing a WrapPanel group
    ''' in MetroStackPanel.</param>
    ''' <param name="tileWrapPanel">The WrapPanel that was added to MetroStackPanel.</param>
    ''' <remarks></remarks>
    Private Sub WrapPanelLocation(ByVal letter As String, ByVal tileWrapPanel As Windows.Controls.WrapPanel)
        If (WrapPanelDi.Count = 0) Then
            WrapPanelDi.Add(letter, 0)
        Else
            WrapPanelDi.Add(letter, wrapPanelX)
        End If

        ' Increase value of wrapPanelX as appropriate. 
        ' 6 is right margin of a Tile. 
        If (tileWrapPanel.Children.Count <= 3) Then
            wrapPanelX += ((110 + 6) + 18)
        Else
            Dim numberOfColumns As Double = Math.Ceiling(tileWrapPanel.Children.Count / 3)
            Dim x As Double = (numberOfColumns * 110) + (numberOfColumns * 6) + 18
            wrapPanelX += x
        End If
    End Sub
End Class
