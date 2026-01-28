Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmNewMachienInsallation

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
    Dim SelectedAgreement As String

    Dim IsNewAgreement As Boolean = False '// CAPTURE new agreement or not for the customer

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

            dgBw.EndEdit(True)
            dgColor.EndEdit(True)
            Try

                If cbNewAgreement.Checked = True Then
                    IsNewAgreement = True
                    SelectedAgreement = GenarateAGNo()
                Else

                    IsNewAgreement = False
                    SelectedAgreement = Trim(txtSelectedAG.Text)
                End If

                connectionStaet()

                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction

                If IsNewAgreement = False Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE  TBL_CUS_AGREEMENT SET  CUS_CODE =@CUS_CODE, CUS_TYPE =@CUS_TYPE, BILLING_METHOD =@BILLING_METHOD, SLAB_METHOD =@SLAB_METHOD, BILLING_PERIOD =@BILLING_PERIOD, MD_BY ='" & userSession & "', MD_DATE =GETDATE() , INV_STATUS=@INV_STATUS , MACHINE_TYPE=@MACHINE_TYPE , AG_PERIOD_START=@AG_PERIOD_START , AG_PERIOD_END=@AG_PERIOD_END ,AG_RENTAL_PRICE=@AG_RENTAL_PRICE,REP_CODE=@REP_CODE  WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID)"

                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO TBL_CUS_AGREEMENT (COM_ID, AG_ID, CUS_CODE, CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, CR_BY, CR_DATE,INV_STATUS,MACHINE_TYPE,AG_PERIOD_START,AG_PERIOD_END,AG_RENTAL_PRICE,REP_CODE) VALUES     (@COM_ID, @AG_ID, @CUS_CODE, @CUS_TYPE, @BILLING_METHOD, @SLAB_METHOD, @BILLING_PERIOD, '" & userSession & "', GETDATE(),@INV_STATUS,@MACHINE_TYPE,@AG_PERIOD_START,@AG_PERIOD_END,@AG_RENTAL_PRICE,@REP_CODE)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(SelectedAgreement))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
                If cbGroup.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_TYPE", "GROUP")
                End If
                If cbIndividual.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_TYPE", "INDIVIDUAL")
                End If
                If rbtnCommitment.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "COMMITMENT")
                End If
                If rbtnActual.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "ACTUAL")
                End If

                If rbtnRental.Checked = True Then

                    dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_METHOD", "RENTAL")
                End If

                If Trim(txtRental.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_RENTAL_PRICE", CDbl(Trim(txtRental.Text)))
                End If
                If rbtnInvStatusAll.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "ALL")
                End If
                If rbtnInvStatusIndividual.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS", "INDIVIDUAL")
                End If
                If rbtnBw.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "BW")
                End If
                If rbtnColor.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "COLOR")
                End If


                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_PERIOD_START", dtpAPStart.Value)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_PERIOD_END", dtpAPEnd.Value)

                dbConnections.sqlCommand.Parameters.AddWithValue("@SLAB_METHOD", Trim(cmbSlabMethod.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@BILLING_PERIOD", Trim(cmbBilPeriod.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRepCode.Text))
             

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                'BW_RANGE_1
                'BW_RANGE_2
                'BW_RATE

                'COLOR_RANGE_1
                'COLOR_RANGE_2
                'COLOR_RATE
                If IsNewAgreement = True Then '//  commitments will save only for new agreement creation
                    If dgBw.Rows.Count > 0 Then


                        strSQL = "DELETE FROM TBL_AG_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        dbConnections.sqlCommand.ExecuteNonQuery()

                        'Data grid Data feeding code

                        For Each row As DataGridViewRow In dgBw.Rows

                            If dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value <> Nothing Then
                                dbConnections.sqlCommand.Parameters.Clear()

                                strSQL = "INSERT   INTO            TBL_AG_BW_COMMITMENT(COM_ID, CUS_ID, AG_CODE, BW_RANGE_1, BW_RANGE_2, BW_RATE) VALUES     (@COM_ID, @CUS_ID, @AG_CODE, @BW_RANGE_1, @BW_RANGE_2, @BW_RATE)"
                                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(SelectedAgreement))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_1", dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RANGE_2", dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@BW_RATE", dgBw.Rows(row.Index).Cells("BW_RATE").Value)

                                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                            End If


                        Next
                    End If

                    If dgColor.Rows.Count > 0 Then


                        strSQL = "DELETE FROM TBL_AG_COLOR_COMMITMENT  WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                        dbConnections.sqlCommand.ExecuteNonQuery()

                        'Data grid Data feeding code

                        For Each row As DataGridViewRow In dgColor.Rows
                            If dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value <> Nothing Then


                                dbConnections.sqlCommand.Parameters.Clear()

                                strSQL = "INSERT INTO TBL_AG_COLOR_COMMITMENT    (COM_ID, CUS_ID, AG_CODE, COLOR_RANGE_1, COLOR_RANGE_2, COLOR_RATE) VALUES     (@COM_ID, @CUS_ID, @AG_CODE, @COLOR_RANGE_1, @COLOR_RANGE_2, @COLOR_RATE)"
                                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(SelectedAgreement))
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_1", dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RANGE_2", dgColor.Rows(row.Index).Cells("COLOR_RANGE_2").Value)
                                dbConnections.sqlCommand.Parameters.AddWithValue("@COLOR_RATE", dgColor.Rows(row.Index).Cells("COLOR_RATE").Value)

                                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
                            End If
                        Next
                    End If
                End If

                '// Adding Machine to the transaction
                strSQL = "SELECT CASE WHEN EXISTS (SELECT     SERIAL FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(SelectedAgreement) & "') AND (SERIAL = '" & Trim(txtSerialNo.Text) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.CommandText = strSQL
                If dbConnections.sqlCommand.ExecuteScalar Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET MACHINE_PN =@MACHINE_PN, P_NO =@P_NO, IS_SPECIAL_CASE =@IS_SPECIAL_CASE, SPECIAL_CASE_DESC =@SPECIAL_CASE_DESC, M_LOC1 =@M_LOC1, M_LOC2 =@M_LOC2, M_LOC3 =@M_LOC3, M_DEPT =@M_DEPT, CONTACT_PERSON =@CONTACT_PERSON, CONTACT_NO =@CONTACT_NO,   INSTALLATION_DATE =@INSTALLATION_DATE, START_MR =@START_MR,  START_MR_COLOR=@START_MR_COLOR, BOOK_VALUE =@BOOK_VALUE, TECH_CODE =@TECH_CODE, REP_CODE =@REP_CODE , CUS_ID=@CUS_ID , CUS_NAME=@CUS_NAME ,ACCESSORIES=@ACCESSORIES WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID) AND (SERIAL = @SERIAL)"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO TBL_MACHINE_TRANSACTIONS  (COM_ID, AG_ID, SERIAL,  MACHINE_PN, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR,START_MR_COLOR, BOOK_VALUE, TECH_CODE, REP_CODE,CUS_ID,SMR_ADUJESTED_STATUS,CUS_NAME,ACCESSORIES) VALUES     (@COM_ID, @AG_ID, @SERIAL, @MACHINE_PN, @P_NO, @IS_SPECIAL_CASE, @SPECIAL_CASE_DESC, @M_LOC1, @M_LOC2, @M_LOC3, @M_DEPT, @CONTACT_PERSON, @CONTACT_NO, @INSTALLATION_DATE, @START_MR,@START_MR_COLOR, @BOOK_VALUE, @TECH_CODE, @REP_CODE,@CUS_ID,@SMR_ADUJESTED_STATUS,@CUS_NAME,@ACCESSORIES)"
                End If

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(SelectedAgreement))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_PN", Trim(txtMachinePN.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@P_NO", Trim(txtPno.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_SPECIAL_CASE", cbSpecialCase.CheckState)
                If cbSpecialCase.Checked = False Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@SPECIAL_CASE_DESC", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@SPECIAL_CASE_DESC", Trim(txtSpecialCase.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC1", Trim(txtMLocation1.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC2", Trim(txtMLocation2.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@M_LOC3", Trim(txtMLocation3.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@M_DEPT", Trim(txtDept.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CONTACT_PERSON", Trim(txtContact.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CONTACT_NO", Trim(txtTel.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INSTALLATION_DATE", dtpInstallationDate.Value)
                dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", Trim(txtStartMR.Text))

                If Trim(txtStartMRC.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", Trim(txtStartMRC.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@BOOK_VALUE", Trim(txtBookValue.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRepCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NAME", Trim(txtCustomerName.Text))


                If errorEvent = "Save" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@SMR_ADUJESTED_STATUS", "PENDING CAPTURE")
                    '// Capture status
                    '// 1 PENDING CAPTURE
                    '// 2 UPDATED
                End If
                'ACCESSORIES
                If Trim(txtAccessories.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@ACCESSORIES", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@ACCESSORIES", Trim(txtAccessories.Text))
                End If



                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                '// UPDATING STOCK
                strSQL = "SELECT CASE WHEN EXISTS (SELECT      SERIAL FROM         TBL_MACHINE_STOCK WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (SERIAL = '" & Trim(txtSerialNo.Text) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.CommandText = strSQL
                If dbConnections.sqlCommand.ExecuteScalar Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE  TBL_MACHINE_STOCK SET  MACHINE_PN =@MACHINE_PN, BOOK_VALUE =@BOOK_VALUE, SN_STATUS =@SN_STATUS, CURRENT_CUS_ID =@CURRENT_CUS_ID, CURRENT_AG_ID =@CURRENT_AG_ID, CURRENT_MR =@CURRENT_MR, MACHINE_TYPE =@IS_ACTIVE, IS_ACTIVE =@IS_ACTIVE , CURRENT_MR_COLOR=@CURRENT_MR_COLOR WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"

                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO TBL_MACHINE_STOCK   (COM_ID, SERIAL, MACHINE_PN, BOOK_VALUE,  SN_STATUS, CURRENT_CUS_ID, CURRENT_AG_ID, CURRENT_MR, MACHINE_TYPE, IS_ACTIVE,CURRENT_MR_COLOR) VALUES     (@COM_ID, @SERIAL, @MACHINE_PN, @BOOK_VALUE,  @SN_STATUS, @CURRENT_CUS_ID, @CURRENT_AG_ID, @CURRENT_MR, @MACHINE_TYPE, @IS_ACTIVE,@CURRENT_MR_COLOR)"

                End If


                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_PN", Trim(txtMachinePN.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@BOOK_VALUE", CDbl(txtBookValue.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SN_STATUS", "UN-AVAILABLE")
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_CUS_ID", Trim(txtCustomerID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR", CInt(txtStartMR.Text))

                If Trim(txtStartMRC.Text) = "" Or Trim(txtStartMRC.Text) = "0" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR_COLOR", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR_COLOR", CInt(txtStartMRC.Text))
                End If

                If rbtnBw.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "BW")
                End If
                If rbtnColor.Checked = True Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "COLOR")
                End If
                dbConnections.sqlCommand.Parameters.AddWithValue("@IS_ACTIVE", True)

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False



                '// ADDING RECORD TO MACHINE AUDIT LOG
                errorEvent = "Save"
                strSQL = "INSERT INTO TBL_MACHINE_AUDIT  (COM_ID, SERIAL, MACHINE_PN, CUS_ID, AG_ID, AU_DATE, AU_STATUS) VALUES     ( @COM_ID, @SERIAL, @MACHINE_PN, @CUS_ID, @AG_ID, GETDATE(), @AU_STATUS)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_PN", Trim(txtMachinePN.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AU_STATUS", "OUT-STOCK")
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                dbConnections.sqlTransaction.Commit()


            Catch ex As Exception
                dbConnections.sqlTransaction.Rollback()
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

        Catch ex As Exception
            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X2) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            connectionClose()

        End Try
        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtSerialNo.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            'txtVatTypeName.Enabled = True
            'txtVatPre.Enabled = True
            'txtVatTypeName.Focus()
            isEditClicked = True
            globalFunctions.globalButtonActivation(True, True, False, False, False, True)
            Me.saveBtnStatus()
        End If
    End Sub



    Private Function AgreementMachineTransfer() As Boolean
        AgreementMachineTransfer = False
        If Trim(txtNewAgreementID.Text) = "" Then
            Exit Function
        End If

        If Trim(txtSerialNo.Text) = "" Then
            Exit Function
        End If

        strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET   AG_ID =@AG_ID , CUS_ID=@CUS_ID WHERE     (COM_ID =@COM_ID) AND (SERIAL =@SERIAL)"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtNewAgreementID.Text))
        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtNewCusID.Text))
        Dim something As Integer = 0
        something = dbConnections.sqlCommand.ExecuteNonQuery()
        If something > 0 Then
            Console.WriteLine(something)
        End If
        'If dbConnections.sqlCommand.ExecuteNonQuery() Then AgreementMachineTransfer = True Else AgreementMachineTransfer = False

        If AgreementMachineTransfer Then
            MessageBox.Show("Transfer Sccessful.", "Transfered.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        'Try
        '    strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET   AG_ID =@AG_ID , CUS_ID=@CUS_ID WHERE     (COM_ID =@COM_ID) AND (SERIAL =@SERIAL)"
        '    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtNewAgreementID.Text))
        '    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtNewCusID.Text))
        '    Dim something As Integer = 0
        '    something = dbConnections.sqlCommand.ExecuteNonQuery()
        '    If something > 0 Then
        '        Console.WriteLine(something)
        '    End If
        '    'If dbConnections.sqlCommand.ExecuteNonQuery() Then AgreementMachineTransfer = True Else AgreementMachineTransfer = False

        '    If AgreementMachineTransfer Then
        '        MessageBox.Show("Transfer Sccessful.", "Transfered.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If


        'Catch ex As Exception
        '    'MsgBox(ex.Message)
        'End Try

        Return AgreementMachineTransfer
    End Function

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmNewMachienInsallation_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmNewMachienInsallation_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmNewMachienInsallation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNewMachienInsallation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmNewMachienInsallation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmNewMachienInsallation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()

    End Sub

    Private Sub frmNewMachienInsallation_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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




    Private Function GenarateAGNo() As String

        GenarateAGNo = ""

        errorEvent = "Reading information"
        connectionStaet()


        Try
            strSQL = "SELECT    MAX( AG_ID) as 'MAX' FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID LIKE '" & Trim(globalVariables.selectedCompanyID) & "/" & Trim(txtCustomerID.Text) & "%') "
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GenarateAGNo = globalVariables.selectedCompanyID & "/" & Trim(txtCustomerID.Text) & "/" & 1
            Else

                If dbConnections.sqlCommand.ExecuteScalar = "" Then
                    GenarateAGNo = globalVariables.selectedCompanyID & "/" & Trim(txtCustomerID.Text) & "/" & 1
                Else
                    Dim AGCodeSplit() As String
                    Dim NoRecordFound As Boolean = False
                    Dim AGID As Integer = 0
                    AGCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                    AGID = AGCodeSplit(2)
                    Do Until NoRecordFound = True
                        strSQL = "SELECT CASE WHEN EXISTS (SELECT     AG_ID FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & AGCodeSplit(0) & "/" & AGCodeSplit(1) & "/" & AGID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                        dbConnections.sqlCommand.CommandText = strSQL
                        If dbConnections.sqlCommand.ExecuteScalar = False Then
                            NoRecordFound = True
                        Else
                            AGID = AGID + 1
                        End If
                    Loop
                    If NoRecordFound = True Then
                        GenarateAGNo = AGCodeSplit(0) & "/" & AGCodeSplit(1) & "/" & AGID
                    Else
                        Exit Function
                    End If
                End If

            End If


        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally

            connectionClose()
        End Try
    End Function



    Private Sub LoadCommitments(ByRef AG_ID As String, ByRef CUS_ID As String)
        dgBw.Rows.Clear()
        Try
            strSQL = "SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                populatreDatagrid_BW_Commitment(dbConnections.dReader.Item("BW_RANGE_1"), dbConnections.dReader.Item("BW_RANGE_2"), dbConnections.dReader.Item("BW_RATE"))
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        dgColor.Rows.Clear()
        Try
            strSQL = "SELECT     COLOR_RANGE_1, COLOR_RANGE_2, COLOR_RATE FROM         TBL_AG_COLOR_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                populatreDatagrid_Color_Commitment(dbConnections.dReader.Item("COLOR_RANGE_1"), dbConnections.dReader.Item("COLOR_RANGE_2"), dbConnections.dReader.Item("COLOR_RATE"))
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try


    End Sub
    Private Sub populatreDatagrid_BW_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal)
        dgBw.ColumnCount = 3
        dgBw.Rows.Add(Range1, Range2, Rate)
    End Sub
    Private Sub populatreDatagrid_Color_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal)
        dgColor.ColumnCount = 3
        dgColor.Rows.Add(Range1, Range2, Rate)
    End Sub

    Private Sub populatreDatagrAgreements(ByRef AG_ID_Val As String, ByRef AG_NAME_Val As String)
        dgAgreement.ColumnCount = 2
        dgAgreement.Rows.Add(AG_ID_Val, AG_NAME_Val)
    End Sub


    Private Sub LoadSelectedAgreement()

        If Trim(txtSelectedAG.Text) = "" Then
            Exit Sub
        End If

        Try
            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE,AG_RENTAL_PRICE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read


                If IsDBNull(dbConnections.dReader.Item("CUS_TYPE")) Then
                    cbGroup.Checked = False
                    cbIndividual.Checked = False
                Else
                    If dbConnections.dReader.Item("CUS_TYPE") = "GROUP" Then
                        cbGroup.Checked = True
                    ElseIf dbConnections.dReader.Item("CUS_TYPE") = "INDIVIDUAL" Then
                        cbIndividual.Checked = True
                    Else
                        cbGroup.Checked = False
                        cbIndividual.Checked = False
                    End If

                End If


                If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
                    rbtnActual.Checked = False
                    rbtnCommitment.Checked = False
                Else
                    If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
                        rbtnCommitment.Checked = True
                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
                        rbtnActual.Checked = True
                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "RENTAL" Then
                        rbtnRental.Checked = True
                    Else
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                    End If
                    '

                End If

                If IsDBNull(dbConnections.dReader.Item("AG_RENTAL_PRICE")) Then
                    txtRental.Text = ""
                Else
                    txtRental.Text = dbConnections.dReader.Item("AG_RENTAL_PRICE")
                End If

                If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
                    cmbSlabMethod.SelectedIndex = 0
                Else
                    cmbSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
                End If


                If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
                    cmbBilPeriod.SelectedIndex = 0
                Else
                    cmbBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
                End If
                If IsDBNull(dbConnections.dReader.Item("AG_PERIOD_START")) Then
                    dtpAPStart.Value = Today.Date
                Else
                    dtpAPStart.Value = dbConnections.dReader.Item("AG_PERIOD_START")
                End If

                If IsDBNull(dbConnections.dReader.Item("AG_PERIOD_END")) Then
                    dtpAPEnd.Value = Today.Date
                Else
                    dtpAPEnd.Value = dbConnections.dReader.Item("AG_PERIOD_END")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_STATUS")) Then
                    rbtnInvStatusAll.Checked = False
                    rbtnInvStatusIndividual.Checked = False
                Else
                    If dbConnections.dReader.Item("INV_STATUS") = "ALL" Then
                        rbtnInvStatusAll.Checked = True
                    ElseIf dbConnections.dReader.Item("INV_STATUS") = "INDIVIDUAL" Then
                        rbtnInvStatusIndividual.Checked = True
                    Else
                        rbtnInvStatusAll.Checked = False
                        rbtnInvStatusIndividual.Checked = False
                    End If

                End If

                If IsDBNull(dbConnections.dReader.Item("MACHINE_TYPE")) Then
                    rbtnBw.Checked = False
                    rbtnColor.Checked = False
                Else
                    If dbConnections.dReader.Item("MACHINE_TYPE") = "BW" Then
                        rbtnBw.Checked = True
                    ElseIf dbConnections.dReader.Item("MACHINE_TYPE") = "COLOR" Then
                        rbtnColor.Checked = True
                    Else
                        rbtnBw.Checked = False
                        rbtnColor.Checked = False
                    End If

                End If


            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))

    End Sub


    '// search by  = bySN,ByPN/ByAG
    Private Function Search(ByRef SearchBy As String) As Boolean
        Search = False

        If Trim(txtSearch.Text) = "" Then
            Exit Function
        End If
        Dim SelectedSN As String = ""
        Dim SelectedPNo As String = ""
        Dim SelectedAg As String = ""
        Dim SelCusCode As String = ""
        Try




            If SearchBy = "SN" Then
                strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (SERIAL = '" & Trim(txtSearch.Text) & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    Search = True
                    SelectedSN = dbConnections.dReader.Item("SERIAL")
                    SelectedPNo = dbConnections.dReader.Item("P_NO")
                    SelectedAg = dbConnections.dReader.Item("AG_ID")
                    SelCusCode = dbConnections.dReader.Item("CUS_ID")
                End While
                dbConnections.dReader.Close()
            Else
                strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (P_NO = '" & Trim(txtSearch.Text) & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    SelectedSN = dbConnections.dReader.Item("SERIAL")
                    SelectedPNo = dbConnections.dReader.Item("P_NO")
                    SelectedAg = dbConnections.dReader.Item("AG_ID")
                    SelCusCode = dbConnections.dReader.Item("CUS_ID")
                End While
                dbConnections.dReader.Close()
            End If



            If SelectedSN = "" Then
                Exit Function
            End If
            txtCustomerID.Text = SelCusCode



            txtSelectedAG.Text = SelectedAg

            Dim AgreementName As String = ""
            dgAgreement.Rows.Clear()
            strSQL = "SELECT     AG_ID, AG_NAME FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = @COM_ID) AND (CUS_CODE =@CUS_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", SelCusCode)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                dgAgreement.ColumnCount = 1
                'dgAgreement.Rows.Add(dbConnections.dReader.Item("AG_ID"))
                AgreementName = ""
                If IsDBNull(dbConnections.dReader.Item("AG_NAME")) Then
                    AgreementName = dbConnections.dReader.Item("AG_ID")
                Else
                    AgreementName = dbConnections.dReader.Item("AG_NAME")
                End If
                populatreDatagrAgreements(dbConnections.dReader.Item("AG_ID"), AgreementName)
            End While
            dbConnections.dReader.Close()



            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", SelCusCode)
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", SelectedAg)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read


                If IsDBNull(dbConnections.dReader.Item("CUS_TYPE")) Then
                    cbGroup.Checked = False
                    cbIndividual.Checked = False
                Else
                    If dbConnections.dReader.Item("CUS_TYPE") = "GROUP" Then
                        cbGroup.Checked = True
                    ElseIf dbConnections.dReader.Item("CUS_TYPE") = "INDIVIDUAL" Then
                        cbIndividual.Checked = True
                    Else
                        cbGroup.Checked = False
                        cbIndividual.Checked = False
                    End If

                End If


                If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
                    rbtnActual.Checked = False
                    rbtnCommitment.Checked = False
                Else
                    If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
                        rbtnCommitment.Checked = True
                    ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
                        rbtnActual.Checked = True
                    Else
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                    End If

                End If

                If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
                    cmbSlabMethod.SelectedIndex = 0
                Else
                    cmbSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
                End If


                If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
                    cmbBilPeriod.SelectedIndex = 0
                Else
                    cmbBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
                End If
                If IsDBNull(dbConnections.dReader.Item("AG_PERIOD_START")) Then
                    dtpAPStart.Value = Today.Date
                Else
                    dtpAPStart.Value = dbConnections.dReader.Item("AG_PERIOD_START")
                End If

                If IsDBNull(dbConnections.dReader.Item("AG_PERIOD_END")) Then
                    dtpAPEnd.Value = Today.Date
                Else
                    dtpAPEnd.Value = dbConnections.dReader.Item("AG_PERIOD_END")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_STATUS")) Then
                    rbtnInvStatusAll.Checked = False
                    rbtnInvStatusIndividual.Checked = False
                Else
                    If dbConnections.dReader.Item("INV_STATUS") = "ALL" Then
                        rbtnInvStatusAll.Checked = True
                    ElseIf dbConnections.dReader.Item("INV_STATUS") = "INDIVIDUAL" Then
                        rbtnInvStatusIndividual.Checked = True
                    Else
                        rbtnInvStatusAll.Checked = False
                        rbtnInvStatusIndividual.Checked = False
                    End If

                End If

                If IsDBNull(dbConnections.dReader.Item("MACHINE_TYPE")) Then
                    rbtnBw.Checked = False
                    rbtnColor.Checked = False
                Else
                    If dbConnections.dReader.Item("MACHINE_TYPE") = "BW" Then
                        rbtnBw.Checked = True
                    ElseIf dbConnections.dReader.Item("MACHINE_TYPE") = "COLOR" Then
                        rbtnColor.Checked = True
                    Else
                        rbtnBw.Checked = False
                        rbtnColor.Checked = False
                    End If

                End If

            End While
            dbConnections.dReader.Close()

            '// loading Commitment info
            LoadCommitments(SelectedAg, SelCusCode)

            '// loading machine Info

            strSQL = "SELECT      MACHINE_PN, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE,START_MR_COLOR ,ACCESSORIES FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID =@COM_ID) AND (AG_ID =@AG_ID) AND (SERIAL =@SERIAL)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", SelectedAg)
            dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", SelectedSN)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                txtMachinePN.Text = dbConnections.dReader.Item("MACHINE_PN")
                txtPno.Text = dbConnections.dReader.Item("P_NO")
                If IsDBNull(dbConnections.dReader.Item("IS_SPECIAL_CASE")) Then
                    cbSpecialCase.Checked = False
                Else
                    cbSpecialCase.Checked = dbConnections.dReader.Item("IS_SPECIAL_CASE")
                End If


                If IsDBNull(dbConnections.dReader.Item("SPECIAL_CASE_DESC")) Then
                    txtSpecialCase.Text = ""
                Else
                    txtSpecialCase.Text = dbConnections.dReader.Item("SPECIAL_CASE_DESC")
                End If

                If IsDBNull(dbConnections.dReader.Item("M_LOC1")) Then
                    txtMLocation1.Text = ""
                Else
                    txtMLocation1.Text = dbConnections.dReader.Item("M_LOC1")
                End If

                If IsDBNull(dbConnections.dReader.Item("M_LOC2")) Then
                    txtMLocation2.Text = ""
                Else
                    txtMLocation2.Text = dbConnections.dReader.Item("M_LOC2")
                End If


                If IsDBNull(dbConnections.dReader.Item("M_LOC3")) Then
                    txtMLocation3.Text = ""
                Else
                    txtMLocation3.Text = dbConnections.dReader.Item("M_LOC3")
                End If

                If IsDBNull(dbConnections.dReader.Item("M_DEPT")) Then
                    txtDept.Text = "N/A"
                Else
                    txtDept.Text = dbConnections.dReader.Item("M_DEPT")
                End If

                If IsDBNull(dbConnections.dReader.Item("CONTACT_PERSON")) Then
                    txtContact.Text = ""
                Else
                    txtContact.Text = dbConnections.dReader.Item("CONTACT_PERSON")
                End If
                If IsDBNull(dbConnections.dReader.Item("CONTACT_NO")) Then
                    txtTel.Text = ""
                Else
                    txtTel.Text = dbConnections.dReader.Item("CONTACT_NO")
                End If

                If IsDBNull(dbConnections.dReader.Item("INSTALLATION_DATE")) Then
                    dtpInstallationDate.Value = Today.Date
                Else
                    dtpInstallationDate.Value = dbConnections.dReader.Item("INSTALLATION_DATE")
                End If

                txtStartMR.Text = dbConnections.dReader.Item("START_MR")
                If IsDBNull(dbConnections.dReader.Item("START_MR_COLOR")) Then
                    txtStartMRC.Text = ""
                Else
                    txtStartMRC.Text = dbConnections.dReader.Item("START_MR_COLOR")
                End If

                If IsDBNull(dbConnections.dReader.Item("BOOK_VALUE")) Then
                    txtBookValue.Text = 0
                Else
                    txtBookValue.Text = dbConnections.dReader.Item("BOOK_VALUE")
                End If


                If IsDBNull(dbConnections.dReader.Item("TECH_CODE")) Then
                    txtTechCode.Text = ""
                Else
                    txtTechCode.Text = dbConnections.dReader.Item("TECH_CODE")
                End If
                If IsDBNull(dbConnections.dReader.Item("REP_CODE")) Then
                    txtRepCode.Text = ""
                Else
                    txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")
                End If

                If IsDBNull(dbConnections.dReader.Item("ACCESSORIES")) Then
                    txtAccessories.Text = ""
                Else
                    txtAccessories.Text = dbConnections.dReader.Item("ACCESSORIES")
                End If

            End While
            dbConnections.dReader.Close()


            '// load selected Machine Desc
            strSQL = "SELECT     MACHINE_ID, MACHINE_MODEL FROM         MTBL_MACHINE_MASTER WHERE     (COM_ID = @COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachinePN.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("MACHINE_MODEL")) Then
                    lblMachineName.Text = "ERROR"
                Else
                    lblMachineName.Text = dbConnections.dReader.Item("MACHINE_MODEL")
                End If


            End While
            dbConnections.dReader.Close()


            '// load Tech Name

            strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
                    lblTechName.Text = "ERROR"
                Else
                    lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                End If


            End While
            dbConnections.dReader.Close()


            '// Load Rep Name

            strSQL = "SELECT     TBLU_USERHED.USERHED_TITLE FROM         TBLU_USERHED INNER JOIN  TBLU_COMPANY_DET ON TBLU_USERHED.USERHED_USERCODE = TBLU_COMPANY_DET.USERHED_USERCODE WHERE     (TBLU_USERHED.USERHED_USERCODE = @USERHED_USERCODE) and TBLU_COMPANY_DET.COM_ID = @COM_ID"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtRepCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("USERHED_TITLE")) Then
                    lblRepName.Text = "ERROR"
                Else
                    lblRepName.Text = dbConnections.dReader.Item("USERHED_TITLE")
                End If
            End While
            dbConnections.dReader.Close()
            txtSerialNo.Text = SelectedSN



            '// load agreements
            LoadCustomerInfo()

        Catch ex As Exception
            dbConnections.dReader.Close()
            'MsgBox(ex.Message)
        End Try



        Return Search
    End Function

    Private Function IsSerialExsist(ByRef Serial As String) As Boolean
        IsSerialExsist = False
        If Trim(Serial) = "" Then
            Exit Function
        End If
        Try
            Dim IsInvoiced As Boolean = False
            strSQL = "SELECT CASE WHEN EXISTS (SELECT     SERIAL FROM         TBL_MACHINE_TRANSACTIONS WHERE     (SERIAL = '" & Trim(Serial) & "') and COM_ID = '" & globalVariables.selectedCompanyID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsSerialExsist = True
            Else
                IsSerialExsist = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Return IsSerialExsist
    End Function

    Private Function IsPDNoExsist(ByRef PDNo As String) As Boolean
        IsPDNoExsist = False
        If Trim(PDNo) = "" Then
            Exit Function
        End If
        Try
            Dim IsInvoiced As Boolean = False
            strSQL = "SELECT CASE WHEN EXISTS (SELECT     P_NO FROM         TBL_MACHINE_TRANSACTIONS WHERE     (P_NO = '" & Trim(txtPno.Text) & "') and COM_ID = '" & globalVariables.selectedCompanyID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsPDNoExsist = True
            Else
                IsPDNoExsist = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Return IsPDNoExsist
    End Function


    Private Sub LoadCustomerInfo()
        errorEvent = "Reading information"
        'If IsFormClosing() Then Exit Sub
        'If Not isFormFocused Then Exit Sub
        'If Trim(sender.Text) = "" Then
        '    e.Cancel = True
        '    Exit Sub
        'End If
        connectionStaet()
        Try
            strSQL = "SELECT  CUS_NAME FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID) AND (CUS_ID =@CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")

            End While
            dbConnections.dReader.Close()


            Dim AgreementName As String = ""
            dgAgreement.Rows.Clear()
            strSQL = "SELECT     AG_ID, AG_NAME FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = @COM_ID) AND (CUS_CODE =@CUS_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True

                'dgAgreement.Rows.Add(dbConnections.dReader.Item("AG_ID"))
                AgreementName = ""
                If IsDBNull(dbConnections.dReader.Item("AG_NAME")) Then
                    AgreementName = dbConnections.dReader.Item("AG_ID")
                Else
                    AgreementName = dbConnections.dReader.Item("AG_NAME")
                End If
                populatreDatagrAgreements(dbConnections.dReader.Item("AG_ID"), AgreementName)
            End While
            dbConnections.dReader.Close()


            If hasRecords = False Then
                cbNewAgreement.Checked = True
            Else
                cbNewAgreement.Checked = False
            End If

            If hasRecords = True Then
                LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))
            End If


            'If hasRecords Then
            '    globalFunctions.globalButtonActivation(False, True, True, True, True, True)
            '    Me.saveBtnStatus()
            'Else
            '    '//New user permissions
            '    txtCustomerID.Enabled = False
            '    globalFunctions.globalButtonActivation(True, True, False, False, False, False)
            '    Me.saveBtnStatus()
            'End If
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub LoadNewAgrrementCUSinfor()
        If Trim(txtNewAgreementID.Text) = "" Then
            Exit Sub
        End If

        Try
            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT     TBL_CUS_AGREEMENT.CUS_CODE, MTBL_CUSTOMER_MASTER.CUS_NAME FROM  TBL_CUS_AGREEMENT INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_CUS_AGREEMENT.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_CUS_AGREEMENT.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_CUS_AGREEMENT.COM_ID = @COM_ID) AND (TBL_CUS_AGREEMENT.AG_ID = @AG_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtNewAgreementID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read


                If IsDBNull(dbConnections.dReader.Item("CUS_CODE")) Then
                    txtNewCusID.Text = ""
                Else
                    txtNewCusID.Text = dbConnections.dReader.Item("CUS_CODE")
                End If

                If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
                    txtNewCusName.Text = ""
                Else
                    txtNewCusName.Text = dbConnections.dReader.Item("CUS_NAME")
                End If


            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Dim IsErrorHave As Boolean = False
    Private Function isDataValid()
        isDataValid = False
        IsErrorHave = False
        ErrorProvider1.Clear()

        If generalValObj.isPresent(txtCustomerID) = False Then
            IsErrorHave = True
        End If
        If generalValObj.isPresent(txtCustomerName) = False Then
            IsErrorHave = True
        End If

        If cbGroup.Checked = False And cbIndividual.Checked = False Then
            'ErrorProvider1.SetError(cbGroup, "Please select the required option.")
            ErrorProvider1.SetError(cbIndividual, "Please select the required option.")
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtMachinePN) = False Then
            IsErrorHave = True
        End If
        If generalValObj.isPresent(txtSerialNo) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtPno) = False Then
            IsErrorHave = True
        End If

        If cbSpecialCase.Checked = True Then
            If generalValObj.isPresent(txtSpecialCase) = False Then
                IsErrorHave = True
            End If
        End If

        If generalValObj.isPresent(txtMLocation1) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtMLocation2) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtMLocation3) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtDept) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtContact) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtTel) = False Then
            IsErrorHave = True
        End If



        If rbtnInvStatusAll.Checked = False And rbtnInvStatusIndividual.Checked = False Then
            ErrorProvider1.SetError(rbtnInvStatusAll, "Please select the required option.")
            'ErrorProvider1.SetError(rbtnInvStatusIndividual, "Please select the required option.")
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtStartMR) = False Then
            IsErrorHave = True
        End If
        If generalValObj.isPresent(txtBookValue) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtTechCode) = False Then
            IsErrorHave = True
        End If

        If generalValObj.isPresent(txtRepCode) = False Then
            IsErrorHave = True
        End If


        If rbtnCommitment.Checked = False And rbtnActual.Checked = False And rbtnRental.Checked = False Then
            ErrorProvider1.SetError(rbtnActual, "Please select the required option.")
            IsErrorHave = True
        End If

        If rbtnBw.Checked = False And rbtnColor.Checked = False Then
            'ErrorProvider1.SetError(rbtnBw, "Please select the required option.")
            ErrorProvider1.SetError(rbtnColor, "Please select the required option.")
            IsErrorHave = True
        End If

        If cmbSlabMethod.Text = "--Select--" Then
            ErrorProvider1.SetError(cmbSlabMethod, "Please select the required option.")
            IsErrorHave = True
        End If

        If cmbBilPeriod.Text = "--Select--" Then
            ErrorProvider1.SetError(cmbBilPeriod, "Please select the required option.")
            IsErrorHave = True
        End If
        If rbtnRental.Checked = True Then
            If generalValObj.isPresent(txtRental) = False Then
                IsErrorHave = True
            End If
        End If


        If IsErrorHave = True Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        lblSerialInUse.Visible = False
        lblPDNoExsist.Visible = False
        cmbBilPeriod.SelectedIndex = 0
        cmbSlabMethod.SelectedIndex = 0
        txtSearch.Text = ""
        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        cbGroup.Checked = False
        cbIndividual.Checked = False
        txtSelectedAG.Text = ""
        cbNewAgreement.Checked = False
        txtMachinePN.Text = ""
        lblMachineName.Text = ""
        txtSerialNo.Text = ""
        txtPno.Text = ""
        cbSpecialCase.Checked = False
        txtSpecialCase.Text = ""
        txtMLocation1.Text = ""
        txtMLocation2.Text = ""
        txtMLocation3.Text = ""
        txtDept.Text = ""
        txtContact.Text = ""
        txtTel.Text = ""
        dtpInstallationDate.Value = Today.Date
        rbtnInvStatusAll.Checked = False
        rbtnInvStatusIndividual.Checked = False
        txtStartMR.Text = ""
        txtBookValue.Text = ""
        dtpAPStart.Value = Today.Date
        dtpAPEnd.Value = Today.Date
        txtTechCode.Text = ""
        lblTechName.Text = ""
        txtRepCode.Text = ""
        lblRepName.Text = ""
        rbtnCommitment.Checked = False
        rbtnActual.Checked = False
        cmbSlabMethod.SelectedIndex = 0
        rbtnBw.Checked = False
        rbtnColor.Checked = False
        cmbBilPeriod.SelectedIndex = 0
        rbtnRental.Checked = False
        txtRental.Text = ""
        lblMachineStartCode.Text = globalVariables.MachineRefCode + " No"
        dgAgreement.Rows.Clear()
        dgBw.Rows.Clear()
        dgColor.Rows.Clear()
        txtNewAgreementID.Text = ""
        txtNewCusID.Text = ""
        txtNewCusName.Text = ""
        txtAccessories.Text = ""
        txtCustomerID.Focus()


        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()
    End Sub



