Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmReturnMachine


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

        Dim RTN_ID As String = ""

        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If

            RTN_ID = Genarate_Return_NO()
            Try


                connectionStaet()

                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction




                errorEvent = "Save"
                strSQL = "INSERT INTO TBL_RETUN_TRANSACTION (COM_ID, R_ID, R_DATE, SERIAL, AG_ID, MACHINE_PN, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE, CUS_ID, R_COMMENT) VALUES     (@COM_ID, @R_ID, GETDATE() , @SERIAL, @AG_ID, @MACHINE_PN, @P_NO, @IS_SPECIAL_CASE, @SPECIAL_CASE_DESC, @M_LOC1, @M_LOC2, @M_LOC3, @M_DEPT, @CONTACT_PERSON, @CONTACT_NO, @INSTALLATION_DATE, @START_MR, @BOOK_VALUE, @TECH_CODE, @REP_CODE, @CUS_ID, @R_COMMENT)"




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

                If Trim(txtStartMR.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", 0)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@START_MR_COLOR", CDbl(Trim(txtStartMR.Text)))
                End If


                If Trim(txtBookValue.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BOOK_VALUE", 0)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@BOOK_VALUE", CDbl(Trim(txtBookValue.Text)))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRepCode.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

                dbConnections.sqlCommand.Parameters.AddWithValue("@R_ID", RTN_ID)

                dbConnections.sqlCommand.Parameters.AddWithValue("@R_COMMENT", Trim(txtComment.Text))

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                '// UPDATING STOCK

                errorEvent = "Edit"
                strSQL = "UPDATE  TBL_MACHINE_STOCK SET  MACHINE_PN =@MACHINE_PN, BOOK_VALUE =@BOOK_VALUE, SN_STATUS =@SN_STATUS, CURRENT_CUS_ID =@CURRENT_CUS_ID, CURRENT_AG_ID =@CURRENT_AG_ID, CURRENT_MR =@CURRENT_MR, MACHINE_TYPE =@IS_ACTIVE, IS_ACTIVE =@IS_ACTIVE, CURRENT_MR_COLOR=@CURRENT_MR_COLOR WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_PN", Trim(txtMachinePN.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@BOOK_VALUE", CDbl(txtBookValue.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@SN_STATUS", "AVAILABLE")
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_CUS_ID", Trim(txtCustomerID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_AG_ID", Trim(txtAgreementID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR", CInt(txtStartMR.Text))
                If Trim(txtStartMRC.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR_COLOR", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CURRENT_MR_COLOR", CInt(txtStartMRC.Text))
                End If

                If Trim(txtStartMRC.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_TYPE", "BW")
                Else
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
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtAgreementID.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AU_STATUS", "IN-STOCK")
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False



                strSQL = "DELETE FROM TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL =@SERIAL)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(txtSerialNo.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                dbConnections.sqlTransaction.Commit()

                If save Then
                    MessageBox.Show("Machine Removed.", "Removed.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
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

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmReturnMachine_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmReturnMachine_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmReturnMachine_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmReturnMachine_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmReturnMachine_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmReturnMachine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()

    End Sub

    Private Sub frmReturnMachine_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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

            txtAgreementID.Text = SelectedAg
            txtSerialNo.Text = SelectedSN
            If SelectedSN = "" Then
                Exit Function
            End If
            txtCustomerID.Text = SelCusCode


            strSQL = "SELECT  CUS_NAME FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID) AND (CUS_ID =@CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")

            End While
            dbConnections.dReader.Close()
       
            '// loading machine Info

            strSQL = "SELECT      MACHINE_PN, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID =@COM_ID) AND (AG_ID =@AG_ID) AND (SERIAL =@SERIAL)"
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


                txtDept.Text = dbConnections.dReader.Item("M_DEPT")
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
                txtBookValue.Text = dbConnections.dReader.Item("BOOK_VALUE")
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

            strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtRepCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
                    lblRepName.Text = "ERROR"
                Else
                    lblRepName.Text = dbConnections.dReader.Item("TECH_NAME")
                End If


            End While
            dbConnections.dReader.Close()

         

        Catch ex As Exception
            dbConnections.dReader.Close()
            'MsgBox(ex.Message)
        End Try



        Return Search
    End Function




    ''// search by  = bySN,ByPN/ByAG
    'Private Function Search(ByRef SearchBy As String) As Boolean
    '    Search = False

    '    If Trim(txtSearch.Text) = "" Then
    '        Exit Function
    '    End If
    '    Dim SelectedSN As String = ""
    '    Dim SelectedPNo As String = ""
    '    Dim SelectedAg As String = ""
    '    Dim SelCusCode As String = ""
    '    Try




    '        If SearchBy = "SN" Then
    '            strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (SERIAL = '" & Trim(txtSearch.Text) & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                Search = True
    '                SelectedSN = dbConnections.dReader.Item("SERIAL")
    '                SelectedPNo = dbConnections.dReader.Item("P_NO")
    '                SelectedAg = dbConnections.dReader.Item("AG_ID")
    '                SelCusCode = dbConnections.dReader.Item("CUS_ID")
    '                txtCustomerID.Text = SelCusCode
    '                txtSerialNo.Text = SelectedSN
    '                txtAgreementID.Text = SelectedAg
    '            End While
    '            dbConnections.dReader.Close()
    '        Else
    '            strSQL = "SELECT     AG_ID, SERIAL, P_NO,CUS_ID FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (P_NO = '" & Trim(txtSearch.Text) & "')"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '            While dbConnections.dReader.Read
    '                SelectedSN = dbConnections.dReader.Item("SERIAL")
    '                SelectedPNo = dbConnections.dReader.Item("P_NO")
    '                SelectedAg = dbConnections.dReader.Item("AG_ID")
    '                SelCusCode = dbConnections.dReader.Item("CUS_ID")
    '                txtCustomerID.Text = SelCusCode
    '                txtSerialNo.Text = SelectedSN
    '            End While
    '            dbConnections.dReader.Close()
    '        End If



    '        If SelectedSN = "" Then
    '            Exit Function
    '        End If

    '        strSQL = "SELECT  CUS_NAME FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID = @COM_ID) AND (CUS_ID =@CUS_ID)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read

    '            txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")

    '        End While
    '        dbConnections.dReader.Close()


    '        '// loading machine Info

    '        strSQL = "SELECT      MACHINE_PN, P_NO, IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, M_DEPT, CONTACT_PERSON, CONTACT_NO, INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID =@COM_ID) AND (AG_ID =@AG_ID) AND (SERIAL =@SERIAL)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", SelectedAg)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", SelectedSN)
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read

    '            txtMachinePN.Text = dbConnections.dReader.Item("MACHINE_PN")
    '            txtPno.Text = dbConnections.dReader.Item("P_NO")
    '            cbSpecialCase.Checked = dbConnections.dReader.Item("IS_SPECIAL_CASE")

    '            If IsDBNull(dbConnections.dReader.Item("SPECIAL_CASE_DESC")) Then
    '                txtSpecialCase.Text = ""
    '            Else
    '                txtSpecialCase.Text = dbConnections.dReader.Item("SPECIAL_CASE_DESC")
    '            End If


    '            txtMLocation1.Text = dbConnections.dReader.Item("M_LOC1")

    '            txtMLocation2.Text = dbConnections.dReader.Item("M_LOC2")
    '            txtMLocation3.Text = dbConnections.dReader.Item("M_LOC3")

    '            txtDept.Text = dbConnections.dReader.Item("M_DEPT")
    '            txtContact.Text = dbConnections.dReader.Item("CONTACT_PERSON")
    '            txtTel.Text = dbConnections.dReader.Item("CONTACT_NO")
    '            dtpInstallationDate.Value = dbConnections.dReader.Item("INSTALLATION_DATE")
    '            txtStartMR.Text = dbConnections.dReader.Item("START_MR")
    '            txtBookValue.Text = dbConnections.dReader.Item("BOOK_VALUE")
    '            txtTechCode.Text = dbConnections.dReader.Item("TECH_CODE")
    '            txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")


    '        End While
    '        dbConnections.dReader.Close()


    '        '// load selected Machine Desc
    '        strSQL = "SELECT     MACHINE_ID, MACHINE_MODEL FROM         MTBL_MACHINE_MASTER WHERE     (COM_ID = @COM_ID) AND (MACHINE_ID = @MACHINE_ID)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@MACHINE_ID", Trim(txtMachinePN.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("MACHINE_MODEL")) Then
    '                lblMachineName.Text = "ERROR"
    '            Else
    '                lblMachineName.Text = dbConnections.dReader.Item("MACHINE_MODEL")
    '            End If


    '        End While
    '        dbConnections.dReader.Close()


    '        '// load Tech Name

    '        strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
    '                lblTechName.Text = "ERROR"
    '            Else
    '                lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
    '            End If


    '        End While
    '        dbConnections.dReader.Close()


    '        '// Load Rep Name

    '        strSQL = "SELECT     TBLU_USERHED.USERHED_TITLE FROM         TBLU_USERHED INNER JOIN  TBLU_COMPANY_DET ON TBLU_USERHED.USERHED_USERCODE = TBLU_COMPANY_DET.USERHED_USERCODE WHERE     (TBLU_USERHED.USERHED_USERCODE = @USERHED_USERCODE) and TBLU_COMPANY_DET.COM_ID = @COM_ID"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtRepCode.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("USERHED_TITLE")) Then
    '                lblRepName.Text = "ERROR"
    '            Else
    '                lblRepName.Text = dbConnections.dReader.Item("USERHED_TITLE")
    '            End If


    '        End While
    '        dbConnections.dReader.Close()


    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try



    '    Return Search
    'End Function


    Private Function Genarate_Return_NO() As String

        Genarate_Return_NO = ""
        Dim preFixComName As String = ""

        errorEvent = "Reading information"
        connectionStaet()

        Try



            strSQL = "SELECT     max(R_ID) FROM         TBL_RETUN_TRANSACTION WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') and R_ID like '%" & globalVariables.selectedCompanyID & "/RTN/%'"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                Genarate_Return_NO = globalVariables.selectedCompanyID & "/" & "RTN" & "/" & 1
            Else
                Dim IRCodeSplit() As String
                Dim NoRecordFound As Boolean = False
                Dim IRID As Integer = 0
                IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                IRID = IRCodeSplit(2)
                Do Until NoRecordFound = True
                    strSQL = "SELECT CASE WHEN EXISTS (SELECT    R_ID FROM         TBL_RETUN_TRANSACTION WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') and R_ID  =  '" & globalVariables.selectedCompanyID & "/RTN/ " & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteScalar = False Then
                        NoRecordFound = True
                    Else
                        IRID = IRID + 1
                    End If
                Loop

                If NoRecordFound = True Then
                    Genarate_Return_NO = globalVariables.selectedCompanyID & "/RTN/" & IRID
                Else
                    Exit Function
                End If

            End If


        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally

            connectionClose()
        End Try
        Return Genarate_Return_NO
    End Function

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Dim IsErrorHave As Boolean = False
    Private Function isDataValid()
        isDataValid = False

        ErrorProvider1.Clear()

        If generalValObj.isPresent(txtComment) = False Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()


        txtSearch.Text = ""
        txtCustomerID.Text = ""
        txtCustomerName.Text = ""

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
        txtStartMR.Text = ""
        txtBookValue.Text = ""
        txtTechCode.Text = ""
        lblTechName.Text = ""
        txtRepCode.Text = ""
        lblRepName.Text = ""
        lblMachineStartCode.Text = globalVariables.MachineRefCode + " No"
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
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
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



        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try

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


    Private Sub cbSpecialCase_CheckedChanged(sender As Object, e As EventArgs) Handles cbSpecialCase.CheckedChanged
        If sender.Checked = True Then
            sender.Text = "Yes"
        Else
            sender.Text = "No"
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Search("SN") = False Then
            Search("PNO")
        End If

    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        save()
    End Sub


    Private Sub txtSearch_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtSearch.Validating
        If Search("SN") = False Then
            Search("PNO")
        End If

    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub
End Class