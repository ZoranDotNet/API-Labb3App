using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Labb3.Migrations
{
    /// <inheritdoc />
    public partial class Updatemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Hobbies_HobbyId",
                table: "Links");

            migrationBuilder.AlterColumn<int>(
                name: "HobbyId",
                table: "Links",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Hobbies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Hobbies_People_PersonId",
                table: "Hobbies",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Hobbies_HobbyId",
                table: "Links",
                column: "HobbyId",
                principalTable: "Hobbies",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "HobbyId",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Hobbies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
