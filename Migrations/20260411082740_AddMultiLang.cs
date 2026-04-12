using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabakBlog.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiLang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "baslik_EN",
                table: "Posts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "baslik_RU",
                table: "Posts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "baslik_EN", "baslik_RU" },
                values: new object[] { "What is C#?", "Что такое C#?" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "baslik_EN", "baslik_RU" },
                values: new object[] { "Calisthenics Chest Exercises", "Упражнения для груди в калистенике" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "baslik_EN",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "baslik_RU",
                table: "Posts");
        }
    }
}
