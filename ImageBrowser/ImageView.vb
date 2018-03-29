Public Class ImageView

    Private originalSize As Size = New Size(0, 0)
    Private sourceView As ListView
    Private currentIndex As Integer = -1

    Public Sub Init(ByVal sourceView As ListView, ByVal index As Integer)
        Me.sourceView = sourceView
        Me.ViewImage(index)
    End Sub

    Private Sub ViewImage(ByVal index As Integer)
        If index < 0 Or index >= Me.sourceView.Items.Count Then Return
        Me.currentIndex = index
        Dim fileR As Model.File = Me.sourceView.Items(Me.currentIndex).Tag
        If fileR IsNot Nothing Then
            Me.Text = fileR.name
            Me.btnLast.Enabled = index > 0
            Me.btnNext.Enabled = index < Me.sourceView.Items.Count - 1
            Me.imgView.Image = Image.FromFile(IO.Path.Combine(fileR.path, fileR.name))
            If Me.originalSize.Width > 0 And Me.originalSize.Height > 0 Then
                ' not first load
                Me.ResizeByImage()
            End If
        End If
    End Sub

    Private Sub ImageView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.originalSize.Width = 0 Then Me.originalSize.Width = Me.imgView.Size.Width
        If Me.originalSize.Height = 0 Then Me.originalSize.Height = Me.imgView.Size.Height
        Me.ResizeByImage()
    End Sub

    Private Sub ResizeByImage()
        ' resize window
        Dim ow = Me.originalSize.Width
        Dim oh = Me.originalSize.Height
        Dim cw = Me.imgView.Width
        Dim ch = Me.imgView.Height
        Dim w = Me.imgView.Image.Width
        Dim h = Me.imgView.Image.Height
        Dim nw = If(h > w, w / h * oh, ow)
        Dim nh = If(h < w, h / w * ow, oh)
        Me.Size = New Size(nw, nh) + Me.Size - Me.imgView.Size
        Me.imgView.Origin = New Size(Me.imgView.Size.Width / 2, Me.imgView.Size.Height / 2)
        Me.imgView.FitToScreen()
        Me.CenterToScreen()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLast.Click
        ViewImage(Me.currentIndex - 1)
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ViewImage(Me.currentIndex + 1)
    End Sub

    Private Sub ImageView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Left Then ViewImage(Me.currentIndex - 1)
        If e.KeyCode = Keys.Right Then ViewImage(Me.currentIndex + 1)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Select Case keyData
            Case Keys.Up
                ' Return True ' <-- If you want to suppress default handling of arrow keys
            Case Keys.Right
                ViewImage(Me.currentIndex + 1)
                Return True ' <-- If you want to suppress default handling of arrow keys
            Case Keys.Down
                ' Return True ' <-- If you want to suppress default handling of arrow keys
            Case Keys.Left
                ViewImage(Me.currentIndex - 1)
                Return True ' <-- If you want to suppress default handling of arrow keys
        End Select
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ImageView_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.imgView.Origin = New Size(Me.imgView.Size.Width / 2, Me.imgView.Size.Height / 2)
        Me.imgView.FitToScreen()
    End Sub

    Private Sub btnRotateRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotateRight.Click
        Me.imgView.RotateFlip(RotateFlipType.Rotate90FlipNone)
        Me.ResizeByImage()
    End Sub

    Private Sub btnRotateLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotateLeft.Click
        Me.imgView.RotateFlip(RotateFlipType.Rotate270FlipNone)
        Me.ResizeByImage()
    End Sub
End Class