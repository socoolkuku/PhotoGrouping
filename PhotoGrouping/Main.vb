Imports ExifLibrary

Public Class Main

    Private logList As New List(Of String)
    Private current = 0
    Private total = 0
    Private thShowProgress As New Threading.Timer(Function(state As Object)
                                                      Me.ShowProgress()
                                                  End Function, Nothing, Threading.Timeout.Infinite, Threading.Timeout.Infinite)

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.btnStart.Enabled = Me.ValidateSource And Me.ValidateTarget
        Me.btnCount.Enabled = Me.ValidateSource
    End Sub

    Private Sub txtSource_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSource.TextChanged
        Me.btnStart.Enabled = Me.ValidateSource And Me.ValidateTarget
        Me.btnCount.Enabled = Me.ValidateSource
    End Sub

    Private Sub txtTarget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTarget.TextChanged
        Me.btnStart.Enabled = Me.ValidateSource And Me.ValidateTarget
        Me.btnCount.Enabled = Me.ValidateSource
    End Sub

    Private Function ValidateSource() As Boolean
        Dim folderPath = Me.txtSource.Text
        Me.lblSourceError.Visible = False
        If Not IO.Directory.Exists(folderPath) Then
            Me.lblSourceError.Text = "Folder does not exist."
            Me.lblSourceError.Visible = True
            Return False
        End If
        Return True
    End Function

    Private Function ValidateTarget() As Boolean
        Dim folderPath = Me.txtTarget.Text
        Me.lblTargetError.Visible = False
        If Not IO.Directory.Exists(folderPath) Then
            Me.lblTargetError.Text = "Folder does not exist."
            Me.lblTargetError.Visible = True
            Return False
        End If
        'If IO.Directory.GetFiles(folderPath).Count > 0 Or IO.Directory.GetDirectories(folderPath).Count > 0 Then
        '    Me.lblTargetError.Text = "Folder is not empty."
        '    Me.lblTargetError.Visible = True
        '    Return False
        'End If
        Return True
    End Function

    Private Sub Log(ByVal message As String)
        Me.logList.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " " & message)
        Using s = IO.File.AppendText(DateTime.Now.ToString("yyyy-MM-dd") & ".txt")
            s.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & message)
        End Using
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Me.logList.Clear()
        Me.btnStart.Enabled = False
        Me.current = 0
        Me.total = 0
        If Not Me.ValidateSource Or Not Me.ValidateTarget Then Exit Sub
        Dim currentFile = ""
        Try
            Me.thShowProgress.Change(0, 2000)
            Me.Log("Counting number of files...")
            Dim filePaths = GetFiles(Me.txtSource.Text)
            Me.total = filePaths.Count

            Me.Log("Start processing...")
            For Each filePath In filePaths
                currentFile = filePath
                Dim info = New IO.FileInfo(filePath)
                Dim pathTail = info.DirectoryName
                pathTail = pathTail.Replace(Me.txtSource.Text, "")
                If pathTail.Length > 0 AndAlso GetChar(pathTail, 1) = IO.Path.DirectorySeparatorChar Then pathTail = pathTail.Substring(1)
                
                Try
                    Dim time = info.LastWriteTime
                    Dim path = Me.txtTarget.Text
                    path = IO.Path.Combine(path, DateTime.Today.ToString("yyyyMMdd"))
                    If Not IO.Directory.Exists(path) Then IO.Directory.CreateDirectory(path)
                    If info.Extension.ToUpper = ".CR2" Then
                        path = IO.Path.Combine(path, "RAW")
                        path = IO.Path.Combine(path, time.ToString("yyyy"))
                        'path = IO.Path.Combine(path, time.ToString("yyyyMM"))
                        path = IO.Path.Combine(path, time.ToString("yyyyMMdd"))
                    ElseIf info.Extension.ToUpper = ".JPG" Or info.Extension.ToUpper = ".JPEG" Then
                        Dim exif = ExifFile.Read(filePath)
                        If exif.Properties.ContainsKey(ExifTag.DateTimeOriginal) Then
                            Dim _time = exif.Properties(ExifTag.DateTimeOriginal).Value
                            If time.Year = 1 Then
                                path = IO.Path.Combine(path, "IMG-NoExif")
                            Else
                                time = _time
                                path = IO.Path.Combine(path, "IMG")
                            End If
                        Else
                            path = IO.Path.Combine(path, "IMG-NoExif")
                        End If
                        path = IO.Path.Combine(path, time.ToString("yyyy"))
                        'path = IO.Path.Combine(path, time.ToString("yyyyMM"))
                        path = IO.Path.Combine(path, time.ToString("yyyyMMdd"))
                    ElseIf info.Extension.ToUpper = ".AVI" Or info.Extension.ToUpper = ".MOV" Or info.Extension.ToUpper = ".MP4" Then
                        path = IO.Path.Combine(path, "MOV")
                        path = IO.Path.Combine(path, time.ToString("yyyy"))
                        'path = IO.Path.Combine(path, time.ToString("yyyyMM"))
                        path = IO.Path.Combine(path, time.ToString("yyyyMMdd"))
                    Else
                        path = IO.Path.Combine(path, "OTHERS")
                        path = IO.Path.Combine(path, time.ToString("yyyy"))
                        'path = IO.Path.Combine(path, time.ToString("yyyyMM"))
                        path = IO.Path.Combine(path, time.ToString("yyyyMMdd"))
                    End If
                    Dim targetFilePath = Me.GetTargetPath(path, info.Name, info.Extension)
                    If Me.chkMove.Checked Then
                        info.MoveTo(targetFilePath)
                        Me.Log("Move from [" & filePath & "] to [" & targetFilePath & "]")
                    Else
                        info.CopyTo(targetFilePath)
                        Me.Log("Copy from [" & filePath & "] to [" & targetFilePath & "]")
                    End If
                Catch ex As NotValidJPEGFileException
                    Dim path = IO.Path.Combine(Me.txtTarget.Text, "INVALID")
                    path = IO.Path.Combine(path, pathTail)
                    path = Me.GetTargetPath(path, info.Name, info.Extension)
                    If Me.chkMove.Checked Then
                        info.MoveTo(path)
                    Else
                        info.CopyTo(path)
                    End If
                    Me.Log("Error, file: " & filePath & ", error: " & ex.Message)
                Catch ex As Exception
                    Dim path = IO.Path.Combine(Me.txtTarget.Text, "ERROR")
                    path = IO.Path.Combine(path, pathTail)
                    path = Me.GetTargetPath(path, info.Name, info.Extension)
                    If Me.chkMove.Checked Then
                        info.MoveTo(path)
                    Else
                        info.CopyTo(path)
                    End If
                    Me.Log("Error, file: " & filePath & ", error: " & ex.Message)
                End Try

                Me.current += 1
                Application.DoEvents()
            Next
            Me.Log("Finish processing")
        Catch ex As Exception
            If currentFile = "" Then
                Me.Log("Ex: " & ex.Message & vbCrLf & "Detail: " & ex.ToString)
            Else
                Me.Log("Ex on file [" & currentFile & "]: " & ex.Message & vbCrLf & "Detail: " & ex.ToString)
            End If
        Finally
            Me.thShowProgress.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)
            Me.ShowProgress()
        End Try
        Me.btnStart.Enabled = True
    End Sub

    Private Function GetTargetPath(ByVal folderPath As String, ByVal fileName As String, ByVal extension As String) As String
        If Not IO.Directory.Exists(folderPath) Then IO.Directory.CreateDirectory(folderPath)
        Dim idx = 1
        Dim targetFilePath = IO.Path.Combine(folderPath, fileName)
        While IO.File.Exists(targetFilePath)
            Dim newName = IO.Path.GetFileNameWithoutExtension(fileName) & " (" & idx & ")" & extension
            targetFilePath = IO.Path.Combine(folderPath, newName)
            idx += 1
        End While
        Return targetFilePath
    End Function

    Private Delegate Sub ShowProgressDelegate()
    Private Sub ShowProgress()
        If Me.InvokeRequired Then
            Me.Invoke(New ShowProgressDelegate(AddressOf ShowProgress))
        Else
            Me.lblCount.Text = Me.current & " / " & Me.total
            Me.txtLog.Text = Join(Me.logList.ToArray, vbCrLf) & vbCrLf
            Me.txtLog.SelectionStart = Me.txtLog.Text.Length
            Me.txtLog.ScrollToCaret()
            Me.Refresh()
        End If
    End Sub

    Private Function GetFiles(ByVal folderPath As String) As List(Of String)
        Dim filePaths As New List(Of String)
        For Each subFolderPath In IO.Directory.GetDirectories(folderPath)
            filePaths.AddRange(Me.GetFiles(subFolderPath))
        Next
        For Each filePath In IO.Directory.GetFiles(folderPath)
            filePaths.Add(filePath)
        Next
        Return filePaths
    End Function

    Private Sub btnCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCount.Click
        Me.logList.Clear()
        Me.btnStart.Enabled = False
        Me.current = 0
        Me.total = 0
        If Not Me.ValidateSource Then Exit Sub
        Try
            Me.thShowProgress.Change(0, 2000)
            Me.Log("Counting number of files...")
            Dim filePaths = GetFiles(Me.txtSource.Text)
            Me.total = filePaths.Count
            Me.Log("Finish counting")
        Catch ex As Exception
            Me.Log("Ex: " & ex.Message & vbCrLf & "Detail: " & ex.ToString)
        Finally
            Me.thShowProgress.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)
            Me.ShowProgress()
        End Try
        Me.btnStart.Enabled = True
    End Sub

    Private Sub btnBrowseSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseSource.Click
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtSource.Text = Me.fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub btnBrowseTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseTarget.Click
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.txtTarget.Text = Me.fbdBrowse.SelectedPath
        End If
    End Sub
End Class
