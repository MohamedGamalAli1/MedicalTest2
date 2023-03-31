using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class renamepatienttoemployee2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_CategoryId",
                table: "Employees",
                newName: "IX_Employees_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Categories_CategoryId",
                table: "Employees",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Categories_CategoryId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Patients");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CategoryId",
                table: "Patients",
                newName: "IX_Patients_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Categories_CategoryId",
                table: "Patients",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
