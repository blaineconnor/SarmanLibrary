Imports [Lib].Application
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.AspNetCore.Mvc.Filters

Public Class ExceptionHandlerFilter
    Implements IExceptionFilter

    Public Sub OnException(context As ExceptionContext) Implements IExceptionFilter.OnException
        Dim result As New Result(Of Object) With {.Success = False}

        If TypeOf context.Exception Is NotFoundException Then
            Dim notFoundException As NotFoundException = context.Exception
            result.Errors = New List(Of String) From {notFoundException.Message}
        ElseIf TypeOf context.Exception Is AlreadyExistsException Then
            Dim alreadyExistsException As AlreadyExistsException = context.Exception
            result.Errors = New List(Of String) From {alreadyExistsException.Message}
        ElseIf TypeOf context.Exception Is ValidateException Then
            Dim validationException As ValidateException = context.Exception
            result.Errors.AddRange(validationException.ErrorMessage)
        Else
            result.Errors = New List(Of String) From {If(context.Exception.InnerException IsNot Nothing, context.Exception.InnerException.Message, context.Exception.Message)}
        End If



        context.Result = New JsonResult(result)
        context.HttpContext.Response.StatusCode = 400
        context.ExceptionHandled = True
    End Sub
End Class

Public Class Test
    Implements IActionFilter

    Public Sub OnActionExecuted(context As ActionExecutedContext) Implements IActionFilter.OnActionExecuted
        Throw New NotImplementedException()
    End Sub

    Public Sub OnActionExecuting(context As ActionExecutingContext) Implements IActionFilter.OnActionExecuting
        Throw New NotImplementedException()
    End Sub
End Class
