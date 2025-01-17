using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technexa.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "categorieDesignation",
                table: "Posts",
                newName: "categorieDid");

            migrationBuilder.AlterColumn<string>(
                name: "categorieid",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts",
                column: "categorieid",
                principalTable: "Categorie",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "categorieDid",
                table: "Posts",
                newName: "categorieDesignation");

            migrationBuilder.AlterColumn<string>(
                name: "categorieid",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categorie_categorieid",
                table: "Posts",
                column: "categorieid",
                principalTable: "Categorie",
                principalColumn: "id");
        }
    }
}
