Imports System.Net
Imports System.Net.Mail
Imports System.Text

Public Class FuncMail

    ''' <summary>
    ''' 發送通知信件
    ''' </summary>
    ''' <param name="toEmail">收件者信箱</param>
    ''' <param name="toBody">信件內容</param>
    ''' <returns>是否發送成功</returns>
    ''' <remarks>發送通知信件</remarks>
    Public Shared Function SendMail(ByVal toEmail As String, ByVal toBody As String) As Boolean

        Dim Mail As New MailMessage()

        '寄件者設定
        Dim from As New MailAddress(Mail_SenderEmail, Mail_SenderName, Encoding.UTF8)

        '信件內容
        With Mail
            .From = from

            '郵件標題
            .SubjectEncoding = Encoding.UTF8
            .Subject = Mail_Sender_Subject

            '郵件內容
            .BodyEncoding = Encoding.UTF8
            .Body = toBody

            .IsBodyHtml = True
            .Priority = MailPriority.High
        End With

        '將字串裡的;分為多個收件人
        For Each MA As String In toEmail.Split(";")
            If MA <> "" Then
                Mail.To.Add(New Net.Mail.MailAddress(MA, "收件人"))
            End If
        Next

        'SMTP伺服器設定
        Dim MailClient As New SmtpClient

        With MailClient
            '.Host = "smtp.gmail.com"
            '.Port = 587
            '.Credentials = New NetworkCredential("", "")
            '.EnableSsl = True

            .Host = "msa.hinet.net"
            .Port = 25
            .EnableSsl = False
        End With

        Try
            'Send Mail
            'MailClient.SendAsync(Mail, Mail)
            'AddHandler MailClient.SendCompleted, AddressOf MailClient_SendCompleted
            MailClient.Send(Mail)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function
End Class
