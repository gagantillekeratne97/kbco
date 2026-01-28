Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class frmSetSoftwareUpdate
    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Const WMCLOSE As String = "WmClose"
    Private generalValObj As New generalValidation
    Private PCname As String = System.Net.Dns.GetHostName
    Private img As String
    Private MD5obj As New MD5
    '//Active form perform btn click case 
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                ' Me.createNew()
            Case "Save"
                ' If Save() Then FormClear()
            Case "Edit"
                ' Me.FormEdit()
            Case "Delete"
                ' If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'showCrystalReport()
        End Select
    End Sub

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"
    Private Sub createNew()
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save() As Boolean
        errorEvent = "Save"
        save = False
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If

        Dim conf = MessageBox.Show("Do you wish to save this record ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If

            connectionStaet()
            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
            dgSWUpdate.RefreshEdit()
            dgSWUpdate.Refresh()
            Try
                Dim RowCount As Integer = dgSWUpdate.RowCount
                For Each row As DataGridViewRow In dgSWUpdate.Rows


                    dbConnections.sqlCommand.Parameters.Clear()

                    strSQL = "UPDATE " & selectedDatabaseName & ".dbo.TBLU_USERHED SET USERHED_SWUPDATE =@USERHED_SWUPDATE, USERHED_SWUP_DATE =GETDATE() WHERE     (USERHED_USERCODE = '" & Trim(dgSWUpdate.Rows(row.Index).Cells("U_CODE").Value()) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)

                    dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_SWUPDATE", Trim(dgSWUpdate.Rows(row.Index).Cells("U_UPDATE").Value()))
             
                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                Next
                'Add your save code here  

                dbConnections.sqlTransaction.Commit()
                If save Then
                    MessageBox.Show("Update Sent", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As SqlException

              
                        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                dbConnections.sqlTransaction.Rollback()

            Finally
                dbConnections.dReader.Close()
                connectionClose()

            End Try
            If save = True Then
                Try
                    connectionStaet()
                    errorEvent = "Edit"
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.U_TBL_SW_UPDATE_LOG ( UPDATE_DESC, UPDATE_SUBMIT_DATE, DEPARTMENT, UPDATE_BY,MODE_REQUEST_BY) VALUES     (@UPDATE_DESC, GETDATE(), @DEPARTMENT, @UPDATE_BY,@MODE_REQUEST_BY)"

                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                    dbConnections.sqlCommand.Parameters.AddWithValue("@UPDATE_DESC", Trim(txtPurpose.Text))
                    dbConnections.sqlCommand.Parameters.AddWithValue("@DEPARTMENT", Trim(cmbDepartment.Text))
                    dbConnections.sqlCommand.Parameters.AddWithValue("@UPDATE_BY", Trim(userSession))
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MODE_REQUEST_BY", Trim(txtRequestBy.Text))
                    '
                    dbConnections.sqlCommand.ExecuteNonQuery()
                Catch ex As Exception
                    'MsgBox(ex.Message)
                Finally
                    connectionClose()
                End Try
            End If
            

        End If

        Return save
    End Function


#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmAddUser_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & " " & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmAddUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        globalFunctions.globalButtonActivation(True, True, True, True, True, True)
        AddtoGrid()
        GetLastUpdateDate()
        AddCompanyDEPTTOoCMB()
        cmbDepartment.SelectedIndex = 0
    End Sub

    Private Sub frmAddUser_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmAddUser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmAddUser_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmAddUser_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        errorEvent = "read User Permission"
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try
        AddCompanyDEPTTOoCMB()
    End Sub
 

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''all functions of the form .......................................................
    '===================================================================================================================
#Region "Functions & Subs"
    Public Function IsFormClosing() As Boolean
        Dim stackTrace As System.Diagnostics.StackTrace = New System.Diagnostics.StackTrace
        For Each sf As System.Diagnostics.StackFrame In stackTrace.GetFrames
            If (sf.GetMethod.Name = WMCLOSE) Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub saveBtnStatus()
        If frmMDImain.tsbtnSave.Enabled Then btnStatus(0) = True Else btnStatus(0) = False
        If frmMDImain.tsbtnNew.Enabled Then btnStatus(1) = True Else btnStatus(1) = False
        If frmMDImain.tsbtnEdit.Enabled Then btnStatus(2) = True Else btnStatus(2) = False
        If frmMDImain.tsbtnDelete.Enabled Then btnStatus(3) = True Else btnStatus(3) = False
        If frmMDImain.tsbtnPrint.Enabled Then btnStatus(4) = True Else btnStatus(4) = False
    End Sub

    Private Sub AddCompanyDEPTTOoCMB()
        connectionStaet()
        errorEvent = "add to cmb"
        cmbDepartment.Items.Clear()
        Try
            cmbDepartment.Items.Add("ALL")
            cmbDepartment.Items.Add("NONE")
            strSQL = "SELECT DEPT_NAME FROM [" & selectedDatabaseName & "].[dbo].[L_TBL_COMPANYDEPT] GROUP BY L_TBL_COMPANYDEPT.DEPT_NAME HAVING      (COUNT(*) > 0)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                cmbDepartment.Items.Add(dbConnections.dReader.Item("DEPT_NAME"))
            End While
            dbConnections.dReader.Close()
            cmbDepartment.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)

        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub AddtoGrid()
        Dim UseFor As String = ""

        errorEvent = "Add to grid()"
        dgSWUpdate.Rows.Clear()
        Try
            connectionStaet()
            strSQL = "SELECT     USERHED_USERCODE, USERHED_TITLE, USERHED_USERPC, USERHED_SWUPDATE, USERHED_SWUP_DATE,USERHED_POSITION FROM " & selectedDatabaseName & ".dbo.TBLU_USERHED WHERE (USERHED_ACTIVEUSER = 1)"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("USERHED_USERPC")) = False Then

                    populatreDatagrid(dbConnections.dReader("USERHED_USERCODE"), dbConnections.dReader("USERHED_TITLE"), dbConnections.dReader.Item("USERHED_USERPC"), DateTime.Now, dbConnections.dReader.Item("USERHED_POSITION"), True)

                End If
            End While
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef UserCode As String, ByRef Username As String, ByRef UserPC As String, ByRef SWDate As String, ByRef Department As String, ByRef IsUpdate As Boolean)
        dgSWUpdate.ColumnCount = 6
        dgSWUpdate.Rows.Add(UserCode, Username, UserPC, SWDate, Department, IsUpdate)
    End Sub
    Private Sub GetLastUpdateDate()
        'SELECT     MAX(UPDATE_SUBMIT_DATE) AS Expr1 FROM         Gestetner.dbo.U_TBL_SW_UPDATE_LOG
        Try
            connectionStaet()
            strSQL = "SELECT     MAX(UPDATE_SUBMIT_DATE) AS Expr1 FROM         " & selectedDatabaseName & ".dbo.U_TBL_SW_UPDATE_LOG"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                lblLastUpdateDate.Text = "None"
            Else
                lblLastUpdateDate.Text = dbConnections.sqlCommand.ExecuteScalar
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            connectionClose()
        End Try

    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtRequestBy) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtPurpose) = False Then
            Exit Function
        End If
     
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtPurpose.Text = ""
        txtRequestBy.Text = ""
        cmbDepartment.Text = "ALL"
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReportHistory()

        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"

        Dim path As String = globalVariables.crystalReportpath & "\Reports\rptSWststus.rpt"

        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

        cryRpt.Load(path)


        '  globalFunctions.generateReportTemplate(cryRpt, "K-Bridge Software Update Status")

        cryRpt.DataDefinition.FormulaFields.Item("CompanyName").Text = "'" & globalVariables.companyName & "'"
        cryRpt.DataDefinition.FormulaFields.Item("CompanyAddress").Text = "'" & globalVariables.CompanyAddress & "'"
        cryRpt.DataDefinition.FormulaFields.Item("ReportgenaratedBy").Text = "'" & globalVariables.ReportgenaratedBy & "'"
        cryRpt.DataDefinition.FormulaFields.Item("copyrights").Text = "'" & globalVariables.copyrights & "'"

        With crConnectionInfo
            .ServerName = selectedServerName
            .DatabaseName = selectedDatabaseName
            .UserID = "db_ab8b61_kbco_admin"
            .Password = "Ssg789.541351"
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)



        Next
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.Show()
    End Sub
