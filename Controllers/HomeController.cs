using Contract_Monthly_Claim_System2.Models;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DinkToPdf;
using DinkToPdf.Contracts;

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
        public IActionResult ValidateLecturer(Role Roles)
        {
            var lecturerUsername = "lecturer";
            var lecturerPassword = "lecturer";

            if (Roles.username == lecturerUsername && Roles.password == lecturerPassword)
            {
                Role.Roles currentRoles = Role.Roles.Lecturer;
                ViewBag.currentRoles = currentRoles;
                return View("Claim");
            } 
            else
            {
                return View("Failedlogin");
            }
        }

        public IActionResult ValidateManager(Role Roles) 
        {
            var adminUsername = "admin";
            var adminPassword = "admin";

            if (Roles.username == adminUsername && Roles.password == adminPassword)
            {
                Role.Roles currentRoles = Role.Roles.Admin;
                ViewBag.currentRoles = currentRoles;
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Failedlogin");
            }
        }

        public IActionResult ValidateHR(Role Roles) 
        {
            var hrUsername = "hr";
            var hrPassword = "hr";

            if (Roles.username == hrUsername && Roles.password == hrPassword)
            {
                Role.Roles currentRoles = Role.Roles.HR;
                ViewBag.currentRoles = currentRoles;
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Failedlogin");
            }
        }

        public IActionResult LoginLecturer()
        {

            return View("LecturerLogin");
        }

        public IActionResult LoginManager()
        {

            return View("ManagerLogin");
        }

        public IActionResult LoginHR()
        {

            return View("HRLogin");
        }

        public IActionResult Manage()
        {
            try
            {
                var claims = _context.Claims.ToList();
                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving claims.");
                return View("Error");
            }
        }

        [Route("Home/ManageClaim/{id}")]
        public IActionResult ManageClaim(int id)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ID == id);
                if (claim == null)
                {
                    _logger.LogWarning($"Claim with ID {id} not found.");
                    return View("Manage");
                }
                return View(claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the claim.");
                return View("Error");
            }
        }
        
        public IActionResult ApproveClaim(int id)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ID == id);
                if (claim != null)
                {
                        claim.Approval = 1;
                        _context.SaveChanges();
                }
                return RedirectToAction("ManageClaim", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while approving the claim.");
                return View("Error");
            }
        }

        public IActionResult RejectClaim(int id)
        {
            try
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ID == id);
                if (claim != null)
                {
                    claim.Approval = 2;
                    _context.SaveChanges();
                }
                return RedirectToAction("ManageClaim", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while rejecting the claim.");
                return View("Error");
            }
        }

        public IActionResult ClaimForm(CreateClaim model)
        {
            try
            {
                model.Total = model.Hours * model.Rate;

                if (model.DocumentFile != null && model.DocumentFile.Length > 0)
                {
                    var fileName = Path.GetFileName(model.DocumentFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads"));

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.DocumentFile.CopyTo(fileStream);
                    }
                    model.Document = $"/uploads/{fileName}";
                }

                // Define the hardcoded limits for automatic claim approval or rejection
                const double MinHours = 160;
                const double MaxHours = 200;
                const double MinRate = 91;
                const double MaxRate = 112;
                const double MinTotal = 14000;
                const double MaxTotal = 22000;

                if (model.Hours < MinHours || model.Hours > MaxHours || model.Rate < MinRate || model.Rate > MaxRate)
                {
                    model.Approval = 2;
                }
                else if (model.Total >= MinTotal && model.Total <= MaxTotal)
                {
                    model.Approval = 1;
                }
                else
                {
                    model.Approval = 0;
                }

                if (ModelState.IsValid)
                {
                    _context.Claims.Add(model);
                    _context.SaveChanges();
                }
                else
                {
                    return View("Claim", model); 
                }

                return RedirectToAction("ClaimHub");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while submitting the claim form.");
                return View("Error"); 
            }
        }

        public IActionResult ClaimHub()
        {
            try
            {
                var allClaims = _context.Claims.ToList();
                return View(allClaims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all claims.");
                return View("Error");
            }
        }

        public IActionResult EditClaim(CreateClaim model)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ID == model.ID);
            if (claim == null)
            {
                return View("Error");
            }
            return View(claim);
        }
        public IActionResult UpdateClaim(CreateClaim model)
        {
            if (ModelState.IsValid)
            {
                var claim = _context.Claims.FirstOrDefault(c => c.ID == model.ID);
                if (claim != null)
                {
                    claim.Name = model.Name;
                    claim.Surname = model.Surname;
                    claim.Hours = model.Hours;
                    claim.Rate = model.Rate;
                    claim.Notes = model.Notes;
                    claim.Total = model.Hours * model.Rate;

                    _context.SaveChanges();

                    return RedirectToAction("ManageClaim", new { id = model.ID});
                }
                else
                {
                    return View("Error");
                }
            }
            return View("EditClaim");
        }

        public IActionResult Report(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ID == id);
            if (claim == null)
            {
                return View("Error");
            }

            //Generate Pdf
            string htmlContent = $@"
            <html>
            <head>
                <h1>Claim Report</h1>
                <p><Strong>Name:</Strong> {claim.Name}</p>
                <p><Strong>Surname:</Strong> {claim.Surname}</p>
                <p><Strong>Hours:</Strong> {claim.Hours}</p>
                <p><Strong>Rate:</Strong> {claim.Rate}</p>
                <p><Strong>Notes:</Strong> {claim.Notes}</p>
                <p><Strong>Total:</Strong> {claim.Total}</p>
            </body>
            </html>";

            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };
            byte[] pdf = converter.Convert(doc);

            return File(pdf,"application/pdf", "Report().pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
