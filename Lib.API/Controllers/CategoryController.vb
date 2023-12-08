Imports [Lib].Application
Imports Microsoft.AspNetCore.Mvc

<Route("api/[controller]")>
<ApiController>
Public Class CategoryController
    Inherits ControllerBase

    Private ReadOnly _categoryService As ICategoryService

    Public Sub New(categoryService As ICategoryService)
        _categoryService = categoryService
    End Sub

    <HttpGet("all")>
    Public Async Function GetAllCategories() As Task(Of ActionResult(Of List(Of CategoryDTO)))
        Dim categories = Await _categoryService.GetAllCategories()
        Return Ok(categories)
    End Function

    <HttpGet("{categoryId}")>
    Public Async Function GetCategoryById(categoryId As Long) As Task(Of ActionResult(Of CategoryDTO))
        Dim category = Await _categoryService.GetCategoryById(categoryId)
        If category IsNot Nothing Then
            Return Ok(category)
        End If
        Return NotFound()
    End Function

    <HttpPost("create")>
    Public Sub CreateCategory(createCategoryVM As CreateCategoryVM)
        _categoryService.CreateCategory(createCategoryVM)
    End Sub

    <HttpPut("update")>
    Public Sub UpdateCategory(updateCategoryVM As UpdateCategoryVM)
        _categoryService.UpdateCategory(updateCategoryVM)
    End Sub

    <HttpDelete("{categoryId}")>
    Public Sub DeleteCategory(categoryId As Long)
        _categoryService.DeleteCategory(categoryId)
    End Sub
End Class