Imports System.IO
Imports System.Drawing
'Imports IWshRuntimeLibrary
Imports System.Data.SqlClient

Public Class IconsAndPaths
    Public IconsPathsDi As New Dictionary(Of String, String())

    Public Function GetIconsAndPaths() As Dictionary(Of String, String())

        Dim strSQL As String
        Try

            strSQL = "SELECT MENUITEM_MENUTAG,MENUITEM_MENUNAME FROM MENUITEM INNER JOIN TBLU_USERDET ON MENUITEM.MENUITEM_MENUTAG = TBLU_USERDET.USERDET_MENUTAG WHERE TBLU_USERDET.USERDET_USERCODE = '" & userSession & "' AND  MENUITEM_MENULEVEL >0 AND TBLU_USERDET.COM_ID = '" & globalVariables.selectedCompanyID & "' ORDER BY MENUITEM_MENUTAG ASC"
            dbConnections.sqlAdapter = New SqlDataAdapter(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlAdapter.Fill(dbConnections.dset, "tmp_treeMenu")                                                              '//02  myReports treeview parent

            For childTreeElements As Integer = 0 To dset.Tables("tmp_treeMenu").Rows.Count - 1

                Dim iconimagepath As String = globalVariables.applicationPath + "MenuIcons\" + dset.Tables("tmp_treeMenu").Rows(childTreeElements).Item(0) + ".png"
                If Not IO.File.Exists(iconimagepath) Then
                    iconimagepath = globalVariables.applicationPath + "swlogoicon.ico"
                Else
                    iconimagepath = iconimagepath
                End If

                AddToDictionary(dset.Tables("tmp_treeMenu").Rows(childTreeElements).Item(0) + ":" + dset.Tables("tmp_treeMenu").Rows(childTreeElements).Item(1), iconimagepath, "")
                iconimagepath = ""
            Next

        Catch ex As SqlException
            'MsgBox(ex.Message)
        Finally
            dbConnections.dset.Dispose()
        End Try

        Return IconsPathsDi
    End Function


    Public Function GetIconsAndPathsFavorit() As Dictionary(Of String, String())

        Dim strSQL As String
        Try

            strSQL = "SELECT  [MENUITEM_MENUTAG],[MENUITEM_MENUNAME] FROM [" & selectedDatabaseName & "].[dbo].[MENUITEM_FAVORITS] WHERE [USERHED_USERCODE] = '" & userSession & "' AND COM_ID = '" & globalVariables.selectedCompanyID & "'  ORDER BY MENUITEM_MENUTAG ASC"
            dbConnections.sqlAdapter = New SqlDataAdapter(strSQL, dbConnections.sqlConnection)
            dbConnections.sqlAdapter.Fill(dbConnections.dset, "tmp_treeMenuFav")                                                              '//02  myReports treeview parent

            For childTreeElements As Integer = 0 To dset.Tables("tmp_treeMenuFav").Rows.Count - 1

                Dim iconimagepath As String = globalVariables.applicationPath + "MenuIcons\" + dset.Tables("tmp_treeMenuFav").Rows(childTreeElements).Item(0) + ".png"
                If Not IO.File.Exists(iconimagepath) Then
                    iconimagepath = globalVariables.applicationPath + "swlogoicon.ico"
                Else
                    iconimagepath = iconimagepath
                End If

                AddToDictionary(dset.Tables("tmp_treeMenuFav").Rows(childTreeElements).Item(0) + ":" + dset.Tables("tmp_treeMenuFav").Rows(childTreeElements).Item(1), iconimagepath, "")
                iconimagepath = ""
            Next

        Catch ex As SqlException
            'MsgBox(ex.Message)
        Finally
            dbConnections.dset.Dispose()
        End Try

        Return IconsPathsDi
    End Function


    Private Sub AddToDictionary(ByVal displayName As String, ByVal tileIconPath As String, ByVal exePath As String)
        If Not IconsPathsDi.ContainsKey(displayName) Then
            IconsPathsDi.Add(displayName, New String() {tileIconPath, exePath})
        End If
    End Sub



    Private Sub CreateIconsDirectory()
        Dim dir As String = Environment.CurrentDirectory & "\WPF Metro Icons"
        If (Directory.Exists(dir)) Then
            Dim di As New DirectoryInfo(dir)
            For Each fi As FileInfo In di.GetFiles
                fi.Delete()
            Next
        Else
            Directory.CreateDirectory(dir)
        End If
    End Sub
End Class
