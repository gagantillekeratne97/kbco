Imports System.IO
Imports System.Data.SqlClient

Public NotInheritable Class ssMyCompanylogo

    Private strSQL As String = ""
    Private errorEvent As String = ""

    Private Sub ssMyCompanylogo_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        'dbConnections.sqlConnection.Close()
    End Sub
    Private Sub ssMyCompanylogo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' My.Application.MinimumSplashScreenDisplayTime = 5000

        lblCopyRights.Text = globalVariables.copyrights
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ProgressBar1.Value += 2

        If ProgressBar1.Value <= 30 Then

            Label1.Text = "Initialized Application ....."


        ElseIf ProgressBar1.Value <= 40 Then
            Label1.Text = "Checking for Updates ....."
            If CheckSoftwareUpdates() Then
                If SaveSoftwareUpdate() Then
                    Label1.Text = "Backup last KBCO version ....."
                    BackupOldExe()
                    Label1.Text = "Starting Update ....."


                    frmLoginWinForm.Dispose()
                    Label1.Text = "KBCO update module opening ....."
                    Process.Start(My.Application.Info.DirectoryPath + "\KBSoftwareUpdate.exe")

                End If
            Else
                Label1.Text = "No New Updates ....."
            End If

        ElseIf ProgressBar1.Value <= 50 Then

            Label1.Text = "Loading Modules ....."

        ElseIf ProgressBar1.Value <= 70 Then

            Label1.Text = "Loading Forms ....."

        ElseIf ProgressBar1.Value <= 100 Then

            Label1.Text = "Please Wait ....."

        End If

        If ProgressBar1.Value = 100 Then



            Me.Visible = False
            frmLoginWinForm.Show()
            Timer1.Stop()
            Timer1.Dispose()
        End If
    End Sub


    Private Sub BackupOldExe()
        If (Not System.IO.Directory.Exists(My.Application.Info.DirectoryPath + "\LastBackup")) Then
            System.IO.Directory.CreateDirectory(My.Application.Info.DirectoryPath + "\LastBackup")
        End If
        Try
            FileCopy(My.Application.Info.DirectoryPath + "\KBCO.exe", My.Application.Info.DirectoryPath + "\LastBackup\KBCO.exe")
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CheckSoftwareUpdates() As Boolean
        Dim Count As Integer

        errorEvent = "Check Software update"
        Try
            strSQL = "SELECT COUNT( [USERHED_USERCODE]) FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERPC] = '" & System.Net.Dns.GetHostName & "' and [USERHED_SWUPDATE] = 1"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            Count = dbConnections.sqlCommand.ExecuteScalar

            If Count = 0 Then
                CheckSoftwareUpdates = False
            Else
                CheckSoftwareUpdates = True
            End If
        Catch ex As Exception

            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally

        End Try
        Return CheckSoftwareUpdates
    End Function

    Private Function SaveSoftwareUpdate() As Boolean
        errorEvent = "Save Software Update"


        Try
            'Dim sFile As New FileInfo("\\192.168.100.6\swtech$\Kbridge\SwUpdate\KBSoftwareUpdate.exe") '\\ check file exsists in the coping folder
            'Dim fileExist As Boolean = sFile.Exists
            'If fileExist Then
            '    FileCopy("\\192.168.100.6\swtech$\Kbridge\SwUpdate\KBSoftwareUpdate.exe", My.Application.Info.DirectoryPath + "\KBSoftwareUpdate.exe") ' \\ Coping .exe from source to destinaction
            'End If

            connectionStaet()
            errorEvent = "Save"
            strSQL = "UPDATE " & selectedDatabaseName & ".dbo.TBLU_USERHED SET USERHED_SWUPDATE =@USERHED_SWUPDATE WHERE     (USERHED_USERPC = '" & System.Net.Dns.GetHostName & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_SWUPDATE", False)
            If dbConnections.sqlCommand.ExecuteNonQuery() Then SaveSoftwareUpdate = True Else SaveSoftwareUpdate = False
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)


        End Try

        Return SaveSoftwareUpdate
    End Function

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
