using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    public class ManagerController : Controller
    {
        private const string Username = "1234";
        private const string Password = "1234";

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == Username && password == Password)
            {
                HttpContext.Session.SetString("ManagerAuth", "true");
                TempData["Success"] = "Manager logged in successfully!";
                return RedirectToAction("PendingClaims"); // This looks for Views/Manager/PendingClaims.cshtml
            }
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult PendingClaims()
        {
            if (HttpContext.Session.GetString("ManagerAuth") != "true")
                return RedirectToAction("Login");

            var claims = ClaimRepository.Claims.Where(c => c.Status == "Pending").ToList();

            // AUTOMATION: High-risk flags (Part 3 requirement)
            foreach (var c in claims)
            {
                if (c.HoursWorked > 40) c.Notes = "HIGH RISK: Hours > 40 | " + c.Notes;
                if (c.TotalAmount > 15000) c.Notes = "HIGH VALUE CLAIM | " + c.Notes;
            }
            return View(claims); // ← Now finds Views/Manager/PendingClaims.cshtml
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null) claim.Status = "Approved";
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null) claim.Status = "Rejected";
            return RedirectToAction("PendingClaims");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}