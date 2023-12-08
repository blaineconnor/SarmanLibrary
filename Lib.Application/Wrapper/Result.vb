Public Class Result(Of T As New)
    Public Property Data As T
    Public Property Success As Boolean = True
    Public Property Errors As List(Of String) = New List(Of String)()
End Class
