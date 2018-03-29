Namespace Blc

    Public Class Logger
        Inherits MarshalByRefObject
        Implements IDisposable

        Private Const LogNumber = 100
        Private _messageList As New List(Of String)
        Private _logFile As String = ""
        Private _logLock As New Object

        Public Sub New()
            Me._logFile = IO.Path.GetDirectoryName(Application.ExecutablePath)
            Me._logFile = IO.Path.Combine(Me._logFile, "Log")
            If Not IO.Directory.Exists(Me._logFile) Then IO.Directory.CreateDirectory(Me._logFile)
        End Sub

        Public Sub Log(ByVal msg As String)
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & vbTab & msg
            SyncLock _logLock
                Dim logFile = IO.Path.Combine(Me._logFile, "Log_" & DateTime.Now.ToString("yyyyMMdd") & ".txt")
                Using sw As New IO.StreamWriter(logFile, True)
                    sw.WriteLine(msg)
                End Using
            End SyncLock
            Me._messageList.Add(msg)
            If _messageList.Count > LogNumber Then
                _messageList.RemoveRange(0, _messageList.Count - LogNumber)
            End If
        End Sub

        Public Sub ClearAll()
            Me._messageList.Clear()
        End Sub

        Public Function SearchAll() As String()
            Return Me._messageList.ToArray
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace