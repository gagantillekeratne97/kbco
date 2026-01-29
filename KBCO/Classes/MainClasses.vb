Public Class CustomerLocationVM
    Public Property customerName As String
    Public Property customerAddress01 As String
    Public Property customerAddress02 As String
End Class

Public Class CustomerAgreementVM
    Public Property BillingPeriod As String
    Public Property SlabMethod As String
    Public Property InvoiceStatus As String
    Public Property AgRentalPrice As String
    Public Property RepCode As String
End Class

Public Class MachineLocationVM
    Public Property MachineLocation1 As String
    Public Property MachineLocation2 As String
    Public Property MachineLocation3 As String
End Class

Public Class AgreementInformationVM
    Public Property AG_ID As String
    Public Property AG_NAME As String
End Class

Public Class CustomerInformation
    Public Property CUS_NAME As String
    Public Property VAT_TYPE_ID As String
    Public Property VAT_DESC As String
    Public Property IS_NBT As Boolean
    Public Property IS_VAT As Boolean
End Class

Public Class MeterReadingData
    Public Property SerialNo As String
    Public Property PNo As String
    Public Property MakeModel As String
    Public Property Location As String
    Public Property StartMR As Integer
    Public Property EndMR As Integer
    Public Property Copies As Integer
    Public Property Waistage As Integer
End Class

Public Class BWCommitmentData
    Public Property Range1 As Integer
    Public Property Range2 As Integer
    Public Property Rate As Decimal
End Class

Public Class AdjustmentData
    Public Property ADJUSTMENT As Decimal?
End Class

Public Class AgreementData
    Public Property CUS_TYPE As String
    Public Property BILLING_METHOD As String
    Public Property SLAB_METHOD As String
    Public Property BILLING_PERIOD As String
    Public Property AG_PERIOD_START As DateTime?
    Public Property AG_PERIOD_END As DateTime?
    Public Property INV_STATUS As String
    Public Property MACHINE_TYPE As String
    Public Property AG_RENTAL_PRICE As Decimal?
    Public Property REP_CODE As String
End Class


'Meter Reading Details and Master information 
Public Class MachineReadingDetailsInformation
    Public Property START_MR As String
    Public Property END_MR As String
    Public Property COPIES As String
    Public Property WAISTAGE As String
End Class

Public Class CommitmentVM
    Public Property Range1 As String
    Public Property Range2 As String
    Public Property BWRate As String
    Public Property CopyBreakUp As String
End Class

Public Class MachineTransactionsVM
    Public Property MACHINE_MAKE As String
    Public Property MACHINE_MODEL As String
    Public Property SERIAL As String
    Public Property P_NO As String
    Public Property M_DEPT As String
    Public Property LAST_MR As String
End Class

Public Class MachineData
    Public Property MACHINE_MODEL As String
    Public Property SERIAL As String
    Public Property P_NO As String
    Public Property M_DEPT As String
    Public Property MACHINE_PN As String
    Public Property L_READING As Integer
    Public Property START_MR As Integer?
    Public Property SMR_ADUJESTED_STATUS As String
End Class

Public Class ExistingReading
    Public Property SERIAL_NO As String
    Public Property START_MR As Integer?
    Public Property END_MR As Integer?
    Public Property COPIES As Integer?
    Public Property WAISTAGE As Integer?
End Class