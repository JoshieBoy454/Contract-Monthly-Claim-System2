using Contract_Monthly_Claim_System.Models2;
using Contract_Monthly_Claim_System2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            _context.Claims.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Claim");// to change to a page that will display the claims submitted by specific lecturer
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