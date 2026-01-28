Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmCustomerMaster

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
                    strSQL = "UPDATE    MTBL_CUSTOMER_MASTER SET  CUS_NAME =@CUS_NAME, CUS_ADD1 =@CUS_ADD1, CUS_ADD2 =@CUS_ADD2, CUS_CONTACT_NO =@CUS_CONTACT_NO, CUS_FAX =@CUS_FAX, CUS_EMAIL =@CUS_EMAIL, VAT_TYPE_ID =@VAT_TYPE_ID, CUS_DISCOUNT =@CUS_DISCOUNT, CUS_NBT1 =@CUS_NBT1, CUS_NBT2 =@CUS_NBT2, CUS_BILLING_PERIOD =@CUS_BILLING_PERIOD, CUS_VAT_NO =@CUS_VAT_NO, CUS_SVAT_NO =@CUS_SVAT_NO ,PO_NO=@PO_NO WHERE     (COM_ID = @COM_ID) AND (CUS_ID = @CUS_ID)"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO MTBL_CUSTOMER_MASTER  (COM_ID, CUS_ID, CUS_NAME, CUS_ADD1, CUS_ADD2, CUS_CONTACT_NO, CUS_FAX, CUS_EMAIL, VAT_TYPE_ID, CUS_DISCOUNT, CUS_NBT1, CUS_NBT2, CUS_BILLING_PERIOD, CUS_VAT_NO,  CUS_SVAT_NO,PO_NO) VALUES     (@COM_ID, @CUS_ID, @CUS_NAME, @CUS_ADD1, @CUS_ADD2, @CUS_CONTACT_NO, @CUS_FAX, @CUS_EMAIL, @VAT_TYPE_ID, @CUS_DISCOUNT, @CUS_NBT1, @CUS_NBT2, @CUS_BILLING_PERIOD, @CUS_VAT_NO,  @CUS_SVAT_NO,@PO_NO)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCusID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NAME", Trim(txtCusName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ADD1", Trim(txtAdd1.Text))
                If Trim(txtAdd2.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ADD2", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ADD2", Trim(txtAdd2.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CONTACT_NO", Trim(txtContactNo.Text))
                If Trim(txtFax.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_FAX", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_FAX", Trim(txtFax.Text))
                End If

                If Trim(txtEmail.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_EMAIL", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_EMAIL", Trim(txtEmail.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_TYPE_ID", Trim(txtVatType.Text))
                If Trim(txtDiscount.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_DISCOUNT", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_DISCOUNT", Trim(txtDiscount.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NBT1", cbNBT1.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NBT2", cbNBT2.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_BILLING_PERIOD", Trim(cmbBillingPeriod.Text))
                If Trim(txtVatNo.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_VAT_NO", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_VAT_NO", Trim(txtVatNo.Text))
                End If
                If Trim(txtSvatNo.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_SVAT_NO", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_SVAT_NO", Trim(txtSvatNo.Text))
                End If
                If Trim(txtPONo.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@PO_NO", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@PO_NO", Trim(txtPONo.Text))
                End If

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                If save = True Then
                    If isEditClicked Then
                        AuditDelete(Me.Text, userSession, userName, txtCusID.Text, txtCusName.Text + "(Edit)")
                    Else
                        AuditDelete(Me.Text, userSession, userName, txtCusID.Text, txtCusName.Text + "(Saved)")
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
            Dim IDbackup As String = Trim(txtCusID.Text)
            Dim IDdesc As String = Trim(txtCusName.Text)
            connectionStaet()
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtCusName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID) AND (CUS_ID = @CUS_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCusID.Text))
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

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtCusName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then

            enableSate(True)
            txtCusName.Focus()
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
    Private Sub frmCustomerMaster_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmCustomerMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmCustomerMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmCustomerMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmCustomerMaster_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmCustomerMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmCustomerMaster_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
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


    Private Sub enableSate(ByRef enableSatate As Boolean)
        txtCusName.Enabled = enableSatate
        txtAdd1.Enabled = enableSatate
        txtAdd2.Enabled = enableSatate
        txtContactNo.Enabled = enableSatate
        txtFax.Enabled = enableSatate
        txtEmail.Enabled = enableSatate
        txtVatType.Enabled = enableSatate
        txtVatNo.Enabled = enableSatate
        txtSvatNo.Enabled = enableSatate
        cbNBT1.Enabled = enableSatate
        cbNBT2.Enabled = enableSatate
        cmbBillingPeriod.Enabled = enableSatate
        txtDiscount.Enabled = enableSatate
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtCusID) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtCusName) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtAdd1) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtVatType) = False Then
            Exit Function
        End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtCusID.Text = ""
        txtCusName.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtContactNo.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtVatType.Text = ""
        lblVatTypeVal.Text = "N/A"
        txtVatNo.Text = ""
        txtSvatNo.Text = ""
        cbNBT1.Checked = False
        cbNBT2.Checked = False
        cmbBillingPeriod.SelectedIndex = 0
        txtDiscount.Text = ""
        If globalVariables.selectedCompanyID = "001" Then
            txtCusID.Text = NextCusID()
        End If
        txtPONo.Text = ""

        txtCusID.Enabled = True
        enableSate(True)


        txtCusID.Focus()
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


    Private Sub txtVatType_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVatType.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtCusID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCusID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub




    Private Sub txtVatType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVatType.KeyPress
        generalValObj.isDigit(e)
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub


    Private Sub txtVatTypeID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCusID.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT    CUS_NAME, CUS_ADD1, CUS_ADD2, CUS_CONTACT_NO, CUS_FAX, CUS_EMAIL, VAT_TYPE_ID, CUS_DISCOUNT, CUS_NBT1, CUS_NBT2, CUS_BILLING_PERIOD, CUS_VAT_NO,  CUS_SVAT_NO,PO_NO FROM         MTBL_CUSTOMER_MASTER WHERE COM_ID = @COM_ID and CUS_ID = @CUS_ID"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCusID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True

                If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
                    txtCusName.Text = ""
                Else
                    txtCusName.Text = dbConnections.dReader.Item("CUS_NAME")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_ADD1")) Then
                    txtAdd1.Text = ""
                Else
                    txtAdd1.Text = dbConnections.dReader.Item("CUS_ADD1")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_ADD2")) Then
                    txtAdd2.Text = ""
                Else
                    txtAdd2.Text = dbConnections.dReader.Item("CUS_ADD2")
                End If
                If IsDBNull(dbConnections.dReader.Item("CUS_CONTACT_NO")) Then
                    txtContactNo.Text = ""
                Else
                    txtContactNo.Text = dbConnections.dReader.Item("CUS_CONTACT_NO")
                End If


                If IsDBNull(dbConnections.dReader.Item("CUS_FAX")) Then
                    txtFax.Text = ""
                Else
                    txtFax.Text = dbConnections.dReader.Item("CUS_FAX")
                End If
                If IsDBNull(dbConnections.dReader.Item("CUS_EMAIL")) Then
                    txtEmail.Text = ""
                Else
                    txtEmail.Text = dbConnections.dReader.Item("CUS_EMAIL")
                End If
                If IsDBNull(dbConnections.dReader.Item("VAT_TYPE_ID")) Then
                    txtVatType.Text = ""
                Else
                    txtVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
                End If


                If IsDBNull(dbConnections.dReader.Item("CUS_DISCOUNT")) Then
                    txtDiscount.Text = ""
                Else
                    txtDiscount.Text = dbConnections.dReader.Item("CUS_DISCOUNT")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_NBT1")) Then
                    cbNBT1.Checked = False
                Else
                    cbNBT1.Checked = dbConnections.dReader.Item("CUS_NBT1")
                End If
                If IsDBNull(dbConnections.dReader.Item("CUS_NBT1")) Then
                    cbNBT2.Checked = False
                Else
                    cbNBT2.Checked = dbConnections.dReader.Item("CUS_NBT1")
                End If
                '   , , , , , , ,  CUS_SVAT_NO
                If IsDBNull(dbConnections.dReader.Item("CUS_BILLING_PERIOD")) Then
                    cmbBillingPeriod.SelectedIndex = 0
                Else
                    cmbBillingPeriod.Text = dbConnections.dReader.Item("CUS_BILLING_PERIOD")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_VAT_NO")) Then
                    txtVatNo.Text = ""
                Else
                    txtVatNo.Text = dbConnections.dReader.Item("CUS_VAT_NO")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_SVAT_NO")) Then
                    txtSvatNo.Text = ""
                Else
                    txtSvatNo.Text = dbConnections.dReader.Item("CUS_SVAT_NO")
                End If
               
                If IsDBNull(dbConnections.dReader.Item("PO_NO")) Then
                    txtPONo.Text = ""
                Else
                    txtPONo.Text = dbConnections.dReader.Item("PO_NO")
                End If
                txtCusID.Enabled = False


                enableSate(False)



            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT       VAT_DESC  FROM MTBL_VAT_MASTER WHERE COM_ID = @COM_ID and VAT_TYPE_ID = @VAT_TYPE_ID"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_TYPE_ID", Trim(txtVatType.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                lblVatTypeVal.Text = dbConnections.dReader.Item("VAT_DESC")

            End While
            dbConnections.dReader.Close()



            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtCusID.Enabled = False
                enableSate(True)
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

    Private Function NextCusID() As Integer
        NextCusID = 0
        Try
            strSQL = "SELECT top 1  convert(int,CUS_ID ) as CUS_ID FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID)  ORDER BY CUS_ID DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)

            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("CUS_ID")) Then
                    NextCusID = 1
                Else
                    NextCusID = dbConnections.dReader.Item("CUS_ID") + 1
                End If


            End While
            dbConnections.dReader.Close()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Return NextCusID
    End Function

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



    Private Sub txtCusID_TextChanged(sender As Object, e As EventArgs) Handles txtCusID.TextChanged

    End Sub

    Private Sub cbNBT1_CheckedChanged(sender As Object, e As EventArgs) Handles cbNBT1.CheckedChanged
        If cbNBT1.Checked = True Then
            cbNBT1.Text = "YES"
        Else
            cbNBT1.Text = "NO"
        End If
    End Sub

    Private Sub cbNBT2_CheckedChanged(sender As Object, e As EventArgs) Handles cbNBT2.CheckedChanged
        If cbNBT2.Checked = True Then
            cbNBT2.Text = "YES"
        Else
            cbNBT2.Text = "NO"
        End If
    End Sub



    Private Sub txtVatType_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtVatType.Validating

    End Sub
End Class