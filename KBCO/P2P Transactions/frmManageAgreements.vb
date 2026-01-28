Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmManageAgreements



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
                    strSQL = "UPDATE  TBL_CUS_AGREEMENT SET  CUS_CODE =@CUS_CODE, CUS_TYPE =@CUS_TYPE, BILLING_METHOD =@BILLING_METHOD, SLAB_METHOD =@SLAB_METHOD, BILLING_PERIOD =@BILLING_PERIOD, MD_BY ='" & userSession & "', MD_DATE =GETDATE() , INV_STATUS=@INV_STATUS , MACHINE_TYPE=@MACHINE_TYPE , AG_PERIOD_START=@AG_PERIOD_START , AG_PERIOD_END=@AG_PERIOD_END ,AG_RENTAL_PRICE=@AG_RENTAL_PRICE  WHERE     (COM_ID = @COM_ID) AND (AG_ID =@AG_ID)"

                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO TBL_CUS_AGREEMENT (COM_ID, AG_ID, CUS_CODE, CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, CR_BY, CR_DATE,INV_STATUS,MACHINE_TYPE,AG_PERIOD_START,AG_PERIOD_END,AG_RENTAL_PRICE) VALUES     (@COM_ID, @AG_ID, @CUS_CODE, @CUS_TYPE, @BILLING_METHOD, @SLAB_METHOD, @BILLING_PERIOD, '" & userSession & "', GETDATE(),@INV_STATUS,@MACHINE_TYPE,@AG_PERIOD_START,@AG_PERIOD_END,@AG_RENTAL_PRICE)"
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

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


                'BW_RANGE_1
                'BW_RANGE_2
                'BW_RATE

                'COLOR_RANGE_1
                'COLOR_RANGE_2
                'COLOR_RATE
                'If IsNewAgreement = True Then '//  commitments will save only for new agreement creation
                If dgBw.Rows.Count > 0 Then


                    strSQL = "DELETE FROM TBL_AG_BW_COMMITMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (AG_CODE = '" & Trim(SelectedAgreement) & "')"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()

                    'Data grid Data feeding code

                    For Each row As DataGridViewRow In dgBw.Rows

                        If dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value <> 0 Then
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
                        If dgColor.Rows(row.Index).Cells("COLOR_RANGE_1").Value <> 0 Then


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
                'End If







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

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtCustomerName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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
    Private Sub frmManageAgreements_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmManageAgreements_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmManageAgreements_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmManageAgreements_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmManageAgreements_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmManageAgreements_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()

    End Sub

    Private Sub frmManageAgreements_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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




    'Private Function GenarateAGNo() As String

    '    GenarateAGNo = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()


    '    Try
    '        strSQL = "SELECT    MAX( AG_ID) as 'MAX' FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID LIKE '" & Trim(globalVariables.selectedCompanyID) & "/" & Trim(txtCustomerID.Text) & "%') "
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteNonQuery) Then
    '            GenarateAGNo = globalVariables.selectedCompanyID & "/" & Trim(txtCustomerID.Text) & "/" & 1
    '        Else
    '            Dim AGCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim AGID As Integer = 0
    '            AGCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            AGID = AGCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT     AG_ID FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & AGCodeSplit(0) & "/" & AGCodeSplit(1) & "/" & AGID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    AGID = AGID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                GenarateAGNo = AGCodeSplit(0) & "/" & AGCodeSplit(1) & "/" & AGID
    '            Else
    '                Exit Function
    '            End If

    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X10) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally

    '        connectionClose()
    '    End Try
    'End Function


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
            MsgBox(ex.InnerException.Message)
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
            MsgBox(ex.InnerException.Message)
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
            MsgBox(ex.InnerException.Message)
        End Try


        Dim PNo As String = ""

        dgMachineList.Rows.Clear()
        Try
            'strSQL = "SELECT     TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO, TBL_MACHINE_TRANSACTIONS.M_LOC3, TBL_MACHINE_MODEL_LIST.Descreption FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN TBL_MACHINE_MODEL_LIST ON TBL_MACHINE_TRANSACTIONS.COM_ID = TBL_MACHINE_MODEL_LIST.COM_ID AND  TBL_MACHINE_TRANSACTIONS.MACHINE_PN = TBL_MACHINE_MODEL_LIST.ItemCode WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.AG_ID = '" & Trim(txtSelectedAG.Text) & "')"
            strSQL = "SELECT     TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO, TBL_MACHINE_TRANSACTIONS.M_DEPT, MTBL_MACHINE_MASTER.MACHINE_MODEL FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN    MTBL_MACHINE_MASTER ON TBL_MACHINE_TRANSACTIONS.COM_ID = MTBL_MACHINE_MASTER.COM_ID AND  TBL_MACHINE_TRANSACTIONS.MACHINE_PN = MTBL_MACHINE_MASTER.MACHINE_ID WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.AG_ID = '" & Trim(txtSelectedAG.Text) & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                PNo = ""
                If IsDBNull(dbConnections.dReader.Item("P_NO")) Then
                    PNo = ""
                Else
                    PNo = dbConnections.dReader.Item("P_NO")

                End If
                populatreDatagrid_MachineList(dbConnections.dReader.Item("SERIAL"), PNo, dbConnections.dReader.Item("MACHINE_MODEL"), dbConnections.dReader.Item("M_DEPT"))
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try


        LoadCommitments(Trim(txtSelectedAG.Text), Trim(txtCustomerID.Text))

    End Sub

    Private Sub populatreDatagrid_MachineList(ByRef SN As String, ByRef PNo As String, ByRef model As String, ByRef Location As String)
        dgMachineList.ColumnCount = 4
        dgMachineList.Rows.Add(SN, PNo, model, Location)
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





        If rbtnInvStatusAll.Checked = False And rbtnInvStatusIndividual.Checked = False Then
            ErrorProvider1.SetError(rbtnInvStatusAll, "Please select the required option.")
            'ErrorProvider1.SetError(rbtnInvStatusIndividual, "Please select the required option.")
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

        cmbBilPeriod.SelectedIndex = 0
        cmbSlabMethod.SelectedIndex = 0

        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        cbGroup.Checked = False
        cbIndividual.Checked = False
        txtSelectedAG.Text = ""
        cbNewAgreement.Checked = False

        rbtnInvStatusAll.Checked = False
        rbtnInvStatusIndividual.Checked = False

        dtpAPStart.Value = Today.Date
        dtpAPEnd.Value = Today.Date

        rbtnCommitment.Checked = False
        rbtnActual.Checked = False
        cmbSlabMethod.SelectedIndex = 0
        rbtnBw.Checked = False
        rbtnColor.Checked = False
        cmbBilPeriod.SelectedIndex = 0
        rbtnRental.Checked = False
        txtRental.Text = ""

        dgAgreement.Rows.Clear()
        dgBw.Rows.Clear()
        dgColor.Rows.Clear()
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


            Dim AgreementName As String = ""
            dgAgreement.Rows.Clear()
            strSQL = "SELECT     AG_ID,AG_NAME FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = @COM_ID) AND (CUS_CODE =@CUS_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True

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

    Private Sub populatreDatagrAgreements(ByRef AG_ID As String, ByRef AG_NAME As String)
        dgAgreement.ColumnCount = 2
        dgAgreement.Rows.Add(AG_ID, AG_NAME)
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
            txtAgreementName.Text = dgAgreement.Item(1, e.RowIndex).Value
            LoadSelectedAgreement()
        Catch ex As Exception

        End Try

    End Sub




    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        frmCustomerMaster.MdiParent = frmMDImain
        frmCustomerMaster.Show()
    End Sub

    Private Sub rbtnRental_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnRental.CheckedChanged
        If rbtnRental.Checked = True Then
            txtRental.Enabled = True
        Else
            txtRental.Text = ""
            txtRental.Enabled = False
        End If
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        TransferAgreement()
    End Sub



    Private Sub TransferAgreement()
        errorEvent = "Reading information"
        If Trim(txtTRAgCode.Text) = "" Then
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "UPDATE    TBL_MACHINE_TRANSACTIONS SET              AG_ID ='" & Trim(txtTRAgCode.Text) & "' WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(txtSelectedAG.Text))
            If dbConnections.sqlCommand.ExecuteNonQuery() Then
                MessageBox.Show("Transfer Successful.", "Transfered", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
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
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub
End Class