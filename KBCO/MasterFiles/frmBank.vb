
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmBank
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
    Const WMCLOSE As String = "WmClose"
    Private _lastFormSize As Integer

    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If save() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                showCrystalReport()
        End Select
    End Sub
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

    Private Sub createNew()
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save() As Boolean
        save = False

        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            Try
                connectionStaet()
                If isEditClicked Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE " & selectedDatabaseName & ".dbo.TBL_BANK SET Bank_Name =@Bank_Name,MD_DATE=getdate(),MD_BY='" & userSession & "' WHERE Bank_ID=@Bank_ID and COM_ID='" & globalVariables.selectedCompanyID & "'"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBL_BANK (Bank_ID, Bank_Name,CR_DATE,CR_BY,COM_ID) VALUES (@Bank_ID,@Bank_Name,getdate(),'" & userSession & "','" & globalVariables.selectedCompanyID & "')"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_ID", Trim(txtBankID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_Name", Trim(txtBankName.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                If save = True Then
                    If isEditClicked Then
                        AuditDelete(Me.Text, userSession, userName, txtBankID.Text, txtBankName.Text + "(Edit)")
                    Else
                        AuditDelete(Me.Text, userSession, userName, txtBankID.Text, txtBankName.Text + "(Saved)")
                    End If

                End If

            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
                UserAuthorize = False
            End Try
        End If
        Return save
    End Function

    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False

        Try
            Dim IDbackup As String = Trim(txtBankID.Text)
            Dim IDdesc As String = Trim(txtBankName.Text)
            connectionStaet()
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtBankName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM TBL_BANK WHERE Bank_ID=@Bank_ID and COM_ID = '" & globalVariables.selectedCompanyID & "'"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_ID", Trim(txtBankID.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
            End If
            If delete = True Then
                AuditDelete(Me.Text, userSession, userName, IDbackup, IDdesc)
            End If
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
            UserAuthorize = False
        End Try
        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtBankName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            txtBankName.Enabled = True
            txtBankName.Focus()
            isEditClicked = True
            globalFunctions.globalButtonActivation(True, True, False, False, False, True)
            Me.saveBtnStatus()
        End If
    End Sub

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmBankvb_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmBankvb_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmBankvb_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmBankvb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmBankvb_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmBankvb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmBankvb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + PermissionReadingErrorMessgae, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try

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




#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtBankID) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtBankName) = False Then
            Exit Function
        End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtBankID.Text = ""
        txtBankName.Text = ""
        txtBankID.Enabled = True
        txtBankName.Enabled = True
        txtBankID.Focus()
        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

    Private Sub txtBankID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtBankID.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT [Bank_Name] FROM [" & selectedDatabaseName & "].[dbo].[TBL_BANK] where [Bank_ID] = @Bank_ID and COM_ID = '" & Trim(globalVariables.selectedCompanyID) & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_ID", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtBankName.Text = dbConnections.dReader.Item("Bank_Name")

                sender.ReadOnly = True
                txtBankName.ReadOnly = True
            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtBankID.Enabled = False
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub txtBankID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBankID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtBankID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBankID.KeyPress
        generalValObj.isLetterOrDigit(e)
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()
        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"

        Dim path As String = globalVariables.crystalReportpath & "\Reports\rptBank.rpt"
        crystalReportpath = path
        'Dim manual report As New rptBank
        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
        cryRpt.Load(path)
        If Not txtBankID.Text = "" Then
            cryRpt.DataDefinition.FormulaFields("BankID").Text = "'" & txtBankID.Text & "'"
            cryRpt.RecordSelectionFormula = "{TBL_BANK.Bank_ID}='" & Trim(txtBankID.Text) & "'"
        End If
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
#End Region






    Private Sub txtBankID_TextChanged(sender As Object, e As EventArgs) Handles txtBankID.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'dbChnges.MdiParent = frmMDImain
        'dbChnges.Show()

        dbChanges2.MdiParent = frmMDImain
        dbChanges2.Show()
    End Sub
End Class