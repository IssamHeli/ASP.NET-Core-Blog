using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technexa.Migrations
{
    /// <inheritdoc />
    public partial class srcimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Srcimage",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Srcimage",
                table: "Posts");
        }
    }
}
