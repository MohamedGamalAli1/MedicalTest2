using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class removedateRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchieveViewModel_DateRange_DateRangeId",
                table: "ArchieveViewModel");

            migrationBuilder.DropTable(
                name: "DateRange");

            migrationBuilder.DropIndex(
                name: "IX_ArchieveViewModel_DateRangeId",
                table: "ArchieveViewModel");

            migrationBuilder.DropColumn(
                name: "DateRangeId",
                table: "ArchieveViewModel");

            migrationBuilder.AddColumn<int>(
                name: "ArchieveViewModelId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "ArchieveViewModel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "ArchieveViewModel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ArchieveViewModelId",
                table: "Employees",
                column: "ArchieveViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchieveViewModel_CategoryId",
                table: "ArchieveViewModel",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchieveViewModel_Categories_CategoryId",
                table: "ArchieveViewModel",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ArchieveViewModel_ArchieveViewModelId",
                table: "Employees",
                column: "ArchieveViewModelId",
                principalTable: "ArchieveViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchieveViewModel_Categories_CategoryId",
                table: "ArchieveViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ArchieveViewModel_ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_ArchieveViewModel_CategoryId",
                table: "ArchieveViewModel");

            migrationBuilder.DropColumn(
                name: "ArchieveViewModelId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "From",
                table: "ArchieveViewModel");

            migrationBuilder.DropColumn(
                name: "To",
                table: "ArchieveViewModel");

            migrationBuilder.AddColumn<int>(
                name: "DateRangeId",
                table: "ArchieveViewModel",
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

            migrationBuilder.CreateIndex(
                name: "IX_ArchieveViewModel_DateRangeId",
                table: "ArchieveViewModel",
                column: "DateRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchieveViewModel_DateRange_DateRangeId",
                table: "ArchieveViewModel",
                column: "DateRangeId",
                principalTable: "DateRange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
