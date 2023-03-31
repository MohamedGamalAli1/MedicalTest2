using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class create_gender_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Employees",
                newName: "GenderId");

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Genders_GenderId",
                table: "Employees",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Genders_GenderId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GenderId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Employees",
                newName: "Gender");
        }
    }
}
