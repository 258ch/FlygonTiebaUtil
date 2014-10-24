'v0.4 2013.8.1 - 飞龙

Imports System.Net
Imports System.Text

Public Class WizardHTTP
    Inherits WebClient

    Private _TimeOut As Integer = 8000
    Private _ReadWriteTimeOut As Integer = 8000
    Private _AllowAutoRedirect As Boolean = True

    Public Property TimeOut As Integer
        Get
            Return _TimeOut
        End Get
        Set(ByVal value As Integer)
            _TimeOut = value
        End Set
    End Property

    Public Property ReadWriteTimeOut As Integer
        Get
            Return _ReadWriteTimeOut
        End Get
        Set(ByVal value As Integer)
            _ReadWriteTimeOut = value
        End Set
    End Property

    Public Property AllowAutoRedirect As Boolean
        Get
            Return _AllowAutoRedirect
        End Get
        Set(ByVal value As Boolean)
            _AllowAutoRedirect = value
        End Set
    End Property

    Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As System.Net.WebRequest
        Dim request As HttpWebRequest = MyBase.GetWebRequest(address)
        request.Timeout = _TimeOut
        request.ReadWriteTimeout = _ReadWriteTimeOut
        request.AllowAutoRedirect = _AllowAutoRedirect
        Return request
    End Function

    Public Sub SetDefaultHeader(ByVal Optional mobile As Boolean = False)
        MyBase.Headers.Set(HttpRequestHeader.Accept, "*/*")
        MyBase.Headers.Set(HttpRequestHeader.AcceptLanguage, "zh-cn")
		If mobile Then : MyBase.Headers.Set(HttpRequestHeader.UserAgent, "Dalvik/1.1.0 (Linux; U; Android 2.1; sdk Build/ERD79)")
        Else : MyBase.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)") : End If
        MyBase.Headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded")
        MyBase.Headers.Set("Cache-Control", "no-cache")
    End Sub
End Class
