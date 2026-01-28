Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmDebtorsReport
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
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"

            Case "Save"

            Case "Edit"

            Case "Delete"

            Case "Search"

            Case "Print"

        End Select
    End Sub

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"
    Private Sub frmDebtorsReport_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmDebtorsReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmDebtorsReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmDebtorsReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmDebtorsReport_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmDebtorsReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmDebtorsReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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
        FormClear()
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


    Private Sub populatreDatagrid(ByRef InvDate As String, ByRef InvNo As String, ByRef RepCode As String, ByRef RepName As String, ByRef RepDept As String, ByRef CusID As String, ByRef CusName As String, ByRef Agid As String, ByRef AgName As String, ByRef Amount As Double, ByRef Bal As Double, ByRef Rng1 As Double, ByRef Rng2 As Double, ByRef Rng3 As Double, ByRef Rng4 As Double, ByRef Rng5 As Double, ByRef Rng6 As Double, ByRef Rng7 As Double, ByRef ODD As Integer)
        dgDebtors.ColumnCount = 19
        dgDebtors.Rows.Add(InvDate, InvNo, RepCode, RepName, RepDept, CusID, CusName, Agid, AgName, Amount, Bal, Rng1, Rng2, Rng3, Rng4, Rng5, Rng6, Rng7, ODD)
    End Sub


    Dim RepQeury As String = ""
    Dim CusQuery As String = ""
    Dim InvoiceList As New List(Of String)

    Private Sub AddtoGrid()

        errorEvent = "Add to grid()"
        dgDebtors.Rows.Clear()
        InvoiceList.Clear()
        Try
            Dim Bal_Amount As Double = 0
            connectionStaet()
            '// loading previous data


            '  " & CusCodeQuery + techCodeQuery + RepCodeQuery & "
            'If Trim(txtRCusID.Text) = "" Then
            '    CusQuery = ""
            'Else
            '    CusQuery = " AND TBL_PREV_INV_HISTORY.CUS_ID ='" & Trim(txtRCusID.Text) & "'"
            'End If


            'If Trim(txtRRepCode.Text) = "" Then
            '    RepQeury = ""
            'Else
            '    RepQeury = " AND TBL_PREV_INV_HISTORY.REP_CODE = '" & Trim(txtRRepCode.Text) & "'"
            'End If


            'strSQL = "SELECT     TBL_PREV_INV_HISTORY.INV_DATE, TBL_PREV_INV_HISTORY.INV_NO, TBL_PREV_INV_HISTORY.REP_CODE, TBL_PREV_INV_HISTORY.REP_NAME, TBL_PREV_INV_HISTORY.DEPT, TBL_PREV_INV_HISTORY.CUS_ID, TBL_PREV_INV_HISTORY.CUS_NAME, TBL_PREV_INV_HISTORY.INV_VAL, TBL_PREV_INV_HISTORY.RECIPT_ID,  TBL_PREV_INV_HISTORY.FULL_PAID, TBL_RECIPT_DET.BALANCE_PAYMENT, TBL_PREV_INV_HISTORY.COM_ID FROM         TBL_PREV_INV_HISTORY FULL OUTER JOIN  TBL_RECIPT_DET ON TBL_PREV_INV_HISTORY.COM_ID = TBL_RECIPT_DET.COM_ID AND TBL_PREV_INV_HISTORY.RECIPT_ID = TBL_RECIPT_DET.RECIPT_ID WHERE     (TBL_PREV_INV_HISTORY.COM_ID = '" & globalVariables.selectedCompanyID & "')  AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) OR (TBL_RECIPT_DET.BALANCE_PAYMENT IS NULL) " & CusQuery + RepQeury & " ORDER BY TBL_PREV_INV_HISTORY.INV_NO"

            'dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
            Dim ds As New DataSet()
            Dim Inv_Value_Previous As Decimal = 0
            Dim InvAdd As String = ""

            Dim repCode As String = ""
            Dim RepName As String = ""
            Dim RepArea As String = ""

            Dim Invoicevalue As Double = 0
            Dim IsNBT As Boolean = False
            Dim IsVat As Boolean = False

            Dim Range1 As Double = 0
            Dim Range2 As Double = 0
            Dim Range3 As Double = 0
            Dim Range4 As Double = 0
            Dim Range5 As Double = 0
            Dim Range6 As Double = 0
            Dim Range7 As Double = 0

            Dim AGName As String = "N/A"


            'da.Fill(ds)

            ''fetching data from dataset in disconnected mode
            'For i = 0 To ds.Tables(0).Rows.Count - 1
            '    repCode = ""
            '    RepName = ""
            '    RepArea = ""

            '    IsNBT = False
            '    IsVat = False
            '    Invoicevalue = 0

            '    Range1 = 0
            '    Range2 = 0
            '    Range3 = 0
            '    Range4 = 0
            '    Range5 = 0
            '    Range6 = 0
            '    Range7 = 0

            '    If IsDBNull(ds.Tables(0).Rows(i).Item("INV_VAL")) Then
            '        Invoicevalue = 0
            '    Else
            '        Invoicevalue = ds.Tables(0).Rows(i).Item("INV_VAL")
            '    End If

            '    If ds.Tables(0).Rows(i).Item("COM_ID") = globalVariables.selectedCompanyID Then


            '        If Not InvoiceList.Contains(ds.Tables(0).Rows(i).Item("INV_NO")) Then
            '            InvoiceList.Add(ds.Tables(0).Rows(i).Item("INV_NO"))



            '            ' TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT
            '            Inv_Value_Previous = 0
            '            Inv_Value_Previous = GetLastBalance(ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("INV_NO"), Invoicevalue)





            '            strSQL = "SELECT     TOP (1) MTBL_TECH_MASTER.TECH_CODE , MTBL_TECH_MASTER.TECH_NAME, MTBL_TECH_MASTER.TECH_AREA, TBL_PREV_INV_HISTORY.INV_NO FROM         MTBL_TECH_MASTER INNER JOIN TBL_PREV_INV_HISTORY ON MTBL_TECH_MASTER.COM_ID = TBL_PREV_INV_HISTORY.COM_ID AND MTBL_TECH_MASTER.TECH_CODE = TBL_PREV_INV_HISTORY.REP_CODE WHERE TBL_PREV_INV_HISTORY.COM_ID = '" & globalVariables.selectedCompanyID & "' and  TBL_PREV_INV_HISTORY.INV_NO = '" & ds.Tables(0).Rows(i).Item("INV_NO") & "'"
            '            dbConnections.sqlCommand.CommandText = strSQL
            '            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            '            While dbConnections.dReader.Read
            '                If IsDBNull(dbConnections.dReader.Item("TECH_NAME")) Then
            '                    RepName = "N/A"
            '                Else
            '                    RepName = dbConnections.dReader.Item("TECH_NAME")
            '                End If
            '                If IsDBNull(dbConnections.dReader.Item("TECH_AREA")) Then
            '                    RepArea = "N/A"
            '                Else
            '                    RepArea = dbConnections.dReader.Item("TECH_AREA")
            '                End If
            '                If IsDBNull(dbConnections.dReader.Item("TECH_CODE")) Then
            '                    repCode = "N/A"
            '                Else
            '                    repCode = dbConnections.dReader.Item("TECH_CODE")
            '                End If

            '            End While
            '            dbConnections.dReader.Close()


            '            If getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "A" Then
            '                Range1 = Inv_Value_Previous
            '            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "B" Then
            '                Range2 = Inv_Value_Previous
            '            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "C" Then
            '                Range3 = Inv_Value_Previous
            '            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "D" Then
            '                Range4 = Inv_Value_Previous
            '            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "E" Then
            '                Range5 = Inv_Value_Previous
            '            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "F" Then
            '                Range6 = Inv_Value_Previous
            '            Else
            '                Range7 = Inv_Value_Previous
            '            End If
            '            Dim isRepFilter As Boolean = False
            '            Dim isCusFiler As Boolean = False

            '            If Trim(txtRRepCode.Text) = "" Then
            '                isRepFilter = False
            '            Else

            '                isRepFilter = True
            '            End If

            '            If Trim(txtRCusID.Text) = "" Then
            '                isCusFiler = False
            '            Else

            '                isCusFiler = True
            '            End If
            '            If Inv_Value_Previous <> 0 Then
            '                If isRepFilter = True And isCusFiler = True Then
            '                    If Trim(txtRCusID.Text) = ds.Tables(0).Rows(i).Item("CUS_ID") And Trim(txtRRepCode.Text) = repCode Then

            '                        populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), "N/A", "N/A", Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))

            '                    End If
            '                ElseIf isRepFilter = True Then
            '                    If Trim(txtRRepCode.Text) = repCode Then
            '                        populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), "N/A", "N/A", Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))

            '                    End If
            '                ElseIf isCusFiler = True Then
            '                    If Trim(txtRCusID.Text) = ds.Tables(0).Rows(i).Item("CUS_ID") Then
            '                        populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), "N/A", "N/A", Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))

            '                    End If
            '                Else
            '                    populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), "N/A", "N/A", Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))

            '                End If
            '            End If
            '        End If
            '    End If
            'Next



            If Trim(txtRCusID.Text) = "" Then
                CusQuery = ""
            Else
                CusQuery = " AND TBL_INVOICE_MASTER.CUS_ID ='" & Trim(txtRCusID.Text) & "'"
            End If


            If Trim(txtRRepCode.Text) = "" Then
                RepQeury = ""
            Else
                RepQeury = " AND TBL_INVOICE_MASTER.TECH_CODE = '" & Trim(txtRRepCode.Text) & "'"
            End If

            '// loading system data
            'strSQL = "SELECT     TBL_INVOICE_MASTER.AG_ID, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.TECH_CODE, TBL_RECIPT_DET.PAYMENT_AMOUNT, TBL_RECIPT_DET.BALANCE_PAYMENT, TBL_INVOICE_MASTER.REP_CODE, TBL_INVOICE_MASTER.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_NAME , TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT, TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INVOICE_MASTER.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID FULL OUTER JOIN TBL_RECIPT_DET ON TBL_INVOICE_MASTER.COM_ID = TBL_RECIPT_DET.COM_ID AND TBL_INVOICE_MASTER.INV_NO = TBL_RECIPT_DET.INV_NO WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_INVOICE_MASTER.INV_STATUS_T  <> 'CANCELLED') AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) OR (TBL_RECIPT_DET.BALANCE_PAYMENT IS NULL)  ORDER BY TBL_INVOICE_MASTER.INV_AUTO_NUM"
            'strSQL = "SELECT     TBL_INVOICE_MASTER.AG_ID, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.TECH_CODE,(SELECT  TOP 1   TBL_RECIPT_DET.PAYMENT_AMOUNT FROM         TBL_RECIPT_DET WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO)) AS 'PAYMENT_AMOUNT',(SELECT   TOP 1  TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO)) AS 'BALANCE_PAYMENT' ,  TBL_INVOICE_MASTER.REP_CODE, TBL_INVOICE_MASTER.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_NAME, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT,TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INVOICE_MASTER.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (TBL_INVOICE_MASTER.INV_STATUS_T  is null) " & CusQuery + RepQeury & " ORDER BY TBL_INVOICE_MASTER.INV_AUTO_NUM"
            'strSQL = "SELECT     TBL_INVOICE_MASTER.AG_ID, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.TECH_CODE,(SELECT  TOP 1   TBL_RECIPT_DET.PAYMENT_AMOUNT FROM         TBL_RECIPT_DET WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO)) AS 'PAYMENT_AMOUNT',(SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO) ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) AS 'BALANCE_PAYMENT' ,  TBL_INVOICE_MASTER.REP_CODE, TBL_INVOICE_MASTER.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_NAME, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT,TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INVOICE_MASTER.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (TBL_INVOICE_MASTER.INV_STATUS_T  is null) " & CusQuery + RepQeury & " ORDER BY TBL_INVOICE_MASTER.INV_AUTO_NUM"
            'strSQL = "SELECT TBL_INVOICE_MASTER.AG_ID, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO, TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.TECH_CODE,(SELECT  TOP 1   TBL_RECIPT_DET.PAYMENT_AMOUNT FROM  TBL_RECIPT_DET INNER JOIN TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO) ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) AS 'PAYMENT_AMOUNT',(SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO) ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) AS 'BALANCE_PAYMENT' ,  TBL_INVOICE_MASTER.REP_CODE, TBL_INVOICE_MASTER.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_NAME, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT,TBL_INVOICE_MASTER.COM_ID FROM         TBL_INVOICE_MASTER INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INVOICE_MASTER.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (TBL_INVOICE_MASTER.INV_STATUS_T  is null) " & CusQuery + RepQeury & " ORDER BY TBL_INVOICE_MASTER.INV_AUTO_NUM"

            If selectedCompanyID = "001" Then
                strSQL = "SELECT
    subquery.AG_ID,
    subquery.CUS_ID, 
    subquery.COM_ID, 
    subquery.TECH_CODE, 
    subquery.INV_NO, 
    subquery.INV_VAL, 
    subquery.NBT2_P, 
    subquery.[VAT_P], 
	subquery.NBT_AMOUNT,	
	subquery.INV_VAL + NBT_AMOUNT AS INVOICEWITHNBT, 
	subquery.vat_AMOUNT,   
   subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT AS FULLINVOICEVALUE, 
    subquery.INV_DATE,
    subquery.REP_CODE,
    --subquery.TECH_AREA,
    --subquery.TECH_NAME,
    subquery.IS_NBT,
    subquery.IS_VAT,
    subquery.CUS_NAME,
    subquery.PAYMENT_AMOUNT,
    (subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT )- subquery.PAYMENT_AMOUNT as balance,
	subquery.INV_STATUS_T
