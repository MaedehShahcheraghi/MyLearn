using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLearn.DataLayer.Migrations
{
    public partial class AddFildInAnser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAnswer",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAnswer",
                table: "Answers");
        }
    }
}
