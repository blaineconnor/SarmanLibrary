Public Class Author
    Inherits AuditableEntity

    Public Property FirstName As String
    Public Property LastName As String
    Public Property Country As String
    Public Property BirthDate As Integer

    'NavProp
    Public Overridable Property Year As Year
    Public Overridable Property Nationality As Country

    Public Overridable Property Books As ICollection(Of Book)

    Public Sub New()
        Books = New HashSet(Of Book)()
    End Sub
End Class
