
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Resources
Public Class dbChnges
    Dim strSQL As String = ""


    Private Sub dbChnges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows.Clear()
        AddtoGrid()
    End Sub

    Private Sub AddtoGrid()
        Dim rowCount As Integer = 0
        Dim INVList As New List(Of String)
        DataGridView1.Rows.Clear()
        Try
            connectionStaet()

            strSQL = "SELECT     TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT FROM         TBL_INVOICE_MASTER INNER JOIN                       TBL_CUS_AGREEMENT ON TBL_INVOICE_MASTER.COM_ID = TBL_CUS_AGREEMENT.COM_ID AND TBL_INVOICE_MASTER.AG_ID = TBL_CUS_AGREEMENT.AG_ID AND  TBL_INVOICE_MASTER.CUS_ID = TBL_CUS_AGREEMENT.CUS_CODE WHERE  TBL_CUS_AGREEMENT.MACHINE_TYPE = 'BW' AND TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read

                If INVList.Contains(dbConnections.dReader.Item("INV_NO")) Then

                Else
                    populatreDatagrid(dbConnections.dReader.Item("INV_NO"), dbConnections.dReader.Item("IS_VAT"), dbConnections.dReader.Item("IS_NBT"))

                    INVList.Add(dbConnections.dReader.Item("INV_NO"))
                End If




                rowCount = rowCount + 1
            End While


            ProgressBar1.Maximum = DataGridView1.RowCount 'Set Max Length
            ProgressBar1.Step = 1 'Set Step
            ProgressBar1.Value = 0 'Set Begin value
        Catch ex As Exception
            dbConnections.dReader.Close()


        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub



  



    Dim BWRate As Double = 0.0
    Dim BwCopies As Integer = 0
    Dim BwCalVal As Double = 0
    Dim COLCalVal As Integer = 0
    Dim COl_Rate As Double = 0
    Dim ColCOmmitment As Integer = 0

    Private Function Save1() As Boolean

        Dim GrandTotal As Double = 0
        Dim Rental As Double = 0
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                ProgressBar1.Value = ProgressBar1.Value + 1
                Rental = 0
                LoadSelectedAgreement(Trim(DataGridView1.Rows(row.Index).Cells(0).Value))
                dbConnections.sqlCommand.Parameters.Clear()
                If Trim(txtRental.Text) = "" Then
                    Rental = 0
                Else
                    Rental = CDbl(txtRental.Text)
                End If
                Threading.Thread.Sleep(200)
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET  INV_VAL ='" & (CDbl(txtInvoiceValue.Text) + Rental) & "' WHERE     (INV_NO = @INV_NO)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(DataGridView1.Rows(row.Index).Cells(0).Value))

                If dbConnections.sqlCommand.ExecuteNonQuery() Then Save1 = True Else Save1 = False
            Next

            If Save1 Then
                MsgBox("Saved")

            End If
        Catch ex As Exception

        End Try

        Return Save1
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Save1()
    End Sub

  

    Private Sub populatreDatagrid(ByRef INV As String, ByRef vat As Boolean, ByRef nbt As Boolean)
        DataGridView1.ColumnCount = 3
        DataGridView1.Rows.Add(INV, vat, nbt)
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Control.CheckForIllegalCrossThreadCalls = False
        Save1()
    End Sub



    Private errorEvent As String

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
    Dim MR_ID As String
    Dim viewPreviousData As Boolean = False
    Dim is_FirstRecord As Boolean = True '// reading first record of the dgbw
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                Me.save()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                delete()
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
        
        Return save
    End Function



    Private Function Process_Invoice() As Boolean
        
        Return Process_Invoice
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


    End Sub