FROM (
    SELECT
        IM.COM_ID,
        IM.AG_ID,
        IM.CUS_ID, 
        IM.TECH_CODE,         
        CM.CUS_NAME,
        IM.INV_NO,	 
        IM.INV_VAL, 
        IM.NBT2_P, 
        IM.[VAT_P], 
        IM.INV_DATE,
        IM.REP_CODE,		
		CASE WHEN (IM.IS_NBT = 1)
		 THEN (IM.INV_VAL * IM.NBT2_P / 100)
		 ELSE (0)
		 END AS NBT_AMOUNT,	
		 CASE WHEN (IM.IS_NBT = 1)
		 THEN ((IM.INV_VAL * IM.NBT2_P / 100)+IM.INV_VAL)*IM.VAT_P/100
		 ELSE ((0)+IM.INV_VAL)*IM.VAT_P/100
		 END AS vat_AMOUNT,					         		
        --TECH_AREA,
        --TECH_NAME,		
		IM.IS_NBT,
		IM.IS_VAT,		
		IM.INV_STATUS_T,		
		(SELECT ISNULL(SUM(RD.PAYMENT_AMOUNT), 0) 
         FROM TBL_RECIPT_DET RD 
         WHERE RD.INV_NO = IM.INV_NO) AS PAYMENT_AMOUNT
    FROM 
        TBL_INVOICE_MASTER IM
       --LEFT JOIN
       -- MTBL_TECH_MASTER TM ON IM.REP_CODE = TM.TECH_CODE
    LEFT JOIN
        MTBL_CUSTOMER_MASTER CM ON IM.CUS_ID = CM.CUS_ID
    WHERE 
        (IM.FULL_PAID = '0' OR IM.FULL_PAID IS NULL) 
        AND IM.COM_ID = '" & globalVariables.selectedCompanyID & "'         
		AND (IM.INV_STATUS_T IS NULL OR IM.INV_STATUS_T = '' OR IM.INV_STATUS_T <> 'CANCELLED')
) AS subquery;"
                '                strSQL = "SELECT     TBL_INVOICE_MASTER.AG_ID, TBL_INVOICE_MASTER.CUS_ID, TBL_INVOICE_MASTER.INV_NO,
                'TBL_INVOICE_MASTER.INV_DATE, TBL_INVOICE_MASTER.TECH_CODE,(SELECT  TOP 1   TBL_RECIPT_DET.PAYMENT_AMOUNT FROM  TBL_RECIPT_DET INNER JOIN
                'TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID 
                'WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) 
                'AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO) ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) AS 'PAYMENT_AMOUNT',
                '(SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID 
                'AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID) AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO)
                'ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) AS 'BALANCE_PAYMENT' ,  TBL_INVOICE_MASTER.REP_CODE, TBL_INVOICE_MASTER.INV_VAL, MTBL_CUSTOMER_MASTER.CUS_NAME, TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT,TBL_INVOICE_MASTER.COM_ID 
                'FROM         TBL_INVOICE_MASTER INNER JOIN MTBL_CUSTOMER_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INVOICE_MASTER.CUS_ID = MTBL_CUSTOMER_MASTER.CUS_ID 
                'WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND  (TBL_INVOICE_MASTER.INV_STATUS_T  is null) " & CusQuery + RepQeury & " ORDER BY TBL_INVOICE_MASTER.INV_AUTO_NUM"
            Else
                strSQL = "SELECT
    subquery.AG_ID,
    subquery.CUS_ID, 
    subquery.COM_ID, 
    subquery.TECH_CODE, 
    subquery.INV_NO, 
    subquery.INV_VAL, 
    subquery.NBT2_P, 
    subquery.[VAT_P], 
	subquery.NBT_AMOUNT,	
	subquery.INV_VAL + NBT_AMOUNT AS INVOICEWITHNBT, 
	subquery.vat_AMOUNT,   
   subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT AS FULLINVOICEVALUE, 
    subquery.INV_DATE,
    subquery.REP_CODE,
    --subquery.TECH_AREA,
    --subquery.TECH_NAME,
    subquery.IS_NBT,
    subquery.IS_VAT,
    subquery.CUS_NAME,
    subquery.PAYMENT_AMOUNT,
    (subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT )- subquery.PAYMENT_AMOUNT as balance,
	subquery.INV_STATUS_T
