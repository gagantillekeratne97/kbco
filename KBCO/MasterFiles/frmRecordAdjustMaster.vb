Imports System.Data.SqlClient

Public Class frmRecordAdjustMaster
    Dim strSQL As String = ""
    Private Sub frmRecordAdjustMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtInvNoSIV.Text = ""
        txtAmountSIV.Text = ""
        txtInvoiceSOIP.Text = ""
    End Sub


    Private Function UpdateInvVal() As Boolean
        UpdateInvVal = False
        Try
            Dim conf = MessageBox.Show("Do you wish to preform this action?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbYes Then



                If Trim(txtInvNoSIV.Text) = "" Then
                    Exit Function
                End If
                If Trim(txtAmountSIV.Text) = "" Then
                    Exit Function
                End If
                ' INV_PRINTED =@,
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET              INV_VAL =@INV_VAL WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvNoSIV.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_VAL", Trim(txtAmountSIV.Text))
                If dbConnections.sqlCommand.ExecuteNonQuery() Then UpdateInvVal = True Else UpdateInvVal = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return UpdateInvVal
    End Function

    Private Function SetInvOriginalPrint() As Boolean
        SetInvOriginalPrint = False
        Try
            If Trim(txtInvoiceSOIP.Text) = "" Then
                Exit Function
            End If
            Dim conf = MessageBox.Show("Do you wish to preform this action?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbYes Then


                ' INV_PRINTED =@,
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET            INV_PRINTED=0 WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtInvoiceSOIP.Text))

                If dbConnections.sqlCommand.ExecuteNonQuery() Then SetInvOriginalPrint = True Else SetInvOriginalPrint = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return SetInvOriginalPrint
    End Function


    Private Function SetInvCancel() As Boolean
        SetInvCancel = False
        Try
            If Trim(txtCancelInvoice.Text) = "" Then
                Exit Function
            End If

            If Trim(txtCancelReason.Text) = "" Then
                Exit Function
            End If
            Dim conf = MessageBox.Show("Do you wish to preform this action?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If conf = vbYes Then

                ' INV_PRINTED =@,
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET            INV_STATUS_T=@INV_STATUS_T , CANCELLED_BY=@CANCELLED_BY ,CANCELLED_DATE=GETDATE(),CANCELLED_REASON=@CANCELLED_REASON WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(txtCancelInvoice.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_STATUS_T", "CANCELLED")
                dbConnections.sqlCommand.Parameters.AddWithValue("@CANCELLED_REASON", Trim(txtCancelReason.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@CANCELLED_BY", userSession)


                If dbConnections.sqlCommand.ExecuteNonQuery() Then SetInvCancel = True Else SetInvCancel = False

                If SetInvCancel Then
                    MessageBox.Show("Invoice Cancelled.", "Cancelled.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return SetInvCancel
    End Function


    Private Sub btnUpdateSIV_Click(sender As Object, e As EventArgs) Handles btnUpdateSIV.Click
        If UpdateInvVal() Then
            MessageBox.Show("Updated.", "Invoice Value.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInvNoSIV.Text = ""
            txtAmountSIV.Text = ""
        Else
            MessageBox.Show("Not Updated.", "Invoice Value.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnSetSOIP_Click(sender As Object, e As EventArgs) Handles btnSetSOIP.Click
        If SetInvOriginalPrint() Then
            MessageBox.Show("Changed to Original Print.", "Changed.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtInvoiceSOIP.Text = ""
        Else
            MessageBox.Show("Not Changed.", "Changed.", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


 
    Private Sub btnCancelInvoice_Click(sender As Object, e As EventArgs) Handles btnCancelInvoice.Click
        SetInvCancel()
    End Sub

    Private Sub btnOpenCustomerInternalBlock_Click(sender As Object, e As EventArgs) Handles btnOpenCustomerInternalBlock.Click
        frmBlockCustomers.MdiParent = frmMDImain
        frmBlockCustomers.Show()

    End Sub
End Class