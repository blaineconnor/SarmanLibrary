Public Interface IBookService
    Function GetAllBooks() As Task(Of Result(Of List(Of BookDTO)))
    Function GetBookById(bookId As Long) As Task(Of Result(Of BookDTO))
    Function AddBook(addBookVM As AddBookVM) As Result(Of AddBookVM)
    Function UpdateBook(vM As UpdateBookVM) As Task(Of Result(Of UpdateBookVM))
    Function DeleteBook(bookId As Long) As Result(Of Object)
    Function GetBooksByCategory(categoryId As Long) As Task(Of Result(Of List(Of BookDTO)))
    Function GetBooksByAuthor(authorId As Long) As Task(Of Result(Of List(Of BookDTO)))
    Function GetBooksByPublisher(publisherId As Long) As Task(Of Result(Of List(Of BookDTO)))
End Interface
