using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BabakBlog.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInıtialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kategoriler",
                columns: new[] { "id", "aciklama", "isim" },
                values: new object[,]
                {
                    { 1, null, "Yazılım" },
                    { 2, null, "Spor" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "id", "baslik", "icerik", "kategoriId", "resimUrl", "yayinTarih", "yazar" },
                values: new object[,]
                {
                    { 1, "C# Nedir?", "C#, Microsoft tarafından geliştirilen, nesne yönelimli bir programlama dilidir. C# ile masaüstü uygulamaları, web uygulamaları, mobil uygulamalar ve oyunlar geliştirebilirsiniz.", 1, "resim1.jpg", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Babak Pourkar" },
                    { 2, "Calisthenics Göğüs Hareketleri", "Şınav,dips,diamond şınav", 2, "resim2.jpg", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Babak Pourkar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Kategoriler",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kategoriler",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
