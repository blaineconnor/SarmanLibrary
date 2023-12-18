Public Interface IAuthorService
    Function GetAllAuthors() As Task(Of Result(Of List(Of AuthorDTO)))
    Function GetAuthorById(authorId As Long) As Task(Of Result(Of AuthorDTO))
    Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of AddAuthorVM)
    Function UpdateAuthor(updateAuthorVM As UpdateAuthorVM) As Result(Of UpdateAuthorVM)
    Function DeleteAuthor(authorId As Long) As Result(Of Object)
End Interface
