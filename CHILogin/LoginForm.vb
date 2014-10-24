Imports System.Threading
Imports System.Text
Imports CHILogin.MyFunctions
Imports System.Text.RegularExpressions

Public Class LoginForm

    Private UN As String
    Private PW As String
    Private Ques As Integer
    Private Ans As String
    Private Success As Boolean = False
    Private Proxy As String

    Private Sub ExitButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        End
    End Sub

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        QuesComboBox.SelectedIndex = 0
    End Sub

    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Try
            LoginButton.Enabled = False
            UN = UNTextBox.Text
            PW = PWTextBox.Text
            If UN = "" Or PW = "" Then Throw New Exception("请填写用户名和密码！")
            Ques = QuesComboBox.SelectedIndex
            Ans = AnsTextBox.Text
            Proxy = PrTextBox.Text
            Dim tr As New Thread(AddressOf Login)
            tr.Start()
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Time() + ex.Message)
            LoginButton.Enabled = True
        End Try
    End Sub

    Private Sub Login()
        Try
            Dim wc As New WizardHTTP
            wc.SetDefaultHeader()
            Dim retstr As String = wc.DownloadString("http://www.258ch.com/member.php?mod=logging&action=login&referer=http%3A%2F%2Fwww.258ch.com%2Fforum.php&referer=http%3A//www.258ch.com/member.php%3Fmod%3Dregisterbbsf")
            Dim left As Integer = retstr.IndexOf("name=""formhash") + 23
            Dim right As Integer = retstr.IndexOf("""", left)
            Dim hash As String = retstr.Substring(left, right - left)
            left = retstr.IndexOf("action=""member.php") + 8
            right = retstr.IndexOf("""", left)
            Dim url As String = retstr.Substring(left, right - left)
            url = url.Replace("amp;", "")
            Dim poststr As String = "formhash=" + hash + "&referer=http%3A%2F%2Fwww.258ch.com%2Fmember.php%3Fmod%3Dregisterbbsf&username=" + URLEncoGBK(UN) + "&password=" + PW + "&questionid=" + Ques.ToString() + "&answer=" + URLEncoGBK(Ans)
            wc.SetDefaultHeader()
            If Proxy <> "" Then wc.Proxy = New Net.WebProxy(Proxy)
            retstr = wc.UploadString("http://www.258ch.com/" + url, poststr)
            'Dim rethdr As String = wc.ResponseHeaders.Get("Set-Cookie")
            'Dim re As New Regex("WGMU_2132_auth=.{92}")
            left = retstr.IndexOf("欢迎您回来，")
            If left <> -1 Then
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(Time() + "登录成功！")
                '取用户组信息
                left = retstr.IndexOf(">", left) + 1
                right = retstr.IndexOf("</", left)
                Dim group As String = retstr.Substring(left, right - left)
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(Time() + "用户组：" + group)
                Success = True
                Me.Close()
                Exit Sub
            End If
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Time() + "登录失败！")
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(ex.Message)
        End Try
        LoginButton.Enabled = True
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://www.258ch.com/forum.php")
    End Sub

    Private Sub LoginForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Success Then End
    End Sub
End Class