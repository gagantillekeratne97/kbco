Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Microsoft.Win32
Imports System.Text
Imports System.ComponentModel.Design
Public Class frmReciptMaster
    Dim srilankaTimeZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time")
    Dim utcNow As DateTime = DateTime.UtcNow
    Dim sriLankaTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, srilankaTimeZone)

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
    Public Async Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If Await Something() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'PrintRecipt(txtReciptID.Text)
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

    Private Async Function Something() As Threading.Tasks.Task(Of Boolean)
        Dim isSaved As Boolean = Await save()
        Return isSaved
    End Function

    Private Async Function save() As Threading.Tasks.Task(Of Boolean)
        '//save = False

        If Not canCreate Then
            Exit Function
        End If

        If isDataValid() = False Then
            Exit Function
        End If

        Dim IsEdit As Boolean = False
        Dim InvoiceList As String = ""
        Dim IsFirstSlectedRecord As Boolean = True
        Dim hasRecord As Boolean = False
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            dgGrid.EndEdit()
            dgGrid.CommitEdit(DataGridViewDataErrorContexts.Formatting)
            If isDataValid() = False Then
                Exit Function
            End If
            Dim NextReciptID As String = GetReciptID()
            txtReciptID.Text = NextReciptID
            Try
                For Each row As DataGridViewRow In dgGrid.Rows
                    If dgGrid.Rows(row.Index).Cells("CHECK").Value = True Then
                        hasRecord = True
                        If IsFirstSlectedRecord = True Then
                            InvoiceList = dgGrid.Rows(row.Index).Cells("INV_NO").Value
                            IsFirstSlectedRecord = False
                        Else
                            InvoiceList = InvoiceList + "," + dgGrid.Rows(row.Index).Cells("INV_NO").Value
                        End If
                    End If
                Next

                If hasRecord = False Then
                    MessageBox.Show("Please select invoices to be receipt.", "Invalid selection.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Function
                End If

                errorEvent = "Save"
                Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/receipts/addreceiptmaster"
                Dim utcNow As DateTime = DateTime.UtcNow

                ' Get the Sri Lanka time zone info
                Dim sriLankaTimeZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time")

                ' Convert current UTC time to Sri Lanka time
                Dim sriLankaTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, sriLankaTimeZone)

                Dim receiptDet As New List(Of ReceiptDetails)
                Dim receipt As New TBL_RECEIPT_MASTER With {
       .COM_ID = globalVariables.selectedCompanyID,
       .RECEIPT_ID = txtReciptID.Text,
       .AG_ID = DBNull.Value.ToString(),
       .AG_NAME = DBNull.Value.ToString(),
       .RECEIPT_DATE = sriLankaTime,
       .CUS_ID = txtCustomerID.Text,
       .CUS_NAME = txtCustomerName.Text,
       .PAY_TYPE = cmbReciptType.Text,
       .PAY_METHOD = cmbPaymentMethod.Text,
       .CHEQUE_NO = txtChequeNo.Text,
       .BANK_ID = txtBankID.Text,
       .BANK_NAME = lblBankName.Text,
       .RECEIVED_BY = txtRecivedBy.Text,
       .RECEIVED_BY_NAME = lblTechName.Text,
       .PAYMENT_AMOUNT = Convert.ToDecimal(txtPaymentAmount.Text),
       .ADV_PAYMENT = Convert.ToDecimal(txtAPAmount.Text),
       .PAY_BY_ADV_AMOUNT = cbAPUse.CheckState,
       .BF_PAYMENT = txtBFOutstanding.Text,
       .AMOUNT_IN_WORD = txtAmountInWords.Text,
       .REMARKS = txtRemarks.Text,
       .RECEIPT_TOTAL = Convert.ToDecimal(txtReciptTotal.Text),
       .IS_PRINTED = False,
       .CR_BY = userSession,
       .CR_DATE = sriLankaTime,
       .INVOICE_LIST = InvoiceList
   }
                For Each row As DataGridViewRow In dgGrid.Rows
                    If dgGrid.Rows(row.Index).Cells("CHECK").Value = True Then
                        Dim receiptDetails As New ReceiptDetails With {
                            .COM_ID = globalVariables.selectedCompanyID,
                            .RECIPT_ID = Trim(txtReciptID.Text),
                            .AG_ID = dgGrid.Rows(row.Index).Cells("AG_ID").Value.ToString(),
                            .AG_NAME = dgGrid.Rows(row.Index).Cells("AG_NAME").Value.ToString(),
                            .CUS_ID = txtCustomerID.Text,
                            .INV_NO = dgGrid.Rows(row.Index).Cells("INV_NO").Value.ToString(),
                            .IN_DATE = CDate(dgGrid.Rows(row.Index).Cells("INV_DATE").Value.ToString()),
                            .CUS_LOC = dgGrid.Rows(row.Index).Cells("INV_LOC").Value.ToString(),
                            .INV_AMOUNT = CDbl(dgGrid.Rows(row.Index).Cells("INV_VAL").Value.ToString()),
                            .PAYMENT_AMOUNT = CDbl(dgGrid.Rows(row.Index).Cells("PAY_VAL").Value.ToString()),
                            .BALANCE_PAYMENT = CDbl(dgGrid.Rows(row.Index).Cells("BAL").Value.ToString()),
                            .TECH_CODE = DBNull.Value.ToString(),
                            .REP_CODE = txtRecivedBy.Text
                            }
                        If dgGrid.Rows(row.Index).Cells("BAL").Value = 0 Then
                            receiptDetails.FULL_PAID = True
                        Else
                            receiptDetails.FULL_PAID = False
                        End If
                        receipt.ReceiptDetails.Add(receiptDetails)
                    End If
                Next
                Using client As New HttpClient()
                    Dim json As String = JsonConvert.SerializeObject(receipt)
                    Dim content As New StringContent(json, Encoding.UTF8, "application/json")
                    Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)
                    If response.IsSuccessStatusCode Then
                        Dim isSuccess As Boolean = Await response.Content.ReadAsStringAsync()
                        If isSuccess = True Then
                            MessageBox.Show("Receipt Successfully Saved.")
                        End If
                        Return isSuccess
                    Else
                        Return $"Error: {response.StatusCode.ToString()} - {Await response.Content.ReadAsStringAsync()}"
                        Return False
                    End If
                End Using

            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    'Private Function save() As Boolean
    '    save = False

    '    If Not canCreate Then
    '        Exit Function
    '    End If

    '    If isDataValid() = False Then
    '        Exit Function
    '    End If

    '    Dim IsEdit As Boolean = False
    '    Dim InvoiceList As String = ""
    '    Dim IsFirstSlectedRecord As Boolean = True
    '    Dim hasRecord As Boolean = False
    '    Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '    If conf = vbYes Then
    '        dgGrid.EndEdit()
    '        dgGrid.CommitEdit(DataGridViewDataErrorContexts.Formatting)
    '        If isDataValid() = False Then
    '            Exit Function
    '        End If
    '        Dim NextReciptID As String = GetReciptID()
    '        txtReciptID.Text = NextReciptID


    '        Try
    '            For Each row As DataGridViewRow In dgGrid.Rows
    '                If dgGrid.Rows(row.Index).Cells("CHECK").Value = True Then
    '                    hasRecord = True
    '                    If IsFirstSlectedRecord = True Then
    '                        InvoiceList = dgGrid.Rows(row.Index).Cells("INV_NO").Value
    '                        IsFirstSlectedRecord = False
    '                    Else
    '                        InvoiceList = InvoiceList + "," + dgGrid.Rows(row.Index).Cells("INV_NO").Value
    '                    End If
    '                End If


    '            Next

    '            connectionStaet()


    '            If hasRecord = False Then
    '                MessageBox.Show("Please select invoices to be receipt.", "Invalid selection.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Exit Function
    '            End If

    '            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction


    '            errorEvent = "Save"
    '            'strSQL = "SELECT CASE WHEN EXISTS (SELECT     RECIPT_ID FROM       TBL_RECIPT_MASTER WHERE     (COM_ID = '"& globalVariables.selectedCompanyID &"') AND (RECIPT_ID = '"&  &"')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '            'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '            'If dbConnections.sqlCommand.ExecuteScalar Then
    '            '    IsEdit = True
    '            '    strSQL = "UPDATE    TBL_METER_READING_MASTER SET     INV_ADD1 =@INV_ADD1, INV_ADD2 =@INV_ADD2, INV_ADD3 =@INV_ADD3, IS_NBT=@IS_NBT ,IS_VAT=@IS_VAT, RENTAL_VAL=@RENTAL_VAL WHERE     (COM_ID = @COM_ID) AND (PERIOD_START =@PERIOD_START) AND (PERIOD_END =@PERIOD_END) AND (CUS_ID = @CUS_ID) AND (AG_ID =@AG_ID)"
    '            'Else
    '            '    IsEdit = False
    '            '    strSQL = "INSERT INTO TBL_METER_READING_MASTER (COM_ID, MR_ID, CUS_ID, AG_ID, PERIOD_START, PERIOD_END, INV_ADD1, INV_ADD2, INV_ADD3, CR_BY, CR_DATE,IS_NBT ,IS_VAT,RENTAL_VAL) VALUES     (@COM_ID, @MR_ID, @CUS_ID, @AG_ID, @PERIOD_START, @PERIOD_END, @INV_ADD1, @INV_ADD2, @INV_ADD3, '" & userSession & "', GETDATE(),@IS_NBT ,@IS_VAT,@RENTAL_VAL)"
    '            'End If

    '            'If IsEdit = True Then
    '            '    If Not canModify Then
    '            '        Exit Function
    '            '    End If
    '            'End If


    '            strSQL = "INSERT INTO TBL_RECIPT_MASTER (COM_ID, RECIPT_ID, AG_ID, AG_NAME, RECIPT_DATE, CUS_ID, CUS_NAME, PAY_TYPE, PAY_METHOD, CHEQUE_NO, BANK_ID, BANK_NAME, RECIVED_BY, RECIVED_BY_NAME, PAYMENT_AMOUNT, ADV_PAYMENT, PAY_BY_ADV_AMOUNT, BF_PAYMENT, AMOUNT_IN_WORD, REMARKS, RECIPT_TOTAL, IS_PRINTED, CR_BY, CR_DATE,INVOICE_LIST) VALUES     (@COM_ID, @RECIPT_ID, @AG_ID, @AG_NAME,GETDATE() , @CUS_ID, @CUS_NAME, @PAY_TYPE, @PAY_METHOD, @CHEQUE_NO, @BANK_ID, @BANK_NAME, @RECIVED_BY, @RECIVED_BY_NAME, @PAYMENT_AMOUNT, @ADV_PAYMENT, @PAY_BY_ADV_AMOUNT, @BF_PAYMENT, @AMOUNT_IN_WORD, @REMARKS, @RECIPT_TOTAL, @IS_PRINTED, '" & userSession & "', GETDATE(),@INVOICE_LIST)"

    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_ID", Trim(txtReciptID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", DBNull.Value)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_NAME", DBNull.Value)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_NAME", Trim(txtCustomerName.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@PAY_TYPE", Trim(cmbReciptType.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@PAY_METHOD", Trim(cmbPaymentMethod.Text))
    '            If Trim(txtChequeNo.Text) = "" Then
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value)
    '            Else
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", Trim(txtChequeNo.Text))
    '            End If
    '            If Trim(txtBankID.Text) = "" Then
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_ID", DBNull.Value)
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_NAME", DBNull.Value)
    '            Else
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_ID", Trim(txtBankID.Text))
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_NAME", Trim(lblBankName.Text))
    '            End If

    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RECIVED_BY", Trim(txtRecivedBy.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RECIVED_BY_NAME", Trim(lblTechName.Text))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@PAYMENT_AMOUNT", CDbl(Trim(txtPaymentAmount.Text.Replace(",", ""))))
    '            If Trim(txtAPAmount.Text) = "" Then
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@ADV_PAYMENT", 0)
    '            Else
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@ADV_PAYMENT", CDbl(Trim(txtAPAmount.Text)))
    '            End If

    '            dbConnections.sqlCommand.Parameters.AddWithValue("@PAY_BY_ADV_AMOUNT", cbAPUse.CheckState)
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@BF_PAYMENT", CDbl(Trim(txtBFOutstanding.Text.Replace(",", ""))))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@AMOUNT_IN_WORD", Trim(txtAmountInWords.Text))
    '            If Trim(txtRemarks.Text) = "" Then
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@REMARKS", DBNull.Value)
    '            Else
    '                dbConnections.sqlCommand.Parameters.AddWithValue("@REMARKS", Trim(txtRemarks.Text))
    '            End If

    '            dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_TOTAL", CDbl(Trim(txtReciptTotal.Text.Replace(",", ""))))
    '            dbConnections.sqlCommand.Parameters.AddWithValue("@IS_PRINTED", False)

    '            dbConnections.sqlCommand.Parameters.AddWithValue("@INVOICE_LIST", Trim(InvoiceList))

    '            If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

    '            If save = False Then
    '                Exit Function
    '            End If

    '            InvoiceList = ""
    '            For Each row As DataGridViewRow In dgGrid.Rows

    '                If dgGrid.Rows(row.Index).Cells("CHECK").Value = True Then
    '                    dbConnections.sqlCommand.Parameters.Clear()

    '                    strSQL = "INSERT INTO TBL_RECIPT_DET (COM_ID, RECIPT_ID, AG_ID, AG_NAME, CUS_ID, INV_NO, INV_DATE, CUS_LOC, INV_AMOUNT, PAYMENT_AMOUNT, BALANCE_PAYMENT, TECH_CODE, REP_CODE, FULL_PAID) VALUES     (@COM_ID, @RECIPT_ID, @AG_ID, @AG_NAME, @CUS_ID, @INV_NO, @INV_DATE, @CUS_LOC, @INV_AMOUNT, @PAYMENT_AMOUNT, @BALANCE_PAYMENT, @TECH_CODE, @REP_CODE, @FULL_PAID)"
    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_ID", Trim(txtReciptID.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", dgGrid.Rows(row.Index).Cells("AG_ID").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@AG_NAME", dgGrid.Rows(row.Index).Cells("AG_NAME").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", dgGrid.Rows(row.Index).Cells("INV_NO").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_DATE", CDate(dgGrid.Rows(row.Index).Cells("INV_DATE").Value))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_LOC", dgGrid.Rows(row.Index).Cells("INV_LOC").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_AMOUNT", CDbl(dgGrid.Rows(row.Index).Cells("INV_VAL").Value))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@PAYMENT_AMOUNT", CDbl(dgGrid.Rows(row.Index).Cells("PAY_VAL").Value))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@BALANCE_PAYMENT", CDbl(dgGrid.Rows(row.Index).Cells("BAL").Value))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", DBNull.Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@REP_CODE", Trim(txtRecivedBy.Text))
    '                    If dgGrid.Rows(row.Index).Cells("BAL").Value = 0 Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", True)
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", False)
    '                    End If



    '                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


    '                    If save = False Then
    '                        Exit Function
    '                    End If


    '                    strSQL = "UPDATE    TBL_INVOICE_MASTER SET  RECIPT_ID =@RECIPT_ID , FULL_PAID=@FULL_PAID, INV_STATUS_T = @INV_STATUS_T WHERE     (COM_ID = @COM_ID) AND (INV_NO = @INV_NO)"
    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", dgGrid.Rows(row.Index).Cells("INV_NO").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_ID", Trim(txtReciptID.Text))
    '                    If dgGrid.Rows(row.Index).Cells("BAL").Value = 0 Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", True)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS_T", "RECIPTED")
    '                    ElseIf dgGrid.Rows(row.Index).Cells("BAL").Value < 0 Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", True)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS_T", "OVERPAID")
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", False)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS_T", "PARTLY PAID")
    '                    End If

    '                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


    '                    If save = False Then
    '                        strSQL = "UPDATE    TBL_PREV_INV_HISTORY SET              RECIPT_ID =@RECIPT_ID, FULL_PAID =@FULL_PAID WHERE     (COM_ID =@COM_ID) AND (INV_NO = @INV_NO)"
    '                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", dgGrid.Rows(row.Index).Cells("INV_NO").Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@RECIPT_ID", Trim(txtReciptID.Text))
    '                        If dgGrid.Rows(row.Index).Cells("BAL").Value = 0 Then
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", True)
    '                        Else
    '                            dbConnections.sqlCommand.Parameters.AddWithValue("@FULL_PAID", False)
    '                        End If

    '                        If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
    '                    End If



    '                    If save = False Then
    '                        Exit Function
    '                    End If

    '                    strSQL = "SELECT CASE WHEN EXISTS (SELECT     CUS_ID FROM  TBL_CUS_DEBTORS WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    If dbConnections.sqlCommand.ExecuteScalar Then
    '                        IsEdit = True
    '                        strSQL = "UPDATE    TBL_CUS_DEBTORS SET               LAST_UPDATE_DATE = GETDATE(), LAST_PAYMENT_AMOUNT = @LAST_PAYMENT_AMOUNT, LAST_RECIPT_ID = @LAST_RECIPT_ID,  DEBTORS_OUTSTANDING = @DEBTORS_OUTSTANDING, ADV_PAYMENT = @ADV_PAYMENT, BF_DEBTORS = @BF_DEBTORS, RECIVED_BY = @RECIVED_BY,  LAST_PAY_METHOD = @LAST_PAY_METHOD, CHEQUE_NO = @CHEQUE_NO, BANK_ID = @BANK_ID, BANK_NANE = @BANK_NANE WHERE COM_ID = @COM_ID and  CUS_ID = @CUS_ID"
    '                    Else
    '                        IsEdit = False
    '                        strSQL = "INSERT INTO TBL_CUS_DEBTORS (COM_ID, CUS_ID, LAST_UPDATE_DATE, LAST_PAYMENT_AMOUNT, LAST_RECIPT_ID, DEBTORS_OUTSTANDING, ADV_PAYMENT, BF_DEBTORS, RECIVED_BY, LAST_PAY_METHOD, CHEQUE_NO, BANK_ID, BANK_NANE) VALUES     (@COM_ID, @CUS_ID, GETDATE(), @LAST_PAYMENT_AMOUNT, @LAST_RECIPT_ID, @DEBTORS_OUTSTANDING, @ADV_PAYMENT, @BF_DEBTORS, @RECIVED_BY, @LAST_PAY_METHOD, @CHEQUE_NO, @BANK_ID, @BANK_NANE)"
    '                    End If

    '                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", dgGrid.Rows(row.Index).Cells("INV_NO").Value)
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@LAST_RECIPT_ID", Trim(txtReciptID.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@LAST_PAYMENT_AMOUNT", CDbl(dgGrid.Rows(row.Index).Cells("PAY_VAL").Value))
    '                    If Trim(txtAPAmount.Text) = "" Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@ADV_PAYMENT", 0)
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@ADV_PAYMENT", CDbl(Trim(txtAPAmount.Text)))
    '                    End If


    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@DEBTORS_OUTSTANDING", CDbl(Trim(txtOutStanding.Text)))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@BF_DEBTORS", CDbl(Trim(txtBalanceTotal.Text.Replace(",", ""))))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@RECIVED_BY", Trim(txtRecivedBy.Text))
    '                    dbConnections.sqlCommand.Parameters.AddWithValue("@LAST_PAY_METHOD", Trim(cmbPaymentMethod.Text))
    '                    If Trim(txtChequeNo.Text) = "" Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", DBNull.Value)
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@CHEQUE_NO", Trim(txtChequeNo.Text))
    '                    End If
    '                    If Trim(txtBankID.Text) = "" Then
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_ID", DBNull.Value)
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_NANE", DBNull.Value)
    '                    Else
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_ID", Trim(txtBankID.Text))
    '                        dbConnections.sqlCommand.Parameters.AddWithValue("@BANK_NANE", Trim(lblBankName.Text))
    '                    End If

    '                    If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


    '                    If save = False Then
    '                        Exit Function
    '                    End If

    '                End If


    '            Next

    '            If save = True Then
    '                dbConnections.sqlTransaction.Commit()
    '            Else
    '                dbConnections.sqlTransaction.Rollback()
    '            End If






    '            MessageBox.Show("Transaction Saved Successfully.", "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        Catch ex As Exception
    '            dbConnections.sqlTransaction.Rollback()
    '            inputErrorLog(Me.Text, "" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '        Finally
    '            dbConnections.dReader.Close()
    '            connectionClose()

    '        End Try
    '    End If

    '    Return save
    'End Function




    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False


        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
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
        globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()

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

    Private Async Sub Load_Cus_Debtors_info()
        Dim Debtors_Out As Double = 0
        Dim Adv_Payment As Double = 0
        Dim BF_Out As Double = 0

        Dim companyID As String = globalVariables.selectedCompanyID
        Dim customerID As String = txtCustomerID.Text.Trim()

        Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/receipts/loadcustomerdebtorsinfor?companyID={companyID}&customerID={customerID}"
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                Dim rowCount As Integer = 0
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim data As List(Of CustomerDebtorsInfo) = JsonConvert.DeserializeObject(Of List(Of CustomerDebtorsInfo))(json)
                    For Each item As CustomerDebtorsInfo In data
                        Debtors_Out = item.DebtorsOutstanding
                        Adv_Payment = item.advancePayment
                        BF_Out = item.BFOut
                    Next
                    txtBFOutstanding.Text = BF_Out.ToString("N2")
                    txtAPAmount.Text = Adv_Payment.ToString("N2")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
    End Sub

    'Private Sub Load_Cus_Debtors_info()
    '    Dim Debtors_Out As Double = 0.0
    '    Dim Adv_Payment As Double = 0.0
    '    Dim BF_Out As Double = 0.0
    '    Dim ishasRecord As Boolean = False
    '    Try
    '        strSQL = "SELECT   TOP 1   DEBTORS_OUTSTANDING, ADV_PAYMENT, BF_DEBTORS FROM  TBL_CUS_DEBTORS WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID) ORDER BY LAST_UPDATE_DATE"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            ishasRecord = True
    '            If IsDBNull(dbConnections.dReader.Item("DEBTORS_OUTSTANDING")) Then
    '                Debtors_Out = 0
    '            Else
    '                Debtors_Out = dbConnections.dReader.Item("DEBTORS_OUTSTANDING")
    '            End If

    '            If IsDBNull(dbConnections.dReader.Item("ADV_PAYMENT")) Then
    '                Adv_Payment = 0
    '            Else
    '                Adv_Payment = dbConnections.dReader.Item("ADV_PAYMENT")
    '            End If


    '            If IsDBNull(dbConnections.dReader.Item("BF_DEBTORS")) Then
    '                BF_Out = 0
    '            Else
    '                BF_Out = dbConnections.dReader.Item("BF_DEBTORS")
    '            End If

    '        End While
    '        dbConnections.dReader.Close()

    '        If ishasRecord = False Then
    '            Debtors_Out = 0
    '            Adv_Payment = 0
    '            BF_Out = 0
    '        End If

    '        txtAPAmount.Text = Adv_Payment.ToString("N2")
    '        txtBFOutstanding.Text = BF_Out.ToString("N2")
    '    Catch ex As Exception
    '        dbConnections.dReader.Close()
    '        MsgBox(ex.InnerException.Message)
    '    End Try
    'End Sub

    Private Function GetLastBalance(ByRef InvNo As String, ByRef PreviousBalance As Decimal) As Decimal
        GetLastBalance = PreviousBalance

        Try



            'strSQL = "SELECT CASE WHEN EXISTS (SELECT     TOP (1) BALANCE_PAYMENT FROM         TBL_RECIPT_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND INV_NO = '" & Trim(InvNo) & "' ORDER BY RECIPT_ID DESC) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            strSQL = "SELECT CASE WHEN EXISTS (SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_RECIPT_DET.CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (TBL_RECIPT_DET.INV_NO = '" & Trim(InvNo) & "') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            If dbConnections.sqlCommand.ExecuteScalar = True Then
                strSQL = "SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_RECIPT_DET.CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (TBL_RECIPT_DET.INV_NO = '" & Trim(InvNo) & "') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                GetLastBalance = dbConnections.sqlCommand.ExecuteScalar
            Else
                GetLastBalance = PreviousBalance
            End If

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        Return GetLastBalance
    End Function


    Private Function GetVATP(ByRef InvNo As String) As Integer
        GetVATP = 0

        Try
            strSQL = "SELECT     VAT_P FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (INV_NO = '" & InvNo & "')"
            dbConnections.sqlCommand.CommandText = strSQL

            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GetVATP = 8
            Else
                GetVATP = dbConnections.sqlCommand.ExecuteScalar
            End If


            dbConnections.dReader.Close()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        Return GetVATP
    End Function


    Private Function GetNBTP(ByRef InvNo As String) As Double
        GetNBTP = 0

        Try
            strSQL = "SELECT     NBT2_P FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (INV_NO = '" & InvNo & "')"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.sqlCommand.CommandType = CommandType.Text
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GetNBTP = 0
            Else
                GetNBTP = dbConnections.sqlCommand.ExecuteScalar
            End If


            dbConnections.dReader.Close()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        Return GetNBTP
    End Function
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False


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
            If generalValObj.isPresent(txtBankID) = False Then
                Exit Function
            End If


        End If
        If generalValObj.isPresent(txtRecivedBy) = False Then
            Exit Function
        End If

        If generalValObj.isPresent(txtOutStanding) = False Then
            Exit Function
        End If

        If CDbl(Trim(txtOutStanding.Text)) <> 0 Then
            MessageBox.Show("Payment amount not matched.", "Wrong Amount.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        End If

        For Each row As DataGridViewRow In dgGrid.Rows
            'dgMR.Rows(row.Index).Cells("END_MR").Value
            If dgGrid.Rows(row.Index).Cells("CHECK").Value = True Then
                If dgGrid.Rows(row.Index).Cells("CHECK").Value = 0 Then
                    MessageBox.Show("Please enter payment value.", "Invalid Attempt.", MessageBoxButtons.OK)
                    Exit Function
                End If

                If IsDBNull(dgGrid.Rows(row.Index).Cells("PAY_VAL").Value) Then
                    MessageBox.Show("Please enter payment value.", "Invalid Attempt.", MessageBoxButtons.OK)
                    Exit Function
                End If

                If dgGrid.Rows(row.Index).Cells("PAY_VAL").Value = "" Then
                    MessageBox.Show("Please enter payment value.", "Invalid Attempt.", MessageBoxButtons.OK)
                    Exit Function
                End If

            End If

        Next

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()

        lblReciptDate.Text = Today.Date.ToShortDateString

        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        cmbReciptType.SelectedIndex = 0
        txtPaymentAmount.Text = ""
        txtAmountInWords.Text = ""
        cmbPaymentMethod.SelectedIndex = 0
        txtChequeNo.Text = ""
        txtBankID.Text = ""
        lblBankName.Text = ""
        txtRecivedBy.Text = ""
        lblTechName.Text = ""
        txtPaymentAmount.Text = ""
        txtAPAmount.Text = ""
        txtBFOutstanding.Text = ""
        txtRemarks.Text = ""
        txtFind.Text = ""
        txtFindIncoice.Text = ""
        txtReciptTotal.Text = ""
        txtBalanceTotal.Text = ""
        txtOutStanding.Text = ""
        dgGrid.Rows.Clear()

        IsEnable(True)


        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()
    End Sub


    Private Sub IsEnable(ByRef enableState As Boolean)



        txtCustomerID.Enabled = enableState
        txtCustomerName.Enabled = enableState
        cmbReciptType.Enabled = enableState
        txtPaymentAmount.Enabled = enableState
        txtAmountInWords.Enabled = enableState
        cmbPaymentMethod.Enabled = enableState
        txtChequeNo.Enabled = enableState
        txtBankID.Enabled = enableState
        txtRecivedBy.Enabled = enableState
    End Sub


    Function NumberToText(ByVal n As Integer) As String

        Select Case n
            Case 0
                Return ""

            Case 1 To 19
                Dim arr() As String = {"One", "Two", "Three", "Four", "Five", "Six", "Seven",
                  "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen",
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
                Return "One Billion " & NumberToText(n Mod 1000000000)

            Case Else
                Return NumberToText(n \ 1000000000) & "Billion " _
                  & NumberToText(n Mod 1000000000)
        End Select
    End Function

    Private Function GetReciptID() As String
        GetReciptID = ""
        errorEvent = "Generating Recipt ID"
        connectionStaet()

        Try
            Dim cmd As New SqlCommand("GenerateReciptID", dbConnections.sqlConnection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 30 ' Optional: increase if needed

            cmd.Parameters.AddWithValue("@CompanyID", globalVariables.selectedCompanyID)

            Dim outputParam As New SqlParameter("@NextReciptID", SqlDbType.VarChar, 100)
            outputParam.Direction = ParameterDirection.Output
            cmd.Parameters.Add(outputParam)

            cmd.ExecuteNonQuery()
            GetReciptID = outputParam.Value.ToString()

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X10) " & GenaralErrorMessage & ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X10", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
        End Try

        Return GetReciptID
    End Function


    'Private Function GetReciptID() As String

    '    GetReciptID = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()


    '    Try
    '        strSQL = "SELECT MAX(RECIPT_ID) AS Expr1 FROM TBL_RECIPT_MASTER WHERE (COM_ID = '" & globalVariables.selectedCompanyID & "')"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            GetReciptID = globalVariables.selectedCompanyID & "/" & "REC" & "/" & 1
    '        Else
    '            Dim IRCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim IRID As Integer = 0
    '            IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            IRID = IRCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT     RECIPT_ID  FROM         TBL_RECIPT_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (RECIPT_ID = '" & globalVariables.selectedCompanyID & "/REC/" & IRID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    IRID = IRID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                GetReciptID = IRCodeSplit(0) & "/REC/" & IRID
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
    '    Return GetReciptID
    'End Function

    Private Sub load_Cus_info()
        dgGrid.DataSource = Nothing
        errorEvent = "Reading information"
        Dim hasRecord As Boolean = False
        'dgGrid.Rows.Clear()
        connectionStaet()

        Try
            '--- Get customer name from previous history
            dbConnections.sqlCommand = New SqlCommand("usp_GetCustomerNameFromPrevHistory", dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader()

            While dbConnections.dReader.Read()
                txtCustomerName.Text = dbConnections.dReader("CUS_NAME").ToString()
            End While
            dbConnections.dReader.Close()

            '--- Load previous invoice history
            dbConnections.sqlCommand = New SqlCommand("usp_GetPrevInvoiceDetails", dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

            Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
            Dim ds As New DataSet()
            da.Fill(ds)

            Dim InvList As New List(Of String)()
            For Each row As DataRow In ds.Tables(0).Rows
                If row("CUS_ID").ToString().Trim() = txtCustomerID.Text.Trim() Then
                    Dim invVal As Decimal = row.Field(Of Decimal)("INV_VAL")
                    Dim finalVal As Decimal = GetLastBalance(row("INV_NO").ToString(), invVal)

                    Dim invAdd As String = If(IsDBNull(row("CUS_ADD1")), "", row("CUS_ADD1")) &
                                       If(IsDBNull(row("CUS_ADD2")), "", " " & row("CUS_ADD2"))

                    populatreDatagrid(row("INV_NO"), row("INV_DATE"), "N/A", "N/A", invAdd, finalVal.ToString("0.00"), "0.00", False, "0.00")

                    txtRecivedBy.Text = row("REP_CODE").ToString()
                    hasRecord = True
                End If
            Next

            '--- Get VAT info
            dbConnections.sqlCommand = New SqlCommand("usp_GetCustomerVATInfo", dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader()

            While dbConnections.dReader.Read()
                txtCustomerName.Text = dbConnections.dReader("CUS_NAME").ToString()
            End While
            dbConnections.dReader.Close()

            '--- Load unpaid invoices
            dbConnections.sqlCommand = New SqlCommand("usp_GetUnpaidInvoices", dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))

            da = New SqlDataAdapter(dbConnections.sqlCommand)
            ds.Clear()
            da.Fill(ds)

            ' Prepare DataTable once
            Dim dataTable As New DataTable()
            With dataTable.Columns
                .Add("INV_NO")
                .Add("INV_DATE")
                .Add("AG_ID")
                .Add("AG_NAME")
                .Add("INV_LOC")
                .Add("INV_VAL", GetType(Double))
                .Add("PAY_VAL", GetType(Double))
                .Add("CHECK", GetType(Boolean))
                .Add("BAL", GetType(Double))
            End With

            ' Fill DataTable in loop
            For Each row As DataRow In ds.Tables(0).Rows
                Dim invNo = row("INV_NO").ToString()
                If Not InvList.Contains(invNo) Then
                    InvList.Add(invNo)

                    Dim invVal As Decimal = row.Field(Of Decimal)("INV_VAL")
                    Dim isNBT As Boolean = row.Field(Of Boolean)("IS_NBT")
                    Dim isVAT As Boolean = row.Field(Of Boolean)("IS_VAT")

                    Dim nbtValue As Double = GetNBTP(invNo)
                    Dim vatValue As Double = GetVATP(invNo)

                    If isNBT Then invVal += (invVal / 100) * nbtValue
                    If isVAT Then invVal += (invVal / 100) * vatValue

                    Dim finalVal = GetLastBalance(invNo, invVal)
                    Dim invAdd3 = row("INV_ADD3").ToString()
                    Dim invDate = row("INV_DATE")
                    Dim agID = row("AG_ID").ToString()

                    dataTable.Rows.Add(invNo, invDate, agID, agID, invAdd3, finalVal, 0.0, False, 0.0)
                End If
            Next

            ' Finally bind the whole DataTable to grid            
            dgGrid.DataSource = dataTable

            '--- Get technician name
            dbConnections.sqlCommand = New SqlCommand("usp_GetTechName", dbConnections.sqlConnection)
            dbConnections.sqlCommand.CommandType = CommandType.StoredProcedure
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtRecivedBy.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader()

            If dbConnections.dReader.Read() Then
                lblTechName.Text = If(IsDBNull(dbConnections.dReader("TECH_NAME")), "ERROR", dbConnections.dReader("TECH_NAME").ToString())
            End If
            dbConnections.dReader.Close()

            Load_Cus_Debtors_info()
            Calculate_Balance()

            If hasRecord Then
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

    '/This function is used to get the customer information by using the web api link 

    'Private Async Sub load_Cus_info()
    '    Dim hasRecord As Boolean = False
    '    'Dim apiUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/receipts/loadcustomerinformation?companyID={globalVariables.selectedCompanyID}&customerID={txtCustomerID.Text.Trim()}"
    '    Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/receipts/loadcustomerinformation?companyID={globalVariables.selectedCompanyID}&customerID={txtCustomerID.Text.Trim()}"
    '    Using client As New HttpClient()
    '        Try
    '            If txtCustomerID.Text IsNot "" Then
    '                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
    '                Dim rowCount As Integer = 0

    '                If response.IsSuccessStatusCode Then
    '                    Dim json As String = Await response.Content.ReadAsStringAsync()
    '                    Dim data As List(Of ReceiptMasterVM) = JsonConvert.DeserializeObject(Of List(Of ReceiptMasterVM))(json)
    '                    dgGrid.Rows.Clear()
    '                    For Each item As ReceiptMasterVM In data
    '                        hasRecord = True
    '                        txtCustomerName.Text = item.CustomerName
    '                        dgGrid.Rows.Add(item.InvoiceNo, item.InvoiceDate, item.AG_ID, item.AG_NAME, item.Location, item.InvoiceValue, item.PaymentValue, item.Check, item.Bal)
    '                    Next

    '                    Load_Cus_Debtors_info()
    '                    Calculate_Balance()

    '                    If hasRecord Then
    '                        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
    '                        Me.saveBtnStatus()
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End Using
    'End Sub

    'Private Sub load_Cus_info()
    '    errorEvent = "Reading information"
    '    Dim hasRecord As Boolean = False
    '    dgGrid.Rows.Clear()
    '    connectionStaet()
    '    Try
    '        '\\ loading previous history

    '        strSQL = "SELECT    CUS_NAME FROM         TBL_PREV_INV_HISTORY WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (FULL_PAID = 0)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
    '        End While
    '        dbConnections.dReader.Close()


    '        'strSQL = "SELECT      AG_ID, INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE, IS_NBT, IS_VAT, RENTAL_VAL, INV_VAL,INV_ADD3 FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (RECIPT_ID IS NULL) OR (FULL_PAID = 0) ORDER BY INV_AUTO_NUM"
    '        strSQL = "SELECT DISTINCT TBL_PREV_INV_HISTORY.INV_DATE, TBL_PREV_INV_HISTORY.INV_NO, TBL_PREV_INV_HISTORY.REP_CODE, TBL_PREV_INV_HISTORY.REP_NAME, TBL_PREV_INV_HISTORY.DEPT, TBL_PREV_INV_HISTORY.CUS_ID, TBL_PREV_INV_HISTORY.CUS_NAME, TBL_PREV_INV_HISTORY.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_ADD1, MTBL_CUSTOMER_MASTER.CUS_ADD2 FROM         TBL_PREV_INV_HISTORY LEFT OUTER JOIN MTBL_CUSTOMER_MASTER ON TBL_PREV_INV_HISTORY.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_PREV_INV_HISTORY.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID  WHERE      (TBL_PREV_INV_HISTORY.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_PREV_INV_HISTORY.CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (TBL_PREV_INV_HISTORY.RECIPT_ID IS NULL) OR (TBL_PREV_INV_HISTORY.FULL_PAID = 0) ORDER BY INV_DATE"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
    '        Dim ds As New DataSet()
    '        Dim Inv_Value_Previous As Decimal = 0
    '        Dim InvAdd As String = ""
    '        da.Fill(ds)

    '        'fetching data from dataset in disconnected mode
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            hasRecord = True
    '            'MsgBox(ds.Tables(0).Rows(i).Item(0))
    '            If Trim(txtCustomerID.Text) = ds.Tables(0).Rows(i).Item("CUS_ID") Then



    '                Inv_Value_Previous = 0
    '                Inv_Value_Previous = GetLastBalance(ds.Tables(0).Rows(i).Item(1), ds.Tables(0).Rows(i).Item(7))

    '                InvAdd = ""
    '                If IsDBNull(ds.Tables(0).Rows(i).Item(8)) Then
    '                    InvAdd = ""
    '                Else
    '                    InvAdd = ds.Tables(0).Rows(i).Item(8)
    '                End If

    '                If IsDBNull(ds.Tables(0).Rows(i).Item(9)) Then
    '                    InvAdd = InvAdd + ""
    '                Else
    '                    InvAdd = InvAdd + " " + ds.Tables(0).Rows(i).Item(9)
    '                End If

    '                '                       Invoice No ,                           Invoice Date,Agreement ID,         Agreement Name   ,                    Inv Add 3 ,                                 Inv value            , Payment, Check, Balance            
    '                populatreDatagrid(ds.Tables(0).Rows(i).Item(1), ds.Tables(0).Rows(i).Item(0), "N/A", "N/A", InvAdd, Format(Inv_Value_Previous, "0.00"), "0.00", False, "0.00")

    '                txtRecivedBy.Text = ds.Tables(0).Rows(i).Item(2)
    '            End If
    '        Next


    '        '\\ loading system history 
    '        strSQL = "SELECT MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
    '        End While
    '        dbConnections.dReader.Close()


    '        strSQL = "SELECT  AG_ID, INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE, IS_NBT, IS_VAT, RENTAL_VAL, INV_VAL,INV_ADD3,REP_CODE,CUS_ID,COM_ID FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND INV_STATUS_T is null AND (CUS_ID = '" & Trim(txtCustomerID.Text) & "') AND (RECIPT_ID IS NULL) OR (FULL_PAID = 0) ORDER BY INV_AUTO_NUM"


    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        da = Nothing
    '        ds.Clear()
    '        da = New SqlDataAdapter(dbConnections.sqlCommand)
    '        ds = New DataSet()
    '        Inv_Value_Previous = 0
    '        da.Fill(ds)

    '        Dim Invoicevalue As Double = 0
    '        Dim IsNBT As Boolean = False
    '        Dim IsVat As Boolean = False
    '        Dim InvList As New List(Of String)

    '        'fetching data from dataset in disconnected mode
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            hasRecord = True
    '            'MsgBox(ds.Tables(0).Rows(i).Item(0))
    '            If ds.Tables(0).Rows(i).Item(3) = "001/INV/1041" Then
    '                Dim x As String = ds.Tables(0).Rows(i).Item("CUS_ID")
    '            End If

    '            If Not InvList.Contains(ds.Tables(0).Rows(i).Item(3)) Then
    '                InvList.Add(ds.Tables(0).Rows(i).Item(3))




    '                If Trim(txtCustomerID.Text) = ds.Tables(0).Rows(i).Item("CUS_ID") Then
    '                    Invoicevalue = 0
    '                    IsNBT = False
    '                    IsVat = False
    '                    IsNBT = ds.Tables(0).Rows(i).Item(5)
    '                    IsVat = ds.Tables(0).Rows(i).Item(6)
    '                    Invoicevalue = ds.Tables(0).Rows(i).Item(8)

    '                    If IsNBT = True Then
    '                        Invoicevalue = Invoicevalue + ((Invoicevalue / 100) * GetNBTP(ds.Tables(0).Rows(i).Item(3)))
    '                    End If

    '                    If IsVat = True Then
    '                        Invoicevalue = Invoicevalue + ((Invoicevalue / 100) * GetVATP(ds.Tables(0).Rows(i).Item(3)))
    '                    End If


    '                    Inv_Value_Previous = 0
    '                    Inv_Value_Previous = GetLastBalance(ds.Tables(0).Rows(i).Item(3), Invoicevalue)





    '                    If ds.Tables(0).Rows(i).Item("COM_ID") = "003" Then

    '                        '                       Invoice No ,                           Invoice Date,                  Agreement ID,         Agreement Name   ,                    Inv Add 3 ,                                 Inv value            , Payment, Check, Balance            
    '                        populatreDatagrid(ds.Tables(0).Rows(i).Item(3), ds.Tables(0).Rows(i).Item(4), ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(9), Format(Inv_Value_Previous, "0.00"), "0.00", False, "0.00")
    '                    Else

    '                        If CDate(ds.Tables(0).Rows(i).Item(4)).Year = 2019 And CDate(ds.Tables(0).Rows(i).Item(4)).Month = 6 Then

    '                        Else

    '                            '                       Invoice No ,                           Invoice Date,                  Agreement ID,         Agreement Name   ,                    Inv Add 3 ,                                 Inv value            , Payment, Check, Balance            
    '                            populatreDatagrid(ds.Tables(0).Rows(i).Item(3), ds.Tables(0).Rows(i).Item(4), ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(9), Format(Inv_Value_Previous, "0.00"), "0.00", False, "0.00")
    '                        End If

    '                    End If


    '                    If hasRecord = False Then
    '                        txtRecivedBy.Text = ds.Tables(0).Rows(i).Item(10)
    '                    End If
    '                End If
    '            End If
    '        Next

    '        strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE) AND (TECH_ACTIVE = 1)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtRecivedBy.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


    '        While dbConnections.dReader.Read
    '            If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
    '                lblTechName.Text = "ERROR"
    '            Else
    '                lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
    '            End If


    '        End While
    '        dbConnections.dReader.Close()






    '        Load_Cus_Debtors_info()
    '        Calculate_Balance()

    '        If hasRecord Then
    '            globalFunctions.globalButtonActivation(True, True, False, False, False, False)
    '            Me.saveBtnStatus()
    '        End If


    '    Catch ex As Exception
    '        MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '    Finally
    '        dbConnections.dReader.Close()
    '        connectionClose()
    '    End Try
    'End Sub




#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"
    Private Sub txtCustomerID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub


    Private Sub txtCustomerID_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCustomerID.Validating
        load_Cus_info()
    End Sub


    Private Sub populatreDatagrid(ByRef InvoiceNo As String, ByRef InvoiceDate As String, ByRef AG_ID As String, ByRef AG_Name As String, ByRef Location As String, ByRef InvoiceValue As Double, ByRef paymentValue As Double, ByRef Check As Double, ByRef Bal As Double)

        dgGrid.ColumnCount = 9
        dgGrid.Rows.Add(InvoiceNo, InvoiceDate, AG_ID, AG_Name, Location, InvoiceValue, paymentValue, Check, Bal)
    End Sub


    Private Sub txtBankName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBankID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtBankName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBankID.Validating


        connectionStaet()
        Try
            strSQL = "SELECT     Bank_Name FROM         TBL_BANK WHERE     (Bank_ID =@Bank_ID) AND (COM_ID =@COM_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@Bank_ID", Trim(txtBankID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                lblBankName.Text = dbConnections.dReader.Item("Bank_Name")

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
        errorEvent = "Reading information"

        connectionStaet()
        Try
            strSQL = "SELECT     TECH_NAME FROM         MTBL_TECH_MASTER WHERE     (COM_ID = @COM_ID) AND (TECH_CODE = @TECH_CODE) AND (TECH_ACTIVE = 1)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtRecivedBy.Text))
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

#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Dim path As String
    Private Sub showCrystalReport()



        Dim reportformObj As New frmCrystalReportViwer
        Dim reportNamestring As String = "Report"
        Dim AdminUser As Boolean = False
        Dim path As String = ""


        ''path = globalVariables.crystalReportpath & "\Reports\frmKBOInternal.rpt"
        'If globalVariables.selectedCompanyID = "003" Then
        '    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
        'Else
        path = globalVariables.crystalReportpath & "\Reports\rptKBOIRecipt.rpt"
        'End If

        Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
        Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
        Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo
        cryRpt.Load(path)
        cryRpt.RecordSelectionFormula = "{TBL_RECIPT_MASTER.RECIPT_ID} = '" & Trim(txtReciptID.Text) & "' AND {TBL_RECIPT_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"



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

        ''''''''''''''''''''''''''''''''''''''''
        cryRpt.PrintOptions.PrinterName = globalVariables.DefaultPrinterName

        '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
        Try
            Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
            Dim PkSize As New System.Drawing.Printing.PaperSize
            ObjPrinterSetting.PrinterName = globalVariables.DefaultPrinterName
            For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
                    PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                    Exit For
                End If
            Next

            If PkSize IsNot Nothing Then
                Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                myAppPrintOptions.PrinterName = globalVariables.DefaultPrinterName
                myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

            End If
            PkSize = Nothing



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Dim PrtDialog = New PrintDialog
        PrtDialog.PrinterSettings.PrinterName = globalVariables.DefaultPrinterName
        cryRpt.PrintOptions.PrinterName = PrtDialog.PrinterSettings.PrinterName

        ''''''''''''''''''''''''''''''''''''''''
        reportformObj.CrystalReportViewer1.Refresh()
        cryRpt.Refresh()
        reportformObj.CrystalReportViewer1.ReportSource = cryRpt
        reportformObj.CrystalReportViewer1.Refresh()
        reportformObj.Show()

        path = ""

    End Sub

    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

    Private Function Recipt_Print() As Boolean
        If Trim(txtReciptID.Text) = "" Then
            Recipt_Print = False
            Exit Function
        End If


        Try
            Dim path As String = ""
            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"
                'If globalVariables.selectedCompanyID = "003" Then
                '    path = globalVariables.crystalReportpath & "\Reports\rptKBOIRecipt.rpt"
                'Else
                path = globalVariables.crystalReportpath & "\Reports\rptKBOIRecipt.rpt"
                'End If

                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_RECIPT_MASTER.RECIPT_ID} = '" & Trim(txtReciptID.Text) & "' AND {TBL_RECIPT_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"



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
                cryRpt.PrintOptions.PrinterName = globalVariables.DefaultPrinterName

                '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
                Try
                    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
                    Dim PkSize As New System.Drawing.Printing.PaperSize
                    ObjPrinterSetting.PrinterName = globalVariables.DefaultPrinterName
                    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
                            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                            Exit For
                        End If
                    Next

                    If PkSize IsNot Nothing Then
                        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                        myAppPrintOptions.PrinterName = globalVariables.DefaultPrinterName
                        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

                    End If
                    PkSize = Nothing



                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Dim PrtDialog = New PrintDialog
                PrtDialog.PrinterSettings.PrinterName = globalVariables.DefaultPrinterName
                cryRpt.PrintOptions.PrinterName = PrtDialog.PrinterSettings.PrinterName

                cryRpt.PrintToPrinter(1, False, 0, 0)

                path = ""
            End Using

            Recipt_Print = True
        Catch ex As Exception
            Recipt_Print = False
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
        Return Recipt_Print
    End Function
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '===================================================================================================================
#Region "Button Events"

#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' API Events  ...............................................................
    '===================================================================================================================
#Region "API Events"

#End Region



    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Recipt_Print()
    End Sub



    Private Sub txtPaymentAmount_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtPaymentAmount.Validating

        Dim PayAmount As Decimal = 0
        If Trim(txtPaymentAmount.Text) = "" Then
            Exit Sub
        End If

        PayAmount = CDbl(Trim(txtPaymentAmount.Text))

        txtAmountInWords.Text = ConvertNum(PayAmount)
    End Sub


    Private Sub dgGrid_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgGrid.EditingControlShowing
        If Me.dgGrid.CurrentCell.ColumnIndex = 6 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            tb.Name = "txtEndReading"

            AddHandler tb.Validating, AddressOf TextBox_Validating
        End If
    End Sub
    Private Sub TextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Calculate_Balance()
    End Sub



    Dim InvoiceValue As Double = 0
    Dim PaymentValue As Double = 0
    Private Sub dgGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgGrid.CellContentClick

        If e.ColumnIndex = 7 Then
            InvoiceValue = 0
            PaymentValue = 0
            If Trim(txtPaymentAmount.Text) = "" Then
                PaymentValue = 0
            Else
                PaymentValue = CDbl(Trim(txtPaymentAmount.Text))
            End If
            If IsDBNull(dgGrid.Rows(e.RowIndex).Cells(5).Value) Then
                InvoiceValue = 0
            Else
                InvoiceValue = CDbl(Trim(dgGrid.Rows(e.RowIndex).Cells(5).Value))
            End If

            If dgGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).EditedFormattedValue = True Then
                dgGrid.Rows(e.RowIndex).Cells(6).Value = Format(dgGrid.Rows(e.RowIndex).Cells(5).Value, "0.00")
            Else
                dgGrid.Rows(e.RowIndex).Cells(6).Value = "0.00"
            End If
            Calculate_Balance()
        End If
    End Sub

    Private Sub Calculate_Balance()
        Dim BalanceSum As Double = 0
        Dim ReciptSum As Double = 0
        txtReciptTotal.Text = ReciptSum.ToString("N2")
        txtBalanceTotal.Text = BalanceSum.ToString("N2")
        txtOutStanding.Text = "0.00"
        If Trim(txtPaymentAmount.Text) = "" Then
            PaymentValue = 0
        Else
            PaymentValue = CDbl(Trim(txtPaymentAmount.Text))
        End If
        Try
            For Each row As DataGridViewRow In dgGrid.Rows


                dgGrid.Rows(row.Index).Cells(8).Value = dgGrid.Rows(row.Index).Cells(5).Value - dgGrid.Rows(row.Index).Cells(6).Value

                If (dgGrid.Rows(row.Index).Cells(5).Value - dgGrid.Rows(row.Index).Cells(6).Value) < 0 Then
                    dgGrid.Rows(row.Index).Cells(8).Style.ForeColor = Color.Red
                End If

                If (dgGrid.Rows(row.Index).Cells(5).Value) < 0 Then
                    dgGrid.Rows(row.Index).Cells(5).Style.ForeColor = Color.Red
                End If

                BalanceSum = BalanceSum + dgGrid.Rows(row.Index).Cells(8).Value
                ReciptSum = ReciptSum + dgGrid.Rows(row.Index).Cells(6).Value
            Next
            txtReciptTotal.Text = ReciptSum.ToString("N2")
            txtBalanceTotal.Text = BalanceSum.ToString("N2")

            txtOutStanding.Text = (ReciptSum - PaymentValue).ToString("N2")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbAPUse_CheckedChanged(sender As Object, e As EventArgs) Handles cbAPUse.CheckedChanged
        If cbAPUse.Checked = True Then
            cbAPUse.Text = "Y"
            txtAPAmount.ReadOnly = False
        Else
            cbAPUse.Text = "N"
            txtAPAmount.ReadOnly = True
        End If
    End Sub



    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        showCrystalReport()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        FindBy_Inv_No()
        load_Cus_info()
    End Sub

    Private Async Sub FindBy_Inv_No()
        Try
            Dim invoiceNo As String = txtFindIncoice.Text.Trim()
            Dim companyID As String = globalVariables.selectedCompanyID
            Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/api/receipts/findbyinvoiceno?companyID={companyID}&invoiceNo={invoiceNo}"
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                If response.IsSuccessStatusCode Then
                    Dim rawJson As String = Await response.Content.ReadAsStringAsync()
                    Dim customerId As String = JsonConvert.DeserializeObject(Of String)(rawJson)
                    txtCustomerID.Text = customerId
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub FindBy_Inv_No()
    '    Try
    '        strSQL = "SELECT     CUS_ID FROM         TBL_PREV_INV_HISTORY WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtFindIncoice.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            txtCustomerID.Text = dbConnections.dReader.Item("CUS_ID")
    '        End While
    '        dbConnections.dReader.Close()

    '        strSQL = "SELECT     CUS_ID FROM         TBL_INVOICE_MASTER WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
    '        dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtFindIncoice.Text))
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            txtCustomerID.Text = dbConnections.dReader.Item("CUS_ID")
    '        End While
    '        dbConnections.dReader.Close()
    '    Catch ex As Exception
    '        dbConnections.dReader.Close()
    '        MsgBox(ex.InnerException.Message)
    '    End Try
    'End Sub


    Private Sub txtFindIncoice_Validated(sender As Object, e As EventArgs) Handles txtFindIncoice.Validated
        FindBy_Inv_No()
        load_Cus_info()
    End Sub

    Private Sub txtFindIncoice_TextChanged(sender As Object, e As EventArgs) Handles txtFindIncoice.TextChanged

    End Sub

    Private Sub txtRecivedBy_TextChanged(sender As Object, e As EventArgs) Handles txtRecivedBy.TextChanged

    End Sub

    Private Sub txtBankID_TextChanged(sender As Object, e As EventArgs) Handles txtBankID.TextChanged

    End Sub
End Class

Public Class CustomerDebtorsInfo
    Public Property DebtorsOutstanding As Double
    Public Property advancePayment As Double
    Public Property BFOut As Double
End Class

Public Class ReceiptDetails
    Public Property COM_ID As String
    Public Property RECIPT_ID As String
    Public Property AG_ID As String
    Public Property AG_NAME As String
    Public Property CUS_ID As String
    Public Property INV_NO As String
    Public Property IN_DATE As DateTime
    Public Property CUS_LOC As String
    Public Property INV_AMOUNT As Double
    Public Property PAYMENT_AMOUNT As Double
    Public Property BALANCE_PAYMENT As Double
    Public Property TECH_CODE As String
    Public Property REP_CODE As String
    Public Property FULL_PAID As Boolean

End Class

Public Class TBL_RECEIPT_MASTER
    Public Property COM_ID As String
    Public Property RECEIPT_ID As String
    Public Property AG_ID As String
    Public Property AG_NAME As String
    Public Property RECEIPT_DATE As DateTime
    Public Property CUS_ID As String
    Public Property CUS_NAME As String
    Public Property PAY_TYPE As String
    Public Property PAY_METHOD As String
    Public Property CHEQUE_NO As String
    Public Property BANK_ID As String
    Public Property INV_AMOUNT As Decimal
    Public Property INV_DATE As DateTime
    Public Property BANK_NAME As String
    Public Property RECEIVED_BY As String
    Public Property RECEIVED_BY_NAME As String
    Public Property PAYMENT_AMOUNT As Decimal
    Public Property ADV_PAYMENT As Decimal
    Public Property PAY_BY_ADV_AMOUNT As Decimal
    Public Property BF_PAYMENT As Decimal
    Public Property AMOUNT_IN_WORD As String
    Public Property REMARKS As String
    Public Property CUS_LOC As String
    Public Property RECEIPT_TOTAL As Decimal
    Public Property BALANCE_PAYMENT As Decimal
    Public Property IS_PRINTED As String
    Public Property CR_BY As String
    Public Property CR_DATE As DateTime
    Public Property MD_BY As String
    Public Property MD_DATE As DateTime
    Public Property INVOICE_LIST As String
    Public Property RepCode As String
    Public Property ReceiptDetails As List(Of ReceiptDetails)
    Public Sub New()
        ReceiptDetails = New List(Of ReceiptDetails)()
    End Sub
    'Public Property ReciptDetails As List(Of ReceiptDetails)
End Class

Public Class ReceiptMasterVM
    Public Property InvoiceNo As String
    Public Property InvoiceDate As DateTime
    Public Property AG_ID As String
    Public Property AG_NAME As String
    Public Property Location As String
    Public Property CustomerName As String
    Public Property InvoiceValue As Decimal
    Public Property PaymentValue As Decimal
    Public Property Bal As Decimal
    Public Property Check As Boolean
    Public Property TechCode As String
End Class