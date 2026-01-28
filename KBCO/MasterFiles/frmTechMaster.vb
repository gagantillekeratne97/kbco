Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmTechMaster

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
                    strSQL = "UPDATE  MTBL_TECH_MASTER SET  TECH_SAGE_CODE =@TECH_SAGE_CODE, TECH_NAME =@TECH_NAME, TECH_MOBILE =@TECH_MOBILE, TECH_AREA =@TECH_AREA, TECH_ACTIVE =@TECH_ACTIVE WHERE     (COM_ID =@COM_ID) AND (TECH_CODE =@TECH_CODE)"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO MTBL_TECH_MASTER  (COM_ID, TECH_CODE, TECH_SAGE_CODE, TECH_NAME, TECH_MOBILE, TECH_AREA, TECH_ACTIVE) VALUES     (@COM_ID, @TECH_CODE, @TECH_SAGE_CODE, @TECH_NAME, @TECH_MOBILE, @TECH_AREA, @TECH_ACTIVE)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_NAME", Trim(txtTechName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_MOBILE", Trim(txtTechMobile.Text))
                If Trim(txtTechMobile.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_SAGE_CODE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_SAGE_CODE", Trim(txtTechSageCode.Text))
                End If
                If Trim(txtTechArea.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_AREA", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_AREA", Trim(txtTechArea.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_ACTIVE", cbActive.CheckState)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                If save = True Then
                    If isEditClicked Then
                        AuditDelete(Me.Text, userSession, userName, txtTechCode.Text, txtTechName.Text + "(Edit)")
                    Else
                        AuditDelete(Me.Text, userSession, userName, txtTechCode.Text, txtTechName.Text + "(Saved)")
                    End If

                End If
            Catch ex As SqlException
                inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Catch ex As Exception
                inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Finally
                dbConnections.dReader.Close()
                connectionClose()

            End Try
        End If
        Return save
    End Function

    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False

        Try
            Dim IDbackup As String = Trim(txtTechCode.Text)
            Dim IDdesc As String = Trim(txtTechName.Text)
            connectionStaet()
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtTechName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM MTBL_TECH_MASTER WHERE     (COM_ID =@COM_ID) AND (TECH_CODE =@TECH_CODE)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
            End If
            If delete = True Then
                AuditDelete(Me.Text, userSession, userName, IDbackup, IDdesc)
            End If
        Catch ex As Exception
            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            connectionClose()

        End Try
        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtTechName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            txtTechName.Enabled = True
            txtTechSageCode.Enabled = True
            txtTechMobile.Enabled = True
            txtTechArea.Enabled = True
            cbActive.Enabled = True
            txtTechName.Focus()
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
    Private Sub frmTechMaster_Master_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmTechMaster_Master_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmTechMaster_Master_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmTechMaster_Master_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmTechMaster_Master_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmTechMaster_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmTechMaster_Master_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X3) " + PermissionReadingErrorMessgae, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        If generalValObj.isPresent(txtTechCode) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtTechName) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtTechSageCode) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtTechArea) = False Then
            Exit Function
        End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtTechCode.Text = ""
        txtTechName.Text = ""
        txtTechSageCode.Text = ""
        txtTechMobile.Text = ""
        txtTechArea.Text = ""
        cbActive.Checked = False

        txtTechCode.Enabled = True
        txtTechName.Enabled = True
        txtTechSageCode.Enabled = True
        txtTechMobile.Enabled = True
        txtTechArea.Enabled = True
        cbActive.Enabled = True
        txtTechCode.Focus()
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

    Private Sub txtTechCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTechCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub






    Private Sub txtTechCode_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTechCode.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT      TECH_SAGE_CODE, TECH_NAME, TECH_MOBILE, TECH_AREA, TECH_ACTIVE FROM         MTBL_TECH_MASTER WHERE     (COM_ID =@COM_ID) AND (TECH_CODE = @TECH_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                If IsDBNull(dbConnections.dReader.Item("TECH_SAGE_CODE")) Then
                    txtTechSageCode.Text = ""
                Else
                    txtTechSageCode.Text = dbConnections.dReader.Item("TECH_SAGE_CODE")
                End If

                If IsDBNull(dbConnections.dReader.Item("TECH_MOBILE")) Then
                    txtTechMobile.Text = ""
                Else
                    txtTechMobile.Text = dbConnections.dReader.Item("TECH_MOBILE")
                End If

                If IsDBNull(dbConnections.dReader.Item("TECH_AREA")) Then
                    txtTechArea.Text = ""
                Else
                    txtTechArea.Text = dbConnections.dReader.Item("TECH_AREA")
                End If

                cbActive.Checked = dbConnections.dReader.Item("TECH_ACTIVE")

                txtTechCode.Enabled = False
                txtTechName.Enabled = False
                txtTechSageCode.Enabled = False
                txtTechArea.Enabled = False
                txtTechMobile.Enabled = False
                cbActive.Enabled = False


            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtTechCode.Enabled = False
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



#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()
        'Dim reportformObj As New frmCrystalReportViwer
        'Dim reportNamestring As String = "Report"

        'Dim path As String = globalVariables.crystalReportpath & "\Reports\rptBank.rpt"
        'crystalReportpath = path
        ''Dim manual report As New rptBank
        'Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        'Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        'Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        'Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
        'cryRpt.Load(path)
        'If Not txtBankID.Text = "" Then
        '    cryRpt.DataDefinition.FormulaFields("BankID").Text = "'" & txtBankID.Text & "'"
        '    cryRpt.RecordSelectionFormula = "{TBL_BANK.Bank_ID}='" & Trim(txtBankID.Text) & "'"
        'End If
        'globalFunctions.generateReportTemplate(cryRpt, Me.Text)



        'With crConnectionInfo
        '    .ServerName = selectedServerName
        '    .DatabaseName = selectedDatabaseName
        '    .UserID = "db_ab8b61_kbco_admin"
        '    .Password = "Ssg789.541351"
        'End With

        'CrTables = cryRpt.Database.Tables
        'For Each CrTable In CrTables
        '    crtableLogoninfo = CrTable.LogOnInfo
        '    crtableLogoninfo.ConnectionInfo = crConnectionInfo
        '    CrTable.ApplyLogOnInfo(crtableLogoninfo)
        'Next
        'reportformObj.CrystalReportViewer1.Refresh()
        'reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        'reportformObj.CrystalReportViewer1.Refresh()
        'reportformObj.Show()
    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Event  ...............................................................
    '===================================================================================================================
#Region "Button Event"
    Private Sub cbIsVat_CheckedChanged(sender As Object, e As EventArgs) Handles cbActive.CheckedChanged
        If cbActive.Checked = True Then
            cbActive.Text = "YES"
        Else
            cbActive.Text = "NO"
        End If
    End Sub

#End Region

    Private Sub txtTechCode_TextChanged(sender As Object, e As EventArgs) Handles txtTechCode.TextChanged

    End Sub
End Class