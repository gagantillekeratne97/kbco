Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmRecipts


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
                PrintRecipt(txtReciptID.Text)
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
            Dim NextReciptID As Integer = GetReciptID()
            txtReciptID.Text = NextReciptID
            Try
                connectionStaet()

                strSQL = "INSERT INTO TBL_RECIPTS (COM_ID,  RECIPT_ID, RECIPT_DATE, INV_NO, RECIPT_TYPE, RECIPT_AMOUNT, RECIPT_AMOUNT_IN_WORDS, RECIPT_PAYMENT_METHOD, CHEQUE_NO, CHEQUE_BANK, PAYMENT_COLLECTED_BY, PAYMENT_COLLECTED_BY_NAME, RECIPT_BY, RECIPT_BY_NAME, RECIPT_STATE, IS_FIRST_PRINT) VALUES     (@COM_ID, @RECIPT_ID, GETDATE(), @INV_NO, @RECIPT_TYPE, @RECIPT_AMOUNT, @RECIPT_AMOUNT_IN_WORDS, @RECIPT_PAYMENT_METHOD, @CHEQUE_NO, @CHEQUE_BANK, @PAYMENT_COLLECTED_BY, @PAYMENT_COLLECTED_BY_NAME, @RECIPT_BY, @RECIPT_BY_NAME, @RECIPT_STATE, 1)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_ID", NextReciptID)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_TYPE", cmbReciptType.Text)
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_AMOUNT", CDbl(txtPaymentAmount.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_AMOUNT_IN_WORDS", Trim(txtAmountInWords.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_PAYMENT_METHOD", cmbPaymentMethod.Text)

                If cmbPaymentMethod.Text = "CHEQUE" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value)
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_BANK", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", Trim(txtChequeNo.Text))
                    dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_BANK", Trim(txtBankName.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@PAYMENT_COLLECTED_BY", Trim(txtRecivedBy.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@PAYMENT_COLLECTED_BY_NAME", Trim(lblTechName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_BY", userSession)
                dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_BY_NAME", userName)

                If cmbReciptType.Text = "FULL PAYMENT" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_STATE", "FULL PAID")
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_STATE", "OUTSTANDING")
                End If


                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


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


        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtInvoiceNo.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then

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
    Private Sub frmRecipts_Master_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmRecipts_Master_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmRecipts_Master_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmRecipts_Master_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmRecipts_Master_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmRecipts_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmRecipts_Master_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
        If generalValObj.isPresent(txtInvoiceNo) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtInvoiceAmount) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtPaymentAmount) = False Then
            Exit Function
        End If

        If generalValObj.isPresent(txtAmountInWords) = False Then
            Exit Function
        End If


        If cmbPaymentMethod.Text = "CHEQUE" Then
            If generalValObj.isPresent(txtChequeNo) = False Then
                Exit Function
            End If
            If generalValObj.isPresent(txtBankName) = False Then
                Exit Function
            End If


        End If
        If generalValObj.isPresent(txtRecivedBy) = False Then
            Exit Function
        End If


        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()

        txtInvoiceNo.Text = ""
        txtInvoiceAmount.Text = ""
        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        cmbReciptType.SelectedIndex = 0
        txtPaymentAmount.Text = ""
        txtAmountInWords.Text = ""
        cmbPaymentMethod.SelectedIndex = 0
        txtChequeNo.Text = ""
        txtBankName.Text = ""
        txtRecivedBy.Text = ""
        txtInvoiceNo.Enabled = True

        IsEnable(True)
        txtInvoiceNo.Focus()


        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()
    End Sub


    Private Sub IsEnable(ByRef enableState As Boolean)


        txtInvoiceAmount.Enabled = enableState
        txtCustomerID.Enabled = enableState
        txtCustomerName.Enabled = enableState
        cmbReciptType.Enabled = enableState
        txtPaymentAmount.Enabled = enableState
        txtAmountInWords.Enabled = enableState
        cmbPaymentMethod.Enabled = enableState
        txtChequeNo.Enabled = enableState
        txtBankName.Enabled = enableState
        txtRecivedBy.Enabled = enableState
    End Sub


    Function NumberToText(ByVal n As Integer) As String

        Select Case n
            Case 0
                Return ""

            Case 1 To 19
                Dim arr() As String = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", _
                  "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", _
                    "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Return arr(n - 1) & " "

            Case 20 To 99
                Dim arr() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Return arr(n \ 10 - 2) & " " & NumberToText(n Mod 10)

            Case 100 To 199
                Return "One Hundred " & NumberToText(n Mod 100)

            Case 200 To 999
                Return NumberToText(n \ 100) & "Hundreds " & NumberToText(n Mod 100)

            Case 1000 To 1999
                Return "One Thousand " & NumberToText(n Mod 1000)

            Case 2000 To 999999
                Return NumberToText(n \ 1000) & "Thousands " & NumberToText(n Mod 1000)

            Case 1000000 To 1999999
                Return "One Million " & NumberToText(n Mod 1000000)

            Case 1000000 To 999999999
                Return NumberToText(n \ 1000000) & "Millions " & NumberToText(n Mod 1000000)

            Case 1000000000 To 1999999999
                Return "One Billion " & NumberTotext(n Mod 1000000000)

            Case Else
                Return NumberToText(n \ 1000000000) & "Billion " _
                  & NumberToText(n Mod 1000000000)
        End Select
    End Function


    Private Function GetReciptID() As Integer
        GetReciptID = 1
        Try
            strSQL = "SELECT     MAX(RECIPT_ID) + 1 AS Expr1 FROM         TBL_RECIPTS WHERE     (COM_ID = @COM_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GetReciptID = 1
            Else
                GetReciptID = dbConnections.sqlCommand.ExecuteScalar

            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

        Return GetReciptID
    End Function



    Private Sub Load_PreviousData()
        Try
            dgHistory.Rows.Clear()
            strSQL = "SELECT     SEQ_ID, RECIPT_ID, RECIPT_DATE, INV_NO, RECIPT_AMOUNT FROM         TBL_RECIPTS WHERE     (INV_NO =@INV_NO) AND (COM_ID = @COM_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                populatreDatagrid(dbConnections.dReader.Item("RECIPT_ID"), dbConnections.dReader.Item("RECIPT_AMOUNT"), CDate(dbConnections.dReader.Item("RECIPT_DATE")).ToShortDateString)
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef ReciptNo As String, ByRef Amount As Decimal, ByRef TDate As String)
        dgHistory.ColumnCount = 3
        dgHistory.Rows.Add(ReciptNo, Amount, TDate)
    End Sub


    Private Sub Calculate_balance()
        Dim HistryValue As Decimal = 0
        Dim InvoiceValue As Decimal = 0
        Dim BalanceValue As Decimal = 0

        Try
            If Trim(txtInvoiceAmount.Text) = "" Then
                InvoiceValue = 0
            Else
                InvoiceValue = CDbl(Trim(txtInvoiceAmount.Text))
            End If
            For Each row As DataGridViewRow In dgHistory.Rows

                HistryValue = HistryValue + dgHistory.Rows(row.Index).Cells("RECIPT_AMOUNT").Value

            Next

            BalanceValue = InvoiceValue - HistryValue

            lblOutstanding.Text = BalanceValue.ToString("N2")

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

    Private Sub txtInvoiceNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtInvoiceNo.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub



    Private Sub txtInvoiceNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtInvoiceNo.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT     TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.INV_PRINTED, MTBL_CUSTOMER_MASTER.CUS_NAME FROM         TBL_INVOICE_MASTER INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID WHERE     (TBL_INVOICE_MASTER.COM_ID = @COM_ID) AND (TBL_INVOICE_MASTER.INV_NO = @INV_NO)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtCustomerID.Text = dbConnections.dReader.Item("CUS_ID")
                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")

            End While
            dbConnections.dReader.Close()
            '  SELECT    SUM( BW_RATE * BW_COPY_BREAKUP) AS Expr1 FROM         TBL_INV_BW_COMMITMENT WHERE     (COM_ID = '001') AND (INV_NO = '001/INV/1')


            strSQL = "SELECT    SUM( BW_RATE * BW_COPY_BREAKUP) AS Expr1 FROM         TBL_INV_BW_COMMITMENT WHERE     (COM_ID = @COM_ID) AND (INV_NO = @INV_NO)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceNo.Text))
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                txtInvoiceAmount.Text = "0.00"
            Else
                txtInvoiceAmount.Text = dbConnections.sqlCommand.ExecuteScalar

            End If

            Load_PreviousData()
            Calculate_balance()
            globalFunctions.globalButtonActivation(False, True, True, True, True, True)

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub txtBankName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBankName.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtBankName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBankName.Validating


        connectionStaet()
        Try
            strSQL = "SELECT     Bank_Name FROM         TBL_BANK WHERE     (Bank_ID =@Bank_ID) AND (COM_ID =@COM_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_ID", Trim(txtBankName.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                lblBankName.Text = dbConnections.dReader.Item("VAT_DESC")

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

    Private Sub txtRecivedBy_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRecivedBy.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub


    Private Sub txtRecivedBy_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtRecivedBy.Validating
        connectionStaet()
        Try
            strSQL = "SELECT     TBLU_USERHED.USERHED_USERCODE, TBLU_USERHED.USERHED_TITLE, TBLU_COMPANY_DET.COM_ID FROM         TBLU_USERHED INNER JOIN  TBLU_COMPANY_DET ON TBLU_USERHED.USERHED_USERCODE = TBLU_COMPANY_DET.USERHED_USERCODE WHERE     (TBLU_USERHED.USERHED_USERCODE = @USERHED_USERCODE) and TBLU_COMPANY_DET.COM_ID = @COM_ID"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@USERHED_USERCODE", Trim(txtRecivedBy.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                lblTechName.Text = dbConnections.dReader.Item("USERHED_TITLE")

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
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

#End Region

    Private Sub txtPaymentAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPaymentAmount.KeyPress
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub


    Private Sub txtPaymentAmount_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtPaymentAmount.Validating

        Dim PayAmount As Decimal = 0
        If Trim(txtPaymentAmount.Text) = "" Then
            Exit Sub
        End If

        PayAmount = CDbl(Trim(txtPaymentAmount.Text))

        txtAmountInWords.Text = ConvertNum(PayAmount)
        Calculate_balance()
    End Sub


    Public Shared Function ConvertNum(ByVal Input As Decimal) As String


        Dim formatnumber As String
        Dim numparts(10) As String ' break the number into parts
        Dim suffix(10) As String 'trillion, billion .million etc
        Dim Wordparts(10) As String  'add the number parts and suffix

        Dim output As String = Nothing

        Dim T As String = ""
        Dim B As String = ""
        Dim M As String = ""
        Dim TH As String = ""
        Dim H As String = ""
        Dim C As String = ""


        formatnumber = Format(Input, "0000000000000.00") 'format the input number to a 16 characters string by suffixing and prefixing 0s
        '
        numparts(0) = primWord(Mid(formatnumber, 1, 1)) 'Trillion

        numparts(1) = primWord(Mid(formatnumber, 2, 1)) 'hundred billion..x
        numparts(2) = primWord(Mid(formatnumber, 3, 2)) 'billion

        numparts(3) = primWord(Mid(formatnumber, 5, 1)) 'hundred million...x
        numparts(4) = primWord(Mid(formatnumber, 6, 2)) 'million

        numparts(5) = primWord(Mid(formatnumber, 8, 1)) 'hundred thousand....x
        numparts(6) = primWord(Mid(formatnumber, 9, 2)) 'thousand


        numparts(7) = primWord(Mid(formatnumber, 11, 1)) 'hundred
        numparts(8) = primWord(Mid(formatnumber, 12, 2)) 'Tens

        numparts(9) = primWord(Mid(formatnumber, 15, 2)) 'cents



        suffix(0) = " Trillion "
        suffix(1) = " Hundred "  '....x
        suffix(2) = " Billion "
        suffix(3) = " Hundred " '  ....x
        suffix(4) = " Million "
        suffix(5) = " Hundred " ' .....x
        suffix(6) = " Thousand "
        suffix(7) = " Hundred "
        suffix(8) = " "
        suffix(9) = ""

        For i = 0 To 9
            If numparts(i) <> "" Then
                Wordparts(i) = numparts(i) & suffix(i)
            End If

            T = Wordparts(0)

            If Wordparts(1) <> "" And Wordparts(2) = "" Then
                B = Wordparts(1) & " Billion "
            Else
                B = Wordparts(1) & Wordparts(2)
            End If

            If Wordparts(3) <> "" And Wordparts(4) = "" Then
                M = Wordparts(3) & " Million "
            Else
                M = Wordparts(3) & Wordparts(4)
            End If

            If Wordparts(5) <> "" And Wordparts(6) = "" Then

                TH = Wordparts(5) & " Thousand "
            Else
                TH = Wordparts(5) & Wordparts(6)
            End If

            H = Wordparts(7) & Wordparts(8)
            If Wordparts(9) = "" Then
                C = " and  Zero Cents "
            Else
                C = " and " & Wordparts(9) & " Cents "
            End If
        Next
        output = T & B & M & TH & H & C
        Return output


    End Function


    Public Shared Function primWord(ByVal Num As Integer) As String

        'This two dimensional array store the primary word convertion of numbers 0 to 99
        primWord = ""
        Dim wordList(,) As Object = {{1, "One"}, {2, "Two"}, {3, "Three"}, {4, "Four"}, {5, "Five"},
    {6, "Six "}, {7, "Seven "}, {8, "Eight "}, {9, "Nine "}, {10, "Ten "}, {11, "Eleven "}, {12, "Twelve "}, {13, "Thirteen "},
    {14, "Fourteen "}, {15, "Fifteen "}, {16, "Sixteen "}, {17, "Seventeen "}, {18, "Eighteen "}, {19, "Nineteen "},
    {20, "Twenty "}, {21, "Twenty One "}, {22, "Twenty Two"}, {23, "Twenty Three"}, {24, "Twenty Four"}, {25, "Twenty Five"},
    {26, "Twenty Six"}, {27, "Twenty Seven"}, {28, "Twenty Eight"}, {29, "Twenty Nine"}, {30, "Thirty "}, {31, "Thirty One "},
    {32, "Thirty Two"}, {33, "Thirty Three"}, {34, "Thirty Four"}, {35, "Thirty Five"}, {36, "Thirty Six"}, {37, "Thirty Seven"},
    {38, "Thirty Eight"}, {39, "Thirty Nine"}, {40, "Forty "}, {41, "Forty One "}, {42, "Forty Two"}, {43, "Forty Three"},
    {44, "Forty Four"}, {45, "Forty Five"}, {46, "Forty Six"}, {47, "Forty Seven"}, {48, "Forty Eight"}, {49, "Forty Nine"},
    {50, "Fifty "}, {51, "Fifty One "}, {52, "Fifty Two"}, {53, "Fifty Three"}, {54, "Fifty Four"}, {55, "Fifty Five"},
    {56, "Fifty Six"}, {57, "Fifty Seven"}, {58, "Fifty Eight"}, {59, "Fifty Nine"}, {60, "Sixty "}, {61, "Sixty One "},
    {62, "Sixty Two"}, {63, "Sixty Three"}, {64, "Sixty Four"}, {65, "Sixty Five"}, {66, "Sixty Six"}, {67, "Sixty Seven"}, {68, "Sixty Eight"},
    {69, "Sixty Nine"}, {70, "Seventy "}, {71, "Seventy One "}, {72, "Seventy Two"}, {73, "Seventy Three"}, {74, "Seventy Four"},
    {75, "Seventy Five"}, {76, "Seventy Six"}, {77, "Seventy Seven"}, {78, "Seventy Eight"}, {79, "Seventy Nine"},
    {80, "Eighty "}, {81, "Eighty One "}, {82, "Eighty Two"}, {83, "Eighty Three"}, {84, "Eighty Four"}, {85, "Eighty Five"},
    {86, "Eighty Six"}, {87, "Eighty Seven"}, {88, "Eighty Eight"}, {89, "Eighty Nine"}, {90, "Ninety "}, {91, "Ninety One "},
    {92, "Ninety Two"}, {93, "Ninety Three"}, {94, "Ninety Four"}, {95, "Ninety Five"}, {96, "Ninety Six"}, {97, "Ninety Seven"},
    {98, "Ninety Eight"}, {99, "Ninety Nine"}}

        Dim i As Integer
        For i = 0 To UBound(wordList)
            If Num = wordList(i, 0) Then
                primWord = wordList(i, 1)
                Exit For
            End If
        Next
        Return primWord
    End Function


    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click


        'frmPrintRecipt.MdiParent = frmMDImain
        frmPrintRecipt.Text = Trim(txtReciptID.Text)
        frmPrintRecipt.Show()
    End Sub



    Private Sub PrintRecipt(ByRef ReciptID As String)

        If Trim(txtReciptID.Text) = "" Then
            Exit Sub
        End If
        Try


        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"

        Dim path As String = globalVariables.crystalReportpath & "\Reports\rptKBOIRecipt.rpt"
        crystalReportpath = path
        'Dim manual report As New rptBank
        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo


        cryRpt.Load(path)

        'If Not txtBankID.Text = "" Then
        'cryRpt.DataDefinition.FormulaFields("BankID").Text = "'" & ReciptID & "'"
        cryRpt.RecordSelectionFormula = "{TBL_RECIPTS.RECIPT_ID} = " & ReciptID & " and {TBL_RECIPTS.COM_ID} = '" & globalVariables.selectedCompanyID & "'"
        'End If
        'globalFunctions.generateReportTemplate(cryRpt, Me.Text)



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

        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
            ' reportformObj.Show()
            'reportformObj.CrystalReportViewer1.PrintReport()

        'crLabel.PrintOptions.PrinterName = "<Label>"
            cryRpt.PrintToPrinter(1, False, 0, 0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtInvoiceNo_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceNo.TextChanged

    End Sub

    Private Sub txtPaymentAmount_TextChanged(sender As Object, e As EventArgs) Handles txtPaymentAmount.TextChanged

    End Sub
End Class