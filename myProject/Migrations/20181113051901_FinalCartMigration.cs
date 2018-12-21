using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace myProject.Migrations
{
    public partial class FinalCartMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HamperId",
                table: "TblCart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper",
                column: "CartId",
                principalTable: "TblCart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper");

            migrationBuilder.DropIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "TblHamper");

            migrationBuilder.AddColumn<int>(
                name: "HamperId",
                table: "TblCart",
                nullable: false,
                defaultValue: 0);
        }
    }
}
