Public Class InvoiceRequestModel
    Public Property CompanyID As String
    Public Property AgreementID As String
    Public Property CustomerID As String
    Public Property PeriodStart As DateTime
    Public Property PeriodEnd As DateTime
    Public Property InvoiceNo As String
    Public Property InvoiceDate As DateTime

    Public Property Address1 As String
    Public Property Address2 As String
    Public Property Address3 As String

    Public Property IsPrinted As Boolean

    Public Property BillingMethod As String
    Public Property InvoiceStatus As String
    Public Property VATType As String

    Public Property IsNBT As Boolean
    Public Property IsVAT As Boolean

    Public Property Rental As String
    Public Property Adjustment As String
    Public Property InvoiceValue As String

    Public Property VATPercent As Double
    Public Property NBT2Percent As Double

    Public Property RepCode As String

    Public Property UserSession As String
    Public Property UserName As String

    Public Property MeterReadings As List(Of MeterReading)
    Public Property BwCommitments As List(Of BwCommitment)
End Class

Public Class MeterReading
    Public Property SerialNo As String
    Public Property MakeModel As String
    Public Property Location As String

    Public Property StartReading As Double
    Public Property EndReading As Double

    Public Property Copies As Integer
    Public Property Wastage As Nullable(Of Double)

    Public Property ProductNo As String
End Class

Public Class BwCommitment
    Public Property Range1 As Integer
    Public Property Range2 As Integer
    Public Property Rate As Double
    Public Property CopyBreakup As String
End Class
