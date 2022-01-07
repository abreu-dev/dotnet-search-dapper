using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetSearch.Infra.Data.Migrations
{
    public partial class RenameLivroCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroCategorias");

            migrationBuilder.CreateTable(
                name: "LivroCategoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroCategoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivroCategoria_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroCategoria_Livro_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroCategoria_CategoriaId",
                table: "LivroCategoria",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_LivroCategoria_LivroId",
                table: "LivroCategoria",
                column: "LivroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroCategoria");

            migrationBuilder.CreateTable(
                name: "LivroCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivroCategorias_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroCategorias_Livro_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroCategorias_CategoriaId",
                table: "LivroCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_LivroCategorias_LivroId",
                table: "LivroCategorias",
                column: "LivroId");
        }
    }
}
