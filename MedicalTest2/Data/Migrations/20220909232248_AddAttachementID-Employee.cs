using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class AddAttachementIDEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AttachmentId",
                table: "Employees",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Attachments_AttachmentId",
                table: "Employees",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Attachments_AttachmentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AttachmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Employees");
        }
    }
}
