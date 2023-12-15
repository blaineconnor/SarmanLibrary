Public Class Author
    Inherits AuditableEntity

    Public Property FirstName As String
    Public Property LastName As String
    Public Property BirthDate As Date
    Public Property CountryId As Long

    'NavProp
    Public Overridable Property Nationality As Country

    Public Overridable Property Books As ICollection(Of Book)

    Public Sub New()
        Books = New HashSet(Of Book)()
    End Sub
End Class
