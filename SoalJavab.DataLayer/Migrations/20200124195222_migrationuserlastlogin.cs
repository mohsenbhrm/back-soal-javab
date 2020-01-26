using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoalJavab.DataLayer.Migrations
{
    public partial class migrationuserlastlogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "oldLoggedIn",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "oldLoggedIn",
                table: "Users");
        }
    }
}
