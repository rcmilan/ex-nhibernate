using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;

namespace ex_nhibernate.Database
{
    public static class MigrationRunner
    {
        public static void RunMigrations(string connectionString)
        {
            try
            {
                string dbName = "dbexample";

                // Connect to the master database
                using var connection = new SqlConnection("Server=localhost,1433;Database=master;User Id=sa;Password=msSQL@123;TrustServerCertificate=True;");
                connection.Open();

                // Check if the database exists
                string checkDbQuery = $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{dbName}') BEGIN CREATE DATABASE {dbName}; END";
                using var command = new SqlCommand(checkDbQuery, connection);
                command.ExecuteNonQuery();

                Console.WriteLine("Database verified/created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ensuring database exists: {ex.Message}");
                throw;
            }

            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(MigrationRunner).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();


            runner.MigrateUp();
        }
    }
}
