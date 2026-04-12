using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabakBlog.Models
{
    public class Post
    {
        //---------------------------
        [Key]
        public int id { get; set; }
        //---------------------------------------------------------
        [Required(ErrorMessage = "Başlık boş bırakılamaz!")]
        [MaxLength(200, ErrorMessage = "200 karaktere aşamazsınız!")]

        public string baslik { get; set; }
        //---------------------------------------------------
        [Required(ErrorMessage = "İçerik boş bırakılamaz!")]
        public string icerik { get; set; }
        //------------------------------------------------
        [Required(ErrorMessage = "Yazar boş bırakılamaz!")]
        [MaxLength(100, ErrorMessage = "100 karaktere aşamazsınız!")]
        public string yazar { get; set; }
        //-----------------------------------------------------
        [ValidateNever]
        public string resimUrl { get; set; }
        //---------------------------------------------------
        [Required]
        [DataType(DataType.Date)]
        public DateTime yayinTarih { get; set; } = DateTime.Now;
        //--------------------------------------------------

        //--------------------------------------
        [ForeignKey("Kategori")]
        [Required(ErrorMessage = "Lütfen bir kategori seçiniz.")]
        [DisplayName("Kategori")]
        public int? kategoriId { get; set; }

        [ValidateNever]
        public Kategori Kategori { get; set; }
        //---------------------------------------
        [ValidateNever]
        public ICollection<Yorum> Yorumlar { get; set; }



        [Required(ErrorMessage = "İngilizce başlık zorunludur!")]
        [MaxLength(200, ErrorMessage = "200 karakteri geçemez!")]
        public string baslik_EN { get; set; }

        [Required(ErrorMessage = "Rusça başlık zorunludur!")]
        [MaxLength(200, ErrorMessage = "200 karakteri geçemez!")]
        public string baslik_RU { get; set; }



        [Required(ErrorMessage = "İngilizce içerik zorunludur!")]
        public string icerik_EN { get; set; }

        [Required(ErrorMessage = "Rusça içerik zorunludur!")]
        public string icerik_RU { get; set; }

    }
}
