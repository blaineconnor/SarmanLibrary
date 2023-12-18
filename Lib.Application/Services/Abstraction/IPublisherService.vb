Public Interface IPublisherService
    Function GetAllPublishers() As Task(Of Result(Of List(Of PublisherDTO)))
    Function GetPublisherById(publisherId As Long) As Task(Of Result(Of PublisherDTO))
    Function AddPublisher(addPublisherVM As AddPublisherVM) As Result(Of AddPublisherVM)
    Function UpdatePublisher(updatePublisherVM As UpdatePublisherVM) As Result(Of UpdatePublisherVM)
    Function DeletePublisher(publisherId As Long) As Result(Of Object)
End Interface
