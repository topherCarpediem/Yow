using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YowServer.Migrations
{
    public partial class DeleteNonceField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nonce",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Nonce",
                table: "Messages",
                nullable: true);
        }
    }
}
