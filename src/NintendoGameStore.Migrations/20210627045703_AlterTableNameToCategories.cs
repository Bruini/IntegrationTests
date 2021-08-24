using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NintendoGameStore.Migrations
{
    public partial class AlterTableNameToCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGame_Categorys_CategoriesId",
                table: "CategoryGame");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGame_Categories_CategoriesId",
                table: "CategoryGame",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGame_Categories_CategoriesId",
                table: "CategoryGame");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.CategoryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_Name",
                table: "Categorys",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGame_Categorys_CategoriesId",
                table: "CategoryGame",
                column: "CategoriesId",
                principalTable: "Categorys",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
