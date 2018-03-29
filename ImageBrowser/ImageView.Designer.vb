<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImageView))
        Me.imgView = New Queens_ImageControl.ImageControl()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRotateRight = New System.Windows.Forms.Button()
        Me.btnRotateLeft = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'imgView
        '
        Me.imgView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.imgView.Image = Nothing
        Me.imgView.initialimage = Nothing
        Me.imgView.Location = New System.Drawing.Point(12, 12)
        Me.imgView.Name = "imgView"
        Me.imgView.Origin = New System.Drawing.Point(0, 0)
        Me.imgView.PanButton = System.Windows.Forms.MouseButtons.Left
        Me.imgView.PanMode = True
        Me.imgView.ScrollbarsVisible = True
        Me.imgView.Size = New System.Drawing.Size(760, 685)
        Me.imgView.StretchImageToFit = False
        Me.imgView.TabIndex = 0
        Me.imgView.ZoomFactor = 1.0R
        Me.imgView.ZoomOnMouseWheel = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = Global.ImageBrowser.My.Resources.Resources.delete
        Me.btnClose.Location = New System.Drawing.Point(724, 703)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(48, 48)
        Me.btnClose.TabIndex = 1
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRotateRight
        '
        Me.btnRotateRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRotateRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRotateRight.Image = Global.ImageBrowser.My.Resources.Resources.rotate_right
        Me.btnRotateRight.Location = New System.Drawing.Point(200, 703)
        Me.btnRotateRight.Name = "btnRotateRight"
        Me.btnRotateRight.Size = New System.Drawing.Size(48, 48)
        Me.btnRotateRight.TabIndex = 5
        Me.btnRotateRight.UseVisualStyleBackColor = True
        '
        'btnRotateLeft
        '
        Me.btnRotateLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRotateLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRotateLeft.Image = Global.ImageBrowser.My.Resources.Resources.rotate_left
        Me.btnRotateLeft.Location = New System.Drawing.Point(146, 703)
        Me.btnRotateLeft.Name = "btnRotateLeft"
        Me.btnRotateLeft.Size = New System.Drawing.Size(48, 48)
        Me.btnRotateLeft.TabIndex = 4
        Me.btnRotateLeft.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Image = Global.ImageBrowser.My.Resources.Resources.arrow_right
        Me.btnNext.Location = New System.Drawing.Point(66, 703)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(48, 48)
        Me.btnNext.TabIndex = 3
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnLast
        '
        Me.btnLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLast.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLast.Image = Global.ImageBrowser.My.Resources.Resources.arrow_left
        Me.btnLast.Location = New System.Drawing.Point(12, 703)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(48, 48)
        Me.btnLast.TabIndex = 2
        Me.btnLast.UseVisualStyleBackColor = True
        '
        'ImageView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(784, 761)
        Me.Controls.Add(Me.btnRotateRight)
        Me.Controls.Add(Me.btnRotateLeft)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnLast)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.imgView)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "ImageView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ImageView"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents imgView As Queens_ImageControl.ImageControl
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnRotateLeft As System.Windows.Forms.Button
    Friend WithEvents btnRotateRight As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
End Class
