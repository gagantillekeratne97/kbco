Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Text.Json
Imports System.IO
Imports System.Text

Public Class InternalCancelledListsReport
    Public startDate As String = ""
    Public endDate As String = ""
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        LoadCancelledInternals()
    End Sub

    Private Async Sub LoadCancelledInternals()
        Dim apiUrl As String = $"{dbConnections.kbcoAPIEndPoint}/getcancelledrequest?companyID={globalVariables.selectedCompanyID}&startDate={startDate}&endDate={endDate}"
        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)
                Dim dt As New DataTable()
                If response.IsSuccessStatusCode Then
                    Dim jsonString As String = Await response.Content.ReadAsStringAsync()
                    Dim techMasterDataList As List(Of InternalCancelledVM) = JsonConvert.DeserializeObject(Of List(Of InternalCancelledVM))(jsonString)
                    ' Create DataTable with columns matching your class properties                    
                    dt.Columns.Add("IR_NO", GetType(String))
                    dt.Columns.Add("IR_DATE", GetType(DateTime))
                    dt.Columns.Add("BELEETA_REFERENCE_NO", GetType(String))
                    dt.Columns.Add("PN_NO", GetType(String))
                    dt.Columns.Add("SERIAL_NO", GetType(String))
                    dt.Columns.Add("CUS_NAME", GetType(String))
                    dt.Columns.Add("CUS_LOC", GetType(String))
                    dt.Columns.Add("IR_STATE", GetType(String))

                    ' Loop through list and add rows
                    For Each item As InternalCancelledVM In techMasterDataList
                        dt.Rows.Add(item.IR_NO, item.IR_DATE, item.BELEETA_REFERENCE_NO, item.PN_NO,
                                    item.SERIAL_NO, item.CUS_NAME, item.CUS_LOC, item.IR_STATE)
                    Next

                    ' Bind DataTable to DataGridView
                    dgCancelledIR.DataSource = dt
                    ' Optional: Set nicer headers
                    With dgCancelledIR.Columns
                        .Item("IR_NO").HeaderText = "IR NO"
                        .Item("IR_DATE").HeaderText = "IR DATE"
                        .Item("BELEETA_REFERENCE_NO").HeaderText = "Beleeta No"
                        .Item("PN_NO").HeaderText = "PN No"
                        .Item("SERIAL_NO").HeaderText = "Serial No"
                        .Item("CUS_NAME").HeaderText = "Customer Name"
                        .Item("CUS_LOC").HeaderText = "Location"
                        .Item("IR_STATE").HeaderText = "Status"
                    End With
                Else
                    MessageBox.Show("Failed to load data. Status: " & response.StatusCode)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "Excel Documents (*.xls)|*.xls"
        'sfd.FileName = "" & Trim(txtRepCode.Text) & "SOBackup.xls"
        If sfd.ShowDialog() = DialogResult.OK Then
            ToCsV(dgCancelledIR, sfd.FileName)
        End If
    End Sub
End Class

Public Class InternalCancelledVM
    Public Property IR_NO As String
    Public Property IR_DATE As DateTime
    Public Property BELEETA_REFERENCE_NO As String
    Public Property PN_NO As String
    Public Property SERIAL_NO As String
    Public Property CUS_NAME As String
    Public Property CUS_LOC As String
    Public Property IR_STATE As String
End Class