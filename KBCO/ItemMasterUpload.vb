Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML
Imports ClosedXML.Excel
Imports Dapper
Imports DocumentFormat.OpenXml.Spreadsheet

Public Class frmItemMasterUpload
    Dim connectionString As String =
        ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private Sub btnDownloadTemplateFile_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplateFile.Click
        Try
            '//Get the template file path
            Dim templatePath As String = Path.Combine(Application.StartupPath, "Template", "template.xls")

            '//IF the file does not exists shows an error message 
            If Not File.Exists(templatePath) Then
                MessageBox.Show("Template file not found.",
                           "Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
                Return
            End If

            'Initialize the save file dialog properties 
            Dim sfd As New SaveFileDialog()

            sfd.Filter = "Excel Files|*.xlsx"
            sfd.FileName = "ItemMasterTemplate.xlsx"

            If sfd.ShowDialog() = DialogResult.OK Then
                File.Copy(templatePath, sfd.FileName, True)

                MessageBox.Show("Template downloaded successfully.",
                           "Success",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        Try
            Dim ofd As New OpenFileDialog()

            ' Filter only Excel files
            ofd.Filter = "Excel Files|*.xlsx;*.xls"

            ' Dialog title
            ofd.Title = "Select Excel File"

            ' Open dialog
            If ofd.ShowDialog() = DialogResult.OK Then

                ' Show selected file path in textbox
                txtFilePath.Text = ofd.FileName

                ' fill the datagridview 
                Dim dt As New DataTable()

                Using Workbook As New XLWorkbook(txtFilePath.Text)
                    Dim ws = Workbook.Worksheet(1)

                    Dim range = ws.RangeUsed()

                    If range Is Nothing Then
                        MessageBox.Show("Excel file is empty")
                    End If

                    For Each cell In range.FirstRow().Cells()
                        dt.Columns.Add(cell.Value.ToString())
                    Next

                    For Each row In range.RowsUsed().Skip(1)
                        Dim dr As DataRow = dt.NewRow()
                        For i As Integer = 0 To dt.Columns.Count - 1
                            dr(i) = row.Cell(i + 1).Value.ToString()
                        Next

                        dt.Rows.Add(dr)
                    Next
                End Using

                dgItemView.DataSource = dt

                MessageBox.Show("Excel data loaded successfully.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message,
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSaveToSystem_Click(sender As Object, e As EventArgs) Handles btnSaveToSystem.Click
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                'initializing variables 
                Dim itemCode As String = ""
                Dim itemName As String = ""
                Dim quantity As Integer = 0
                Dim costPrice As Decimal = 0

                Dim query As String = ""

                ProgressBar1.Minimum = 0
                ProgressBar1.Maximum = dgItemView.Rows.Count - 1
                ProgressBar1.Value = 0

                Dim currentRow As Integer = 0

                'read the datagridview and get the items details 
                For Each row As DataGridViewRow In dgItemView.Rows
                    If Not row.IsNewRow Then
                        itemCode = row.Cells(0).Value.ToString()
                        itemName = row.Cells(1).Value.ToString()
                        quantity = row.Cells(2).Value
                        costPrice = row.Cells(3).Value
                    End If

                    Dim exists As Integer = connection.ExecuteScalar(Of Integer)(
                        "SELECT COUNT(*) FROM TBL_DEVICES_AND_ITEMS 
                        WHERE DAI_PN = @daipn", New With {
                            .daipn = itemCode
                        }, commandTimeout:=120)

                    If exists > 0 Then
                        query = "UPDATE TBL_DEVICES_AND_ITEMS SET DAI_UNIT_PRICE = @unitprice, QTY = @quantity
                        WHERE DAI_PN = @itemcode"

                        connection.Execute(query, New With {
                            .unitprice = costPrice,
                            .quantity = quantity,
                            .itemcode = itemCode
                        }, commandTimeout:=120)
                    Else
                        query = "INSERT INTO TBL_DEVICES_AND_ITEMS (COM_ID, DAI_PN, DAI_NAME, DAI_DESC, 
                        DAI_UNIT_PRICE, DAI_ACTIVE, QTY, ITEM_CLASS, VAT_AVAILABLE)
                        VALUES (@companyid, @daipn, @dainame, @daidesc, @daiunitprice, @daiactive, @qty, 
                        @itemclass, @vatavailable)"

                        connection.Execute(query, New With {
                            .companyid = globalVariables.selectedCompanyID,
                            .daipn = itemCode,
                            .dainame = itemName,
                            .daidesc = itemName,
                            .daiunitprice = costPrice,
                            .daiactive = True,
                            .qty = quantity,
                            .itemclass = "Stock item",
                            .vatavailable = True
                        }, commandTimeout:=120)
                    End If

                    currentRow += 1
                    ProgressBar1.Value = currentRow

                    Application.DoEvents()
                Next

                MessageBox.Show("Items processed successfully")
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message,
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error)
        End Try
    End Sub
End Class