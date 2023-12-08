Public Interface IAuthorService
    Function GetAllAuthors() As Task(Of List(Of AuthorDTO))
    Function GetAuthorById(authorId As Long) As Task(Of AuthorDTO)
    Sub AddAuthor(addAuthorVM As AddAuthorVM)
    Sub UpdateAuthor(updateAuthorVM As UpdateAuthorVM)
    Sub DeleteAuthor(authorId As Long)
End Interface
