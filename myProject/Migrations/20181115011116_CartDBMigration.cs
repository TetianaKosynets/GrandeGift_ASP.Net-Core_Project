using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace myProject.Migrations
{
    public partial class CartDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "TblCart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "TblCart");
        }
    }
}
