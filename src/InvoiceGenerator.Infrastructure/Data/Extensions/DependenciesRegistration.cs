using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Infrastructure.Data.Extensions;

public static class DependenciesRegistration
{
    public static void ConfigureSqlContext(this IServiceCollection services, string ConnectionString)
        => services.AddDbContext<AppDbContext>(options => options.UseNpgsql(ConnectionString));
}
