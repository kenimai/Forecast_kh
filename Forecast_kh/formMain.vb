Public Class formMain

    Public TargetURL As String '目標網頁
    Public ValDate As String '日期
    Public ValPsi As String 'PSI值
    Public ValPsiLow As String
    Public ValPsiHigh As String

    '指標汙染物
    Public ValPsiType As String

    '檢查碼
    Public ErrorCode As Integer = 0

    '計時器
    Dim myTimer As New Windows.Forms.Timer

    '現在時間
    Dim TimeNow As Date

    '檔案讀取用
    Dim FS As New FuncFile

    ''' <summary>
    ''' 加入message
    ''' </summary>
    ''' <param name="MsgTxt">文字內容</param>
    ''' <remarks></remarks>
    Private Sub ListBoxAdd(ByVal MsgTxt As String)
        If LB_Runlog.Items.Count < 10 Then
            LB_Runlog.Items.Add(MsgTxt)
        Else
            LB_Runlog.Items.Remove(LB_Runlog.Items(0).ToString)
            LB_Runlog.Items.Add(MsgTxt)
        End If
    End Sub

    ''' <summary>
    ''' 取得最新PSI值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>按鈕取得最新PSI值</remarks>
    Private Sub BtnGetPsi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGetPsi.Click
        If PsiDataProcess(ValArea) Then
            If ValPsiType = "" Then
                ListBoxAdd(ValArea & "地區 " & ValDate & " PSI值為 [ " & ValPsi & " ]")
            Else
                ListBoxAdd(ValArea & "地區 " & ValDate & " PSI值為 [ " & ValPsi & " ] [ " & ValPsiType & " ]")
            End If
        Else
            ListBoxAdd("資料來源有問題 code:01522")
        End If
    End Sub

    ''' <summary>
    ''' 取得網頁psi值
    ''' </summary>
    ''' <param name="Area">地區</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PsiDataProcess(ByVal Area As String) As Boolean

        TargetURL = "http://taqm.epa.gov.tw/taqm/zh-tw/PsiForecastMap.aspx"

        Dim ParaArea As String
        Dim ParaType As String

        ValDate = ""
        ValPsi = ""
        ValPsiLow = ""
        ValPsiHigh = ""

        Select Case Area
            Case "北部"
                ParaArea = "<span id=""ctl19_labPsi1_1"">"
                ParaType = "<span id=""ctl19_labParam1_1"">"
            Case "竹苗"
                ParaArea = "<span id=""ctl19_labPsi1_2"">"
                ParaType = "<span id=""ctl19_labParam1_2"">"
            Case "中部"
                ParaArea = "<span id=""ctl19_labPsi1_3"">"
                ParaType = "<span id=""ctl19_labParam1_3"">"
            Case "雲嘉南"
                ParaArea = "<span id=""ctl19_labPsi1_4"">"
                ParaType = "<span id=""ctl19_labParam1_4"">"
            Case "高屏"
                ParaArea = "<span id=""ctl19_labPsi1_5"">"
                ParaType = "<span id=""ctl19_labParam1_5"">"
            Case "宜蘭"
                ParaArea = "<span id=""ctl19_labPsi1_6"">"
                ParaType = "<span id=""ctl19_labParam1_6"">"
            Case "花東"
                ParaArea = "<span id=""ctl19_labPsi1_7"">"
                ParaType = "<span id=""ctl19_labParam1_7"">"
            Case "馬祖"
                ParaArea = "<span id=""ctl19_labPsi1_11"">"
                ParaType = "<span id=""ctl19_labParam1_11"">"
            Case "金門"
                ParaArea = "<span id=""ctl19_labPsi1_12"">"
                ParaType = "<span id=""ctl19_labParam1_12"">"
            Case "澎湖"
                ParaArea = "<span id=""ctl19_labPsi1_13"">"
                ParaType = "<span id=""ctl19_labParam1_13"">"
            Case Else
                '預設為高屏
                ParaArea = "<span id=""ctl19_labPsi1_5"">"
                ParaType = "<span id=""ctl19_labParam1_5"">"
        End Select

        Dim indexEnd As Integer
        Dim valueAttribute As String
        Dim valueContain As String

        '使用WebRequest的Get方法取得整個網頁的HTML
        'Dim htmlAll As String = WebPageGenFunc.getHTMLGet(TargetURL)

        '取得網頁Body的部份
        Dim htmlBody As String = WebPageGenFunc.getHTMLBody(TargetURL)
        If Not htmlBody Is Nothing Then

            '取得日期
            indexEnd = htmlBody.IndexOf("<span id=""ctl19_labDay1"">")
            valueAttribute = Nothing
            valueContain = Nothing
            If indexEnd > 0 Then
                htmlBody = htmlBody.Substring(indexEnd)
                WebPageGenFunc.getHTMLTagContain(htmlBody, "span", valueContain, indexEnd)
                ValDate = Trim(valueContain)
            End If

            '取得PSI的內容
            indexEnd = htmlBody.IndexOf(ParaArea)
            valueAttribute = Nothing
            valueContain = Nothing
            If indexEnd > 0 Then
                htmlBody = htmlBody.Substring(indexEnd)
                WebPageGenFunc.getHTMLTagContain(htmlBody, "span", valueContain, indexEnd)
                ValPsi = Trim(valueContain)
            End If

            If ValPsi <> "" Then
                ValPsiLow = MainFunc.GetPsiValue(ValPsi)
                ValPsiHigh = MainFunc.GetPsiValue(ValPsi, "High")
            End If

            '取得指標汙染物
            indexEnd = htmlBody.IndexOf(ParaType)
            valueAttribute = Nothing
            valueContain = Nothing
            If indexEnd > 0 Then
                htmlBody = htmlBody.Substring(indexEnd)
                WebPageGenFunc.getHTMLTagContain(htmlBody, "span", valueContain, indexEnd)
                ValPsiType = Trim(valueContain)
            End If

        End If

        '取得數值
        If ValPsiLow <> "" Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 寄送電子郵件
    ''' </summary>
    ''' <param name="ValType">汙染物類型</param>
    ''' <remarks></remarks>
    Private Sub FuncMailSend(ByVal ValType As Integer)

        Dim toEmail As String = ""
        Dim toBody As String = ""

        toEmail = FS.GetEmailAddress(FileName_MailList)

        '判斷汙染物類型  1:懸浮微粒 2:臭氧
        If ValType = 1 Then
            toBody = FS.GetEmailBody(FileName_MailBody_particle)
        Else
            toBody = FS.GetEmailBody(FileName_MailBody_O3)
        End If

        '填入PSI值
        toBody = Replace(toBody, "{0}", ValPsi)
        toBody = Replace(toBody, vbCrLf, "<br />")

        If FuncMail.SendMail(toEmail, toBody) Then
            ListBoxAdd("信件已發送成功")
        Else
            ListBoxAdd("信件發送失敗")
        End If
    End Sub

    ''' <summary>
    ''' 發送簡訊
    ''' </summary>
    ''' <param name="ValType">汙染物類型</param>
    ''' <remarks></remarks>
    Private Sub FuncSMSSend(ByVal ValType As Integer)

        Dim SMSBody As String

        '判斷汙染物類型  1:懸浮微粒 2:臭氧
        If ValType = 1 Then
            SMSBody = FS.GetSMS(FileName_SMS_particle)
        Else
            SMSBody = FS.GetSMS(FileName_SMS_O3)
        End If

        '填入PSI值
        SMSBody = Replace(SMSBody, "{0}", ValPsi)

        If FuncSMS.sendSMS(SMSBody) Then
            ListBoxAdd("簡訊已發送成功")
        Else
            ListBoxAdd("簡訊發送失敗")
        End If
    End Sub

    '設定計時器
    Private Sub Btn_SetTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SetTime.Click
        If IsNumeric(TB_Minute.Text) Then
            Lab_Timer.Text = ""
            Lab_Msg.Text = ""
            TimerGo()
        Else
            MessageBox.Show("分鐘必須要是數字", "時間錯誤", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TB_Minute.Focus()
        End If
    End Sub

    '開啟或關閉計時器
    Private Sub TimerGo()
        If myTimer.Enabled Then
            myTimer.Stop()
            Lab_Runtime.Text = ""
            Lab_Timer_state.Text = "計時器已停止"
        Else
            '計時器開始
            myTimer.Start()

            Lab_Runtime.Text = "每日 " & NUD_hour.Value & " 點" & TB_Minute.Text & "分 執行"
            Lab_Timer_state.Text = "計時器執行中..."

            '開始時間
            Lab_Timer.Text = "開始時間：" & Now
        End If
    End Sub

    '計時器程式
    Private Sub TimeCheck()

        '取得現在時間
        TimeNow = Now

        If ErrorCode <> 0 Then
            PsiCheck()
        End If

        '判斷是否到達設定時間
        If TimeNow.Hour = CInt(NUD_hour.Value) And TimeNow.Minute = CInt(TB_Minute.Text) And TimeNow.Second >= 0 And TimeNow.Second < 10 Then

            '判斷是否為假日
            If TimeNow.DayOfWeek = DayOfWeek.Saturday Or TimeNow.DayOfWeek = DayOfWeek.Sunday Then
                '判斷Psi值
                PsiCheck_Holiday()
            Else
                '(測試版平常日也要發送簡訊)
                'PsiCheck_Holiday()

                PsiCheck()
            End If

            ListBoxAdd("執行時間：" & TimeNow)
        End If
    End Sub

    '判斷PSI值 (一般上班日無簡訊)
    Private Sub PsiCheck()
        '取得psi值
        If PsiDataProcess(ValArea) Then
            '是否為數字
            If IsNumeric(ValPsiLow) Then
                If CInt(ValPsiLow) >= ValPsiLimit Then

                    '判斷是[懸浮微粒] 或 [臭氧]
                    If ValPsiType = "懸浮微粒" Then
                        '寄信
                        FuncMailSend(1)
                    Else
                        '寄信
                        FuncMailSend(2)
                    End If

                    ErrorCode = 0
                Else
                    ErrorCode = 0
                End If
            Else
                Lab_Msg.Text = "PSI值錯誤"
                ErrorCode = 1521
            End If
        Else
            Lab_Msg.Text = "取得資料失敗"
            ErrorCode = 1520
        End If
    End Sub

    '判斷PSI值 (假日)
    Private Sub PsiCheck_Holiday()
        '取得psi值
        If PsiDataProcess(ValArea) Then
            '是否為數字
            If IsNumeric(ValPsiLow) Then
                If CInt(ValPsiLow) >= ValPsiLimit Then

                    '判斷是[懸浮微粒] 或 [臭氧]
                    If ValPsiType = "懸浮微粒" Then
                        '寄信
                        FuncMailSend(1)
                        '發送簡訊
                        FuncSMSSend(1)
                    Else
                        '寄信
                        FuncMailSend(2)
                        '發送簡訊
                        FuncSMSSend(2)
                    End If

                    ErrorCode = 0
                Else
                    ErrorCode = 0
                End If
            Else
                Lab_Msg.Text = "PSI值錯誤"
                ErrorCode = 1521
            End If
        Else
            Lab_Msg.Text = "取得資料失敗"
            ErrorCode = 1520
        End If
    End Sub

    '表單載入
    Private Sub formMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '設定標題
        Me.Text = App_Title
        NotifyIconMain.Text = App_Title

        '測試用按鈕
        Btn_mail.Visible = False
        Btn_SMS.Visible = False

        Lab_Msg.Text = ""
        Lab_Timer.Text = ""
        Lab_Timer_state.Text = ""
        Lab_Runtime.Text = ""
        LB_Runlog.Items.Clear()

        '計時器基本設定

        '預設10秒
        myTimer.Interval = 10000
        AddHandler myTimer.Tick, AddressOf TimeCheck

        '預設時間 (下午五點半)
        NUD_hour.Value = 17
        TB_Minute.Text = 30

        '啟動計時器
        TimerGo()

        '顯示地區及標準值
        Lab_area.Text = ValArea
        Lab_ValueLimit.Text = "目前標準值為 : " & ValPsiLimit

        '系統列設定
        Me.NotifyIconMain.Visible = False
    End Sub

    '測試信件
    Private Sub Btn_mail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_mail.Click
        If MessageBox.Show("確定要發送測試信件？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            '判斷是[懸浮微粒] 或 [臭氧]
            If ValPsiType = "懸浮微粒" Then
                FuncMailSend(1)
            Else
                FuncMailSend(2)
            End If
        End If
    End Sub

    '測試簡訊
    Private Sub Btn_SMS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_SMS.Click
        If MessageBox.Show("確定要發送測試簡訊？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            '判斷是[懸浮微粒] 或 [臭氧]
            If ValPsiType = "懸浮微粒" Then
                FuncSMSSend(1)
            Else
                FuncSMSSend(2)
            End If
        End If
    End Sub

    '關閉程式
    Private Sub Btn_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Exit.Click
        If MsgBox("確定要關閉程式？", MsgBoxStyle.YesNo, "注意") = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    '縮小到系統列
    Private Sub Btn_Trip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Trip.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.Hide()
            Me.NotifyIconMain.Visible = True
        End If
    End Sub

    '由系統列放大
    Private Sub NotifyIconMain_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIconMain.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.NotifyIconMain.Visible = False
    End Sub
End Class
