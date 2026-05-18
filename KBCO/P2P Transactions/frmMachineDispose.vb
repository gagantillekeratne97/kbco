Imports System.ComponentModel
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices.ComTypes
Imports System.Windows
Imports Dapper
Imports Org.BouncyCastle.Asn1.Cms
Imports unvell.ReoGrid.Actions

Public Class frmMachineDispose
    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Search("SN")
    End Sub

    Private Function Search(ByRef SearchBy As String) As Boolean
        Search = False
        If Trim(txtSearch.Text) = "" Then
            Exit Function
        End If

        Dim selectedSerialNo As String = ""
        Dim selectedPNo As String = ""
        Dim selectedAgreementNo As String = ""
        Dim selectedCustomerCode As String = ""
        Dim sql As String = ""
        Dim returnMachineResult As Object
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()

                If SearchBy = "SN" Then
                    sql = "
                    SELECT * 
                    FROM TBL_MACHINE_RETURN_NEW 
                    WHERE COM_ID = @companyid AND SERIAL = @serialNo"

                    returnMachineResult = connection.QuerySingleOrDefault(Of MachineReturnNew)(sql, New With {
                        .companyid = globalVariables.selectedCompanyID,
                        .serialNo = Trim(txtSearch.Text)
                    })

                    selectedSerialNo = returnMachineResult.SERIAL
                    selectedPNo = returnMachineResult.P_NO
                    selectedAgreementNo = returnMachineResult.AG_ID
                    selectedCustomerCode = returnMachineResult.CUS_ID

                    If selectedSerialNo = "" Then
                        Exit Function
                    End If

                    txtSerialNo.Text = selectedSerialNo
                    txtAgreementID.Text = selectedAgreementNo
                    txtCustomerID.Text = selectedCustomerCode
                    txtPno.Text = selectedPNo
                Else
                    sql = "
                    SELECT * 
                    FROM TBL_MACHINE_RETURN_NEW 
                    WHERE COM_ID = @companyid AND P_NO = @pno"

                    returnMachineResult = connection.QuerySingleOrDefault(Of MachineReturnNew)(sql, New With {
                        .companyid = globalVariables.selectedCompanyID,
                        .pno = Trim(txtSearch.Text)
                    })

                    selectedSerialNo = returnMachineResult.SERIAL
                    selectedPNo = returnMachineResult.P_NO
                    selectedAgreementNo = returnMachineResult.AG_ID
                    selectedCustomerCode = returnMachineResult.CUS_ID

                    If selectedSerialNo = "" Then
                        Exit Function
                    End If

                    txtSerialNo.Text = selectedSerialNo
                    txtAgreementID.Text = selectedAgreementNo
                    txtCustomerID.Text = selectedCustomerCode
                    txtPno.Text = selectedPNo
                End If

                Dim customerNameSql As String = "
                    SELECT CUS_NAME FROM 
                    MTBL_CUSTOMER_MASTER 
                    WHERE COM_ID = @companyid AND CUS_ID = @customercode"

                txtCustomerName.Text = connection.QuerySingleOrDefault(Of String)(customerNameSql, New With {
                    .companyid = globalVariables.selectedCompanyID,
                    .customercode = Trim(txtCustomerID.Text)
                })

                txtMachinePN.Text = returnMachineResult.MACHINE_PN
                cbSpecialCase.Checked = returnMachineResult.IS_SPECIAL_CASE
                txtSpecialCase.Text = returnMachineResult.SPECIAL_CASE_DESC
                txtMLocation1.Text = returnMachineResult.M_LOC1
                txtMLocation2.Text = returnMachineResult.M_LOC2
                txtMLocation3.Text = returnMachineResult.M_LOC3
                txtContact.Text = returnMachineResult.CONTACT_PERSON
                txtTel.Text = returnMachineResult.CONTACT_NO
                dtpInstallationDate.Value = returnMachineResult.INSTALLATION_DATE
                txtStartMR.Text = returnMachineResult.START_MR
                txtBookValue.Text = returnMachineResult.BOOK_VALUE
                txtTechCode.Text = returnMachineResult.TECH_CODE
                txtRepCode.Text = returnMachineResult.REP_CODE

                Dim machineModelSql As String = "
                    SELECT MACHINE_MODEL 
                    FROM MTBL_MACHINE_MASTER 
                    WHERE MACHINE_ID = @machineid AND COM_ID = @companyid"

                Dim machineModelResult = connection.QuerySingleOrDefault(Of String)(machineModelSql, New With {
                    .machineid = Trim(txtMachinePN.Text),
                    .companyid = globalVariables.selectedCompanyID
                })
                lblMachineName.Text = machineModelResult

                Dim techNameSql As String = "
                    SELECT TECH_NAME FROM 
                    MTBL_TECH_MASTER 
                    WHERE TECH_CODE = @techcode
                    AND COM_ID = @companyid"

                lblTechName.Text = connection.QuerySingleOrDefault(Of String)(techNameSql, New With {
                    .techcode = returnMachineResult.TECH_CODE,
                    .companyid = globalVariables.selectedCompanyID
                })

                techNameSql = "
                    SELECT TECH_NAME FROM 
                    MTBL_TECH_MASTER 
                    WHERE TECH_CODE = @techcode
                    AND COM_ID = @companyid"

                lblRepName.Text = connection.QuerySingleOrDefault(Of String)(techNameSql, New With {
                    .techcode = returnMachineResult.TECH_CODE,
                    .companyid = globalVariables.selectedCompanyID
                })
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub btnDispose_Click(sender As Object, e As EventArgs) Handles btnDispose.Click
        Try
            Using connection As New SqlConnection(connectionString)
                connection.Open()
                Dim sql As String = "
                SELECT * FROM TBL_MACHINE_RETURN_NEW 
                WHERE SERIAL = @serialNo AND COM_ID = @companyID"

                Dim serialNo As String = Trim(txtSerialNo.Text)
                Dim disposeComment As String = Trim(txtComment.Text)
                Dim disposeDate As DateTime = DateTime.Now

                Dim result = connection.QuerySingleOrDefault(Of MachineReturnNew)(sql, New With {
                    .serialNo = serialNo,
                    .companyID = globalVariables.selectedCompanyID
                })

                Dim companyID As String = result.COM_ID
                Dim companyName As String = result.COM_NAME
                Dim returnID As String = result.R_ID
                Dim returnDate As DateTime? = result.R_DATE
                Dim agreementID As String = result.AG_ID
                Dim machinePN As String = result.MACHINE_PN
                Dim pno As String = result.P_NO
                Dim isSpecialCase As Boolean = result.IS_SPECIAL_CASE
                Dim specialCaseDesc As String = result.SPECIAL_CASE_DESC
                Dim mloc1 As String = result.M_LOC1
                Dim mloc2 As String = result.M_LOC2
                Dim mloc3 As String = result.M_LOC3
                Dim mDept As String = result.M_DEPT
                Dim contactPerson As String = result.CONTACT_PERSON
                Dim contactNo As String = result.CONTACT_NO
                Dim installationDate As DateTime? = result.INSTALLATION_DATE
                Dim startMr As String = result.START_MR
                Dim bookValue As Decimal? = result.BOOK_VALUE
                Dim techCode As String = result.TECH_CODE
                Dim repCode As String = result.REP_CODE
                Dim customerID As String = result.CUS_ID
                Dim customerName As String = result.CUS_NAME
                Dim returnComment As String = result.R_COMMENT

                sql = "
                INSERT INTO TBL_MACHINE_DISPOSE
                (COM_ID, COM_NAME, DISPOSE_DATE, DISPOSE_COMMENT, R_ID, R_DATE, SERIAL, AG_ID, MACHINE_PN, P_NO, 
                IS_SPECIAL_CASE, SPECIAL_CASE_DESC, M_LOC1, M_LOC2, M_LOC3, CONTACT_PERSON, CONTACT_NO, 
                INSTALLATION_DATE, START_MR, BOOK_VALUE, TECH_CODE, REP_CODE, CUS_ID, CUS_NAME, R_COMMENT) 
                VALUES 
                (@companyid, @companyName, @disposeDate, @disposeComment, @rid, @rdate, @serial, @agid, @machinepn, @pno, 
                @isspecialcase, @specialcasedesc, @mloc1, @mloc2, @mloc3, @contactperson, @contactno, @installationdate, @startmr, 
                @bookvalue, @techcode, @repcode, @cusid, @cusname, @rcomment)"

                Dim insertResult = connection.Execute(sql, New With {
                    .companyid = companyID,
                    .companyName = companyName,
                    .disposeDate = disposeDate,
                    .disposeComment = disposeComment,
                    .rid = returnID,
                    .rdate = returnDate,
                    .serial = serialNo,
                    .agid = agreementID,
                    .machinepn = machinePN,
                    .pno = pno,
                    .isspecialcase = isSpecialCase,
                    .specialcasedesc = specialCaseDesc,
                    .mloc1 = mloc1,
                    .mloc2 = mloc2,
                    .mloc3 = mloc3,
                    .contactperson = contactPerson,
                    .contactno = contactNo,
                    .installationdate = installationDate,
                    .startmr = startMr,
                    .bookvalue = bookValue,
                    .techcode = techCode,
                    .repcode = repCode,
                    .cusid = customerID,
                    .cusname = customerName,
                    .rcomment = returnComment
                })

                Dim deleteReturnMachineQuery As String = "
                DELETE FROM TBL_MACHINE_RETURN_NEW 
                WHERE SERIAL = @serialNo"

                Dim deletedResult = connection.Execute(deleteReturnMachineQuery, New With {.serialNo = serialNo})

                If deletedResult > 0 Then
                    MessageBox.Show("Machine Successfully Disposed.", "Information", MessageBoxButton.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

Public Class MachineReturnNew
    Public Property COM_ID As String
    Public Property COM_NAME As String
    Public Property R_ID As String
    Public Property R_DATE As DateTime?
    Public Property SERIAL As String
    Public Property AG_ID As String
    Public Property MACHINE_PN As String
    Public Property P_NO As String
    Public Property IS_SPECIAL_CASE As String
    Public Property SPECIAL_CASE_DESC As String
    Public Property M_LOC1 As String
    Public Property M_LOC2 As String
    Public Property M_LOC3 As String
    Public Property M_DEPT As String
    Public Property CONTACT_PERSON As String
    Public Property CONTACT_NO As String
    Public Property INSTALLATION_DATE As DateTime?
    Public Property START_MR As String
    Public Property BOOK_VALUE As Decimal?
    Public Property TECH_CODE As String
    Public Property REP_CODE As String
    Public Property CUS_ID As String
    Public Property CUS_NAME As String
    Public Property R_COMMENT As String

End Class