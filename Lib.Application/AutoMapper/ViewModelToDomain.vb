Imports AutoMapper
Imports [Lib].Application
Imports [Lib].Domain

Public Class ViewModelToDomain
    Inherits Profile
    Public Sub New()
        'Author
        CreateMap(Of AddAuthorVM, Author)()
        CreateMap(Of UpdateAuthorVM, Author)()


        'Book
        CreateMap(Of AddBookVM, Book)()
        CreateMap(Of UpdateBookVM, Book)()


        'Category
        CreateMap(Of CreateCategoryVM, Category)()
        CreateMap(Of UpdateCategoryVM, Category)()


        'Publisher
        CreateMap(Of AddPublisherVM, Publisher)()
        CreateMap(Of UpdatePublisherVM, Publisher)()
    End Sub
End Class
