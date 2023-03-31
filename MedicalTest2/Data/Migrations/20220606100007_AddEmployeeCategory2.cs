using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class AddEmployeeCategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeCategoryId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCategoryId",
                table: "Employees",
                column: "EmployeeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeCategories_EmployeeCategoryId",
                table: "Employees",
                column: "EmployeeCategoryId",
                principalTable: "EmployeeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeCategories_EmployeeCategoryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeCategoryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeCategoryId",
                table: "Employees");
        }
    }
}
