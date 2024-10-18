using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoiceGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedseedingforallentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Servicios de llanos online", "Servicios Llaneros" },
                    { 2, "Empresa inmobiliaria del norte", "Inmobiliaria Belice" },
                    { 3, "Empresa con logo de sol", "Emplayados Sol" },
                    { 4, "Servicios de maquinado y soldadura", "Maquinados Frontera" },
                    { 5, "Integrados y equipos para maquiladoras", "Integrados Del Noreste" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Details", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Reparacion, mantenimiento, etc", "Reparacion de Grua", 8999.99m },
                    { 2, "Mantenimiento Preventivo", "Mantenimiento de Equipos", 3500m },
                    { 3, "Pintura a grua", "Capa de Pintura a Grua", 4500.00m },
                    { 4, "Reparaciones para modulos electricos", "Reparaciones electricas", 15000.00m },
                    { 5, "Maquinados chidos", "Maquinados perrones", 1799.99m },
                    { 6, "Transportacion de maquinaria a toda la ciudad", "Transporte de Maquinaria", 10000.00m }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressType", "City", "Country", "CustomerId", "PostalCode", "State", "Street", "Street2" },
                values: new object[,]
                {
                    { 1, "Billing", "Reynosa", "Mexico", 1, "46875", "Tamaulipas", "san Lorenzo", "356" },
                    { 2, "Shipping", "Reynosa", "Mexico", 1, "48791", "Tamaulipas", "Calle Hidalgo", "N/A" },
                    { 3, "Billing", "Reynosa", "Mexico", 2, "1465", "Tamaulipas", "Calle Occidental", "1080" },
                    { 4, "Billing", "Rio Bravo", "Mexico", 3, "34522", "Tamaulipas", "Calle Aldama", "4689" },
                    { 5, "Shipping", "Rio Bravo", "Mexico", 3, "79845", "Tamaulipas", "Fraccionamiento Azteca", "7879" },
                    { 6, "Billing", "Matamoros", "Mexico", 4, "88976", "Tamaulipas", "Calle Horacio", "467858" },
                    { 7, "Shipping", "Rio Matamoros", "Mexico", 4, "75319", "Tamaulipas", "Fracc. Tovar", "356" },
                    { 8, "Billing", "Rio Bravo", "Mexico", 5, "136479", "Tamaulipas", "Cantera Pelones", "86479" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DueDate", "Identifier", "ValidStartDate" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 10, 17), 1, new DateOnly(2024, 11, 18), "INV-467958", new DateOnly(2024, 10, 24) },
                    { 2, new DateOnly(2024, 10, 17), 1, new DateOnly(2024, 11, 16), "INV-134678", new DateOnly(2024, 10, 22) },
                    { 3, new DateOnly(2024, 10, 17), 2, new DateOnly(2024, 11, 14), "INV-258795", new DateOnly(2024, 10, 20) },
                    { 4, new DateOnly(2024, 10, 17), 3, new DateOnly(2024, 11, 18), "INV-462579", new DateOnly(2024, 10, 24) },
                    { 5, new DateOnly(2024, 10, 17), 3, new DateOnly(2024, 11, 18), "INV-231645", new DateOnly(2024, 10, 24) },
                    { 6, new DateOnly(2024, 10, 17), 4, new DateOnly(2024, 11, 13), "INV-465281", new DateOnly(2024, 10, 19) },
                    { 7, new DateOnly(2024, 10, 17), 4, new DateOnly(2024, 11, 13), "INV-356891", new DateOnly(2024, 10, 19) },
                    { 8, new DateOnly(2024, 10, 17), 4, new DateOnly(2024, 11, 16), "INV-197846", new DateOnly(2024, 10, 22) },
                    { 9, new DateOnly(2024, 10, 17), 5, new DateOnly(2024, 11, 19), "INV-1387541", new DateOnly(2024, 10, 25) },
                    { 10, new DateOnly(2024, 10, 17), 5, new DateOnly(2024, 11, 19), "INV-978548", new DateOnly(2024, 10, 25) },
                    { 11, new DateOnly(2024, 10, 17), 5, new DateOnly(2024, 11, 10), "INV-159632", new DateOnly(2024, 10, 16) },
                    { 12, new DateOnly(2024, 10, 17), 2, new DateOnly(2024, 11, 7), "INV-1547862", new DateOnly(2024, 10, 13) }
                });

            migrationBuilder.InsertData(
                table: "InvoiceDetails",
                columns: new[] { "Id", "BillingAddressId", "InvoiceId", "SubTotal", "Tax", "Total" },
                values: new object[,]
                {
                    { 1, 1, 1, 14999m, 18, 16000m },
                    { 2, 1, 2, 10000m, 18, 11111m },
                    { 3, 3, 3, 45000m, 18, 50000m },
                    { 4, 4, 4, 3900m, 18, 4999m },
                    { 5, 4, 5, 8000m, 18, 9999m },
                    { 6, 6, 6, 4560m, 18, 5000m },
                    { 7, 6, 7, 12000m, 18, 12890m },
                    { 8, 6, 8, 6500m, 18, 8560m },
                    { 9, 8, 9, 13465m, 18, 143656m },
                    { 10, 8, 10, 9000m, 18, 11000m },
                    { 11, 8, 11, 1354m, 18, 2700m },
                    { 12, 3, 12, 2345m, 18, 2356m }
                });

            migrationBuilder.InsertData(
                table: "InvoiceLineItems",
                columns: new[] { "Id", "InvoiceDetailsId", "ItemId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 4 },
                    { 2, 1, 2, 3 },
                    { 3, 1, 1, 4 },
                    { 4, 2, 3, 2 },
                    { 5, 2, 6, 1 },
                    { 6, 3, 4, 1 },
                    { 7, 4, 4, 1 },
                    { 8, 4, 4, 1 },
                    { 9, 4, 5, 1 },
                    { 10, 4, 6, 2 },
                    { 12, 5, 2, 1 },
                    { 13, 5, 3, 1 },
                    { 14, 6, 3, 1 },
                    { 15, 7, 1, 1 },
                    { 16, 7, 2, 1 },
                    { 17, 8, 2, 1 },
                    { 18, 8, 6, 3 },
                    { 19, 1, 4, 1 },
                    { 20, 1, 1, 1 },
                    { 21, 11, 6, 1 },
                    { 22, 12, 5, 1 },
                    { 23, 12, 6, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "InvoiceLineItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "InvoiceDetails",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
