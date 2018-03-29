Namespace Blc

    Public Class Tag

        Public Shared Function Find(name As String) As Model.Tag
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Return (From o In ctx.Tag Where o.name = name And o.active).FirstOrDefault
            End Using
        End Function

        Public Shared Function SearchAll() As Model.Tag()
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Return (From o In ctx.Tag Where o.active).ToArray
            End Using
        End Function

        Public Shared Function SearchFiles(ByVal tagR As Model.Tag) As Model.File()
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Return (From file In ctx.File.Include("thumbs")
                        Where
                        (From tf In ctx.TagFile Where tf.fileId = file.id And tf.tagId = tagR.id).Any And
                        file.active
                        Select file).ToArray
            End Using
        End Function

        Public Shared Sub AssignFiles(ByVal tagR As Model.Tag, ByVal files As Model.File())
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim assignedIds = (From o In ctx.TagFile Where o.tagId = tagR.id Select o.fileId).ToArray
                For Each file In files
                    If Not assignedIds.Contains(file.id) Then
                        Dim tfR = New Model.TagFile
                        tfR.id = Guid.NewGuid
                        tfR.fileId = file.id
                        tfR.tagId = tagR.id
                        ctx.TagFile.AddObject(tfR)
                    End If
                Next
                ctx.SaveChanges()
                ' update count
                ctx.Tag.Attach(tagR)
                tagR.count = (From o In ctx.TagFile Where o.tagId = tagR.id).Count
                ctx.Tag.ApplyCurrentValues(tagR)
                ctx.SaveChanges()
                ctx.Tag.Detach(tagR)
            End Using
        End Sub

        Public Shared Sub DeassignFiles(ByVal tagR As Model.Tag, ByVal files As Model.File())
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim assignedRs = (From o In ctx.TagFile Where o.tagId = tagR.id).ToArray
                For Each file In files
                    Dim tfr = (From o In assignedRs Where o.fileId = file.id).FirstOrDefault
                    If tfr IsNot Nothing Then
                        ctx.TagFile.DeleteObject(tfr)
                    End If
                Next
                ctx.SaveChanges()
                ' update count
                ctx.Tag.Attach(tagR)
                tagR.count = (From o In ctx.TagFile Where o.tagId = tagR.id).Count
                ctx.Tag.ApplyCurrentValues(tagR)
                ctx.SaveChanges()
                ctx.Tag.Detach(tagR)
            End Using
        End Sub

        Public Shared Function Create(ByVal name As String) As Model.Tag
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                Dim tagR = New Model.Tag
                tagR.id = Guid.NewGuid
                tagR.name = name
                tagR.count = 0
                tagR.create = DateTime.Now
                tagR.modify = DateTime.Now
                tagR.active = True
                ctx.Tag.AddObject(tagR)
                ctx.SaveChanges()
                Return tagR
            End Using
        End Function

        Public Shared Sub Update(ByVal tagR As Model.Tag)
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                If (From o In ctx.Tag Where o.id = tagR.id).FirstOrDefault Is Nothing Then
                    Throw New ApplicationException("Tag not found, id: " & tagR.id.ToString)
                End If
                tagR.modify = DateTime.Now
                tagR.active = True
                ctx.Tag.ApplyCurrentValues(tagR)
                ctx.SaveChanges()
            End Using
        End Sub

        Public Shared Sub Delete(ByVal tagR As Model.Tag)
            Using ctx As New Model.Entities
                ctx.ContextOptions.ProxyCreationEnabled = False
                If (From o In ctx.Tag Where o.id = tagR.id).FirstOrDefault Is Nothing Then
                    Throw New ApplicationException("Tag not found, id: " & tagR.id.ToString)
                End If
                tagR.modify = DateTime.Now
                tagR.active = False
                ctx.Tag.ApplyCurrentValues(tagR)
                ctx.SaveChanges()
            End Using
        End Sub

    End Class

End Namespace