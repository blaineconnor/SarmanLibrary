Public Class Book
    Inherits AuditableEntity

    Public Property BookName As String
    Public Property Page As Integer
    Public Property ReleaseDate As Date
    Public Property CategoryId As Long
    Public Property Detail As String
    Public Property IsRead As Boolean
    Public Property PublisherId As Long
    Public Property AuthorId As Long

    ' Navigation properties
    Public Overridable Property Publisher As Publisher
    Public Overridable Property Author As Author
    Public Overridable Property Category As Category
End Class