#End Region

    Private Sub frmMeaterReading_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmMeaterReading_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmMeaterReading_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmMeaterReading_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmMeaterReading_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmMeaterReading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()

    End Sub

    Private Sub frmMeaterReading_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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


    Private Function Genarate_INV_NO() As String

        Genarate_INV_NO = ""

        errorEvent = "Reading information"
        connectionStaet()


        Try
            strSQL = "SELECT     MAX(INV_NO) as 'max' FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"


            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                Genarate_INV_NO = globalVariables.selectedCompanyID & "/" & "INV" & "/" & 1
            Else
                Dim IRCodeSplit() As String
                Dim NoRecordFound As Boolean = False
                Dim IRID As Integer = 0
                IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                IRID = IRCodeSplit(2)
                Do Until NoRecordFound = True
                    strSQL = "SELECT CASE WHEN EXISTS (SELECT     INV_NO  FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (INV_NO = '" & globalVariables.selectedCompanyID & "/INV/" & IRID & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteScalar = False Then
                        NoRecordFound = True
                    Else
                        IRID = IRID + 1
                    End If
                Loop

                If NoRecordFound = True Then
                    Genarate_INV_NO = IRCodeSplit(0) & "/INV/" & IRID
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
    End Function



    Private Function GenarateMRNo() As String

        GenarateMRNo = ""

        errorEvent = "Reading information"
        connectionStaet()


        Try
            strSQL = "SELECT     MAX(MR_ID) as 'max' FROM         TBL_METER_READING_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                GenarateMRNo = globalVariables.selectedCompanyID & "/" & "MR" & "/" & 1
            Else
                Dim IRCodeSplit() As String
                Dim NoRecordFound As Boolean = False
                Dim IRID As Integer = 0
                IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
                IRID = IRCodeSplit(2)
                Do Until NoRecordFound = True
                    strSQL = "SELECT CASE WHEN EXISTS (SELECT    MR_ID FROM         TBL_METER_READING_MASTER WHERE COM_ID  = '" & globalVariables.selectedCompanyID & "' AND MR_ID = '" & globalVariables.selectedCompanyID & "/" & "MR" & "/" & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

                    dbConnections.sqlCommand.CommandText = strSQL
                    If dbConnections.sqlCommand.ExecuteScalar = False Then
                        NoRecordFound = True
                    Else
                        IRID = IRID + 1
                    End If
                Loop

                If NoRecordFound = True Then
                    GenarateMRNo = IRCodeSplit(0) & "/MR/" & IRID
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
    End Function


    'Private Function Genarate_INV_NO() As String

    '    Genarate_INV_NO = ""
    '    Dim preFixComName As String = ""

    '    errorEvent = "Reading information"
    '    connectionStaet()

    '    Try
    '        strSQL = "SELECT     COM_PRE_FIX_NAME FROM         L_TBL_COMPANIES WHERE    (COM_ID = '" & globalVariables.selectedCompanyID & "')"
    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            preFixComName = globalVariables.selectedCompanyID
    '        Else
    '            preFixComName = dbConnections.sqlCommand.ExecuteScalar
    '        End If



    '        strSQL = "SELECT     MAX(INV_NO) as 'max' FROM         TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "')"

    '        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '        dbConnections.sqlCommand.CommandText = strSQL
    '        If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
    '            Genarate_INV_NO = preFixComName & "/" & "INV" & "/" & 1
    '        Else
    '            Dim IRCodeSplit() As String
    '            Dim NoRecordFound As Boolean = False
    '            Dim IRID As Integer = 0
    '            IRCodeSplit = dbConnections.sqlCommand.ExecuteScalar.ToString.Split("/")
    '            IRID = IRCodeSplit(2)
    '            Do Until NoRecordFound = True
    '                strSQL = "SELECT CASE WHEN EXISTS (SELECT    INV_NO FROM         TBL_INVOICE_MASTER WHERE COM_ID  = '" & globalVariables.selectedCompanyID & "' AND INV_NO = '" & preFixComName & "/" & "INV" & "/" & IRID & "') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
    '                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

    '                dbConnections.sqlCommand.CommandText = strSQL
    '                If dbConnections.sqlCommand.ExecuteScalar = False Then
    '                    NoRecordFound = True
    '                Else
    '                    IRID = IRID + 1
    '                End If
    '            Loop

    '            If NoRecordFound = True Then
    '                Genarate_INV_NO = IRCodeSplit(0) & "/INV/" & IRID
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


    Dim sqlCMD As New SqlCommand

    Private Sub LoadSelectedAgreement(ByRef InvoiceNo As String)

        Dim AGCode As String = ""
        Dim CUCode As String = ""
        Dim DRange1 As Date
        Dim DRange2 As Date
        Dim MkeModel As String
        Dim BillingPeriod As Integer = 1
        Dim LastBillingDate As DateTime
        Dim IsPrevousRecordLoading As Boolean = False
        Dim lastMR As Integer = 0
        Dim EndReading As String = ""
        Dim Waistage As String = ""
        Dim Copies As Integer = 0
        Dim SN As String
        Dim PNo As String
        Dim MLoc As String
        Dim IsMRhave As Boolean = False

        dgMR.Rows.Clear()
        Try


            '// Get Ag ID from INvoice
            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT     AG_ID,CUS_ID,INV_PERIOD_START,INV_PERIOD_END FROM         TBL_INVOICE_MASTER WHERE     (INV_NO = '" & Trim(InvoiceNo) & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                AGCode = dbConnections.dReader.Item("AG_ID")
                CUCode = dbConnections.dReader.Item("CUS_ID")
                dtpStart.Value = dbConnections.dReader.Item("INV_PERIOD_START")
                dtpEnd.Value = dbConnections.dReader.Item("INV_PERIOD_END")
            End While
            dbConnections.dReader.Close()





            strSQL = "SELECT     MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
                lblVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
                lblVatTypeName.Text = dbConnections.dReader.Item("VAT_DESC")

                If IsDBNull(dbConnections.dReader.Item("IS_NBT")) Then
                    cbNBT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_NBT") Then
                        cbNBT.Checked = True
                    Else
                        cbNBT.Checked = False
                    End If
                End If


                If IsDBNull(dbConnections.dReader.Item("IS_VAT")) Then
                    cbVAT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_VAT") Then
                        cbVAT.Checked = True
                    Else
                        cbVAT.Checked = False
                    End If
                End If

            End While
            dbConnections.dReader.Close()




            '// get billing period
            dbConnections.sqlCommand.Parameters.Clear()
            strSQL = "SELECT    ISNULL( BILLING_PERIOD,1) FROM         TBL_CUS_AGREEMENT WHERE     (AG_ID = '" & Trim(AGCode) & "') and (COM_ID = '" & globalVariables.selectedCompanyID & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            BillingPeriod = dbConnections.sqlCommand.ExecuteScalar

            '// get last billing date range
            strSQL = "SELECT   ISNULL( MAX(PERIOD_START),GETDATE()) AS Expr1  FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            LastBillingDate = dbConnections.sqlCommand.ExecuteScalar

            If globalVariables.selectedCompanyID = "003" Then
                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     CUS_NAME, CUS_ADD1, CUS_ADD2 FROM         MTBL_CUSTOMER_MASTER WHERE     (COM_ID =@COM_ID) AND (CUS_ID =@CUS_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    If IsDBNull(dbConnections.dReader.Item("CUS_NAME")) Then
                        txLocation1.Text = ""
                    Else
                        txLocation1.Text = dbConnections.dReader.Item("CUS_NAME")
                    End If



                    If IsDBNull(dbConnections.dReader.Item("CUS_ADD1")) Then
                        txtLocation2.Text = ""
                    Else
                        txtLocation2.Text = dbConnections.dReader.Item("CUS_ADD1")
                    End If
                    If IsDBNull(dbConnections.dReader.Item("CUS_ADD2")) Then
                        txtLocation3.Text = ""
                    Else
                        txtLocation3.Text = dbConnections.dReader.Item("CUS_ADD2")
                    End If

                End While
                dbConnections.dReader.Close()
            Else '// this will take machine location as invoice address

                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     TOP (1) M_LOC1, M_LOC2, M_LOC3 FROM         TBL_MACHINE_TRANSACTIONS  WHERE     (COM_ID = @COM_ID) AND (AG_ID = @AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read

                    txLocation1.Text = dbConnections.dReader.Item("M_LOC1")
                    txtLocation2.Text = dbConnections.dReader.Item("M_LOC2")
                    txtLocation3.Text = dbConnections.dReader.Item("M_LOC3")
                End While
                dbConnections.dReader.Close()

            End If


            dbConnections.sqlCommand.Parameters.Clear()

            dbConnections.sqlCommand.Parameters.Clear()
            Dim IsInvoiced As Boolean = False
            strSQL = "SELECT CASE WHEN EXISTS (SELECT INV_NO FROM TBL_INVOICE_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "') AND (INV_PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (INV_PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            dbConnections.sqlCommand.CommandText = strSQL
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsInvoiced = True
            Else
                IsInvoiced = False
            End If

            strSQL = "SELECT     MTBL_MACHINE_MASTER.MACHINE_MAKE, MTBL_MACHINE_MASTER.MACHINE_MODEL, TBL_MACHINE_TRANSACTIONS.SERIAL, TBL_MACHINE_TRANSACTIONS.P_NO, TBL_MACHINE_TRANSACTIONS.M_DEPT, (SELECT  TOP 1  isnull( END_MR,0) FROM         TBL_TBL_METER_READING_DET WHERE     (SERIAL_NO = TBL_MACHINE_TRANSACTIONS.SERIAL) AND (COM_ID = '" & globalVariables.selectedCompanyID & "') ORDER BY TRANS_ID  DESC) as 'L_READING' FROM         TBL_MACHINE_TRANSACTIONS INNER JOIN MTBL_MACHINE_MASTER ON TBL_MACHINE_TRANSACTIONS.COM_ID = MTBL_MACHINE_MASTER.COM_ID AND TBL_MACHINE_TRANSACTIONS.MACHINE_PN = MTBL_MACHINE_MASTER.MACHINE_ID WHERE     (TBL_MACHINE_TRANSACTIONS.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_MACHINE_TRANSACTIONS.CUS_ID = @CUS_ID) AND (TBL_MACHINE_TRANSACTIONS.AG_ID =@AG_ID) ORDER BY TBL_MACHINE_TRANSACTIONS.P_NO DESC"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))


            Dim da As New SqlDataAdapter(sqlCommand)

            Dim ds As New DataSet()

            da.Fill(ds)


            For i = 0 To ds.Tables(0).Rows.Count - 1
                'ds.Tables(0).Rows(i).Item(0)
                SN = ds.Tables(0).Rows(i).Item(2)
                If IsDBNull(ds.Tables(0).Rows(i).Item(3)) Then
                    PNo = ""
                Else
                    PNo = ds.Tables(0).Rows(i).Item(3)
                End If
                EndReading = ""
                lastMR = 0
                Waistage = ""
                Copies = 0


                MLoc = ds.Tables(0).Rows(i).Item(4)
                MkeModel = ds.Tables(0).Rows(i).Item(1)
                dbConnections.dReader.Close()
                strSQL = "SELECT     START_MR, END_MR, COPIES, WAISTAGE FROM         TBL_TBL_METER_READING_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & Trim(AGCode) & "') AND (SERIAL_NO = '" & SN & "') AND (CUS_ID = '" & Trim(CUCode) & "') AND (PERIOD_START = '" & dtpStart.Value.ToString("yyyy/MM/dd") & "') AND (PERIOD_END = '" & dtpEnd.Value.ToString("yyyy/MM/dd") & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read
                    IsMRhave = True
                    If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
                        If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then
                            lastMR = 0
                        Else
                            lastMR = ds.Tables(0).Rows(i).Item(5)
                        End If
                    Else
                        lastMR = dbConnections.dReader.Item("START_MR")
                    End If

                    If IsDBNull(dbConnections.dReader.Item("END_MR")) Then
                        EndReading = ""
                    Else
                        EndReading = dbConnections.dReader.Item("END_MR")
                    End If


                    If IsDBNull(dbConnections.dReader.Item("WAISTAGE")) Then
                        Waistage = ""
                    Else
                        Waistage = dbConnections.dReader.Item("WAISTAGE")
                    End If

                    If IsDBNull(dbConnections.dReader.Item("COPIES")) Then
                        Copies = 0
                    Else
                        Copies = dbConnections.dReader.Item("COPIES")
                    End If

                End While
                dbConnections.dReader.Close()

                If IsMRhave = False Then
                    If IsDBNull(ds.Tables(0).Rows(i).Item(5)) Then

                        '// get First meter reading  form master transaction
                        strSQL = "SELECT     START_MR FROM         TBL_MACHINE_TRANSACTIONS WHERE     (COM_ID = @COM_ID) AND (SERIAL = @SERIAL)"
                        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                        dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(CUCode))
                        dbConnections.sqlCommand.Parameters.AddWithValue("@SERIAL", Trim(SN))
                        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                        While dbConnections.dReader.Read
                            If IsDBNull(dbConnections.dReader.Item("START_MR")) Then
                                lastMR = 0
                            Else
                                lastMR = dbConnections.dReader.Item("START_MR")
                            End If

                        End While

                        dbConnections.dReader.Close()


                    Else
                        lastMR = ds.Tables(0).Rows(i).Item(5)
                    End If


                End If
                If PNo = "" Then
                    PNo = "0"
                End If


                populatreDatagrid(MkeModel, SN, PNo, MLoc, lastMR, EndReading, Copies, Waistage)


            Next






            dbConnections.sqlCommand.Parameters.Clear()




            Try
                dbConnections.sqlCommand.Parameters.Clear()
                strSQL = "SELECT     CUS_TYPE, BILLING_METHOD, SLAB_METHOD, BILLING_PERIOD, AG_PERIOD_START,AG_PERIOD_END,INV_STATUS,MACHINE_TYPE,AG_RENTAL_PRICE,REP_CODE FROM  TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_CODE = @CUS_CODE) AND (AG_ID = @AG_ID)"
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_CODE", Trim(CUCode))
                dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AGCode))
                dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                While dbConnections.dReader.Read


                    If IsDBNull(dbConnections.dReader.Item("BILLING_METHOD")) Then
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                        rbtnRental.Checked = False
                    Else
                        rbtnActual.Checked = False
                        rbtnCommitment.Checked = False
                        rbtnRental.Checked = False

                        If dbConnections.dReader.Item("BILLING_METHOD") = "COMMITMENT" Then
                            rbtnCommitment.Checked = True
                        ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "ACTUAL" Then
                            rbtnActual.Checked = True
                        ElseIf dbConnections.dReader.Item("BILLING_METHOD") = "RENTAL" Then
                            rbtnRental.Checked = True
                        Else
                            rbtnActual.Checked = False
                            rbtnCommitment.Checked = False
                            rbtnRental.Checked = False
                        End If

                    End If

                    If IsDBNull(dbConnections.dReader.Item("SLAB_METHOD")) Then
                        txtSlabMethod.Text = ""
                    Else
                        txtSlabMethod.Text = dbConnections.dReader.Item("SLAB_METHOD")
                    End If


                    If IsDBNull(dbConnections.dReader.Item("BILLING_PERIOD")) Then
                        txtBilPeriod.Text = ""
                    Else
                        txtBilPeriod.Text = dbConnections.dReader.Item("BILLING_PERIOD")
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


                    If IsDBNull(dbConnections.dReader.Item("AG_RENTAL_PRICE")) Then
                        txtRental.Text = ""
                    Else
                        txtRental.Text = Format(dbConnections.dReader.Item("AG_RENTAL_PRICE"), "0.00")
                    End If

                    If IsDBNull(dbConnections.dReader.Item("REP_CODE")) Then
                        txtRepCode.Text = ""
                    Else
                        txtRepCode.Text = dbConnections.dReader.Item("REP_CODE")
                    End If


                End While
                dbConnections.dReader.Close()
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
            End Try

            LoadCommitments(Trim(AGCode), Trim(CUCode))



            CalculateInvoiceValue()


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try


    End Sub

    Private Sub LoadCommitments(ByRef AG_ID As String, ByRef CUS_ID As String)
        dgBw.Rows.Clear()

        Try
            strSQL = "SELECT  BW_RANGE_1, BW_RANGE_2, BW_RATE FROM         TBL_AG_BW_COMMITMENT WHERE       (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = @CUS_ID) AND (AG_CODE = @AG_CODE)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CUS_ID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_CODE", Trim(AG_ID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
          
                populatreDatagrid_BW_Commitment(dbConnections.dReader.Item("BW_RANGE_1"), dbConnections.dReader.Item("BW_RANGE_2"), dbConnections.dReader.Item("BW_RATE"), 0)
            End While
            dbConnections.dReader.Close()
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try



    End Sub

    Private Sub populatreDatagrid_BW_Commitment(ByRef Range1 As Integer, ByRef Range2 As Integer, ByRef Rate As Decimal, ByRef CopyCount As Integer)
        dgBw.ColumnCount = 4
        dgBw.Rows.Add(Range1, Range2, Rate, CopyCount)
    End Sub

    Private Sub populatreDatagrid(ByRef Make As String, ByRef SN As String, ByRef PNO As Integer, ByRef Location As String, ByRef StartReading As String, ByRef EndReading As String, ByRef Copies As String, ByRef Waistage As String)
        dgMR.ColumnCount = 8
        dgMR.Rows.Add(Make, SN, PNO, Location, StartReading, EndReading, Copies, Waistage)
    End Sub



    Private Sub CalculateInvoiceValue()
        Dim CopyCount As Integer = 0
        Dim Waistage As Integer = 0
        Dim InvCopyCount As Integer = 0
        Dim CommitmentBreakup As Integer = 0
        '// Actual Calculation variables
        Dim ARate As Decimal = 0


        '// Commitment Calculation variables
        '// Slab 2
        Dim isCapturedRange As Boolean = False
        Dim CapturedLastRate As Double = 0
        '// Master variables
        Dim InvoiceValue As Double
        Dim NBTval As Double
        Dim VATVal As Double
        Dim NetValue As Double
        Dim BwNBT As Double
        Dim BwVAT As Double



        '// Rental
        Dim rental_Val As Double = 0

        Try
            For Each row As DataGridViewRow In dgMR.Rows
                If dgMR.Rows(row.Index).Cells("MR_COPIES").Value <> Nothing Then
                    CopyCount = CopyCount + dgMR.Rows(row.Index).Cells("MR_COPIES").Value
                End If

                If dgMR.Rows(row.Index).Cells("WAISTAGE").Value <> Nothing Then
                    Waistage = Waistage + dgMR.Rows(row.Index).Cells("WAISTAGE").Value
                End If

            Next

            txtTotalCopies.Text = CopyCount
            txtTotalWaistage.Text = Waistage
            InvCopyCount = (CopyCount - Waistage)
            txtInvCopies.Text = InvCopyCount
            Dim CopyCountVal As Integer = 0 '// this user to crystal report bug fixign

            For Each row2 As DataGridViewRow In dgBw.Rows
                dgBw.Item(3, row2.Index).Value = Nothing
            Next

            If rbtnActual.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then
                    '// slab confirm
                    ARate = dgBw.Item(2, 0).Value
                    InvoiceValue = (InvCopyCount * ARate)
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)
                    dgBw.Item(3, 0).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database
                ElseIf Trim(txtSlabMethod.Text) = "SLAB-2" Then
                    '// slab confirm
                    CommitmentBreakup = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value <> Nothing Then



                            If (InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value) Then



                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                                dgBw.Item(3, row.Index).Value = InvCopyCount '// use to crystal report counting bug using bug fix from database

                                If row.Index > 0 Then
                                    dgBw.Item(3, row.Index - 1).Value = Nothing

                                End If
                                isCapturedRange = True
                                Exit For
                            Else
                                CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                isCapturedRange = False
                            End If

                            If isCapturedRange = False Then

                                InvoiceValue = CapturedLastRate * InvCopyCount


                                dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                If row.Index > 0 Then
                                    dgBw.Item(3, row.Index - 1).Value = Nothing

                                End If
                            End If

                        End If
                    Next

                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)


                End If


            End If

            If rbtnCommitment.Checked = True Then
                If Trim(txtSlabMethod.Text) = "SLAB-1" Then
                    Dim TotalCopies As Integer = InvCopyCount
                    Dim Diffreance As Integer = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        ''dgMR.Rows(row.Index).Cells("MR_COPIES").Value

                        If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value <> Nothing Then

                            If TotalCopies <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then
                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)

                                dgBw.Item(3, 0).Value = dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value  '// use to crystal report counting bug using bug fix from database
                            Else
                                InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * TotalCopies)
                                dgBw.Item(3, 0).Value = TotalCopies  '// use to crystal report counting bug using bug fix from database
                            End If

                        End If


                    Next
                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)

                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)

                ElseIf Trim(txtSlabMethod.Text) = "SLAB-2" Then
                    '// slab confirm
                    'For Each row2 As DataGridViewRow In dgBw.Rows
                    '    dgBw.Item(3, row2.Index).Value = Nothing
                    'Next
                    Dim IsFirstRecord As Boolean = True
                    Dim CurrentCopyCoyunt As Integer = InvCopyCount
                    Dim LastReadIndex As Integer = 0
                    Dim x As Integer = 0
                    Dim Diffrence As Integer = 0
                    For Each row As DataGridViewRow In dgBw.Rows
                        Diffrence = 0

                        'If row.Index = dgBw.RowCount - 2 Then

                        'Else
                        '    Diffrence = (dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value - dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value) + 1
                        'End If
                        Diffrence = (dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value - dgBw.Rows(row.Index).Cells("BW_RANGE_1").Value) + 1
                        If dgBw.Rows(row.Index).Cells("BW_RATE").Value <> Nothing Then
                            If dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value = 0 Then

                                InvoiceValue = InvoiceValue + (InvCopyCount * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database

                            ElseIf row.Index = 0 Then

                                If CurrentCopyCoyunt <= Diffrence Then
                                    InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                    dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    Exit For
                                Else
                                    InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                    dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                    CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                                End If

                            ElseIf CurrentCopyCoyunt <= Diffrence Then
                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If

                            ElseIf row.Index = dgBw.RowCount - 2 Then

                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                'CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence


                            ElseIf CurrentCopyCoyunt >= Diffrence Then
                                InvoiceValue = InvoiceValue + (Diffrence * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = Diffrence '// use to crystal report counting bug using bug fix from database
                                CurrentCopyCoyunt = CurrentCopyCoyunt - Diffrence
                            Else

                                InvoiceValue = InvoiceValue + (CurrentCopyCoyunt * dgBw.Rows(row.Index).Cells("BW_RATE").Value)
                                dgBw.Item(3, row.Index).Value = CurrentCopyCoyunt '// use to crystal report counting bug using bug fix from database
                                If CurrentCopyCoyunt > 0 Then
                                    CurrentCopyCoyunt = 0
                                End If




                            End If

                        End If

                    Next

                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)


                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)


                ElseIf Trim(txtSlabMethod.Text) = "SLAB-3" Then
                    '// slab confirmed
                    'For Each row2 As DataGridViewRow In dgBw.Rows
                    '    dgBw.Item(3, row2.Index).Value = Nothing
                    'Next
                    is_FirstRecord = True
                    Dim RowCount As Integer = dgBw.RowCount - 2
                    For Each row As DataGridViewRow In dgBw.Rows
                        If dgBw.Rows(row.Index).Cells("BW_RATE").Value <> Nothing Then


                            ''// if reading last row
                            'If row.Index = RowCount Then

                            '    isCapturedRange = True

                            'Else

                            '    'If InvCopyCount > dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value And InvCopyCount <= dgBw.Rows(row.Index + 1).Cells("BW_RANGE_2").Value Then

                            '    'End If
                            '    If (InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value) Then


                            '        isCapturedRange = True

                            '    ElseIf InvCopyCount > dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value And InvCopyCount <= dgBw.Rows(row.Index + 1).Cells("BW_RANGE_2").Value Then

                            '        isCapturedRange = True

                            '    Else
                            '        isCapturedRange = False
                            '        CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                            '    End If

                            'End If

                            If is_FirstRecord = True Then
                                If InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then

                                    InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                                    dgBw.Item(3, row.Index).Value = dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value  '// use to crystal report counting bug using bug fix from database
                                    isCapturedRange = True
                                    is_FirstRecord = False
                                    Exit For
                                Else
                                    isCapturedRange = False
                                    CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                    is_FirstRecord = False
                                End If

                            Else

                                If row.Index = RowCount Then

                                    InvoiceValue = (dgBw.Rows(RowCount).Cells("BW_RATE").Value * InvCopyCount)
                                    dgBw.Item(3, RowCount).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                    isCapturedRange = True
                                    Exit For

                                Else
                                    If InvCopyCount > dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then
                                        isCapturedRange = False
                                        CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                                    Else
                                        InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                                        dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                                        isCapturedRange = True
                                        Exit For
                                    End If

                                End If




                            End If



                            'If row.Index = 0 Then '// if first slab is less than the commitment need to be charge
                            '    InvoiceValue = (dgBw.Rows(RowCount).Cells("BW_RATE").Value * dgBw.Rows(RowCount).Cells("BW_RANGE_2").Value)
                            '    dgBw.Item(3, RowCount).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                            '    Exit For
                            'Else

                            'End If
                            'If InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value Then

                            'Else
                            '    isCapturedRange = False
                            '    CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value
                            'End If



                            'If isCapturedRange = True Then
                            '    'InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)
                            '    InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                            '    dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                            '    Exit For



                            'End If


                            'If (InvCopyCount <= dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value) Then
                            '    If is_FirstRecord = True Then

                            '        InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * dgBw.Rows(row.Index).Cells("BW_RANGE_2").Value)


                            '        dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                            '        If row.Index > 0 Then
                            '            dgBw.Item(3, row.Index - 1).Value = Nothing

                            '        End If

                            '        is_FirstRecord = False
                            '    Else
                            '        InvoiceValue = (dgBw.Rows(row.Index).Cells("BW_RATE").Value * InvCopyCount)
                            '    End If

                            '    isCapturedRange = True
                            '    Exit For
                            'Else
                            '    CapturedLastRate = dgBw.Rows(row.Index).Cells("BW_RATE").Value

                            '    isCapturedRange = False
                            'End If

                            'If isCapturedRange = False Then
                            '    InvoiceValue = CapturedLastRate * InvCopyCount
                            '    dgBw.Item(3, row.Index).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                            '    If row.Index > 0 Then
                            '        dgBw.Item(3, row.Index - 1).Value = Nothing
                            '    End If
                            'End If

                        End If
                    Next


                    If isCapturedRange = False Then
                        InvoiceValue = (dgBw.Rows(RowCount).Cells("BW_RATE").Value * InvCopyCount)
                        dgBw.Item(3, RowCount).Value = InvCopyCount  '// use to crystal report counting bug using bug fix from database
                    End If


                    If cbNBT.Checked = True Then
                        NBTval = ((InvoiceValue / 100) * 2)
                    Else
                        NBTval = 0
                    End If
                    If cbVAT.Checked = True Then
                        VATVal = (((InvoiceValue + NBTval) / 100) * 15)
                    Else
                        VATVal = 0
                    End If

                    NetValue = (InvoiceValue + NBTval + VATVal)
                    Calculate_Net_Value(InvoiceValue, NBTval, VATVal, NetValue)

                End If
            End If

            If rbtnRental.Checked = True Then
                InvoiceValue = 0

                dgBw.Item(3, 0).Value = Nothing


                Dim ExceedCommitment As Integer = 0
                Dim ExceededCopyValue As Double = 0
                Dim RentalNBT As Double = 0
                Dim RentalVAT As Double = 0

                If Trim(txtRental.Text) = "" Then
                    rental_Val = 0
                    txtRental.Text = 0
                Else
                    rental_Val = CDbl(txtRental.Text)
                    txtRental.Text = rental_Val.ToString("N2")
                End If
                If dgBw.Item(1, 0).Value = 1 Then '// Actual Rentals 

                    ExceedCommitment = InvCopyCount
                    InvoiceValue = (ExceedCommitment * dgBw.Item(2, 0).Value)
                ElseIf InvCopyCount <= dgBw.Item(1, 0).Value Then
                    InvoiceValue = 0
                Else
                    ExceedCommitment = (InvCopyCount - dgBw.Item(1, 0).Value)
                    InvoiceValue = (ExceedCommitment * dgBw.Item(2, 0).Value)
                End If

                'InvoiceValue = (InvCopyCount * dgBw.Item(2, 0).Value)
                dgBw.Item(3, 0).Value = ExceedCommitment
                txtTotalCopies.Text = ExceedCommitment


                If cbNBT.Checked = True Then
                    BwNBT = (InvoiceValue / 100) * 2
                Else
                    BwNBT = 0
                End If

                If cbVAT.Checked = True Then
                    BwVAT = ((InvoiceValue + BwNBT) / 100) * 15
                Else
                    BwVAT = 0
                End If



                txtInvoiceValue.Text = (InvoiceValue).ToString("N2")



                If cbNBT.Checked = True Then
                    RentalNBT = (rental_Val / 100) * 2
                Else
                    RentalNBT = 0
                End If

                If cbVAT.Checked = True Then
                    RentalVAT = ((rental_Val + RentalNBT) / 100) * 15
                Else
                    RentalVAT = 0
                End If



                txtNBT.Text = (BwNBT + RentalNBT).ToString("N2")
                txtVAT.Text = (BwVAT + RentalVAT).ToString("N2")

                NetValue = (InvoiceValue + BwNBT + BwVAT) + (rental_Val + RentalNBT + RentalVAT)
                txtNetValue.Text = NetValue.ToString("N2")
            End If


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Exit Sub
    End Sub


    Private Sub Calculate_Net_Value(ByRef InvoiceValue As Double, ByRef NBTval As Double, ByRef VATVal As Double, ByRef NetValue As Double)
        txtInvoiceValue.Text = InvoiceValue.ToString("N2")
        txtNBT.Text = NBTval.ToString("N2")
        txtVAT.Text = VATVal.ToString("N2")
        txtNetValue.Text = NetValue.ToString("N2")
    End Sub


    
    Private Sub GetLastInvInfo(ByRef CusID As String, ByRef AgID As String)
        Try
            Dim isRecordHave As Boolean = False
            strSQL = "SELECT     TOP (1) INV_PERIOD_START, INV_PERIOD_END, INV_NO, INV_DATE FROM    TBL_INVOICE_MASTER WHERE     (COM_ID ='" & globalVariables.selectedCompanyID & "') AND (CUS_ID =@CUS_ID) AND (AG_ID = @AG_ID) ORDER BY INV_DATE DESC"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(CusID))
            dbConnections.sqlCommand.Parameters.AddWithValue("@AG_ID", Trim(AgID))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                isRecordHave = True
                If IsDBNull(dbConnections.dReader.Item("INV_NO")) Then
                    lblInvoiceNo.Text = ""
                Else
                    lblInvoiceNo.Text = dbConnections.dReader.Item("INV_NO")
                End If
                If IsDBNull(dbConnections.dReader.Item("INV_PERIOD_START")) Then
                    lblSDate.Text = ""
                Else
                    lblSDate.Text = dbConnections.dReader.Item("INV_PERIOD_START")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_PERIOD_END")) Then
                    lblEDate.Text = ""
                Else
                    lblEDate.Text = dbConnections.dReader.Item("INV_PERIOD_END")
                End If

                If IsDBNull(dbConnections.dReader.Item("INV_DATE")) Then
                    lblLInvDate.Text = ""
                Else
                    lblLInvDate.Text = dbConnections.dReader.Item("INV_DATE")
                End If


            End While

            If isRecordHave = False Then
                lblInvoiceNo.Text = ""
                lblSDate.Text = ""
                lblEDate.Text = ""
                lblLInvDate.Text = ""
            End If
            dbConnections.dReader.Close()
        Catch ex As Exception
            dbConnections.dReader.Close()
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub


