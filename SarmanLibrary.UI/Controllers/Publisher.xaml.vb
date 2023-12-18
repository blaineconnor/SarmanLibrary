Public Class Publisher
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub addPublisher_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New AddPublisher()
        add.ShowDialog()
    End Sub
End Class
