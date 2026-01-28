Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Imports System.Threading.Tasks
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Data.Common
Imports System.Security.Permissions

Public Class frmInternalQ


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
    Private Sub frmInternalRequest_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmInternalRequest_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmInternalRequest_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInternalRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmInternalRequest_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmInternalRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmInternalRequest_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
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

    Private Sub populatreDatagrid(ByRef IRNo As String, ByRef IRDate As String, ByRef PNo As String, ByRef SN As String, ByRef CusName As String, ByRef MLoc As String, ByRef IrStatus As String)
        dgApprovedInternalQ.ColumnCount = 7
        dgApprovedInternalQ.Rows.Add(IRNo, IRDate, PNo, SN, CusName, MLoc, IrStatus)
    End Sub

    Private Function GetCostCalculation(IrNo As String) As Decimal
        Try
            Dim costValue As Decimal = 0
            Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/getinternalcost?companyID={globalVariables.selectedCompanyID}&InternalRequestsNo={IrNo}"

            Using client As New HttpClient()
                ' Optional: Set request headers if needed
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/json"))

                Dim response As HttpResponseMessage = client.GetAsync(apiUrl).Result

                If response.IsSuccessStatusCode Then
                    Dim result As String = response.Content.ReadAsStringAsync().Result
                    Decimal.TryParse(result, costValue)
                Else
                    MessageBox.Show($"API call failed. Status Code: {response.StatusCode}")
                End If
            End Using

            Return costValue
        Catch ex As Exception
            MessageBox.Show($"Error retrieving cost: {ex.Message}")
            Return 0
        End Try
    End Function


    Private Async Function AddtoGrid() As Task
        'Changes made on 06-06-2025
        'Converting the data adding process to API level 
        'Changes made by Gagan Tillekeratne.

        'Dim apiUrl As String = $"{dbconnections.kbcoAPIEndPoint}/getinternalrequests?companyID={globalVariables.selectedCompanyID}"
        Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/getinternalrequests?companyID={globalVariables.selectedCompanyID}"

        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                Dim rowCount As Integer = 0
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim data As List(Of InternalRequestDto) = JsonConvert.DeserializeObject(Of List(Of InternalRequestDto))(json)
                    dgApprovedInternalQ.Rows.Clear()
                    For Each item As InternalRequestDto In data
                        Dim splitIRNO() As String = item.IR_NO.Split("/"c)
                        Dim companyID As String = splitIRNO(0)
                        If companyID = globalVariables.selectedCompanyID Then
                            Dim status As String = item.IR_STATE.Trim()
                            ' Set row color based on IR_STATE
                            Dim newRowIndex As Integer = dgApprovedInternalQ.Rows.Add(item.IR_NO, item.IR_DATE, item.PN_NO, item.TOTAL_COST, item.SERIAL_NO, item.BELEETA_REFERENCE_NO, item.CUS_NAME, item.CUS_LOC, item.IR_STATE)
                            Select Case status
                                Case "INTERNAL PRINT PENDING"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.LightSeaGreen
                                Case "INTERNAL PENDING PRINT"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.LightSeaGreen
                                Case "PENDING APPROVAL"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                                Case "PENDING GM APPROVAL"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.LightCoral
                                Case "PENDING DISPATCH"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.Thistle
                                Case "ISSUED"
                                    dgApprovedInternalQ.Rows(newRowIndex).DefaultCellStyle.BackColor = Color.SteelBlue
                            End Select
                        End If
                    Next

                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Function



    'Private Sub AddtoGrid()
    '    Dim rowCount As Integer = 0
    '    errorEvent = "Add to grid()"
    '    dgApprovedInternalQ.Rows.Clear()
    '    Try
    '        connectionStaet()

    '        'strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_LOC,   MTBL_CUSTOMER_MASTER.CUS_NAME,TBL_INTERNAL_MAIN.IR_STATE  FROM  TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.COM_ID ='" & Trim(globalVariables.selectedCompanyID) & "') AND (TBL_INTERNAL_MAIN.IR_STATE IN ('INTERNAL PRINT PENDING','PENDING APPROVAL','PENDING GM APPROVAL','PENDING DISPATCH','REJECT') ) AND TBL_INTERNAL_MAIN.IR_PRINTED <> 1"
    '        strSQL = "SELECT     TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_LOC,   MTBL_CUSTOMER_MASTER.CUS_NAME,TBL_INTERNAL_MAIN.IR_STATE  FROM  TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.COM_ID ='" & Trim(globalVariables.selectedCompanyID) & "') AND (TBL_INTERNAL_MAIN.IR_STATE IN ('INTERNAL PRINT PENDING','PENDING APPROVAL','PENDING GM APPROVAL','PENDING DISPATCH','REJECT') ) AND TBL_INTERNAL_MAIN.IR_PRINTED <> 1"
    '        dbConnections.sqlCommand.CommandText = strSQL
    '        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

    '        While dbConnections.dReader.Read
    '            populatreDatagrid(dbConnections.dReader.Item("IR_NO"), CDate(dbConnections.dReader.Item("IR_DATE")).ToShortDateString, dbConnections.dReader.Item("PN_NO"), dbConnections.dReader.Item("SERIAL_NO"), dbConnections.dReader.Item("CUS_NAME"), dbConnections.dReader.Item("CUS_LOC"), dbConnections.dReader.Item("IR_STATE"))

    '            If dbConnections.dReader.Item("IR_STATE") = "INTERNAL PRINT PENDING" Then
    '                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightSeaGreen
    '            ElseIf dbConnections.dReader.Item("IR_STATE") = "PENDING APPROVAL" Then
    '                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightGoldenrodYellow

    '            ElseIf dbConnections.dReader.Item("IR_STATE") = "PENDING GM APPROVAL" Then
    '                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightCoral

    '            ElseIf dbConnections.dReader.Item("IR_STATE") = "PENDING DISPATCH" Then
    '                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.Thistle
    '            ElseIf dbConnections.dReader.Item("IR_STATE") = "ISSUED" Then
    '                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.SteelBlue
    '            End If

    '            rowCount = rowCount + 1
    '        End While

    '    Catch ex As Exception
    '        dbConnections.dReader.Close()
    '        inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
    '        MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

    '    Finally
    '        dbConnections.dReader.Close()
    '        connectionClose()
    '    End Try
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

        If globalVariables.selectedCompanyID <> "003" Then
            lblGMApproval.Visible = False
            pbGmApproval.Visible = False
        End If
        dgApprovedInternalQ.Rows.Clear()
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
    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        frmInternalRequest.MdiParent = frmMDImain
        frmInternalRequest.Show()
    End Sub

    Private Sub btnOpenManually_Click(sender As Object, e As EventArgs) Handles btnOpenManually.Click
        frmInternalRequest.MdiParent = frmMDImain
        frmInternalRequest.Show()
    End Sub




    Private Sub dgHODApproval_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgApprovedInternalQ.CellDoubleClick

        frmInternalPrintView.MdiParent = frmMDImain
        frmInternalPrintView.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalPrintView.Show()

    End Sub

    Private Sub dgHODApproval_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgApprovedInternalQ.RowHeaderMouseDoubleClick


        frmInternalPrintView.MdiParent = frmMDImain
        frmInternalPrintView.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalPrintView.Show()

    End Sub

    Private Sub dgApprovedInternalQ_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgApprovedInternalQ.CellContentClick

    End Sub

    Private Async Sub txtInternalSearching_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtInternalSearching.Validating

        'Changes made converting to API level for validation 
        'Changes made by Gagan Tillekeratne.

        Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/getinternalrequestsbysearch?companyID={globalVariables.selectedCompanyID}&internalRNo={txtInternalSearching.Text}"

        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                Dim rowCount As Integer = 0
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Dim data As List(Of InternalRequestDto) = JsonConvert.DeserializeObject(Of List(Of InternalRequestDto))(json)
                    dgApprovedInternalQ.Rows.Clear()
                    For Each item As InternalRequestDto In data
                        dgApprovedInternalQ.Rows.Add(item.IR_NO, item.IR_DATE, item.PN_NO, item.SERIAL_NO, item.CUS_NAME, item.CUS_LOC, item.IR_STATE)

                        ' Set row color based on IR_STATE
                        Select Case item.IR_STATE
                            Case "INTERNAL PENDING PRINT"
                                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightSeaGreen
                            Case "PENDING APPROVAL"
                                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
                            Case "PENDING GM APPROVAL"
                                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightCoral
                            Case "PENDING DISPATCH"
                                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.Thistle
                            Case "ISSUED"
                                dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.SteelBlue
                        End Select

                        rowCount += 1
                    Next

                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' API Events  ...............................................................
    '===================================================================================================================
#Region "API Events"

#End Region


End Class

Public Class InternalRequestDto
    Public Property IR_NO As String
    Public Property IR_DATE As DateTime
    Public Property SERIAL_NO As String
    Public Property PN_NO As String
    Public Property CUS_LOC As String
    Public Property CUS_NAME As String
    Public Property IR_STATE As String
    Public Property BELEETA_REFERENCE_NO As String
    Public Property TOTAL_COST As Decimal
End Class
