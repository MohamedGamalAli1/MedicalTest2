using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class addArchieveDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchieveDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ArchieveViewModelId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DateRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<DateTime>(type: "datetime2", nullable: true),
                    To = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendSMSDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendSMSDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchieveViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DateRangeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchieveViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchieveViewModel_DateRange_DateRangeId",
                        column: x => x.DateRangeId,
                        principalTable: "DateRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ArchieveViewModelId",
                table: "Categories",
                column: "ArchieveViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchieveViewModel_DateRangeId",
                table: "ArchieveViewModel",
                column: "DateRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ArchieveViewModel_ArchieveViewModelId",
                table: "Categories",
                column: "ArchieveViewModelId",
                principalTable: "ArchieveViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ArchieveViewModel_ArchieveViewModelId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "ArchieveViewModel");

            migrationBuilder.DropTable(
                name: "SendSMSDto");

            migrationBuilder.DropTable(
                name: "DateRange");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ArchieveViewModelId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ArchieveDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ArchieveViewModelId",
                table: "Categories");
        }
    }
}
