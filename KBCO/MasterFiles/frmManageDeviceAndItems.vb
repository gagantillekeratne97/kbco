


'Error log error event  - MDA00x#
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Public Class frmManageDeviceAndItems

    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean
    '//User rights
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Const WMCLOSE As String = "WmClose"
    Private generalValObj As New generalValidation
    Private PCname As String = System.Net.Dns.GetHostName
    Private img As String
    Private MD5obj As New MD5
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
                    strSQL = "UPDATE    " & selectedDatabaseName & ".dbo.TBL_DEVICES_AND_ITEMS SET QTY=@QTY, DAI_NAME =@DAI_NAME, DAI_DESC =@DAI_DESC, DAI_UNIT_PRICE =@DAI_UNIT_PRICE, DAI_ACTIVE =@DAI_ACTIVE,VAT_AVAILABLE=@VAT_AVAILABLE, MD_BY ='" & userSession & "', MD_DATE =GETDATE(),ITEM_CLASS=@ITEM_CLASS,WARRANTY_YIELE=@WARRANTY_YIELE  WHERE DAI_PN =@DAI_PN and COM_ID='" & globalVariables.selectedCompanyID & "'"
                Else
                    errorEvent = "Save"
                    strSQL = "INSERT INTO " & selectedDatabaseName & ".dbo.TBL_DEVICES_AND_ITEMS  (QTY,DAI_PN, DAI_NAME, DAI_DESC, DAI_UNIT_PRICE, DAI_ACTIVE,VAT_AVAILABLE, CR_BY, CR_DATE,ITEM_CLASS,WARRANTY_YIELE,COM_ID) VALUES     (@QTY,@DAI_PN, @DAI_NAME, @DAI_DESC, @DAI_UNIT_PRICE, @DAI_ACTIVE,@VAT_AVAILABLE, '" & userSession & "', GETDATE(),@ITEM_CLASS,@WARRANTY_YIELE,'" & globalVariables.selectedCompanyID & "')"
                End If
                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(txtPartNo.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_NAME", Trim(txtpartname.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_DESC", Trim(txtDesc.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_UNIT_PRICE", Trim(txtUnitPrice.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_ACTIVE", cbActiveState.CheckState)
                dbConnections.sqlCommand.Parameters.AddWithValue("@VAT_AVAILABLE", cbVatAvailable.CheckState)

                dbConnections.sqlCommand.Parameters.AddWithValue("@ITEM_CLASS", Trim(cmbItemClass.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@QTY", CDbl(txtQtyOnHand.Text))



                If Trim(txtWarrantyYiele.Text) = "" Then
                    dbConnections.sqlCommand.Parameters.AddWithValue("@WARRANTY_YIELE", DBNull.Value)
                Else
                    dbConnections.sqlCommand.Parameters.AddWithValue("@WARRANTY_YIELE", CInt(Trim(txtWarrantyYiele.Text)))
                End If


                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

                If save = True Then
                    If isEditClicked Then
                        AuditDelete(Me.Text, userSession, userName, txtPartNo.Text, txtpartname.Text + "(Edit)")
                    Else
                        AuditDelete(Me.Text, userSession, userName, txtPartNo.Text, txtpartname.Text + "(Saved)")
                    End If
                End If
            Catch ex As SqlException
                Select Case ex.Number
                    Case 2627
                        Dim confirm = MessageBox.Show(AllradyaddedErrorMessage, "Already exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If confirm = vbYes Then
                            txtPartNo.Focus()
                            Exit Function
                        Else
                            FormClear()
                        End If
                    Case Else

                End Select
            Catch ex As Exception
                MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            Finally
                dbConnections.dReader.Close()
                connectionClose()
                UserAuthorize = False
            End Try
        End If
        Return save
    End Function

    Private Function delete() As Boolean
        errorEvent = "Delete"
        delete = False

        Try
            Dim IDbackup As String = Trim(txtPartNo.Text)
            Dim IDdesc As String = Trim(txtpartname.Text)
            connectionStaet()
            Dim confDelete = MessageBox.Show("" & DeleteMessage & "" & txtpartname.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If confDelete = vbYes Then
                strSQL = "DELETE FROM " & selectedDatabaseName & ".dbo.TBL_DEVICES_AND_ITEMS WHERE DAI_PN =@DAI_PN and COM_ID = '" & globalVariables.selectedCompanyID & "'"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(txtPartNo.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then delete = True Else delete = False
            End If
            If delete = True Then
                AuditDelete(Me.Text, userSession, userName, IDbackup, IDdesc)
            End If
        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            connectionClose()
            UserAuthorize = False
        End Try
        Return delete
    End Function

    Private Sub FormEdit()

        Dim conf = MessageBox.Show("" & EditMessage & "" & txtpartname.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            Isenable(False)
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
    Private Sub frmManageDeviceAndItems_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & " " & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub frmManageDeviceAndItems_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmManageDeviceAndItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        globalFunctions.globalButtonActivation(True, True, True, True, True, True)

    End Sub

    Private Sub frmManageDeviceAndItems_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False

    End Sub

    Private Sub frmManageDeviceAndItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmManageDeviceAndItems_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmManageDeviceAndItems_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        errorEvent = "read User Permission"
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'  AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
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

    Private Sub Isenable(ByRef enablestate As Boolean)
        'txtpartname.Enabled = enablestate
        'txtDesc.Enabled = enablestate
        'txtUnitPrice.Enabled = enablestate
        'txtParentPartNo.Enabled = enablestate
        'txtQtyOnHand.Enabled = enablestate
        'cmbItemClass.Enabled = enablestate
    End Sub
#End Region


    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtpartname) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtDesc) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtUnitPrice) = False Then
            Exit Function
        End If
        If generalValObj.isPresent(txtQtyOnHand) = False Then
            Exit Function
        End If


        If cmbItemClass.Text = "None" Then
            MessageBox.Show("Please select correct Item class.", "Invalid Item Class", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        End If

        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtPartNo.Text = ""
        txtpartname.Text = ""
        txtDesc.Text = ""
        txtUnitPrice.Text = ""

        txtQtyOnHand.Text = ""

        Isenable(False)
        txtPartNo.Enabled = True
        isEditClicked = False
        cmbItemClass.SelectedIndex = 0
        cbActiveState.Checked = True
        cbVatAvailable.Checked = True
        txtWarrantyYiele.Text = ""
        txtPartNo.Focus()
        '//Set en-ability of global buttons
        globalFunctions.globalButtonActivation(False, True, False, False, True, True)
        Me.saveBtnStatus()


    End Sub
#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' text Boxes Events ...............................................................
    '===================================================================================================================
#Region "Text Box events"
    Private Sub txtPartNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPartNo.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Private Sub txtUnitPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnitPrice.KeyPress
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub

    Private Sub txtPartNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPartNo.KeyPress
        'generalValObj.isLetterOrDigit(e)
    End Sub
    Private Sub txtParentPartNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtParentPartNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub
    Private Sub txtPartNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPartNo.Validating
        errorEvent = "Reading information"
        If IsFormClosing() Then Exit Sub
        If Not isFormFocused Then Exit Sub
        If Trim(sender.Text) = "" Then
            e.Cancel = True
            Exit Sub
        End If
        connectionStaet()
        Try
            strSQL = "SELECT     DAI_NAME, DAI_DESC, DAI_UNIT_PRICE, DAI_ACTIVE, DAI_PARENT_PN, VAT_AVAILABLE, QTY, ITEM_CLASS, SELECTION_CONDICTION, SELECTION_QTY,WARRANTY_YIELE FROM         TBL_DEVICES_AND_ITEMS WHERE     (DAI_PN = @DAI_PN)  AND COM_ID ='" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@DAI_PN", Trim(sender.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtpartname.Text = dbConnections.dReader.Item("DAI_NAME")
                txtDesc.Text = dbConnections.dReader.Item("DAI_DESC")
                txtUnitPrice.Text = dbConnections.dReader.Item("DAI_UNIT_PRICE")
                cbActiveState.Checked = dbConnections.dReader.Item("DAI_ACTIVE")
                If IsDBNull(dbConnections.dReader.Item("QTY")) Then
                    txtQtyOnHand.Text = "0.0"
                Else
                    txtQtyOnHand.Text = dbConnections.dReader.Item("QTY")
                End If

                If IsDBNull(dbConnections.dReader.Item("ITEM_CLASS")) Then
                    cmbItemClass.SelectedIndex = 0
                Else
                    cmbItemClass.SelectedItem = dbConnections.dReader.Item("ITEM_CLASS")
                End If

                cbVatAvailable.Checked = dbConnections.dReader.Item("VAT_AVAILABLE")
                If IsDBNull(dbConnections.dReader.Item("WARRANTY_YIELE")) Then
                    txtWarrantyYiele.Text = ""
                Else
                    txtWarrantyYiele.Text = dbConnections.dReader.Item("WARRANTY_YIELE")
                End If
                sender.Enabled = False

            End While
            dbConnections.dReader.Close()
            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
                Me.saveBtnStatus()
                Isenable(False)
            Else
                '//New user permissions
                Isenable(True)

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
    Private Sub txtQtyOnHand_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQtyOnHand.KeyPress
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub

    Private Sub txtSeletion_Unit_Qty_KeyPress(sender As Object, e As KeyPressEventArgs)
        generalValObj.isNumericWithDecimals(sender, e)
    End Sub

    Private Sub txtWarrantyYiele_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWarrantyYiele.KeyPress
        generalValObj.isDigit(e)
    End Sub


#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Crystal Report  ...............................................................
    '===================================================================================================================
#Region "Crystal report"

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Button events  ...............................................................
    '===================================================================================================================
#Region "Button events"
    Private Sub cbActiveState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbActiveState.CheckedChanged
        If cbActiveState.Checked = True Then
            cbActiveState.Text = "Active"
        Else
            cbActiveState.Text = "De-active"
        End If
    End Sub
#End Region





End Class