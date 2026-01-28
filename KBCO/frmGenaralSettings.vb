'need to update error log
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Public Class frmGenaralSettings
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

    'testing for user authorization

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
                'If delete() Then FormClear()
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
                    strSQL = "UPDATE " & selectedDatabaseName & ".dbo.TBLU_GENARAL_SETTINGS SET BACKUP_SERVER=@BACKUP_SERVER, COMMITMENT =@COMMITMENT, GP_MERGING =@GP_MERGING,VAT=@VAT,SVAT=@SVAT,SMS=@SMS, MD_BY ='" & userSession & "', MD_DATE =GETDATE(),SW_AUTO_UPDATE=@SW_AUTO_UPDATE,VAT_SERVICE=@VAT_SERVICE,NBT1=@NBT1,NBT2=@NBT2,VAT_SERVICE_ACTIVE=@VAT_SERVICE_ACTIVE,INTEREST_ON_CAPITAL=@INTEREST_ON_CAPITAL,CCP_ON_CONS_SPEAES_AND_LABOR=@CCP_ON_CONS_SPEAES_AND_LABOR,CPP_ON_BLACK=@CPP_ON_BLACK,CPP_ON_COLOR=@CPP_ON_COLOR,SALES_GP_MERGING=@SALES_GP_MERGING, N_BW_COPIER_COST=@N_BW_COPIER_COST,N_COLOR_COPIER_COST=@N_COLOR_COPIER_COST,N_COLOR_COPIERBW_COST=@N_COLOR_COPIERBW_COST,N_DUPLO_COST=@N_DUPLO_COST,N_DUPLO_INK_COST=@N_DUPLO_INK_COST,EXCHANGE_RATE_BUY=@EXCHANGE_RATE_BUY,CPP_ON_BLACK_B_MACHINE=@CPP_ON_BLACK_B_MACHINE , DEBTORS_CHECK_DAYS_LIMIT=@DEBTORS_CHECK_DAYS_LIMIT WHERE ID = '1' and COM_ID='" & globalVariables.selectedCompanyID & "'"
                Else
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBLU_GENARAL_SETTINGS (BACKUP_SERVER,ID,COMMITMENT, GP_MERGING,VAT,SVAT,SMS, CR_BY, CR_DATE,SW_AUTO_UPDATE,VAT_SERVICE,NBT1,NBT2,VAT_SERVICE_ACTIVE,INTEREST_ON_CAPITAL,CCP_ON_CONS_SPEAES_AND_LABOR,CPP_ON_BLACK,CPP_ON_COLOR,SALES_GP_MERGING,N_BW_COPIER_COST,N_COLOR_COPIER_COST,N_COLOR_COPIERBW_COST,N_DUPLO_COST,N_DUPLO_INK_COST,EXCHANGE_RATE_BUY,CPP_ON_BLACK_B_MACHINE,COM_ID,DEBTORS_CHECK_DAYS_LIMIT) VALUES     (@BACKUP_SERVER,'1',@COMMITMENT, @GP_MERGING,@VAT,@SVAT,@SMS, '" & userSession & "', GETDATE(),@SW_AUTO_UPDATE,@VAT_SERVICE,@NBT1,@NBT2,@VAT_SERVICE_ACTIVE,@INTEREST_ON_CAPITAL,@CCP_ON_CONS_SPEAES_AND_LABOR,@CPP_ON_BLACK,@CPP_ON_COLOR,@SALES_GP_MERGING,@N_BW_COPIER_COST,@N_COLOR_COPIER_COST,@N_COLOR_COPIERBW_COST,@N_DUPLO_COST,@N_DUPLO_INK_COST,@EXCHANGE_RATE_BUY,@CPP_ON_BLACK_B_MACHINE,@COM_ID,@DEBTORS_CHECK_DAYS_LIMIT)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COMMITMENT", Trim(txtCommitment.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@GP_MERGING", Trim(txtGPMerging.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT", Trim(txtVatItems.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SVAT", Trim(txtSvat.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SMS", cbSMSstate.CheckState)
                If txtBckupServerIP.Text = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BACKUP_SERVER", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BACKUP_SERVER", Trim(txtBckupServerIP.Text))
                End If
                dbConnections.sqlCommand.Parameters.AddWithValue("@SW_AUTO_UPDATE", cbAutomaticSwUpdate.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_SERVICE", Trim(txtVATService.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@NBT1", Trim(txtNBT1.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@NBT2", Trim(txtNBT2.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_SERVICE_ACTIVE", cbVATserviceActive.CheckState)

                dbConnections.sqlCommand.Parameters.AddWithValue("@INTEREST_ON_CAPITAL", Trim(txtIOC.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CCP_ON_CONS_SPEAES_AND_LABOR", Trim(txtCCP_ON_CSL.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@CPP_ON_BLACK", Trim(txtCppOnBlack.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CPP_ON_COLOR", Trim(txtCPPOnColor.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SALES_GP_MERGING", Trim(txtSalesGPMargin.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@N_BW_COPIER_COST", Trim(txtBlackCopierCost.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@N_COLOR_COPIER_COST", Trim(txtColorCopierCost.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@N_COLOR_COPIERBW_COST", Trim(txtColorCopierBwCost.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@N_DUPLO_COST", Trim(txtDuploMachineCost.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@N_DUPLO_INK_COST", Trim(txtDuploMachineInkCost.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CPP_ON_BLACK_B_MACHINE", Trim(txtCPPonBlackBlackMachine.Text))

                If Trim(txtDebtorsCheckDaysLimit.Text) = "" Then
                    'DEBTORS_CHECK_DAYS_LIMIT
                    dbConnections.sqlCommand.Parameters.AddWithValue("@DEBTORS_CHECK_DAYS_LIMIT", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@DEBTORS_CHECK_DAYS_LIMIT", CInt(Trim(txtDebtorsCheckDaysLimit.Text)))
                End If

                If Trim(txtExchange_Rate.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@EXCHANGE_RATE_BUY", 0.0)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@EXCHANGE_RATE_BUY", Trim(txtExchange_Rate.Text))
                End If

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                If save = True Then
                    MessageBox.Show("Please Restart K-Bridge system to effect this changes.", "Restart request.", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                End If
            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtCommitment.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else
                        MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                End Select
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
                UserAuthorize = False
            End Try
        End If
        Return save
    End Function


    Private Sub FormEdit()

        Dim conf = MessageBox.Show("Do you wish to edit the record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            ISEnable(True)
            txtCommitment.Focus()
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
    Private Sub frmGenaralSettings_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmGenaralSettings_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmGenaralSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmGenaralSettings_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmGenaralSettings_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmGenaralSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
        GetGenaralSettings()
        If cbSMSstate.Checked = True Then
            cbSMSstate.Text = "ON"
            cbSMSstate.ForeColor = Color.Green
        Else
            cbSMSstate.Text = "OFF"
            cbSMSstate.ForeColor = Color.Red
        End If
    End Sub

    Private Sub frmGenaralSettings_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'"
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
        If generalValObj.isPresent(txtCommitment) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtGPMerging) = False Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        ISEnable(False)
        isEditClicked = False
        '//Set enability of global buttons
        globalFunctions.globalButtonActivation(False, False, True, False, False, False)
        Me.saveBtnStatus()
    End Sub
#End Region


    Private Sub GetGenaralSettings()
        txtCommitment.Text = globalVariables.Commitment
        txtGPMerging.Text = globalVariables.GPmerging
        txtVatItems.Text = globalVariables.VAT
        txtVATService.Text = globalVariables.VATService
        txtNBT1.Text = globalVariables.NBT1
        txtNBT2.Text = globalVariables.NBT2
        txtSvat.Text = globalVariables.SVAT
        cbSMSstate.Checked = globalVariables.SMSstate
        txtBckupServerIP.Text = globalVariables.BackupServerIPAddress
        cbAutomaticSwUpdate.Checked = globalVariables.AutomaticSwUpdateBool
        cbVATserviceActive.Checked = globalVariables.VAT_Service_Active
        txtIOC.Text = globalVariables.Interest_on_Capitale
        txtCCP_ON_CSL.Text = globalVariables.CPP_on_Cons_Spears_and_Labor
        txtCppOnBlack.Text = globalVariables.CPP_ON_BLACK
        txtCPPOnColor.Text = globalVariables.CPP_ON_COLOR
        txtSalesGPMargin.Text = globalVariables.SaleGPMargineVal
        txtBlackCopierCost.Text = globalVariables.N_Black_Copier_Cost
        txtColorCopierCost.Text = globalVariables.N_Color_Copier_Cost
        txtColorCopierBwCost.Text = globalVariables.N_Color_Copier_Black
        txtDuploMachineCost.Text = globalVariables.N_Duplo_Copier_Cost
        txtDuploMachineInkCost.Text = globalVariables.N_Duplo_ink_cost
        txtExchange_Rate.Text = globalVariables.ExchangeRate_Buy
        txtCPPonBlackBlackMachine.Text = globalVariables.CPP_ON_BLACK_BLACK_MACHINE
        txtDebtorsCheckDaysLimit.Text = globalVariables.DebtorsCheckDayLimit
    End Sub
    Private Sub ISEnable(ByRef enablestate As Boolean)
        txtCommitment.Enabled = enablestate
        txtGPMerging.Enabled = enablestate
        txtSvat.Enabled = enablestate
        txtVatItems.Enabled = enablestate
        txtVATService.Enabled = enablestate
        txtNBT1.Enabled = enablestate
        txtNBT2.Enabled = enablestate
        cbAutomaticSwUpdate.Enabled = enablestate
        cbVATserviceActive.Enabled = enablestate
        cbSMSstate.Enabled = enablestate
        txtBckupServerIP.Enabled = enablestate
        txtIOC.Enabled = enablestate
        txtCCP_ON_CSL.Enabled = enablestate
        txtCppOnBlack.Enabled = enablestate
        txtCPPOnColor.Enabled = enablestate
        txtColorCopierCost.Enabled = enablestate
        txtBlackCopierCost.Enabled = enablestate
        txtColorCopierBwCost.Enabled = enablestate
        txtCPPonBlackBlackMachine.Enabled = enablestate
    End Sub



    Private Sub cbSMSstate_Click(sender As Object, e As EventArgs) Handles cbSMSstate.Click
        If cbSMSstate.Checked = True Then
            cbSMSstate.Text = "ON"
            cbSMSstate.ForeColor = Color.Green
        Else
            cbSMSstate.Text = "OFF"
            cbSMSstate.ForeColor = Color.Red
        End If
    End Sub

    Private Sub cbAutomaticSwUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles cbAutomaticSwUpdate.CheckedChanged
        If cbAutomaticSwUpdate.Checked = True Then
            cbAutomaticSwUpdate.Text = "ON"
        Else
            cbAutomaticSwUpdate.Text = "OFF"
        End If
    End Sub


    Private Sub cbVATserviceActive_CheckedChanged(sender As Object, e As EventArgs) Handles cbVATserviceActive.CheckedChanged
        If cbVATserviceActive.Checked = True Then
            cbVATserviceActive.Text = "Active"
        Else
            cbVATserviceActive.Text = "Inactive"
        End If
    End Sub
End Class