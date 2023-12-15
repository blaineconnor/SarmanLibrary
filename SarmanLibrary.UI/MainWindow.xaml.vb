﻿Imports System.Net.Http
Imports System.Text
Imports DevExpress.Xpf.Core

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Partial Public Class MainWindow




    Inherits ThemedWindow
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub btn_kapat_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub btn_minimalize_Click(sender As Object, e As RoutedEventArgs)
        Me.WindowState = WindowState.Minimized
    End Sub

    Private Sub brdTopRight_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles brdTopRight.MouseDown
        If (Mouse.LeftButton = MouseButtonState.Pressed) Then
            Me.DragMove()
        End If
    End Sub

    Private Sub btn_fullScreen_Click(sender As Object, e As RoutedEventArgs)
        If (WindowState = WindowState.Normal) Then
            Me.WindowState = WindowState.Maximized
        ElseIf (WindowState = WindowState.Maximized) Then
            Me.WindowState = WindowState.Normal
        End If
    End Sub
    Private Sub sideBar_AllBooks_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New AllBooks())
    End Sub

    Private Sub sideBar_Authors_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Authors())
    End Sub

    Private Sub sideBar_Categories_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Categories())
    End Sub

    Private Sub sideBar_Publishers_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Publisher())
    End Sub
End Class