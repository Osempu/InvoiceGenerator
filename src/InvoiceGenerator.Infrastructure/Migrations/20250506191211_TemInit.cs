using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TemInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ValidStartDate",
                table: "Invoices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 10, 20));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Invoices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2025, 5, 6),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateOnly(2025, 5, 6));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ValidStartDate",
                table: "Invoices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 10, 20),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 6));

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreatedAt",
                table: "Invoices",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2024, 10, 20),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2025, 5, 6));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateOnly(2024, 10, 20));
        }
    }
}
