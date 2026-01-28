Public Class Something
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
    Const WMCLOSE As String = "WmClose"
    Private _lastFormSize As Integer

    Private Sub frmCreditNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtCustomerName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerName.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub

    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"
                If Save() Then FormClear()
            Case "Edit"
                Me.FormEdit()
            Case "Delete"
                If delete() Then FormClear()
            Case "Search"
                SendKeys.Send("{F2}")
            Case "Print"
                'PrintRecipt(txtReciptID.Text)
        End Select
    End Sub

    Private Function Save() As Boolean

    End Function

    Private Function delete()

    End Function

    Private Function FormEdit()

    End Function

    Private Sub createNew()
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub

    Private Sub FormClear()
        txtCustomerID.Text = ""
        txtCustomerName.Text = ""
        txtAddressLine1.Text = ""
        txtAddressLine2.Text = ""
        txtAddressLine3.Text = ""
        txtAddressLine3.Text = ""
        RichTextBox1.Text = ""
        TextBox1.Text = ""
        TextBox3.Text = ""
        txtInvoiceAmount.Text = ""
        txtTaxAmount.Text = ""
        txtTotalAmount.Text = ""
    End Sub
End Class