'Error log error code -  CU00#
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmSysConnectedUsers
    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Dim generalValObj As New generalValidation
    Private DGselectedIndex As Integer = -1
    Const WMCLOSE As String = "WmClose"
    Private ItemQtyCost As Double


    '// approval matrix variables
    Private UserInformdate As String
    Private UserCreditlimit As String
    Private UserDebtorOutstanding As String
    Private UserGrossProfit As String
    Private UserCommitment As String
    Private UserSalesType As String
    Private CorrectUser As Boolean = False

    Private Sub frmSysConnectedUsers_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        AddtoGrid()
        lblUserCount.Text = dgOnlineUsers.RowCount.ToString + "" + "Users"
    End Sub

    Private Sub frmSysConnectedUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddtoGrid()
        lblUserCount.Text = dgOnlineUsers.RowCount.ToString + "" + "Users"
    End Sub

    Private Sub AddtoGrid()

        errorEvent = "Add to grid()"
        dgOnlineUsers.Rows.Clear()
        Try
            connectionStaet()
            'strSQL = "SELECT  hostname,net_library,net_address,client_net_address FROM    sys.sysprocesses AS S INNER JOIN    sys.dm_exec_connections AS decc ON S.spid = decc.session_id"
            strSQL = "SELECT  s.hostname,s.net_library,s.net_address, decc.client_net_address FROM    sys.sysprocesses AS S INNER JOIN    sys.dm_exec_connections AS decc ON S.spid = decc.session_id group by s.hostname,s.net_library,s.net_address,decc.client_net_address"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                populatreDatagrid(Trim(dbConnections.dReader("hostname")), dbConnections.dReader("net_library"), dbConnections.dReader("net_address"), dbConnections.dReader("client_net_address"))
            End While
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
    Private Sub populatreDatagrid(ByVal A As String, ByVal B As String, ByVal C As String, ByVal D As String)
        dgOnlineUsers.ColumnCount = 4
        dgOnlineUsers.Rows.Add(A, B, C, D)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        AddtoGrid()
        lblUserCount.Text = dgOnlineUsers.RowCount.ToString + "" + "Users"
    End Sub
End Class