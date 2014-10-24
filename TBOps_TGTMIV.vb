Imports TGTMIV.Utility
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Net
Imports System.IO
Imports LitJson

Public Class TBOps_TGTMIV

    Public Shared Function GetTbs(ByVal wc As WizardHTTP) As String '请预先设置好cookie
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/dc/common/tbs")
        Return JsonMapper.ToObject(retstr).Item("tbs")
    End Function

    Public Shared Function GetFid(ByVal wc As WizardHTTP, ByVal tb As String) As String
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/f/commit/share/fnameShareApi?fname=" + _
                                                 URLEncoding(tb, Encoding.Default))
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim no As Integer = json.Item("no")
        If no <> 0 Then Throw New Exception("fid获取失败")
        Return json.Item("data").Item("fid").ToString()
    End Function

    Public Shared Function ClientLogin(ByVal wc As WizardHTTP, ByVal id As String, _
                                       ByVal pw As String, ByVal vcode As String, _
                                       ByVal vmd5 As String) As LoginResult
        Dim cid As String = GetStampMobile(True)
        Dim poststr As String = "_client_id=" + cid + "&_client_type=2&_client_version=1.0.1&_phone_imei=000000000000000" + _
                  "&from=baidu_appstore&isphone=0&net_type=1&passwd=" + ToBase64(pw) + "&un=" + id + _
                  "&vcode=" + vcode + "&vcode_md5=" + vmd5
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr = "_client_id=" + cid + "&_client_type=2&_client_version=1.0.1&_phone_imei=000000000000000" + _
                  "&from=baidu_appstore&isphone=0&net_type=1&passwd=" + ToBase64(pw) + "&un=" + _
                  URLEncoding(id, Encoding.UTF8) + "&vcode=" + vcode + "&vcode_md5=" + vmd5 + "&sign=" + sign
        wc.SetDefaultHeader(True)
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/s/login", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)

        vmd5 = ""
        Dim ndvcode As Integer
        Try
            ndvcode = json.Item("anti").Item("need_vcode")
        Catch ex As Exception
            ndvcode = 0
        End Try
        If ndvcode = 1 Then  '需要验证码
            vmd5 = json.Item("anti").Item("vcode_md5")
        End If

        Dim errno As String = json.Item("error_code")
        If errno <> "0" Then
            Dim errmsg As String = json.Item("error_msg")
            Return New LoginResult(Convert.ToInt32(errno), errmsg, vmd5, "")
        Else
            Dim cookie As String = "BDUSS=" + CType(json.Item("user").Item("BDUSS"), String)
            Return New LoginResult(0, "", "", cookie)
        End If
    End Function

    Public Shared Function ClientSign(ByVal wc As WizardHTTP, ByVal fid As String, ByVal tb As String) As Result
        Dim tbs As String = GetTbs(wc)
        Dim cid As String = GetStampMobile(True)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=2.5.1" + _
                                "&_phone_imei=000000000000000&fid=" + fid + "&from=tieba&kw=" + tb + _
                                "&net_type=1&tbs=" + tbs
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8).ToUpper()
        poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=2.5.1" + _
                           "&_phone_imei=000000000000000" + "&fid=" + fid + "&from=tieba&kw=" + _
                           URLEncoding(tb, Encoding.UTF8) + "&net_type=1&tbs=" + tbs + "&sign=" + sign
        wc.SetDefaultHeader(True)
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/c/forum/sign", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")
        If errno <> "0" Then
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        Else
            Return New Result(0, "")
        End If
    End Function

    Public Shared Function GetTBList(ByVal wc As WizardHTTP) As String()
        wc.SetDefaultHeader()
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim poststr As String = cookie + "&_client_id=" + GetStampMobile(True) + "&_client_type=2&_client_version=5.7.0" + _
                                "&_phone_imei=000000000000000&from=tieba&like_forum=1&recommend=0&topic=0"
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr += "&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/f/forum/forumrecommend", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim list As JsonData = json.Item("like_forum")
        Dim res As New List(Of String)
        For i As Integer = 0 To list.Count - 1
            res.Add(list.Item(i).Item("forum_name"))
        Next
        Return res.ToArray()
    End Function

    Public Shared Function GetIDAndTbs(ByVal wc As WizardHTTP, Optional ByVal name As String = "") As String()
        wc.SetDefaultHeader()
        Dim url As String = "http://tieba.baidu.com/i/sys/user_json"
        If name <> "" Then url += "?un=" + URLEncoding(name, Encoding.Default)
        Dim retstr As String = wc.DownloadString(url)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim tbs As String = json.Item("tbs")
        Dim id As String = json.Item("id").ToString()
        Return New String() {id, tbs}
    End Function

    Public Shared Function CleanFans(ByVal wc As WizardHTTP, ByVal id As String, ByVal tbs As String) As Result
        wc.SetDefaultHeader()
        Dim poststr As String = "cmd=add_black_list&itieba_id=" + id + "&tbs=" + tbs + "&ie=utf-8"
        Dim retstr As String = wc.UploadString("http://tieba.baidu.com/i/commit", poststr)
        '{"is_done":false,"error_no":3,"msg":[],"_info":"\u672a\u77e5\u9519\u8bef","ret":[]}
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_no").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("_info")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function RemoveClean(ByVal wc As WizardHTTP, ByVal id As String, ByVal tbs As String) As Result
        wc.SetDefaultHeader()
        Dim poststr As String = "cmd=cancel_black_list&itieba_id=" + id + "&tbs=" + tbs + "&ie=utf-8"
        Dim retstr As String = wc.UploadString("http://tieba.baidu.com/i/commit", poststr)
        '{"is_done":false,"error_no":3,"msg":[],"_info":"\u672a\u77e5\u9519\u8bef","ret":[]}
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_no").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("_info")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function GetThreadList(ByVal wc As WizardHTTP, ByVal tb As String, ByVal pn As String) As ThreadInfo()
        wc.SetDefaultHeader()
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim cid As String = GetStampMobile(True)
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.7.0" + _
                                "&_phone_imei=000000000000000&data_size=51531&from=tieba&kw=" + tb + "&pn=" + pn + "&q_type=1&rn=35" + _
                                "&scr_dip=1.5&scr_h=800&scr_w=480&with_group=1"
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.7.0" + _
                  "&_phone_imei=000000000000000&data_size=51531&from=tieba&kw=" + URLEncoding(tb, Encoding.UTF8) + _
                  "&pn=" + pn + "&q_type=1&rn=35" + "&scr_dip=1.5&scr_h=800&scr_w=480&with_group=1&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/f/frs/page", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim list As JsonData = json.Item("thread_list")
        Dim res As New List(Of ThreadInfo)
        For i As Integer = 0 To list.Count - 1
            Dim tid As String = list.Item(i).Item("tid")
            Dim title As String = list.Item(i).Item("title")
            Dim author As String = list.Item(i).Item("author").Item("name")
            res.Add(New ThreadInfo(title, tid, author))
        Next
        Return res.ToArray()
    End Function

    Public Shared Function Ban(ByVal wc As WizardHTTP, ByVal name As String, ByVal fid As String, ByVal tb As String) As Result
        Dim tbs As String = GetTbs(wc)
        Dim poststr As String = "cm=filter_forum_user&user_name=" + URLEncoding(name, Encoding.UTF8) + _
                                 "&ban_days=1&word=" + URLEncoding(tb, Encoding.Default) + "&fid=" + fid + _
                                 "&tbs=" + tbs + "&ie=utf-8"
        wc.SetDefaultHeader()
        Dim retstr As String = wc.UploadString("http://tieba.baidu.com/bawu/cm", poststr)
        '{"error":{"retval":0,"msg":"","errno":0,"errmsg":""}}
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error").Item("errno").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Return New Result(Convert.ToInt32(errno), "错误信息：" + errno)
        End If
    End Function

    Public Shared Function DeleteThread(ByVal wc As WizardHTTP, ByVal tid As String, _
                                        ByVal tb As String, ByVal fid As String) As Result
        Dim tbs As String = GetTbs(wc)
        Dim poststr As String = "ie=gbk&tbs=" + tbs + "&kw=" + URLEncoding(tb, Encoding.Default) + _
                                "&fid=" + fid + "&tid=" + tid
        wc.SetDefaultHeader()
        Dim retstr As String = wc.UploadString("http://tieba.baidu.com/f/commit/thread/delete", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("no").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Return New Result(Convert.ToInt32(errno), "错误信息：" + errno)
        End If
    End Function

    Public Shared Function GetFansList(ByVal wc As WizardHTTP, ByVal itieba As String, ByVal pn As Integer) As FansInfo()
        Dim re_id As New Regex("inid=""\d+""")
        Dim re_un As New Regex("href=""#"" n="".{1,14}"" ")
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/i/" + itieba + _
                                                 "/fans?pn=" + pn.ToString())
        Dim rms_id As MatchCollection = re_id.Matches(retstr)
        Dim rms_un As MatchCollection = re_un.Matches(retstr)
        Dim res As New List(Of FansInfo)
        For j As Integer = 0 To rms_id.Count - 1
            Dim innerid As String = rms_id(j).Value.Substring(6, rms_id(j).Value.Length - 7)
            Dim un As String = rms_un(j).Value.Substring(12, rms_un(j).Value.Length - 14)
            res.Add(New FansInfo(innerid, un))
        Next
        Return res.ToArray()
    End Function
End Class

Public Structure ThreadInfo
    Public title As String
    Public tid As String
    Public author As String

    Public Sub New(ByVal title_ As String, tid_ As String, ByVal author_ As String)
        title = title_
        tid = tid_
        author = author_
    End Sub
End Structure

Public Structure FansInfo
    Public innerid As String
    Public name As String

    Public Sub New(ByVal i As String, n As String)
        innerid = i
        name = n
    End Sub
End Structure