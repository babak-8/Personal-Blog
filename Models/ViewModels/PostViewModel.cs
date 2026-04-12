using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BabakBlog.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IFormFile resimUrl { get; set; }


      
    }
}
