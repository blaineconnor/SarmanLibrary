Imports [Lib].Domain

Public Class BookService
    Implements IBookService
    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllBooks() As Task(Of List(Of BookDTO)) Implements IBookService.GetAllBooks

        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim books = Await bookRepository.GetAllAsync()

        Return books.Select(Function(book) New BookDTO With {
        .Id = book.Id,
        .BookName = book.BookName,
        .Page = book.Page,
        .ReleaseDate = book.ReleaseDate,
        .CategoryId = book.CategoryId,
        .Detail = book.Detail,
        .IsRead = book.IsRead,
        .PublisherId = book.PublisherId,
        .AuthorId = book.AuthorId
    }).ToList()
    End Function

    Public Sub AddBook(addBookVM As AddBookVM) Implements IBookService.AddBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim newBook As New Book With {
            .BookName = addBookVM.BookName,
            .Page = addBookVM.Page,
            .ReleaseDate = addBookVM.ReleaseDate,
            .CategoryId = addBookVM.CategoryId,
            .Detail = addBookVM.Detail,
            .IsRead = addBookVM.IsRead,
            .PublisherId = addBookVM.PublisherId,
            .AuthorId = addBookVM.AuthorId
        }

        bookRepository.Add(newBook)
        _unitOfWork.CommitAsync().Wait()
    End Sub
    Public Async Sub UpdateBook(vM As UpdateBookVM) Implements IBookService.UpdateBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim existingBook = Await bookRepository.GetById(vM.Id)

        If existingBook IsNot Nothing Then
            existingBook.BookName = vM.BookName
            existingBook.Page = vM.Page
            existingBook.ReleaseDate = vM.ReleaseDate
            existingBook.CategoryId = vM.CategoryId
            existingBook.Detail = vM.Detail
            existingBook.IsRead = vM.IsRead
            existingBook.PublisherId = vM.PublisherId
            existingBook.AuthorId = vM.AuthorId

            bookRepository.Update(existingBook)
            Await _unitOfWork.CommitAsync()
        End If
    End Sub

    Public Sub DeleteBook(bookId As Long) Implements IBookService.DeleteBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim bookToDelete = bookRepository.GetById(bookId)

        If bookToDelete IsNot Nothing Then
            bookRepository.Delete(bookToDelete)
            _unitOfWork.CommitAsync()
        End If
    End Sub
    Public Async Function GetBookById(bookId As Long) As Task(Of BookDTO) Implements IBookService.GetBookById
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim book = Await bookRepository.GetById(bookId)

        If book IsNot Nothing Then
            Return New BookDTO With {
            .Id = book.Id,
            .BookName = book.BookName,
            .Page = book.Page,
            .ReleaseDate = book.ReleaseDate,
            .CategoryId = book.CategoryId,
            .Detail = book.Detail,
            .IsRead = book.IsRead,
            .PublisherId = book.PublisherId,
            .AuthorId = book.AuthorId,
        .Publisher = New PublisherDTO With {
            .Id = book.Publisher.Id,
            .Name = book.Publisher.Name
        },
        .Author = New AuthorDTO With {
            .Id = book.Author.Id,
            .FirstName = book.Author.FirstName
        },
        .Category = New CategoryDTO With {
            .Id = book.Category.Id,
            .Name = book.Category.Name
        }
        }
        End If

        Return Nothing
    End Function

    Public Async Function GetBooksByCategory(categoryId As Long) As Task(Of List(Of BookDTO)) Implements IBookService.GetBooksByCategory
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.CategoryId = categoryId, "Author", "Category", "Publisher")

        Return books.Select(Function(book) New BookDTO With {
        .Id = book.Id,
        .BookName = book.BookName,
        .Page = book.Page,
        .ReleaseDate = book.ReleaseDate,
        .CategoryId = book.CategoryId,
        .Detail = book.Detail,
        .IsRead = book.IsRead,
        .PublisherId = book.PublisherId,
        .AuthorId = book.AuthorId,
        .Publisher = New PublisherDTO With {
            .Id = book.Publisher.Id,
            .Name = book.Publisher.Name
        },
        .Author = New AuthorDTO With {
            .Id = book.Author.Id,
            .FirstName = book.Author.FirstName
        },
        .Category = New CategoryDTO With {
            .Id = book.Category.Id,
            .Name = book.Category.Name
        }
    }).ToList()
    End Function
    Public Async Function GetBooksByAuthor(authorId As Long) As Task(Of List(Of BookDTO)) Implements IBookService.GetBooksByAuthor
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.AuthorId = authorId, "Author", "Category", "Publisher")

        Return books.Select(Function(book) New BookDTO With {
        .Id = book.Id,
        .BookName = book.BookName,
        .Page = book.Page,
        .ReleaseDate = book.ReleaseDate,
        .CategoryId = book.CategoryId,
        .Detail = book.Detail,
        .IsRead = book.IsRead,
        .PublisherId = book.PublisherId,
        .AuthorId = book.AuthorId,
        .Publisher = New PublisherDTO With {
            .Id = book.Publisher.Id,
            .Name = book.Publisher.Name
        },
        .Author = New AuthorDTO With {
            .Id = book.Author.Id,
            .FirstName = book.Author.FirstName
        },
        .Category = New CategoryDTO With {
            .Id = book.Category.Id,
            .Name = book.Category.Name
        }
    }).ToList()
    End Function
    Public Async Function GetBooksByPublisher(publisherId As Long) As Task(Of List(Of BookDTO)) Implements IBookService.GetBooksByPublisher
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.PublisherId = publisherId, "Author", "Category", "Publisher")

        Return books.Select(Function(book) New BookDTO With {
        .Id = book.Id,
        .BookName = book.BookName,
        .Page = book.Page,
        .ReleaseDate = book.ReleaseDate,
        .CategoryId = book.CategoryId,
        .Detail = book.Detail,
        .IsRead = book.IsRead,
        .PublisherId = book.PublisherId,
        .AuthorId = book.AuthorId,
        .Publisher = New PublisherDTO With {
            .Id = book.Publisher.Id,
            .Name = book.Publisher.Name
        },
        .Author = New AuthorDTO With {
            .Id = book.Author.Id,
            .FirstName = book.Author.FirstName
        },
        .Category = New CategoryDTO With {
            .Id = book.Category.Id,
            .Name = book.Category.Name
        }
    }).ToList()
    End Function


End Class
