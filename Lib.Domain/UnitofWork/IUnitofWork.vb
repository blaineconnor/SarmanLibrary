Public Interface IUnitofWork
    Inherits IDisposable

    Function GetRepository(Of T As BaseEntity)() As IRepository(Of T)
    Function CommitAsync() As Task(Of Boolean)
End Interface
