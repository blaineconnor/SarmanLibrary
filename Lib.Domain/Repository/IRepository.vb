Imports System.Linq.Expressions

Public Interface IRepository(Of T As BaseEntity)
    Function GetAllAsync(ParamArray includeColums As String()) As Task(Of IQueryable(Of T))
    Function GetByFilterAsync(ByVal filter As Expression(Of Func(Of T, Boolean)), ParamArray includeColumns As String()) As Task(Of IQueryable(Of T))
    Function GetSingleByFilterAsync(ByVal filter As Expression(Of Func(Of T, Boolean)), ParamArray includeColumns As String()) As Task(Of T)
    Function AnyAsync(ByVal filter As Expression(Of Func(Of T, Boolean))) As Task(Of Boolean)
    Function GetById(ByVal id As Object) As Task(Of T)
    Sub Add(ByVal entity As T)
    Sub Update(ByVal entity As T)
    Sub Delete(ByVal entity As T)
    Sub Delete(ByVal id As Object)
End Interface
