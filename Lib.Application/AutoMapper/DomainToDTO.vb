Imports AutoMapper
Imports [Lib].Domain

Public Class DomainToDTO
    Inherits Profile
    Public Sub New()
        CreateMap(Of Author, AuthorDTO)()
        CreateMap(Of Book, BookDTO)()
        CreateMap(Of Category, CategoryDTO)()

        CreateMap(Of Publisher, PublisherDTO)()
    End Sub
End Class
