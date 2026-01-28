Imports System.Data.SqlClient
Imports System.Data
Public Class frmP2PSearch
    Private callingFormControl As Object
    Private strSelectQuery As String
    Private tableName As String
    Private columns As New Dictionary(Of String, String)
    Private returnField As Integer
    Private viewMode As Integer
    Private resultsPerPage As Integer
    Private querySplit() As String '//Full query spited into two parts
    Private columnSizes() As String
    Private orderBy As String
    Private defaultOrder As String
    Private endWildCard As String
    Private startingWildCard As String
    Private totalPageCount As Integer  '//Saves the amount of pages retrieved during each search
    Private pageStep As Integer = 1
    Private recordCount As Integer

    Private Sub frmSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Enter Then SendKeys.Send("{TAB}")
        If e.KeyCode = Keys.ShiftKey Then txtSearch.Focus()
        If e.KeyCode = Keys.PageDown Then Me.showNextPage()
        If e.KeyCode = Keys.PageUp Then Me.showPreviousPage()
        If e.KeyCode = Keys.Escape Then Me.Close()
        If Not dgSearch.Focused And (e.KeyCode = Keys.Up Or e.KeyCode) = Keys.Down Then dgSearch.Focus()
    End Sub

    Public Sub New(ByVal sender As Object, ByVal formTag As String, Optional ByVal OverrideTblName As String = "")
        InitializeComponent()
        callingFormControl = sender
        Dim controlName As String = ""
        connectionStaet()
        '//Determine the sending form control which triggered the search
        If TypeOf callingFormControl Is System.Windows.Forms.TextBox Then
            controlName = callingFormControl.Name '//sets the selected row item to the txtBox
            Me.Text = "Search - " & callingFormControl.Text
        ElseIf TypeOf callingFormControl Is DataGridViewTextBoxCell Then

            controlName = callingFormControl.OwningColumn.Name.ToString '//sets the selected row item to the grid view cell
            Me.Text = "Search - " & callingFormControl.OwningColumn.HeaderText.ToString
        End If
        'Me.Text = "Search - " & sender.FindForm.Text
        '//Select search controls from the db
        Try
            strSelectQuery = "SELECT CUSTOM_SQL,DEFAULT_SEARCHBY,RETURN_FIELD,DEFAULT_VIEW_MODE,RESULTS_PER_PAGE,COLUMN_SIZES,ORDER_BY,DEFAULT_ORDER_BY FROM U_TBLSEARCH_CONTROLS WHERE FORM_TAG='" & formTag & "' AND CONTROL_NAME='" & controlName & "'"
            dbConnections.sqlCommand = New SqlCommand(strSelectQuery, sqlConnection)
            dbConnections.dReader = sqlCommand.ExecuteReader
            If dbConnections.dReader.Read Then
                querySplit = Split(dbConnections.dReader.Item(0), "FROM")
                If OverrideTblName = "" Then '//Check whether the table name is passed by the calling form
                    tableName = querySplit(1)
                Else
                    tableName = " " & OverrideTblName
                End If
                Dim tmpColumns() As String = Split(querySplit(0), ",")

                For Each column As String In tmpColumns
                    columns.Add(Split(column, "AS")(0), Split(column, "AS")(1))
                Next

                '//Add values to from combo
                Dim pair As New KeyValuePair(Of String, String)
                For Each pair In columns
                    cmbSearchFrom.Items.Add(Replace(pair.Value, "'", ""))
                Next
                cmbSearchFrom.SelectedIndex = dbConnections.dReader.Item(1)
                returnField = dbConnections.dReader.Item(2)
                viewMode = dbConnections.dReader.Item(3)
                resultsPerPage = dbConnections.dReader.Item(4)
                columnSizes = Split(dbConnections.dReader.Item(5), ",")
                orderBy = dbConnections.dReader.Item(6)
                defaultOrder = dbConnections.dReader.Item(7)
                dbConnections.dReader.Close()
                If viewMode = 1 Then '//If show all is enabled
                    Me.showAll()
                End If
            End If
            Me.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dbConnections.dReader.Close()
            connectionClose()
        End Try
    End Sub

    Private Sub search()
        P2PConnection.ConnectionString = P2PConnectionString
        P2PConnection.Open()

        If Not txtSearch.Text.Length = 0 Or viewMode = 1 Then '//Disables searching if no text is entered, but provides a exception when show all record view mode is enabled
            Try
                Dim WhereOrAnd As String = " WHERE "
                '//If the SQL statement contains Where keywords replace them with and to make a valid SQL
                If querySplit(1).Contains("WHERE") Or querySplit(1).Contains("Where") Or querySplit(1).Contains("where") Then WhereOrAnd = " AND "
                strSelectQuery = "SELECT COUNT(*) FROM" & tableName & WhereOrAnd & columns.Keys(cmbSearchFrom.SelectedIndex) & " LIKE " & startingWildCard & "'+@searchText+'" & endWildCard & ""
                P2PCommand = New SqlCommand(strSelectQuery, P2PConnection)
                P2PCommand.Parameters.AddWithValue("@searchText", Trim(txtSearch.Text))
                recordCount = dbConnections.P2PCommand.ExecuteScalar
                totalPageCount = Math.Ceiling(recordCount / resultsPerPage)
                If recordCount >= 0 Then '//If there are records to be searched
                    If (pageStep * recordCount) > recordCount Then
                        strSelectQuery = "SELECT TOP " & recordCount Mod resultsPerPage & " * FROM (SELECT TOP " & recordCount & " " & querySplit(0) & " FROM" & tableName & WhereOrAnd & columns.Keys(cmbSearchFrom.SelectedIndex) & " LIKE " & startingWildCard & "'+@searchText+'" & endWildCard & " " & orderBy & ")As Results ORDER BY " & defaultOrder & " DESC"
                    Else
                        strSelectQuery = "SELECT TOP " & resultsPerPage & " * FROM (SELECT TOP " & pageStep * resultsPerPage & " " & querySplit(0) & " FROM" & tableName & WhereOrAnd & columns.Keys(cmbSearchFrom.SelectedIndex) & " LIKE " & startingWildCard & "'+@searchText+'" & endWildCard & " " & orderBy & ") AS Results ORDER BY " & defaultOrder & " DESC"
                    End If
                    P2PCommand = New SqlCommand(strSelectQuery, P2PConnection)
                    ' P2PCommand.CommandText = strSelectQuery
                    P2PCommand.Parameters.AddWithValue("@searchText", Trim(txtSearch.Text))
                    P2PDset = New DataSet
                    P2PsqlAdeptor = New SqlDataAdapter(P2PCommand)
                    P2PsqlAdeptor.Fill(P2PDset, "tmpResults")
                    Dim colWidthCount As Integer = 0
                    dgSearch.DataSource = P2PDset.Tables("tmpResults")
                    For Each ColumnWidth As String In columnSizes
                        dgSearch.Columns(colWidthCount).Width = ColumnWidth
                        colWidthCount += 1
                    Next
                    Me.updateStatusStrip()
                    dgSearch.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Finally
                dbConnections.dset.Dispose()
                P2PConnection.Close()
            End Try
        Else
            dgSearch.DataSource = Nothing
            searchInfo.Show("Please enter a valid content to search", txtSearch, New Point((txtSearch.Width / 2) - 115, (txtSearch.Height / 2) + 15), 2500)
            txtSearch.Focus()
        End If
        P2PConnection.Close()
    End Sub

    Private Sub updateStatusStrip()
        searchStatusStrip.Items.Clear() '//Clear strip before adding items
        Dim dummyLabel As New ToolStripStatusLabel With {.Text = "", .Spring = True}
        searchStatusStrip.Items.Add(dummyLabel)
        Dim sStripPageCount As New ToolStripStatusLabel With {.Text = "Pages: " & totalPageCount}
        searchStatusStrip.Items.Add(sStripPageCount)
        Dim sStripResultCount As New ToolStripLabel With {.Text = "Possible results: " & recordCount}
        searchStatusStrip.Items.Add(sStripResultCount)
    End Sub

    Private Sub showAll()
        pageStep = 1
        txtSearch.Text = ""
        Me.search()
    End Sub

    Private Sub showPreviousPage()
        If pageStep <= 1 Then
        Else
            pageStep -= 1
            If pageStep <= 0 Then
                pageStep = 1
            End If
            Me.search()
            dgSearch.Focus()
        End If
    End Sub

    Private Sub showNextPage()
        If pageStep = totalPageCount Then
        Else
            pageStep += 1
            Me.search()
            dgSearch.Focus()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        search()
        DatagrdAssendingorder(dgSearch)
        dgSearch.Focus()
    End Sub

    Private Sub rdoStarting_CheckedChanged(ByVal sender As RadioButton, ByVal e As System.EventArgs) Handles rdoStarting.CheckedChanged
        If sender.Checked Then
            startingWildCard = "'"
            endWildCard = "%'"
        End If
    End Sub

    Private Sub rdoMiddleLetters_CheckedChanged(ByVal sender As RadioButton, ByVal e As System.EventArgs) Handles rdoMiddleLetters.CheckedChanged
        If sender.Checked Then
            endWildCard = "%'"
            startingWildCard = "'%"
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        showNextPage()
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        showPreviousPage()
    End Sub

    Private Sub dgSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            KeyPresSoundDisable(e)
        End If
        If e.KeyCode = Keys.Enter And recordCount > 0 Then '//On cell selection made by enter
            e.Handled = True '//prevent default action of enter key
            Dim selectedRowIndex As Integer = sender.CurrentCell.RowIndex
            Dim selectedRowItem As String = dgSearch.Item(returnField, selectedRowIndex).Value
            '//Determine the sending form control which triggered the search
            If TypeOf callingFormControl Is System.Windows.Forms.TextBox Then
                callingFormControl.Text = selectedRowItem '//sets the selected row item to the txtBox
            ElseIf TypeOf callingFormControl Is DataGridViewTextBoxCell Then
                callingFormControl.Value = selectedRowItem '//sets the selected row item to the grid view cell
            End If
            Me.Close()
        End If

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            KeyPresSoundDisable(e)
            Me.search()
        End If
        DatagrdAssendingorder(dgSearch)
    End Sub

    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DatagrdAssendingorder(dgSearch)
    End Sub


    Private Sub DatagrdAssendingorder(ByRef datagridName As DataGridView)
        'grid items sort to ascending order
        datagridName.Sort(datagridName.Columns("CODE"), System.ComponentModel.ListSortDirection.Ascending)
        datagridName.Refresh()
    End Sub

    Private Sub dgSearch_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgSearch.MouseDoubleClick
        Dim selectedRowIndex As Integer = sender.CurrentCell.RowIndex
        Dim selectedRowItem As String = dgSearch.Item(returnField, selectedRowIndex).Value
        '//Determine the sending form control which triggered the search
        If TypeOf callingFormControl Is System.Windows.Forms.TextBox Then
            callingFormControl.Text = selectedRowItem '//sets the selected row item to the txtBox
        ElseIf TypeOf callingFormControl Is DataGridViewTextBoxCell Then
            callingFormControl.Value = selectedRowItem '//sets the selected row item to the grid-view cell
        End If
        Me.Close()
    End Sub


End Class