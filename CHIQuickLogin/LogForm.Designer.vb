<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogForm))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IDTextBox = New System.Windows.Forms.TextBox()
        Me.PWTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LoginButton = New System.Windows.Forms.Button()
        Me.EscButton = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "用户名"
        '
        'IDTextBox
        '
        Me.IDTextBox.Location = New System.Drawing.Point(79, 26)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.Size = New System.Drawing.Size(194, 21)
        Me.IDTextBox.TabIndex = 1
        '
        'PWTextBox
        '
        Me.PWTextBox.Location = New System.Drawing.Point(79, 53)
        Me.PWTextBox.Name = "PWTextBox"
        Me.PWTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PWTextBox.Size = New System.Drawing.Size(194, 21)
        Me.PWTextBox.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "密码"
        '
        'LoginButton
        '
        Me.LoginButton.Location = New System.Drawing.Point(108, 80)
        Me.LoginButton.Name = "LoginButton"
        Me.LoginButton.Size = New System.Drawing.Size(75, 23)
        Me.LoginButton.TabIndex = 4
        Me.LoginButton.Text = "登录"
        Me.LoginButton.UseVisualStyleBackColor = True
        '
        'EscButton
        '
        Me.EscButton.Location = New System.Drawing.Point(198, 80)
        Me.EscButton.Name = "EscButton"
        Me.EscButton.Size = New System.Drawing.Size(75, 23)
        Me.EscButton.TabIndex = 5
        Me.EscButton.Text = "退出"
        Me.EscButton.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.Purple
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(32, 85)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(65, 12)
        Me.LinkLabel1.TabIndex = 6
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "苍海·国际"
        '
        'LogForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(319, 136)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.EscButton)
        Me.Controls.Add(Me.LoginButton)
        Me.Controls.Add(Me.PWTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.IDTextBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LogForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "请登录"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PWTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LoginButton As System.Windows.Forms.Button
    Friend WithEvents EscButton As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
