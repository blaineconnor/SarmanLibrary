Imports [Lib].Domain

Public Class CategoryService
    Implements ICategoryService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllCategories() As Task(Of Result(Of List(Of CategoryDTO))) Implements ICategoryService.GetAllCategories
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()
        Dim result As New Result(Of List(Of CategoryDTO))

        Try
            Dim categories = Await categoryRepository.GetAllAsync()

            result.Data = categories.Select(Function(category) New CategoryDTO With {
                .Id = category.Id,
                .Name = category.Name
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetCategoryById(categoryId As Long) As Task(Of Result(Of CategoryDTO)) Implements ICategoryService.GetCategoryById
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()
        Dim result As New Result(Of CategoryDTO)

        Try
            Dim category = Await categoryRepository.GetById(categoryId)

            If category IsNot Nothing Then
                result.Data = New CategoryDTO With {
                    .Id = category.Id,
                    .Name = category.Name
                }
                result.Success = True
            Else
                result.Errors.Add("Kategori bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function CreateCategory(createCategoryVM As CreateCategoryVM) As Result(Of Integer) Implements ICategoryService.CreateCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()
        Dim result As New Result(Of Object)

        Try
            Dim newCategory As New Category With {
                .Name = createCategoryVM.Name
            }

            categoryRepository.Add(newCategory)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function UpdateCategory(updateCategoryVM As UpdateCategoryVM) As Result(Of Integer) Implements ICategoryService.UpdateCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()
        Dim result As New Result(Of Object)

        Try
            Dim existingCategory = categoryRepository.GetById(updateCategoryVM.Id).Result

            If existingCategory IsNot Nothing Then
                existingCategory.Name = updateCategoryVM.Name

                categoryRepository.Update(existingCategory)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Kategori bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function DeleteCategory(categoryId As Long) As Result(Of Integer) Implements ICategoryService.DeleteCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()
        Dim result As New Result(Of Object)

        Try
            Dim categoryToDelete = categoryRepository.GetById(categoryId).Result

            If categoryToDelete IsNot Nothing Then
                categoryRepository.Delete(categoryToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Kategori bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function
End Class
