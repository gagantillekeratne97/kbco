Imports System.Data.SqlClient

Public Class frmPrintInvoice

    Private Sub frmPrintInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()
        PrintInvoice()
        Me.Dispose()
    End Sub

    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo




    Private Sub PrintInvoice()

        If Trim(Me.Text) = "" Then
            Exit Sub
        End If
        Try

            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"

                Dim path As String = globalVariables.crystalReportpath & "\Reports\rptKBOInvoice.rpt"

                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.INV_NO} = '" & Me.Text & "' and {TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


                With crConnectionInfo
                    .ServerName = selectedServerName
                    .DatabaseName = selectedDatabaseName
                    .UserID = "db_ab8b61_kbco_admin"
                    .Password = "Ssg789.541351"
                End With

                CrTables = cryRpt.Database.Tables
                For Each CrTable In CrTables
                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)
                Next

   

                cryRpt.PrintOptions.PrinterName = globalVariables.DefaultPrinterName

                '// Seeting up Internal form Paper size by locating the 'Kbdispatch' name print server propertis and get the paper size
                Try
                    Dim ObjPrinterSetting As New System.Drawing.Printing.PrinterSettings
                    Dim PkSize As New System.Drawing.Printing.PaperSize
                    ObjPrinterSetting.PrinterName = globalVariables.DefaultPrinterName
                    For i As Integer = 0 To ObjPrinterSetting.PaperSizes.Count - 1
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "Letter" Then
                            PkSize = ObjPrinterSetting.PaperSizes.Item(i)
                            Exit For
                        End If
                    Next

                    If PkSize IsNot Nothing Then
                        Dim myAppPrintOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions = cryRpt.PrintOptions
                        myAppPrintOptions.PrinterName = globalVariables.DefaultPrinterName
                        myAppPrintOptions.PaperSize = CType(PkSize.RawKind, CrystalDecisions.Shared.PaperSize)
                        'cryRpt.PrintOptions.PaperOrientation = IIf(1 = 1, CrystalDecisions.Shared.PaperOrientation.Portrait, CrystalDecisions.Shared.PaperOrientation.Landscape)

                    End If
                    PkSize = Nothing



                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try



                cryRpt.PrintToPrinter(1, False, 0, 0)
                'reportformObj.CrystalReportViewer1.Refresh()
                'reportformObj.CrystalReportViewer1.ReportSource = cryRpt
                'reportformObj.CrystalReportViewer1.Refresh()
                'reportformObj.Show()


                path = ""

            End Using

            Try
                Dim strSQL As String
                dbConnections.sqlTransaction = dbConnections.sqlConnection.BeginTransaction
                strSQL = "UPDATE    TBL_INVOICE_MASTER SET              INV_PRINTED =@INV_PRINTED WHERE     (COM_ID =@COM_ID) AND (INV_NO =@INV_NO)"

                dbConnections.sqlCommand = New SqlCommand(strSQL, dbConnections.sqlConnection, dbConnections.sqlTransaction)
                dbConnections.sqlCommand.Parameters.AddWithValue("@COM_ID", Trim(globalVariables.selectedCompanyID))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_NO", Trim(Me.Text))
                dbConnections.sqlCommand.Parameters.AddWithValue("@INV_PRINTED", True)
                dbConnections.sqlCommand.ExecuteNonQuery()

            Catch ex As Exception

            End Try



        Catch ex As Exception
            dbConnections.sqlTransaction.Rollback()
            'MsgBox(ex.Message)
        Finally
            dbConnections.sqlTransaction.Commit()
        End Try
    End Sub
End Class