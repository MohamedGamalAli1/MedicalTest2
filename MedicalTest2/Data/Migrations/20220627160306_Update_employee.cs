using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class Update_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "WorkType",
                table: "Employees",
                newName: "WorkTypeId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRetired",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_WorkTypeId",
                table: "Employees",
                column: "WorkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_WorkTypes_WorkTypeId",
                table: "Employees",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_WorkTypes_WorkTypeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_WorkTypeId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "WorkTypeId",
                table: "Employees",
                newName: "WorkType");

            migrationBuilder.AlterColumn<string>(
                name: "IsRetired",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
