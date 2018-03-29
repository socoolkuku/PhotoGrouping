Imports ExifLibrary

Public Class ImgBrowser

    Private Const FilePattern = "*.JPG|*.JPEG|*.PNG"

    Private itemQ As Queue(Of ListViewItem) = New Queue(Of ListViewItem)
    Private fileQ As Queue(Of Model.File) = New Queue(Of Model.File)
    Private fileCurrent As Integer = 0
    Private thumbCurrent As Integer = 0
    Private total As Integer = 0
    Private lastIndexPercent = 0

    Private lastSelectedFileIndex As Integer = -1
    Private lastSelectedTagIndex As Integer = -1

    Private logger As Blc.Logger = New Blc.Logger

    Private Sub ImageBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtFolderPath.Text = My.Settings.FolderPath
        Me.LoadFolder()
    End Sub

    Private Sub btnBrowseSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSource.Click
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtFolderPath.Text = Me.fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.LoadFolder()
    End Sub

    Private Sub LoadFolder()
        Try
            Dim folderPath = Me.txtFolderPath.Text
            If Not IO.Directory.Exists(folderPath) Then Throw New ApplicationException("Folder not exist, path: " & folderPath)
            Me.logger.ClearAll()
            My.Settings.FolderPath = folderPath
            My.Settings.Save()
            Me.LoadFolderTree(folderPath)
            Me.LoadTags()
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub tvwFolder_BeforeExpand(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwFolder.BeforeExpand
        For Each subNode In e.Node.Nodes
            Me.UpdateFolderNode(subNode)
        Next
    End Sub

    Private Sub LoadFolderTree(ByVal folderPath As String)
        Me.tvwFolder.Nodes.Clear()
        Dim dir = New IO.DirectoryInfo(folderPath)
        Dim fileCount = Me.GetFiles(dir.FullName).Count
        Dim node = New TreeNode
        If fileCount > 0 Then
            node.Text = String.Format("{0} ({1})", dir.Name, fileCount)
        Else
            node.Text = dir.Name
        End If
        node.Tag = dir.FullName
        Me.UpdateFolderNode(node)
        Me.tvwFolder.Nodes.Add(node)
        node.Expand()
    End Sub

    Private Sub UpdateFolderNode(ByVal node As TreeNode)
        Dim folderPath As String = node.Tag
        node.Nodes.Clear()
        For Each subFolderPath In IO.Directory.GetDirectories(folderPath).OrderBy(Function(o) o)
            Dim dir = New IO.DirectoryInfo(subFolderPath)
            Dim fileCount = Me.GetFiles(dir.FullName).Count
            Dim subNode = New TreeNode
            If fileCount > 0 Then
                subNode.Text = String.Format("{0} ({1})", dir.Name, fileCount)
            Else
                subNode.Text = dir.Name
            End If
            subNode.Tag = dir.FullName
            node.Nodes.Add(subNode)
        Next
    End Sub

    Private Sub LoadImages(ByVal folderPath As String)
        Dim dir = New IO.DirectoryInfo(folderPath)
        Dim imagePathList = New List(Of String)
        For Each filePath In Me.GetFiles(dir.FullName).OrderBy(Function(o) o)
            imagePathList.Add(filePath)
        Next
        Dim fileRs = Blc.File.Search(dir.FullName)
        Me.LoadImages(imagePathList.ToArray, fileRs, "Folder", dir.Name)
    End Sub

    Private Sub LoadImages(ByVal tagR As Model.Tag)
        Dim fileList = New List(Of Model.File)
        Dim imagePathList = New List(Of String)
        Dim invalidList = New List(Of Model.File)
        For Each fileR In Blc.Tag.SearchFiles(tagR).OrderBy(Function(o) o.path).ThenBy(Function(o) o.name)
            Dim imagePath = IO.Path.Combine(fileR.path, fileR.name)
            If IO.File.Exists(imagePath) Then
                fileList.Add(fileR)
                imagePathList.Add(imagePath)
            Else
                invalidList.Add(fileR)
            End If
        Next
        If invalidList.Any Then Blc.Tag.DeassignFiles(tagR, invalidList.ToArray)
        Me.LoadImages(imagePathList.ToArray, fileList.ToArray, "Tag", tagR.name)
    End Sub

    Private Sub LoadImages(ByVal imagePaths As String(), ByVal fileRs As Model.File(), ByVal type As String, ByVal title As String)
        CheckClear()
        If Me.bkwMain.IsBusy Then
            Me.bkwMain.CancelAsync()
            Do
                Threading.Thread.Sleep(500)
                Application.DoEvents()
            Loop While Me.bkwMain.IsBusy Or Me.bkwMain.CancellationPending
        End If
        Me.CheckPoint("LoadImages, start")
        Me.lblMain.Text = String.Format("Loading {0} [{1}]", type, title)
        Me.lblCount.Text = ""
        Me.itemQ.Clear()
        Me.fileQ.Clear()
        Me.lvwMain.Items.Clear()
        Me.imglMain.Images.Clear()
        Me.fileCurrent = 0
        Me.thumbCurrent = 0
        Me.total = imagePaths.Count
        Application.DoEvents()
        Dim itemList = New List(Of ListViewItem)
        Dim mapped = New List(Of Model.File)
        ' imagePathList
        For Each filePath In imagePaths
            ' item
            Dim item = New ListViewItem(New String() {
                                        IO.Path.GetFileName(filePath),
                                        IO.Path.GetDirectoryName(filePath),
                                        0,
                                        0
                                    }, filePath)
            itemList.Add(item)
            Me.itemQ.Enqueue(item)
            If itemList.Count = 1 Then
                Me.lblCount.Text = "1 file"
            ElseIf itemList.Count > 1 Then
                Me.lblCount.Text = itemList.Count & " files"
            Else
                Me.lblCount.Text = "No file"
            End If
            ' fileR + thumb
            Dim fileR = Blc.File.Map(filePath, fileRs)
            If fileR IsNot Nothing Then
                item.SubItems(0).Text = fileR.name
                item.SubItems(1).Text = fileR.path
                item.SubItems(2).Text = fileR.width
                item.SubItems(3).Text = fileR.height
                item.Tag = fileR
                fileQ.Enqueue(fileR)
            End If
        Next
        GC.Collect()
        Me.CheckPoint("LoadImages, add item to list")
        Me.lvwMain.Items.AddRange(itemList.ToArray)
        Me.CheckPoint("LoadImages, start worker")
        Me.bkwMain.RunWorkerAsync()
        Me.lblMain.Text = String.Format("{0} [{1}]", type, title)
        Me.CheckPoint("LoadImages, done")
    End Sub

    Private Function GetImage(ByVal fileR As Model.File) As Image
        Dim thumbR = If(fileR.thumbs.Any, fileR.thumbs.First, Nothing)
        If thumbR IsNot Nothing Then
            Using ms = New IO.MemoryStream(thumbR.image)
                Return Image.FromStream(ms)
            End Using
        End If
        Return Nothing
    End Function

    Private Sub AddThumbToImageList(ByVal fileRs As Model.File())
        Me.SuspendLayout()
        Me.CheckPoint("AddThumbToImageList, fileRs.Count: " & fileRs.Count)
        Me.CheckPoint("AddThumbToImageList, load thumb")
        Dim keyList = New List(Of String)
        Dim imageList = New List(Of Image)
        For Each fileR In fileRs
            Dim thumbR = If(fileR.thumbs.Any, fileR.thumbs.First, Nothing)
            If thumbR IsNot Nothing Then
                Using ms = New IO.MemoryStream(thumbR.image)
                    Dim thumb = Image.FromStream(ms)
                    keyList.Add(IO.Path.Combine(fileR.path, fileR.name))
                    imageList.Add(thumb)
                End Using
            End If
        Next
        Me.CheckPoint("AddThumbToImageList, check key")
        For i = 0 To keyList.Count - 1
            If imglMain.Images.ContainsKey(keyList(i)) Then
                Me.CheckPoint("AddThumbToImageList, remove: " & keyList(i))
                Me.imglMain.Images.RemoveByKey(keyList(i))
                Me.thumbCurrent -= 1
            End If
        Next
        Me.CheckPoint("AddThumbToImageList, set key")
        Dim index = Me.imglMain.Images.Count
        Me.imglMain.Images.AddRange(imageList.ToArray)
        For i = 0 To keyList.Count - 1
            Me.CheckPoint("AddThumbToImageList, set key: " & keyList(i))
            'Me.imglMain.Images.Add(keyList(i), imageList(i))
            Me.imglMain.Images.SetKeyName(index + i, keyList(i))
            Me.thumbCurrent += 1
        Next
        Me.imglMain.Images.AddRange({}) ' handle bug when last addRange not work
        Me.CheckPoint("AddThumbToImageList, done")
        Me.ResumeLayout()
        Application.DoEvents()
        GC.Collect()
    End Sub

    Private Sub bkwMain_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkwMain.DoWork
        Me.CheckPoint("DoWork, start")
        Dim percent = 0
        Dim limit As Integer = 100
        If limit > Me.total Then limit = Me.total
        While (Me.fileQ.Any Or Me.itemQ.Any)
            If Me.bkwMain.CancellationPending Then Return
            ' thumbnail
            While fileQ.Any
                If Me.bkwMain.CancellationPending Then Return
                Dim fileList = New List(Of Model.File)
                While fileQ.Any And fileList.Count < limit
                    fileList.Add(Me.fileQ.Dequeue)
                End While
                If Me.total > 0 Then percent = (Me.fileCurrent + Me.thumbCurrent) * 100 / (Me.total / 2)
                Me.bkwMain.ReportProgress(percent, {fileList.ToArray, Nothing, Nothing})
                Threading.Thread.Sleep(100)
            End While
            ' item
            Dim item As ListViewItem = Nothing
            Dim fileR As Model.File = Nothing
            If Me.itemQ.Any Then
                item = itemQ.Dequeue
                fileR = item.Tag
                Dim filePath = item.ImageKey
                If fileR Is Nothing Then
                    fileR = Blc.File.FindByHash(filePath)
                End If
                If fileR Is Nothing Then
                    fileR = Blc.File.Create(filePath)
                    Blc.File.UpdateThumb(fileR)
                ElseIf Blc.File.Refresh(filePath, fileR) Or fileR.thumbs.Count = 0 Then
                    Blc.File.UpdateThumb(fileR)
                Else
                    item = Nothing
                    fileR = Nothing
                End If
                Me.fileCurrent += 1
            End If
            If fileR IsNot Nothing Then Me.fileQ.Enqueue(fileR)
            If Me.total > 0 Then percent = (Me.fileCurrent + Me.thumbCurrent) * 100 / (Me.total / 2)
            Me.bkwMain.ReportProgress(percent, {Nothing, item, fileR})
            Application.DoEvents()
            GC.Collect()
        End While
        If Me.total > 0 Then percent = (Me.fileCurrent + Me.thumbCurrent) * 100 / (Me.total / 2)
        Me.bkwMain.ReportProgress(percent)
        Me.CheckPoint("DoWork, done")
    End Sub

    Private Sub bkwMain_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bkwMain.ProgressChanged
        Me.CheckPoint("ProgressChanged, start")
        If Me.bkwMain.CancellationPending Then Return
        Dim params As Object() = e.UserState
        Dim readyFileRs As Model.File() = If(params IsNot Nothing, params(0), Nothing)
        Dim item As ListViewItem = If(params IsNot Nothing, params(1), Nothing)
        Dim fileR As Model.File = If(params IsNot Nothing, params(2), Nothing)
        ' thumbnail
        If readyFileRs IsNot Nothing AndAlso readyFileRs.Any Then AddThumbToImageList(readyFileRs)
        ' item
        Me.SuspendLayout()
        If item IsNot Nothing And fileR IsNot Nothing Then
            item.SubItems(0).Text = fileR.name
            item.SubItems(1).Text = fileR.path
            item.SubItems(2).Text = fileR.width
            item.SubItems(3).Text = fileR.height
            item.Tag = fileR
        End If
        Me.ResumeLayout()
        If readyFileRs IsNot Nothing Then Me.CheckPoint("Update, readyFileRs.Count: " & readyFileRs.Count)
        If item IsNot Nothing Then Me.CheckPoint("Update, item.name: " & item.Name)
        If fileR IsNot Nothing Then Me.CheckPoint("Update, fileR.name: " & fileR.name)
        If Me.total > 0 Then Me.pbrFile.Value = Math.Min(Me.fileCurrent * 100 / Me.total, 100)
        If Me.total > 0 Then Me.pbrThumb.Value = Math.Min(Me.thumbCurrent * 100 / Me.total, 100)
        Me.CheckPoint("ProgressChanged, done")
    End Sub

    Private Sub bkwMain_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bkwMain.RunWorkerCompleted
        Me.CheckPoint("RunWorkerCompleted")
        If e.Error IsNot Nothing Then
            MessageBox.Show(e.Error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub lvwMain_ItemSelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvwMain.ItemSelectionChanged
        Me.lastSelectedFileIndex = e.Item.Index
    End Sub

    Private Sub lvwMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvwMain.KeyDown
        If e.KeyCode = Keys.Enter Then Me.ViewImage()
    End Sub

    Private Sub lvwMain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwMain.DoubleClick
        Me.ViewImage()
    End Sub

#Region "Tag"

    Private Class TagListItem
        Private Property tagR As Model.Tag
        Public Sub New(ByVal tagR As Model.Tag)
            Me.tagR = tagR
        End Sub
        Public ReadOnly Property Record As Model.Tag
            Get
                Return Me.tagR
            End Get
        End Property
        Public Overrides Function ToString() As String
            If Me.tagR.count > 0 Then
                Return String.Format("{0} ({1})", Me.tagR.name, Me.tagR.count)
            Else
                Return Me.tagR.name
            End If
        End Function
    End Class

    Private Sub LoadTags()
        Try
            Me.lbxTag.Items.Clear()
            Me.lastSelectedTagIndex = -1
            For Each tagR In (From o In Blc.Tag.SearchAll Order By o.name)
                Me.lbxTag.Items.Add(New TagListItem(tagR))
            Next
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub btnAddTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTag.Click
        Dim name = InputBox("Tag Name:")
        name = Trim(name)
        If name = "" Then Return
        Try
            Dim _tagR = Blc.Tag.Find(name)
            If _tagR IsNot Nothing Then
                MessageBox.Show("Duplicated", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            Dim tagR = Blc.Tag.Create(name)
            Me.lbxTag.Items.Add(New TagListItem(tagR))
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub btnRemoveTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveTag.Click
        If Me.lbxTag.SelectedItems.Count = 0 Then Return
        Dim result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = Windows.Forms.DialogResult.Yes Then
            Try
                Dim items = (From o In Me.lbxTag.SelectedItems).ToArray
                For Each item As TagListItem In items
                    Dim tagR As Model.Tag = item.Record
                    Blc.Tag.Delete(tagR)
                    Me.lbxTag.Items.Remove(item)
                Next
            Catch ex As Exception
                Me.CheckPoint(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnRenameTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenameTag.Click
        If Me.lbxTag.SelectedItems.Count = 0 Then Return
        Dim name = InputBox("New Tag Name:")
        name = Trim(name)
        If name <> "" Then
            Try
                Dim index = Me.lbxTag.SelectedIndices(0)
                Dim item As TagListItem = Me.lbxTag.Items(index)
                Dim tagR = item.Record
                Dim _tagR = Blc.Tag.Find(name)
                If _tagR IsNot Nothing AndAlso tagR.id <> _tagR.id Then
                    MessageBox.Show("Duplicated", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                tagR.name = name
                Blc.Tag.Update(tagR)
                Me.lbxTag.Items(index) = item
            Catch ex As Exception
                Me.CheckPoint(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub lbxTag_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbxTag.DoubleClick
        If Me.lbxTag.SelectedItems.Count = 0 Then Return
        Me.lastSelectedTagIndex = Me.lbxTag.SelectedIndex
        Me.lastSelectedFileIndex = -1
        Dim item As TagListItem = Me.lbxTag.SelectedItem
        Try
            Dim tagR = item.Record
            Me.LoadImages(tagR)
            Me.lbxTag.Items(Me.lbxTag.SelectedIndex) = item   ' refresh
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub lbxTag_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles lbxTag.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.lbxTag.SelectedItems.Count = 0 Then Return
            Me.lastSelectedTagIndex = Me.lbxTag.SelectedIndex
            Me.lastSelectedFileIndex = -1
            Dim item As TagListItem = Me.lbxTag.SelectedItem
            Try
                Dim tagR = item.Record
                Me.LoadImages(tagR)
                Me.lbxTag.Items(Me.lbxTag.SelectedIndex) = item   ' refresh
            Catch ex As Exception
                Me.CheckPoint(ex.ToString)
            End Try
        End If
    End Sub

#End Region

#Region "DragDrop"

    Private Sub lvwMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwMain.MouseDown
        If Me.lvwMain.SelectedItems.Count = 0 Then Return
        If e.Button <> MouseButtons.Left Or e.Clicks <> 1 Then Return ' allow double click
        Dim fileList = New List(Of Model.File)
        For Each item As ListViewItem In Me.lvwMain.SelectedItems
            Dim fileR As Model.File = item.Tag
            If fileR IsNot Nothing Then fileList.Add(fileR)
        Next
        If fileList.Count = 0 Then Return
        Me.lvwMain.DoDragDrop(fileList.ToArray, DragDropEffects.Copy)
    End Sub

    Private Sub lbxTag_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbxTag.DragDrop
        If Me.lbxTag.SelectedItems.Count = 0 Then Return
        If Not e.Data.GetDataPresent("ImageBrowser.Model.File[]") Then Return
        Try
            Dim fileRs As Model.File() = e.Data.GetData("ImageBrowser.Model.File[]")
            If fileRs.Count = 0 Then Return
            For Each index In Me.lbxTag.SelectedIndices
                Dim item = Me.lbxTag.Items(index)
                Blc.Tag.AssignFiles(item.Record, fileRs)
                Me.lbxTag.Items(index) = item
            Next
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub lbxTag_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbxTag.DragEnter
        If Not e.Data.GetDataPresent("ImageBrowser.Model.File[]") Then Return
        If lbxTag.SelectedItems.Count = 0 Then Return
        e.Effect = DragDropEffects.Copy
    End Sub

#End Region

#Region "ImageView"

    Private Sub ViewImage(ByVal index As Integer)
        If index < 0 Or index >= Me.lvwMain.Items.Count Then Return
        ImageView.Init(Me.lvwMain, index)
        ImageView.ShowDialog()
    End Sub

    Private Sub ViewImage()
        If Me.lastSelectedFileIndex = -1 Then Return
        If Me.lvwMain.SelectedIndices.Contains(Me.lastSelectedFileIndex) Then ViewImage(Me.lastSelectedFileIndex)
    End Sub

#End Region

    Private Sub tvwFolder_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwFolder.DoubleClick
        If Me.tvwFolder.SelectedNode Is Nothing Then Return
        Try
            Dim folderPath As String = Me.tvwFolder.SelectedNode.Tag
            Me.LoadImages(folderPath)
        Catch ex As Exception
            Me.CheckPoint(ex.ToString)
        End Try
    End Sub

    Private Sub tvwFolder_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvwFolder.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.tvwFolder.SelectedNode Is Nothing Then Return
            Try
                Dim folderPath As String = Me.tvwFolder.SelectedNode.Tag
                Me.LoadImages(folderPath)
            Catch ex As Exception
                Me.CheckPoint(ex.ToString)
            End Try
        End If
    End Sub

    Public Function GetFiles(ByVal folderPath As String) As String()
        Dim filePaths = New List(Of String)
        Dim MultipleFilters() As String = FilePattern.Split("|")
        For Each FileFilter As String In MultipleFilters
            filePaths.AddRange(IO.Directory.GetFiles(folderPath, FileFilter))
        Next
        Return filePaths.ToArray
    End Function

    Private Sub mnuImage_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnuImage.Opening
        If Me.lvwMain.SelectedItems.Count = 0 Then e.Cancel = True
        Me.mnuImage.Items.Clear()
        Me.mnuImage.Items.Add("View", Nothing, Sub(_sender As Object, _e As System.EventArgs)
                                                   Me.ViewImage()
                                               End Sub)
        If Me.lbxTag.SelectedIndices.Count > 0 And Not Me.lbxTag.SelectedIndices.Contains(Me.lastSelectedTagIndex) Then
            Me.mnuImage.Items.Add("Add to Tag", Nothing, Sub(_sender As Object, _e As System.EventArgs)
                                                             Dim fileList = New List(Of Model.File)
                                                             Dim itemList = New List(Of ListViewItem)
                                                             For Each item As ListViewItem In Me.lvwMain.SelectedItems
                                                                 Dim fileR As Model.File = item.Tag
                                                                 If fileR IsNot Nothing Then
                                                                     fileList.Add(fileR)
                                                                     itemList.Add(item)
                                                                 End If
                                                             Next
                                                             If fileList.Count = 0 Then Return
                                                             Dim fileRs = fileList.ToArray
                                                             For Each index In Me.lbxTag.SelectedIndices
                                                                 Dim item = Me.lbxTag.Items(index)
                                                                 Blc.Tag.AssignFiles(item.Record, fileRs)
                                                                 Me.lbxTag.Items(index) = item
                                                             Next
                                                         End Sub)
        End If
        If Me.lbxTag.SelectedIndices.Contains(Me.lastSelectedTagIndex) Then
            Me.mnuImage.Items.Add("Remove from Tag", Nothing, Sub(_sender As Object, _e As System.EventArgs)
                                                                  Dim fileList = New List(Of Model.File)
                                                                  Dim itemList = New List(Of ListViewItem)
                                                                  For Each item As ListViewItem In Me.lvwMain.SelectedItems
                                                                      Dim fileR As Model.File = item.Tag
                                                                      If fileR IsNot Nothing Then
                                                                          fileList.Add(fileR)
                                                                          itemList.Add(item)
                                                                      End If
                                                                  Next
                                                                  If fileList.Count = 0 Then Return
                                                                  Dim fileRs = fileList.ToArray
                                                                  For Each index In Me.lbxTag.SelectedIndices
                                                                      Dim item = Me.lbxTag.Items(index)
                                                                      Blc.Tag.DeassignFiles(item.Record, fileRs)
                                                                      Me.lbxTag.Items(index) = item
                                                                  Next
                                                                  For Each item In itemList
                                                                      Me.lvwMain.Items.Remove(item)
                                                                  Next
                                                              End Sub)
        End If
        If Me.mnuImage.Items.Count = 0 Then e.Cancel = True
    End Sub

#Region "Index"

    Private Sub btnIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndex.Click
        Dim result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = Windows.Forms.DialogResult.Yes Then
            Try
                Dim folderPath = Me.txtFolderPath.Text
                If Not IO.Directory.Exists(folderPath) Then Throw New ApplicationException("Folder not exist, path: " & folderPath)
                CheckClear()
                If Me.bkwIndex.IsBusy Then
                    Me.bkwIndex.CancelAsync()
                    Do
                        Threading.Thread.Sleep(500)
                        Application.DoEvents()
                    Loop While Me.bkwIndex.IsBusy Or Me.bkwIndex.CancellationPending
                End If
                Me.CheckPoint("Index, start")
                Me.lastIndexPercent = 0
                Me.bkwIndex.RunWorkerAsync(folderPath)
                Me.CheckPoint("Index, done")
            Catch ex As Exception
                Me.CheckPoint(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub bkwIndex_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bkwIndex.DoWork
        Me.CheckPoint("DoWork, start")
        Dim folderPathQ = New Queue(Of String)
        Dim filePathList = New List(Of String)
        Dim folderPath As String = e.Argument
        folderPathQ.Enqueue(folderPath)
        While folderPathQ.Any
            If Me.bkwIndex.CancellationPending Then Return
            folderPath = folderPathQ.Dequeue
            For Each filePath In Me.GetFiles(folderPath).OrderBy(Function(o) o)
                filePathList.Add(filePath)
            Next
            For Each subFolderPath In IO.Directory.GetDirectories(folderPath)
                folderPathQ.Enqueue(subFolderPath)
            Next
        End While
        Me.bkwIndex.ReportProgress(0)
        Me.CheckPoint("DoWork, filePathList.Count: " & filePathList.Count)
        Dim count = 0
        For Each filePath In filePathList
            If Me.bkwIndex.CancellationPending Then Return
            Dim fileR = Blc.File.Find(filePath)
            If fileR Is Nothing Then
                fileR = Blc.File.FindByHash(filePath)
            End If
            If fileR Is Nothing Then
                fileR = Blc.File.Create(filePath)
                Blc.File.UpdateThumb(fileR)
            ElseIf Blc.File.Refresh(filePath, fileR) Or fileR.thumbs.Count = 0 Then
                Blc.File.UpdateThumb(fileR)
            End If
            count += 1
            Me.bkwIndex.ReportProgress(count / filePathList.Count, {count, filePathList.Count})
        Next
        Me.CheckPoint("DoWork, done")
    End Sub

    Private Sub bkwIndex_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bkwIndex.ProgressChanged
        Dim params As Integer() = e.UserState
        Dim count As Integer = If(params IsNot Nothing, params(0), 0)
        Dim total As Integer = If(params IsNot Nothing, params(1), 0)
        If total > 0 Then
            Dim percent = Math.Floor(count / total * 10) * 10
            If percent <> lastIndexPercent Then CheckPoint(String.Format("ProgressChanged: {0} / {1}, {2}%", count, total, percent))
            Me.pbrIndex.Value = percent
            lastIndexPercent = percent
        End If
    End Sub

    Private Sub bkwIndex_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bkwIndex.RunWorkerCompleted
        Me.CheckPoint("RunWorkerCompleted")
    End Sub

#End Region

#Region "Debug"

    Private time As DateTime = DateTime.Now
    Private Sub CheckClear()
        time = DateTime.Now
    End Sub
    Private Sub CheckPoint(ByVal msg)
        Me.logger.Log((DateTime.Now - time).TotalSeconds.ToString("0.00") & ": " & msg)
    End Sub

#End Region

End Class