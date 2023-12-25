Public Interface IAuthorService
    Function GetAllAuthors() As Task(Of Result(Of List(Of AuthorDTO)))
    Function GetAuthorById(authorId As Long) As Task(Of Result(Of AuthorDTO))
    Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of Integer)
    Function UpdateAuthor(updateAuthorVM As UpdateAuthorVM) As Result(Of Integer)
    Function DeleteAuthor(aurhorId As Long) As Result(Of Integer)
End Interface
