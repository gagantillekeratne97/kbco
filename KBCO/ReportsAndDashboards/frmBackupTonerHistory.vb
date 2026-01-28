Imports System.Data.SqlClient

Public Class frmBackupTonerHistory
    Dim strSQL As String = ""
    Private Sub frmBackupTonerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgBTH.Rows.Clear()

        strSQL = "SELECT       TBL_INTERNAL_ITEMS.IR_DATE, TBL_INTERNAL_ITEMS.IR_NO, TBL_INTERNAL_ITEMS.COM_ID, TBL_INTERNAL_ITEMS.SERIAL_NO, TBL_INTERNAL_ITEMS.PN, TBL_INTERNAL_ITEMS.PN_DESC, TBL_INTERNAL_ITEMS.PN_QTY, TBL_INTERNAL_ITEMS.PN_VALUE, TBL_INTERNAL_ITEMS.STD_YIELD FROM         TBL_INTERNAL_ITEMS INNER JOIN TBL_INTERNAL_MAIN ON TBL_INTERNAL_ITEMS.IR_NO = TBL_INTERNAL_MAIN.IR_NO WHERE TBL_INTERNAL_MAIN.CUS_CODE = '" & Trim(lblCustomerID.Text) & "' and TBL_INTERNAL_MAIN.IR_TYPE = 'Backup' and TBL_INTERNAL_ITEMS.COM_ID = '" & Trim(globalVariables.selectedCompanyID) & "' and  TBL_INTERNAL_ITEMS.SERIAL_NO = '" & Trim(lblSerialNo.Text) & "'"
        dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)

        dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

        While dbConnections.dReader.Read
            populatreDatagrid(CDate(dbConnections.dReader.Item("IR_DATE")), dbConnections.dReader.Item("IR_NO"), dbConnections.dReader.Item("PN"), dbConnections.dReader.Item("PN_DESC"), dbConnections.dReader.Item("PN_QTY"), dbConnections.dReader.Item("PN_VALUE"), dbConnections.dReader.Item("STD_YIELD"))

        End While
        dbConnections.dReader.Close()

    End Sub


    Private Sub populatreDatagrid(ByRef IRDate As String, ByRef IRNO As String, ByRef PN As String, ByRef PN_Desc As String, ByRef Qty As String, ByRef IRCost As String, ByRef stdYield As String)
        dgBTH.ColumnCount = 7
        dgBTH.Rows.Add(IRDate, IRNO, PN, PN_Desc, Qty, IRCost, stdYield)
    End Sub

End Class