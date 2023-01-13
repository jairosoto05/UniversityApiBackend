using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApiBackend.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKCOURSESCATEGORIESCategoryId",
                table: "COURSES");

            migrationBuilder.AddForeignKey(
                name: "FKCOURSESCATEGORIESCategoryId",
                table: "COURSES",
                column: "CategoryId",
                principalTable: "CATEGORIES",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKCOURSESCATEGORIESCategoryId",
                table: "COURSES");

            migrationBuilder.AddForeignKey(
                name: "FKCOURSESCATEGORIESCategoryId",
                table: "COURSES",
                column: "CategoryId",
                principalTable: "CATEGORIES",
                principalColumn: "Id");
        }
    }
}
