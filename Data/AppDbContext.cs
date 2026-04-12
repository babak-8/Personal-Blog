using BabakBlog.Controllers;
using BabakBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BabakBlog.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Kategori>().HasData(
                new Kategori { id = 1, isim = "Yazılım", isim_EN = "Software", isim_RU = "Программирование"},
                new Kategori { id = 2, isim = "Spor", isim_EN = "Sports", isim_RU = "Спорт"}
                );

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    id = 1,
                    baslik = "C# Nedir?",
                    baslik_RU = "Что такое C#?",
                    baslik_EN = "What is C#?",
                    icerik = "C#, Microsoft tarafından geliştirilen, nesne yönelimli bir programlama dilidir. C# ile masaüstü uygulamaları, web uygulamaları, mobil uygulamalar ve oyunlar geliştirebilirsiniz.",
                    icerik_EN = "C# is an object-oriented programming language developed by Microsoft. With C#, you can develop desktop applications, web applications, mobile applications, and games.",
                    icerik_RU = "C# - это объектно-ориентированный язык программирования, разработанный Microsoft. С помощью C# вы можете разрабатывать настольные приложения, веб-приложения, мобильные приложения и игры.",
                    yazar = "Babak Pourkar",
                    resimUrl = "resim1.jpg",
                    yayinTarih = new DateTime(2026, 5, 1),
                    kategoriId = 1,
                },
                new Post
                {
                    id = 2,
                    baslik = "Calisthenics Göğüs Hareketleri",
                    baslik_EN = "Calisthenics Chest Exercises",
                    baslik_RU = "Упражнения для груди в калистенике",
                    icerik = "Özgür beden hareketleriyle dolu, görsel olarak büyüleyici disiplin calisthenics, sokakların ve parkların sanatı olarak da bilinir. Videolarda gördüğünüz o akıl almaz jimnastik hareketleri, aslında bu antik sporun modern bir yorumudur. Sadece bir bar ve vücut ağırlığınızla neler yapılabileceğine dair sınırları zorlayan estetik hareketler gerçekten izleyenleri hayran bırakır. Şehir parklarında ya da spor alanlarında demirlere asılı şekilde akrobatik hareketler yapan insanları gördüyseniz, siz de calisthenics’in popülerliğine tanık olmuşsunuz demektir. Hem güç hem de estetiği bir araya getiren spor, fiziksel yeteneklerin sadece spor salonlarında değil, açık havada da nasıl sergilenebileceğinin harika bir örneğidir. Gelin, en dikkat çekici sporlardan biri olan calisthenics’in detaylarına birlikte bakalım.",
                    icerik_EN = "Calisthenics, a visually captivating discipline filled with free body movement, is also known as the art of the streets and parks. The incredible gymnastic movements you see in videos are actually a modern interpretation of this ancient sport. The aesthetic movements, pushing the boundaries of what can be done with just a bar and your body weight, truly amaze viewers. If you've seen people performing acrobatic movements hanging from railings in city parks or sports fields, you've witnessed the popularity of calisthenics. Combining both strength and aesthetics, the sport is a great example of how physical abilities can be showcased not only in gyms but also outdoors. Let's take a closer look at the details of calisthenics, one of the most striking sports.",
                    icerik_RU = "Калистеника, визуально захватывающая дисциплина, наполненная свободой движений тела, также известна как искусство улиц и парков. Невероятные гимнастические движения, которые вы видите на видео, на самом деле являются современной интерпретацией этого древнего вида спорта. Эстетические движения, расширяющие границы того, что можно сделать, используя только перекладину и собственный вес, действительно поражают зрителей. Если вы видели, как люди выполняют акробатические движения, вися на перилах в городских парках или на спортивных площадках, вы стали свидетелем популярности калистеники. Сочетая силу и эстетику, этот вид спорта является прекрасным примером того, как физические способности могут быть продемонстрированы не только в спортзалах, но и на открытом воздухе. Давайте подробнее рассмотрим калистенику, один из самых впечатляющих видов спорта.",
                    yazar = "Babak Pourkar",
                    resimUrl = "resim2.jpg",
                    yayinTarih = new DateTime(2026, 5, 10),
                    kategoriId = 2,
                }
                );
        }
    }
}
