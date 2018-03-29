Imports ExifLibrary

Namespace Blc

    Public Class File

        Private Const ThumbW = 100
        Private Const ThumbH = 100

        Public Shared Function Search(ByVal folderPath As String) As Model.File()
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Return (From o In ctx.File.Include("thumbs") Where o.path = folderPath And o.active).ToArray
            End Using
        End Function

        Public Shared Function Find(ByVal filePath As String) As Model.File
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim fileName = IO.Path.GetFileName(filePath)
                Dim folderPath = IO.Path.GetDirectoryName(filePath)
                Return (From o In ctx.File.Include("thumbs") Where o.name = fileName And o.path = folderPath And o.active).FirstOrDefault
            End Using
        End Function

        Public Shared Function FindByHash(ByVal filePath As String) As Model.File
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim hash = CreateHash(filePath)
                For Each fileR In (From o In ctx.File.Include("thumbs") Where o.hash = hash And o.active)
                    If Not IO.File.Exists(IO.Path.Combine(fileR.path, fileR.name)) Then Return fileR
                Next
                Return Nothing
            End Using
        End Function

        Public Shared Function Map(ByVal filePath As String, ByVal fileRs As Model.File()) As Model.File
            Dim fileName = IO.Path.GetFileName(filePath)
            Dim folderPath = IO.Path.GetDirectoryName(filePath)
            Return (From o In fileRs Where o.name = fileName And o.path = folderPath And o.active).FirstOrDefault
        End Function

        Public Shared Function Create(ByVal filePath As String) As Model.File
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim fileR = New Model.File
                fileR.id = Guid.NewGuid
                fileR.create = DateTime.Now
                If InternalRefresh(filePath, fileR) Then
                    ctx.File.AddObject(fileR)
                    ctx.SaveChanges()
                    Return fileR
                End If
                Return Nothing
            End Using
        End Function

        Public Shared Function Refresh(ByVal filePath As String, ByVal fileR As Model.File) As Boolean
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                If (From o In ctx.File.Include("thumbs") Where o.id = fileR.id And o.active).FirstOrDefault Is Nothing Then
                    Throw New ApplicationException("File not found, id: " & fileR.id.ToString)
                End If
                If InternalRefresh(filePath, fileR) Then
                    ctx.File.ApplyCurrentValues(fileR)
                    ctx.SaveChanges()
                    Return True
                End If
                Return False
            End Using
        End Function

        Private Shared Function InternalRefresh(ByVal filePath As String, ByVal fileR As Model.File) As Boolean
            Dim hash = CreateHash(filePath)
            Dim fileName = IO.Path.GetFileName(filePath)
            Dim folderPath = IO.Path.GetDirectoryName(filePath)
            Dim ext = IO.Path.GetExtension(filePath).ToUpper
            If fileName <> fileR.name Or folderPath <> fileR.path Or hash <> fileR.hash Then
                Using img = Image.FromFile(filePath)
                    fileR.name = IO.Path.GetFileName(filePath)
                    fileR.path = IO.Path.GetDirectoryName(filePath)
                    If ext = ".JPG" Or ext = ".JPEG" Then
                        Dim exif = ExifFile.Read(filePath)
                        fileR.width = If(exif.Properties.ContainsKey(ExifTag.ImageWidth), exif.Properties(ExifTag.ImageWidth).Value, img.Width)
                        fileR.height = If(exif.Properties.ContainsKey(ExifTag.ImageLength), exif.Properties(ExifTag.ImageLength).Value, img.Height)
                        Dim orientation As Orientation = If(exif.Properties.ContainsKey(ExifTag.Orientation), exif.Properties(ExifTag.Orientation).Value, orientation.Normal)
                        fileR.orientation = orientation.ToString
                    Else
                        fileR.width = img.Width
                        fileR.height = img.Height
                        fileR.orientation = Orientation.Normal.ToString
                    End If
                    fileR.hash = hash
                    fileR.modify = DateTime.Now
                    fileR.active = True
                End Using
                Return True
            End If
            Return False
        End Function

        Public Shared Sub UpdateThumb(ByVal fileR As Model.File)
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                If Not (From o In ctx.File.Include("thumbs") Where o.id = fileR.id And o.active).Any Then
                    Throw New ApplicationException("File not found, id: " & fileR.id.ToString)
                End If
                If fileR Is Nothing Then Throw New ApplicationException("File not found, id: " & fileR.id.ToString)
                Dim filePath = IO.Path.Combine(fileR.path, fileR.name)
                Dim thumbR As Model.Thumb = Nothing
                Dim create = False
                ctx.File.Attach(fileR)
                If Not (From o In fileR.thumbs Where o.active).Any Then
                    thumbR = New Model.Thumb
                    thumbR.id = Guid.NewGuid
                    thumbR.fileId = fileR.id
                    thumbR.image = (New ImageConverter).ConvertTo(GetThumb(filePath), GetType(Byte()))
                    thumbR.create = DateTime.Now
                    thumbR.modify = DateTime.Now
                    thumbR.active = True
                    fileR.thumbs.Add(thumbR)
                    ctx.File.ApplyCurrentValues(fileR)
                Else
                    thumbR = (From o In fileR.thumbs Where o.active).First
                    thumbR.image = (New ImageConverter).ConvertTo(GetThumb(filePath), GetType(Byte()))
                    thumbR.modify = DateTime.Now
                    thumbR.active = True
                    ctx.Thumb.ApplyCurrentValues(thumbR)
                End If
                ctx.SaveChanges()
            End Using
        End Sub

        Private Shared Function CreateHash(ByVal filePath As String) As String
            Dim file = New IO.FileInfo(filePath)
            Using fs = New IO.FileStream(filePath, IO.FileMode.Open, IO.FileAccess.Read)
                Dim provider = New Security.Cryptography.MD5CryptoServiceProvider
                Dim bytes = provider.ComputeHash(fs)
                Return Convert.ToBase64String(bytes)
            End Using
        End Function

        Private Shared Function GetThumb(ByVal filePath As String) As Image
            Dim thumb As Bitmap = New Bitmap(ThumbW, ThumbH)
            Using gr = Graphics.FromImage(thumb)
                Using img = Image.FromFile(filePath)
                    Dim ratio = CDbl(img.Width / img.Height)
                    Dim w = ThumbW
                    Dim h = ThumbH
                    Dim x = 0
                    Dim y = 0
                    If ratio > 1 Then
                        w = ThumbH * ratio
                        x = (ThumbW - w) / 2
                    ElseIf ratio < 1 Then
                        h = ThumbW / ratio
                        y = (ThumbH - h) / 2
                    End If
                    gr.DrawImage(img, x, y, w, h)
                End Using
            End Using
            Return thumb
        End Function
    End Class

End Namespace