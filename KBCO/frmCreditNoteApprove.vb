Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data
Public Class frmCreditNoteApprove
    Private strSql As String
    Dim IsApprove As Boolean
    Private canCreate As Boolean
    Private canDelete As Boolean
    Private canModify As Boolean
    Private btnStatus(5) As Boolean

    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
            Case "Save"
                Me.Update()
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

    Private Sub txtCNNo_Validated(sender As Object, e As EventArgs) Handles txtCNNo.Validated
        connectionStaet()
        If txtCNNo.Text <> "" Then
            strSql = "SELECT * FROM TBL_CREDIT_NOTES WHERE CN_NO = '" & txtCNNo.Text & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            Using sqlCommand As New SqlCommand(strSql, dbConnections.sqlConnection)
                dbConnections.dReader = sqlCommand.ExecuteReader
                While dbConnections.dReader.Read
                    txtInvoiceNo.Text = If(Not IsDBNull(dbConnections.dReader.Item("INVOICE_NO")), dbConnections.dReader.Item("INVOICE_NO").ToString(), "")
                    txtInvoiceDate.Text = If(Not IsDBNull(dbConnections.dReader.Item("REQ_DATE")), dbConnections.dReader.Item("REQ_DATE").ToString(), "")
                    txtCusCode.Text = If(Not IsDBNull(dbConnections.dReader.Item("CUS_ID")), dbConnections.dReader.Item("CUS_ID").ToString(), "")
                    txtVATValue.Text = If(Not IsDBNull(dbConnections.dReader.Item("LINE_TAX")), dbConnections.dReader.Item("LINE_TAX").ToString(), "")
                    txtCusName.Text = If(Not IsDBNull(dbConnections.dReader.Item("CUS_NAME")), dbConnections.dReader.Item("CUS_NAME").ToString(), "")
                    txtSSCL.Text = If(Not IsDBNull(dbConnections.dReader.Item("SSCL")), dbConnections.dReader.Item("SSCL").ToString(), "")
                    '//txtReqRepCode.Text = If(Not IsDBNull(dbConnections.dReader.Item("REQUESTED_REP_CODE")), dbConnections.dReader.Item("REQUESTED_REP_CODE").ToString(), "")
                    txtReason.Text = If(Not IsDBNull(dbConnections.dReader.Item("REASON")), dbConnections.dReader.Item("REASON").ToString(), "")
                    txtInvoiceSubTotal.Text = If(Not IsDBNull(dbConnections.dReader.Item("INVOICE_AMOUNT")), dbConnections.dReader.Item("INVOICE_AMOUNT").ToString(), "")
                    txtInvoiceTotal.Text = If(Not IsDBNull(dbConnections.dReader.Item("INVOICE_AMOUNT")), dbConnections.dReader.Item("INVOICE_AMOUNT").ToString(), "")

                End While
                dbConnections.dReader.Close()
            End Using
            strSql = "SELECT * FROM TBL_CREDIT_NOTE_ITEMS WHERE CN_NO = '" & txtCNNo.Text & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            Dim dataTable As New DataTable()

            ' Assuming your table is named "Invoices" and has columns like "InvoiceNo", "CustomerName", etc.
            Dim query As String = $"SELECT * FROM TBL_CREDIT_NOTE_ITEMS WHERE CN_NO = '{txtCNNo.Text}'"

            Using command As New SqlCommand(query, dbConnections.sqlConnection)
                ' Open the connection                
                Using dataAdapter As New SqlDataAdapter(command)
                    dataAdapter.Fill(dataTable)
                End Using
                DataGridView1.DataSource = dataTable
            End Using
            'dbConnections.sqlCommand = New SqlCommand(strSql, dbConnections.sqlConnection)            
        End If
    End Sub

    Private Sub btnApprove1_Click(sender As Object, e As EventArgs) Handles btnApprove1.Click
        strSql = "UPDATE TBL_CREDIT_NOTES SET STATUS = 'HOD CREDIT NOTE APPROVED' WHERE  CN_NO = '" & txtCNNo.Text & "'"
        Using updateCommand As New SqlCommand(strSql, dbConnections.sqlConnection)
            Dim conf = MessageBox.Show("Do you need to Approve this Credit Note ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = DialogResult.Yes Then
                updateCommand.ExecuteNonQuery()
                Me.Close()
            ElseIf conf = DialogResult.No Then
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        strSql = "UPDATE TBL_CREDIT_NOTES WHERE SET STATUS = 'HOD CREDIT NOTE REJECTED' AND CN_NO = " & txtCNNo.Text & ""
        Using updateCommand As New SqlCommand(strSql, dbConnections.sqlConnection)
            Dim conf = MessageBox.Show("Do you want to Reject this Credit Note ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = DialogResult.Yes Then
                updateCommand.ExecuteNonQuery()
                Me.Close()
            ElseIf conf = DialogResult.No Then
                Me.Close()
            End If
        End Using
    End Sub

    Private Sub frmCreditNoteApprove_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
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
End Class