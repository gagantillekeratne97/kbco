Imports System.IO
Imports System.Management

Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' See if another instance is already running.
            'If AlreadyRunning() Then
            '    MessageBox.Show( _
            '        "Another K-Bridge is already running.", _
            '        "Already Running", _
            '        MessageBoxButtons.OK, _
            '        MessageBoxIcon.Exclamation)
            '    End
            'End If

            If Not Me.getAppPath() Then Exit Sub
            '//If error on connection file
            If setConnectionString() = False Then
                MessageBox.Show("There seems to be an error in the .ini file. Please contact the administrator", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                frmSettings.ShowDialog()
                End
            End If

            '//If error on selected db server
            If canConnectToDB() = False Then
                MessageBox.Show("Unable to connect to the selected server, Please check the following issues." & vbNewLine & vbNewLine & "1. The selected server could be contacted." & vbNewLine & "2. Required privileges are set for the system to access the server/database" & vbNewLine & "3. Selected server has a valid database", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                frmSettings.ShowDialog()
                End
            End If
        End Sub
#Region "Initialize methods. Not related to the login"
        '//Checks whether the selected server could be connected
        Private Function canConnectToDB() As Boolean
            If dbConnections.connectToDB Then Return True Else Return False
        End Function

        '//Set the connectionString
        Private Function setConnectionString() As Boolean
            DBconnectionFileCreate()
            setConnectionString = False
            Dim Reader As StreamReader
            Try
                If File.Exists(globalVariables.applicationPath & "\DatabaseConnectionCode.txt") Then '//Check whether ini file is available
                    Reader = File.OpenText(globalVariables.applicationPath & "\DatabaseConnectionCode.txt")
                    If Not Reader.EndOfStream Then
                        Dim serverName As String = Reader.ReadLine
                        Dim dbName As String = Reader.ReadLine
                        Dim cn As String = Reader.ReadLine
                        If serverName.Contains("DBServer=") = True AndAlso dbName.Contains("Database=") = True Then
                            globalVariables.selectedServerName = serverName.Replace("DBServer=", "").Trim '//Replaces the datasource section of the text
                            globalVariables.selectedDatabaseName = dbName.Replace("Database=", "").Trim
                            globalVariables.GlobelConnectionStates = cn.Replace("GBConnection=", "").Trim
                            Reader.Close()
                            Reader.Dispose()
                            dbConnections.sqlConnection.ConnectionString = "Data Source=" & selectedServerName & ";Initial Catalog=" & selectedDatabaseName & ";User ID=db_ab8b61_kbco_admin;Password=Ssg789.541351;MultipleActiveResultSets=True;"
                            'dbConnections.sqlConnection.ConnectionString = "Data Source=" & selectedServerName & ";Initial Catalog=" & selectedDatabaseName & ";User ID=gestetner;Password=gocl248;MultipleActiveResultSets=True;"
                            setConnectionString = True
                        Else '//If the lines to be read are in incorrect format or order
                            Reader.Close()
                            Reader.Dispose()
                            setConnectionString = False
                        End If
                    Else
                        '//If the file is empty
                        Reader.Close()
                        Reader.Dispose()
                        setConnectionString = False
                    End If
                Else
                    setConnectionString = False
                End If
            Catch ex As NullReferenceException
                setConnectionString = False
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                setConnectionString = False
                End
            End Try
            Return setConnectionString
        End Function

        '//Returns the connection string txtFile
        Private Function getAppPath() As Boolean
            getAppPath = False
            Try
                Dim fileName As String = AppDomain.CurrentDomain.BaseDirectory
                globalVariables.applicationPath = fileName
                getAppPath = True
            Catch ex As Exception
                getAppPath = False
                MessageBox.Show("Unable to retrieve the application path. Please verify that the application path is accessible", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
            Return getAppPath
        End Function

        Private Sub DBconnectionFileCreate()
            Dim fileName As String = globalVariables.applicationPath & "\DatabaseConnectionCode.txt"
            Dim FirsttimeRun As Boolean = False
            If Not System.IO.File.Exists(fileName) = True Then
                Dim writeFile As IO.StreamWriter
                writeFile = IO.File.CreateText(fileName) 'Creates a new file
                FirsttimeRun = True
                writeFile.Close()
            End If

            If FirsttimeRun = True Then
                Dim lines As New List(Of String)
                lines.AddRange(System.IO.File.ReadAllLines(globalVariables.applicationPath & "\DatabaseConnectionCode.txt")) '
                lines.Add("DBServer=")
                lines.Add("Database=")
                lines.Add("GBConnection=ON")
                System.IO.File.WriteAllLines(fileName, lines.ToArray)
            End If

        End Sub
#End Region
    End Class
End Namespace

