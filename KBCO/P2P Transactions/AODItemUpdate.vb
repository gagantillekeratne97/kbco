Imports System.Data.SqlClient

Public Class frmAODUpdate
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim unitPrice As Double = CDbl(txtItemPrice.Text)
            Dim quantity As Integer = CInt(txtItemQuantity.Text)
            Dim query As String = $"UPDATE TBL_DEVICES_AND_ITEMS SET DAI_UNIT_PRICE = '{unitPrice}', QTY = {quantity}  WHERE DAI_PN = 'AOD' AND COM_ID = '{globalVariables.selectedCompanyID}'"

            ' Open connection if it's not open
            If dbConnections.sqlConnection.State <> ConnectionState.Open Then
                dbConnections.sqlConnection.Open()
            End If

            dbConnections.sqlCommand = New SqlCommand(query, dbConnections.sqlConnection)
            Dim rowsAffected As Integer = dbConnections.sqlCommand.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("No records were updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure the connection is closed
            If dbConnections.sqlConnection.State = ConnectionState.Open Then
                dbConnections.sqlConnection.Close()
            End If
        End Try
    End Sub


    Private Sub AODItemUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtItemName.Text = "AOD"
        txtItemPrice.Text = GetCurrentItemPrice().ToString()
        txtItemQuantity.Text = GetCurrentItemQuantity().ToString()
    End Sub

    Private Function GetCurrentItemQuantity() As Integer
        Dim CurrentItemQuantity As Integer = 0
        Dim query As String = $"SELECT QTY FROM TBL_DEVICES_AND_ITEMS WHERE DAI_PN = 'AOD' AND COM_ID = '{globalVariables.selectedCompanyID}'"
        dbConnections.sqlCommand = New SqlCommand(query, dbConnections.sqlConnection)
        CurrentItemQuantity = dbConnections.sqlCommand.ExecuteScalar()
        Return CurrentItemQuantity
    End Function

    Private Function GetCurrentItemPrice() As Double
        Dim CurrentItemPrice As Double = 0
        Dim query As String = $"SELECT DAI_UNIT_PRICE FROM TBL_DEVICES_AND_ITEMS WHERE DAI_PN = 'AOD' AND COM_ID = '{globalVariables.selectedCompanyID}'"
        dbConnections.sqlCommand = New SqlCommand(query, dbConnections.sqlConnection)
        CurrentItemPrice = dbConnections.sqlCommand.ExecuteScalar()
        Return CurrentItemPrice
    End Function
End Class