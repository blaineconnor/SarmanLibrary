Public Interface ICountryService
    Function GetAllCountries() As Task(Of Result(Of List(Of CountryDTO)))
End Interface
