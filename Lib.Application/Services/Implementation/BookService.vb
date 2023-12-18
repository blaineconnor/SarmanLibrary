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
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .AuthorName = book.Author.LastName,
                .ReleaseDate = book.Year.Year
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function AddBook(addBookVM As AddBookVM) As Result(Of AddBookVM) Implements IBookService.AddBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of AddBookVM)

        Try
            Dim newBook As New Book With {
                .BookName = addBookVM.BookName,
                .Page = addBookVM.Page,
                .ReleaseDate = addBookVM.ReleaseDate,
                .Category = New Category With {
                .Name = addBookVM.CategoryName
                 },
                .Detail = addBookVM.Detail,
                .IsRead = addBookVM.IsRead,
                .Author = New Author With {
                    .LastName = addBookVM.AuthorName
                },
                .Publisher = New Publisher With {
                            .Name = addBookVM.PublisherName
                }
            }

            bookRepository.Add(newBook)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function UpdateBook(vM As UpdateBookVM) As Task(Of Result(Of UpdateBookVM)) Implements IBookService.UpdateBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of UpdateBookVM)

        Try
            Dim existingBook = Await bookRepository.GetById(vM.Id)

            If existingBook IsNot Nothing Then
                existingBook.BookName = vM.BookName
                existingBook.Page = vM.Page
                existingBook.ReleaseDate = vM.ReleaseDate
                existingBook.Category.Name = vM.CategoryName
                existingBook.Detail = vM.Detail
                existingBook.IsRead = vM.IsRead
                existingBook.Publisher.Name = vM.PublisherName
                existingBook.Author.LastName = vM.AuthorName

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

        Return result
    End Function

    Public Function DeleteBook(bookId As Long) As Result(Of Object) Implements IBookService.DeleteBook
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

        Return result
    End Function

    Public Async Function GetBookById(bookId As Long) As Task(Of Result(Of BookDTO)) Implements IBookService.GetBookById
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of BookDTO)

        Try
            Dim book = Await bookRepository.GetById(bookId)

            If book IsNot Nothing Then
                result.Data = New BookDTO With {
                    .Id = book.Id,
                    .BookName = book.BookName,
                    .Page = book.Page,
                    .CategoryName = book.Category.Name,
                    .Detail = book.Detail,
                    .IsRead = book.IsRead,
                    .PublisherName = book.Publisher.Name,
                    .AuthorName = book.Author.LastName,
                    .ReleaseDate = book.Year.Year
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
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Category.Name = categoryId, "Author", "Category", "Publisher")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .AuthorName = book.Author.LastName
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
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Author.LastName = authorId, "Author", "Category", "Publisher")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .AuthorName = book.Author.LastName
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
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Publisher.Name = publisherId, "Author", "Category", "Publisher")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .BookName = book.BookName,
                .Page = book.Page,
                .CategoryName = book.Category.Name,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .PublisherName = book.Publisher.Name,
                .AuthorName = book.Author.LastName
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function
End Class
