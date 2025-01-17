using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technexa.Migrations
{
    /// <inheritdoc />
    public partial class CategorieMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categorieDesignation",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "categorieid",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_categorieid",
                table: "Posts",
                column: "categorieid");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts",
                column: "categorieid",
                principalTable: "Categorie",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Posts_categorieid",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "categorieDesignation",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "categorieid",
                table: "Posts");
        }
    }
}