#End Region

#Region "Validation"

    Dim IsErrorHave As Boolean = False
  
    Private Sub FormClear()

        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        txtSelectedAG.Text = ""
        txLocation1.Text = ""
        txtLocation2.Text = ""
        txtLocation3.Text = ""
        dgMR.Columns(2).HeaderText = globalVariables.MachineRefCode + " No"

        txtCustomerID.Focus()
        txtRepCode.Text = ""
        lblInvoiceNo.Text = ""
        lblSDate.Text = ""
        lblEDate.Text = ""
        lblLInvDate.Text = ""
        txtRepCode.Text = ""
        'Dim CurrentDate As DateTime = Today.Date
        'Dim dateVal As Integer
        'Dim MonthVal As Integer
        'Dim YearVal As Integer
        'Dim NewStartDate As DateTime
        'Dim NewEndDate As DateTime
        'dateVal = 1
        'MonthVal = CurrentDate.Month
        'YearVal = CurrentDate.Year

        'NewStartDate = CDate(dateVal & "/" & MonthVal & "/" & YearVal)
        'NewEndDate = NewStartDate.AddMonths(1)
        'dtpStart.Value = NewStartDate
        'dtpEnd.Value = NewEndDate

        dtpEnd.Value = Today.Date
        dtpStart.Value = Today.Date.AddMonths(-1)


        dgMR.Rows.Clear()
        dgBw.Rows.Clear()
        txtSelectedAG.Text = ""
        rbtnActual.Checked = False
        rbtnCommitment.Checked = False
        rbtnInvStatusAll.Checked = False
        rbtnInvStatusIndividual.Checked = False
        txtSlabMethod.Text = ""
        txtBilPeriod.Text = ""
        txtTotalCopies.Text = ""
        txtTotalWaistage.Text = ""
        txtInvCopies.Text = ""
        txtInvoiceValue.Text = ""
        txtNBT.Text = ""
        txtVAT.Text = ""
        cbNBT.Checked = False
        cbVAT.Checked = False
        txtNetValue.Text = ""
        lblVatType.Text = "##"
        lblVatTypeName.Text = "##"



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
       
        connectionStaet()
        Try
            strSQL = "SELECT     MTBL_CUSTOMER_MASTER.CUS_NAME, MTBL_CUSTOMER_MASTER.VAT_TYPE_ID, MTBL_VAT_MASTER.VAT_DESC, MTBL_VAT_MASTER.IS_NBT, MTBL_VAT_MASTER.IS_VAT FROM         MTBL_CUSTOMER_MASTER INNER JOIN  MTBL_VAT_MASTER ON MTBL_CUSTOMER_MASTER.COM_ID = MTBL_VAT_MASTER.COM_ID AND MTBL_CUSTOMER_MASTER.VAT_TYPE_ID = MTBL_VAT_MASTER.VAT_TYPE_ID WHERE     (MTBL_CUSTOMER_MASTER.COM_ID = @COM_ID) AND (MTBL_CUSTOMER_MASTER.CUS_ID = @CUS_ID)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@CUS_ID", Trim(txtCustomerID.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read

                txtCustomerName.Text = dbConnections.dReader.Item("CUS_NAME")
                lblVatType.Text = dbConnections.dReader.Item("VAT_TYPE_ID")
                lblVatTypeName.Text = dbConnections.dReader.Item("VAT_DESC")

                If IsDBNull(dbConnections.dReader.Item("IS_NBT")) Then
                    cbNBT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_NBT") Then
                        cbNBT.Checked = True
                    Else
                        cbNBT.Checked = False
                    End If
                End If


                If IsDBNull(dbConnections.dReader.Item("IS_VAT")) Then
                    cbVAT.Checked = False
                Else
                    If dbConnections.dReader.Item("IS_VAT") Then
                        cbVAT.Checked = True
                    Else
                        cbVAT.Checked = False
                    End If
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
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"

    'Private Sub dgMR_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgMR.CellFormatting

    '    dgMR.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)

    'End Sub


    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgMR.EditingControlShowing

        If Me.dgMR.CurrentCell.ColumnIndex = 5 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            tb.Name = "txtEndReading"

            AddHandler tb.Validating, AddressOf TextBox_Validating
        ElseIf Me.dgMR.CurrentCell.ColumnIndex = 7 And Not e.Control Is Nothing Then
            Dim txtWaistage As TextBox = CType(e.Control, TextBox)
            txtWaistage.Name = "txtWaistage"
            AddHandler txtWaistage.Validating, AddressOf txtWaistage_Validating

        End If

    End Sub




    Dim CurrentMRval As Integer
    Dim YieldPerItem As Integer
    Dim TotalYield As Integer
    Dim PreviousReading As Integer = 0
    Dim ReqQty As Integer
    Dim CurrentCopies As Integer



    Private Sub TextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(4, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value)
            End If


            AllCopieseThisMOnth = (EndReading - StartReading)


            'If rbtnRental.Checked = True Then

            '    If AllCopieseThisMOnth <= dgBw.Item(1, 0).Value Then
            '        AllCopieseThisMOnth = 0
            '    Else

            '        AllCopieseThisMOnth = (AllCopieseThisMOnth - dgBw.Item(1, 0).Value)
            '    End If

            'End If






            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try
        Exit Sub
    End Sub

    Private Sub txtWaistage_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim EndReading As Integer = 0
        Dim StartReading As Integer = 0
        Dim AllCopieseThisMOnth As Integer = 0

        Try

            StartReading = dgMR.Item(4, dgMR.CurrentCell.RowIndex).Value

            If Trim(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value) = "" Then
                EndReading = 0
            Else
                EndReading = CInt(dgMR.Item(5, dgMR.CurrentCell.RowIndex).Value)
            End If

            AllCopieseThisMOnth = (EndReading - StartReading)

            If rbtnRental.Checked = True Then

                If AllCopieseThisMOnth <= dgBw.Item(1, 0).Value Then
                    AllCopieseThisMOnth = 0
                Else

                    AllCopieseThisMOnth = (AllCopieseThisMOnth - dgBw.Item(1, 0).Value)
                End If

            End If

            dgMR.Item(6, dgMR.CurrentCell.RowIndex).Value = AllCopieseThisMOnth

            CalculateInvoiceValue()
        Catch ex As Exception

            MsgBox(ex.InnerException.Message)

        End Try
        Exit Sub
    End Sub

#End Region


    Private Sub btnProcessInvoice_Click(sender As Object, e As EventArgs) Handles btnProcessInvoice.Click

        Process_Invoice()
    End Sub

    Private Sub txtCustomerID_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerID.TextChanged

    End Sub

    Private Sub cbNBT_CheckedChanged(sender As Object, e As EventArgs) Handles cbNBT.CheckedChanged
        If cbNBT.Checked = True Then
            cbNBT.Text = "Yes"
        Else
            cbNBT.Text = "No"
        End If
        CalculateInvoiceValue()
    End Sub

    Private Sub cbVAT_CheckedChanged(sender As Object, e As EventArgs) Handles cbVAT.CheckedChanged
        If cbVAT.Checked = True Then
            cbVAT.Text = "Yes"
        Else
            cbVAT.Text = "No"
        End If
        CalculateInvoiceValue()
    End Sub


   

    Private Sub txtInvoiceNo_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceNo.TextChanged

    End Sub

    
End Class