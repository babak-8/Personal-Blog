using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabakBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddPostContentMultiLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icerik_EN",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "icerik_RU",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "icerik_EN", "icerik_RU" },
                values: new object[] { "C# is an object-oriented programming language developed by Microsoft. With C#, you can develop desktop applications, web applications, mobile applications, and games.", "C# - это объектно-ориентированный язык программирования, разработанный Microsoft. С помощью C# вы можете разрабатывать настольные приложения, веб-приложения, мобильные приложения и игры." });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "icerik_EN", "icerik_RU" },
                values: new object[] { "Push-ups, dips, diamond push-ups", "Отжимания, брусья, алмазные отжимания" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icerik_EN",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "icerik_RU",
                table: "Posts");
        }
    }
}
