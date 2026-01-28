Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmMachineMaster

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
                    strSQL = "UPDATE    MTBL_MACHINE_MASTER SET MACHINE_MAKE =@MACHINE_MAKE, MACHINE_MODEL =@MACHINE_MODEL, MACHINE_TONER_PN =@MACHINE_TONER_PN, MACHINE_DEV_PN =@MACHINE_DEV_PN, MACHINE_DRUM_PN =@MACHINE_DRUM_PN, MACHINE_SAGE_PN=@MACHINE_SAGE_PN, MACHINE_P2PRT_PN=@MACHINE_P2PRT_PN, MACHINE_COST=@MACHINE_COST,MACHINE_AV_QTY=@MACHINE_AV_QTY WHERE     (COM_ID = @COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO MTBL_MACHINE_MASTER  (COM_ID, MACHINE_ID, MACHINE_MAKE, MACHINE_MODEL, MACHINE_TONER_PN, MACHINE_DEV_PN, MACHINE_DRUM_PN, MACHINE_SAGE_PN, MACHINE_P2PRT_PN, MACHINE_COST,MACHINE_AV_QTY) VALUES     (@COM_ID, @MACHINE_ID, @MACHINE_MAKE, @MACHINE_MODEL, @MACHINE_TONER_PN, @MACHINE_DEV_PN, @MACHINE_DRUM_PN, @MACHINE_SAGE_PN, @MACHINE_P2PRT_PN, @MACHINE_COST,@MACHINE_AV_QTY)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachineID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_MAKE", Trim(txtMake.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_MODEL", Trim(txtModel.Text))
                If Trim(txtTonerPN.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TONER_PN", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TONER_PN", Trim(txtTonerPN.Text))
                End If

                If Trim(txtDevPN.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_DEV_PN", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_DEV_PN", Trim(txtDevPN.Text))
                End If

                If Trim(txtDrumPN.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_DRUM_PN", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_DRUM_PN", Trim(txtDrumPN.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_SAGE_PN", Trim(txtSagePN.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_P2PRT_PN", Trim(txtP2PRTCode.Text))
                If Trim(txtLastUnitCost.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_COST", 0.0)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_COST", CDbl(txtLastUnitCost.Text))
                End If

                If Trim(txtAvQty.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_AV_QTY", 0)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_AV_QTY", CInt(txtAvQty.Text))
                End If
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                If save = True Then
                    If isEditClicked Then
                        AuditDelete(Me.Text, userSession, userName, txtMachineID.Text, txtMake.Text + "(Edit)")
                    Else
                        AuditDelete(Me.Text, userSession, userName, txtMachineID.Text, txtMake.Text + "(Saved)")
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
            Dim IDbackup As String = Trim(txtMachineID.Text)
            Dim IDdesc As String = Trim(txtMake.Text)
            connectionStaet()
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtMake.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM MTBL_MACHINE_MASTER WHERE     (COM_ID =@COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachineID.Text))
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

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtModel.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            isEnable(True)
            txtMake.Focus()
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
    Private Sub frmMachineMaster_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmMachineMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmMachineMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMachineMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmMachineMaster_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmMachineMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmMachineMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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


    Private Sub isEnable(ByRef enableSate As Boolean)
        txtMake.Enabled = enableSate
        txtModel.Enabled = enableSate
        txtTonerPN.Enabled = enableSate
        txtDevPN.Enabled = enableSate
        txtDrumPN.Enabled = enableSate
        txtSagePN.Enabled = enableSate
        txtP2PRTCode.Enabled = enableSate
        txtLastUnitCost.Enabled = enableSate
        txtAvQty.Enabled = enableSate

    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtMachineID) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtMake) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtModel) = False Then
            Exit Function
        End If

        If generalValObj.isPresent(txtSagePN) = False Then
            Exit Function
        End If

        If generalValObj.isPresent(txtP2PRTCode) = False Then
            Exit Function
        End If

        If generalValObj.isPresent(txtLastUnitCost) = False Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtMachineID.Text = ""
        txtMake.Text = ""
        txtModel.Text = ""
        txtTonerPN.Text = ""
        txtDevPN.Text = ""
        txtDrumPN.Text = ""
        txtSagePN.Text = ""
        txtP2PRTCode.Text = ""
        txtLastUnitCost.Text = ""
        txtAvQty.Text = ""

        lblDevYield.Text = 0
        lblDrumYield.Text = 0
        lblTonerYield.Text = 0

        txtMachineID.Enabled = True
        isEnable(True)

        txtMachineID.Focus()
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

    Private Sub txtMachineID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMachineID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtTonerPN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTonerPN.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtDevPN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDevPN.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtDrumPN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDrumPN.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub



    Private Sub txtMachineID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMachineID.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT  COM_ID, MACHINE_ID, MACHINE_MAKE, MACHINE_MODEL, MACHINE_TONER_PN, MACHINE_DEV_PN, MACHINE_DRUM_PN, MACHINE_SAGE_PN, MACHINE_P2PRT_PN, MACHINE_COST,MACHINE_AV_QTY FROM MTBL_MACHINE_MASTER WHERE (COM_ID = @COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachineID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtMake.Text = dbConnections.dReader.Item("MACHINE_MAKE")
                txtModel.Text = dbConnections.dReader.Item("MACHINE_MODEL")
                If IsDBNull(dbConnections.dReader.Item("MACHINE_TONER_PN")) Then
                    txtTonerPN.Text = ""
                Else
                    txtTonerPN.Text = dbConnections.dReader.Item("MACHINE_TONER_PN")
                End If
                If IsDBNull(dbConnections.dReader.Item("MACHINE_DEV_PN")) Then
                    txtDevPN.Text = ""
                Else
                    txtDevPN.Text = dbConnections.dReader.Item("MACHINE_DEV_PN")
                End If

                If IsDBNull(dbConnections.dReader.Item("MACHINE_DRUM_PN")) Then
                    txtDrumPN.Text = ""
                Else
                    txtDrumPN.Text = dbConnections.dReader.Item("MACHINE_DRUM_PN")
                End If

                If IsDBNull(dbConnections.dReader.Item("MACHINE_SAGE_PN")) Then
                    txtSagePN.Text = ""
                Else
                    txtSagePN.Text = dbConnections.dReader.Item("MACHINE_SAGE_PN")
                End If
                If IsDBNull(dbConnections.dReader.Item("MACHINE_P2PRT_PN")) Then
                    txtP2PRTCode.Text = ""
                Else
                    txtP2PRTCode.Text = dbConnections.dReader.Item("MACHINE_P2PRT_PN")
                End If
                If IsDBNull(dbConnections.dReader.Item("MACHINE_DRUM_PN")) Then
                    txtDrumPN.Text = ""
                Else
                    txtDrumPN.Text = dbConnections.dReader.Item("MACHINE_DRUM_PN")
                End If
                If IsDBNull(dbConnections.dReader.Item("MACHINE_COST")) Then
                    txtLastUnitCost.Text = "0.00"
                Else
                    txtLastUnitCost.Text = dbConnections.dReader.Item("MACHINE_COST")
                End If

                If IsDBNull(dbConnections.dReader.Item("MACHINE_AV_QTY")) Then
                    txtAvQty.Text = 0
                Else
                    txtAvQty.Text = dbConnections.dReader.Item("MACHINE_AV_QTY")
                End If

                txtMachineID.Enabled = False
                isEnable(False)

            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT     WARRANTY_YIELE FROM         TBL_DEVICES_AND_ITEMS WHERE  (COM_ID = @COM_ID) AND (DAI_PN = @DAI_PN)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(txtTonerPN.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                If IsDBNull(dbConnections.dReader.Item("WARRANTY_YIELE")) Then
                    lblTonerYield.Text = "0"
                Else
                    lblTonerYield.Text = dbConnections.dReader.Item("WARRANTY_YIELE")
                End If

            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT     WARRANTY_YIELE FROM         TBL_DEVICES_AND_ITEMS WHERE  (COM_ID = @COM_ID) AND (DAI_PN = @DAI_PN)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(txtDevPN.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                If IsDBNull(dbConnections.dReader.Item("WARRANTY_YIELE")) Then
                    lblDevYield.Text = "0"
                Else
                    lblDevYield.Text = dbConnections.dReader.Item("WARRANTY_YIELE")
                End If

            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT     WARRANTY_YIELE FROM         TBL_DEVICES_AND_ITEMS WHERE  (COM_ID = @COM_ID) AND (DAI_PN = @DAI_PN)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(txtDrumPN.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                If IsDBNull(dbConnections.dReader.Item("WARRANTY_YIELE")) Then
                    lblDrumYield.Text = "0"
                Else
                    lblDrumYield.Text = dbConnections.dReader.Item("WARRANTY_YIELE")
                End If

            End While
            dbConnections.dReader.Close()


            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtMachineID.Enabled = False
                isEnable(True)

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


    Private Sub txtMachineID_TextChanged(sender As Object, e As EventArgs) Handles txtMachineID.TextChanged

    End Sub


    Private Sub txtTonerPN_TextChanged(sender As Object, e As EventArgs) Handles txtTonerPN.TextChanged

    End Sub

    Private Sub txtTonerPN_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTonerPN.Validating

    End Sub
End Class