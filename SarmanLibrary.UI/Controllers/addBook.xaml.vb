Public Class addBook
    Inherits Window
    Private Sub closeWindow_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub DraggMove(sender As Object, e As MouseButtonEventArgs) Handles addBookMove.MouseDown
        If (Mouse.LeftButton = MouseButtonState.Pressed) Then
            Me.DragMove()
        End If
    End Sub
End Class
