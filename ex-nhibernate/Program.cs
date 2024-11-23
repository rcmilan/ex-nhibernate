using ex_nhibernate.Database;

namespace ex_nhibernate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception();

        Console.WriteLine("Migrations...");
        MigrationRunner.RunMigrations(connectionString);
        Console.WriteLine("Migrations Ok!");

        // Add services to the container.
        builder.Services.AddNHibernate(connectionString);

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
