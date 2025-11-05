using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrascructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderEmployeeIndexesWithSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_OrderId_EmployeeId",
                table: "OrderEmployees");

            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_OrderId_IsPrimary",
                table: "OrderEmployees");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_OrderId_EmployeeId",
                table: "OrderEmployees",
                columns: new[] { "OrderId", "EmployeeId" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEmployees_OrderId_IsPrimary",
                table: "OrderEmployees",
                columns: new[] { "OrderId", "IsPrimary" },
                unique: true,
                filter: "\"IsPrimary\" = true AND \"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_OrderId_EmployeeId",
                table: "OrderEmployees");

            migrationBuilder.DropIndex(
                name: "IX_OrderEmployees_OrderId_IsPrimary",
                table: "OrderEmployees");

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
    }
}
