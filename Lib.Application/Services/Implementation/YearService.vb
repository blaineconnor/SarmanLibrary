Imports [Lib].Domain

Public Class YearService
    Implements IYearService

    Private ReadOnly _unitOfWork As IUnitofWork
    Public Async Function GetAllYear() As Task(Of Result(Of List(Of YearDTO))) Implements IYearService.GetAllYear
        Dim yearRepository = _unitOfWork.GetRepository(Of Year)()
        Dim result As New Result(Of List(Of YearDTO))

        Try
            Dim years = Await yearRepository.GetAllAsync()

            result.Data = years.Select(Function(year) New YearDTO With {
                .Id = year.Id,
                .IntDate = year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function
End Class
