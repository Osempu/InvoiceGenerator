using System;
using InvoiceGenerator.Core.Model;
using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Data.Seeding;

public static class CustomerSeeding
{
    public static void CustomerSeed(this ModelBuilder builder)
    {
        builder.Entity<Customer>().HasData(
            new Customer {
                Id = 1, 
                Name = "Servicios Llaneros", 
                Description = "Servicios de llanos online", 
            },
            new Customer {
                Id = 2,
                Name = "Inmobiliaria Belice",
                Description = "Empresa inmobiliaria del norte"
            },
            new Customer {
                Id = 3,
                Name = "Emplayados Sol",
                Description = "Empresa con logo de sol"
            },
            new Customer {
                Id = 4,
                Name = "Maquinados Frontera",
                Description = "Servicios de maquinado y soldadura"
            },
            new Customer {
                Id = 5,
                Name = "Integrados Del Noreste",
                Description = "Integrados y equipos para maquiladoras"
            }
        );

        builder.Entity<Address>().HasData(
            new Address(1, "san Lorenzo", "46875", "Tamaulipas", "Reynosa", "Mexico", "Billing", 1,  "356"),
            new Address(2, "Calle Hidalgo", "48791", "Tamaulipas", "Reynosa", "Mexico", "Shipping", 1,  "N/A"),
            new Address(3, "Calle Occidental", "1465", "Tamaulipas", "Reynosa", "Mexico", "Billing", 2, "1080"),
            new Address(4, "Calle Aldama", "34522", "Tamaulipas", "Rio Bravo", "Mexico", "Billing",3, "4689"),
            new Address(5, "Fraccionamiento Azteca", "79845", "Tamaulipas", "Rio Bravo", "Mexico", "Shipping",3, "7879"),
            new Address(6, "Calle Horacio", "88976", "Tamaulipas", "Matamoros", "Mexico", "Billing",4, "467858"),
            new Address(7, "Fracc. Tovar", "75319", "Tamaulipas", "Rio Matamoros", "Mexico", "Shipping",4, "356"),
            new Address(8, "Cantera Pelones", "136479", "Tamaulipas", "Rio Bravo", "Mexico", "Billing",5, "86479")
        );
    }
}
