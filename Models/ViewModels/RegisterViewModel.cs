using System.ComponentModel.DataAnnotations;

namespace BabakBlog.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "emailHata")]
        [EmailAddress(ErrorMessage = "emailFormat")]
        public string email { get; set; }

        [Required(ErrorMessage = "sifreHata")]
        [DataType(DataType.Password)]
        public string sifre { get; set; }

        /*[Compare("sifre", ErrorMessage = "Şifre ile aynı olmak zorunda!")]
        [Required(ErrorMessage = "Şifre tekrar boş bırakılamaz!")]
        [DataType(DataType.Password)]
        public string sifreOnay { get; set; }*/

        [Required(ErrorMessage = "sifreOnayHata")]
        [Compare("sifre", ErrorMessage = "sifreAyniHata")]
        [DataType(DataType.Password)]
        public string sifreOnay { get; set; }
    }
}
