Imports [Lib].Domain

Public Class CategoryService
    Implements ICategoryService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllCategories() As Task(Of List(Of CategoryDTO)) Implements ICategoryService.GetAllCategories
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()

        Dim categories = Await categoryRepository.GetAllAsync()

        Return categories.Select(Function(category) New CategoryDTO With {
            .Id = category.Id,
            .Name = category.Name
        }).ToList()
    End Function

    Public Async Function GetCategoryById(categoryId As Long) As Task(Of CategoryDTO) Implements ICategoryService.GetCategoryById
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()

        Dim category = Await categoryRepository.GetById(categoryId)

        If category IsNot Nothing Then
            Return New CategoryDTO With {
                .Id = category.Id,
                .Name = category.Name
            }
        End If

        Return Nothing
    End Function

    Public Sub CreateCategory(createCategoryVM As CreateCategoryVM) Implements ICategoryService.CreateCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()

        Dim newCategory As New Category With {
            .Name = createCategoryVM.Name
        }

        categoryRepository.Add(newCategory)
        _unitOfWork.CommitAsync().Wait()
    End Sub

    Public Sub UpdateCategory(updateCategoryVM As UpdateCategoryVM) Implements ICategoryService.UpdateCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()

        Dim existingCategory = categoryRepository.GetById(updateCategoryVM.Id).Result

        If existingCategory IsNot Nothing Then
            existingCategory.Name = updateCategoryVM.Name

            categoryRepository.Update(existingCategory)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub

    Public Sub DeleteCategory(categoryId As Long) Implements ICategoryService.DeleteCategory
        Dim categoryRepository = _unitOfWork.GetRepository(Of Category)()

        Dim categoryToDelete = categoryRepository.GetById(categoryId).Result

        If categoryToDelete IsNot Nothing Then
            categoryRepository.Delete(categoryToDelete)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub
End Class
