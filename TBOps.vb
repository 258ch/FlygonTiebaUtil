'v1.0 2014.5.1 by 飞龙 - CHI

Imports Providence.Utility
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Net
Imports System.IO
Imports LitJson

Public Class TBOps

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

    Public Shared Function TestBan(ByVal wc As WizardHTTP, Optional ByVal name As String = "") As Boolean
        wc.SetDefaultHeader()
        Dim url As String = "http://tieba.baidu.com/i/sys/user_json"
        If name <> "" Then url += "?un=" + URLEncoding(name, Encoding.Default)
        Dim retstr As String = wc.DownloadString(url)
        Return JsonMapper.ToObject(retstr).Item("creator").Item("is_prison")
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

    Public Shared Function ClientFollow(ByVal wc As WizardHTTP, ByVal port As String, _
                                        ByVal tbs As String) As Result
        wc.SetDefaultHeader(True)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim poststr As String = cookie + "&_client_id=" + GetStampMobile(True) + "&_client_type=2" + _
                                "&_client_version=1.0.1&_phone_imei=000000000000000&from=tieba&net_type=1" + _
                                "&portrait=" + port + "&tbs=" + tbs
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr += "&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/c/user/follow", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function FollowTbs(ByVal wc As WizardHTTP, ByVal un As String) As String
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/i/sys/user_json?ie=utf-8&un=" + _
                                                 URLEncoding(un, Encoding.UTF8))
        Return JsonMapper.ToObject(retstr).Item("tbs")
    End Function

    Public Shared Function TBHomeFollow(ByVal wc As WizardHTTP, ByVal un As String) As Result
        Dim tbs = GetTbs(wc)
        Dim poststr As String = "ie=utf-8&un=" + URLEncoding(un, Encoding.UTF8) + "&tbs=" + tbs
        wc.SetDefaultHeader()
        Dim retstr = wc.UploadString("http://tieba.baidu.com/home/post/follow", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("no").ToString()

        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function ClientZan(ByVal wc As WizardHTTP, ByVal tid As String, ByVal pid As String) As Result
        wc.SetDefaultHeader()
        Dim cid As String = Utility.GetStampMobile(True)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.7.0" + _
                      "&_phone_imei=000000000000000&action=like&from=tieba&post_id=" + pid + _
                      "&thread_id=" + tid
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr += "&sign=" + sign
        Dim retstr = wc.UploadString("http://c.tieba.baidu.com/c/c/zan/like", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function TestOffLine(ByVal wc As WizardHTTP)
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/dc/common/tbs")
        Dim i As Integer = JsonMapper.ToObject(retstr).Item("is_login")
        Return i <> 1
    End Function

    Public Shared Function ClientLike(ByVal wc As WizardHTTP, ByVal fid As String, ByVal tb As String) As Result
        Dim tbs As String = GetTbs(wc)
        wc.SetDefaultHeader(True)
        Dim cid As String = GetStampMobile(True)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=2.5.1" + _
                                "&_phone_imei=000000000000000&fid=" + fid + "&from=tieba&kw=" + tb + _
                                "&net_type=1&tbs=" + tbs
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8).ToUpper()
        poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=2.5.1" + _
                  "&_phone_imei=000000000000000&fid=" + fid + "&from=tieba&kw=" + URLEncoding(tb, Encoding.UTF8) + _
                  "&net_type=1" + "&tbs=" + tbs + "&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/c/forum/like", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function WebSign(ByVal wc As WizardHTTP, ByVal tb As String) As Result
        Dim tbs As String = GetTbs(wc)
        wc.SetDefaultHeader()
        Dim poststr = "ie=utf-8&kw=" + URLEncoding(tb, Encoding.UTF8) + "&tbs=" + tbs
        Dim retstr = wc.UploadString("http://tieba.baidu.com/sign/add", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("no").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function MeiZhi(ByVal wc As WizardHTTP, ByVal fid As String, ByVal kw As String, _
                                  ByVal tar_uid As String, ByVal self_uid As String, ByVal type As String) As Result
        Dim tbs As String = GetTbs(wc)
        wc.SetDefaultHeader()
        Dim poststr As String = "content=&tbs=" + tbs + "&fid=" + fid + "&kw=" + URLEncoding(kw, Encoding.UTF8) + _
                                "&uid=" + tar_uid + _
                                "&scid=" + self_uid + "&vtype=" + type + "&ie=utf-8&vcode=&new_vcode=1&tag=11"
        Dim retstr As String = wc.UploadString("http://tieba.baidu.com/encourage/post/meizhi/vote", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("no").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function ClientMsg(ByVal wc As WizardHTTP, ByVal tar_uid As String, _
                                     ByVal self_uid As String, ByVal content As String) As Result
        wc.SetDefaultHeader(True)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim cid As String = GetStampMobile(True)
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.2.2" + _
                                "&_phone_imei=000000000000000&com_id=" + tar_uid + "&content=" + content + _
                                "&from=baidu_appstore&last_msg_id=0&net_type=1&user_id=" + self_uid
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.2.2" + _
                  "&_phone_imei=000000000000000&com_id=" + tar_uid + "&content=" + _
                  URLEncoding(content, Encoding.UTF8) + "&from=baidu_appstore&last_msg_id=0&net_type=1" + _
                  "&user_id=" + self_uid + "&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/s/addmsg", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")

        If errno = "0" Then
            errno = json.Item("error").Item("errno")
            If errno = "0" Then
                Return New Result(0, "")
            Else
                Dim errmsg As String = json.Item("error").Item("usermsg")
                Return New Result(Convert.ToInt32(errno), errmsg)
            End If
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function GetUid(ByVal wc As WizardHTTP, Optional ByVal name As String = "") As String
        wc.SetDefaultHeader()
        Dim url As String = "http://tieba.baidu.com/i/sys/user_json"
        If name <> "" Then url += "?un=" + URLEncoding(name, Encoding.Default)
        Dim retstr = wc.DownloadString(url)
        Return JsonMapper.ToObject(retstr).Item("creator").Item("id").ToString()
    End Function

    Public Shared Function GetPortrait(ByVal wc As WizardHTTP, Optional ByVal name As String = "") As String
        wc.SetDefaultHeader()
        Dim url As String = "http://tieba.baidu.com/i/sys/user_json"
        If name <> "" Then url += "?un=" + URLEncoding(name, Encoding.Default)
        Dim retstr = wc.DownloadString(url)
        Return JsonMapper.ToObject(retstr).Item("creator").Item("portrait")
    End Function

    Public Shared Function GetIdAndPortrait(ByVal wc As WizardHTTP, Optional ByVal name As String = "") As String()
        wc.SetDefaultHeader()
        Dim url As String = "http://tieba.baidu.com/i/sys/user_json"
        If name <> "" Then url += "?un=" + URLEncoding(name, Encoding.Default)
        Dim retstr = wc.DownloadString(url)
        Dim port As String = JsonMapper.ToObject(retstr).Item("creator").Item("portrait")
        Dim id As String = JsonMapper.ToObject(retstr).Item("id").ToString()
        Return New String() {id, port}
    End Function

    Public Shared Function Unban(ByVal wc As WizardHTTP)
        wc.SetDefaultHeader()
        Dim retstr = wc.DownloadString("http://tieba.baidu.com/pmc/listmsg?" + _
                                       "punish_type=system&status=todo&currpage=1&read=all")
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("errno").ToString()
        If errno <> "0" Then
            Dim errmsg As String = json.Item("errmsg")
            Return New Result(errno, errmsg)
        End If

        Dim list As JsonData = json.Item("data").Item("list")
        If list.Count = 0 Then
            Return New Result(-1, "未被封禁或屏蔽")
        End If

        Dim msgid As String = list.Item(0).Item("msg_id")
        Dim poststr = "msgid=" + msgid + "&pid=0&tid=0&content=%E6%88%91%E7%9C%9F%E7%9A%84%E5%BE%88%E7%83%AD%E7%88%B1" + _
                      "%E8%B4%B4%E5%90%A7%EF%BC%8C%E5%B8%8C%E6%9C%9B%E8%A7%A3%E5%B0%81%E6%88%91%E7%9A%84ID%EF%BC%8C%E7" + _
                      "%AE%A1%E7%90%86%E5%91%98%E8%BE%9B%E8%8B%A6%E4%BA%86%EF%BC%8C%E8%A7%A3%E5%B0%81%E4%BA%86%E5%90%A7" + _
                      "%EF%BC%81"
        wc.SetDefaultHeader()
        retstr = wc.UploadString("http://tieba.baidu.com/pmc/commitmanual", poststr)
        json = JsonMapper.ToObject(retstr)
        errno = json.Item("errno").ToString()
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("errmsg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function TestVcode(ByVal wc As WizardHTTP, ByVal tb As String) As Boolean
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/f/user/json_needvcode?rs1=0&rs10=1&lm=73787&word=" + _
                                                 URLEncoding(tb, Encoding.Default) + "&t=0.8650570850499848")
        Dim need As Integer = JsonMapper.ToObject(retstr).Item("data").Item("need")
        Return need = 1
    End Function

    Public Shared Function TestMask(ByVal wc As WizardHTTP) As Boolean
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/pmc/listmsg?time=1&t=&bawu=0&pn=1")
        Return JsonMapper.ToObject(retstr).Item("data").Item("list").Count <> 0
    End Function

    Public Shared Function GetMember(ByVal wc As WizardHTTP, ByVal kw As String, _
                              ByVal pg As Integer) As List(Of String)
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/f/like/furank?kw=" + _
                                                 URLEncoding(kw, Encoding.Default) + "&pn=" + pg.ToString())
        Dim left As Integer = 0
        Dim list As New List(Of String)
        While True
            left = retstr.IndexOf("username=""", left)
            If left = -1 Then Exit While
            left = retstr.IndexOf(">", left) + 1
            Dim right As Integer = retstr.IndexOf("</", left)
            If right = -1 Then Exit While
            Dim un As String = retstr.Substring(left, right - left)
            list.Add(un)
            left = right
        End While
        Return list
    End Function

    Public Shared Function UploadIcon(ByVal cookie As String, ByVal data As Byte()) As Result
        Dim req As HttpWebRequest = HttpWebRequest.Create("http://c.tieba.baidu.com/c/c/img/portrait")
        req.Method = "POST"
        req.Accept = "*/*"
        req.Headers.Set(HttpRequestHeader.AcceptLanguage, "zh-cn")
        req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)"
        req.ContentType = "multipart/form-data; boundary=----------------------------providence"
        req.Headers.Set(HttpRequestHeader.Cookie, cookie)
        Dim st As Stream = req.GetRequestStream()
        st.Write(data, 0, data.Length)
        st.Close()
        Dim res As HttpWebResponse = req.GetResponse()
        Dim sr As New StreamReader(res.GetResponseStream(), Encoding.Default)
        Dim retstr = sr.ReadToEnd()
        sr.Close()
        res.Close()

        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")
        If errno = "0" Then
            Return New Result(0, "")
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New Result(Convert.ToInt32(errno), errmsg)
        End If
    End Function

    Public Shared Function ClientReply(ByVal wc As WizardHTTP, ByVal type As String, ByVal tb As String, _
                                       ByVal tid As String, ByVal fid As String, ByVal content As String, _
                                       ByVal vcode As String, ByVal vmd5 As String) As LoginResult
        Dim tbs As String = GetTbs(wc)
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim cid As String = GetStampMobile(True)
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=" + type + "&_client_version=1.0.4" + _
                              "&_phone_imei=000000000000000&anonymous=0&content=" + content + "&fid=" + fid + _
                              "&from=baidu_appstore&kw=" + tb + "&net_type=1&tbs=" + tbs + "&tid=" + tid + "&vcode=" + vcode + _
                              "&vcode_md5=" + vmd5
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr = cookie + "&_client_id=" + cid + "&_client_type=" + type + "&_client_version=1.0.4" + _
                  "&_phone_imei=000000000000000&anonymous=0&content=" + URLEncoding(content, Encoding.UTF8) + _
                  "&fid=" + fid + "&from=baidu_appstore&kw=" + URLEncoding(tb, Encoding.UTF8) + "&net_type=1&tbs=" + _
                  tbs + "&tid=" + tid + _
                  "&vcode=" + URLEncoding(vcode, Encoding.UTF8) + "&vcode_md5=" + vmd5 + "&sign=" + sign
        wc.SetDefaultHeader(True)
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/c/post/add", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim errno As String = json.Item("error_code")

        vmd5 = ""
        If errno = "5" Then  '需要验证码
            vmd5 = json.Item("vcode_md5")
        End If

        If errno = "0" Then
            Return New LoginResult(0, "", "", "")
        Else
            Dim errmsg As String = json.Item("error_msg")
            Return New LoginResult(Convert.ToInt32(errno), errmsg, vmd5, "")
        End If
    End Function

    Public Shared Function GetFidAndTB(ByVal wc As WizardHTTP, ByVal tid As String) As String()
        wc.SetDefaultHeader()
        Dim retstr As String = wc.DownloadString("http://tieba.baidu.com/p/" + tid)
        Dim left As Integer = retstr.IndexOf("value=""") + 7
        Dim right As Integer = retstr.IndexOf("""", left)
        Dim tb As String = retstr.Substring(left, right - left)
        left = retstr.IndexOf("fid:'") + 5
        right = retstr.IndexOf("'", left)
        Dim fid As String = retstr.Substring(left, right - left)
        Return New String() {fid, tb}
    End Function

    Public Shared Function GetZeroReplyThread(ByVal wc As WizardHTTP, ByVal tb As String) As String
        wc.SetDefaultHeader()
        Dim cookie As String = wc.Headers(HttpRequestHeader.Cookie).Replace(";", "")
        Dim cid As String = GetStampMobile(True)
        Dim poststr As String = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.7.0" + _
                                "&_phone_imei=000000000000000&data_size=51531&from=tieba&kw=" + tb + "&pn=1&q_type=1&rn=35" + _
                                "&scr_dip=1.5&scr_h=800&scr_w=480&with_group=1"
        Dim sign As String = MD5Encrypt(poststr.Replace("&", "") + "tiebaclient!!!", Encoding.UTF8)
        poststr = cookie + "&_client_id=" + cid + "&_client_type=2&_client_version=5.7.0" + _
                  "&_phone_imei=000000000000000&data_size=51531&from=tieba&kw=" + URLEncoding(tb, Encoding.UTF8) + _
                  "&pn=1&q_type=1&rn=35" + "&scr_dip=1.5&scr_h=800&scr_w=480&with_group=1&sign=" + sign
        Dim retstr As String = wc.UploadString("http://c.tieba.baidu.com/c/f/frs/page", poststr)
        Dim json As JsonData = JsonMapper.ToObject(retstr)
        Dim list As JsonData = json.Item("thread_list")
        For i As Integer = 0 To list.Count - 1
            If CType(list.Item(i).Item("reply_num"), String) = "0" Then
                Return list.Item(i).Item("tid")
            End If
        Next
        Return ""
    End Function
End Class
