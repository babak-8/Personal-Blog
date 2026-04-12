using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabakBlog.Models
{
    public class Yorum
    {
        [Key]
        public int id { get; set; }
        //---------------------------------------------------
        [Required(ErrorMessage = "Kullanıcı Adı boş bırakılamaz!")]
        [MaxLength(100, ErrorMessage = "100 karaktere aşılamaz!")]
        public string kullaniciAdi { get; set; }
        //-------------------------------------------------------
        [DataType(DataType.Date)]
        public DateTime yorumTarih { get; set; }
        //----------------------------------------------------------
        [Required(ErrorMessage = "İçerik boş bırakılamaz!")]
        public string icerik { get; set; }



        //-------------------------------
        [ForeignKey("Post")]
        public int postId { get; set; }
        public Post Post { get; set; }
        //-------------------------------
    }
}
