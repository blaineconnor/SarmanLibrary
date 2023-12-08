Imports System.Linq.Expressions
Imports [Lib].Domain
Imports Microsoft.EntityFrameworkCore

Public Class Repository(Of T As BaseEntity)
    Implements IRepository(Of T)
    Private ReadOnly _dbSet As DbSet(Of T)

    Public Sub New(dbContext As LibContext)
        _dbSet = dbContext.Set(Of T)()
    End Sub

    Public Async Function GetAllAsync(ParamArray includeColumns As String()) As Task(Of IQueryable(Of T)) Implements IRepository(Of T).GetAllAsync
        Dim query = _dbSet.AsQueryable()

        If includeColumns.Any() Then
            For Each includeColumn In includeColumns
                query = query.Include(includeColumn)
            Next
        End If

        Return Await Task.FromResult(query)
    End Function

    Public Async Function GetByFilterAsync(ByVal filter As Expression(Of Func(Of T, Boolean)), ParamArray includeColumns As String()) As Task(Of IQueryable(Of T)) Implements IRepository(Of T).GetByFilterAsync
        Dim query = _dbSet.AsQueryable()

        If includeColumns.Any() Then
            For Each includeColumn In includeColumns
                query = query.Include(includeColumn)
            Next
        End If

        Return Await Task.FromResult(query.Where(filter))
    End Function

    Public Async Function GetSingleByFilterAsync(ByVal filter As Expression(Of Func(Of T, Boolean)), ParamArray includeColumns As String()) As Task(Of T) Implements IRepository(Of T).GetSingleByFilterAsync
        Dim query = _dbSet.AsQueryable()

        If includeColumns.Any() Then
            For Each includeColumn In includeColumns
                query = query.Include(includeColumn)
            Next
        End If

        Return Await query.FirstOrDefaultAsync(filter)
    End Function

    Public Async Function GetById(ByVal id As Object) As Task(Of T) Implements IRepository(Of T).GetById
        Dim entity = Await _dbSet.FindAsync(id)
        Return entity
    End Function

    Public Async Function AnyAsync(ByVal filter As Expression(Of Func(Of T, Boolean))) As Task(Of Boolean) Implements IRepository(Of T).AnyAsync
        Return Await _dbSet.AnyAsync(filter)
    End Function

    Public Sub Add(entity As T) Implements IRepository(Of T).Add
        _dbSet.Add(entity)
    End Sub

    Public Sub Update(entity As T) Implements IRepository(Of T).Update
        _dbSet.Update(entity)
    End Sub

    Public Sub Delete(entity As T) Implements IRepository(Of T).Delete
        _dbSet.Remove(entity)
    End Sub

    Public Sub Delete(id As Object) Implements IRepository(Of T).Delete
        Dim item = _dbSet.Find(id)
        _dbSet.Remove(item)
    End Sub
End Class
