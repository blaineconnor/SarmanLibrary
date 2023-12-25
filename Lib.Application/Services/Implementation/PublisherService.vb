Imports [Lib].Domain

Public Class PublisherService
    Implements IPublisherService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllPublishers() As Task(Of Result(Of List(Of PublisherDTO))) Implements IPublisherService.GetAllPublishers
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
        Dim result As New Result(Of List(Of PublisherDTO))

        Try
            Dim publishers = Await publisherRepository.GetAllAsync()

            result.Data = publishers.Select(Function(publisher) New PublisherDTO With {
                .Id = publisher.Id,
                .Name = publisher.Name,
                .Description = publisher.Description
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Async Function GetPublisherById(publisherId As Long) As Task(Of Result(Of PublisherDTO)) Implements IPublisherService.GetPublisherById
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
        Dim result As New Result(Of PublisherDTO)

        Try
            Dim publisher = Await publisherRepository.GetById(publisherId)

            If publisher IsNot Nothing Then
                result.Data = New PublisherDTO With {
                    .Id = publisher.Id,
                    .Name = publisher.Name,
                    .Description = publisher.Description
                }
                result.Success = True
            Else
                result.Errors.Add("Yayınevi bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result
    End Function

    Public Function AddPublisher(addPublisherVM As AddPublisherVM) As Result(Of Integer) Implements IPublisherService.AddPublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
        Dim result As New Result(Of Object)

        Try
            Dim newPublisher As New Publisher With {
                .Name = addPublisherVM.Name,
                .Description = addPublisherVM.Description
            }

            publisherRepository.Add(newPublisher)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function UpdatePublisher(updatePublisherVM As UpdatePublisherVM) As Result(Of Integer) Implements IPublisherService.UpdatePublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
        Dim result As New Result(Of Object)

        Try
            Dim existingPublisher = publisherRepository.GetById(updatePublisherVM.Id).Result

            If existingPublisher IsNot Nothing Then
                existingPublisher.Name = updatePublisherVM.Name
                existingPublisher.Description = updatePublisherVM.Description

                publisherRepository.Update(existingPublisher)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yayınevi bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function

    Public Function DeletePublisher(publisherId As Long) As Result(Of Integer) Implements IPublisherService.DeletePublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
        Dim result As New Result(Of Object)

        Try
            Dim publisherToDelete = publisherRepository.GetById(publisherId).Result

            If publisherToDelete IsNot Nothing Then
                publisherRepository.Delete(publisherToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yayınevi bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add(ex.Message)
        End Try

        Return result.Data
    End Function
End Class
