Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data
Imports System.Globalization

Public Class frmCreditNote
    Private strSql As String
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Private btnStatus(5) As Boolean

    Dim PeriodStart As String
    Dim PeriodEnd As String

    Dim VatAmount As Double
    Dim SsclVatAmount As Double

    Dim creditNo As String

    Dim currentCrNo As Integer = 0

    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
            Case "Save"
                Me.CreateNew()
            Case "Print"
        End Select
    End Sub

    Private Sub saveBtnStatus()
        frmMDImain.tsbtnSave.Enabled = True
        'If frmMDImain.tsbtnSave.Enabled Then btnStatus(0) = True Else btnStatus(0) = False
        'If frmMDImain.tsbtnNew.Enabled Then btnStatus(1) = True Else btnStatus(1) = False
        'If frmMDImain.tsbtnEdit.Enabled Then btnStatus(2) = True Else btnStatus(2) = False
        'If frmMDImain.tsbtnDelete.Enabled Then btnStatus(3) = True Else btnStatus(3) = False
        'If frmMDImain.tsbtnPrint.Enabled Then btnStatus(4) = True Else btnStatus(4) = False
    End Sub

    Private Sub frmCreditNote_Load(sender As Object, e As EventArgs)
    End Sub

    Private Sub txtCustomerID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub RepCode(repCode As String)
        txtRepCode.Text = repCode
    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged
        connectionStaet()
        If txtCustomerID.Text <> "" Then
            strSql = "SELECT * FROM MTBL_CUSTOMER_MASTER WHERE CUS_ID = '" & txtCustomerID.Text & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSql, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            While dbConnections.dReader.Read
                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
                txtCustomerAddress01.Text = dbConnections.dReader.Item("CUS_ADD1")
                txtCustomerAddress02.Text = dbConnections.dReader.Item("CUS_ADD2")
            End While
            dbConnections.dReader.Close()
            LoadInvoiceList(txtCustomerID.Text)
        End If
    End Sub

    Private Sub FillInvoiceDetails(invoiceNo As String)
        Dim strSql As String = "SELECT * FROM TBL_INVOICE_MASTER WHERE INV_NO = '" & invoiceNo & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
        dbConnections.sqlCommand = New SqlCommand(strSql, dbConnections.sqlConnection)
        Using reader As SqlDataReader = dbConnections.sqlCommand.ExecuteReader()
            While reader.Read()
                txtInvoiceNo.Text = invoiceNo

                RepCode(reader("REP_CODE").ToString())
                Dim vatPercentage As Integer
                If Not IsDBNull(reader("VAT_P")) Then
                    vatPercentage = Convert.ToInt32(reader("VAT_P").ToString())
                Else
                    vatPercentage = 0
                End If

                Dim InvoiceValue As Decimal
                If Not IsDBNull(reader("INV_VAL")) Then
                    InvoiceValue = Convert.ToDecimal(reader("INV_VAL").ToString())
                Else
                    InvoiceValue = 0
                End If

                Dim SsclPercentage As Integer
                If Not reader("NBT2_P") = 0.0 Then
                    SsclPercentage = Convert.ToInt32(reader("NBT2_P").ToString())
                Else
                    SsclPercentage = 0
                End If


                Dim SSclAmount As Double = InvoiceValue * SsclPercentage / 100
                Dim VatAmount As Double = (InvoiceValue + SSclAmount) * vatPercentage / 100
                Dim TotalAmount As Decimal = InvoiceValue + VatAmount

                'Dim TotalAmount As Decimal = InvoiceValue * vatPercentage / 100
                'TotalAmount += InvoiceValue
                txtInvoiceDate.Text = reader("INV_DATE").ToString()
                txtInvoiceSubTotal.Text = Convert.ToDouble(InvoiceValue.ToString()).ToString()
                txtVATValue.Text = VatAmount.ToString()
                txtSSCL.Text = SSclAmount.ToString()
                txtInvoiceTotal.Text = TotalAmount.ToString()


                SsclVatAmount = SSclAmount

                'VatAmount = InvoiceValue * vatPercentage / 100
                'SsclVatAmount = (InvoiceValue * SsclPercentage / 100) + InvoiceValue

                Dim inputFormat As String = "dd-MMM-yy h:mm:ss tt"
                Dim outputFormat As String = "MM-dd-yyyy"

                Dim startDateParseDate As DateTime = DateTime.ParseExact(reader("INV_PERIOD_START").ToString(), inputFormat, CultureInfo.InvariantCulture)
                PeriodStart = startDateParseDate.ToString(outputFormat)

                Dim endDateParseDate As DateTime = DateTime.ParseExact(reader("INV_PERIOD_END").ToString(), inputFormat, CultureInfo.InvariantCulture)
                PeriodEnd = endDateParseDate.ToString(outputFormat)


            End While
        End Using
    End Sub

    Private Sub LoadInvoiceList(customerID As String)
        strSql = "SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE CUS_ID = '" & txtCustomerID.Text & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "' AND INV_STATUS_T IS NULL"
        dbConnections.sqlCommand = New SqlCommand(strSql, dbConnections.sqlConnection)
        Using reader As SqlDataReader = dbConnections.sqlCommand.ExecuteReader()
            While reader.Read()
                ListBox1.Items.Add(reader("INV_NO").ToString())
            End While
        End Using
    End Sub

    Private Sub txtRepCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRepCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedItems.Count > 0 Then
            Dim selectedInvoiceNo = ListBox1.SelectedItems(0).ToString()
            FillInvoiceDetails(selectedInvoiceNo)
            ' Create a DataTable to store the result
            Dim dataTable As New DataTable()

            ' Assuming your table is named "Invoices" and has columns like "InvoiceNo", "CustomerName", etc.
            Dim query As String = $"SELECT * FROM TBL_INVOICE_DET WHERE INV_NO = '{selectedInvoiceNo}'"

            Using command As New SqlCommand(query, dbConnections.sqlConnection)
                ' Open the connection                
                Using dataAdapter As New SqlDataAdapter(command)
                    dataAdapter.Fill(dataTable)
                End Using
                DataGridView1.DataSource = dataTable
            End Using
        End If
    End Sub

    Private Sub frmCreditNote_Validated(sender As Object, e As EventArgs)

    End Sub

    Function DetectDateFormat(dateString As String) As String
        Dim formatsToCheck() As String = {"MM-dd-yyyy", "M/d/yyyy", "yyyy-MM-dd", "dd-MM-yyyy"}

        For Each format As String In formatsToCheck
            Dim parsedDate As DateTime
            If DateTime.TryParseExact(dateString, format, Nothing, DateTimeStyles.None, parsedDate) Then
                Return format
            End If
        Next

        Return "Unknown"
    End Function

    Function GenerateCreditNoteNo() As String
        Dim SelectQuery = "SELECT MAX(CN_NO) FROM TBL_CREDIT_NOTES"
        Dim command As New SqlCommand(SelectQuery, dbConnections.sqlConnection)
        Dim maxId As Integer = 0
        Dim crno As String = command.ExecuteScalar().ToString()
        Dim something As String() = crno.Split("/")
        If IsDBNull(command.ExecuteScalar()) Then
            maxId = 0
        Else
            maxId = Convert.ToInt32(something(2))
        End If

        Dim newId As Integer = maxId + 1
        Return $"{globalVariables.selectedCompanyID}/CRN/D/{newId}"
    End Function

    Private Sub CreateNew()
        Try
            Dim department As String
            department = cmbDept.SelectedItem.ToString()
            Dim masterQuery As String = "INSERT INTO TBL_CREDIT_NOTES(COM_ID, CN_NO, REQ_DATE, INVOICE_DATE , INVOICE_PERIOD_START, INVOICE_PERIOD_END, REQUEST_NO, INVOICE_NO, DEPT, CUS_NAME, CUS_ID, REASON, INVOICE_AMOUNT, STATUS, LINE_TAX, SSCL) VALUES " +
        "(@COM_ID, @CN_NO,@REQ_DATE, @INVOICE_DATE, @INVOICE_PERIOD_START, @INVOICE_PERIOD_END, @REQUEST_NO,@INVOICE_NO,@DEPT,@CUS_NAME,@CUS_ID,@REASON,@INVOICE_AMOUNT,@STATUS, @LINE_TAX, @SSCL)"
            Using masterCommand As New SqlCommand(masterQuery, dbConnections.sqlConnection)
                Dim invoiceNo As String = txtInvoiceNo.Text
                Dim segments As String() = invoiceNo.Split("/")
                Dim invoiceDate As DateTime = txtInvoiceDate.Text

                creditNo = GenerateCreditNoteNo()
                If segments.Length = 3 Then
                    masterCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                    masterCommand.Parameters.AddWithValue("@CN_NO", creditNo)
                    masterCommand.Parameters.AddWithValue("@REQ_DATE", DateTime.Now)
                    masterCommand.Parameters.AddWithValue("@REQUEST_NO", creditNo)
                    masterCommand.Parameters.AddWithValue("@INVOICE_NO", invoiceNo)
                    masterCommand.Parameters.AddWithValue("@INVOICE_DATE", invoiceDate)
                    masterCommand.Parameters.AddWithValue("@INVOICE_PERIOD_START", PeriodStart)
                    masterCommand.Parameters.AddWithValue("@INVOICE_PERIOD_END", PeriodEnd)
                    masterCommand.Parameters.AddWithValue("@DEPT", department)
                    masterCommand.Parameters.AddWithValue("@CUS_NAME", txtCustomerName.Text)
                    masterCommand.Parameters.AddWithValue("@CUS_ID", txtCustomerID.Text)
                    masterCommand.Parameters.AddWithValue("@LINE_TAX", txtVATValue.Text)
                    masterCommand.Parameters.AddWithValue("@REASON", RichTextBox1.Text)
                    masterCommand.Parameters.AddWithValue("@REQUESTED_REP_CODE", txtRepCode.Text)
                    masterCommand.Parameters.AddWithValue("@INVOICE_AMOUNT", txtInvoiceSubTotal.Text)
                    masterCommand.Parameters.AddWithValue("@SSCL", SsclVatAmount)
                    masterCommand.Parameters.AddWithValue("@STATUS", "PENDING HOD APPROVAL")
                    Dim masterRowAffected As Integer = masterCommand.ExecuteNonQuery()
                    If masterRowAffected > 0 Then
                        Dim updateInvoiceQuery As String = "UPDATE TBL_INVOICE_MASTER SET INV_TRANSACTION_STATUS = 'CREDIT NOTE REQUESTED' " &
                    "WHERE INV_NO = @INV_NO"
                        Dim conf = MessageBox.Show("Are you want to Save Credit Note ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                        If conf = DialogResult.Yes Then
                            Using updateCommand As New SqlCommand(updateInvoiceQuery, dbConnections.sqlConnection)
                                updateCommand.Parameters.AddWithValue("@INV_NO", invoiceNo)
                                updateCommand.ExecuteNonQuery()
                            End Using
                            SaveCreditNoteItems(creditNo)
                        End If
                    End If
                End If
            End Using

        Catch ex As Exception
            If ex.Message.Contains("PRIMARY KEY") Then
                MessageBox.Show("This Invoice is already a Credit Note")
            End If
            If cmbDept.Text = "" Then
                MessageBox.Show("Please select a department.")
            End If
        End Try
    End Sub

    Private Sub txtInvoiceNo_Validated(sender As Object, e As EventArgs) Handles txtInvoiceNo.Validated
        ListBox1.Items.Clear()
        Dim strSql As String = "Select INV_NO FROM TBL_INVOICE_MASTER WHERE INV_NO = '" & txtInvoiceNo.Text & "' AND COM_ID = '003'"
        dbConnections.sqlCommand = New SqlCommand(strSql, dbConnections.sqlConnection)
        Using reader As SqlDataReader = dbConnections.sqlCommand.ExecuteReader()
            If txtInvoiceNo.Text = "" Then
                MessageBox.Show("Please Select an Invoice No.")
            Else
                ListBox1.Items.Add(reader("INV_NO").ToString())
            End If
        End Using
    End Sub

    Private Sub SaveCreditNoteItems(ByRef creditNote As String)
        Dim selectQuery As String = "SELECT * FROM TBL_INVOICE_DET WHERE INV_NO = '" & txtInvoiceNo.Text & "'"
        Dim insertQuery As String = "INSERT INTO TBL_CREDIT_NOTE_ITEMS (COM_ID, CN_NO, AG_ID, CUS_ID, PERIOD_START, PERIOD_END, SERIAL_NO, 
        INV_NO, MAKE_MODEL, INV_ADD1, INV_ADD2, INV_ADD3, BILLING_METHOD, M_LOC, START_MR, END_MR, INV_COPIES, WAISTAGE, P_NO, COLOR_START_MR, COLOR_END_MR, 
COLOR_INV_COPIES, COLOR_WAISTAGE) VALUES (@COM_ID, @CN_NO, @AG_ID, @CUS_ID, @PERIOD_START, @PERIOD_END, @SERIAL_NO, @INV_NO, @MAKE_MODEL, @INV_ADD1, @INV_ADD2, @INV_ADD3, @BILLING_METHOD, @M_LOC, @START_MR, @END_MR, @INV_COPIES, @WAISTAGE, 
@P_NO, @COLOR_START_MR, @COLOR_END_MR, @COLOR_INV_COPIES, @COLOR_WAISTAGE
)"

        Using command As New SqlCommand(selectQuery, dbConnections.sqlConnection)
            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Using insertCommand As New SqlCommand(insertQuery, dbConnections.sqlConnection)
                        insertCommand.Parameters.AddWithValue("@COM_ID", reader("COM_ID"))
                        insertCommand.Parameters.AddWithValue("@CN_NO", creditNote)
                        insertCommand.Parameters.AddWithValue("@AG_ID", reader("AG_ID"))
                        insertCommand.Parameters.AddWithValue("@CUS_ID", reader("CUS_ID"))
                        insertCommand.Parameters.AddWithValue("@PERIOD_START", reader("PERIOD_START"))
                        insertCommand.Parameters.AddWithValue("@PERIOD_END", reader("PERIOD_END"))
                        insertCommand.Parameters.AddWithValue("@SERIAL_NO", reader("SERIAL_NO"))
                        insertCommand.Parameters.AddWithValue("@INV_NO", reader("INV_NO"))
                        insertCommand.Parameters.AddWithValue("@MAKE_MODEL", reader("MAKE_MODEL"))
                        insertCommand.Parameters.AddWithValue("@INV_ADD1", reader("INV_ADD1"))
                        insertCommand.Parameters.AddWithValue("@INV_ADD2", reader("INV_ADD2"))
                        insertCommand.Parameters.AddWithValue("@INV_ADD3", reader("INV_ADD3"))
                        insertCommand.Parameters.AddWithValue("@BILLING_METHOD", reader("BILLING_METHOD"))
                        insertCommand.Parameters.AddWithValue("@M_LOC", reader("M_LOC"))
                        insertCommand.Parameters.AddWithValue("@START_MR", reader("START_MR"))
                        insertCommand.Parameters.AddWithValue("@END_MR", reader("END_MR"))
                        insertCommand.Parameters.AddWithValue("@INV_COPIES", reader("INV_COPIES"))
                        insertCommand.Parameters.AddWithValue("@WAISTAGE", reader("WAISTAGE"))
                        insertCommand.Parameters.AddWithValue("@P_NO", reader("P_NO"))
                        insertCommand.Parameters.AddWithValue("@COLOR_START_MR", reader("COLOR_START_MR"))
                        insertCommand.Parameters.AddWithValue("@COLOR_END_MR", reader("COLOR_END_MR"))
                        insertCommand.Parameters.AddWithValue("@COLOR_INV_COPIES", reader("COLOR_INV_COPIES"))
                        insertCommand.Parameters.AddWithValue("@COLOR_WAISTAGE", reader("COLOR_WAISTAGE"))
                        Dim something As Integer = insertCommand.ExecuteNonQuery()
                    End Using
                End While
            End Using
        End Using
    End Sub

    Private Sub frmCreditNote_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        Try
            connectionStaet()
            strSql = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'AND USERDET_MENUTAG='" & Me.Tag & "' AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSql, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True

            saveBtnStatus()
        Catch ex As Exception
            MessageBox.Show("Error code(" & globalVariables.selectedCompanyID + "-" + Me.Tag & "X3) " + PermissionReadingErrorMessgae, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            connectionClose()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath & "\Reports\CreditNoteInvoice.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)
            cryRpt.RecordSelectionFormula = "{TBL_CREDIT_NOTES.CN_NO} = '" & creditNo & "'"

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
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class