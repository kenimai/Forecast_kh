Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Reflection

Public Class WebPageGenFunc
    Private Shared rd As New Random(Now.Second)
    Private Shared sec As Integer
    Private Shared lastsec As Integer = -1

    Public Shared Function sleepTime(Optional ByVal minimatime As Integer = 2600, Optional ByVal rangeSecond As Integer = 5) As Integer
        sec = (rd.Next Mod rangeSecond) * 1000 + minimatime
        While sec = lastsec
            sec = (rd.Next Mod rangeSecond) * 1000 + minimatime
        End While
        lastsec = sec
        System.Threading.Thread.Sleep(sec)
        Return sec
    End Function

    Private Shared Sub SetAllowUnsafeHeaderParsing20()
        Dim a As New System.Net.Configuration.SettingsSection
        Dim aNetAssembly As System.Reflection.Assembly = Assembly.GetAssembly(a.GetType)
        Dim aSettingsType As Type = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal")
        Dim args As Object() = Nothing
        Dim anInstance As Object = aSettingsType.InvokeMember("Section", BindingFlags.Static Or BindingFlags.GetProperty Or BindingFlags.NonPublic, Nothing, Nothing, args)
        Dim aUseUnsafeHeaderParsing As FieldInfo = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic Or BindingFlags.Instance)
        aUseUnsafeHeaderParsing.SetValue(anInstance, True)
    End Sub


    ''' <summary>
    ''' 使用Get方法取得網頁內容
    ''' </summary>
    ''' <param name="url">網址</param>
    ''' <param name="noCashe">不使用快取</param>
    ''' <returns>網頁的HTML</returns>
    ''' <remarks>使用Get方法取得網頁內容</remarks>
    Public Shared Function getHTMLGet(ByVal url As String, Optional ByVal noCashe As Boolean = False) As String
        getHTMLGet = Nothing
        SetAllowUnsafeHeaderParsing20()
        Dim wRs As HttpWebResponse
        Dim wRq As HttpWebRequest
        ' Create the request using the WebRequestFactory.

        wRq = CType(WebRequest.Create(url), HttpWebRequest)
        With wRq
            .UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
            .ContentType = "application/x-www-form-urlencoded"
            .Headers.Add("Accept-Language", "zh-tw")
            .Method = "GET"
            .Timeout = 10000
            If noCashe Then
                Dim policy As New Cache.HttpRequestCachePolicy(Cache.HttpRequestCacheLevel.NoCacheNoStore)
                .CachePolicy = policy
                .Headers.Add("Cache-Control", "no-cache")
            End If
        End With

        Try
            ' Return the response stream.
            wRs = CType(wRq.GetResponse(), HttpWebResponse)
            Dim streamResponse As Stream = wRs.GetResponseStream()
            Dim streamRead As New StreamReader(streamResponse)
            Dim responseString As String = streamRead.ReadToEnd()
            getHTMLGet = responseString
            ' Close Stream object.
            streamResponse.Close()
            streamRead.Close()
            ' Release the HttpWebResponse.
            wRs.Close()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 使用Post方法取得網頁內容
    ''' </summary>
    ''' <param name="url">網址</param>
    ''' <param name="postdata">傳遞參數</param>
    ''' <param name="noCashe">不使用快取</param>
    ''' <returns>網頁的HTML</returns>
    ''' <remarks>使用Post方法取得網頁內容</remarks>
    ''' 
    Public Shared Function getHTMLPost(ByVal url As String, Optional ByVal postdata As String = Nothing, Optional ByVal noCashe As Boolean = False) As String
        getHTMLPost = Nothing
        SetAllowUnsafeHeaderParsing20()
        Dim wRs As HttpWebResponse
        Dim wRq As HttpWebRequest
        ' Create the request using the WebRequestFactory.
        wRq = CType(WebRequest.Create(url), HttpWebRequest)
        With wRq
            .UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
            .Headers.Add("Accept-Language", "zh-tw")
            .Method = "POST"
            .Timeout = 10000
            .KeepAlive = False
            If noCashe Then
                Dim policy As New Cache.HttpRequestCachePolicy(Cache.HttpRequestCacheLevel.NoCacheNoStore)
                .CachePolicy = policy
                .Headers.Add("Cache-Control", "no-cache")
            End If
            If Not postdata Is Nothing Then
                .Timeout = 60000
                Dim encoding As New System.Text.ASCIIEncoding()
                Dim byte1 As Byte() = encoding.GetBytes(postdata)
                .ContentType = "application/x-www-form-urlencoded"
                .ContentLength = byte1.Length
                .GetRequestStream().Write(byte1, 0, byte1.Length)
            End If
        End With
        wRq.GetRequestStream().Close()
        Try
            ' Return the response stream.
            wRs = CType(wRq.GetResponse(), HttpWebResponse)
            Dim streamResponse As Stream = wRs.GetResponseStream()
            Dim streamRead As New StreamReader(streamResponse)
            Dim responseString As String = streamRead.ReadToEnd()
            getHTMLPost = responseString
            ' Close Stream object.
            streamResponse.Close()
            streamRead.Close()
            ' Release the HttpWebResponse.
            wRs.Close()

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function
    ''' <summary>
    ''' 使用WebClient取得網頁內容
    ''' </summary>
    ''' <param name="url">網址</param>
    ''' <param name="postdata">傳遞的參數</param>
    ''' <param name="method">使用的方法，預設為POST</param>
    ''' <param name="noCashe">不使用快取</param>
    ''' <returns>網頁的HTML</returns>
    ''' <remarks>使用WebClient取得網頁內容</remarks>
    Public Shared Function getHTMLWebClient(ByVal url As String, ByRef postdata As Specialized.NameValueCollection, Optional ByVal method As String = "POST", Optional ByVal noCashe As Boolean = False) As String
        getHTMLWebClient = Nothing
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim myWebClient As New WebClient()
            If noCashe Then
                Dim policy As New Cache.HttpRequestCachePolicy(Cache.HttpRequestCacheLevel.NoCacheNoStore)
                myWebClient.CachePolicy = policy
                myWebClient.Headers.Add("Cache-Control", "no-cache")
            Else
                Dim rheaders As WebHeaderCollection = myWebClient.ResponseHeaders
                If Not rheaders Is Nothing Then
                    Dim header As String = rheaders("Set-Cookie")
                    If Not header Is Nothing Then
                        myWebClient.Headers.Add("Cookie", header)
                    End If
                End If
            End If
            myWebClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)")
            myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
            myWebClient.Headers.Add("Accept-Language", "zh-tw")
            If postdata Is Nothing Then postdata = New Specialized.NameValueCollection
            Dim responseArray As Byte() = myWebClient.UploadValues(url, method, postdata)
            Dim encoding As New System.Text.UTF8Encoding
            getHTMLWebClient = encoding.GetString(responseArray)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Function

    ''' <summary>
    ''' 取得網頁的Body區段
    ''' </summary>
    ''' <param name="url">網址</param>
    ''' <param name="postdata">傳遞的參數，若有值會使用getHTMLPost取得</param>
    ''' <param name="postValue">傳遞的參數，若有值會使用getHTMLWebClient取得</param>
    ''' <param name="noCashe">不使用快取</param>
    ''' <returns>網頁的HTML</returns>
    ''' <remarks>取得網頁的Body區段</remarks>
    Public Shared Function getHTMLBody(ByVal url As String, Optional ByVal postdata As String = Nothing, Optional ByVal postValue As Specialized.NameValueCollection = Nothing, Optional ByVal noCashe As Boolean = False, Optional ByVal useWebClient As Boolean = False) As String
        getHTMLBody = Nothing
        Dim html As String
        If Not postValue Is Nothing Then
            html = getHTMLWebClient(url, postValue, , noCashe)
        ElseIf Not postdata Is Nothing Then
            html = getHTMLPost(url, postdata, noCashe)
        Else
            If useWebClient Then
                html = getHTMLWebClient(url, Nothing, "GET", noCashe)
            Else
                html = getHTMLGet(url, noCashe)
            End If

        End If
        If Not html Is Nothing Then
            getHTMLTagContain(html, "body", getHTMLBody)
        End If
    End Function
    ''' <summary>
    ''' 取得HTML中第一個符合的標籤內容的指標，並將取得的標籤內容寫入參數中
    ''' </summary>
    ''' <param name="html">HTML</param>
    ''' <param name="tag">標籤</param>
    ''' <param name="contain">取得的標籤內容</param>
    ''' <param name="indexEnd">此標籤結束於HTML的指標</param>
    ''' <returns>標籤內容的指標</returns>
    ''' <remarks>取得HTML中第一個符合的標籤內容的指標，並將取得的標籤內容寫入參數中</remarks>
    Public Shared Function getHTMLTagContain(ByVal html As String, ByVal tag As String, Optional ByRef contain As String = Nothing, Optional ByRef indexEnd As Integer = -1) As Integer
        tag = tag.ToLower
        contain = Nothing
        indexEnd = -1
        Dim indexBegin As Integer = -1
        Dim indexbBegin As Integer = -1
        If Not html Is Nothing Then
            indexBegin = html.ToLower.IndexOf("<" & tag)
            If indexBegin > -1 Then
                indexbBegin = html.IndexOf(">", indexBegin)
                If indexbBegin > -1 Then
                    indexbBegin += 1
                End If
                Dim findTag As Boolean = False
                indexEnd = indexbBegin
                Dim lastStart As Integer = indexbBegin
                Dim stopLimit2 As Integer = 9999
                Do
                    indexEnd = html.ToLower.IndexOf("</" & tag, indexEnd)
                    If indexEnd > -1 Then
                        If html.Substring(lastStart, indexEnd - lastStart).IndexOf("<" & tag) > -1 Then
                            lastStart = indexEnd
                            indexEnd = indexEnd + ("</" & tag).Length
                            findTag = True
                        Else
                            findTag = False
                        End If
                    Else
                        findTag = False
                    End If
                    stopLimit2 -= 1
                Loop While findTag And stopLimit2 > 0

                If indexEnd > -1 Then
                    contain = html.Substring(indexbBegin, indexEnd - indexbBegin)

                    indexEnd = html.IndexOf(">", indexEnd)
                    If indexEnd > -1 Then
                        indexEnd += 1
                    End If
                End If

            End If
        End If
        Return indexbBegin
    End Function
    ''' <summary>
    ''' 取得HTML中第一個符合的標籤屬性的指標，並將取得的標籤內容與該屬性內容寫入參數中
    ''' </summary>
    ''' <param name="html">HTML</param>
    ''' <param name="tag">標籤</param>
    ''' <param name="attName">屬性名稱</param>
    ''' <param name="att">取得的屬性內容</param>
    ''' <param name="contain">取得的標籤內容</param>
    ''' <param name="indexEnd">此標籤結束於HTML的指標</param>
    ''' <returns></returns>
    ''' <remarks>取得HTML中第一個符合的標籤屬性的指標，並將取得的標籤內容與該屬性內容寫入參數中</remarks>
    Public Shared Function getHTMLTagAtt(ByVal html As String, ByVal tag As String, ByVal attName As String, Optional ByRef att As String = Nothing, Optional ByRef contain As String = Nothing, Optional ByRef indexEnd As Integer = -1) As Integer
        att = Nothing
        contain = Nothing
        tag = tag.ToLower
        attName = attName.ToLower
        indexEnd = -1
        Dim indexBegin As Integer = -1
        Dim indexaBegin As Integer = -1
        Dim indexbEnd As Integer = -1
        Dim findTag As Boolean = False
        Dim sign As String

        If Not html Is Nothing Then
            indexBegin = 0
            Dim stopLimit1 As Integer = 9999
            Do
                indexbEnd = -1
                indexBegin = html.ToLower.IndexOf("<" & tag, indexBegin)
                If indexBegin > -1 Then
                    indexbEnd = html.IndexOf(">", indexBegin)
                    If indexbEnd > -1 Then
                        indexaBegin = html.Substring(0, indexbEnd).Replace("""", "'").ToLower.IndexOf(attName & "='", indexBegin)
                        If indexaBegin > -1 Then
                            sign = html.Substring(indexaBegin + (attName & "=").Length, 1)
                            indexaBegin += (attName & "='").Length
                            Dim indexaEnd As Integer = html.Substring(0, indexbEnd).IndexOf(sign, indexaBegin)
                            If indexaEnd > -1 Then
                                att = html.Substring(indexaBegin, indexaEnd - indexaBegin)
                            End If
                        Else
                            indexBegin = indexbEnd + 1
                            Continue Do
                        End If
                        indexbEnd += 1
                    End If

                    findTag = False
                    indexEnd = indexbEnd
                    Dim lastStart As Integer = indexbEnd

                    Dim stopLimit2 As Integer = 9999
                    Do
                        indexEnd = html.ToLower.IndexOf("</" & tag, indexEnd)
                        If indexEnd > -1 Then
                            If html.Substring(lastStart, indexEnd - lastStart).IndexOf("<" & tag) > -1 Then
                                lastStart = indexEnd
                                indexEnd = indexEnd + ("</" & tag).Length
                                findTag = True
                            Else
                                findTag = False
                            End If
                        Else
                            findTag = False
                        End If
                        stopLimit2 -= 1
                    Loop While findTag And stopLimit2 > 0

                    If indexEnd > -1 Then
                        contain = html.Substring(indexbEnd, indexEnd - indexbEnd)
                        indexEnd = html.IndexOf(">", indexEnd)
                        If indexEnd > -1 Then
                            indexEnd += 1
                        End If
                    End If
                Else
                    Exit Do
                End If
                stopLimit1 -= 1
            Loop While att Is Nothing And stopLimit1 > 0
        End If
        Return indexbEnd
    End Function
    ''' <summary>
    ''' Url參數值編碼
    ''' </summary>
    ''' <param name="value">參數值</param>
    ''' <returns>編碼結果</returns>
    ''' <remarks>Url參數值編碼</remarks>
    Public Shared Function getEncodeStr(ByVal value As String)
        Return Web.HttpUtility.UrlEncode(value)
    End Function
    ''' <summary>
    ''' Url參數值解碼
    ''' </summary>
    ''' <param name="value">參數值</param>
    ''' <returns>解碼結果</returns>
    ''' <remarks>Url參數值解碼</remarks>
    Public Shared Function getDecodeStr(ByVal value As String)
        Return Web.HttpUtility.UrlDecode(value)
    End Function


End Class
