Imports [Lib].Domain

Public Class AuthorService
    Implements IAuthorService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllAuthors() As Task(Of List(Of AuthorDTO)) Implements IAuthorService.GetAllAuthors
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim authors = Await authorRepository.GetAllAsync()

        Return authors.Select(Function(author) New AuthorDTO With {
            .Id = author.Id,
            .FirstName = author.FirstName,
            .LastName = author.LastName,
            .BirthDate = author.BirthDate,
            .Nationality = author.Nationality
        }).ToList()
    End Function

    Public Async Function GetAuthorById(authorId As Long) As Task(Of AuthorDTO) Implements IAuthorService.GetAuthorById
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim author = Await authorRepository.GetById(authorId)

        If author IsNot Nothing Then
            Return New AuthorDTO With {
                .Id = author.Id,
                .FirstName = author.FirstName,
                .LastName = author.LastName,
                .BirthDate = author.BirthDate,
                .Nationality = author.Nationality
            }
        End If

        Return Nothing
    End Function

    Public Sub AddAuthor(addAuthorVM As AddAuthorVM) Implements IAuthorService.AddAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim newAuthor As New Author With {
            .FirstName = addAuthorVM.FirstName,
            .LastName = addAuthorVM.LastName,
            .BirthDate = addAuthorVM.BirthDate,
            .Nationality = addAuthorVM.Nationality
        }

        authorRepository.Add(newAuthor)
        _unitOfWork.CommitAsync().Wait()
    End Sub

    Public Sub UpdateAuthor(updateAuthorVM As UpdateAuthorVM) Implements IAuthorService.UpdateAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim existingAuthor = authorRepository.GetById(updateAuthorVM.Id).Result

        If existingAuthor IsNot Nothing Then
            existingAuthor.FirstName = updateAuthorVM.FirstName
            existingAuthor.LastName = updateAuthorVM.LastName
            existingAuthor.BirthDate = updateAuthorVM.BirthDate
            existingAuthor.Nationality = updateAuthorVM.Nationality

            authorRepository.Update(existingAuthor)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub

    Public Sub DeleteAuthor(authorId As Long) Implements IAuthorService.DeleteAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim authorToDelete = authorRepository.GetById(authorId).Result

        If authorToDelete IsNot Nothing Then
            authorRepository.Delete(authorToDelete)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub
End Class
