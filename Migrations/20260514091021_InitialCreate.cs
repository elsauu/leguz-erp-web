using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LEGUZ.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryRoutes",
                columns: table => new
                {
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRoutes", x => x.DeliveryRouteId);
                });

            migrationBuilder.CreateTable(
                name: "Salespersons",
                columns: table => new
                {
                    SalespersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salespersons", x => x.SalespersonId);
                    table.ForeignKey(
                        name: "FK_Salespersons_DeliveryRoutes_DeliveryRouteId",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "DeliveryRouteId");
                });

            migrationBuilder.InsertData(
                table: "DeliveryRoutes",
                columns: new[] { "DeliveryRouteId", "IsActive", "Name", "Number" },
                values: new object[,]
                {
                    { 1, true, "BALCONES RIOS", 1 },
                    { 2, true, "BALCONES LAGOS", 2 },
                    { 3, true, "LA JOYA", 3 },
                    { 4, true, "BUGAMBILIAS", 4 },
                    { 5, true, "ALMAGUER", 5 },
                    { 6, true, "LA JUAREZ", 6 },
                    { 7, true, "PASEO RINCON", 7 },
                    { 8, true, "VAMOS TAMAULIPAS", 8 },
                    { 9, true, "RANCHO GRANDE", 9 },
                    { 10, true, "PUERTA DEL SOL", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salespersons_DeliveryRouteId",
                table: "Salespersons",
                column: "DeliveryRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salespersons");

            migrationBuilder.DropTable(
                name: "DeliveryRoutes");
        }
    }
}
