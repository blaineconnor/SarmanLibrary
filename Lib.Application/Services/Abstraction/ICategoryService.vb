Public Interface ICategoryService
    Function GetAllCategories() As Task(Of Result(Of List(Of CategoryDTO)))
    Function GetCategoryById(categoryId As Long) As Task(Of Result(Of CategoryDTO))
    Function CreateCategory(createCategoryVM As CreateCategoryVM) As Result(Of CreateCategoryVM)
    Function UpdateCategory(updateCategoryVM As UpdateCategoryVM) As Result(Of UpdateCategoryVM)
    Function DeleteCategory(categoryId As Long) As Result(Of Object)
End Interface
