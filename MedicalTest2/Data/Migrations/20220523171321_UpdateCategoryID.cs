using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class UpdateCategoryID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
