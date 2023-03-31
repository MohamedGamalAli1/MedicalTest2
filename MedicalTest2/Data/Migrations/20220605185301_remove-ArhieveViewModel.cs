using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalTest2.Data.Migrations
{
    public partial class removeArhieveViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchieveViewModel_Categories_CategoryId",
                table: "ArchieveViewModel");

            migrationBuilder.DropIndex(
                name: "IX_ArchieveViewModel_CategoryId",
                table: "ArchieveViewModel");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ArchieveViewModel",
                newName: "CatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CatId",
                table: "ArchieveViewModel",
                newName: "CategoryId");

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
        }
    }
}