#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

    Private Sub txtCustomerID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtMachinePN_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMachinePN.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtTechCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTechCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtRepCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRepCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtCustomerID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCustomerID.Validating
        LoadCustomerInfo()

    End Sub

    Private Sub txtMachinePN_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMachinePN.Validating
        errorEvent = "Reading information"

        connectionStaet()
        Try
            strSQL = "SELECT     MACHINE_ID, MACHINE_MODEL FROM         MTBL_MACHINE_MASTER WHERE     (COM_ID = @COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachinePN.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("MACHINE_MODEL")) Then
                    lblMachineName.Text = "ERROR"
                Else
                    lblMachineName.Text = dbConnections.dReader.Item("MACHINE_MODEL")
                End If
            End While
            dbConnections.dReader.Close()

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub


    Private Sub txtRepCode_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtRepCode.Validating
        If globalVariables.selectedCompanyID = "003" Then

            errorEvent = "Reading information"

            connectionStaet()
            Try
                strSQL = "SELECT     TBLU_USERHED.USERHED_TITLE FROM         TBLU_USERHED INNER JOIN  TBLU_COMPANY_DET ON TBLU_USERHED.USERHED_USERCODE = TBLU_COMPANY_DET.USERHED_USERCODE WHERE     (TBLU_USERHED.USERHED_USERCODE = @USERHED_USERCODE) and TBLU_USERHED.USERHED_ACTIVEUSER = 1 and TBLU_COMPANY_DET.COM_ID = @COM_ID"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtRepCode.Text))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


                While dbConnections.dReader.Read
                    If IsDBNull(dbConnections.dReader.Item("USERHED_TITLE")) Then
                        lblRepName.Text = "ERROR"
                    Else
                        lblRepName.Text = dbConnections.dReader.Item("USERHED_TITLE")
                    End If


                End While
                dbConnections.dReader.Close()

            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
            End Try
        Else

            errorEvent = "Reading information"

            connectionStaet()
            Try
                strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE) AND (TECH_ACTIVE = 1)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtRepCode.Text))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


                While dbConnections.dReader.Read
                    If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
                        lblTechName.Text = "ERROR"
                    Else
                        lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                    End If


                End While
                dbConnections.dReader.Close()

            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
            End Try

        End If
       
    End Sub

    Private Sub txtTechCode_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTechCode.Validating
        errorEvent = "Reading information"

        connectionStaet()
        Try
            strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE) AND (TECH_ACTIVE = 1)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
                    lblTechName.Text = "ERROR"
                Else
                    lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                End If


            End While
            dbConnections.dReader.Close()

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
    '''''''''''''''''''''''''''''''''' Button Event  ...............................................................
    '===================================================================================================================
#Region "Button Event"
    Private Sub cbNewAgreement_CheckedChanged(sender As Object, e As EventArgs) Handles cbNewAgreement.CheckedChanged
        If sender.Checked = True Then
            sender.Text = "Yes"
        Else
            sender.Text = "No"
        End If

    End Sub

    Private Sub cbSpecialCase_CheckedChanged(sender As Object, e As EventArgs) Handles cbSpecialCase.CheckedChanged
        If sender.Checked = True Then
            sender.Text = "Yes"
        Else
            sender.Text = "No"
        End If
    End Sub

    Private Sub cbGroup_CheckedChanged(sender As Object, e As EventArgs) Handles cbGroup.CheckedChanged
        If cbGroup.Checked = True Then
            cbIndividual.Checked = False
        End If
    End Sub

    Private Sub cbIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles cbIndividual.CheckedChanged
        If cbIndividual.Checked = True Then
            cbGroup.Checked = False
        End If
    End Sub
#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()

    End Sub
#End Region





    Private Sub dgAgreement_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAgreement.CellClick
        Try
            txtSelectedAG.Text = dgAgreement.Item(0, e.RowIndex).Value
            If IsDBNull(dgAgreement.Item(1, e.RowIndex).Value) Then
                txtAgreementName.Text = dgAgreement.Item(0, e.RowIndex).Value
            Else
                txtAgreementName.Text = dgAgreement.Item(1, e.RowIndex).Value
            End If

            LoadSelectedAgreement()
        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Search("SN") = False Then
            Search("PNO")
        End If

    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub


    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        frmCustomerMaster.MdiParent = frmMDImain
        frmCustomerMaster.Show()
    End Sub

    Private Sub rbtnRental_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnRental.CheckedChanged
        If rbtnRental.Checked = True Then
            txtRental.Enabled = True
        Else
            txtRepCode.Text = 0
            txtRental.Enabled = False
        End If
    End Sub


    Private Sub txtStartMR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStartMR.KeyPress
        generalValObj.isDigit(e)
    End Sub

    Private Sub txtStartMRC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStartMRC.KeyPress
        generalValObj.isDigit(e)
    End Sub


    Private Sub dgAgreement_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAgreement.CellContentClick

    End Sub

    Private Sub btnUpdateAGName_Click(sender As Object, e As EventArgs) Handles btnUpdateAGName.Click
        Try
            Dim conf = MessageBox.Show("Do you wish to update Agreement name?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If conf = vbYes Then
                If Trim(txtAgreementName.Text) = "" Then
                    Exit Sub
                End If
                Dim IsSaved As Boolean = False
                strSQL = "UPDATE    TBL_CUS_AGREEMENT SET  AG_NAME =@AG_NAME WHERE     (COM_ID =@COM_ID) AND (AG_ID =@AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_NAME", Trim(txtAgreementName.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then IsSaved = True Else IsSaved = False

                If IsSaved = True Then
                    MessageBox.Show("Saved Successful.", "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

  

    Private Sub txtSerialNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSerialNo.Validating
        If Trim(txtSerialNo.Text) = "" Then
            lblSerialInUse.Visible = False
            Exit Sub
        End If
        If IsSerialExsist(txtSerialNo.Text) Then
            lblSerialInUse.Visible = True
        Else
            lblSerialInUse.Visible = False
        End If
    End Sub

   

    Private Sub txtPno_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtPno.Validating
        If Trim(txtPno.Text) = "" Then
            lblPDNoExsist.Visible = False
            Exit Sub
        End If
        If IsPDNoExsist(txtPno.Text) Then
            lblPDNoExsist.Visible = True
        Else
            lblPDNoExsist.Visible = False
        End If
    End Sub

   

    Private Sub txtSearch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating
        If Search("SN") = False Then
            Search("PNO")
        End If

    End Sub

    Private Sub txtRepCode_TextChanged(sender As Object, e As EventArgs) Handles txtRepCode.TextChanged

    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        AgreementMachineTransfer()
    End Sub


 

    Private Sub txtNewAgreementID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNewAgreementID.Validating
        LoadNewAgrrementCUSinfor()
    End Sub

    Private Sub txtTechCode_TextChanged(sender As Object, e As EventArgs) Handles txtTechCode.TextChanged

    End Sub

    Private Sub brnAddDownload_Click(sender As Object, e As EventArgs) Handles brnAddDownload.Click
        strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2, CUS_CONTACT_NO FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID = @CUS_ID)"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

        While dbConnections.dReader.Read

            If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
                txtMLocation1.Text = ""
            Else
                txtMLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
            End If
            If IsDBNull(dbConnections.dReader.Item("CUS_ADD1")) Then
                txtMLocation2.Text = ""
            Else
                txtMLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
            End If
            If IsDBNull(dbConnections.dReader.Item("CUS_ADD2")) Then
                txtMLocation3.Text = ""
            Else
                txtMLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
            End If

            If IsDBNull(dbConnections.dReader.Item("CUS_CONTACT_NO")) Then
                txtTel.Text = ""
            Else
                txtTel.Text = dbConnections.dReader.Item("CUS_CONTACT_NO")
            End If

        End While
        dbConnections.dReader.Close()
    End Sub

    Private Sub btnSlabHelp_Click(sender As Object, e As EventArgs) Handles btnSlabHelp.Click


        'OpenFile(globalVariables.crystalReportpath + "\Manuals\SlabMethods.pdf")

        System.Diagnostics.Process.Start(globalVariables.crystalReportpath + "\Manuals\SlabMethods.pdf")
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub btnSetMR_Click(sender As Object, e As EventArgs) Handles btnSetMR.Click
        If SetMR() = True Then
            MessageBox.Show("Meter Reading Updated.", "Updated.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Meter Reading not Updated.", "Not Updated.", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End If
    End Sub


    Private Function SetMR() As Boolean
        SetMR = False

        If Trim(txtSelectedAG.Text) = "" Then
            Exit Function
        End If

        If Trim(txtSerialNo.Text) = "" Then
            Exit Function
        End If

        If rbtnBw.Checked = True Then
            If Trim(txtStartMR.Text) = "" Then
                Exit Function
            End If
        Else
            If Trim(txtStartMR.Text) = "" Then
                Exit Function
            End If

            If Trim(txtStartMRC.Text) = "" Then
                Exit Function
            End If
        End If


        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then

            strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET SMR_ADUJESTED_STATUS=@SMR_ADUJESTED_STATUS ,START_MR=@START_MR, START_MR_COLOR=@START_MR_COLOR  WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID) AND (SERIAL = @SERIAL)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
            dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))


            dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR", Trim(txtStartMR.Text))

            If Trim(txtStartMRC.Text) = "" Then
                dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", DBNull.Value)
            Else
                dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", Trim(txtStartMRC.Text))
            End If

            dbConnections.sqlCommand.Parameters.AddWithValue("@SMR_ADUJESTED_STATUS", "PENDING CAPTURE")
            '// Capture status
            '// 1 PENDING CAPTURE
            '// 2 UPDATED




            If dbConnections.sqlCommand.ExecuteNonQuery() Then SetMR = True Else SetMR = False
        End If
        Return SetMR
    End Function
End Class