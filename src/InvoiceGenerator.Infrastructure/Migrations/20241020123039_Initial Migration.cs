using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvoiceGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Details = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Street = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Street2 = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2024, 10, 20)),
                    ValidStartDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(2024, 10, 20)),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubTotal = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Tax = table.Column<int>(type: "integer", precision: 12, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    BillingAddressId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Addresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    InvoiceDetailsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_InvoiceDetails_InvoiceDetailsId",
                        column: x => x.InvoiceDetailsId,
                        principalTable: "InvoiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { 1, "Reparacion, mantenimiento, etc", "Reparacion de Grua", 8999m },
                    { 2, "Mantenimiento Preventivo", "Mantenimiento de Equipos", 3500m },
                    { 3, "Pintura a grua", "Capa de Pintura a Grua", 4500m },
                    { 4, "Reparaciones para modulos electricos", "Reparaciones electricas", 15000m },
                    { 5, "Maquinados chidos", "Maquinados perrones", 1799m },
                    { 6, "Transportacion de maquinaria a toda la ciudad", "Transporte de Maquinaria", 10000m }
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
                    { 1, new DateOnly(2024, 10, 20), 1, new DateOnly(2024, 11, 18), "INV-467958", new DateOnly(2024, 10, 24) },
                    { 2, new DateOnly(2024, 10, 20), 1, new DateOnly(2024, 11, 16), "INV-134678", new DateOnly(2024, 10, 22) },
                    { 3, new DateOnly(2024, 10, 20), 2, new DateOnly(2024, 11, 14), "INV-258795", new DateOnly(2024, 10, 20) },
                    { 4, new DateOnly(2024, 10, 20), 3, new DateOnly(2024, 11, 18), "INV-462579", new DateOnly(2024, 10, 24) },
                    { 5, new DateOnly(2024, 10, 20), 3, new DateOnly(2024, 11, 18), "INV-231645", new DateOnly(2024, 10, 24) },
                    { 6, new DateOnly(2024, 10, 20), 4, new DateOnly(2024, 11, 13), "INV-465281", new DateOnly(2024, 10, 19) },
                    { 7, new DateOnly(2024, 10, 20), 4, new DateOnly(2024, 11, 13), "INV-356891", new DateOnly(2024, 10, 19) },
                    { 8, new DateOnly(2024, 10, 20), 4, new DateOnly(2024, 11, 16), "INV-197846", new DateOnly(2024, 10, 22) },
                    { 9, new DateOnly(2024, 10, 20), 5, new DateOnly(2024, 11, 19), "INV-1387541", new DateOnly(2024, 10, 25) },
                    { 10, new DateOnly(2024, 10, 20), 5, new DateOnly(2024, 11, 19), "INV-978548", new DateOnly(2024, 10, 25) },
                    { 11, new DateOnly(2024, 10, 20), 5, new DateOnly(2024, 11, 10), "INV-159632", new DateOnly(2024, 10, 16) },
                    { 12, new DateOnly(2024, 10, 20), 2, new DateOnly(2024, 11, 7), "INV-1547862", new DateOnly(2024, 10, 13) }
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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_BillingAddressId",
                table: "InvoiceDetails",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceDetailsId",
                table: "InvoiceLineItems",
                column: "InvoiceDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_ItemId",
                table: "InvoiceLineItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
