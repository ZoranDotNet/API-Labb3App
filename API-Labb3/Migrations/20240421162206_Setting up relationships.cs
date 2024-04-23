using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Labb3.Migrations
{
    /// <inheritdoc />
    public partial class Settinguprelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HobbyId",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Hobbies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Links_HobbyId",
                table: "Links",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_PersonId",
                table: "Hobbies",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Hobbies_HobbyId",
                table: "Links",
                column: "HobbyId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Hobbies_HobbyId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_HobbyId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Hobbies_PersonId",
                table: "Hobbies");

            migrationBuilder.DropColumn(
                name: "HobbyId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Hobbies");
        }
    }
}
