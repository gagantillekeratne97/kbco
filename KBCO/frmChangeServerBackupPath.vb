Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class frmChangeServerBackupPath
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

    Private NewPOIMagePath As Object
    Private NewCostSheetImagePath As Object
    Private NewInvoiceImagePath As Object


    Const WMCLOSE As String = "WmClose"
    '//Active form perform btn click case 
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If save() Then FormClear()
            Case "Edit"
                'Me.FormEdit()
            Case "Delete"
                'If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                '()
        End Select
    End Sub
    '===================================================================================================================
    '''''''''''''''''''''''''''''''''' Add / Edit /Delete/ new Code START...............................................
    '===================================================================================================================
#Region "Add/ Save/Delete"

    Private Sub createNew()
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Function save() As Boolean
        save = False
        If Not canCreate Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Function
        End If
        Dim conf = MessageBox.Show(SaveMessage, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If conf = vbNo Then
            Exit Function
        End If
        If isDataValid() = False Then
            Exit Function
        End If
        Try
            connectionStaet()
            Me.Cursor = Cursors.WaitCursor
            dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
            dbConnections.sqlCommand.Transaction = dbConnections.sqlTransaction
       
        

            For Each row As DataGridViewRow In dgPOImagePaths.Rows
                If Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("PO_PATH").Value)) = "" Then
                    NewPOIMagePath = DBNull.Value
                Else
                    NewPOIMagePath = Trim(txtNewServerAddress.Text) + "\POIMAGES\" + Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("PO_PATH").Value)).ToString
                End If

                If Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("COST_SHEET_PATH").Value)) = "" Then
                    NewCostSheetImagePath = DBNull.Value
                Else
                    NewCostSheetImagePath = Trim(txtNewServerAddress.Text) + "\POIMAGES\" + Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("COST_SHEET_PATH").Value)).ToString
                End If
                If Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("INV_PATH").Value)) = "" Then
                    NewInvoiceImagePath = DBNull.Value
                Else
                    NewInvoiceImagePath = Trim(txtNewServerAddress.Text) + "\POIMAGES\" + Trim(ChangeAddress(dgPOImagePaths.Rows(row.Index).Cells("INV_PATH").Value)).ToString
                End If

                strSQL = "UPDATE    TBL_PURCHASE_ORDER SET    PO_IMAGE =@PO_IMAGE, PO_AGREEMENT =@PO_AGREEMENT, INVOICE_IMAGE_PATH =@INVOICE_IMAGE_PATH WHERE     (REF_NO = '" & dgPOImagePaths.Rows(row.Index).Cells(0).Value & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PO_IMAGE", NewPOIMagePath)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PO_AGREEMENT", NewCostSheetImagePath)
                dbConnections.sqlCommand.Parameters.AddWithValue("@INVOICE_IMAGE_PATH", NewInvoiceImagePath)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False
            Next

            Dim Attachment As Object = ""
            Dim QuotImagePath As Object = ""

            For Each row As DataGridViewRow In dgLocalPurchase.Rows
                If Trim(ChangeAddress(dgLocalPurchase.Rows(row.Index).Cells("QUOT_IMAGE").Value)) = "" Then
                    QuotImagePath = DBNull.Value
                Else
                    QuotImagePath = Trim(txtNewServerAddress.Text) + "\LPIMAGES\" + Trim(ChangeAddress(dgLocalPurchase.Rows(row.Index).Cells("QUOT_IMAGE").Value)).ToString
                End If

                If Trim(ChangeAddress(dgLocalPurchase.Rows(row.Index).Cells("ATT_PATH").Value)) = "" Then
                    Attachment = DBNull.Value
                Else
                    Attachment = Trim(txtNewServerAddress.Text) + "\LPIMAGES\" + Trim(ChangeAddress(dgLocalPurchase.Rows(row.Index).Cells("ATT_PATH").Value)).ToString
                End If
              
                strSQL = "UPDATE " & selectedDatabaseName & ".dbo.L_TBL_LOCAL_PURCHASE_ORDER SET ATTACHMENT =@ATTACHMENT, QUOTATION_IMAGE_PATH =@QUOTATION_IMAGE_PATH WHERE     (REFNO = '" & dgLocalPurchase.Rows(row.Index).Cells(0).Value & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@ATTACHMENT", Attachment)
                dbConnections.sqlCommand.Parameters.AddWithValue("@QUOTATION_IMAGE_PATH", QuotImagePath)
                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

            Next

   

        '=============================================================================
            Attachment = ""


            For Each row As DataGridViewRow In dgNashua_PO.Rows

                If Trim(ChangeAddress(dgNashua_PO.Rows(row.Index).Cells("N_PO_PATH").Value)) = "" Then
                    Attachment = DBNull.Value
                Else
                    Attachment = Trim(txtNewServerAddress.Text) + "\LPIMAGES\" + Trim(ChangeAddress(dgNashua_PO.Rows(row.Index).Cells("N_PO_PATH").Value)).ToString
                End If

                strSQL = "UPDATE    TBL_N_SALE_ORDER SET PO_PATH =@PO_PATH WHERE     (REF_NO =  '" & dgNashua_PO.Rows(row.Index).Cells(0).Value & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PO_PATH", Attachment)

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

            Next



            '=============================================================================
            Attachment = ""


            For Each row As DataGridViewRow In dgCN.Rows

                If Trim(ChangeAddress(dgCN.Rows(row.Index).Cells("CN_ATT_PATH").Value)) = "" Then
                    Attachment = DBNull.Value
                Else
                    Attachment = Trim(txtNewServerAddress.Text) + "\LPIMAGES\" + Trim(ChangeAddress(dgCN.Rows(row.Index).Cells("CN_ATT_PATH").Value)).ToString
                End If

                strSQL = "UPDATE    TBL_N_SALE_ORDER SET PO_PATH =@PO_PATH WHERE     (REF_NO =  '" & dgCN.Rows(row.Index).Cells(0).Value & "')"
                dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@PO_PATH", Attachment)

                If dbConnections.sqlCommand.ExecuteNonQuery() Then save = True Else save = False

            Next


        dbConnections.sqlTransaction.Commit()

        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            MessageBox.Show("Error code(" & Me.Tag & "X1) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X1", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try



    End Function

   

    Private Sub FormEdit()
        If Not canModify Then
            MessageBox.Show(UserPermissionErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        Dim conf = MessageBox.Show("Do you want to change server backup path?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then
            'txtLocDesc.Enabled = True
            'txtLocDesc.Focus()
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
    Private Sub frmLocationsvb_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Enter
        isFormFocused = True
    End Sub

    Private Sub frmLocationsvb_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        globalFunctions.globalButtonActivation(False, False, False, False, False, False)
    End Sub

    Private Sub frmLocationsvb_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = 3 Then
            Dim conf = MessageBox.Show("" & ExitformMessage & "" & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbNo Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmLocationsvb_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmLocationsvb_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        isFormFocused = False
    End Sub

    Private Sub frmLocationsvb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormClear()
        AddtoGrid()
        AddtoGrid2()
        AddtoGrid3()
        AddtoGrid4()
    End Sub

    Private Sub frmLocationsvb_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        globalFunctions.globalButtonActivation(btnStatus(0), btnStatus(1), btnStatus(2), btnStatus(3), btnStatus(4), btnStatus(5))
        errorEvent = "read User Permission"
        Try
            connectionStaet()
            strSQL = "SELECT USERDET_MENURIGHT FROM TBLU_USERDET WHERE USERDET_USERCODE='" & globalVariables.userSession & "' AND USERDET_MENUTAG='" & Me.Tag & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, sqlConnection)

            Dim rights As String = Trim(dbConnections.sqlCommand.ExecuteScalar)
            If InStr(1, rights, "C") Then canCreate = True
            If InStr(1, rights, "D") Then canDelete = True
            If InStr(1, rights, "M") Then canModify = True

        Catch ex As Exception
            MessageBox.Show("Error code(" & Me.Tag & "X2) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X2", errorEvent, userSession, userName, DateTime.Now, ex.Message)
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
    Private Sub AddtoGrid()
        errorEvent = "Add to grid()"
        Dim poImagepath As String
        Dim CostsheetImagePath As String
        Dim InvoiceImagePath As String
        Dim hasRecords As Boolean
        Try
            connectionStaet()


            strSQL = "SELECT REF_NO, PO_IMAGE, PO_AGREEMENT, INVOICE_IMAGE_PATH FROM TBL_PURCHASE_ORDER"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader("PO_IMAGE")) Then
                    poImagepath = ""
                Else
                    poImagepath = dbConnections.dReader("PO_IMAGE")
                End If
                If IsDBNull(dbConnections.dReader("PO_AGREEMENT")) Then
                    CostsheetImagePath = ""
                Else
                    CostsheetImagePath = dbConnections.dReader("PO_AGREEMENT")
                End If

                If IsDBNull(dbConnections.dReader("INVOICE_IMAGE_PATH")) Then
                    InvoiceImagePath = ""
                Else
                    InvoiceImagePath = dbConnections.dReader("INVOICE_IMAGE_PATH")
                End If

                populatreDatagrid(dbConnections.dReader("REF_NO"), poImagepath, CostsheetImagePath, InvoiceImagePath)
            End While


            If hasRecords Then
                globalFunctions.globalButtonActivation(False, True, True, True, True, True)
              
            Else
                '//New user permissions
              
                globalFunctions.globalButtonActivation(True, True, False, False, False, False)
                Me.saveBtnStatus()
            End If
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X3) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X3", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub AddtoGrid2()
        errorEvent = "Add to grid()"
        Dim Attachment As String = ""
        Dim QuotImage As String = ""
        Try
            connectionStaet()

            strSQL = "SELECT REFNO,ATTACHMENT ,QUOTATION_IMAGE_PATH FROM [" & selectedDatabaseName & "].[dbo].[L_TBL_LOCAL_PURCHASE_ORDER]"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader("ATTACHMENT")) Then
                    Attachment = ""
                Else
                    Attachment = dbConnections.dReader("ATTACHMENT")
                End If
                If IsDBNull(dbConnections.dReader("QUOTATION_IMAGE_PATH")) Then
                    QuotImage = ""
                Else
                    QuotImage = dbConnections.dReader("QUOTATION_IMAGE_PATH")
                End If

                populatreDatagrid2(dbConnections.dReader("REFNO"), QuotImage, Attachment)
            End While


           
        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X4) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X4", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub




    Private Sub AddtoGrid3()
        errorEvent = "Add to grid()"
        Dim Attachment As String = ""

        Try
            connectionStaet()

            strSQL = "SELECT     REF_NO, PO_PATH FROM         TBL_N_SALE_ORDER"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader("PO_PATH")) Then
                    Attachment = ""
                Else
                    Attachment = dbConnections.dReader("PO_PATH")
                End If

                populatreDatagrid3(dbConnections.dReader("REF_NO"), Attachment)
            End While



        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub AddtoGrid4()
        errorEvent = "Add to grid()"
        Dim Attachment As String = ""

        Try
            connectionStaet()

            strSQL = "SELECT     CN_NO, REPLACE_INVOICE_ATT_PATH FROM         TBL_CREDIT_NOTE_MASTER"
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            Dim rowCount As Integer = 0
            While dbConnections.dReader.Read
                If IsDBNull(dbConnections.dReader("REPLACE_INVOICE_ATT_PATH")) Then
                    Attachment = ""
                Else
                    Attachment = dbConnections.dReader("REPLACE_INVOICE_ATT_PATH")
                End If
                populatreDatagrid4(dbConnections.dReader("CN_NO"), Attachment)
            End While



        Catch ex As Exception
            dbConnections.dReader.Close()
            MessageBox.Show("Error code(" & Me.Tag & "X6) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            inputErrorLog(Me.Text, "" & Me.Tag & "X6", errorEvent, userSession, userName, DateTime.Now, ex.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByVal A As String, ByVal B As String, ByVal C As String, ByVal D As String)
        dgPOImagePaths.ColumnCount = 4
        dgPOImagePaths.Rows.Add(A, B, C, D)
    End Sub

    Private Sub populatreDatagrid2(ByVal A As String, ByVal B As String, ByVal C As String)
        dgLocalPurchase.ColumnCount = 3
        dgLocalPurchase.Rows.Add(A, B, C)
    End Sub

    Private Sub populatreDatagrid3(ByVal A As String, ByVal B As String)
        dgNashua_PO.ColumnCount = 2
        dgNashua_PO.Rows.Add(A, B)
    End Sub

    Private Sub populatreDatagrid4(ByVal A As String, ByVal B As String)
        dgCN.ColumnCount = 2
        dgCN.Rows.Add(A, B)
    End Sub
    Private Function ChangeAddress(ByRef OldAddress As String) As String
        Dim str As String
        ' Dim strArr() As String

        str = OldAddress
        'strArr = str.Split("\").Last
        Dim lstrLastValue As String = str.Split("\").Last
        Return lstrLastValue
    End Function

#End Region

    '===================================================================================================================
    '''''''''''''''''''''''''''''''''''Validations......................................................................
    '===================================================================================================================
#Region "Validations"
    Private Function isDataValid()
        isDataValid = False
        If generalValObj.isPresent(txtNewServerAddress) = False Then
            Exit Function
        End If
        isDataValid = True
        Return isDataValid
    End Function

    Private Sub FormClear()
        txtNewServerAddress.Text = ""
        txtNewServerAddress.Focus()

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
    Private Sub txtNewServerAddress_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNewServerAddress.KeyDown
        KeyPresSoundDisable(e)
    End Sub
#End Region



End Class