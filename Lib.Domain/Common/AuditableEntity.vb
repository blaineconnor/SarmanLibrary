Public Class AuditableEntity
    Inherits BaseEntity

    Public Property CreatedBy As String
    Public Property CreatedDate As DateTime?
    Public Property UpdatedBy As String
    Public Property UpdatedDate As DateTime?
    Public Property DeletedBy As String
    Public Property DeletedDate As DateTime?
End Class
