Imports [Lib].Domain

Public Class UnitofWork
    Implements IUnitofWork

    Private _repositories As Dictionary(Of Type, Object)
    Private ReadOnly _context As LibContext
    Public Sub New(context As LibContext)
        _repositories = New Dictionary(Of Type, Object)()
        _context = context
    End Sub
    Private disposedValue As Boolean

    Public Function GetRepository(Of T As BaseEntity)() As IRepository(Of T) Implements IUnitofWork.GetRepository
        If _repositories.ContainsKey(GetType(IRepository(Of T))) Then
            Return DirectCast(_repositories(GetType(IRepository(Of T))), IRepository(Of T))
        End If

        Dim repository = New Repository(Of T)(_context)
        _repositories.Add(GetType(IRepository(Of T)), repository)
        Return repository
    End Function

    Public Async Function CommitAsync() As Task(Of Boolean) Implements IUnitofWork.CommitAsync
        Dim result As Boolean = False
        Dim rollbackException As Exception = Nothing

        Using transaction = _context.Database.BeginTransaction()
            Try
                Await _context.SaveChangesAsync()
                Await transaction.CommitAsync()
                result = True
            Catch ex As Exception
                rollbackException = ex
            End Try

            If rollbackException IsNot Nothing Then
                Await transaction.RollbackAsync()
                Throw rollbackException
            End If
        End Using

        Return Await Task.FromResult(result)
    End Function


    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If _repositories IsNot Nothing Then
                    _repositories.Clear()
                End If

                If _context IsNot Nothing Then
                    _context.Dispose()
                End If
            End If
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
