using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace myProject.Migrations
{
    public partial class HamperMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hamper_TblCategory_CategoryId",
                table: "Hamper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hamper",
                table: "Hamper");

            migrationBuilder.RenameTable(
                name: "Hamper",
                newName: "TblHamper");

            migrationBuilder.RenameIndex(
                name: "IX_Hamper_CategoryId",
                table: "TblHamper",
                newName: "IX_TblHamper_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblHamper",
                table: "TblHamper",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCategory_CategoryId",
                table: "TblHamper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblHamper",
                table: "TblHamper");

            migrationBuilder.RenameTable(
                name: "TblHamper",
                newName: "Hamper");

            migrationBuilder.RenameIndex(
                name: "IX_TblHamper_CategoryId",
                table: "Hamper",
                newName: "IX_Hamper_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hamper",
                table: "Hamper",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hamper_TblCategory_CategoryId",
                table: "Hamper",
                column: "CategoryId",
                principalTable: "TblCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
