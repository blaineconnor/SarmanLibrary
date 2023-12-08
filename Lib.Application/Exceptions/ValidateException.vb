
Imports FluentValidation.Results

Public Class ValidateException
    Inherits Exception

    Public Property ErrorMessage As List(Of String)

    Public Sub New(ByVal result As ValidationResult)
        MyBase.New()
        ErrorMessage = result.Errors.[Select](Function(x) x.ErrorMessage).ToList()
    End Sub
End Class
