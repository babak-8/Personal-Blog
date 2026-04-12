using System.ComponentModel.DataAnnotations;

namespace BabakBlog.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "emailHata")]
        [EmailAddress(ErrorMessage = "emailFormat")]
        public string email { get; set; }

        [Required(ErrorMessage = "sifreHata")]
        [DataType(DataType.Password)]
        public string sifre { get; set; }
    }
}
