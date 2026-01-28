Public Class ListViewEx
    Inherits System.Windows.Forms.ListView

    Private _gridLines As Boolean = False
    Private _useDefaultGridLines As Boolean = False
    Private _gridLineColor As Color = Color.Black
    Private _itemHighlightColor As Color = Color.FromKnownColor(KnownColor.Highlight)
    Private _itemNotFocusedHighlighColor As Color = Color.FromKnownColor(KnownColor.MenuBar)

    Public Property GridLineColor() As Color
        Get
            Return _gridLineColor
        End Get
        Set(ByVal value As Color)
            If value <> _gridLineColor Then
                _gridLineColor = value
                If _gridLines Then
                    Me.Invalidate()
                End If
            End If
        End Set
    End Property

    Public Property ItemHighlightColor() As Color
        Get
            Return _itemHighlightColor
        End Get
        Set(ByVal value As Color)
            If value <> _itemHighlightColor Then
                _itemHighlightColor = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property ItemNotFocusedHighlighColor() As Color
        Get
            Return _itemNotFocusedHighlighColor
        End Get
        Set(ByVal value As Color)
            If value <> _itemNotFocusedHighlighColor Then
                _itemNotFocusedHighlighColor = value
                Me.Invalidate()
            End If
        End Set
    End Property

    Private ReadOnly Property DrawCustomGridLines() As Boolean
        Get
            Return (_gridLines And Not _useDefaultGridLines)
        End Get
    End Property

    Public Shadows Property GridLines() As Boolean
        Get
            Return _gridLines
        End Get
        Set(ByVal value As Boolean)
            _gridLines = value
        End Set
    End Property

    Public Property UseDefaultGridLines() As Boolean
        Get
            Return _useDefaultGridLines
        End Get
        Set(ByVal value As Boolean)
            If _useDefaultGridLines <> value Then
                _useDefaultGridLines = value
            End If
            MyBase.GridLines = value
            MyBase.OwnerDraw = Not value
        End Set
    End Property

    Protected Overrides Sub OnDrawColumnHeader(ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs)
        e.DrawDefault = True
        MyBase.OnDrawColumnHeader(e)
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)
        For Each selectedIndex As Integer In MyBase.SelectedIndices
            MyBase.RedrawItems(selectedIndex, selectedIndex, False)
        Next
        MyBase.OnLostFocus(e)
    End Sub

    Protected Overrides Sub OnDrawSubItem(ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs)


        Dim drawAsDefault As Boolean = False
        Dim highlightBounds As Rectangle = Nothing
        Dim highlightBrush As SolidBrush = Nothing

        'FIRST DETERMINE THE COLOR
        If e.Item.Selected Then
            If MyBase.Focused Then
                highlightBrush = New SolidBrush(_itemHighlightColor)
            ElseIf HideSelection Then
                drawAsDefault = True
            Else
                highlightBrush = New SolidBrush(_itemNotFocusedHighlighColor)
            End If
        Else
            drawAsDefault = True
        End If

        If drawAsDefault Then
            e.DrawBackground()
        Else
            'NEXT DETERMINE THE BOUNDS IN WHICH TO DRAW THE BACKGROUND
            If FullRowSelect Then
                highlightBounds = e.Bounds
            Else
                highlightBounds = e.Item.GetBounds(ItemBoundsPortion.Label)
            End If

            'ONLY DRAW HIGHLIGHT IN 1 OF 2 CASES
            'CASE 1 - FULL ROW SELECT (AND DRAWING ANY ITEM)
            'CASE 2 - NOT FULL ROW SELECT (AND DRAWING 1ST ITEM)
            If FullRowSelect Then
                e.Graphics.FillRectangle(highlightBrush, highlightBounds)
            ElseIf e.ColumnIndex = 0 Then
                e.Graphics.FillRectangle(highlightBrush, highlightBounds)
            Else
                e.DrawBackground()
            End If
        End If

        e.DrawText()

        If _gridLines Then
            e.Graphics.DrawRectangle(New Pen(_gridLineColor), e.Bounds)
        End If


        If FullRowSelect Then
            e.DrawFocusRectangle(e.Item.GetBounds(ItemBoundsPortion.Entire))
        Else
            e.DrawFocusRectangle(e.Item.GetBounds(ItemBoundsPortion.Label))
        End If


        MyBase.OnDrawSubItem(e)

    End Sub
End Class
