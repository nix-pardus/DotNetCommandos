using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrascructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "EffectiveTo",
                table: "ScheduleExceptions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EffectiveFrom",
                table: "ScheduleExceptions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "ScheduleExceptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExceptions_EmployeeId",
                table: "ScheduleExceptions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleExceptions_Employees_EmployeeId",
                table: "ScheduleExceptions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleExceptions_Employees_EmployeeId",
                table: "ScheduleExceptions");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleExceptions_EmployeeId",
                table: "ScheduleExceptions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ScheduleExceptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveTo",
                table: "ScheduleExceptions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveFrom",
                table: "ScheduleExceptions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
