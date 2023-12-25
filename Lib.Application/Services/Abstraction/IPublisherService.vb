Public Interface IPublisherService
    Function GetAllPublishers() As Task(Of Result(Of List(Of PublisherDTO)))
    Function GetPublisherById(publisherId As Long) As Task(Of Result(Of PublisherDTO))
    Function AddPublisher(addPublisherVM As AddPublisherVM) As Result(Of Integer)
    Function UpdatePublisher(updatePublisherVM As UpdatePublisherVM) As Result(Of Integer)
    Function DeletePublisher(publisherId As Long) As Result(Of Integer)
End Interface
