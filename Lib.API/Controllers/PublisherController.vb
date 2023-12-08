Imports [Lib].Application
Imports Microsoft.AspNetCore.Mvc

<Route("api/[controller]")>
<ApiController>
Public Class PublisherController
    Inherits ControllerBase

    Private ReadOnly _publisherService As IPublisherService

    Public Sub New(publisherService As IPublisherService)
        _publisherService = publisherService
    End Sub

    <HttpGet("all")>
    Public Async Function GetAllPublishers() As Task(Of ActionResult(Of List(Of PublisherDTO)))
        Dim publishers = Await _publisherService.GetAllPublishers()
        Return Ok(publishers)
    End Function

    <HttpGet("{publisherId}")>
    Public Async Function GetPublisherById(publisherId As Long) As Task(Of ActionResult(Of PublisherDTO))
        Dim publisher = Await _publisherService.GetPublisherById(publisherId)
        If publisher IsNot Nothing Then
            Return Ok(publisher)
        End If
        Return NotFound()
    End Function

    <HttpPost("add")>
    Public Sub AddPublisher(addPublisherVM As AddPublisherVM)
        _publisherService.AddPublisher(addPublisherVM)
    End Sub

    <HttpPut("update")>
    Public Sub UpdatePublisher(updatePublisherVM As UpdatePublisherVM)
        _publisherService.UpdatePublisher(updatePublisherVM)
    End Sub

    <HttpDelete("{publisherId}")>
    Public Sub DeletePublisher(publisherId As Long)
        _publisherService.DeletePublisher(publisherId)
    End Sub
End Class
