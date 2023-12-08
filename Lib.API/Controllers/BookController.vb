Imports [Lib].Application
Imports Microsoft.AspNetCore.Mvc

<Route("api/[controller]")>
<ApiController>
Public Class BookController
    Inherits ControllerBase

    Private ReadOnly _bookService As IBookService

    Public Sub New(bookService As IBookService)
        _bookService = bookService
    End Sub

    <HttpGet("all")>
    Public Async Function GetAllBooks() As Task(Of ActionResult(Of List(Of BookDTO)))
        Dim books = Await _bookService.GetAllBooks()
        Return Ok(books)
    End Function

    <HttpGet("{bookId}")>
    Public Async Function GetBookById(bookId As Long) As Task(Of ActionResult(Of BookDTO))
        Dim book = Await _bookService.GetBookById(bookId)
        If book IsNot Nothing Then
            Return Ok(book)
        End If
        Return NotFound()
    End Function

    <HttpGet("category/{categoryId}")>
    Public Async Function GetBooksByCategory(categoryId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
        Dim books = Await _bookService.GetBooksByCategory(categoryId)
        Return Ok(books)
    End Function

    <HttpGet("author/{authorId}")>
    Public Async Function GetBooksByAuthor(authorId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
        Dim books = Await _bookService.GetBooksByAuthor(authorId)
        Return Ok(books)
    End Function

    <HttpGet("publisher/{publisherId}")>
    Public Async Function GetBooksByPublisher(publisherId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
        Dim books = Await _bookService.GetBooksByPublisher(publisherId)
        Return Ok(books)
    End Function

    <HttpPost("add")>
    Public Sub AddBook(addBookVM As AddBookVM)
        _bookService.AddBook(addBookVM)
    End Sub

    <HttpPut("update")>
    Public Sub UpdateBook(updateBookVM As UpdateBookVM)
        _bookService.UpdateBook(updateBookVM)
    End Sub

    <HttpDelete("{bookId}")>
    Public Sub DeleteBook(bookId As Long)
        _bookService.DeleteBook(bookId)
    End Sub
End Class
