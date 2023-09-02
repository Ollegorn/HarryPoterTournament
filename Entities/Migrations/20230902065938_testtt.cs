using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class testtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentTournamentPoints",
                table: "AspNetUsers",
                newName: "Wins");

            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTournamentPoints",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TotalTournamentPoints",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Wins",
                table: "AspNetUsers",
                newName: "CurrentTournamentPoints");
        }
    }
}
