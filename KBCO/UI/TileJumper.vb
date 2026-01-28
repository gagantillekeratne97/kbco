Imports System.Windows.Media.Animation

Module QuickJumper

    Public WrapPanelDi As New Dictionary(Of String, Double)

    ' Depending on which key was pressed moves MetroStackPanel so that
    ' the WrapPanel containing required tiles is in view.
    Public Sub ShiftStackPanel(ByRef letter As String, ByRef metroStackPanel As Windows.Controls.StackPanel)
        If WrapPanelDi.ContainsKey(letter.ToLower()) Then
            Dim doubleAnim As New DoubleAnimationUsingKeyFrames()
            Dim newX As Double = WrapPanelDi(letter.ToLower())
            doubleAnim.Duration = TimeSpan.FromMilliseconds(1800)

            doubleAnim.KeyFrames.Add(New SplineDoubleKeyFrame(-newX, _
                                                              KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), _
                                                              New KeySpline(0.161, 0.079, 0.008, 1)))
            doubleAnim.FillBehavior = FillBehavior.HoldEnd
            metroStackPanel.BeginAnimation(Windows.Controls.Canvas.LeftProperty, doubleAnim)
            doubleAnim.KeyFrames.Clear()
        End If
    End Sub

End Module
