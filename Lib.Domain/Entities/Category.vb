Public Class Category
    Inherits BaseEntity

    Public Property Name As String

    Public Overridable Property Books As ICollection(Of Book)

    Public Sub New()
        Books = New HashSet(Of Book)()
    End Sub
End Class
