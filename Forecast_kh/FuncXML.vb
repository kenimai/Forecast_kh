Imports System.IO
Imports System.Xml

Public Class FuncXML

    ''' <summary>
    ''' 取得測站的PSI或指標
    ''' </summary>
    ''' <param name="SiteName">測站名稱</param>
    ''' <param name="DataKind">取得指標</param>
    ''' <returns>測站的PSI或指標</returns>
    ''' <remarks>取得測站的PSI或指標</remarks>
    Public Shared Function GetPsiValue(ByVal SiteName As String, ByVal DataKind As String) As String

        Dim Result As String = ""

        '路徑
        Dim TargetURL As String = "http://taqm.epa.gov.tw/taqm/DataService.aspx?src=PSI_SITE"

        Dim XMLDOC As New XmlDocument
        XMLDOC.Load(TargetURL)

        If XMLDOC.HasChildNodes Then

            '取得文件根節點
            Dim DocRoot As XmlElement = XMLDOC.DocumentElement

            Dim Item As XmlElement
            Dim ItemEpaSiteName As XmlElement

            Dim Chk_psi As Integer = 0

            '取得所有item節點
            For Each Item In DocRoot.SelectNodes("/rss/channel/item")
                If Item.HasChildNodes Then
                    For Each ItemEpaSiteName In Item.ChildNodes
                        '判斷是否為此測站
                        If ItemEpaSiteName.Name = "epa:siteName" Then
                            If ItemEpaSiteName.InnerText = SiteName Then
                                Chk_psi = 1
                            End If
                        End If

                        '取得psi值及指標汙染物
                        If Chk_psi = 1 Then

                            If DataKind = "psi" Then
                                DataKind = "epa:psi"
                            End If

                            If DataKind = "pollution" Then
                                DataKind = "epa:pollution"
                            End If

                            '取得值
                            If ItemEpaSiteName.Name = DataKind Then
                                Result = ItemEpaSiteName.InnerText
                                Exit For
                            End If
                        End If
                    Next

                    If Chk_psi = 1 Then
                        Exit For
                    End If
                End If
            Next
        End If

        Return Result

    End Function

End Class
