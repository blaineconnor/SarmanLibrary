Partial Public Class AllBooks
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub btn_AddBook_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New addBook()
        add.ShowDialog()
    End Sub
End Class
