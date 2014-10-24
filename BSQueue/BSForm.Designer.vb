<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BSForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BSForm))
        Me.BSTextBox = New System.Windows.Forms.TextBox()
        Me.BSPictureBox1 = New System.Windows.Forms.PictureBox()
        Me.BSPictureBox2 = New System.Windows.Forms.PictureBox()
        Me.BSPictureBox3 = New System.Windows.Forms.PictureBox()
        Me.BSPictureBox4 = New System.Windows.Forms.PictureBox()
        Me.BSPictureBox5 = New System.Windows.Forms.PictureBox()
        Me.TopCheckBox = New System.Windows.Forms.CheckBox()
        Me.SubCheckBox = New System.Windows.Forms.CheckBox()
        CType(Me.BSPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BSPictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BSPictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BSPictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BSPictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BSTextBox
        '
        Me.BSTextBox.Location = New System.Drawing.Point(24, 24)
        Me.BSTextBox.Name = "BSTextBox"
        Me.BSTextBox.Size = New System.Drawing.Size(121, 21)
        Me.BSTextBox.TabIndex = 0
        '
        'BSPictureBox1
        '
        Me.BSPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BSPictureBox1.Location = New System.Drawing.Point(24, 63)
        Me.BSPictureBox1.Name = "BSPictureBox1"
        Me.BSPictureBox1.Size = New System.Drawing.Size(121, 50)
        Me.BSPictureBox1.TabIndex = 1
        Me.BSPictureBox1.TabStop = False
        '
        'BSPictureBox2
        '
        Me.BSPictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BSPictureBox2.Location = New System.Drawing.Point(24, 119)
        Me.BSPictureBox2.Name = "BSPictureBox2"
        Me.BSPictureBox2.Size = New System.Drawing.Size(121, 50)
        Me.BSPictureBox2.TabIndex = 2
        Me.BSPictureBox2.TabStop = False
        '
        'BSPictureBox3
        '
        Me.BSPictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BSPictureBox3.Location = New System.Drawing.Point(24, 175)
        Me.BSPictureBox3.Name = "BSPictureBox3"
        Me.BSPictureBox3.Size = New System.Drawing.Size(121, 50)
        Me.BSPictureBox3.TabIndex = 3
        Me.BSPictureBox3.TabStop = False
        '
        'BSPictureBox4
        '
        Me.BSPictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BSPictureBox4.Location = New System.Drawing.Point(24, 231)
        Me.BSPictureBox4.Name = "BSPictureBox4"
        Me.BSPictureBox4.Size = New System.Drawing.Size(121, 50)
        Me.BSPictureBox4.TabIndex = 4
        Me.BSPictureBox4.TabStop = False
        '
        'BSPictureBox5
        '
        Me.BSPictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BSPictureBox5.Location = New System.Drawing.Point(24, 287)
        Me.BSPictureBox5.Name = "BSPictureBox5"
        Me.BSPictureBox5.Size = New System.Drawing.Size(121, 50)
        Me.BSPictureBox5.TabIndex = 5
        Me.BSPictureBox5.TabStop = False
        '
        'TopCheckBox
        '
        Me.TopCheckBox.AutoSize = True
        Me.TopCheckBox.Location = New System.Drawing.Point(24, 354)
        Me.TopCheckBox.Name = "TopCheckBox"
        Me.TopCheckBox.Size = New System.Drawing.Size(72, 16)
        Me.TopCheckBox.TabIndex = 6
        Me.TopCheckBox.Text = "总在最前"
        Me.TopCheckBox.UseVisualStyleBackColor = True
        '
        'SubCheckBox
        '
        Me.SubCheckBox.AutoSize = True
        Me.SubCheckBox.Location = New System.Drawing.Point(24, 376)
        Me.SubCheckBox.Name = "SubCheckBox"
        Me.SubCheckBox.Size = New System.Drawing.Size(72, 16)
        Me.SubCheckBox.TabIndex = 7
        Me.SubCheckBox.Text = "回车提交"
        Me.SubCheckBox.UseVisualStyleBackColor = True
        '
        'BSForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(168, 406)
        Me.ControlBox = False
        Me.Controls.Add(Me.SubCheckBox)
        Me.Controls.Add(Me.TopCheckBox)
        Me.Controls.Add(Me.BSPictureBox5)
        Me.Controls.Add(Me.BSPictureBox4)
        Me.Controls.Add(Me.BSPictureBox3)
        Me.Controls.Add(Me.BSPictureBox2)
        Me.Controls.Add(Me.BSPictureBox1)
        Me.Controls.Add(Me.BSTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "BSForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "验证码队列"
        CType(Me.BSPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BSPictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BSPictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BSPictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BSPictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents BSPictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BSPictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents BSPictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents BSPictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents BSPictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents TopCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SubCheckBox As System.Windows.Forms.CheckBox
End Class
