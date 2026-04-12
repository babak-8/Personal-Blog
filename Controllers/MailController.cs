using BabakBlog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BabakBlog.Controllers
{
    public class MailController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MailViewModel em)
        {
            if (!ModelState.IsValid) return View(em);

            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add("babirapson19@gmail.com"); 
                mm.Subject = "Blog İletişim: " + em.konu;

                mm.Body = $"Gönderen: {em.adSoyad}\n" +
                          $"E-posta: {em.email}\n" +
                          $"Konu: {em.konu}\n\n" +
                          $"Mesaj:\n{em.mesaj}";

                mm.From = new MailAddress("babirapson19@gmail.com", "Blog Sistem");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("babirapson19@gmail.com", "lvmr oxim boex gess"); 

                await smtp.SendMailAsync(mm);

                ViewBag.Success = "Mesajınız başarıyla iletildi!";
                return View(new MailViewModel());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Hata: " + ex.Message;
                return View(em);
            }
        }
    }
}
