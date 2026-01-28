Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Threading
Imports System.IO

Public Class frmSettings
    Private servers As SqlDataSourceEnumerator
    Private tableServers As DataTable
    Private strSQL As String
    Private isApplicationRestart As Boolean = False
    Private isFormFocused As Boolean
    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
        servers = SqlDataSourceEnumerator.Instance
        tableServers = New DataTable()
    End Sub

    Private Sub updateServerList()
        cmbDatabaseList.Items.Clear()
        Me.Cursor = Cursors.WaitCursor
        If tableServers.Rows.Count = 0 Then
            tableServers = servers.GetDataSources()
            cmbServerList.Items.Add("localhost")
            For Each rowServer As DataRow In tableServers.Rows
                If String.IsNullOrEmpty(rowServer("InstanceName").ToString()) Then '//If only the server name exist
                    cmbServerList.Items.Add(rowServer("ServerName").ToString())
                Else '//if servername and instance name both exist
                    cmbServerList.Items.Add((rowServer("ServerName") & "\" & rowServer("InstanceName")))
                End If
            Next
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub updateDatabaseList()
        '//Load the database names related to the selected server
        Try
            Dim selectedSettings As String = "Data Source=" & cmbServerList.SelectedItem & ";Initial Catalog=master; User ID=sa;Integrated Security=SSPI"
            'Dim selectedSettings As String = "Data Source=" & cmbServerList.SelectedItem & ";Initial Catalog=master;User ID=sa;Trusted_Connection=True;Integrated Security=SSPI"
            Dim sqlConnection As New SqlConnection(selectedSettings)
            sqlConnection.Open()
            '//Get databases names in server in a datareader
            strSQL = "select name FROM sys.databases"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim dReader As SqlDataReader = dbConnections.sqlCommand.ExecuteReader()

            While dReader.Read
                cmbDatabaseList.Items.Add(dReader(0).ToString())
            End While

            If cmbDatabaseList.Items.Count > 0 Then
                cmbDatabaseList.SelectedIndex = 0
            End If
        Catch ex As Exception
            MessageBox.Show("Error code(ST00X1) " + GenaralErrorMessage + ex.Message & ": Database list could not be loaded. Please check the following issues." & vbNewLine & vbNewLine & "1. There is atleast one valid database" & vbNewLine & "2. Verify that the selected server could be contacted" & vbNewLine & "3. You have sufficient privileges to access the selected database server", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            inputErrorLog(Me.Text, "ST00X1", "Databse list not loading", userSession, userName, DateTime.Now, ex.Message)
        End Try
    End Sub

    Private Sub cmbServerList_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServerList.DropDown
        updateServerList()
    End Sub

    Private Sub cmbServerList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServerList.SelectedIndexChanged
        updateDatabaseList()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        updateServerList()
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        If cmbServerList.SelectedIndex = -1 AndAlso cmbDatabaseList.SelectedIndex = -1 Then
            MessageBox.Show("Please selected a valid server and a database.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        '//If the settings are valid
        Dim conf = MessageBox.Show("Are you sure you want to apply these settings ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then writeSettingsToFile()
    End Sub

    Private Sub writeSettingsToFile()
        Try
            Dim objWriter As New StreamWriter(globalVariables.applicationPath & "\DatabaseConnectionCode.txt", False)
            objWriter.WriteLine("DBServer=" & Me.cmbServerList.Text)
            objWriter.WriteLine("Database=" & Me.cmbDatabaseList.SelectedItem)
            objWriter.WriteLine("GBConnection=ON")
            objWriter.Close()
            objWriter.Dispose()
            MessageBox.Show("Connection file updated successfully. Application will now restart for the new settings to be applied", "Settings applied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            isApplicationRestart = True
            Application.Restart()
        Catch ex As Exception
            MessageBox.Show("Error code(ST00X2) " + GenaralErrorMessage + ex.Message & vbNewLine & ": An error occured while updating the settings file. Please re-try", "Error updating the settings file", MessageBoxButtons.OK, MessageBoxIcon.Error)
            inputErrorLog(Me.Text, "ST00X2", "Write Setting to file", userSession, userName, DateTime.Now, ex.Message)
        End Try
    End Sub

    Private Sub frmSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isApplicationRestart = True Then Exit Sub
        Dim conf = MessageBox.Show("Are you sure you want to exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbNo Then e.Cancel = True
    End Sub

    Private Sub frmSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
    End Sub

    Private Sub frmSettings_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        If OpenThroughMDI = True Then
            isFormFocused = False
            OpenThroughMDI = False
        End If
    End Sub

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If globalVariables.selectedServerName = "" Then
            lblConnectedTo.Text = "Currently connected to: N/A"
        Else
            lblConnectedTo.Text = "Currently connected to: " & globalVariables.selectedServerName & "-" & globalVariables.selectedDatabaseName
        End If
    End Sub
End Class