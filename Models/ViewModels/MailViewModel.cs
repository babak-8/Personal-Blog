using System.ComponentModel.DataAnnotations;

namespace BabakBlog.Models.ViewModels
{
    public class MailViewModel
    {
        [Required(ErrorMessage = "adSoyisim")]
        [Display(Name = "Ad Soyad")]
        public string adSoyad { get; set; }

        [Required(ErrorMessage = "emailHata")]
        [EmailAddress(ErrorMessage = "emailGecerli")]
        [Display(Name = "E-posta")]
        public string email { get; set; }

        [Required(ErrorMessage = "konuHata")]
        [StringLength(100, ErrorMessage = "konuUzun")]
        public string konu { get; set; }

        [Required(ErrorMessage = "mesajHata")]
        [MinLength(10, ErrorMessage = "mesajKisa")]
        public string mesaj { get; set; }
    }
}
