Public Interface IPublisherService
    Function GetAllPublishers() As Task(Of List(Of PublisherDTO))
    Function GetPublisherById(publisherId As Long) As Task(Of PublisherDTO)
    Sub AddPublisher(addPublisherVM As AddPublisherVM)
    Sub UpdatePublisher(updatePublisherVM As UpdatePublisherVM)
    Sub DeletePublisher(publisherId As Long)
End Interface
