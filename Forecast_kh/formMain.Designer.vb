<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formMain
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請不要使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formMain))
        Me.BtnGetPsi = New System.Windows.Forms.Button
        Me.Btn_SetTime = New System.Windows.Forms.Button
        Me.Lab_Timer = New System.Windows.Forms.Label
        Me.Lab_Msg = New System.Windows.Forms.Label
        Me.Lab_DateTime = New System.Windows.Forms.Label
        Me.Lab_hour = New System.Windows.Forms.Label
        Me.NUD_hour = New System.Windows.Forms.NumericUpDown
        Me.Lab_Timer_state = New System.Windows.Forms.Label
        Me.Lab_Runtime = New System.Windows.Forms.Label
        Me.LB_Runlog = New System.Windows.Forms.ListBox
        Me.Btn_mail = New System.Windows.Forms.Button
        Me.GB_Timer = New System.Windows.Forms.GroupBox
        Me.Lab_minute = New System.Windows.Forms.Label
        Me.TB_Minute = New System.Windows.Forms.TextBox
        Me.Btn_SMS = New System.Windows.Forms.Button
        Me.Lab_area = New System.Windows.Forms.Label
        Me.Lab_ValueLimit = New System.Windows.Forms.Label
        Me.Btn_Trip = New System.Windows.Forms.Button
        Me.Btn_Exit = New System.Windows.Forms.Button
        Me.NotifyIconMain = New System.Windows.Forms.NotifyIcon(Me.components)
        CType(Me.NUD_hour, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Timer.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnGetPsi
        '
        Me.BtnGetPsi.Location = New System.Drawing.Point(312, 173)
        Me.BtnGetPsi.Name = "BtnGetPsi"
        Me.BtnGetPsi.Size = New System.Drawing.Size(268, 40)
        Me.BtnGetPsi.TabIndex = 0
        Me.BtnGetPsi.Text = "取得最新資料"
        Me.BtnGetPsi.UseVisualStyleBackColor = True
        '
        'Btn_SetTime
        '
        Me.Btn_SetTime.Location = New System.Drawing.Point(12, 21)
        Me.Btn_SetTime.Name = "Btn_SetTime"
        Me.Btn_SetTime.Size = New System.Drawing.Size(101, 40)
        Me.Btn_SetTime.TabIndex = 8
        Me.Btn_SetTime.Text = "設定計時器"
        Me.Btn_SetTime.UseVisualStyleBackColor = True
        '
        'Lab_Timer
        '
        Me.Lab_Timer.AutoSize = True
        Me.Lab_Timer.Location = New System.Drawing.Point(10, 108)
        Me.Lab_Timer.Name = "Lab_Timer"
        Me.Lab_Timer.Size = New System.Drawing.Size(53, 12)
        Me.Lab_Timer.TabIndex = 9
        Me.Lab_Timer.Text = "開始時間"
        '
        'Lab_Msg
        '
        Me.Lab_Msg.AutoSize = True
        Me.Lab_Msg.Location = New System.Drawing.Point(10, 131)
        Me.Lab_Msg.Name = "Lab_Msg"
        Me.Lab_Msg.Size = New System.Drawing.Size(53, 12)
        Me.Lab_Msg.TabIndex = 10
        Me.Lab_Msg.Text = "執行時間"
        '
        'Lab_DateTime
        '
        Me.Lab_DateTime.AutoSize = True
        Me.Lab_DateTime.Location = New System.Drawing.Point(151, 21)
        Me.Lab_DateTime.Name = "Lab_DateTime"
        Me.Lab_DateTime.Size = New System.Drawing.Size(77, 12)
        Me.Lab_DateTime.TabIndex = 11
        Me.Lab_DateTime.Text = "每日執行時間"
        '
        'Lab_hour
        '
        Me.Lab_hour.AutoSize = True
        Me.Lab_hour.Location = New System.Drawing.Point(184, 51)
        Me.Lab_hour.Name = "Lab_hour"
        Me.Lab_hour.Size = New System.Drawing.Size(17, 12)
        Me.Lab_hour.TabIndex = 13
        Me.Lab_hour.Text = "點"
        '
        'NUD_hour
        '
        Me.NUD_hour.Location = New System.Drawing.Point(132, 41)
        Me.NUD_hour.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.NUD_hour.Name = "NUD_hour"
        Me.NUD_hour.Size = New System.Drawing.Size(46, 22)
        Me.NUD_hour.TabIndex = 14
        '
        'Lab_Timer_state
        '
        Me.Lab_Timer_state.AutoSize = True
        Me.Lab_Timer_state.Location = New System.Drawing.Point(10, 79)
        Me.Lab_Timer_state.Name = "Lab_Timer_state"
        Me.Lab_Timer_state.Size = New System.Drawing.Size(65, 12)
        Me.Lab_Timer_state.TabIndex = 15
        Me.Lab_Timer_state.Text = "計時器狀態"
        '
        'Lab_Runtime
        '
        Me.Lab_Runtime.AutoSize = True
        Me.Lab_Runtime.Location = New System.Drawing.Point(151, 79)
        Me.Lab_Runtime.Name = "Lab_Runtime"
        Me.Lab_Runtime.Size = New System.Drawing.Size(45, 12)
        Me.Lab_Runtime.TabIndex = 16
        Me.Lab_Runtime.Text = "Runtime"
        '
        'LB_Runlog
        '
        Me.LB_Runlog.FormattingEnabled = True
        Me.LB_Runlog.ItemHeight = 12
        Me.LB_Runlog.Location = New System.Drawing.Point(12, 105)
        Me.LB_Runlog.Name = "LB_Runlog"
        Me.LB_Runlog.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.LB_Runlog.Size = New System.Drawing.Size(276, 256)
        Me.LB_Runlog.TabIndex = 18
        '
        'Btn_mail
        '
        Me.Btn_mail.Location = New System.Drawing.Point(312, 219)
        Me.Btn_mail.Name = "Btn_mail"
        Me.Btn_mail.Size = New System.Drawing.Size(101, 20)
        Me.Btn_mail.TabIndex = 19
        Me.Btn_mail.Text = "信件測試"
        Me.Btn_mail.UseVisualStyleBackColor = True
        '
        'GB_Timer
        '
        Me.GB_Timer.Controls.Add(Me.Lab_minute)
        Me.GB_Timer.Controls.Add(Me.TB_Minute)
        Me.GB_Timer.Controls.Add(Me.Lab_Runtime)
        Me.GB_Timer.Controls.Add(Me.Lab_Msg)
        Me.GB_Timer.Controls.Add(Me.Lab_Timer)
        Me.GB_Timer.Controls.Add(Me.Lab_hour)
        Me.GB_Timer.Controls.Add(Me.NUD_hour)
        Me.GB_Timer.Controls.Add(Me.Lab_Timer_state)
        Me.GB_Timer.Controls.Add(Me.Btn_SetTime)
        Me.GB_Timer.Controls.Add(Me.Lab_DateTime)
        Me.GB_Timer.Location = New System.Drawing.Point(312, 12)
        Me.GB_Timer.Name = "GB_Timer"
        Me.GB_Timer.Size = New System.Drawing.Size(268, 155)
        Me.GB_Timer.TabIndex = 20
        Me.GB_Timer.TabStop = False
        Me.GB_Timer.Text = "設定每日執行時間"
        '
        'Lab_minute
        '
        Me.Lab_minute.AutoSize = True
        Me.Lab_minute.Location = New System.Drawing.Point(239, 49)
        Me.Lab_minute.Name = "Lab_minute"
        Me.Lab_minute.Size = New System.Drawing.Size(17, 12)
        Me.Lab_minute.TabIndex = 18
        Me.Lab_minute.Text = "分"
        '
        'TB_Minute
        '
        Me.TB_Minute.Location = New System.Drawing.Point(207, 40)
        Me.TB_Minute.MaxLength = 2
        Me.TB_Minute.Name = "TB_Minute"
        Me.TB_Minute.Size = New System.Drawing.Size(26, 22)
        Me.TB_Minute.TabIndex = 17
        '
        'Btn_SMS
        '
        Me.Btn_SMS.Location = New System.Drawing.Point(312, 245)
        Me.Btn_SMS.Name = "Btn_SMS"
        Me.Btn_SMS.Size = New System.Drawing.Size(101, 20)
        Me.Btn_SMS.TabIndex = 22
        Me.Btn_SMS.Text = "簡訊測試"
        Me.Btn_SMS.UseVisualStyleBackColor = True
        '
        'Lab_area
        '
        Me.Lab_area.AutoSize = True
        Me.Lab_area.Font = New System.Drawing.Font("新細明體", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Lab_area.Location = New System.Drawing.Point(12, 12)
        Me.Lab_area.Name = "Lab_area"
        Me.Lab_area.Size = New System.Drawing.Size(49, 29)
        Me.Lab_area.TabIndex = 23
        Me.Lab_area.Text = "----"
        '
        'Lab_ValueLimit
        '
        Me.Lab_ValueLimit.AutoSize = True
        Me.Lab_ValueLimit.Location = New System.Drawing.Point(15, 72)
        Me.Lab_ValueLimit.Name = "Lab_ValueLimit"
        Me.Lab_ValueLimit.Size = New System.Drawing.Size(11, 12)
        Me.Lab_ValueLimit.TabIndex = 24
        Me.Lab_ValueLimit.Text = "0"
        '
        'Btn_Trip
        '
        Me.Btn_Trip.Location = New System.Drawing.Point(312, 271)
        Me.Btn_Trip.Name = "Btn_Trip"
        Me.Btn_Trip.Size = New System.Drawing.Size(268, 40)
        Me.Btn_Trip.TabIndex = 25
        Me.Btn_Trip.Text = "縮小到系統列"
        Me.Btn_Trip.UseVisualStyleBackColor = True
        '
        'Btn_Exit
        '
        Me.Btn_Exit.Location = New System.Drawing.Point(312, 321)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(268, 40)
        Me.Btn_Exit.TabIndex = 26
        Me.Btn_Exit.Text = "關閉程式"
        Me.Btn_Exit.UseVisualStyleBackColor = True
        '
        'NotifyIconMain
        '
        Me.NotifyIconMain.Icon = CType(resources.GetObject("NotifyIconMain.Icon"), System.Drawing.Icon)
        Me.NotifyIconMain.Text = "空氣品質預報自動通報"
        Me.NotifyIconMain.Visible = True
        '
        'formMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(592, 373)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.Btn_Trip)
        Me.Controls.Add(Me.Lab_ValueLimit)
        Me.Controls.Add(Me.Lab_area)
        Me.Controls.Add(Me.Btn_SMS)
        Me.Controls.Add(Me.GB_Timer)
        Me.Controls.Add(Me.Btn_mail)
        Me.Controls.Add(Me.LB_Runlog)
        Me.Controls.Add(Me.BtnGetPsi)
        Me.MaximizeBox = False
        Me.Name = "formMain"
        Me.Text = "空氣品質預報自動通報"
        CType(Me.NUD_hour, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Timer.ResumeLayout(False)
        Me.GB_Timer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnGetPsi As System.Windows.Forms.Button
    Friend WithEvents Btn_SetTime As System.Windows.Forms.Button
    Friend WithEvents Lab_Timer As System.Windows.Forms.Label
    Friend WithEvents Lab_Msg As System.Windows.Forms.Label
    Friend WithEvents Lab_DateTime As System.Windows.Forms.Label
    Friend WithEvents Lab_hour As System.Windows.Forms.Label
    Friend WithEvents NUD_hour As System.Windows.Forms.NumericUpDown
    Friend WithEvents Lab_Timer_state As System.Windows.Forms.Label
    Friend WithEvents Lab_Runtime As System.Windows.Forms.Label
    Friend WithEvents LB_Runlog As System.Windows.Forms.ListBox
    Friend WithEvents Btn_mail As System.Windows.Forms.Button
    Friend WithEvents GB_Timer As System.Windows.Forms.GroupBox
    Friend WithEvents Btn_SMS As System.Windows.Forms.Button
    Friend WithEvents TB_Minute As System.Windows.Forms.TextBox
    Friend WithEvents Lab_minute As System.Windows.Forms.Label
    Friend WithEvents Lab_area As System.Windows.Forms.Label
    Friend WithEvents Lab_ValueLimit As System.Windows.Forms.Label
    Friend WithEvents Btn_Trip As System.Windows.Forms.Button
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents NotifyIconMain As System.Windows.Forms.NotifyIcon

End Class
