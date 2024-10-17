using Contract_Monthly_Claim_System.Models2;
using Contract_Monthly_Claim_System2.Models;
using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Contract_Monthly_Claim_System2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ClaimDBContext _context;

        public HomeController(ILogger<HomeController> logger, ClaimDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Claim()
        {
            return View();
        }
        public IActionResult ClaimForm(CreateClaim model)
        {
            if (model.DocumentFile != null && model.DocumentFile.Length > 0)
            {
                var fileName = Path.GetFileName(model.DocumentFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", model.DocumentFile.FileName);

                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.DocumentFile.CopyTo(fileStream);
                }
                model.Document = $"/uploads/{fileName}";
            }
            _context.Claims.Add(model);
            _context.SaveChanges();

            return RedirectToAction("ClaimHub");
        }
        public IActionResult ClaimHub()
        {
            var allClaims = _context.Claims.ToList();
            return View(allClaims);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}