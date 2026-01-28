using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InvoiceGenerator.Infrastructure.Data
{
    /// <summary>
    /// Factory for creating AppDbContext instances at design-time (for migrations).
    /// This is used by EF Core tools when running commands like 'dotnet ef migrations add'.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use a default connection string for design-time
            // You can also read from appsettings.json if needed
            var connectionString = "Server=localhost;Database=InvoiceDb;Port=5432;User Id=postgres;Password=postgres;";

            optionsBuilder.UseNpgsql(connectionString)
                          .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name })
                          .EnableSensitiveDataLogging();

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
