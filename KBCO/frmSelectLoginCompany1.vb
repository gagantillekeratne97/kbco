Imports System.Data.SqlClient

Public Class frmSelectLoginCompany1

    Private errorEvent As String
    Private strSQL As String
    Private isFormFocused As Boolean
    Private isEditClicked As Boolean = False
    Private btnStatus(5) As Boolean

    'Private Sub frmSelectLoginCompany_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    If e.CloseReason = 3 Then
    '        Dim conf = MessageBox.Show("" & ExitformMessage & " " & Me.Text & " ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
    '        If conf = vbNo Then
    '            e.Cancel = True
    '        End If
    '    End If
    'End Sub


    Private Sub LoadCompanies()
        connectionStaet()

        Try
            dgCompanies.Rows.Clear()
            strSQL = "SELECT     TBLU_COMPANY_DET.COM_ID, L_TBL_COMPANIES.COM_NAME FROM         TBLU_COMPANY_DET INNER JOIN L_TBL_COMPANIES ON TBLU_COMPANY_DET.COM_ID = L_TBL_COMPANIES.COM_ID WHERE     (TBLU_COMPANY_DET.USERHED_USERCODE = '" & userSession & "')"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader
            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader.Item("COM_ID"), dbConnections.dReader.Item("COM_NAME"))
            End While


        Catch ex As Exception
            dbConnections.dReader.Close()
            inputErrorLog(Me.Text, "" & Me.Tag & "X5", errorEvent, userSession, userName, DateTime.Now, ex.Message)
            MessageBox.Show("Error code(" & Me.Tag & "X5) " + GenaralErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef ComID As String, ByRef ComName As String)
        dgCompanies.ColumnCount = 2
        dgCompanies.Rows.Add(ComID, ComName)
    End Sub





    Private Sub dgCompanies_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgCompanies.CellContentDoubleClick
        selectedCompanyID = dgCompanies.Rows.Item(dgCompanies.CurrentRow.Index).Cells(0).Value
        frmMDImain.Show()
        frmLoginWinForm.Hide()
        Me.Hide()
    End Sub

    Private Sub dgCompanies_RowHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgCompanies.RowHeaderMouseDoubleClick
        selectedCompanyID = dgCompanies.Rows.Item(dgCompanies.CurrentRow.Index).Cells(0).Value
        frmMDImain.Show()
        frmLoginWinForm.Hide()
        Me.Hide()
    End Sub


    Private Sub frmSelectLoginCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCompanies()
    End Sub
End Class