Imports System.Data.SqlClient
Imports System.Text
Imports System.IO

Public Class frmCustomReport
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
    Dim dataadapter As New SqlDataAdapter
    '//Active form perform btn click case
    Public Sub Preform_btn_click(ByVal strString As String)
        Select Case strString
            Case "New"
                Me.createNew()
            Case "Save"

            Case "Edit"

            Case "Delete"

            Case "Search"

            Case "Print"

        End Select
    End Sub



    Private Sub createNew()
        Dim conf = MessageBox.Show(CreateNewMessgae, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        If conf = vbYes Then FormClear()
    End Sub


    Private Sub FormClear()
        txtReportNo.Text = ""
        txtReportName.Text = ""

        txtSql.Text = ""
        txtDesc.Text = ""
        txtReportNo.Enabled = True
        txtReportName.Enabled = True

        txtSql.Enabled = True
        txtDesc.Enabled = True
        txtReportNo.Focus()
        dataadapter.Dispose()
        dgReport.DataSource = Nothing
        dgReport.DataMember = Nothing
        dgReport.Rows.Clear()

    End Sub

    Private Sub btnGenarate_Click(sender As Object, e As EventArgs) Handles btnGenarate.Click
        dataadapter.Dispose()
        If Trim(txtSql.Text) = "" Then
            Exit Sub
        End If
        dgReport.DataSource = Nothing
        dgReport.DataMember = Nothing
        dgReport.Rows.Clear()
        If Trim(txtSql.Text) = "" Then
            Exit Sub
        End If
        Try
            strSQL = Trim(txtSql.Text)
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            If Trim(txtSql.Text).Contains("@startDate") Then
                dbConnections.sqlCommand.Parameters.AddWithValue("@startDate", dtpStartDate.Value.Date)

            End If
            If Trim(txtSql.Text).Contains("@endDate") Then

                dbConnections.sqlCommand.Parameters.AddWithValue("@endDate", dtpEndDate.Value.Date)
            End If



            If Trim(txtSql.Text).Contains("@filter") Then
                dbConnections.sqlCommand.Parameters.AddWithValue("@filter", Trim(txtFilter.Text))

            End If

            dataadapter = New SqlDataAdapter(dbConnections.sqlCommand)
            Dim ds As New DataSet()
            dataadapter.Fill(ds, "REPORT")
            dgReport.DataSource = ds
            dgReport.DataMember = "REPORT"
            dgReport.Update()
            cmbColFind.Items.Clear()

            For i = 0 To dgReport.ColumnCount - 1
                cmbColFind.Items.Add(dgReport.Columns(i).HeaderText)
            Next

            cmbColFind.SelectedIndex = 0
            ProgressBar1.Maximum = dgReport.RowCount
        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try


    End Sub

    Private Sub btnExport2_Click(sender As Object, e As EventArgs) Handles btnExport2.Click
        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "Excel Documents (*.xls)|*.xls"
            sfd.FileName = "Report.xls"
            If sfd.ShowDialog() = DialogResult.OK Then
                ToCsV(dgReport, sfd.FileName)
            End If
            MsgBox("Export Completed")

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        End Try
    End Sub

    Private Sub ToCsV(dGV As DataGridView, filename As String)
        Dim stOutput As String = ""
        ' Export titles:
        Dim sHeaders As String = ""

        For j As Integer = 0 To dGV.Columns.Count - 1
            sHeaders = sHeaders.ToString() & Convert.ToString(dGV.Columns(j).HeaderText) & vbTab
        Next
        stOutput += sHeaders & vbCr & vbLf
        ' Export data.
        For i As Integer = 0 To dGV.RowCount - 1
            Dim stLine As String = ""
            For j As Integer = 0 To dGV.Rows(i).Cells.Count - 1
                stLine = stLine.ToString() & Convert.ToString(dGV.Rows(i).Cells(j).Value) & vbTab
            Next
            stOutput += stLine & vbCr & vbLf
        Next
        Dim utf16 As Encoding = Encoding.GetEncoding(1254)
        Dim output As Byte() = utf16.GetBytes(stOutput)
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)
        bw.Write(output, 0, output.Length)
        'write the encoded file
        bw.Flush()
        bw.Close()
        fs.Close()

    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        frmCreateCustomeReport.MdiParent = frmMDImain
        frmCreateCustomeReport.Show()
    End Sub

    Private Sub dgReport_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgReport.CellContentClick

    End Sub

    Private Sub txtReportNo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtReportNo.Validating
        Load_Query()
    End Sub


    Private Sub Load_Query()
        If Trim(txtReportNo.Text) = "" Then
            Exit Sub
        End If
        Try
            strSQL = "SELECT     REPORT_NAME, QUERY, REPORT_DESC  FROM         UTBL_CUSTOME_REPORT WHERE REPORT_NO =@REPORT_NO AND ACTIVE_REPORT = 1 AND COM_ID = '" & globalVariables.selectedCompanyID & "'"
            dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlCommand.Parameters.AddWithValue("@REPORT_NO", Trim(txtReportNo.Text))
            dbConnections.dReader = dbConnections.sqlCommand.ExecuteReader

            Dim hasRecords As Boolean = False
            While dbConnections.dReader.Read
                hasRecords = True
                txtReportName.Text = dbConnections.dReader.Item("REPORT_NAME")

                txtSql.Text = dbConnections.dReader.Item("QUERY")
                If IsDBNull(dbConnections.dReader.Item("REPORT_DESC")) Then
                    txtDesc.Text = ""
                Else
                    txtDesc.Text = dbConnections.dReader.Item("REPORT_DESC")
                End If

            End While
            dbConnections.dReader.Close()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub
    Private Sub txtReportNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReportNo.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub
    Private Sub txtReportNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReportNo.KeyPress
        generalValObj.isDigit(e)
    End Sub

    Private Sub txtReportNo_TextChanged(sender As Object, e As EventArgs) Handles txtReportNo.TextChanged

    End Sub

    Private Sub frmCustomReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        DisableALLTextBoxSound(Me, e)
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
    End Sub

    Private Sub frmCustomReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not UsergroupName = "Administrator" Then
            txtSql.Visible = False

        End If



    End Sub


    Dim Current_RowIndex As Integer = 0
    Dim LastKeyword As String = ""
    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        btnFind.Enabled = False
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        If LastKeyword <> Trim(txtFindKeyWord.Text) Then
            Current_RowIndex = 0
            ProgressBar1.Value = Current_RowIndex
        End If


        For i = Current_RowIndex To dgReport.RowCount
            If IsDBNull(dgReport.Rows(i).Cells("" & cmbColFind.Text & "").Value) = False Then

                If dgReport.RowCount = i + 1 Then
                    Current_RowIndex = 0
                    ProgressBar1.Value = 0
                    MessageBox.Show("No records found.", "No records.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit For
                End If


                If dgReport.Rows(i).Cells("" & cmbColFind.Text & "").Value.ToString = Trim(txtFindKeyWord.Text) Then
                    dgReport.Rows(i).Selected = True
                    dgReport.FirstDisplayedScrollingRowIndex = i
                    Current_RowIndex = i + 1
                    Exit For
                End If


            End If

            ProgressBar1.Value = Current_RowIndex
            Current_RowIndex = i
        Next

        LastKeyword = Trim(txtFindKeyWord.Text)
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        btnFind.Enabled = True
    End Sub

    Private Sub dgReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgReport.CellFormatting
        If cbRowNumbers.Checked = True Then
            dgReport.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)
        End If

    End Sub

    Private Sub cbRowNumbers_CheckedChanged(sender As Object, e As EventArgs) Handles cbRowNumbers.CheckedChanged
        If cbRowNumbers.Checked = True Then
            cbRowNumbers.Text = "YES"
        Else
            cbRowNumbers.Text = "NO"
        End If
    End Sub
End Class