#End Region

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        cmbDepartment.SelectedIndex = 0
        AddtoGrid()
        GetLastUpdateDate()
    End Sub

    Private Sub btnSendUpdate_Click(sender As Object, e As EventArgs) Handles btnSendUpdate.Click
        save()
        GetLastUpdateDate()
    End Sub

    Private Sub btnViewSWupdateStatus_Click(sender As Object, e As EventArgs) Handles btnViewSWupdateStatus.Click
        showCrystalReport()
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged

        If cmbDepartment.Text = "ALL" Then

            For i = 0 To dgSWUpdate.Rows.Count - 1
                dgSWUpdate.Rows(i).Cells("U_UPDATE").Value = True
            Next


        ElseIf cmbDepartment.Text = "NONE" Then

            For i = 0 To dgSWUpdate.Rows.Count - 1
                dgSWUpdate.Rows(i).Cells("U_UPDATE").Value = False
            Next

        Else
            For i = 0 To dgSWUpdate.Rows.Count - 1

                If dgSWUpdate.Rows(i).Cells("DEPT").Value = cmbDepartment.Text Then
                    '
                    dgSWUpdate.Rows(i).Cells("U_UPDATE").Value = True
                Else
                    dgSWUpdate.Rows(i).Cells("U_UPDATE").Value = False
                End If
            Next
        End If

      
    End Sub



    Private Sub showCrystalReport()
        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"

        Dim path As String = globalVariables.crystalReportpath & "\Reports\rptSWUpdateHistory.rpt"
        crystalReportpath = path
        'Dim manualreport As New rptBank
        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
        cryRpt.Load(path)
     
        globalFunctions.generateReportTemplate(cryRpt, Me.Text)

        With crConnectionInfo
            .ServerName = selectedServerName
            .DatabaseName = selectedDatabaseName
            .UserID = "db_ab8b61_kbco_admin"
            .Password = "Ssg789.541351"
        End With

        CrTables = cryRpt.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.Show()
    End Sub

    Private Sub btnUpdateHistory_Click(sender As Object, e As EventArgs) Handles btnUpdateHistory.Click
        showCrystalReportHistory()
    End Sub
End Class