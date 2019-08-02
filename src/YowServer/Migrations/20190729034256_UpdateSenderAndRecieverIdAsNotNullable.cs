using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YowServer.Migrations
{
    public partial class UpdateSenderAndRecieverIdAsNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RecieverId",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "RecieverId",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
