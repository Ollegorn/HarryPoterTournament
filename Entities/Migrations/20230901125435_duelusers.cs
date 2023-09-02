using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class duelusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Duels_DuelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DuelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DuelId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserOneId",
                table: "Duels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserTwoId",
                table: "Duels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Duels_UserOneId",
                table: "Duels",
                column: "UserOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Duels_UserTwoId",
                table: "Duels",
                column: "UserTwoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_AspNetUsers_UserOneId",
                table: "Duels",
                column: "UserOneId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Duels_AspNetUsers_UserTwoId",
                table: "Duels",
                column: "UserTwoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Duels_AspNetUsers_UserOneId",
                table: "Duels");

            migrationBuilder.DropForeignKey(
                name: "FK_Duels_AspNetUsers_UserTwoId",
                table: "Duels");

            migrationBuilder.DropIndex(
                name: "IX_Duels_UserOneId",
                table: "Duels");

            migrationBuilder.DropIndex(
                name: "IX_Duels_UserTwoId",
                table: "Duels");

            migrationBuilder.DropColumn(
                name: "UserOneId",
                table: "Duels");

            migrationBuilder.DropColumn(
                name: "UserTwoId",
                table: "Duels");

            migrationBuilder.AddColumn<Guid>(
                name: "DuelId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
