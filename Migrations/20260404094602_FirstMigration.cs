using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabakBlog.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isim = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yazar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    resimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    yayinTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    kategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Kategoriler_kategoriId",
                        column: x => x.kategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kullaniciAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    yorumTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    icerik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.id);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Posts_postId",
                        column: x => x.postId,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_kategoriId",
                table: "Posts",
                column: "kategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_postId",
                table: "Yorumlar",
                column: "postId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yorumlar");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
