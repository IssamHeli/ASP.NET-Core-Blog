using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technexa.Migrations
{
    /// <inheritdoc />
    public partial class newlastone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_categorieid",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "categorieid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "categorieDid",
                table: "Posts",
                newName: "categorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categorie",
                table: "Posts",
                newName: "categorieDid");

            migrationBuilder.AddColumn<string>(
                name: "categorieid",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_categorieid",
                table: "Posts",
                column: "categorieid");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts",
                column: "categorieid",
                principalTable: "Categorie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
