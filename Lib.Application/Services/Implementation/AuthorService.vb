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
                .BirthDate = author.Year.Year,
                .CountryName = author.Nationality.Name
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
            Dim author = Await authorRepository.GetById(authorId)

            If author IsNot Nothing Then
                result.Data = New AuthorDTO With {
                    .Id = author.Id,
                    .FirstName = author.FirstName,
                    .LastName = author.LastName,
                    .BirthDate = author.Year.Year,
                    .CountryName = author.Nationality.Name
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

    Public Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of AddAuthorVM) Implements IAuthorService.AddAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of AddAuthorVM)

        Try
            Dim newAuthor As New Author With {
                .FirstName = addAuthorVM.FirstName,
                .LastName = addAuthorVM.LastName,
                .BirthDate = addAuthorVM.BirthDate,
                .Country = addAuthorVM.CountryName
            }

            authorRepository.Add(newAuthor)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function UpdateAuthor(updateAuthorVM As UpdateAuthorVM) As Result(Of UpdateAuthorVM) Implements IAuthorService.UpdateAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of UpdateAuthorVM)

        Try
            Dim existingAuthor = authorRepository.GetById(updateAuthorVM.Id).Result
            If existingAuthor IsNot Nothing Then
                existingAuthor.FirstName = updateAuthorVM.FirstName
                existingAuthor.LastName = updateAuthorVM.LastName
                existingAuthor.BirthDate = updateAuthorVM.BirthDate
                existingAuthor.Country = updateAuthorVM.CountryId

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

        Return result
    End Function

    Public Function DeleteAuthor(authorId As Long) As Result(Of Object) Implements IAuthorService.DeleteAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of Object)

        Try
            Dim authorToDelete = authorRepository.GetById(authorId).Result

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

        Return result
    End Function
End Class
