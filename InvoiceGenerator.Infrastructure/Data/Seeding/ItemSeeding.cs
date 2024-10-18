using InvoiceGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Data.Seeding;

public static class ItemSeeding
{
    public static void ItemSeed(this ModelBuilder builder)
    {
        builder.Entity<Item>().HasData(
            new Item {Id = 1, Name = "Reparacion de Grua", Details = "Reparacion, mantenimiento, etc", Price = 8999.99M},
            new Item {Id = 2, Name = "Mantenimiento de Equipos", Details = "Mantenimiento Preventivo", Price = 3500M},
            new Item {Id = 3, Name = "Capa de Pintura a Grua", Details = "Pintura a grua", Price = 4500.00M},
            new Item {Id = 4, Name = "Reparaciones electricas", Details = "Reparaciones para modulos electricos", Price = 15000.00M},
            new Item {Id = 5, Name = "Maquinados perrones", Details = "Maquinados chidos", Price = 1799.99M},
            new Item {Id = 6, Name = "Transporte de Maquinaria", Details = "Transportacion de maquinaria a toda la ciudad", Price = 10000.00M}
        );
    }
}
