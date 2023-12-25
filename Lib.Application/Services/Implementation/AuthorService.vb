Imports System.Net
Imports [Lib].Domain

Public Class AuthorService
    Implements IAuthorService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllAuthors() As Task(Of Result(Of List(Of AuthorDTO))) Implements IAuthorService.GetAllAuthors
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of List(Of AuthorDTO))

        Try
            Dim authors = Await authorRepository.GetAllAsync()

            Dim r = authors.Select(Function(author) New AuthorDTO With {
                .Id = author.Id,
                .FirstName = author.FirstName,
                .LastName = author.LastName,
                .IntDate = author.Year.Id,
                .CountryName = author.Country.Id
            }).ToList()

            result.Data = r
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetAuthorById(authorId As Long) As Task(Of Result(Of AuthorDTO)) Implements IAuthorService.GetAuthorById
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of AuthorDTO)
        Try
            Dim author = Await authorRepository.GetSingleByFilterAsync(Function(e) e.Id = authorId, {"Year", "Country"})

            If author IsNot Nothing Then
                result.Data = New AuthorDTO With {
                    .Id = author.Id,
                    .FirstName = author.FirstName,
                    .LastName = author.LastName,
                    .IntDate = author.Year.IntDate,
                    .CountryName = author.Country.Name
                }
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of Integer) Implements IAuthorService.AddAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of Object)

        Try
            Dim newAuthor As New Author With {
                .FirstName = addAuthorVM.FirstName,
                .LastName = addAuthorVM.LastName,
                .YearId = addAuthorVM.BirthDate,
                .CountryId = addAuthorVM.CountryId
            }

            authorRepository.Add(newAuthor)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function UpdateAuthor(updateAuthorVM As UpdateAuthorVM) As Result(Of Integer) Implements IAuthorService.UpdateAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of Object)

        Try
            Dim existingAuthor = authorRepository.GetById(updateAuthorVM.Id).Result
            If existingAuthor IsNot Nothing Then
                existingAuthor.FirstName = updateAuthorVM.FirstName
                existingAuthor.LastName = updateAuthorVM.LastName
                existingAuthor.YearId = updateAuthorVM.YearId
                existingAuthor.CountryId = updateAuthorVM.CountryId

                authorRepository.Update(existingAuthor)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function DeleteAuthor(deleteAuthorByIdVM As Long) As Result(Of Integer) Implements IAuthorService.DeleteAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of Object)

        Try
            Dim authorToDelete = authorRepository.GetById(deleteAuthorByIdVM).Result

            If authorToDelete IsNot Nothing Then
                authorRepository.Delete(authorToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function
End Class
