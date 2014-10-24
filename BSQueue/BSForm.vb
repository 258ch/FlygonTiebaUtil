Imports System.Threading

Public Class BSForm

    Private _BS As String() '储存各线程所需要的验证码结果
    Private _BSData As List(Of BSQItem) '验证码队列的实际数据
    Private EnterSub As Boolean = False '是否回车提交

    Public Sub Init(ByVal num As Integer)
        ReDim _BS(num - 1)
        _BSData = New List(Of BSQItem)(num)
        For i As Integer = 0 To num - 1
            _BS(i) = ""
        Next
        BSTextBox.Clear()
        ClearImg()
    End Sub

    Public Sub Add(ByRef bsqi As BSQItem)
        _BSData.Add(bsqi)
    End Sub

    Public Sub ShowBSInvoke()
        Dim mi As New MethodInvoker(AddressOf ShowBS)
        Invoke(mi)
    End Sub

    Public Sub HideInvoke()
        Dim mi As New MethodInvoker(AddressOf Hide)
        Invoke(mi)
    End Sub

    Public ReadOnly Property Count As Integer '已有验证码数
        Get
            Return _BSData.Count
        End Get
    End Property

    Public Property BS(ByVal index As Integer) As String
        Get
            Return _BS(index)
        End Get
        Set(ByVal value As String)
            _BS(index) = value
        End Set
    End Property

    Private Sub ClearImg()
        BSPictureBox1.Image = Nothing
        BSPictureBox2.Image = Nothing
        BSPictureBox3.Image = Nothing
        BSPictureBox4.Image = Nothing
        BSPictureBox5.Image = Nothing
    End Sub

    Private Sub ShowBS()
        ClearImg()
        If _BSData.Count > 0 Then
            BSPictureBox1.Image = _BSData(0).pic
        End If
        If _BSData.Count > 1 Then
            BSPictureBox2.Image = _BSData(1).pic
        End If
        If _BSData.Count > 2 Then
            BSPictureBox3.Image = _BSData(2).pic
        End If
        If _BSData.Count > 3 Then
            BSPictureBox4.Image = _BSData(3).pic
        End If
        If _BSData.Count > 4 Then
            BSPictureBox5.Image = _BSData(4).pic
        End If
    End Sub

    Private Sub BSTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSTextBox.TextChanged
        If BSTextBox.Text.Length = 4 And _BSData.Count <> 0 And EnterSub = False Then
            Dim index As Integer = _BSData(0).index
            BS(index) = BSTextBox.Text
            BSTextBox.Clear()
            _BSData.RemoveAt(0) '刷新
            ShowBS() '显示
        End If
    End Sub

    Private Sub BSTextBox_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles BSTextBox.KeyPress
        If e.KeyChar = Chr(13) And _BSData.Count <> 0 Then
            Dim index As Integer = _BSData(0).index
            If BSTextBox.Text = "" Then : _BS(index) = "quit"
            Else : _BS(index) = BSTextBox.Text : End If
            BSTextBox.Clear()
            _BSData.RemoveAt(0) '刷新
            ShowBS() '显示
        End If
    End Sub

    Private Sub BSForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub TopCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles TopCheckBox.CheckedChanged
        Me.TopMost = TopCheckBox.Checked
    End Sub

    Private Sub SubCheckBox_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles SubCheckBox.CheckedChanged
        EnterSub = SubCheckBox.Checked
    End Sub
End Class

Public Structure BSQItem
    Public index As Integer
    Public pic As Image
End Structure