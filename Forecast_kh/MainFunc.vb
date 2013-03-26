Imports System.String

Public Class MainFunc

    ''' <summary>
    ''' 輸入PSI範圍, 取得Psi最低值
    ''' </summary>
    ''' <param name="Psi">網址</param>
    ''' <param name="PsiType">PSI值高或低</param>
    ''' <returns>Psi值</returns>
    ''' <remarks>使用Get方法取得網頁內容</remarks>
    Public Shared Function GetPsiValue(ByVal Psi As String, Optional ByVal PsiType As String = "Low") As String

        Dim LowValue() As String
        Dim Result As String = ""

        LowValue = Psi.Split("-")

        If LowValue.Length = 2 Then
            Select Case PsiType
                Case "Low"
                    Result = LowValue(0)
                Case "High"
                    Result = LowValue(1)
                Case Else
                    Result = ""
            End Select
        End If

        Return Result
    End Function
End Class
