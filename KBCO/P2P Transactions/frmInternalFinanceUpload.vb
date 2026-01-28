Public Class frmInternalFinanceUpload
    Private strSQL As String
    Private Sub frmInternalFinanceUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInternalRequests()
    End Sub

    Private Sub LoadInternalRequests()
        Dim rowCount As Integer = 0
        dgApprovedInternalQ.Rows.Clear()
        Try
            connectionStaet()
            strSQL = "SELECT TBL_INTERNAL_MAIN.IR_NO, TBL_INTERNAL_MAIN.BELEETA_REFERENCE_NO, TBL_INTERNAL_MAIN.IR_DATE, TBL_INTERNAL_MAIN.SERIAL_NO, TBL_INTERNAL_MAIN.PN_NO, TBL_INTERNAL_MAIN.CUS_LOC,   MTBL_CUSTOMER_MASTER.CUS_NAME,TBL_INTERNAL_MAIN.IR_STATE  FROM  TBL_INTERNAL_MAIN INNER JOIN  MTBL_CUSTOMER_MASTER ON TBL_INTERNAL_MAIN.COM_ID = MTBL_CUSTOMER_MASTER.COM_ID AND TBL_INTERNAL_MAIN.CUS_CODE = MTBL_CUSTOMER_MASTER.CUS_ID WHERE     (TBL_INTERNAL_MAIN.COM_ID ='" & Trim(globalVariables.selectedCompanyID) & "') AND (TBL_INTERNAL_MAIN.IR_STATE IN ('UPLOADED TO BELEETA')) "
            dbConnections.sqlCommand.CommandText = strSQL
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            While dbConnections.dReader.Read
                populatreDatagrid(dbConnections.dReader.Item("IR_NO"), dbConnections.dReader.Item("BELEETA_REFERENCE_NO"), CDate(dbConnections.dReader.Item("IR_DATE")).ToShortDateString, dbConnections.dReader.Item("PN_NO"), dbConnections.dReader.Item("SERIAL_NO"), dbConnections.dReader.Item("CUS_NAME"), dbConnections.dReader.Item("CUS_LOC"), dbConnections.dReader.Item("IR_STATE"))

                If dbConnections.dReader.Item("IR_STATE") = "UPLOADED TO BELEETA" Then
                    dgApprovedInternalQ.Rows(rowCount).DefaultCellStyle.BackColor = Color.LightSeaGreen
                End If

                rowCount = rowCount + 1
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub populatreDatagrid(ByRef IRNo As String, ByRef BeleetaRefNo As String, IRDate As String, ByRef PNo As String, ByRef SN As String, ByRef CusName As String, ByRef MLoc As String, ByRef IrStatus As String)
        dgApprovedInternalQ.ColumnCount = 7
        dgApprovedInternalQ.Rows.Add(IRNo, BeleetaRefNo, IRDate, PNo, SN, CusName, MLoc, IrStatus)
    End Sub

    Private Sub dgApprovedInternalQ_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgApprovedInternalQ.CellContentDoubleClick
        frmInternalPrintView.MdiParent = frmMDImain
        frmInternalPrintView.txtIRNo.Text = dgApprovedInternalQ.Rows.Item(dgApprovedInternalQ.CurrentRow.Index).Cells(0).Value
        frmInternalPrintView.Show()
    End Sub
End Class