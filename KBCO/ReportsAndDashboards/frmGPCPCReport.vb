Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Text
Public Class frmGPCPCReport

   



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
    Private Sub frmGPCPCReport_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmGPCPCReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmGPCPCReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmGPCPCReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmGPCPCReport_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmGPCPCReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmGPCPCReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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


    Private Sub populatreDatagrid(ByRef Dept As String, ByRef techCode As String, ByRef techName As String, ByRef CopyVol As Integer, ByRef InternalCost As Double, ByRef CPC As Double)
        dgCPC.ColumnCount = 6
        dgCPC.Rows.Add(Dept, techCode, techName, CopyVol, InternalCost, CPC)
    End Sub


    Dim RepQeury As String = ""
    Dim CusQuery As String = ""
    Dim InvoiceList As New List(Of String)

    Private Sub AddtoGrid()

        errorEvent = "Add to grid()"
        dgCPC.Rows.Clear()

        Try
            Dim Bal_Amount As Double = 0
            connectionStaet()
            '// loading previous data




            Dim Dept As String = ""
            Dim TechName As String = ""
            Dim techCode As String = ""
            Dim InvCC As Integer = 0
            Dim InternalCOst As Double = 0.0
            Dim techQuery As String = ""
            Dim DateQuery As String = ""

            If Trim(txtTechCode.Text) = "" Then
                techQuery = ""
            Else
                techQuery = " AND TECH_CODE='" & Trim(txtTechCode.Text) & "'"
            End If




            strSQL = "SELECT     TECH_CODE, TECH_NAME, TECH_AREA FROM         MTBL_TECH_MASTER WHERE     (COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TECH_ACTIVE = 1) " & techQuery & " ORDER BY TECH_AREA"

            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            Dim da As New SqlDataAdapter(dbConnections.sqlCommand)
            Dim ds As New DataSet()
           

            da.Fill(ds)

            'fetching data from dataset in disconnected mode
            For i = 0 To ds.Tables(0).Rows.Count - 1
               

                InvCC = 0
                InternalCOst = 0

                If IsDBNull(ds.Tables(0).Rows(i).Item("TECH_CODE")) Then
                    techCode = "N/A"
                Else
                    techCode = ds.Tables(0).Rows(i).Item("TECH_CODE")
                End If

                If IsDBNull(ds.Tables(0).Rows(i).Item("TECH_NAME")) Then
                    TechName = "N/A"
                Else
                    TechName = ds.Tables(0).Rows(i).Item("TECH_NAME")
                End If
                If IsDBNull(ds.Tables(0).Rows(i).Item("TECH_AREA")) Then
                    Dept = "N/A"
                Else
                    Dept = ds.Tables(0).Rows(i).Item("TECH_AREA")
                End If
             
                strSQL = "SELECT     SUM((ISNULL(TBL_INVOICE_DET.INV_COPIES, 0) + ISNULL(TBL_INVOICE_DET.COLOR_INV_COPIES, 0)) - (ISNULL(TBL_INVOICE_DET.WAISTAGE, 0)  + ISNULL(TBL_INVOICE_DET.COLOR_WAISTAGE, 0))) AS Expr1 FROM         TBL_INVOICE_DET INNER JOIN   TBL_INVOICE_MASTER ON TBL_INVOICE_DET.COM_ID = TBL_INVOICE_MASTER.COM_ID AND TBL_INVOICE_DET.INV_NO = TBL_INVOICE_MASTER.INV_NO WHERE     (TBL_INVOICE_DET.COM_ID = '" & globalVariables.selectedCompanyID & "') AND (TBL_INVOICE_MASTER.REP_CODE = '" & techCode & "')  AND TBL_INVOICE_MASTER.INV_DATE between '" & Format(dtpStartDate.Value, "yyyy-MM-dd") & "' and '" & Format(dtpEndDate.Value, "yyyy-MM-dd") & "'"

                dbConnections.sqlCommand.CommandText = strSQL

                If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                    InvCC = 0
                Else
                    InvCC = dbConnections.sqlCommand.ExecuteScalar
                End If


                strSQL = "SELECT    SUM( (TBL_INTERNAL_ITEMS.PN_VALUE * TBL_INTERNAL_ITEMS.PN_QTY)) as 'price' FROM         TBL_INTERNAL_ITEMS INNER JOIN TBL_INTERNAL_MAIN ON TBL_INTERNAL_ITEMS.IR_NO = TBL_INTERNAL_MAIN.IR_NO AND TBL_INTERNAL_ITEMS.COM_ID = TBL_INTERNAL_MAIN.COM_ID WHERE TBL_INTERNAL_MAIN.COM_ID = '" & globalVariables.selectedCompanyID & "' AND TBL_INTERNAL_MAIN.ISSUED_TO = '" & techCode & "'  AND TBL_INTERNAL_MAIN.IR_DATE between '" & Format(dtpStartDate.Value, "yyyy-MM-dd") & "' and '" & Format(dtpEndDate.Value, "yyyy-MM-dd") & "'"
                dbConnections.sqlCommand.CommandText = strSQL
                If IsDBNull(dbConnections.sqlCommand.ExecuteScalar) Then
                    InternalCOst = 0
                Else
                    InternalCOst = dbConnections.sqlCommand.ExecuteScalar
                End If

                populatreDatagrid(Dept.ToUpper, techCode, TechName, InvCC, InternalCOst, Format((InternalCOst / InvCC), "0.00"))


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


    'Private Sub Load_Other_info()
    '    For i = 0 To dgDebtors.Rows.Count - 1
    '        'dgDebtors.Rows(i).Cells("AMO").Value
    '    Next

    'End Sub

  
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

        dgCPC.Rows.Clear()
        AddtoGrid()
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






    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (*.xls)|*.xls"
        'sfd.FileName = "" & Trim(txtRepCode.Text) & "SOBackup.xls"
        If sfd.ShowDialog() = DialogResult.OK Then
            ToCsV(dgCPC, sfd.FileName)
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
End Class