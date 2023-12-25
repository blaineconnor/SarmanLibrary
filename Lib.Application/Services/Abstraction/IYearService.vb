Public Interface IYearService
    Function GetAllYear() As Task(Of Result(Of List(Of YearDTO)))
End Interface
