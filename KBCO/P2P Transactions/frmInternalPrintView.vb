Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO

Public Class frmInternalPrintView


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
    Private SavedIR_NO As String
    Private IsNegative_Internal As String = ""
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

        Return save
    End Function


    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False


        Return delete
    End Function

    Private Sub FormEdit()

        'Dim conf = MessageBox.Show("" & EditMessage & "" & txtVatTypeName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        'If conf = vbYes Then

        '    isEditClicked = True
        '    globalFunctions.globalButtonActivation(True, True, False, False, False, True)
        '    Me.saveBtnStatus()
        'End If
    End Sub

#End Region

    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmInternalPrintView_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalPrintView_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalPrintView_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalPrintView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalPrintView_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub EnablePrintButton()
        Try
            strSQL = "SELECT TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_CODE, TBL_INTERNAL_MAIN.CUS_LOC, TBL_INTERNAL_MAIN.ISSUED_TO, TBL_INTERNAL_MAIN.CURRENT_MR, TBL_INTERNAL_MAIN.IR_TYPE, TBL_INTERNAL_MAIN.IR_STATE, TBL_INTERNAL_MAIN.IR_PRINTED, MTBL_TECH_MASTER.TECH_NAME,MTBL_CUSTOMER_MASTER.CUS_NAME FROM TBL_INTERNAL_MAIN WHERE BELEETA_REF_NO IS NULL"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmInternalPrintView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userGroupName As String = globalVariables.UsergroupName

        If userGroupName = "Accounts" Or userGroupName = "Administrator" Or userGroupName = "Doculine" Or userGroupName = "Finance" Then
            lblNPState.Visible = True
            lblNPState.Text = "Upload to Beleeta"
            btnPrintViewInternal.Visible = False
            lblSelectedPrinter.Visible = False
            btnUploadToBeleeta.Visible = True
            lblDispatchNo.Visible = True
            cmbPrinterList.Visible = False
            txtBeleetaRefNo.Visible = True
        End If

        FormClear()
        loadInternal()
        cmbPrinterList.Text = globalFunctions.GetDefaultPrinter()
    End Sub

    Private Sub frmInternalPrintView_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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

    Private Sub Loading_Printer_List()
        Try
            Dim InstalledPrinters As String

            ' Find all printers installed
            For Each InstalledPrinters In _
                System.Drawing.Printing.PrinterSettings.InstalledPrinters
                Me.cmbPrinterList.Items.Add(InstalledPrinters)
            Next InstalledPrinters

            ' Set the combo to the first printer in the list
            Me.cmbPrinterList.SelectedIndex = 0
            Me.cmbPrinterList.Text = globalVariables.DefaultPrinterName

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub
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

    Private Function IsDebtorsOutstandingHave(ByRef DaysLimit As Integer, ByRef IsShowMsg As Boolean) As Boolean
        IsDebtorsOutstandingHave = False
        Dim TransactionDays As Integer = 0
        Dim ReadingInvNo As String = ""
        Try
            strSQL = "SELECT     TBL_INVOICE_MASTER.INV_NO, TBL_RECIPTS.RECIPT_ID, TBL_INVOICE_MASTER.INV_DATE, DATEDIFF(DAY, GETDATE(), TBL_INVOICE_MASTER.INV_DATE) AS Expr1, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER LEFT OUTER JOIN  TBL_RECIPTS ON TBL_INVOICE_MASTER.COM_ID = TBL_RECIPTS.COM_ID AND TBL_INVOICE_MASTER.INV_NO = TBL_RECIPTS.INV_NO WHERE     (TBL_RECIPTS.RECIPT_ID IS NULL) and TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "' and TBL_INVOICE_MASTER.CUS_ID = '" & Trim(txtCusCode.Text) & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@TECH_CODE", Trim(txtTechCode.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader.Item("Expr1")) Then
                    TransactionDays = 0
                Else
                    TransactionDays = dbConnections.dReader.Item("Expr1")
                End If

                ReadingInvNo = dbConnections.dReader.Item("INV_NO")

                If TransactionDays >= DaysLimit Then
                    IsDebtorsOutstandingHave = True
                    Exit While
                End If

            End While
            dbConnections.dReader.Close()





            If IsShowMsg = True Then
                If IsDebtorsOutstandingHave = True Then
                    MessageBox.Show("Invoice No " & ReadingInvNo & " is not settled in " & DaysLimit & ". Please settle this before processing this internal.", "Pending Payment detected.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try

        Return IsDebtorsOutstandingHave
    End Function

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False





        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        IsNegative_Internal = ""

        dgInternal.Rows.Clear()
        Loading_Printer_List()


        isEditClicked = False
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()

        If IsDebtorsOutstandingHave(globalVariables.DebtorsCheckDayLimit, False) Then

        End If
    End Sub
    Private Function IsPrint_Enable() As Boolean
        IsPrint_Enable = False

        Try
            strSQL = "SELECT CASE WHEN EXISTS (SELECT     IR_NO FROM         TBL_INTERNAL_MAIN WHERE     (IR_STATE in ( 'PENDING FOR BELEETA UPLOAD', 'PENDING DISPATCH','INTERNAL CANCELLED' )) AND (IR_NO =@IR_NO) AND (COM_ID =@COM_ID)) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            'strSQL = "SELECT CASE WHEN EXISTS (SELECT     IR_NO FROM         TBL_INTERNAL_MAIN WHERE     (IR_STATE in ( 'INTERNAL PRINT PENDING', 'PENDING DISPATCH','INTERNAL CANCELLED' )) AND (IR_NO =@IR_NO) AND (COM_ID =@COM_ID)) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            If dbConnections.sqlCommand.ExecuteScalar Then
                IsPrint_Enable = True

            Else
                IsPrint_Enable = False
                MessageBox.Show("This Internal is in pending approval stage.", "Pending Approval.", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Return IsPrint_Enable
    End Function

    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

    Private Function Internal_Print() As Boolean
        If Trim(Me.Text) = "" Then
            Internal_Print = False
            Exit Function
        End If


        Try
            Dim path As String = ""
            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"
                If globalVariables.selectedCompanyID = "003" Then
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
                Else
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal.rpt"
                End If


                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.IR_NO} = '" & Trim(txtIRNo.Text) & "' and {TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


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
                cryRpt.PrintOptions.PrinterName = cmbPrinterList.Text

                '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
                Try
                    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
                    Dim PkSize As New System.Drawing.Printing.PaperSize
                    ObjPrinterSetting.PrinterName = cmbPrinterList.Text
                    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
                            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                            Exit For
                        End If
                    Next

                    If PkSize IsNot Nothing Then
                        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                        myAppPrintOptions.PrinterName = cmbPrinterList.Text
                        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

                    End If
                    PkSize = Nothing



                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                Dim PrtDialog = New PrintDialog
                PrtDialog.PrinterSettings.PrinterName = cmbPrinterList.Text
                cryRpt.PrintOptions.PrinterName = PrtDialog.PrinterSettings.PrinterName

                cryRpt.PrintToPrinter(1, False, 0, 0)

                path = ""
            End Using

            Internal_Print = True
        Catch ex As Exception
            Internal_Print = False
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
        Return Internal_Print
    End Function

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"



    Private Sub txtCurrentMR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrentMR.KeyPress
        generalValObj.isDigit(e)
    End Sub





    Private Sub txtCurrentMR_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentMR.TextChanged
        If Trim(txtCurrentMR.Text) = "" Then
            dgInternal.Enabled = False
        Else
            dgInternal.Enabled = True

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

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button Events  ...............................................................
    '==================================================================================================================

#Region "Button Events"

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Data grid view Events  ...............................................................
    '===================================================================================================================

#Region "Data Grid View Events"


#End Region



    Private Sub txtIRNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtIRNo.Validating

    End Sub


    Private Sub loadInternal()
        If Trim(txtIRNo.Text) = "" Then
            Exit Sub
        End If
        dgInternal.Rows.Clear()
        Try


            strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_CODE, TBL_INTERNAL_MAIN.CUS_LOC, TBL_INTERNAL_MAIN.ISSUED_TO, TBL_INTERNAL_MAIN.CURRENT_MR, TBL_INTERNAL_MAIN.IR_TYPE, TBL_INTERNAL_MAIN.IR_STATE, TBL_INTERNAL_MAIN.IR_PRINTED, MTBL_TECH_MASTER.TECH_NAME,MTBL_CUSTOMER_MASTER.CUS_NAME FROM         TBL_INTERNAL_MAIN INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID INNER JOIN                       MTBL_TECH_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_TECH_MASTER.COM_ID AND TBL_INTERNAL_MAIN.ISSUED_TO = MTBL_TECH_MASTER.TECH_CODE WHERE     (TBL_INTERNAL_MAIN.COM_ID = @COM_ID) AND (TBL_INTERNAL_MAIN.IR_NO = @IR_NO) AND (TBL_INTERNAL_MAIN.IR_PRINTED <> 1)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader


            While dbConnections.dReader.Read
                '// GET PART NAME
                txtSerial.Text = dbConnections.dReader.Item("SERIAL_NO")
                txtPNo.Text = dbConnections.dReader.Item("PN_NO")
                txtCusCode.Text = dbConnections.dReader.Item("CUS_CODE")
                txtCusName.Text = dbConnections.dReader.Item("CUS_NAME")
                txtCusAdd.Text = dbConnections.dReader.Item("CUS_LOC")
                txtCurrentMR.Text = dbConnections.dReader.Item("CURRENT_MR")
                cmbIRType.Text = dbConnections.dReader.Item("IR_TYPE")
                lblTechName.Text = dbConnections.dReader.Item("TECH_NAME")
                txtTechCode.Text = dbConnections.dReader.Item("ISSUED_TO")


            End While
            dbConnections.dReader.Close()


            strSQL = "SELECT     IR_DATE, SERIAL_NO, PN, PN_DESC, PN_QTY, PN_TYPE, PN_VALUE, MR_TO_DATE, PREVIOUS_READING, CURRENT_READING, COPIES, STD_YIELD FROM         TBL_INTERNAL_ITEMS WHERE     (COM_ID =@COM_ID) AND (IR_NO = @IR_NO)"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader.Item("PN_DESC"), dbConnections.dReader.Item("PN"), dbConnections.dReader.Item("PN_QTY"), dbConnections.dReader.Item("PN_TYPE"), dbConnections.dReader.Item("PN_VALUE"), dbConnections.dReader.Item("PREVIOUS_READING"), dbConnections.dReader.Item("COPIES"), (dbConnections.dReader.Item("STD_YIELD") * dbConnections.dReader.Item("PN_QTY")), dbConnections.dReader.Item("STD_YIELD"))

            End While
            dbConnections.dReader.Close()


        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub
    Private Sub populatreDatagrid(ByRef Desc As String, ByRef PN As String, ByRef Qty As String, ByRef Type As String, ByRef Value As String, ByRef PReading As String, ByRef Copies As String, ByRef TYield As String, ByRef Yield As String)
        dgInternal.ColumnCount = 9
        dgInternal.Rows.Add(Desc, PN, Qty, Type, Value, PReading, Copies, TYield, Yield)
    End Sub

    Private Sub btnPrintViewInternal_Click(sender As Object, e As EventArgs) Handles btnPrintViewInternal.Click

        If IsPrint_Enable() = True Then
            If Internal_Print() Then
                UpdateIRPrint()
            End If
        End If

    End Sub

    Private Function UpdateIRPrint() As Boolean
        UpdateIRPrint = False
        Try
            connectionStaet()

            errorEvent = "Save"
            strSQL = "UPDATE    TBL_INTERNAL_MAIN  SET IR_STATE =@IR_STATE, IR_PRINTED =@IR_PRINTED WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (IR_NO = @IR_NO)"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_STATE", "PENDING DISPATCH")
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_NO", Trim(txtIRNo.Text))
            dbConnections.sqlCommand.Parameters.AddWithValue("@IR_PRINTED", True)
            If dbConnections.sqlCommand.ExecuteNonQuery() Then UpdateIRPrint = True Else UpdateIRPrint = False

            If UpdateIRPrint = True Then
                MessageBox.Show("Internal Print Successful.", "Printed.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
        Return UpdateIRPrint
    End Function

    Private Sub btnUploadToBeleeta_Click(sender As Object, e As EventArgs) Handles btnUploadToBeleeta.Click
        Try
            strSQL = $"UPDATE    TBL_INTERNAL_MAIN SET 
            IR_STATE = 'UPLOADED TO BELEETA', BELEETA_REFERENCE_NO = '{Trim(txtBeleetaRefNo.Text)}', 
            BELEETA_UPLOADED_DATE = '{DateTime.Now.Date}', UPLOADED_BY = '{globalVariables.userName}', 
            IS_BELEETA_UPLOADED = '1'
            WHERE     (COM_ID = '{globalVariables.selectedCompanyID}') AND (IR_NO = '{Trim(txtIRNo.Text)}')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.ExecuteNonQuery()
            MsgBox("Successfully Updated.")
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub
End Class