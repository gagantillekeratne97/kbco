Imports System.Windows.Threading
Imports System.Windows.Media.Animation
Imports System.IO
Imports System.Data.SqlClient
Imports System.Windows.Media.Imaging

Public Class MainMenu

    Private initMouseX As Double
    Private finalMouseX As Double
    Private x As Double
    Private newX As Double
    Private timer As DispatcherTimer
    Private anim As DoubleAnimationUsingKeyFrames

    Private Sub MainWindow_Initialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized
        timer = New DispatcherTimer
        anim = New DoubleAnimationUsingKeyFrames

        anim.Duration = TimeSpan.FromMilliseconds(1800)
        timer.Interval = New TimeSpan(0, 0, 0, 0, 1000)
        AddHandler timer.Tick, AddressOf timer_Tick
    End Sub

    'Private Sub MainWindow_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
    '    ' Move MetroStackPanel so that the WrapPanel with the
    '    ' required alphabetical group is displayed first.
    '    ShiftStackPanel(e.Key.ToString(), MetroStackPanel)
    'End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim metro As New Metrolizer
        Dim metro2 As New Metrolizer2
        metro.DisplayTiles(MetroStackPanel)
        metro2.DisplayTiles(MetroStackPanelFavorits)
        LoginInfor()
        lblSelectedCompanyName.Content = Get_CompanyName(globalVariables.selectedCompanyID)
        If Not UsergroupName = "Administrator" Then
            btnProfile_Copy1.Visibility = Windows.Visibility.Hidden
            btnProfile_Copy2.Visibility = Windows.Visibility.Hidden
        End If
    End Sub


    Private Sub MainCanvas_PreviewMouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles MainCanvas.PreviewMouseLeftButtonDown
        initMouseX = e.GetPosition(MainCanvas).X
        x = Windows.Controls.Canvas.GetLeft(MetroStackPanel)
    End Sub


    Private Sub MainCanvas_PreviewMouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles MainCanvas.PreviewMouseLeftButtonUp
        finalMouseX = e.GetPosition(MainCanvas).X
        Dim diff As Double = Math.Abs(finalMouseX - initMouseX)

        ' Make sure the diff is substantial so that tiles
        ' don't scroll on double-click.
        If (diff > 5) Then
            If (finalMouseX < initMouseX) Then
                newX = x - (diff * 2)
            ElseIf (finalMouseX > initMouseX) Then
                newX = x + (diff * 2)
            End If

            anim.KeyFrames.Add(New SplineDoubleKeyFrame(newX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), New KeySpline(0.161, 0.079, 0.008, 1)))
            anim.FillBehavior = FillBehavior.HoldEnd
            MetroStackPanel.BeginAnimation(Windows.Controls.Canvas.LeftProperty, anim)
            anim.KeyFrames.Clear()
            timer.Start()
        End If
    End Sub

    ' Check whether the StackPanel is no longer in view and
    ' return it to a suitable postion.
    Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        Dim mspWidth As Double = MetroStackPanel.ActualWidth

        If (newX > 200) Then
            anim.KeyFrames.Add(New SplineDoubleKeyFrame(45, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), New KeySpline(0.161, 0.079, 0.008, 1)))
            anim.FillBehavior = FillBehavior.HoldEnd
            MetroStackPanel.BeginAnimation(Windows.Controls.Canvas.LeftProperty, anim)
            anim.KeyFrames.Clear()
        ElseIf ((newX + mspWidth) < 500) Then
            Dim widthX As Double = 500 - (newX + mspWidth)
            Dim shiftX As Double = newX + widthX
            anim.KeyFrames.Add(New SplineDoubleKeyFrame(shiftX, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)), New KeySpline(0.161, 0.079, 0.008, 1)))
            anim.FillBehavior = FillBehavior.HoldEnd
            MetroStackPanel.BeginAnimation(Windows.Controls.Canvas.LeftProperty, anim)
            anim.KeyFrames.Clear()
        End If
        timer.Stop()
    End Sub


    Private Sub ChangeSkin(ByVal path As String)
        Dim skinRD As New Windows.ResourceDictionary
        skinRD.Source = New Uri(path, UriKind.Relative)
        Me.Resources.MergedDictionaries.Clear()
        Me.Resources.MergedDictionaries.Add(skinRD)
    End Sub

    Private Sub SaveSkin(ByVal path As String)
        My.Settings.SkinPath = path
        My.Settings.Save()
    End Sub


    Private Sub btnProfile_Click(sender As Object, e As Windows.RoutedEventArgs) Handles btnProfile.Click
        frmUserProfile.MdiParent = frmMDImain
        frmUserProfile.Show()
        frmMDImain.mainmenuPenal.Height = 0
    End Sub



    Private Sub LoginInfor()
        lblUserCode.Text = userSession
        lblUserName.Text = userName
    End Sub



    Private Sub btnProfile_Copy_Click(sender As Object, e As Windows.RoutedEventArgs) Handles btnProfile_Copy.Click
        frmAbout.MdiParent = frmMDImain
        frmAbout.Show()
        frmMDImain.mainmenuPenal.Height = 0
    End Sub

    Private Sub btnProfile_Copy1_Click(sender As Object, e As Windows.RoutedEventArgs) Handles btnProfile_Copy1.Click
        frmManageMenuItems.MdiParent = frmMDImain
        frmManageMenuItems.Show()
        frmMDImain.mainmenuPenal.Height = 0
    End Sub

    Private Sub btnProfile_Copy1_Loaded(sender As Object, e As Windows.RoutedEventArgs) Handles btnProfile_Copy1.Loaded
        If Not UsergroupName = "Administrator" Then
            btnProfile_Copy1.Visibility = Windows.Visibility.Hidden
            btnProfile_Copy2.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub btnProfile_Copy2_Click(sender As Object, e As Windows.RoutedEventArgs) Handles btnProfile_Copy2.Click
        frmManageSearchControllers.MdiParent = frmMDImain
        frmManageSearchControllers.Show()
        frmMDImain.mainmenuPenal.Height = 0
    End Sub

    Dim strSQL As String = ""

    Private Function Get_CompanyName(ByRef COM_ID As String) As String
        Get_CompanyName = ""
        Try
            strSQL = "SELECT COM_NAME FROM L_TBL_COMPANIES WHERE     (COM_ID = '" & Trim(COM_ID) & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Get_CompanyName = Trim(dbConnections.sqlCommand.ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        Return Get_CompanyName
    End Function

    Private Sub btnCreditNote_Click(sender As Object, e As Windows.RoutedEventArgs)

    End Sub
End Class
