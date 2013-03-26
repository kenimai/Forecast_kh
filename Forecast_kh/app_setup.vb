Module app_setup
    '[程式基本設定] ////////////////////////////////////
    Public App_Title As String = "環保局空氣品質預報自動通報"

    '路徑設定
    Public Path_App As String = "C:\KH_forecast\"

    'Log檔案名稱
    Public FileName_log As String = "forecast_" & Now.Date.ToString("yyyy_MM_dd") & ".log"

    '[郵件設定] ////////////////////////////////////

    '收件人清單檔案名稱
    Public FileName_MailList As String = Path_App & "email_list.ini"
    '信件內容檔案(微粒)
    Public FileName_MailBody_particle As String = Path_App & "email_body_particle.ini"
    '信件內容檔案(臭氧)
    Public FileName_MailBody_O3 As String = Path_App & "email_body_o3.ini"

    '寄件者信箱 (一定要有信箱)
    Public Mail_SenderEmail As String = "username@email"
    '寄件者名稱
    Public Mail_SenderName As String = "環保局"
    '信件標題
    Public Mail_Sender_Subject As String = "環保局空氣品質預報自動通報"

    '[簡訊設定] ////////////////////////////////////

    '簡訊檔案(微粒)
    Public FileName_SMS_particle As String = Path_App & "sms_particle.ini"

    '簡訊檔案(臭氧)
    Public FileName_SMS_O3 As String = Path_App & "sms_o3.ini"

    '[執行設定] ////////////////////////////////////

    '地區
    Public ValArea As String = "高屏"

    '標準值
    Public ValPsiLimit As Integer = 85
End Module
