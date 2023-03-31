using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class removeArhieveViewModel_SMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ArchieveViewModel_ArchieveViewModelId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ArchieveViewModel_ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ArchieveViewModel");

            migrationBuilder.DropTable(
                name: "SendSMSDto");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ArchieveViewModelId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ArchieveViewModelId",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArchieveViewModelId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArchieveViewModelId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchieveViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatId = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchieveViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendSMSDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendSMSDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ArchieveViewModelId",
                table: "Employees",
                column: "ArchieveViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ArchieveViewModelId",
                table: "Categories",
                column: "ArchieveViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ArchieveViewModel_ArchieveViewModelId",
                table: "Categories",
                column: "ArchieveViewModelId",
                principalTable: "ArchieveViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ArchieveViewModel_ArchieveViewModelId",
                table: "Employees",
                column: "ArchieveViewModelId",
                principalTable: "ArchieveViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
