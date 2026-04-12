using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BabakBlog.Models
{
    public class Kategori
    {
        [Key]
        public int id { get; set; }
        //--------------------------------------------------
        [Required(ErrorMessage = "İsim boş bırakılamaz! ")]
        [MaxLength(100, ErrorMessage = "100 karaktere aşılamaz!")]
        public string isim { get; set; }
        //--------------------------------------------------
        public string? aciklama { get; set; }
        //--------------------------------------------------


        public ICollection<Post> Posts { get; set; }


        [Required(ErrorMessage = "İngilizce kategori adı zorunludur!")]
        [MaxLength(100, ErrorMessage = "100 karakteri aşamaz!")]
        public string isim_EN { get; set; }

        [Required(ErrorMessage = "Rusça kategori adı zorunludur!")]
        [MaxLength(100, ErrorMessage = "100 karakteri aşamaz!")]
        public string isim_RU { get; set; }
    }
}
