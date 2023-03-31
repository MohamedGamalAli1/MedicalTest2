using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class Addsentdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsSent",
                table: "EmployeeAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentDate",
                table: "EmployeeAttachments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "EmployeeAttachments");

            migrationBuilder.DropColumn(
                name: "SentDate",
                table: "EmployeeAttachments");
        }
    }
}
