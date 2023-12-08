Imports [Lib].Domain

Public Class PublisherService
    Implements IPublisherService

    Private ReadOnly _unitOfWork As IUnitofWork

    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function GetAllPublishers() As Task(Of List(Of PublisherDTO)) Implements IPublisherService.GetAllPublishers
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()

        Dim publishers = Await publisherRepository.GetAllAsync()

        Return publishers.Select(Function(publisher) New PublisherDTO With {
            .Id = publisher.Id,
            .Name = publisher.Name,
            .Description = publisher.Description
        }).ToList()
    End Function

    Public Async Function GetPublisherById(publisherId As Long) As Task(Of PublisherDTO) Implements IPublisherService.GetPublisherById
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()

        Dim publisher = Await publisherRepository.GetById(publisherId)

        If publisher IsNot Nothing Then
            Return New PublisherDTO With {
                .Id = publisher.Id,
                .Name = publisher.Name,
                .Description = publisher.Description
            }
        End If

        Return Nothing
    End Function

    Public Sub AddPublisher(addPublisherVM As AddPublisherVM) Implements IPublisherService.AddPublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()

        Dim newPublisher As New Publisher With {
            .Name = addPublisherVM.Name,
            .Description = addPublisherVM.Description
        }

        publisherRepository.Add(newPublisher)
        _unitOfWork.CommitAsync().Wait()
    End Sub

    Public Sub UpdatePublisher(updatePublisherVM As UpdatePublisherVM) Implements IPublisherService.UpdatePublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()

        Dim existingPublisher = publisherRepository.GetById(updatePublisherVM.Id).Result

        If existingPublisher IsNot Nothing Then
            existingPublisher.Name = updatePublisherVM.Name
            existingPublisher.Description = updatePublisherVM.Description

            publisherRepository.Update(existingPublisher)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub

    Public Sub DeletePublisher(publisherId As Long) Implements IPublisherService.DeletePublisher
        Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()

        Dim publisherToDelete = publisherRepository.GetById(publisherId).Result

        If publisherToDelete IsNot Nothing Then
            publisherRepository.Delete(publisherToDelete)
            _unitOfWork.CommitAsync().Wait()
        End If
    End Sub
End Class