FROM (
    SELECT
        IM.COM_ID,
        IM.AG_ID,
        IM.CUS_ID, 
        IM.TECH_CODE,         
        CM.CUS_NAME,
        IM.INV_NO,	 
        IM.INV_VAL, 
        IM.NBT2_P, 
        IM.[VAT_P], 
        IM.INV_DATE,
        IM.REP_CODE,		
		CASE WHEN (IM.IS_NBT = 1)
		 THEN (IM.INV_VAL * IM.NBT2_P / 100)
		 ELSE (0)
		 END AS NBT_AMOUNT,	
		 CASE WHEN (IM.IS_NBT = 1)
		 THEN ((IM.INV_VAL * IM.NBT2_P / 100)+IM.INV_VAL)*IM.VAT_P/100
		 ELSE ((0)+IM.INV_VAL)*IM.VAT_P/100
		 END AS vat_AMOUNT,					         		
        --TECH_AREA,
        --TECH_NAME,		
		IM.IS_NBT,
		IM.IS_VAT,		
		IM.INV_STATUS_T,		
		(SELECT ISNULL(SUM(RD.PAYMENT_AMOUNT), 0) 
         FROM TBL_RECIPT_DET RD 
         WHERE RD.INV_NO = IM.INV_NO) AS PAYMENT_AMOUNT
    FROM 
        TBL_INVOICE_MASTER IM
       --LEFT JOIN
       -- MTBL_TECH_MASTER TM ON IM.REP_CODE = TM.TECH_CODE
    LEFT JOIN
        MTBL_CUSTOMER_MASTER CM ON IM.CUS_ID = CM.CUS_ID
    WHERE 
        (IM.FULL_PAID = '0' OR IM.FULL_PAID IS NULL) 
        AND IM.COM_ID = '" & globalVariables.selectedCompanyID & "'         
		AND (IM.INV_STATUS_T IS NULL OR IM.INV_STATUS_T = '' OR IM.INV_STATUS_T <> 'CANCELLED')
) AS subquery;"
            End If

            '/ AND (IM.INV_TRANSACTION_STATUS IS NULL) /
            'SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '001') AND (TBL_RECIPT_DET.BALANCE_PAYMENT <> 0) AND (TBL_RECIPT_DET.INV_NO = '001/INV/1041') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            da = New SqlDataAdapter(dbConnections.sqlCommand)
            ds = New DataSet()
            Inv_Value_Previous = 0
            InvAdd = ""

            repCode = ""
            RepName = ""
            RepArea = ""

            Invoicevalue = 0
            IsNBT = False
            IsVat = False


            da.Fill(ds)

            'fetching data from dataset in disconnected mode
            For i = 0 To ds.Tables(0).Rows.Count - 1
                repCode = ""
                RepName = ""
                RepArea = ""

                IsNBT = False
                IsVat = False
                Invoicevalue = 0

                Range1 = 0
                Range2 = 0
                Range3 = 0
                Range4 = 0
                Range5 = 0
                Range6 = 0
                Range7 = 0


                If Not InvoiceList.Contains(ds.Tables(0).Rows(i).Item("INV_NO")) Then
                    InvoiceList.Add(ds.Tables(0).Rows(i).Item("INV_NO"))

                    If ds.Tables(0).Rows(i).Item("COM_ID") = globalVariables.selectedCompanyID Then

                        IsNBT = ds.Tables(0).Rows(i).Item("IS_NBT")
                        IsVat = ds.Tables(0).Rows(i).Item("IS_VAT")
                        Invoicevalue = ds.Tables(0).Rows(i).Item("FULLINVOICEVALUE")


                        'If IsNBT = True Then
                        '    Invoicevalue = Invoicevalue + ((Invoicevalue / 100) * GetNBTP(ds.Tables(0).Rows(i).Item("INV_NO")))
                        'End If

                        'If IsVat = True Then
                        '    Invoicevalue = Invoicevalue + ((Invoicevalue / 100) * GetVATP(ds.Tables(0).Rows(i).Item("INV_NO")))
                        'End If

                        ' TBL_INVOICE_MASTER.IS_NBT, TBL_INVOICE_MASTER.IS_VAT
                        Inv_Value_Previous = 0
                        Inv_Value_Previous = ds.Tables(0).Rows(i).Item("balance")
                        'Inv_Value_Previous = GetLastBalance(ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("INV_NO"), Invoicevalue)

                        If Inv_Value_Previous <> 0 Then

                            strSQL = "SELECT     TOP (1) MTBL_TECH_MASTER.TECH_NAME, MTBL_TECH_MASTER.TECH_AREA, TBL_INVOICE_MASTER.REP_CODE FROM         TBL_INVOICE_MASTER INNER JOIN  MTBL_TECH_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_TECH_MASTER.COM_ID AND TBL_INVOICE_MASTER.REP_CODE = MTBL_TECH_MASTER.TECH_CODE WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_INVOICE_MASTER.INV_NO = '" & ds.Tables(0).Rows(i).Item("INV_NO") & "')"
                            dbConnections.sqlCommand.CommandText = strSQL
                            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                            While dbConnections.dReader.Read
                                RepName = dbConnections.dReader.Item("TECH_NAME")
                                RepArea = dbConnections.dReader.Item("TECH_AREA")
                                repCode = dbConnections.dReader.Item("REP_CODE")

                            End While
                            dbConnections.dReader.Close()




                            strSQL = "SELECT     AG_NAME, AG_ID FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & ds.Tables(0).Rows(i).Item("AG_ID") & "')"
                            dbConnections.sqlCommand.CommandText = strSQL
                            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

                            While dbConnections.dReader.Read

                                If IsDBNull(dbConnections.dReader.Item("AG_NAME")) Then
                                    AGName = dbConnections.dReader.Item("AG_ID")
                                Else
                                    AGName = dbConnections.dReader.Item("AG_NAME")
                                End If

                            End While
                            dbConnections.dReader.Close()

                            If getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "A" Then
                                Range1 = Inv_Value_Previous
                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "B" Then
                                Range2 = Inv_Value_Previous
                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "C" Then
                                Range3 = Inv_Value_Previous
                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "D" Then
                                Range4 = Inv_Value_Previous
                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "E" Then
                                Range5 = Inv_Value_Previous
                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "F" Then
                                Range6 = Inv_Value_Previous
                            Else
                                Range7 = Inv_Value_Previous
                            End If

                            If ds.Tables(0).Rows(i).Item("COM_ID") = "003" Then
                                populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), ds.Tables(0).Rows(i).Item("AG_ID"), AGName, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))
                            Else

                                If CDate(ds.Tables(0).Rows(i).Item("INV_DATE")).Year = 2019 And CDate(ds.Tables(0).Rows(i).Item("INV_DATE")).Month = 6 Then

                                Else
                                    If Inv_Value_Previous <> 0 Then
                                        populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), ds.Tables(0).Rows(i).Item("AG_ID"), AGName, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))
                                    End If
                                End If

                            End If

                        End If

                        '//Testing

                    End If
                End If
            Next

        Catch ex As Exception
            dbConnections.dReader.Close()
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
    'Private Async Sub AddtoGrid()
    '    errorEvent = "Add to grid()"
    '    dgDebtors.Rows.Clear()
    '    InvoiceList.Clear()
    '    Try
    '        Dim apiUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/debtors/getdebtorreport?companyID={globalVariables.selectedCompanyID}"
    '        Using client As New HttpClient()
    '            Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
    '            Dim rowCount As Integer = 0
    '            Dim Bal_Amount As Double = 0

    '            Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
    '            Dim ds As New DataSet()
    '            Dim Inv_Value_Previous As Decimal = 0
    '            Dim InvAdd As String = ""

    '            Dim repCode As String = ""
    '            Dim RepName As String = ""
    '            Dim RepArea As String = ""

    '            Dim Invoicevalue As Double = 0
    '            Dim IsNBT As Boolean = False
    '            Dim IsVat As Boolean = False

    '            Dim Range1 As Double = 0
    '            Dim Range2 As Double = 0
    '            Dim Range3 As Double = 0
    '            Dim Range4 As Double = 0
    '            Dim Range5 As Double = 0
    '            Dim Range6 As Double = 0
    '            Dim Range7 As Double = 0
    '            Dim CustomerID As String = 0
    '            Dim AGName As String = "N/A"
    '            Dim CustomerName As String = ""
    '            If response.IsSuccessStatusCode Then
    '                Dim json As String = Await response.Content.ReadAsStringAsync()
    '                Dim data As List(Of DebtorReportVM) = JsonConvert.DeserializeObject(Of List(Of DebtorReportVM))(json)
    '                Inv_Value_Previous = 0
    '                InvAdd = ""

    '                repCode = ""
    '                RepName = ""
    '                RepArea = ""

    '                Invoicevalue = 0
    '                IsNBT = False
    '                IsVat = False

    '                dgDebtors.Rows.Clear()
    '                For Each item As DebtorReportVM In data
    '                    repCode = ""
    '                    RepName = ""
    '                    RepArea = ""
    '                    CustomerID = item.CustomerID
    '                    CustomerName = CustomerName
    '                    IsNBT = False
    '                    IsVat = False
    '                    Invoicevalue = 0
    '                    Range1 = 0
    '                    Range2 = 0
    '                    Range3 = 0
    '                    Range4 = 0
    '                    Range5 = 0
    '                    Range6 = 0
    '                    Range7 = 0
    '                    Dim agreementID As String = ""
    '                    Dim agreementName As String = ""
    '                    agreementID = item.AgreementID
    '                    If Not InvoiceList.Contains(item.InvoiceNo) Then
    '                        InvoiceList.Add(item.InvoiceNo)
    '                        If item.ComId = globalVariables.selectedCompanyID Then
    '                            IsNBT = item.IsNbt
    '                            IsVat = item.IsVat
    '                            Invoicevalue = item.FullInvoiceValue
    '                            Inv_Value_Previous = 0
    '                            Inv_Value_Previous = item.Balance
    '                            If Inv_Value_Previous <> 0 Then
    '                                Dim techMasterAPIUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/debtors/gettechmaster?companyID={globalVariables.selectedCompanyID}&invoiceNo={item.InvoiceNo}"
    '                                Dim techMasterResponse As HttpResponseMessage = Await client.GetAsync(techMasterAPIUrl)
    '                                If techMasterResponse.IsSuccessStatusCode Then
    '                                    Dim techMasterJson As String = Await techMasterResponse.Content.ReadAsStringAsync()
    '                                    Dim techMasterDataList As List(Of TechMasterVM) = JsonConvert.DeserializeObject(Of List(Of TechMasterVM))(techMasterJson)
    '                                    For Each item01 As TechMasterVM In techMasterDataList
    '                                        repCode = item01.REP_CODE
    '                                        RepArea = item01.TECH_AREA
    '                                        RepName = item01.TECH_NAME
    '                                    Next
    '                                End If

    '                                Dim agreementMasterAPIUrl As String = $"{dbconnections.kbcoAPIEndPoint}/api/debtors/getcustomeragreement?companyID={globalVariables.selectedCompanyID}&agreementID={agreementID}"
    '                                Dim agreementMasterResponse As HttpResponseMessage = Await client.GetAsync(agreementMasterAPIUrl)
    '                                If agreementMasterResponse.IsSuccessStatusCode Then
    '                                    Dim jsonString As String = Await agreementMasterResponse.Content.ReadAsStringAsync()
    '                                    Dim jsonArray As JArray = JArray.Parse(jsonString)
    '                                    Dim agreementObject As Object = jsonArray(0)
    '                                    agreementID = agreementObject("AG_ID").ToString()
    '                                    agreementName = agreementObject("AG_NAME").ToString()
    '                                End If
    '                                Dim invoiceDateAPI As String = $"{dbconnections.kbcoAPIEndPoint}/api/debtors/getcustomeragreement?companyID={globalVariables.selectedCompanyID}&agreementID={agreementID}"
    '                                Dim invoiceDateResponse As HttpResponseMessage = Await client.GetAsync(agreementMasterAPIUrl)
    '                                If invoiceDateResponse.IsSuccessStatusCode Then
    '                                    Dim jsonString As String = Await invoiceDateResponse.Content.ReadAsStringAsync()
    '                                    Dim techMasterDataList As List(Of DebtorReportVM) = JsonConvert.DeserializeObject(Of List(Of DebtorReportVM))(jsonString)
    '                                    For Each item02 As DebtorReportVM In techMasterDataList
    '                                        If getDebtors_outdate_range(item.InvDate) = "A" Then
    '                                            Range1 = Inv_Value_Previous
    '                                        ElseIf getDebtors_outdate_range(item.InvDate) = "B" Then
    '                                            Range2 = Inv_Value_Previous
    '                                        ElseIf getDebtors_outdate_range(item.InvDate) = "C" Then
    '                                            Range3 = Inv_Value_Previous
    '                                        ElseIf getDebtors_outdate_range(item.InvDate) = "D" Then
    '                                            Range4 = Inv_Value_Previous
    '                                        ElseIf getDebtors_outdate_range(item.InvDate) = "E" Then
    '                                            Range5 = Inv_Value_Previous
    '                                        ElseIf getDebtors_outdate_range(item.InvDate) = "F" Then
    '                                            Range6 = Inv_Value_Previous
    '                                        Else
    '                                            Range7 = Inv_Value_Previous
    '                                        End If
    '                                    Next
    '                                    If item.ComId = "003" Then
    '                                        populatreDatagrid(item.InvDate, item.InvoiceNo, repCode, RepName, RepArea, CustomerID, item.CustomerName, item.AgreementID, item.AgreementID, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(item.InvDate))
    '                                    Else
    '                                        If item.ComId = "003" Then
    '                                            populatreDatagrid(item.InvDate, item.InvoiceNo, repCode, RepName, RepArea, CustomerID, item.CustomerName, item.AgreementID, item.AgreementID, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(item.InvDate))
    '                                        Else

    '                                            If CDate(item.InvDate).Year = 2019 And CDate(item.InvDate).Month = 6 Then

    '                                            Else
    '                                                If Inv_Value_Previous <> 0 Then
    '                                                    populatreDatagrid(item.InvDate, item.InvoiceNo, repCode, RepName, RepArea, CustomerID, item.CustomerID, item.AgreementID, item.AgreementID, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(item.InvDate))
    '                                                End If
    '                                            End If

    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    'End Sub
    '    Private Sub AddtoGrid()

    '        errorEvent = "Add to grid()"
    '        dgDebtors.Rows.Clear()
    '        InvoiceList.Clear()
    '        Try
    '            Dim Bal_Amount As Double = 0
    '            connectionStaet()

    '            Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
    '            Dim ds As New DataSet()
    '            Dim Inv_Value_Previous As Decimal = 0
    '            Dim InvAdd As String = ""

    '            Dim repCode As String = ""
    '            Dim RepName As String = ""
    '            Dim RepArea As String = ""

    '            Dim Invoicevalue As Double = 0
    '            Dim IsNBT As Boolean = False
    '            Dim IsVat As Boolean = False

    '            Dim Range1 As Double = 0
    '            Dim Range2 As Double = 0
    '            Dim Range3 As Double = 0
    '            Dim Range4 As Double = 0
    '            Dim Range5 As Double = 0
    '            Dim Range6 As Double = 0
    '            Dim Range7 As Double = 0

    '            Dim AGName As String = "N/A"



    '            If Trim(txtRCusID.Text) = "" Then
    '                CusQuery = ""
    '            Else
    '                CusQuery = " AND TBL_INVOICE_MASTER.CUS_ID ='" & Trim(txtRCusID.Text) & "'"
    '            End If


    '            If Trim(txtRRepCode.Text) = "" Then
    '                RepQeury = ""
    '            Else
    '                RepQeury = " AND TBL_INVOICE_MASTER.TECH_CODE = '" & Trim(txtRRepCode.Text) & "'"
    '            End If

    '            strSQL = "SELECT
    '    subquery.AG_ID,
    '    subquery.CUS_ID, 
    '    subquery.COM_ID, 
    '    subquery.TECH_CODE, 
    '    subquery.INV_NO, 
    '    subquery.INV_VAL, 
    '    subquery.NBT2_P, 
    '    subquery.[VAT_P], 
    '	subquery.NBT_AMOUNT,	
    '	subquery.INV_VAL + NBT_AMOUNT AS INVOICEWITHNBT, 
    '	subquery.vat_AMOUNT,   
    '   subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT AS FULLINVOICEVALUE, 
    '    subquery.INV_DATE,
    '    subquery.REP_CODE,
    '    --subquery.TECH_AREA,
    '    --subquery.TECH_NAME,
    '    subquery.IS_NBT, 
    '    subquery.IS_VAT,
    '    subquery.CUS_NAME,
    '    subquery.PAYMENT_AMOUNT,
    '    (subquery.INV_VAL + subquery.NBT_AMOUNT +subquery.vat_AMOUNT )- subquery.PAYMENT_AMOUNT as balance,
    '	subquery.INV_STATUS_T
    'FROM (
    '    SELECT
    '        IM.COM_ID,
    '        IM.AG_ID,
    '        IM.CUS_ID, 
    '        IM.TECH_CODE,         
    '        CM.CUS_NAME,
    '        IM.INV_NO,	 
    '        IM.INV_VAL, 
    '        IM.NBT2_P, 
    '        IM.[VAT_P], 
    '        IM.INV_DATE,
    '        IM.REP_CODE,		
    '		CASE WHEN (IM.IS_NBT = 1)
    '		 THEN (IM.INV_VAL * IM.NBT2_P / 100)
    '		 ELSE (0)
    '		 END AS NBT_AMOUNT,	
    '		 CASE WHEN (IM.IS_NBT = 1)
    '		 THEN ((IM.INV_VAL * IM.NBT2_P / 100)+IM.INV_VAL)*IM.VAT_P/100
    '		 ELSE ((0)+IM.INV_VAL)*IM.VAT_P/100
    '		 END AS vat_AMOUNT,					         		
    '        --TECH_AREA,
    '        --TECH_NAME,		
    '		IM.IS_NBT,
    '		IM.IS_VAT,		
    '		IM.INV_STATUS_T,		
    '		(SELECT ISNULL(SUM(RD.PAYMENT_AMOUNT), 0) 
    '         FROM TBL_RECIPT_DET RD 
    '         WHERE RD.INV_NO = IM.INV_NO) AS PAYMENT_AMOUNT
    '    FROM 
    '        TBL_INVOICE_MASTER IM    
    '       --LEFT JOIN
    '       -- MTBL_TECH_MASTER TM ON IM.REP_CODE = TM.TECH_CODE
    '    LEFT JOIN
    '        MTBL_CUSTOMER_MASTER CM ON IM.CUS_ID = CM.CUS_ID
    '    WHERE 
    '        (IM.FULL_PAID = '0' OR IM.FULL_PAID IS NULL) 
    '        AND IM.COM_ID = '" & globalVariables.selectedCompanyID & "'         
    '		AND (IM.INV_STATUS_T IS NULL OR IM.INV_STATUS_T = '' OR IM.INV_STATUS_T <> 'CANCELLED')
    ') AS subquery;"
    '            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
    '            da = New SqlDataAdapter(dbConnections.sqlCommand)
    '            ds = New DataSet()
    '            Inv_Value_Previous = 0
    '            InvAdd = ""

    '            repCode = ""
    '            RepName = ""
    '            RepArea = ""

    '            Invoicevalue = 0
    '            IsNBT = False
    '            IsVat = False


    '            da.Fill(ds)

    '            'fetching data from dataset in disconnected mode
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                repCode = ""
    '                RepName = ""
    '                RepArea = ""

    '                IsNBT = False
    '                IsVat = False
    '                Invoicevalue = 0

    '                Range1 = 0
    '                Range2 = 0
    '                Range3 = 0
    '                Range4 = 0
    '                Range5 = 0
    '                Range6 = 0
    '                Range7 = 0


    '                If Not InvoiceList.Contains(ds.Tables(0).Rows(i).Item("INV_NO")) Then
    '                    InvoiceList.Add(ds.Tables(0).Rows(i).Item("INV_NO"))

    '                    If ds.Tables(0).Rows(i).Item("COM_ID") = globalVariables.selectedCompanyID Then

    '                        IsNBT = ds.Tables(0).Rows(i).Item("IS_NBT")
    '                        IsVat = ds.Tables(0).Rows(i).Item("IS_VAT")
    '                        Invoicevalue = ds.Tables(0).Rows(i).Item("FULLINVOICEVALUE")



    '                        Inv_Value_Previous = 0
    '                        Inv_Value_Previous = ds.Tables(0).Rows(i).Item("balance")



    '                        If Inv_Value_Previous <> 0 Then

    '                            strSQL = "SELECT     TOP (1) MTBL_TECH_MASTER.TECH_NAME, MTBL_TECH_MASTER.TECH_AREA, TBL_INVOICE_MASTER.REP_CODE FROM         TBL_INVOICE_MASTER INNER JOIN  MTBL_TECH_MASTER ON TBL_INVOICE_MASTER.COM_ID = MTBL_TECH_MASTER.COM_ID AND TBL_INVOICE_MASTER.REP_CODE = MTBL_TECH_MASTER.TECH_CODE WHERE     (TBL_INVOICE_MASTER.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_INVOICE_MASTER.INV_NO = '" & ds.Tables(0).Rows(i).Item("INV_NO") & "')"
    '                            dbConnections.sqlCommand.CommandText = strSQL
    '                            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                            While dbConnections.dReader.Read
    '                                RepName = dbConnections.dReader.Item("TECH_NAME")
    '                                RepArea = dbConnections.dReader.Item("TECH_AREA")
    '                                repCode = dbConnections.dReader.Item("REP_CODE")

    '                            End While
    '                            dbConnections.dReader.Close()




    '                            strSQL = "SELECT     AG_NAME, AG_ID FROM         TBL_CUS_AGREEMENT WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (AG_ID = '" & ds.Tables(0).Rows(i).Item("AG_ID") & "')"
    '                            dbConnections.sqlCommand.CommandText = strSQL
    '                            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '                            While dbConnections.dReader.Read

    '                                If IsDBNull(dbConnections.dReader.Item("AG_NAME")) Then
    '                                    AGName = dbConnections.dReader.Item("AG_ID")
    '                                Else
    '                                    AGName = dbConnections.dReader.Item("AG_NAME")
    '                                End If

    '                            End While
    '                            dbConnections.dReader.Close()

    '                            If getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "A" Then
    '                                Range1 = Inv_Value_Previous
    '                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "B" Then
    '                                Range2 = Inv_Value_Previous
    '                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "C" Then
    '                                Range3 = Inv_Value_Previous
    '                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "D" Then
    '                                Range4 = Inv_Value_Previous
    '                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "E" Then
    '                                Range5 = Inv_Value_Previous
    '                            ElseIf getDebtors_outdate_range(ds.Tables(0).Rows(i).Item("INV_DATE")) = "F" Then
    '                                Range6 = Inv_Value_Previous
    '                            Else
    '                                Range7 = Inv_Value_Previous
    '                            End If

    '                            If ds.Tables(0).Rows(i).Item("COM_ID") = "003" Then
    '                                populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), ds.Tables(0).Rows(i).Item("AG_ID"), AGName, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))
    '                            Else

    '                                If CDate(ds.Tables(0).Rows(i).Item("INV_DATE")).Year = 2019 And CDate(ds.Tables(0).Rows(i).Item("INV_DATE")).Month = 6 Then

    '                                Else
    '                                    If Inv_Value_Previous <> 0 Then
    '                                        populatreDatagrid(ds.Tables(0).Rows(i).Item("INV_DATE"), ds.Tables(0).Rows(i).Item("INV_NO"), repCode, RepName, RepArea, ds.Tables(0).Rows(i).Item("CUS_ID"), ds.Tables(0).Rows(i).Item("CUS_NAME"), ds.Tables(0).Rows(i).Item("AG_ID"), AGName, Invoicevalue, Inv_Value_Previous, Range1, Range2, Range3, Range4, Range5, Range6, Range7, getDebtors_outdate_Dasy(ds.Tables(0).Rows(i).Item("INV_DATE")))
    '                                    End If
    '                                End If

    '                            End If

    '                        End If

    '                        '//Testing

    '                    End If
    '                End If
    '            Next

    '        Catch ex As Exception
    '            dbConnections.dReader.Close()
    '            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

    '        Finally
    '            dbConnections.dReader.Close()
    '            connectionClose()
    '        End Try
    '    End Sub


    Private Sub Load_Other_info()
        For i = 0 To dgDebtors.Rows.Count - 1
            'dgDebtors.Rows(i).Cells("AMO").Value
        Next

    End Sub

    Private Function GetLastBalance(ByRef CusID As String, ByRef InvNo As String, ByRef PreviousBalance As Decimal) As Decimal
        GetLastBalance = PreviousBalance

        Try

            'strSQL = "SELECT CASE WHEN EXISTS (SELECT     TOP (1) BALANCE_PAYMENT FROM         TBL_RECIPT_DET WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (CUS_ID = '" & Trim(CusID) & "') AND INV_NO = '" & Trim(InvNo) & "' ORDER BY RECIPT_ID DESC) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"
            'SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '001') AND (TBL_RECIPT_DET.CUS_ID = '591') AND (TBL_RECIPT_DET.INV_NO = '001/INV/1041') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC
            strSQL = "SELECT CASE WHEN EXISTS (SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_RECIPT_DET.CUS_ID = '" & Trim(CusID) & "') AND (TBL_RECIPT_DET.INV_NO =  '" & Trim(InvNo) & "') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC) THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

            If dbConnections.sqlCommand.ExecuteScalar = True Then
                strSQL = "SELECT     TOP (1) TBL_RECIPT_DET.BALANCE_PAYMENT FROM         TBL_RECIPT_DET INNER JOIN  TBL_RECIPT_MASTER ON TBL_RECIPT_DET.COM_ID = TBL_RECIPT_MASTER.COM_ID AND TBL_RECIPT_DET.RECIPT_ID = TBL_RECIPT_MASTER.RECIPT_ID WHERE     (TBL_RECIPT_DET.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_RECIPT_DET.CUS_ID = '" & Trim(CusID) & "') AND (TBL_RECIPT_DET.INV_NO =  '" & Trim(InvNo) & "') ORDER BY TBL_RECIPT_MASTER.RECIPT_DATE DESC"
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

        dgDebtors.Rows.Clear()
        '//AddtoGrid()
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(True, True, False, False, False, False)
        Me.saveBtnStatus()
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

