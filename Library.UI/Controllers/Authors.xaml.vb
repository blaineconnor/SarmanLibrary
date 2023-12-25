Public Class Authors
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AddAuthor_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New addAuthor()
        add.ShowDialog()
    End Sub
End Class
