using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok_Temp.Migrations
{
    /// <inheritdoc />
    public partial class tablesareupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_ParentCategory_ParentCategoryId",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentCategory",
                table: "ParentCategory");

            migrationBuilder.RenameTable(
                name: "ParentCategory",
                newName: "parentcats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_parentcats",
                table: "parentcats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_parentcats_ParentCategoryId",
                table: "categories",
                column: "ParentCategoryId",
                principalTable: "parentcats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_parentcats_ParentCategoryId",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_parentcats",
                table: "parentcats");

            migrationBuilder.RenameTable(
                name: "parentcats",
                newName: "ParentCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentCategory",
                table: "ParentCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_ParentCategory_ParentCategoryId",
                table: "categories",
                column: "ParentCategoryId",
                principalTable: "ParentCategory",
                principalColumn: "Id");
        }
    }
}
