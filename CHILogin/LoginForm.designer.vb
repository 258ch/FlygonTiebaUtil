<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UNTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PWTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.QuesComboBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AnsTextBox = New System.Windows.Forms.TextBox()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.LoginButton = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PrTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "用户名"
        '
        'UNTextBox
        '
        Me.UNTextBox.Location = New System.Drawing.Point(65, 22)
        Me.UNTextBox.Name = "UNTextBox"
        Me.UNTextBox.Size = New System.Drawing.Size(100, 21)
        Me.UNTextBox.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(171, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "密码"
        '
        'PWTextBox
        '
        Me.PWTextBox.Location = New System.Drawing.Point(218, 22)
        Me.PWTextBox.Name = "PWTextBox"
        Me.PWTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PWTextBox.Size = New System.Drawing.Size(100, 21)
        Me.PWTextBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "问题"
        '
        'QuesComboBox
        '
        Me.QuesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.QuesComboBox.FormattingEnabled = True
        Me.QuesComboBox.Items.AddRange(New Object() {"安全提问", "母亲的名字", "爷爷的名字", "父亲出生的生日", "您其中一位老师的名字", "您个人计算机的型号", "您最喜欢的餐馆名称", "驾驶执照最后四位数字"})
        Me.QuesComboBox.Location = New System.Drawing.Point(65, 49)
        Me.QuesComboBox.Name = "QuesComboBox"
        Me.QuesComboBox.Size = New System.Drawing.Size(253, 20)
        Me.QuesComboBox.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "答案"
        '
        'AnsTextBox
        '
        Me.AnsTextBox.Location = New System.Drawing.Point(65, 75)
        Me.AnsTextBox.Name = "AnsTextBox"
        Me.AnsTextBox.Size = New System.Drawing.Size(253, 21)
        Me.AnsTextBox.TabIndex = 7
        '
        'ExitButton
        '
        Me.ExitButton.Location = New System.Drawing.Point(243, 129)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(75, 23)
        Me.ExitButton.TabIndex = 8
        Me.ExitButton.Text = "退出"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'LoginButton
        '
        Me.LoginButton.Location = New System.Drawing.Point(144, 129)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(75, 23)
        Me.LoginButton.TabIndex = 9
        Me.LoginButton.Text = "登录"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.Purple
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(18, 134)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(65, 12)
        Me.LinkLabel1.TabIndex = 10
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "苍海·国际"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "代理"
        '
        'PrTextBox
        '
        Me.PrTextBox.Location = New System.Drawing.Point(65, 102)
        Me.PrTextBox.Name = "PrTextBox"
        Me.PrTextBox.Size = New System.Drawing.Size(253, 21)
        Me.PrTextBox.TabIndex = 12
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 169)
        Me.Controls.Add(Me.PrTextBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.LoginButton)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.AnsTextBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.QuesComboBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PWTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.UNTextBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LoginForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "请登录"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UNTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PWTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents QuesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AnsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExitButton As System.Windows.Forms.Button
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PrTextBox As System.Windows.Forms.TextBox
End Class
