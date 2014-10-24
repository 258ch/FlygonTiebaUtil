Imports System.Threading

Public Class LogForm

    Private UN As String
    Private PW As String
    Private Success As Boolean = False

    Private Sub EscButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EscButton.Click
        End
    End Sub

    Private Sub LogForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Success Then End
    End Sub

    Private Sub LoginButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Try
            LoginButton.Enabled = False
            UN = IDTextBox.Text
            PW = PWTextBox.Text
            If UN = "" Or PW = "" Then Throw New Exception("请输入用户名和密码！")
            Dim tr As New Thread(AddressOf login)
            tr.Start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            LoginButton.Enabled = True
        End Try
    End Sub

    Private Sub login()
        Try
            Dim wc As New WizardHTTP
            wc.SetDefaultHeader()
            Dim poststr As String = "fastloginfield=username&username=" + MyFunctions.URLEncoGBK(UN) + "&password=" + PW + "&quickforward=yes&handlekey=ls"
            Dim retstr As String = wc.UploadString("http://www.258ch.com/member.php?mod=logging&action=login&loginsubmit=yes&infloat=yes&lssubmit=yes&inajax=1", poststr)
            Dim left As Integer = retstr.IndexOf("[CDATA[")
            If left = -1 Then Throw New Exception("登录失败！")
            left += 7
            Dim right As Integer = retstr.IndexOf("<script")
            If right = -1 Then Throw New Exception("登录失败！")
            Dim err As String = retstr.Substring(left, right - left)
            If err <> "" Then Throw New Exception(err)
            MessageBox.Show("登录成功！")
            Success = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            LoginButton.Enabled = True
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://www.258ch.com/forum.php")
    End Sub
End Class