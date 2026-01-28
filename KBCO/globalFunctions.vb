Imports System.Management
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Module globalFunctions




    Public Function GetDefaultPrinter() As String
        Dim settings As PrinterSettings = New PrinterSettings()
        Return settings.PrinterName
    End Function


    ' Return True if another instance
    ' of this program is already running.
    Public Function AlreadyRunning() As Boolean
        ' Get our process name.
        Dim my_proc As Process = Process.GetCurrentProcess
        Dim my_name As String = my_proc.ProcessName

        ' Get information about processes with this name.
        Dim procs() As Process = _
            Process.GetProcessesByName(my_name)

        ' If there is only one, it's us.
        If procs.Length = 1 Then Return False

        ' If there is more than one process,
        ' see if one has a StartTime before ours.
        Dim i As Integer
        For i = 0 To procs.Length - 1
            If procs(i).StartTime < my_proc.StartTime Then _
                Return True
        Next i

        ' If we get here, we were first.
        Return False
    End Function

    Private errorevent As String

    Public Sub AuditDelete(ByVal formName As String, ByRef DeletedBy As String, ByRef DeletedByName As String, ByRef DeletedRecordID As String, ByRef DleletedRecordDesc As String)
        Dim strSQL As String
        connectionStaet()
        errorevent = "Audit Save"
        Try
            strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLAU_AUDIT (AU_FORM_NAME, AU_DE_BY, AU_DE_NAME, AU_DE_DATE, AU_DE_ID, AU_DE_DESC,COM_ID) VALUES     ('" & formName & "', '" & DeletedBy & "', '" & DeletedByName & "',getdate(), '" & DeletedRecordID & "', '" & DleletedRecordDesc & "', '" & globalVariables.selectedCompanyID & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error code(GFUD00X1) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            inputErrorLog("globleFunction", "GFUD00X1", errorevent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
    End Sub

    Public Sub globalButtonActivation(ByVal btnSave As Boolean, ByVal btnNew As Boolean, ByVal btnEdit As Boolean, ByVal btnDelete As Boolean, ByVal btnSearch As Boolean, ByVal btnPrint As Boolean)
        frmMDImain.tsbtnSave.Enabled = btnSave
        frmMDImain.tsbtnNew.Enabled = btnNew
        frmMDImain.tsbtnEdit.Enabled = btnEdit
        frmMDImain.tsbtnDelete.Enabled = btnDelete
        frmMDImain.tsbtnPrint.Enabled = btnPrint
        frmMDImain.tsbtnSearch.Enabled = btnSearch
        'updateStatusStrip() '//Updates the status bar at the bottom of the main menu
    End Sub

    '//Checks for dbNull fields
    Public Function checkForDBNull(ByVal value As Object) As String
        If IsDBNull(value) Then
            Return ""
        Else
            Return value
        End If
    End Function

    Public Sub generateReportTemplate(ByVal report As Object, ByVal formName As String)
        'report.DataDefinition.FormulaFields.Item("PrintUserName").Text = "'" & userName & "'"
        report.DataDefinition.FormulaFields.Item("ReportName").Text = "'" & formName & " REPORT" & "'"
        report.DataDefinition.FormulaFields.Item("CompanyName").Text = "'" & globalVariables.companyName & "'"
        report.DataDefinition.FormulaFields.Item("CompanyAddress").Text = "'" & globalVariables.CompanyAddress & "'"
        report.DataDefinition.FormulaFields.Item("ReportgenaratedBy").Text = "'" & globalVariables.ReportgenaratedBy & "'"
        report.DataDefinition.FormulaFields.Item("copyrights").Text = "'" & globalVariables.copyrights & "'"
    End Sub

    Public Sub KeyPresSoundDisable(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Public Sub DisableALLTextBoxSound(ByRef FormName As Form, ByVal e As System.Windows.Forms.KeyEventArgs)
        For Each c As Control In FormName.Controls
            If c.GetType Is GetType(TextBox) Then
                KeyPresSoundDisable(e)
            End If
        Next
    End Sub
    ' when globe connection Off this code will open for security purpose.
    Public Sub connectionStaet()
        If Not globalVariables.GlobelConnectionStates = "ON" Then
            If dbConnections.sqlConnection.State = ConnectionState.Open Then
                dbConnections.sqlConnection.Close()
                dbConnections.sqlConnection.Open()
            Else
                dbConnections.sqlConnection.Open()
            End If
        End If
    End Sub
    Public Sub connectionClose()
        If Not globalVariables.GlobelConnectionStates = "ON" Then
            dbConnections.sqlConnection.Close()
        End If
    End Sub

    Public Sub ButtonstatesReactivate(ByRef RefCode As String)
        'If Trim(RefCode) = "" Then
        '    globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        'Else
        '    globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        'End If
    End Sub

    ' get first date of month
    Public Function GetFirstDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtFrom As DateTime = dtDate
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1))
        Return dtFrom
    End Function
    ' get last day of the month
    Public Function GetLastDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As New DateTime(dtDate.Year, dtDate.Month, 1)
        dtTo = dtTo.AddMonths(1)
        dtTo = dtTo.AddDays(-(dtTo.Day))
        Return dtTo
    End Function
    'Form name / error id /event/userid / username / datetime/ error message
    Public Sub inputErrorLog(ByRef formname As String, ByRef errorID As String, ByRef eventtype As String, ByRef userid As String, ByVal username As String, ByRef Edatetime As String, ByRef errorMessage As String)
        If userSession = "U001" Then
            MsgBox(errorMessage)
            Exit Sub
        End If
        Dim fileName As String = "\\" & globalVariables.BackupServerIPAddress & "\KBridge\SystemErrorLog_KBCO.txt"
        Dim lines As New List(Of String)
        lines.AddRange(System.IO.File.ReadAllLines(fileName))
        lines.Add(Edatetime + " | " + formname + " | " + errorID + " | " + eventtype + " | " + userid + " | " + username + " | " + errorMessage)

        ' replace the original file
        System.IO.File.WriteAllLines(fileName, lines.ToArray)

    End Sub

    Public Function IncrementString(ByVal strString As String) As String
        ' Increments a string counter
        ' e.g.  "a" -> "b"
        '       "az" -> "ba"
        '       "zzz" -> "aaaa"
        ' strString is the string to increment, assumed to be lower-case alphabetic
        ' Return value is the incremented string

        Dim lngLenString As Long
        Dim strChar As String
        Dim lngI As Long

        lngLenString = Len(strString)

        ' Start at far right
        For lngI = lngLenString To 0 Step -1

            ' If we reach the far left then add an A and exit
            If lngI = 0 Then
                strString = "A" & strString
                Exit For
            End If

            ' Consider next character
            strChar = Mid(strString, lngI, 1)
            If strChar = "Z" Then
                ' If we find Z then increment this to A
                ' and increment the character after this (in next loop iteration)
                strString = Microsoft.VisualBasic.Left(strString, lngI - 1) & "a" & Mid(strString, lngI + 1, lngLenString)
            Else
                ' Increment this non-Z and exit
                strString = Microsoft.VisualBasic.Left(strString, lngI - 1) & Chr(Asc(strChar) + 1) & Mid(strString, lngI + 1, lngLenString)
                Exit For
            End If

        Next lngI

        IncrementString = strString
        Exit Function

    End Function




    '// form resize code for child forms
    Public Sub FormResize(ByVal MDIFormName As Object, ByRef ChildForm As Form)
        If globalVariables.MDISizeW = 0 And globalVariables.LastMDISizeW = 0 Then
            Exit Sub
        End If
        If globalVariables.MDISizeH = 0 And globalVariables.LastMDISizeH Then
            Exit Sub
        End If
        Dim x As Integer = (globalVariables.MDISizeW / globalVariables.LastMDISizeW) * 100
        Dim y As Integer = (globalVariables.MDISizeH / globalVariables.LastMDISizeH) * 100
        MsgBox(x)
        MsgBox(y)
        Dim realx As Integer = (ChildForm.Width * x) / 100
        Dim realy As Integer = (ChildForm.Height * y) / 100

        ' MDIFormName.Size = New Size(ChildForm.Width + nonClientWidth, ChildForm.Height + nonClientHeight)

        ChildForm.Size = New Size(realx, realy)

    End Sub





    Public Sub OpenFile(ByVal ImagePath As String)
        Dim fileName As String = ""

        Dim result As String = ImagePath
        If ImagePath.Length > 4 Then
            result = ImagePath.Substring(ImagePath.Length - 4)
        End If

        If Not result = ".pdf" Then
            fileName = ImagePath + ".pdf"
        Else
            fileName = ImagePath
        End If


        'If File.Exists(fileName) Then
        '    System.Diagnostics.Process.Start((fileName))
        'End If
        'Dim reportformObj As New frmPDFViwer
        Using reportformObj As New frmPDFViwer
            reportformObj.wbPDF.Navigate(fileName)
            reportformObj.Show()
        End Using




    End Sub
    '// Update Datagrid and update values in it
    Public Sub UpdateGrid(ByRef dgTable As DataGridView)
        If Not dgTable.Rows.Count = Nothing Then
            dgTable.CurrentRow.DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
            dgTable.EndEdit()
            dgTable.Update()
            dgTable.Refresh()
        End If

    End Sub

    Public Sub DiposeForm(ByRef FormName As Form)
        FormName.Dispose()
    End Sub


End Module