#End Region



    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

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



    Private Function getDebtors_outdate_range(ByRef InvDate As Date) As String
        getDebtors_outdate_range = "A"
        'Dim TotalDays As Integer = 0
        'Dim dt1 As DateTime = Convert.ToDateTime(InvDate)

        'Dim dt2 As DateTime = Convert.ToDateTime(Today.Date)

        'Dim ts As TimeSpan = dt2.Subtract(dt1)



        Dim d1 As DateTime = InvDate
        Dim d2 As DateTime = Today.Date
        Dim TTF As New TimeSpan
        TTF = d2.Subtract(d1)



        Select Case CInt(TTF.TotalDays)
            Case 0 To 15
                getDebtors_outdate_range = "A"
            Case 16 To 30
                getDebtors_outdate_range = "B"
            Case 31 To 60
                getDebtors_outdate_range = "C"
            Case 61 To 90
                getDebtors_outdate_range = "D"
            Case 91 To 180
                getDebtors_outdate_range = "E"
            Case 181 To 365
                getDebtors_outdate_range = "F"
            Case Else
                getDebtors_outdate_range = "G"
        End Select

        ''1-15 - A
        ''16-30 - B
        ''31-60 - C
        ''61-90 - D
        ''91-180 - E
        ''181-365 - F
        ''365-abv - G

        Return getDebtors_outdate_range
    End Function


    Private Function getDebtors_outdate_Dasy(ByRef InvDate As Date) As Integer
        getDebtors_outdate_Dasy = 0
        Dim TotalDays As Integer = 0
        Dim dt1 As DateTime = Convert.ToDateTime(InvDate)

        Dim dt2 As DateTime = Convert.ToDateTime(Today.Date)

        Dim ts As TimeSpan = dt2.Subtract(dt1)



        Return Convert.ToInt32(ts.Days)
    End Function


    Private Sub btnProcess_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (*.xls)|*.xls"
        'sfd.FileName = "" & Trim(txtRepCode.Text) & "SOBackup.xls"
        If sfd.ShowDialog() = DialogResult.OK Then
            ToCsV(dgDebtors, sfd.FileName)
        End If
    End Sub

    Private Sub ToCsV(dGV As DataGridView, filename As String)
        Dim stOutput As String = ""
        ' Export titles:
        Dim sHeaders As String = ""

        For j As Integer = 0 To dGV.Columns.Count - 1
            sHeaders = sHeaders.ToString() & Convert.ToString(dGV.Columns(j).HeaderText) & vbTab
        Next
        stOutput += sHeaders & vbCr & vbLf
        ' Export data.
        For i As Integer = 0 To dGV.RowCount - 1
            Dim stLine As String = ""
            For j As Integer = 0 To dGV.Rows(i).Cells.Count - 1
                stLine = stLine.ToString() & Convert.ToString(dGV.Rows(i).Cells(j).Value) & vbTab
            Next
            stOutput += stLine & vbCr & vbLf
        Next
        Dim utf16 As Encoding = Encoding.GetEncoding(1254)
        Dim output As Byte() = utf16.GetBytes(stOutput)
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)
        bw.Write(output, 0, output.Length)
        'write the encoded file
        bw.Flush()
        bw.Close()
        fs.Close()

    End Sub

    Private Sub btnProcess_Click_1(sender As Object, e As EventArgs) Handles btnProcess.Click
        AddtoGrid()
    End Sub

    Private Sub txtRCusID_TextChanged(sender As Object, e As EventArgs) Handles txtRCusID.TextChanged

    End Sub



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

    Private Sub dgDebtors_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgDebtors.CellContentClick

    End Sub
End Class

Public Class TechMasterVM
    Public Property TECH_NAME As String
    Public Property TECH_AREA As String
    Public Property REP_CODE As String
End Class

Public Class DebtorReportVM
    Public Property CustomerID As String
    Public Property CustomerName As String
    Public Property AgreementID As String
    Public Property InvoiceNo As String
    Public Property ComId As String
    Public Property IsNbt As Boolean
    Public Property IsVat As Boolean
    Public Property InvDate As DateTime
    Public Property FullInvoiceValue As Double
    Public Property Balance As Double
End Class