Imports [Lib].Application
Imports Microsoft.AspNetCore.Mvc

<Route("api/[controller]")>
<ApiController>
Public Class AuthorController
    Inherits ControllerBase

    Private ReadOnly _authorService As IAuthorService

    Public Sub New(authorService As IAuthorService)
        _authorService = authorService
    End Sub

    <HttpGet("all")>
    Public Async Function GetAllAuthors() As Task(Of ActionResult(Of List(Of AuthorDTO)))
        Dim authors = Await _authorService.GetAllAuthors()
        Return Ok(authors)
    End Function

    <HttpGet("{authorId}")>
    Public Async Function GetAuthorById(authorId As Long) As Task(Of ActionResult(Of AuthorDTO))
        Dim author = Await _authorService.GetAuthorById(authorId)
        If author IsNot Nothing Then
            Return Ok(author)
        End If
        Return NotFound()
    End Function

    <HttpPost("add")>
    Public Sub AddAuthor(addAuthorVM As AddAuthorVM)
        _authorService.AddAuthor(addAuthorVM)
    End Sub

    <HttpPut("update")>
    Public Sub UpdateAuthor(updateAuthorVM As UpdateAuthorVM)
        _authorService.UpdateAuthor(updateAuthorVM)
    End Sub

    <HttpDelete("{authorId}")>
    Public Sub DeleteAuthor(authorId As Long)
        _authorService.DeleteAuthor(authorId)
    End Sub
End Class
