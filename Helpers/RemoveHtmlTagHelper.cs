using System.Net;
using System.Text.RegularExpressions;

namespace BabakBlog.Helpers
{
    public class RemoveHtmlTagHelper
    {
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            string step1 = Regex.Replace(input, "<.*?>", string.Empty);
            string step2 = WebUtility.HtmlDecode(step1);
            return step2.Trim();
        }
        
    }
}
