Imports System.Data.SqlClient
Public Class frmCreateCustomeReport

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
                connectionStaet()
                If isEditClicked Then
                    errorEvent = "Edit"
                    strSQL = "UPDATE    UTBL_CUSTOME_REPORT SET    REPORT_NAME =@REPORT_NAME, REQUEST_BY =@REQUEST_BY, QUERY =@QUERY, REPORT_DESC =@REPORT_DESC, ACTIVE_REPORT =@ACTIVE_REPORT, MD_BY ='" & userSession & "', MD_DATE =GETDATE() WHERE REPORT_NO = @REPORT_NO and COM_ID =@COM_ID "
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO UTBL_CUSTOME_REPORT ( REPORT_NAME, REQUEST_BY, QUERY, REPORT_DESC, ACTIVE_REPORT, CR_BY, CR_DATE,COM_ID) VALUES     ( @REPORT_NAME, @REQUEST_BY, @QUERY, @REPORT_DESC, @ACTIVE_REPORT, '" & userSession & "', GETDATE(),@COM_ID)"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_NO", Trim(txtReportNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_NAME", Trim(txtReportName.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@REQUEST_BY", Trim(txtRequestBy.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@QUERY", Trim(txtQuery.Text))
                If Trim(txtDesc.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_DESC", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_DESC", Trim(txtDesc.Text))
                End If

                dbConnections.sqlCommand.Parameters.AddWithValue("@ACTIVE_REPORT", cbActiveReport.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", globalVariables.selectedCompanyID)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False


            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtReportNo.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else
                        MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                End Select
            Catch ex As Exception
                MsgBox(ex.InnerException.Message)
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

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtReportName.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            txtRequestBy.Enabled = True
            txtReportName.Enabled = True
            txtQuery.Enabled = True
            txtDesc.Enabled = True
            cbActiveReport.Enabled = True
            txtReportName.Focus()
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
    Private Sub frmCreateCustomeReport_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmCreateCustomeReport_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)

    End Sub

    Private Sub frmCreateCustomeReport_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmCreateCustomeReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmCreateCustomeReport_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmCreateCustomeReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        FormClear()
    End Sub

    Private Sub frmCreateCustomeReport_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = " read user permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'  AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + PermissionReadingErrorMessgae, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
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
        If generalValObj.isPresent(txtRequestBy) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtReportName) = False Then
            Exit Function
        End If
        If Trim(txtQuery.Text) = "" Then
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtReportNo.Text = ""
        txtReportName.Text = ""
        txtRequestBy.Text = ""
        txtQuery.Text = ""
        txtDesc.Text = ""
        txtReportNo.Enabled = True
        txtReportName.Enabled = True
        txtRequestBy.Enabled = True
        txtQuery.Enabled = True
        txtDesc.Enabled = True
        txtReportNo.Focus()

        isEditClicked = False
        '//Set enability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"

    Private Sub txtReportNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtReportNo.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        'If Trim(sender.Text) = "" Then
        '    e.Cancel = True
        '    Exit Sub
        'End If
        connectionStaet()
        Try
            strSQL = "SELECT     REPORT_NAME, REQUEST_BY, QUERY, REPORT_DESC, ACTIVE_REPORT FROM         UTBL_CUSTOME_REPORT WHERE REPORT_NO =@REPORT_NO AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_NO", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtReportName.Text = dbConnections.dReader.Item("REPORT_NAME")
                txtRequestBy.Text = dbConnections.dReader.Item("REQUEST_BY")
                txtQuery.Text = dbConnections.dReader.Item("QUERY")
                If IsDBNull(dbConnections.dReader.Item("REPORT_DESC")) Then
                    txtDesc.Text = ""
                Else
                    txtDesc.Text = dbConnections.dReader.Item("REPORT_DESC")
                End If
                txtReportNo.Enabled = False
                txtReportNo.Enabled = False
                txtReportName.Enabled = False
                txtRequestBy.Enabled = False
                txtQuery.Enabled = False
                txtDesc.Enabled = False

            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                txtReportNo.Enabled = False
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

    Private Sub txtReportNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReportNo.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtReportNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportNo.KeyPress
        generalValObj.isDigit(e)
    End Sub

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"
    Private Sub showCrystalReport()

    End Sub
#End Region

    Private Sub txtReportNo_TextChanged(sender As Object, e As EventArgs) Handles txtReportNo.TextChanged

    End Sub
End Class