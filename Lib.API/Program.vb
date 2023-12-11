Imports [Lib].Application
Imports [Lib].Domain
Imports [Lib].Persistence
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting


Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Veritabaný baðlantý dizesini al
        Dim connectionString = builder.Configuration.GetConnectionString("BasicLibrary")

        builder.Services.AddDbContext(Of LibContext)(Sub(options) options.UseSqlServer(connectionString))

        'Dependency Injection

        builder.Services.AddScoped(Of IAuthorService, AuthorService)
        builder.Services.AddScoped(Of IBookService, BookService)
        builder.Services.AddScoped(Of ICategoryService, CategoryService)
        builder.Services.AddScoped(Of IPublisherService, PublisherService)


        builder.Services.AddScoped(GetType(IRepository(Of BaseEntity)), GetType(Repository(Of BaseEntity)))

        builder.Services.AddScoped(Of IUnitofWork, UnitofWork)

        builder.Services.AddControllers(Sub(opt)
                                            opt.Filters.Add(New ExceptionHandlerFilter())
                                        End Sub)

        builder.Services.AddHttpContextAccessor()

        builder.Services.AddAutoMapper(GetType(DomainToDTO), GetType(ViewModelToDomain))

        ' Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Dim app = builder.Build()

        ' Configure the HTTP request pipeline.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()
    End Sub
End Module