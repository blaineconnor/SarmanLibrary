Public Class Publisher
    Inherits AuditableEntity

    Public Property Name As String
    Public Property Description As String

    ' Navigation property
    Public Overridable Property Books As ICollection(Of Book)
End Class
