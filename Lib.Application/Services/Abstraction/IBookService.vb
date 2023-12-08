Public Interface IBookService
    Function GetAllBooks() As Task(Of List(Of BookDTO))
    Function GetBookById(bookId As Long) As Task(Of BookDTO)
    Function GetBooksByCategory(categoryId As Long) As Task(Of List(Of BookDTO))
    Function GetBooksByAuthor(authorId As Long) As Task(Of List(Of BookDTO))
    Function GetBooksByPublisher(publisherId As Long) As Task(Of List(Of BookDTO))
    Sub AddBook(addBookVM As AddBookVM)
    Sub UpdateBook(vM As UpdateBookVM)
    Sub DeleteBook(bookId As Long)
End Interface
