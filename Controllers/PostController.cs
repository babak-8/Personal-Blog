using BabakBlog.Data;
using BabakBlog.Models;
using BabakBlog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BabakBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _allowedExtension = { ".jpg", ".jpeg", ".png" };

        private readonly IStringLocalizer<PostController> _localizer;

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IStringLocalizer<PostController> localizer)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

            _localizer = localizer;
        }

        public IActionResult Hakkimda()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult Index(int? kategoriId)
        {
            var postQuery = _context.Posts
                .Include(x=>x.Kategori)
                .Include(p => p.Yorumlar)
                .AsQueryable();
            if (kategoriId.HasValue)
            {
                postQuery = postQuery.Where(p => p.kategoriId == kategoriId.Value);
            }
            var posts = postQuery.ToList();

            ViewBag.Kategoriler = _context.Kategoriler.ToList();
            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            var postFromDb = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.id == editViewModel.Post.id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            if (editViewModel.resimUrl != null)
            {
                var inputFileExtension = Path.GetExtension(editViewModel.resimUrl.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtension);

                if (!isAllowed)
                {
                    ModelState.AddModelError("", "Sadece .jpg, .jpeg ve .png uzantılı dosyalar yüklenebilir!");
                    return View(editViewModel);
                }

                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Path.GetFileName(postFromDb.resimUrl));

                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }

                editViewModel.Post.resimUrl = await UploadFiletoFolder(editViewModel.resimUrl);
            }
            else
            {
                editViewModel.Post.resimUrl = postFromDb.resimUrl;
            }
            _context.Posts.Update(editViewModel.Post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string lang = "tr")
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Kategori)
                .Include(p => p.Yorumlar)
                .FirstOrDefaultAsync(p => p.id == id); 

            if (post == null)
            {
                return NotFound();
            }

            ViewBag.Lang = lang;

            return View(post);
        }

        [Authorize(Roles = "User")]
        public JsonResult AddComment([FromBody]Yorum yorum)
        {
            yorum.yorumTarih = DateTime.Now;
            _context.Yorumlar.Add(yorum);
            _context.SaveChanges();

            return Json(new
            {
                username = yorum.kullaniciAdi,
                commentDate = yorum.yorumTarih.ToString("dd MMMM yyyy"),
                content = yorum.icerik
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.id == id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            EditViewModel editViewModel = new EditViewModel
            {
                Post = postFromDb,
                Kategoriler = _context.Kategoriler.Select(c =>
                new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.isim
                }
            ).ToList()
            };

            return View(editViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Kategoriler = _context.Kategoriler
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.isim
                })
            .ToList();


            return View(postViewModel);
        }
 
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {

            if (ModelState.IsValid)
            {
                var inputFileExtension = Path.GetExtension(postViewModel.resimUrl.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtension);

                if (!isAllowed)
                {
                    ModelState.AddModelError("", "Sadece .jpg, .jpeg ve .png uzantılı dosyalar yüklenebilir!");
                    return View(postViewModel);
                }

                postViewModel.Post.resimUrl = await UploadFiletoFolder(postViewModel.resimUrl);


              

                await _context.Posts.AddAsync(postViewModel.Post);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        public async Task<string> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName).ToLower();
            var fileName = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var imagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, fileName);
            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                return "Dosya yüklenirken bir hata oluştu: " + ex.Message;
            }
            return "/images/" + fileName;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.id == id);
            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(postFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.id == id);

            if (string.IsNullOrEmpty(postFromDb.resimUrl))
            {
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Path.GetFileName(postFromDb.resimUrl));

                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }
            _context.Posts.Remove(postFromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        /************************************************************************************/

        [HttpGet]
        public IActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KategoriEkle(Kategori kategori)
        {
            if (!string.IsNullOrEmpty(kategori.isim))
            {
                kategori.id = 0; 

                _context.Kategoriler.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction("Kategoriler");
            }
            ModelState.AddModelError("isim", "Kategori adı boş olamaz!");
            return View(kategori);
        }

        public IActionResult Kategoriler()
        {
            var kategoriler = _context.Kategoriler.ToList();
            return View(kategoriler);
        }

        [HttpGet]
        public async Task<IActionResult> KategoriSil(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);

            if (kategori == null)
                return NotFound();

            return View(kategori);
        }

        [HttpPost]
        public async Task<IActionResult> KategoriSil(Kategori model)
        {
            var kategori = await _context.Kategoriler.FindAsync(model.id);

            if (kategori == null)
                return NotFound();

            _context.Kategoriler.Remove(kategori);
            await _context.SaveChangesAsync();

            return RedirectToAction("Kategoriler");
        }

        [HttpGet]
        public async Task<IActionResult> KategoriEdit(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);

            if (kategori == null)
                return NotFound();

            return View(kategori);
        }

        [HttpPost]
        public async Task<IActionResult> KategoriEdit(Kategori model)
        {
            ModelState.Remove("Posts");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var kategoriFromDb = await _context.Kategoriler.FindAsync(model.id);

            if (kategoriFromDb == null)
                return NotFound();

            kategoriFromDb.isim = model.isim;
            kategoriFromDb.isim_EN = model.isim_EN;
            kategoriFromDb.isim_RU = model.isim_RU;

            await _context.SaveChangesAsync();

            return RedirectToAction("Kategoriler");
        }

        /************************************************************************************/

        [HttpGet]
        public async Task<IActionResult> YorumlarYonetimi()
        {
            var yorumlar = await _context.Yorumlar.Include(y => y.Post).OrderByDescending(y => y.yorumTarih).ToListAsync();
            return View(yorumlar);
        }

        [HttpGet]
        public async Task<IActionResult> YorumSil(int id)
        {
            var yorum = await _context.Yorumlar
                .Include(y => y.Post)
                .FirstOrDefaultAsync(y => y.id == id);

            if (yorum == null)
                return NotFound();

            return View(yorum);
        }

        [HttpPost]
        public async Task<IActionResult> YorumSil(Yorum model)
        {
            var yorum = await _context.Yorumlar.FindAsync(model.id);

            if (yorum == null)
                return NotFound();

            _context.Yorumlar.Remove(yorum);
            await _context.SaveChangesAsync();
            return RedirectToAction("YorumlarYonetimi");
        }


        private void CopyToAsync(FileStream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
