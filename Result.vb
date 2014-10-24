Public Structure Result
    Public errno As Integer
    Public errmsg As String

    Public Sub New(ByVal no As Integer, ByVal msg As String)
        errno = no
        errmsg = msg
    End Sub
End Structure

Public Structure LoginResult
    Public errno As Integer
    Public errmsg As String
    Public vmd5 As String
    Public cookie As String

    Public Sub New(ByVal no As Integer, ByVal msg As String, ByVal md5 As String, ByVal co As String)
        errno = no
        errmsg = msg
        vmd5 = md5
        cookie = co
    End Sub
End Structure
