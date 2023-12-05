﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarriageAgency.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameToClient : Migration
    {
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_UserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_UserId",
                table: "Clients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
