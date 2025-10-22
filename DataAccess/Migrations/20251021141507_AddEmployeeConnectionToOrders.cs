using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrascructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeConnectionToOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderEmployees_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_EmployeeId",
                table: "OrderEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_OrderId_EmployeeId",
                table: "OrderEmployees",
                columns: new[] { "OrderId", "EmployeeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_OrderId_IsPrimary",
                table: "OrderEmployees",
                columns: new[] { "OrderId", "IsPrimary" },
                unique: true,
                filter: "\"IsPrimary\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderEmployees");
        }
    }
}
