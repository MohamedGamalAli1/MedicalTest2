using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class UpdateAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowNotification",
                table: "EmployeeAttachments");

            migrationBuilder.AddColumn<bool>(
                name: "AllowNotification",
                table: "Attachments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowNotification",
                table: "Attachments");

            migrationBuilder.AddColumn<bool>(
                name: "AllowNotification",
                table: "EmployeeAttachments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
