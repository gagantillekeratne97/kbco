Imports System.Data.SqlClient

Public Class frmCreditNoteReqQue
    Dim strSql As String
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub LoadInvoiceList(ByRef department)
        Dim dataTable As New DataTable()
        ' Create a SqlConnection and a SqlCommand        
        ' Assuming your table is named "Invoices" and has columns like "InvoiceNo", "CustomerName", etc.        
        strSql = "SELECT * FROM TBL_CREDIT_NOTES WHERE COM_ID = '" & globalVariables.selectedCompanyID & "' AND DEPT = '" & department & "' AND STATUS = 'PENDING HOD APPROVAL'"
        Using command As New SqlCommand(strSql, dbConnections.sqlConnection)
            ' Open the connection            
            Using dataAdapter As New SqlDataAdapter(command)
                dataAdapter.Fill(dataTable)
            End Using
            DataGridView1.DataSource = dataTable
        End Using
    End Sub

    Private Sub CreditNoteAppt()
        Dim dataTable As New DataTable()
        strSql = "SELECT * FROM TBL_CREDIT_NOTES WHERE COM_ID = '" & globalVariables.selectedCompanyID & "' AND DEPT = '" & cmbDepartment.Text & "' AND STATUS = 'HOD CREDIT NOTE APPROVED'"
        Using command As New SqlCommand(strSql, dbConnections.sqlConnection)
            ' Open the connection            
            Using dataAdapter As New SqlDataAdapter(command)
                dataAdapter.Fill(dataTable)
            End Using
            DataGridView1.DataSource = dataTable
        End Using
    End Sub

    Private Sub CreditNoteRejt()
        Dim dataTable As New DataTable()
        strSql = "SELECT * FROM TBL_CREDIT_NOTES WHERE COM_ID = '" & globalVariables.selectedCompanyID & "' AND DEPT = '" & cmbDepartment.Text & "' AND STATUS = 'HOD CREDIT NOTE REJECTED'"
        Using command As New SqlCommand(strSql, dbConnections.sqlConnection)
            ' Open the connection            
            Using dataAdapter As New SqlDataAdapter(command)
                dataAdapter.Fill(dataTable)
            End Using
            DataGridView1.DataSource = dataTable
        End Using
    End Sub

    Private Sub frmCreditNoteReqQue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim department As String = cmbDepartment.Text
        LoadInvoiceList(department)
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Dim selectedRowIndex As Integer = DataGridView1.CurrentRow.Index

        If selectedRowIndex >= 0 AndAlso DataGridView1.Rows.Count > 0 Then
            Dim cellValue As Object = DataGridView1.Rows.Item(selectedRowIndex).Cells(0).Value

            If cellValue IsNot Nothing AndAlso cellValue IsNot DBNull.Value Then
                Dim frmCreditNoteApprove As New frmCreditNoteApprove()
                frmCreditNoteApprove.txtCNNo.Text = cellValue.ToString()
                frmCreditNoteApprove.MdiParent = frmMDImain
                frmCreditNoteApprove.Show()
            Else
                MessageBox.Show("Selected cell value is null or DBNull.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("No rows in the DataGridView.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CreditNoteAppt()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CreditNoteRejt()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        LoadInvoiceList(cmbDepartment.Text)
    End Sub
End Class