Imports System.Net.Http
Imports System.Text.Json

Public Class HttpConnection(Of T As New)

    Private Shared BaseAddress As String = "https://localhost:49961/api"

    Public Async Function GetData(url As String) As Task(Of Result(Of T))
        Dim client As New HttpClient()
        Dim k As String = $"{BaseAddress}{url}"
        Dim x = Await client.GetAsync(k)
        Dim y = Await x.Content.ReadAsStringAsync()
        Dim jsonData = JsonSerializer.Deserialize(Of Result(Of T))(y, New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True})
        Return jsonData
    End Function
End Class
