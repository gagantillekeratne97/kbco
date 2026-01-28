Public Class frmPrintRecipt
    'Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

    Private Sub frmPrintRecipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()
        PrintRecipt()
        Me.Dispose()
    End Sub


    Private Sub PrintRecipt()

        If Trim(Me.Text) = "" Then
            Exit Sub
        End If
        Try

            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"

                Dim path As String = globalVariables.crystalReportpath & "\Reports\rptKBOIRecipt.rpt"

                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_RECIPTS.RECIPT_ID} = " & Me.Text & " and {TBL_RECIPTS.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


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
                cryRpt.PrintToPrinter(1, False, 0, 0)

            End Using


        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

        End Try
    End Sub
End Class