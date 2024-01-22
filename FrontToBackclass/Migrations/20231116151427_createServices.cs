using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontToBack.Migrations
{
    /// <inheritdoc />
    public partial class createServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IimgUrl",
                table: "services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IimgUrl",
                table: "services");
        }
    }
}
