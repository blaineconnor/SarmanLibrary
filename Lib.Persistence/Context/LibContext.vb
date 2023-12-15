Imports [Lib].Domain
Imports Microsoft.EntityFrameworkCore

Public Class LibContext
    Inherits DbContext

    Public Sub New(options As DbContextOptions(Of LibContext))
        MyBase.New(options)
    End Sub

    Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
        If Not optionsBuilder.IsConfigured Then
            MyBase.OnConfiguring(optionsBuilder)
            optionsBuilder.UseSqlServer("Server=DESKTOP-R04PVQ3\SQLEXPRESS01; Initial Catalog=BasicLibrary; Integrated Security=true; TrustServerCertificate=True; MultipleActiveResultSets=true;")
        End If
    End Sub

    'DB Props
    Public Property Books As DbSet(Of Book)
    Public Property Authors As DbSet(Of Author)
    Public Property Categories As DbSet(Of Category)
    Public Property Publishers As DbSet(Of Publisher)
    Public Property Country As DbSet(Of Country)
End Class
