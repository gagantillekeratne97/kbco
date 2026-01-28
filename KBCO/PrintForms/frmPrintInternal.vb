Public Class frmPrintInternal

    Private Sub frmPrintInternal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        globalVariables.DefaultPrinterName = globalFunctions.GetDefaultPrinter()
        PrintInternal()
        Me.Dispose()
    End Sub


    Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
    Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
    Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo


    Private Sub PrintInternal()

        If Trim(Me.Text) = "" Then
            Exit Sub
        End If
        Try
            Dim path As String = ""
            Using cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim reportformObj As New frmCrystalReportViwer
                Dim reportNamestring As String = "Report"
                If globalVariables.selectedCompanyID = "003" Then
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal_Fintek.rpt"
                Else
                    path = globalVariables.crystalReportpath & "\Reports\rptKBOInternal.rpt"
                End If


                'Dim manual report As New rptBank

                cryRpt.Load(path)

                cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.IR_NO} = '" & Me.Text & "' and {TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


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
                        If ObjPrinterSetting.PaperSizes.Item(i).PaperName = "KBI" Then
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
                Dim PrtDialog = New PrintDialog
                PrtDialog.PrinterSettings.PrinterName = globalVariables.DefaultPrinterName
                cryRpt.PrintOptions.PrinterName = PrtDialog.PrinterSettings.PrinterName

                cryRpt.PrintToPrinter(1, False, 0, 0)

                path = ""
            End Using


        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally

        End Try
    End Sub
End Class