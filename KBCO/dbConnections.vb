Imports System.Data.SqlClient

Module dbConnections
    Public sqlConnection As New SqlConnection
    Public sqlCommand As New SqlCommand
    Public sqlAdapter As New SqlDataAdapter
    Public dset As DataSet
    Public dReader As SqlDataReader
    Public sqlTransaction As SqlTransaction
    '// servista Connections
    Public ServistaConnectionString = "Data Source=APPSERVER01\SQLEXPRESS;Initial Catalog=gocl_techdatabase;User ID=goclsql; Password=gocsql248"
    Public ServistaCommand As New SqlCommand
    Public ServistaSqlConnection As New SqlConnection
    Public ServistaDreader As SqlDataReader
    Public ServistaDset As DataSet
    Public ServistasqlAdeptor As New SqlDataAdapter
    '// P2P system connection
    Public P2PConnectionString = "Data Source=APPSERVER01\SQLEXPRESS;Initial Catalog=goclp2p;User ID=goclsql; Password=gocsql248"
    Public P2PCommand As New SqlCommand
    Public P2PConnection As New SqlConnection
    Public P2PDreader As SqlDataReader
    Public P2PDset As DataSet
    Public P2PsqlAdeptor As New SqlDataAdapter

    '//KBCO Cloud Server
    'Public cloudConnectionString As String = "Data Source=sql5110.site4now.net;Initial Catalog=db_a67cc4_fintekkbco;User ID=db_a67cc4_fintekkbco_admin;Password=Ssg789.541351;"
    'Public cloudConnectionString As String = "Data Source=sql1003.site4now.net;Initial Catalog=db_ab8b61_kbco;User ID=db_ab8b61_kbco_admin;Password=Ssg789.541351;"
    Public cloudConnectionString As String = "Data Source=Gagan_GD\SQLEXPRESS;Initial Catalog=kbco_db;User ID=gestetner;Password=gocl248;"

    '//KBCO SERVER API ENDPOINT    
    Public kbcoAPIEndPoint As String = "http://rmsgestetner-001-site3.jtempurl.com"
    'Public kbcoAPIEndPoint As String = "http://localhost:44390/"
    'Public kbcoAPIEndPoint As String = "http://rmsgestetner-001-site5.jtempurl.com/"

    '// Fintek system connection
    Public FintekConnectionString = "Data Source=APPSERVER01\SQLEXPRESS;Initial Catalog=KBFintek;User ID=goclsql; Password=gocsql248"
    'Public FintekConnectionString = "Data Source=IT-3;Initial Catalog=KBFintek;Persist Security Info=True;User ID=Gestetner ;Password=gocl248"
    Public FintekCommand As New SqlCommand
    Public FintekConnection As New SqlConnection
    Public FintekDreader As SqlDataReader
    Public FintekDset As DataSet
    Public FinteksqlAdeptor As New SqlDataAdapter


    Public Function connectToDB() As Boolean
        connectToDB = False
        Try
            sqlConnection.Open()
            connectToDB = True
        Catch ex As Exception
            connectToDB = False
            MessageBox.Show(ex.Message, "Error connecting to the database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return connectToDB
    End Function

    Public Sub DisconnectFromDB()
        Try
            sqlConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error occurred while disconnecting from the database", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module
