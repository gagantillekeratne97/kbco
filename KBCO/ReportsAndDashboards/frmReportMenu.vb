Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.Net
Imports System.IO
Public Class frmReportMenu

    Dim techCodeQuery As String = ""
    Dim CusCodeQuery As String = ""
    Dim RepCodeQuery As String = ""
    Dim SNQuery As String = ""

    Private Sub btnInvoiceReport_Click(sender As Object, e As EventArgs) Handles btnInvoiceReport.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoiceList.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If
            '{TBL_INVOICE_MASTER.REP_CODE} {TBL_INVOICE_MASTER.CUS_ID}
            '{TBL_INVOICE_MASTER.INV_STATUS_T} <> "CANCELLED"
            'cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND ({TBL_INVOICE_MASTER.INV_STATUS_T} = 'RECIPTED' and {TBL_INVOICE_MASTER.INV_STATUS_T} = 'NULL') AND {TBL_INVOICE_MASTER.INV_DATE}  in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""
            cryRpt.RecordSelectionFormula = "({TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND " &
    "{TBL_INVOICE_MASTER.INV_DATE} >= CDate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') AND " &
    "{TBL_INVOICE_MASTER.INV_DATE} <= CDate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "')) AND " &
    "(IsNull({TBL_INVOICE_MASTER.INV_STATUS_T}) OR {TBL_INVOICE_MASTER.INV_STATUS_T} <> 'CANCELLED')" &
    CusCodeQuery & techCodeQuery & RepCodeQuery



            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"



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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnMachineList_Click(sender As Object, e As EventArgs) Handles btnMachineList.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBCOMachineList.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            '{TBL_MACHINE_TRANSACTIONS.CUS_ID} = "10"

            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = "AND {TBL_MACHINE_TRANSACTIONS.CUS_ID} ='" & Trim(txtRCusID.Text) & "'"
            End If

            cryRpt.RecordSelectionFormula = "{TBL_MACHINE_TRANSACTIONS.COM_ID}  = '" & globalVariables.selectedCompanyID & "' " & CusCodeQuery & ""




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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnMachineAgreement_Click(sender As Object, e As EventArgs) Handles btnMachineAgreement.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBCOMachineAgreementDetails.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            ' {TBL_MACHINE_TRANSACTIONS.CUS_ID} {TBL_MACHINE_TRANSACTIONS.REP_CODE}
            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_MACHINE_TRANSACTIONS.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_MACHINE_TRANSACTIONS.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If


            cryRpt.RecordSelectionFormula = "{TBL_MACHINE_TRANSACTIONS.COM_ID}  = '" & globalVariables.selectedCompanyID & "' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""




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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnMachineRates_Click(sender As Object, e As EventArgs) Handles btnMachineRates.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOMachineRates.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_MACHINE_TRANSACTIONS.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_MACHINE_TRANSACTIONS.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If




            cryRpt.RecordSelectionFormula = "{TBL_MACHINE_TRANSACTIONS.COM_ID}  = '" & globalVariables.selectedCompanyID & "'  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""




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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub


    Private Sub btnICUR_Click(sender As Object, e As EventArgs) Handles btnICUR1.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInternal_Con_Uty_Report2.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)




            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = "AND {TBL_INTERNAL_MAIN.CUS_CODE} ='" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = " AND  {TBL_INTERNAL_MAIN.ISSUED_TO} = '" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If
            ' {TBL_INTERNAL_MAIN.CUS_CODE} {TBL_INTERNAL_MAIN.ISSUED_TO}




            cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "dd/MM/yyyy") & "')  AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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


            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub


    Private Sub btnICUR2_Click(sender As Object, e As EventArgs) Handles btnICUR2.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInternal_Con_Uty_Report1.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = "  AND {TBL_INTERNAL_MAIN.ISSUED_TO} ='" & Trim(txtRCusID.Text) & "'   "
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If


            cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "dd/MM/yyyy") & "') AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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





            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnMBVReport_Click(sender As Object, e As EventArgs) Handles btnMBVReport.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOMachineBVReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            cryRpt.RecordSelectionFormula = "{TBL_MACHINE_STOCK.COM_ID} = '" & globalVariables.selectedCompanyID & "'"

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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnINV_List_for_Month_Click(sender As Object, e As EventArgs) Handles btnINV_List_for_Month.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInvoiceListForMonth.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If

            '{TBL_INVOICE_MASTER.REP_CODE} {TBL_INVOICE_MASTER.CUS_ID}


            cryRpt.RecordSelectionFormula = "{TBL_INVOICE_DET.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INVOICE_MASTER.INV_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "dd/MM/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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

            '' Set up export options
            'Dim exportOpts As ExportOptions = cryRpt.ExportOptions
            'Dim pdfFormatOpts As New PdfRtfWordFormatOptions()
            'exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat
            'exportOpts.FormatOptions = pdfFormatOpts

            'Dim something = AppDomain.CurrentDomain.BaseDirectory

            '' Define the destination for the exported report
            'Dim diskOpts As New DiskFileDestinationOptions()
            'diskOpts.DiskFileName = something
            'exportOpts.ExportDestinationOptions = diskOpts
            'exportOpts.ExportDestinationType = ExportDestinationType.DiskFile

            '' Export the report to PDF
            'cryRpt.Export()

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnReciptList_Click(sender As Object, e As EventArgs) Handles btnReciptList.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOReciptList.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND  {TBL_RECIPT_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_RECIPT_DET.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If


            cryRpt.RecordSelectionFormula = "{TBL_RECIPT_DET.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_RECIPT_MASTER.RECIPT_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "')  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnInternalBYtech_Click(sender As Object, e As EventArgs) Handles btnInternalBYtech.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInternalByTechOfficer.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = "{TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = " AND {TBL_INTERNAL_MAIN.ISSUED_TO}='" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If
            '{TBL_INTERNAL_MAIN.IR_DATE}{TBL_INTERNAL_MAIN.ISSUED_TO}

            cryRpt.RecordSelectionFormula = " {TBL_INTERNAL_ITEMS.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "')  AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnDebtors_Click(sender As Object, e As EventArgs) Handles btnDebtors.Click
        frmDebtorsReport.MdiParent = frmMDImain
        frmDebtorsReport.txtRCusID.Text = Trim(txtRCusID.Text)
        frmDebtorsReport.txtRRepCode.Text = Trim(txtRRepCode.Text)
        frmDebtorsReport.Show()
    End Sub

    Private Sub btnRcr_Click(sender As Object, e As EventArgs) Handles btnRcr.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOReciptColletionDateReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            '  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ")
            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_RECIPT_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = " AND {TBL_RECIPT_MASTER.RECIVED_BY}='" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If

            '{TBL_RECIPT_MASTER.CUS_ID} {TBL_RECIPT_MASTER.RECIVED_BY}
            cryRpt.RecordSelectionFormula = " {TBL_RECIPT_DET.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_RECIPT_MASTER.RECIPT_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnRLBB_Click(sender As Object, e As EventArgs) Handles btnRLBB.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOReciptListByBankReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            '  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ")
            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_RECIPT_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = " AND {TBL_RECIPT_MASTER.RECIVED_BY}='" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If



            cryRpt.RecordSelectionFormula = " {TBL_RECIPT_MASTER.COM_ID}  = '" & globalVariables.selectedCompanyID & "' AND {TBL_RECIPT_MASTER.RECIPT_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            'cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            'cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnYieldReport_Click(sender As Object, e As EventArgs) Handles btnYieldReport.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOYieldReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            cryRpt.RecordSelectionFormula = " {TBL_DEVICES_AND_ITEMS.COM_ID}  = '" & globalVariables.selectedCompanyID & "'"


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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnMRL_Click(sender As Object, e As EventArgs) Handles btnMRL.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOMachineReturnList.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)



            '  " & CusCodeQuery + techCodeQuery + RepCodeQuery & "
            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_RETUN_TRANSACTION.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_RETUN_TRANSACTION.REP_CODE}='" & Trim(txtRTechCode.Text) & "'"
            End If

            '{TBL_RETUN_TRANSACTION.REP_CODE}{TBL_RETUN_TRANSACTION.CUS_ID}

            cryRpt.RecordSelectionFormula = " {TBL_RETUN_TRANSACTION.COM_ID}   = '" & globalVariables.selectedCompanyID & "' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""


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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub frmReportMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnCusList_Click(sender As Object, e As EventArgs) Handles btnCusList.Click
        'rptKBOCustomers

        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOCustomers.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)




            cryRpt.RecordSelectionFormula = "{MTBL_CUSTOMER_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"


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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""

            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnCPCReport_Click(sender As Object, e As EventArgs) Handles btnCPCReport.Click
        frmGPCPCReport.MdiParent = frmMDImain
        frmGPCPCReport.txtTechCode.Text = txtRTechCode.Text
        frmGPCPCReport.dtpStartDate.Value = dtpStartDate.Value
        frmGPCPCReport.dtpEndDate.Value = dtpEndDate.Value
        frmGPCPCReport.Show()
    End Sub

    Private Sub btnSvatReport_Click(sender As Object, e As EventArgs) Handles btnSvatReport.Click

        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOSvatInvReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND  {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If


            cryRpt.RecordSelectionFormula = "{TBL_INVOICE_DET.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INVOICE_MASTER.VAT_TYPE} ='8' AND  {TBL_INVOICE_MASTER.INV_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "') AND isnull({TBL_INVOICE_MASTER.INV_STATUS_T}) = true  " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            'cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            'cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"


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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub


    Private Sub btnYBSR_Click(sender As Object, e As EventArgs) Handles btnYBSR.Click
        'rptKBOYieldbySerialReport
        '{TBL_INTERNAL_ITEMS.COM_ID} = "001" {TBL_INTERNAL_ITEMS.IR_NO} {TBL_INTERNAL_ITEMS.IR_DATE} {TBL_INTERNAL_MAIN.ISSUED_TO} {TBL_INTERNAL_MAIN.CUS_CODE}
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOYieldbySerialReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = " AND {TBL_INTERNAL_MAIN.ISSUED_TO} = '" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If

            If Trim(txtSN.Text) = "" Then
                SNQuery = ""
            Else
                SNQuery = " AND {TBL_INTERNAL_MAIN.SERIAL_NO}  = '" & Trim(txtSN.Text) & "'"
            End If




            cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_ITEMS.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND  {TBL_INTERNAL_ITEMS.IR_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "')  " & CusCodeQuery + techCodeQuery + RepCodeQuery + SNQuery & ""

            'cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            'cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"


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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub txtRTechCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRTechCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub



    Private Sub txtRRepCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRRepCode.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub



    Private Sub txtRCusID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRCusID.KeyDown
        If e.KeyCode = Keys.F2 Then Dim searchObj As New frmSearch(sender, Me.Tag)
    End Sub


    Private Sub btnCancelledInvReport_Click(sender As Object, e As EventArgs) Handles btnCancelledInvReport.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOCancelledInvoiceList.rpt"


            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)

            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND {TBL_INVOICE_MASTER.CUS_ID} = '" & Trim(txtRCusID.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = ""
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = " AND {TBL_INVOICE_MASTER.REP_CODE} = '" & Trim(txtRRepCode.Text) & "'"
            End If
            '{TBL_INVOICE_MASTER.REP_CODE} {TBL_INVOICE_MASTER.CUS_ID}
            '{TBL_INVOICE_MASTER.INV_STATUS_T} <> "CANCELLED"
            cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INVOICE_MASTER.INV_STATUS_T} = 'CANCELLED' AND {TBL_INVOICE_MASTER.INV_DATE}  in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "') " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"



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



            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnIntCusConsumRpt_Click(sender As Object, e As EventArgs) Handles btnIntCusConsumRpt.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOInternal_Consumtion_Customer_Wice_Report.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            If Trim(txtRCusID.Text) = "" Then
                CusCodeQuery = ""
            Else
                CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRTechCode.Text) & "'"
            End If

            If Trim(txtRTechCode.Text) = "" Then
                techCodeQuery = ""
            Else
                techCodeQuery = "  AND {TBL_INTERNAL_MAIN.ISSUED_TO} ='" & Trim(txtRCusID.Text) & "'   "
            End If

            If Trim(txtRRepCode.Text) = "" Then
                RepCodeQuery = ""
            Else
                RepCodeQuery = ""
            End If


            cryRpt.RecordSelectionFormula = "{TBL_INTERNAL_MAIN.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_INTERNAL_MAIN.IR_DATE} in cdate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') to cdate('" & Format(dtpEndDate.Value, "dd/MM/yyyy") & "') AND {TBL_INTERNAL_MAIN.IR_STATE} <> 'INTERNAL CANCELLED' " & CusCodeQuery + techCodeQuery + RepCodeQuery & ""

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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





            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnCreditNotes_Click(sender As Object, e As EventArgs) Handles btnCreditNotes.Click
        Try
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath & "\Reports\CreditNoteRpt.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)
            cryRpt.RecordSelectionFormula = "{TBL_CREDIT_NOTES.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND {TBL_CREDIT_NOTES.REQ_DATE} in '" & Format(dtpStartDate.Value, "yyyy/MM/dd") & "' to '" & Format(dtpEndDate.Value, "yyyy/MM/dd") & "'"

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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            '/=======================
            Dim reportformObj As New frmCrystalReportViwer
            Dim reportNamestring As String = "Report"
            Dim AdminUser As Boolean = False
            Dim path As String = ""
            path = globalVariables.crystalReportpath + "\Reports\rptKBOMonthlyRevenueReport.rpt"

            Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim CrTables As CrystalDecisions.CrystalReports.Engine.Tables
            Dim crtableLogoninfo As New CrystalDecisions.Shared.TableLogOnInfo
            Dim crConnectionInfo As New CrystalDecisions.Shared.ConnectionInfo

            cryRpt.Load(path)


            'If Trim(txtRCusID.Text) = "" Then
            '    CusCodeQuery = ""
            'Else
            '    CusCodeQuery = " AND  {TBL_INTERNAL_MAIN.CUS_CODE} = '" & Trim(txtRTechCode.Text) & "'"
            'End If

            'If Trim(txtRTechCode.Text) = "" Then
            '    techCodeQuery = ""
            'Else
            '    techCodeQuery = "  AND {TBL_INTERNAL_MAIN.ISSUED_TO} ='" & Trim(txtRCusID.Text) & "'   "
            'End If

            'If Trim(txtRRepCode.Text) = "" Then
            '    RepCodeQuery = ""
            'Else
            '    RepCodeQuery = ""
            'End If

            'Changes made on 16-07-2025
            'Changes made by Gagan Tillekeratne.

            'cryRpt.RecordSelectionFormula = "{TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "'"
            cryRpt.RecordSelectionFormula = "({TBL_INVOICE_MASTER.COM_ID} = '" & globalVariables.selectedCompanyID & "' AND " &
            "{TBL_INVOICE_MASTER.INV_DATE} >= CDate('" & Format(dtpStartDate.Value, "MM/dd/yyyy") & "') AND " &
            "{TBL_INVOICE_MASTER.INV_DATE} <= CDate('" & Format(dtpEndDate.Value, "MM/dd/yyyy") & "')) AND " &
            "(IsNull({TBL_INVOICE_MASTER.INV_STATUS_T}) OR {TBL_INVOICE_MASTER.INV_STATUS_T} <> 'CANCELLED')"

            cryRpt.DataDefinition.FormulaFields.Item("Date").Text = "'" & dtpStartDate.Value.ToShortDateString & "'"
            cryRpt.DataDefinition.FormulaFields.Item("EDate").Text = "'" & dtpEndDate.Value.ToShortDateString & "'"

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

            reportformObj.CrystalReportViewer1.Refresh()
            cryRpt.Refresh()
            reportformObj.CrystalReportViewer1.ReportSource = cryRpt
            reportformObj.CrystalReportViewer1.Refresh()
            path = ""
            reportformObj.CrystalReportViewer1.ShowPrintButton = False
            reportformObj.Show()

        Catch ex As Exception
            MsgBox(ex.InnerException.Message)
        Finally

        End Try
    End Sub

    Private Sub btnInternalCancelledReport_Click(sender As Object, e As EventArgs) Handles btnInternalCancelledReport.Click
        InternalCancelledListsReport.startDate = dtpStartDate.Value.Date.ToString()
        InternalCancelledListsReport.endDate = dtpEndDate.Value.Date.ToString()
        InternalCancelledListsReport.Show()
    End Sub

    Private Sub btnInvoiceListMonthForm_Click(sender As Object, e As EventArgs) Handles btnInvoiceListMonthForm.Click
        frmInvoiceListForMonthForm.startDate = dtpStartDate.Value.ToString()
        frmInvoiceListForMonthForm.endDate = dtpEndDate.Value.ToString()
        frmInvoiceListForMonthForm.Show()
    End Sub
End Class