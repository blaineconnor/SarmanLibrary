Public Class BookDTO
    Public Property Id As Long
    Public Property BookName As String
    Public Property CategoryId As Long
    Public Property Detail As String
    Public Property IsRead As Boolean
    Public Property PublisherId As Long
    Public Property AuthorId As Long

    ' NavProp
    Public Property Publisher As PublisherDTO
    Public Property Author As AuthorDTO
    Public Property Category As CategoryDTO
End Class
