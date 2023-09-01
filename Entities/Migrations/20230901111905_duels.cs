using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class duels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DuelId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Duels",
                columns: table => new
                {
                    DuelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duels", x => x.DuelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DuelId",
                table: "AspNetUsers",
                column: "DuelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Duels_DuelId",
                table: "AspNetUsers",
                column: "DuelId",
                principalTable: "Duels",
                principalColumn: "DuelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Duels_DuelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Duels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DuelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DuelId",
                table: "AspNetUsers");
        }
    }
}
