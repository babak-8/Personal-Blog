using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BabakBlog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKategoriSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isim_EN",
                table: "Kategoriler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "isim_RU",
                table: "Kategoriler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "isim_EN", "isim_RU" },
                values: new object[] { "Software", "Программирование" });

            migrationBuilder.UpdateData(
                table: "Kategoriler",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "isim_EN", "isim_RU" },
                values: new object[] { "Sports", "Спорт" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "icerik", "icerik_EN", "icerik_RU" },
                values: new object[] { "Özgür beden hareketleriyle dolu, görsel olarak büyüleyici disiplin calisthenics, sokakların ve parkların sanatı olarak da bilinir. Videolarda gördüğünüz o akıl almaz jimnastik hareketleri, aslında bu antik sporun modern bir yorumudur. Sadece bir bar ve vücut ağırlığınızla neler yapılabileceğine dair sınırları zorlayan estetik hareketler gerçekten izleyenleri hayran bırakır. Şehir parklarında ya da spor alanlarında demirlere asılı şekilde akrobatik hareketler yapan insanları gördüyseniz, siz de calisthenics’in popülerliğine tanık olmuşsunuz demektir. Hem güç hem de estetiği bir araya getiren spor, fiziksel yeteneklerin sadece spor salonlarında değil, açık havada da nasıl sergilenebileceğinin harika bir örneğidir. Gelin, en dikkat çekici sporlardan biri olan calisthenics’in detaylarına birlikte bakalım.", "Calisthenics, a visually captivating discipline filled with free body movement, is also known as the art of the streets and parks. The incredible gymnastic movements you see in videos are actually a modern interpretation of this ancient sport. The aesthetic movements, pushing the boundaries of what can be done with just a bar and your body weight, truly amaze viewers. If you've seen people performing acrobatic movements hanging from railings in city parks or sports fields, you've witnessed the popularity of calisthenics. Combining both strength and aesthetics, the sport is a great example of how physical abilities can be showcased not only in gyms but also outdoors. Let's take a closer look at the details of calisthenics, one of the most striking sports.", "Калистеника, визуально захватывающая дисциплина, наполненная свободой движений тела, также известна как искусство улиц и парков. Невероятные гимнастические движения, которые вы видите на видео, на самом деле являются современной интерпретацией этого древнего вида спорта. Эстетические движения, расширяющие границы того, что можно сделать, используя только перекладину и собственный вес, действительно поражают зрителей. Если вы видели, как люди выполняют акробатические движения, вися на перилах в городских парках или на спортивных площадках, вы стали свидетелем популярности калистеники. Сочетая силу и эстетику, этот вид спорта является прекрасным примером того, как физические способности могут быть продемонстрированы не только в спортзалах, но и на открытом воздухе. Давайте подробнее рассмотрим калистенику, один из самых впечатляющих видов спорта." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isim_EN",
                table: "Kategoriler");

            migrationBuilder.DropColumn(
                name: "isim_RU",
                table: "Kategoriler");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "icerik", "icerik_EN", "icerik_RU" },
                values: new object[] { "Şınav,dips,diamond şınav", "Push-ups, dips, diamond push-ups", "Отжимания, брусья, алмазные отжимания" });
        }
    }
}
