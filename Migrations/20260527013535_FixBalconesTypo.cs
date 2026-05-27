using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGUZ.Migrations
{
    /// <inheritdoc />
    public partial class FixBalconesTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BallconesSales",
                table: "DailyDeposits",
                newName: "BalconesSales");

            migrationBuilder.RenameColumn(
                name: "BallconesInvoices",
                table: "DailyDeposits",
                newName: "BalconesInvoices");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "AppUserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 26, 20, 35, 34, 455, DateTimeKind.Local).AddTicks(3151), "$2a$11$HkUc7J1/ghT.OAbumgNoh.JWB3.AqZu.7S8wa8UC9P/7DHZ1ZyOau" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BalconesSales",
                table: "DailyDeposits",
                newName: "BallconesSales");

            migrationBuilder.RenameColumn(
                name: "BalconesInvoices",
                table: "DailyDeposits",
                newName: "BallconesInvoices");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "AppUserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 5, 14, 4, 19, 16, 200, DateTimeKind.Local).AddTicks(4037), "$2a$11$dGaPh6X3Ixn75yBPhKC1g.x1CDeNEZI.jlYQbQ1aHe6/y7UDKsjCG" });
        }
    }
}
