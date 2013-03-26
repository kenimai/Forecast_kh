Imports System.Web
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Reflection

Public Class FuncSMS

    ''' <summary>
    ''' 送出簡訊
    ''' </summary>
    ''' <param name="SmsData">訊息內容</param>
    ''' <returns>是否成功</returns>
    ''' <remarks>使用網頁介面送出簡訊</remarks>
    Public Shared Function sendSMS(ByVal SmsData As String) As Boolean

        sendSMS = Nothing

        Dim wRs As HttpWebResponse
        Dim wRq As HttpWebRequest

        Dim url As String

        url = "http://sms.corenet.com.tw:8080/SmSendPost.asp?username=username&password=password&encoding=UTF8"

        wRq = CType(WebRequest.Create(url), HttpWebRequest)

        With wRq
            .UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
            .Headers.Add("Accept-Language", "zh-tw")
            '.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
            .Method = "POST"
            .Timeout = 60000 '逾時時間 : 1分鐘
            .KeepAlive = False

            If SmsData <> "" Then
                '根據介面不同選擇不同編碼
                Dim encoding As New System.Text.UTF8Encoding()
                '送出簡訊內容
                Dim byte1 As Byte() = encoding.GetBytes(SmsData)
                .ContentType = "application/x-www-form-urlencoded"
                .ContentLength = byte1.Length
                .GetRequestStream().Write(byte1, 0, byte1.Length)
            End If
        End With

        wRq.GetRequestStream().Close()

        Try
            Dim responseString As String = ""

            '簡訊內容不為空值才送出
            If SmsData <> "" Then
                wRs = CType(wRq.GetResponse(), HttpWebResponse)

                Dim streamResponse As Stream = wRs.GetResponseStream()
                Dim streamRead As New StreamReader(streamResponse)

                responseString = streamRead.ReadToEnd()

                ' Close Stream object.
                streamResponse.Close()
                streamRead.Close()

                streamResponse.Dispose()
                streamRead.Dispose()

                ' Release the HttpWebResponse.
                wRs.Close()
            End If

            If responseString = "" Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
