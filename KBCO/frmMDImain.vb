Option Explicit On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Windows.Forms.DataVisualization.Charting
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Text
Imports System.Security.Cryptography
Imports System.Reflection
Imports System.IO
Public Class frmMDImain
    Dim ds As New DataSet
    Dim dt As DataTable
    Dim strSQL As String

    Private errorEvent As String

    Private isFormFocused As Boolean

    Dim ChartUserGroup As New List(Of String)

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"

    Private Sub frmMDImain_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If dbConnections.sqlConnection.State = ConnectionState.Broken Then
            dbConnections.sqlConnection.Open()
        End If
        If dbConnections.sqlConnection.State = ConnectionState.Closed Then
            dbConnections.sqlConnection.Open()
        End If
    End Sub



    Private Sub frmMDImain_Enter(sender As Object, e As EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmMDImain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            Else
                dbConnections.DisconnectFromDB()
                NotifyIcon1.Dispose()
                frmLoginWinForm.Dispose()
                Application.Exit()
            End If
        End If

    End Sub

    Private Sub frmMDImain_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmMDImain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmLoginWinForm.Dispose()
        ProcessDirectory()
        ErrorLogFileCreate()



        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
        ' GetLogUsername()

        CheckPermissionUpdate()
        GetGenaralSettings()
        Dim sidemenuObj As New MainMenu
        ElementHost1.Child = sidemenuObj
        GetLogUsername()
        tslblServerName.Text = "Connected to -" & selectedServerName + "\" + selectedDatabaseName & ""
        NotifyIcon1.Text = "K-Bridge (v" & KBridgeVersion & ")"

        If globalVariables.BackupServerIPAddress = "" Then
            globalVariables.crystalReportpath = My.Application.Info.DirectoryPath
        Else
            globalVariables.crystalReportpath = "" & globalVariables.BackupServerIPAddress & "Kbridge"
        End If


        strSQL = "SELECT     MACHINE_START_CODE FROM         L_TBL_COMPANIES WHERE    (COM_ID = '" & globalVariables.selectedCompanyID & "')"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
        dbConnections.sqlCommand.CommandText = strSQL
        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
            globalVariables.MachineRefCode = "P"
        Else
            globalVariables.MachineRefCode = dbConnections.sqlCommand.ExecuteScalar
        End If



        If globalVariables.AutomaticSwUpdateBool = True Then
            If CheckSoftwareUpdates() Then
                If SaveSoftwareUpdate() Then
                    BackupOldExe()
                    connectionClose()
                    dbConnections.sqlConnection.Close()
                    dbConnections.sqlConnection.Dispose()
                    dbConnections.sqlAdapter.Dispose()
                    dbConnections.sqlCommand.Dispose()
                    frmLoginWinForm.Dispose()
                    Process.Start(My.Application.Info.DirectoryPath + "\KBSoftwareUpdate.exe")

                End If

            End If
        End If



    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''all functions of the form .......................................................
    '===================================================================================================================
#Region "Functions & Subs"

    'get log-ed in user name & user group
    Private Sub GetLogUsername()
        errorEvent = "Get Log UserName"
        connectionStaet()
        Try
            strSQL = "SELECT [USERHED_TITLE],[USERHED_POSITION],[USERHED_EMAIL],[USERHED_MOBILE] FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERCODE] ='" & userSession & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                userName = dbConnections.dReader.Item("USERHED_TITLE")
                UsergroupName = dbConnections.dReader.Item("USERHED_POSITION")
                If IsDBNull(dbConnections.dReader.Item("USERHED_EMAIL")) Then
                    UserEmailAddress = ""
                Else
                    UserEmailAddress = dbConnections.dReader.Item("USERHED_EMAIL")
                End If

                If IsDBNull(dbConnections.dReader.Item("USERHED_MOBILE")) Then
                    UserMobileNo = ""
                Else
                    UserMobileNo = dbConnections.dReader.Item("USERHED_MOBILE")
                End If
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            dbConnections.dReader.Close()
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
    'assign shortcut key to save/ add/ create/ delete and print
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Select Case keyData
            Case Keys.F4
                If tsbtnNew.Enabled = True Then
                    performClick("New")
                End If
            Case Keys.F9
                If tsbtnSave.Enabled = True Then
                    performClick("Save")
                End If

            Case Keys.F10
                If tsbtnEdit.Enabled = True Then
                    performClick("Edit")
                End If
            Case Keys.F11
                If tsbtnPrint.Enabled = True Then
                    performClick("Print")
                End If

            Case Keys.F12
                If tsbtnDelete.Enabled = True Then
                    performClick("Delete")
                End If
            Case Keys.Escape
                If Not Me.ActiveMdiChild Is Nothing Then Me.ActiveMdiChild.Close() Else Me.Close() '//If no active forms, close the main form
            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
        Return True
    End Function

    'check active form in the MDI form
    Private Sub performClick(ByVal eventName As String)
        Dim active_form_name As Object = Me.ActiveMdiChild
        active_form_name.preform_btn_click(eventName)
    End Sub
    ' chacking the updates of user group and send to user to update the system
    Private Sub CheckPermissionUpdate()
        errorEvent = "Check for update"
        Dim GroupUpDate As DateTime
        Dim UserUdate As DateTime
        connectionStaet()
        Try
            strSQL = "SELECT TOP (1) TBLU_USERGROUPDET.CR_DATE AS 'GROUP_U_DATE', TBLU_USERDET.DTS_DATE AS 'USER_U_DATE' FROM TBLU_USERDET INNER JOIN TBLU_USERGROUPDET ON TBLU_USERDET.USERDET_MENUTAG = TBLU_USERGROUPDET.UG_MENUTAG WHERE (TBLU_USERDET.USERDET_USERCODE = '" & userSession & "') AND TBLU_USERDET.COM_ID = '" & globalVariables.selectedCompanyID & "' ORDER BY 'USER_U_DATE' DESC, 'GROUP_U_DATE' DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                GroupUpDate = dbConnections.dReader.Item("GROUP_U_DATE")
                UserUdate = dbConnections.dReader.Item("USER_U_DATE")
            End While
            ' check user group date is grater than the group date
            If UserUdate < GroupUpDate Then
                tsbtnPermissionUpdate.Enabled = True
                tsbtnPermissionUpdate.BackColor = Color.Red
            Else
                tsbtnPermissionUpdate.Enabled = False
            End If
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub ErrorLogFileCreate()
        Dim fileName As String = globalVariables.applicationPath & "\SystemErrorLog.txt"
        If System.IO.File.Exists(fileName) = True Then
        Else
            Dim writeFile As IO.StreamWriter
            writeFile = IO.File.CreateText(fileName) 'Creates a new file
        End If
    End Sub

    Private Sub DLL_Injection()
        Dim fileName As String = globalVariables.applicationPath & "\SystemErrorLog.txt"
        If System.IO.File.Exists(fileName) = True Then
        Else
            Dim writeFile As IO.StreamWriter
            writeFile = IO.File.CreateText(fileName) 'Creates a new file
        End If
    End Sub




    ' Process all files in the directory passed in, recurse on any directories
    ' that are found, and process the files they contain.
    Public Shared Sub ProcessDirectory()
        Try

            If selectedDatabaseName = "APPSERVER01\SQLEXPRESS" Then
                Dim SourcePath As String() = Directory.GetFiles("\\192.168.100.6\swtech$\Kbridge\SwUpdate\DLL")
                ' Process the list of files found in the directory.
                Dim fileName As String
                For Each fileName In SourcePath
                    Dim CopyPath As String = My.Application.Info.DirectoryPath & "\" & fileName.Replace("\\192.168.100.6\swtech$\Kbridge\SwUpdate\DLL\", "")

                    If Not File.Exists(My.Application.Info.DirectoryPath & "\" & fileName.Replace("\\192.168.100.6\swtech$\Kbridge\SwUpdate\DLL\", "")) Then
                        FileCopy(fileName, CopyPath)
                    End If

                Next fileName
            End If

        Catch ex As Exception

        End Try



    End Sub 'ProcessDirectory



    Private Sub GetGenaralSettings()
        connectionStaet()
        Try
            strSQL = "SELECT     COMMITMENT, GP_MERGING, VAT, SVAT, SMS, BACKUP_SERVER, SW_AUTO_UPDATE, VAT_SERVICE, NBT1, NBT2, VAT_SERVICE_ACTIVE, INTEREST_ON_CAPITAL, CCP_ON_CONS_SPEAES_AND_LABOR, CPP_ON_BLACK, CPP_ON_COLOR, SALES_GP_MERGING, N_BW_COPIER_COST, N_COLOR_COPIER_COST, N_COLOR_COPIERBW_COST, N_DUPLO_COST, N_DUPLO_INK_COST, EXCHANGE_RATE_BUY,CPP_ON_BLACK_B_MACHINE,DEBTORS_CHECK_DAYS_LIMIT FROM         TBLU_GENARAL_SETTINGS WHERE     (ID = '1') AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim genaralSettingsUpdate As Boolean
            While dbConnections.dReader.Read

                If IsDBNull(dbConnections.dReader.Item("COMMITMENT")) Then
                    genaralSettingsUpdate = False
                Else
                    genaralSettingsUpdate = True
                    globalVariables.Commitment = dbConnections.dReader.Item("COMMITMENT")
                End If
                If IsDBNull(dbConnections.dReader.Item("GP_MERGING")) Then
                    genaralSettingsUpdate = False
                Else
                    genaralSettingsUpdate = True
                    globalVariables.GPmerging = dbConnections.dReader.Item("GP_MERGING")
                End If
                If IsDBNull(dbConnections.dReader.Item("VAT")) Then
                    genaralSettingsUpdate = False
                    globalVariables.VAT = 8
                Else
                    genaralSettingsUpdate = True
                    globalVariables.VAT = dbConnections.dReader.Item("VAT")
                End If
                If IsDBNull(dbConnections.dReader.Item("VAT_SERVICE")) Then
                    globalVariables.VATService = CDbl("12.5")
                Else
                    globalVariables.VATService = dbConnections.dReader.Item("VAT_SERVICE")
                End If

                If IsDBNull(dbConnections.dReader.Item("SVAT")) Then
                    genaralSettingsUpdate = False
                Else
                    genaralSettingsUpdate = True
                    globalVariables.SVAT = dbConnections.dReader.Item("SVAT")
                End If

                If IsDBNull(dbConnections.dReader.Item("NBT1")) Then
                    globalVariables.NBT1 = 2
                Else
                    globalVariables.NBT1 = dbConnections.dReader.Item("NBT1")
                End If

                If IsDBNull(dbConnections.dReader.Item("NBT2")) Then
                    globalVariables.NBT2 = 4
                Else
                    globalVariables.NBT2 = dbConnections.dReader.Item("NBT2")
                End If

                If IsDBNull(dbConnections.dReader.Item("SMS")) Then
                    globalVariables.SMSstate = False
                Else
                    globalVariables.SMSstate = dbConnections.dReader.Item("SMS")
                End If
                If IsDBNull(dbConnections.dReader.Item("BACKUP_SERVER")) Then
                    globalVariables.BackupServerIPAddress = AppDomain.CurrentDomain.BaseDirectory
                    'globalVariables.BackupServerIPAddress = "192.168.100.202"
                Else
                    globalVariables.BackupServerIPAddress = AppDomain.CurrentDomain.BaseDirectory
                    'If selectedServerName = "KASUN-PC\SQLEXPRESS" Then
                    '    globalVariables.BackupServerIPAddress = "D:\My Office Work\Swtech$\KBridge"
                    'Else
                    '    globalVariables.BackupServerIPAddress = dbConnections.dReader.Item("BACKUP_SERVER")
                    'End If

                End If

                If IsDBNull(dbConnections.dReader.Item("SW_AUTO_UPDATE")) Then
                    globalVariables.AutomaticSwUpdateBool = False
                Else
                    globalVariables.AutomaticSwUpdateBool = dbConnections.dReader.Item("SW_AUTO_UPDATE")
                End If

                If IsDBNull(dbConnections.dReader.Item("VAT_SERVICE_ACTIVE")) Then
                    globalVariables.VAT_Service_Active = False
                Else
                    globalVariables.VAT_Service_Active = dbConnections.dReader.Item("VAT_SERVICE_ACTIVE")
                End If

                If genaralSettingsUpdate = False Then
                    MessageBox.Show("Please update General settings before using the system", "Warning..!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                If IsDBNull(dbConnections.dReader.Item("INTEREST_ON_CAPITAL")) Then
                    globalVariables.Interest_on_Capitale = 0.0
                Else
                    globalVariables.Interest_on_Capitale = dbConnections.dReader.Item("INTEREST_ON_CAPITAL")
                End If
                If IsDBNull(dbConnections.dReader.Item("CCP_ON_CONS_SPEAES_AND_LABOR")) Then
                    globalVariables.CPP_on_Cons_Spears_and_Labor = 0.0
                Else
                    globalVariables.CPP_on_Cons_Spears_and_Labor = dbConnections.dReader.Item("CCP_ON_CONS_SPEAES_AND_LABOR")
                End If


                If IsDBNull(dbConnections.dReader.Item("CPP_ON_BLACK")) Then
                    globalVariables.CPP_ON_BLACK = 0.0
                Else
                    globalVariables.CPP_ON_BLACK = dbConnections.dReader.Item("CPP_ON_BLACK")
                End If

                If IsDBNull(dbConnections.dReader.Item("CPP_ON_COLOR")) Then
                    globalVariables.CPP_ON_COLOR = 0.0
                Else
                    globalVariables.CPP_ON_COLOR = dbConnections.dReader.Item("CPP_ON_COLOR")
                End If
                If IsDBNull(dbConnections.dReader.Item("SALES_GP_MERGING")) Then
                    globalVariables.SaleGPMargineVal = 0.0
                Else
                    globalVariables.SaleGPMargineVal = dbConnections.dReader.Item("SALES_GP_MERGING")
                End If
                'N_BW_COPIER_COST,N_COLOR_COPIER_COST
                If IsDBNull(dbConnections.dReader.Item("N_BW_COPIER_COST")) Then
                    globalVariables.N_Black_Copier_Cost = 0.0
                Else
                    globalVariables.N_Black_Copier_Cost = dbConnections.dReader.Item("N_BW_COPIER_COST")
                End If
                If IsDBNull(dbConnections.dReader.Item("N_COLOR_COPIER_COST")) Then
                    globalVariables.N_Color_Copier_Cost = 0.0
                Else
                    globalVariables.N_Color_Copier_Cost = dbConnections.dReader.Item("N_COLOR_COPIER_COST")
                End If
                If IsDBNull(dbConnections.dReader.Item("N_COLOR_COPIERBW_COST")) Then
                    globalVariables.N_Color_Copier_Black = 0.0
                Else
                    globalVariables.N_Color_Copier_Black = dbConnections.dReader.Item("N_COLOR_COPIERBW_COST")
                End If
                ' ,[N_DUPLO_COST] ,[N_DUPLO_INK_COST]

                If IsDBNull(dbConnections.dReader.Item("N_DUPLO_COST")) Then
                    globalVariables.N_Duplo_Copier_Cost = 0.0
                Else
                    globalVariables.N_Duplo_Copier_Cost = dbConnections.dReader.Item("N_DUPLO_COST")
                End If

                If IsDBNull(dbConnections.dReader.Item("N_DUPLO_INK_COST")) Then
                    globalVariables.N_Duplo_ink_cost = 0.0
                Else
                    globalVariables.N_Duplo_ink_cost = dbConnections.dReader.Item("N_DUPLO_INK_COST")
                End If

                If IsDBNull(dbConnections.dReader.Item("EXCHANGE_RATE_BUY")) Then
                    globalVariables.ExchangeRate_Buy = 0.0
                Else
                    globalVariables.ExchangeRate_Buy = dbConnections.dReader.Item("EXCHANGE_RATE_BUY")
                End If

                If IsDBNull(dbConnections.dReader.Item("CPP_ON_BLACK_B_MACHINE")) Then
                    globalVariables.CPP_ON_BLACK_BLACK_MACHINE = 0.0
                Else
                    globalVariables.CPP_ON_BLACK_BLACK_MACHINE = dbConnections.dReader.Item("CPP_ON_BLACK_B_MACHINE")
                End If

                If IsDBNull(dbConnections.dReader.Item("DEBTORS_CHECK_DAYS_LIMIT")) Then
                    globalVariables.DebtorsCheckDayLimit = 0
                Else
                    globalVariables.DebtorsCheckDayLimit = dbConnections.dReader.Item("DEBTORS_CHECK_DAYS_LIMIT")
                End If

            End While
            dbConnections.dReader.Close()

        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub



#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '===================================================================================================================
#Region "Button Events"
    Private Sub tree_MainTreeView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        KeyPresSoundDisable(e) ' disable enter key sound of the MDI form
    End Sub


    Private Sub tsbtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnNew.Click
        performClick("New")
    End Sub

    Private Sub tsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnSave.Click
        performClick("Save")
    End Sub

    Private Sub tsbtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnEdit.Click
        performClick("Edit")
    End Sub

    Private Sub tsbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnDelete.Click
        performClick("Delete")
    End Sub

    Private Sub tsbtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnPrint.Click
        performClick("Print")
    End Sub

    Private Sub tsbtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnSearch.Click
        performClick("Search")
    End Sub
    Private Sub tsbtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnLogin.Click
        Dim x As MessageBoxOptions = MessageBox.Show("Do you want to log-out form the system?", "Log-out", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If x = vbYes Then
            connectionStaet()
            Me.Dispose()
            Application.Restart()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnUpdate.Click
        If SaveSoftwareUpdate() Then
            BackupOldExe()
            connectionClose()
            dbConnections.sqlConnection.Close()
            dbConnections.sqlConnection.Dispose()
            Process.Start(My.Application.Info.DirectoryPath + "\KBSoftwareUpdate.exe")
            End
        End If

    End Sub

    'open User manual .pdf file
    Private Sub tsbtnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbtnHelp.Click
        If File.Exists(globalVariables.applicationPath & "\User Manual" & "\USER MANUAL.pdf") Then
            System.Diagnostics.Process.Start((globalVariables.applicationPath & "\User Manual" & "\USER MANUAL.pdf"))
        End If
    End Sub

    Private Sub lblName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmUserProfile.MdiParent = Me
        frmUserProfile.Show()
    End Sub


#End Region






    Private Sub RegisterDll()
        ''// copy to system 32 folder
        'Dim input_file As String
        'Dim output_dir As String
        'input_file = globalVariables.applicationPath & "crpe32.dll"
        'output_dir = "C:\Windows\System32\crpe32.dll"
        'CopyFiles(input_file, output_dir)

        ''// register dll file
        'Process.Start("regsvr32", "/s " & output_dir)

    End Sub
    Public Sub CopyFiles(ByVal sourcePath As String, ByVal DestinationPath As String)
        If (Directory.Exists(sourcePath)) Then
            For Each fName As String In Directory.GetFiles(sourcePath)
                If File.Exists(fName) Then
                    Dim dFile As String = String.Empty
                    dFile = Path.GetFileName(fName)
                    Dim dFilePath As String = String.Empty
                    dFilePath = DestinationPath + dFile
                    File.Copy(fName, dFilePath, True)
                End If
            Next
        End If
    End Sub




    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs)
        If mainmenuPenal.Height = 0 Then
            mainmenuPenal.Height = Me.Height - 120
        Else
            mainmenuPenal.Height = 0
        End If
    End Sub

    Private Sub btnMainMenu_Click_1(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        If mainmenuPenal.Height = 0 Then
            mainmenuPenal.Height = Me.Height - 120
        Else
            mainmenuPenal.Height = 0
        End If

        If globalVariables.AutomaticSwUpdateBool = True Then
            If CheckSoftwareUpdates() Then


            End If
        End If

    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)

    '    Try
    '        strSQL = "ALTER TABLE L_TBL_LOCAL_PURCHASE_ORDER ADD NBT_AVAILABLE bit"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.ExecuteNonQuery()


    '        MsgBox("Saved")
    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try

    'End Sub

    Private Sub tsbtnPermissionUpdate_Click(sender As Object, e As EventArgs) Handles tsbtnPermissionUpdate.Click
        frmUserUpdates.MdiParent = Me
        frmUserUpdates.Show()
        mainmenuPenal.Height = 0
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
        connectionStaet()
        errorEvent = "Check Software update"
        Try
            strSQL = "SELECT COUNT( [USERHED_USERCODE]) FROM [" & selectedDatabaseName & "].[dbo].[TBLU_USERHED] WHERE [USERHED_USERPC] = '" & System.Net.Dns.GetHostName & "' and [USERHED_SWUPDATE] = 1"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            Count = dbConnections.sqlCommand.ExecuteScalar

            If Count = 0 Then
                tsbtnUpdate.Enabled = False
                tslblNewUpdate.Visible = False
                CheckSoftwareUpdates = False
            Else
                tsbtnUpdate.Enabled = True
                tslblNewUpdate.Visible = True
                CheckSoftwareUpdates = True
                tsbtnUpdate.BackColor = Color.Red
            End If
        Catch ex As Exception

            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
        Return CheckSoftwareUpdates
    End Function

    Private Function SaveSoftwareUpdate() As Boolean
        errorEvent = "Save Software Update"
        Dim conf = MessageBox.Show("New Update Available. Please click YES to update  your system." & vbCrLf & "By clicking Yes will restart KBCO system.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then

            Try
                'Dim sFile As New FileInfo("\\192.168.100.6\swtech$\Kbridge\SwUpdate\KBSoftwareUpdate.exe") '\\ check file exists in the coping folder
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
            Finally
                connectionClose()
            End Try
        End If
        Return SaveSoftwareUpdate
    End Function







End Class
