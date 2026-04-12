using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BabakBlog.Models.ViewModels
{
    public class EditViewModel
    {
        public Post Post { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Kategoriler { get; set; }

        [ValidateNever]
        public IFormFile resimUrl { get; set; }
    }
}
