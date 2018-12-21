using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace myProject.Migrations
{
    public partial class HamperNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TblCart_HamperId",
                table: "TblCart",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCart_TblHamper_HamperId",
                table: "TblCart",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCart_TblHamper_HamperId",
                table: "TblCart");

            migrationBuilder.DropIndex(
                name: "IX_TblCart_HamperId",
                table: "TblCart");
        }
    }
}
