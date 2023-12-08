Public Class NotFoundException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class
