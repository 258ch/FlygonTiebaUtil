'v1.0 生成时间 2014.5.5 by 飞龙 - CHI

Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Security.Cryptography

Public Class Utility

    Public Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As Integer, ByVal dwMinimumWorkingSetSize As Integer, ByVal dwMaximumWorkingSetSize As Integer) As Integer

    Public Shared Function URLEncoding(ByVal text As String, ByVal enco As Encoding) As String
        Dim idata() As Byte = enco.GetBytes(text)
        Dim length As Integer = idata.Length - 1
        Dim i As Integer
        Dim sb As New StringBuilder()
        For i = 0 To length
            If (idata(i) >= AscW("A") And idata(i) <= AscW("Z")) Or _
               (idata(i) >= AscW("a") And idata(i) <= AscW("z")) Or _
               (idata(i) >= AscW("0") And idata(i) <= AscW("9")) Or _
               idata(i) = AscW("_") Then
                sb.Append(ChrW(idata(i)))
            Else
                sb.Append("%" + idata(i).ToString("X2"))
            End If
        Next i
        Return sb.ToString()
    End Function
	
	Public Shared Function URLDecoding(ByVal text As String, ByVal enco As Encoding) As String
		Dim list As New List(Of Byte)
        Dim cur As Integer = 0
        Dim re As New Regex("%[\dA-F]{2}")
        Dim rms As MatchCollection = re.Matches(text)
        For Each rm As Match In rms
            Dim nc As Integer = rm.Index
            list.AddRange(enco.GetBytes(text.SubString(cur, nc - cur)))
            Dim val As Integer = Convert.ToByte(text.Substring(nc + 1, 2), 16)
            list.Add(val)
            cur = nc + 3
        Next
        list.AddRange(enco.GetBytes(text.SubString(cur, text.Length - cur)))
        Return enco.GetString(list.ToArray())
    End Function
	
    Public Shared Function UnicodeDeco(ByVal text As String) As String
        Dim sb As New StringBuilder()
        Dim cur As Integer = 0
        Dim re As New Regex("\\u[\da-f]{4}")
        Dim rms As MatchCollection = re.Matches(text)
        For Each rm As Match In rms
            Dim nc As Integer = rm.Index
            sb.Append(text, cur, nc - cur)
            Dim val As Integer = Convert.ToInt32(text.Substring(nc + 2, 4), 16)
            sb.Append(ChrW(val))
            cur = nc + 6
        Next
        sb.Append(text, cur, text.Length - cur)
        Return sb.ToString()
    End Function
	
	Public Shared Function UnicodeEnco(ByVal text As String) As String
        If text = "" Then Return ""
        Dim length As Integer = text.Length - 1
        Dim sb As New StringBuilder()
        For i As Integer = 0 To length
		    Dim tmp as Integer = AscW(text(i))
            If (tmp >= AscW("A") And tmp <= AscW("Z")) Or _
               (tmp >= AscW("a") And tmp <= AscW("z")) Or _
               (tmp >= AscW("0") And tmp <= AscW("9")) Or _
               tmp = AscW("_") Then
                sb.Append(text(i))
            Else
                sb.Append("\u" + tmp.ToString("x4"))
            End If
        Next i
        Return sb.ToString()
    End Function

    Public Shared Function Time() As String
        Return "[" + TimeString + "] "
    End Function

    Public Shared Function MD5Encrypt(ByVal text As String, ByVal enco As Encoding) As String
        Dim input As Byte() = enco.GetBytes(text)
        Dim md5 As New MD5CryptoServiceProvider()
        Dim output As Byte() = md5.ComputeHash(input)
        Dim sb As New StringBuilder()
        For Each x As Byte In output
            sb.AppendFormat("{0:X2}", x)
        Next
        Return sb.ToString()
    End Function

    Public Shared Function GetMid(ByVal text As String, ByVal lefttext As String, ByVal righttext As String, Optional ByVal start As Integer = 1) As String
        Dim left As Integer = text.IndexOf(lefttext, start)
        If left = -1 Then Return ""
        left += lefttext.Length
        Dim right As Integer = text.IndexOf(righttext, left)
        If right = -1 Then Return ""
        Return text.Substring(left, right - left)
    End Function

    Public Shared Function GetStamp() As String
        Dim ts As TimeSpan = DateTime.Now - New DateTime(1970, 1, 1)
        Return Convert.ToInt32(ts.TotalSeconds).ToString()
    End Function

    Public Shared Function TheNext(ByVal i As Integer, ByVal steplen As Integer, ByVal max As Integer) As Integer
        Return (i + steplen) Mod max
    End Function

    Public Shared Function GetStampMobile(Optional ByVal client As Boolean = False) As String
        Dim ts As TimeSpan = DateTime.Now - New DateTime(1970, 1, 1)
        Dim result As String = IIf(client, "wappc_", "wapp_")
        result += Convert.ToInt64(ts.TotalMilliseconds).ToString() + "_" + ts.Milliseconds.ToString("D3")
        Return result
    End Function

    Public Shared Function ToBase64(ByVal text As String) As String
        Return Convert.ToBase64String(Encoding.Default.GetBytes(text))
    End Function
End Class
