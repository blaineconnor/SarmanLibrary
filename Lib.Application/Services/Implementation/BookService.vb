Imports [Lib].Domain

Public Class BookService
    Implements IBookService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllBooks() As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetAllBooks
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetAllAsync()

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function AddBook(addBookVM As AddBookVM) As Result(Of Integer) Implements IBookService.AddBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of Object)

        Try
            Dim newBook As New Book With {
                .BookName = addBookVM.BookName,
                .Page = addBookVM.Page,
                .YearId = addBookVM.YearId,
                .CategoryId = addBookVM.CategoryId,
                .Detail = addBookVM.Detail,
                .IsRead = addBookVM.IsRead,
                .AuthorId = addBookVM.AuthorId,
                .PublisherId = addBookVM.PublisherId
            }

            bookRepository.Add(newBook)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Async Function UpdateBook(updateBookVM As UpdateBookVM) As Task(Of Result(Of Integer)) Implements IBookService.UpdateBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of Object)

        Try
            Dim existingBook = Await bookRepository.GetById(updateBookVM.Id)

            If existingBook IsNot Nothing Then
                existingBook.BookName = updateBookVM.BookName
                existingBook.Page = updateBookVM.Page
                existingBook.Detail = updateBookVM.Detail
                existingBook.IsRead = updateBookVM.IsRead
                existingBook.PublisherId = updateBookVM.PublisherId
                existingBook.AuthorId = updateBookVM.AuthorId
                existingBook.CategoryId = updateBookVM.CategoryId

                bookRepository.Update(existingBook)
                Await _unitOfWork.CommitAsync()
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function DeleteBook(bookId As Long) As Result(Of Integer) Implements IBookService.DeleteBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of Object)

        Try
            Dim bookToDelete = bookRepository.GetById(bookId)

            If bookToDelete IsNot Nothing Then
                bookRepository.Delete(bookToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Async Function GetBookById(bookId As Long) As Task(Of Result(Of BookDTO)) Implements IBookService.GetBookById
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of BookDTO)

        Try
            Dim book = Await bookRepository.GetSingleByFilterAsync(Function(e) e.Id = bookId, {"Author", "Category", "Year", "Publisher"})

            If book IsNot Nothing Then
                result.Data = New BookDTO With {
                    .Id = book.Id,
                    .BookName = book.BookName,
                    .AuthorFirstName = book.Author.FirstName,
                    .AuthorLastName = book.Author.LastName,
                    .Page = book.Page,
                    .CategoryName = book.Category.Name,
                    .Detail = book.Detail,
                    .IsRead = book.IsRead,
                    .PublisherName = book.Publisher.Name,
                    .IntDate = book.Year.IntDate
                }
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı")
            End If

        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetBooksByCategory(categoryId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByCategory
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Category.Id = categoryId, "Author", "Category", "Year", "Publisher")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetBooksByAuthor(authorId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByAuthor
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Author.Id = authorId, "Author", "Category", "Publisher", "Year")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetBooksByPublisher(publisherId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByPublisher
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Publisher.Id = publisherId, "Author", "Category", "Publisher", "Year")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function
End Class
