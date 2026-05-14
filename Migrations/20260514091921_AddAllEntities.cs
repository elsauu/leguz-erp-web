using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LEGUZ.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalespersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_AppUsers_Salespersons_SalespersonId",
                        column: x => x.SalespersonId,
                        principalTable: "Salespersons",
                        principalColumn: "SalespersonId");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CustomerType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_DeliveryRoutes_DeliveryRouteId",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "DeliveryRouteId");
                });

            migrationBuilder.CreateTable(
                name: "DailyDeposits",
                columns: table => new
                {
                    DailyDepositId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CumbresSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BallconesSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaseoSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CumbresInvoices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BallconesInvoices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaseoInvoices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerInvoices = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardPayments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalToDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDeposits", x => x.DailyDepositId);
                });

            migrationBuilder.CreateTable(
                name: "DailyRouteRecords",
                columns: table => new
                {
                    DailyRouteRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    LoadHotKilos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadHalfKilo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadTacoKilo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadPackages = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadHalfPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadTacoPackage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadChips = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadDough = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadCrackling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadPresentation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TableHotKilos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TablePackages = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TableChips = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TableDough = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TableCrackling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KilosSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    KilosReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagesSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackagesReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChipsSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChipsReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoughSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DoughReturned = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CracklingsSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortageKilos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShortagePackages = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalespersonSigned = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: false),
                    SalespersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRouteRecords", x => x.DailyRouteRecordId);
                    table.ForeignKey(
                        name: "FK_DailyRouteRecords_DeliveryRoutes_DeliveryRouteId",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "DeliveryRouteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyRouteRecords_Salespersons_SalespersonId",
                        column: x => x.SalespersonId,
                        principalTable: "Salespersons",
                        principalColumn: "SalespersonId");
                });

            migrationBuilder.CreateTable(
                name: "DailySalesRecords",
                columns: table => new
                {
                    DailySalesRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bills = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Coins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashShortage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Expenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: false),
                    SalespersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySalesRecords", x => x.DailySalesRecordId);
                    table.ForeignKey(
                        name: "FK_DailySalesRecords_DeliveryRoutes_DeliveryRouteId",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "DeliveryRouteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailySalesRecords_Salespersons_SalespersonId",
                        column: x => x.SalespersonId,
                        principalTable: "Salespersons",
                        principalColumn: "SalespersonId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "CreditNotes",
                columns: table => new
                {
                    CreditNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolioNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Packages = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Kilos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Dough = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Crackling = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Chips = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsBarbacoa = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalespersonId = table.Column<int>(type: "int", nullable: true),
                    DeliveryRouteId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNotes", x => x.CreditNoteId);
                    table.ForeignKey(
                        name: "FK_CreditNotes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_CreditNotes_DeliveryRoutes_DeliveryRouteId",
                        column: x => x.DeliveryRouteId,
                        principalTable: "DeliveryRoutes",
                        principalColumn: "DeliveryRouteId");
                    table.ForeignKey(
                        name: "FK_CreditNotes_Salespersons_SalespersonId",
                        column: x => x.SalespersonId,
                        principalTable: "Salespersons",
                        principalColumn: "SalespersonId");
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserId", "CreatedAt", "FullName", "IsActive", "LastLogin", "PasswordHash", "Role", "SalespersonId", "Username" },
                values: new object[] { 1, new DateTime(2026, 5, 14, 4, 19, 16, 200, DateTimeKind.Local).AddTicks(4037), "Administrador", true, null, "$2a$11$dGaPh6X3Ixn75yBPhKC1g.x1CDeNEZI.jlYQbQ1aHe6/y7UDKsjCG", "ADMIN", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_SalespersonId",
                table: "AppUsers",
                column: "SalespersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_CustomerId",
                table: "CreditNotes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_DeliveryRouteId",
                table: "CreditNotes",
                column: "DeliveryRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditNotes_SalespersonId",
                table: "CreditNotes",
                column: "SalespersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeliveryRouteId",
                table: "Customers",
                column: "DeliveryRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDeposits_Date",
                table: "DailyDeposits",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyRouteRecords_Date_DeliveryRouteId",
                table: "DailyRouteRecords",
                columns: new[] { "Date", "DeliveryRouteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyRouteRecords_DeliveryRouteId",
                table: "DailyRouteRecords",
                column: "DeliveryRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyRouteRecords_SalespersonId",
                table: "DailyRouteRecords",
                column: "SalespersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesRecords_Date_DeliveryRouteId",
                table: "DailySalesRecords",
                columns: new[] { "Date", "DeliveryRouteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesRecords_DeliveryRouteId",
                table: "DailySalesRecords",
                column: "DeliveryRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySalesRecords_SalespersonId",
                table: "DailySalesRecords",
                column: "SalespersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "CreditNotes");

            migrationBuilder.DropTable(
                name: "DailyDeposits");

            migrationBuilder.DropTable(
                name: "DailyRouteRecords");

            migrationBuilder.DropTable(
                name: "DailySalesRecords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
