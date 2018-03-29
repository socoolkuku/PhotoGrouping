<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTarget = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSourceError = New System.Windows.Forms.Label()
        Me.lblTargetError = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.btnCount = New System.Windows.Forms.Button()
        Me.chkMove = New System.Windows.Forms.CheckBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnBrowseSource = New System.Windows.Forms.Button()
        Me.btnBrowseTarget = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(12, 25)
        Me.txtSource.Name = "txtSource"
        Me.txtSource.Size = New System.Drawing.Size(142, 22)
        Me.txtSource.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Source:"
        '
        'txtTarget
        '
        Me.txtTarget.Location = New System.Drawing.Point(12, 76)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(142, 22)
        Me.txtTarget.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Target:"
        '
        'lblSourceError
        '
        Me.lblSourceError.AutoSize = True
        Me.lblSourceError.ForeColor = System.Drawing.Color.Red
        Me.lblSourceError.Location = New System.Drawing.Point(206, 29)
        Me.lblSourceError.Name = "lblSourceError"
        Me.lblSourceError.Size = New System.Drawing.Size(30, 12)
        Me.lblSourceError.TabIndex = 1
        Me.lblSourceError.Text = "Error"
        Me.lblSourceError.Visible = False
        '
        'lblTargetError
        '
        Me.lblTargetError.AutoSize = True
        Me.lblTargetError.ForeColor = System.Drawing.Color.Red
        Me.lblTargetError.Location = New System.Drawing.Point(206, 81)
        Me.lblTargetError.Name = "lblTargetError"
        Me.lblTargetError.Size = New System.Drawing.Size(30, 12)
        Me.lblTargetError.TabIndex = 1
        Me.lblTargetError.Text = "Error"
        Me.lblTargetError.Visible = False
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(12, 104)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 2
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLog.Location = New System.Drawing.Point(13, 133)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(463, 190)
        Me.txtLog.TabIndex = 4
        Me.txtLog.WordWrap = False
        '
        'lblCount
        '
        Me.lblCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount.Location = New System.Drawing.Point(336, 109)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(140, 17)
        Me.lblCount.TabIndex = 5
        Me.lblCount.Text = "0 / 0"
        Me.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnCount
        '
        Me.btnCount.Location = New System.Drawing.Point(93, 104)
        Me.btnCount.Name = "btnCount"
        Me.btnCount.Size = New System.Drawing.Size(75, 23)
        Me.btnCount.TabIndex = 3
        Me.btnCount.Text = "Count"
        Me.btnCount.UseVisualStyleBackColor = True
        '
        'chkMove
        '
        Me.chkMove.AutoSize = True
        Me.chkMove.Location = New System.Drawing.Point(174, 108)
        Me.chkMove.Name = "chkMove"
        Me.chkMove.Size = New System.Drawing.Size(51, 16)
        Me.chkMove.TabIndex = 6
        Me.chkMove.Text = "Move"
        Me.chkMove.UseVisualStyleBackColor = True
        '
        'btnBrowseSource
        '
        Me.btnBrowseSource.Location = New System.Drawing.Point(160, 24)
        Me.btnBrowseSource.Name = "btnBrowseSource"
        Me.btnBrowseSource.Size = New System.Drawing.Size(40, 23)
        Me.btnBrowseSource.TabIndex = 7
        Me.btnBrowseSource.Text = "..."
        Me.btnBrowseSource.UseVisualStyleBackColor = True
        '
        'btnBrowseTarget
        '
        Me.btnBrowseTarget.Location = New System.Drawing.Point(160, 76)
        Me.btnBrowseTarget.Name = "btnBrowseTarget"
        Me.btnBrowseTarget.Size = New System.Drawing.Size(40, 23)
        Me.btnBrowseTarget.TabIndex = 7
        Me.btnBrowseTarget.Text = "..."
        Me.btnBrowseTarget.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 334)
        Me.Controls.Add(Me.btnBrowseTarget)
        Me.Controls.Add(Me.btnBrowseSource)
        Me.Controls.Add(Me.chkMove)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.btnCount)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblTargetError)
        Me.Controls.Add(Me.lblSourceError)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTarget)
        Me.Controls.Add(Me.txtSource)
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSourceError As System.Windows.Forms.Label
    Friend WithEvents lblTargetError As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents btnCount As System.Windows.Forms.Button
    Friend WithEvents chkMove As System.Windows.Forms.CheckBox
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnBrowseSource As System.Windows.Forms.Button
    Friend WithEvents btnBrowseTarget As System.Windows.Forms.Button

End Class
