Public Interface ICategoryService
    Function GetAllCategories() As Task(Of List(Of CategoryDTO))
    Function GetCategoryById(categoryId As Long) As Task(Of CategoryDTO)
    Sub CreateCategory(createCategoryVM As CreateCategoryVM)
    Sub UpdateCategory(updateCategoryVM As UpdateCategoryVM)
    Sub DeleteCategory(categoryId As Long)
End Interface
