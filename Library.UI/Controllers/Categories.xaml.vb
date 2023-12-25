Public Class Categories
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub addCategory_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New AddCategory()
        add.ShowDialog()
    End Sub
End Class
