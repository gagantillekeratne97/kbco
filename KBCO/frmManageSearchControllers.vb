Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmManageSearchControllers
    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Private hasRecords As Boolean
    Dim generalValObj As New generalValidation
    Const WMCLOSE As String = "WmClose"
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                ' Me.createNew()
            Case "Save"
                If save() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                ' If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                ' showCrystalReport()
        End Select
    End Sub


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"
    Private Function save() As Boolean
        errorEvent = "Save"
        save = False
        If Not UsergroupName = "Administrator" Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If

        Dim conf = MessageBox.Show("Do you wish to save this record ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbYes Then
            If isDataValid() = False Then
                Exit Function
            End If
            If isEditClicked = True Then
                connectionStaet()
                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
                Try
                    dbConnections.sqlTransaction.Save("Save1")
                    strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.U_TBLSEARCH_CONTROLS WHERE COM_ID = '" & globalVariables.selectedCompanyID & "'"
                    dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                    dbConnections.sqlCommand.ExecuteNonQuery()
                    Dim forntag As String = ""
                    Dim ControlName As String = ""
                    Dim CustomSQL As String = ""
                    Dim DefualtSearchBy As String = ""
                    Dim retunField As String = ""
                    Dim ColumnSize As String = ""
                    Dim DefualViewMode As String = ""
                    Dim ResultPerPage As String = ""
                    Dim Orderby As String = ""
                    Dim DefualtOrderBy As String = ""

                    'Datagrid Data feeding code
                    Dim RowCount As Integer = dgSearchControllersItems.RowCount
                    For Each row As DataGridViewRow In dgSearchControllersItems.Rows


                        dbConnections.sqlCommand.Parameters.Clear()

                        If Not IsDBNull(dgSearchControllersItems.Rows(row.Index).Cells("FORM_TAG").Value()) Then

                            forntag = dgSearchControllersItems.Rows(row.Index).Cells("FORM_TAG").Value()
                            ControlName = dgSearchControllersItems.Rows(row.Index).Cells("CTRL_NAME").Value()
                            CustomSQL = dgSearchControllersItems.Rows(row.Index).Cells("CUSTOME_SQL").Value()
                            DefualtSearchBy = dgSearchControllersItems.Rows(row.Index).Cells("DEFUALT_SEARCH_BY").Value()
                            retunField = dgSearchControllersItems.Rows(row.Index).Cells("RETURN_FIEALD").Value()
                            ColumnSize = dgSearchControllersItems.Rows(row.Index).Cells("COLUMN_SIZE").Value()
                            DefualViewMode = dgSearchControllersItems.Rows(row.Index).Cells("DEFUALT_VIEW").Value()
                            ResultPerPage = dgSearchControllersItems.Rows(row.Index).Cells("RESULT_PER_PAGE").Value()
                            Orderby = dgSearchControllersItems.Rows(row.Index).Cells("ORDER_BY").Value()
                            DefualtOrderBy = dgSearchControllersItems.Rows(row.Index).Cells("DEFUALT_ORDER_BY").Value()


                            strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.U_TBLSEARCH_CONTROLS (FORM_TAG, CONTROL_NAME, CUSTOM_SQL, DEFAULT_SEARCHBY, RETURN_FIELD, COLUMN_SIZES, DEFAULT_VIEW_MODE, RESULTS_PER_PAGE, ORDER_BY, DEFAULT_ORDER_BY, COM_ID) VALUES     (@FORM_TAG, @CONTROL_NAME, @CUSTOM_SQL, @DEFAULT_SEARCHBY, @RETURN_FIELD, @COLUMN_SIZES, @DEFAULT_VIEW_MODE, @RESULTS_PER_PAGE, @ORDER_BY, @DEFAULT_ORDER_BY, '" & globalVariables.selectedCompanyID & "')"


                            dbConnections.sqlCommand.CommandText = strSQL

                            dbConnections.sqlCommand.Parameters.AddWithValue("@FORM_TAG", Trim(forntag))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CONTROL_NAME", Trim(ControlName))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@CUSTOM_SQL", Trim(CustomSQL))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@DEFAULT_SEARCHBY", Trim(DefualtSearchBy))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@RETURN_FIELD", Trim(retunField))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@COLUMN_SIZES", Trim(ColumnSize))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@DEFAULT_VIEW_MODE", Trim(DefualViewMode))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@RESULTS_PER_PAGE", Trim(ResultPerPage))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@ORDER_BY", Trim(Orderby))
                            dbConnections.sqlCommand.Parameters.AddWithValue("@DEFAULT_ORDER_BY", Trim(DefualtOrderBy))

                            dbConnections.sqlCommand.ExecuteNonQuery()

                        End If
                    Next
                    MessageBox.Show("Save Successful.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    dbConnections.sqlTransaction.Rollback()
                    MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
                    MsgBox(ex.InnerException.Message)
                Finally
                    dbConnections.dReader.Close()
                    connectionClose()
                End Try

                dbConnections.sqlTransaction.Commit()
            End If
        End If
        Return save
    End Function

    Private Sub FormEdit()
        If Not UsergroupName = "Administrator" Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show("" & EditMessage & "" & "Search Controllers", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            isEditClicked = True
            globalFunctions.globalButtonActivation(True, True, False, False, False, False)
            Me.saveBtnStatus()
        End If
    End Sub
#End Region
    '===================================================================================================================
    ''''''''''''''''''''''''''''''''''From Events'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '===================================================================================================================
#Region "Form Events"

    Private Sub frmManageSearchControllers_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmManageSearchControllers_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmManageSearchControllers_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmManageSearchControllers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmManageSearchControllers_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmManageSearchControllers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        OpenMenuItemsToDG()
    End Sub

    Private Sub frmManageSearchControllers_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        OpenMenuItemsToDG()
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

    Private Sub OpenMenuItemsToDG()
        Try
            errorEvent = "get menu item to grid"
            connectionStaet()
            strSQL = "SELECT     FORM_TAG, CONTROL_NAME, CUSTOM_SQL, DEFAULT_SEARCHBY, RETURN_FIELD, COLUMN_SIZES, DEFAULT_VIEW_MODE, RESULTS_PER_PAGE, ORDER_BY, DEFAULT_ORDER_BY FROM " & selectedDatabaseName & ".dbo.U_TBLSEARCH_CONTROLS WHERE COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            dgSearchControllersItems.Rows.Clear()
            Dim rowCount As Integer = 0
            hasRecords = False
            While dbConnections.dReader.Read
                hasRecords = True
                populatreDatagrid(dbConnections.dReader.Item("FORM_TAG"), dbConnections.dReader.Item("CONTROL_NAME"), dbConnections.dReader.Item("CUSTOM_SQL"), dbConnections.dReader.Item("DEFAULT_SEARCHBY"), dbConnections.dReader.Item("RETURN_FIELD"), dbConnections.dReader.Item("COLUMN_SIZES"), dbConnections.dReader.Item("DEFAULT_VIEW_MODE"), dbConnections.dReader.Item("RESULTS_PER_PAGE"), dbConnections.dReader.Item("ORDER_BY"), dbConnections.dReader.Item("DEFAULT_ORDER_BY"))
            End While

            dbConnections.sqlCommand.CommandText = ""
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
            Else
                '//New user permissions
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If

        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
    Private Sub populatreDatagrid(ByRef FormTag As String, ByRef ControlName As String, ByRef CustomeSQL As String, ByRef DefualtSearchBy As String, ByRef ReturnField As String, ByRef ColumnSize As String, ByRef DefualtViewMOde As String, ByRef ResultPerpage As String, ByRef Orderby As String, ByRefDefualtOrderBy As String)
        dgSearchControllersItems.ColumnCount = 10
        dgSearchControllersItems.Rows.Add(FormTag, ControlName, CustomeSQL, DefualtSearchBy, ReturnField, ColumnSize, DefualtViewMOde, ResultPerpage, Orderby, ByRefDefualtOrderBy)
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        '\\ need datagridview validations in here
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()


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

#End Region



    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        ErrorProvider1.Clear()
        If txtFormTag.Text = Nothing And txtControlName.Text = "" And txtCustomSQL.Text = "" And txtColumnSize.Text = "" And txtDefualtSearchBy.Text = "" And txtDefualtViewMode.Text = "" And txtReturnField.Text = "" And txtOrderBy.Text = "" And txtDefualtOrderBy.Text = "" And txtColumnSize.Text = "" And txtResultPerPage.Text = "" Then
            Exit Sub
        End If

        Dim DGselectedIndex As Integer = -1

        DGselectedIndex = dgSearchControllersItems.Rows.Add
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("FORM_TAG").Value = Trim(txtFormTag.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("CTRL_NAME").Value = Trim(txtControlName.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("CUSTOME_SQL").Value = Trim(txtCustomSQL.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("DEFUALT_SEARCH_BY").Value = Trim(txtDefualtSearchBy.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("RETURN_FIEALD").Value = Trim(txtReturnField.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("COLUMN_SIZE").Value = Trim(txtColumnSize.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("DEFUALT_VIEW").Value = Trim(txtDefualtViewMode.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("RESULT_PER_PAGE").Value = Trim(txtResultPerPage.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("ORDER_BY").Value = Trim(txtOrderBy.Text)
        dgSearchControllersItems.Rows.Item(DGselectedIndex).Cells("DEFUALT_ORDER_BY").Value = Trim(txtDefualtOrderBy.Text)

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            Dim msg = MessageBox.Show("Do you want to remove this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If msg = vbYes Then
                For Each row As DataGridViewRow In dgSearchControllersItems.SelectedRows
                    dgSearchControllersItems.Rows.Remove(row)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("Please select correct row for deletion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
End Class