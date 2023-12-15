Public Class AuthorDTO
    Public Property Id As Long
    Public Property FirstName As String
    Public Property LastName As String
    Public Property BirthDate As Date
    Public Property CountryId As Long

    'NavProp
    Public Overridable Property Nationality As CountryDTO
End Class