using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    public class HRController : Controller
    {
        private const string Username = "hr";
        private const string Password = "hr123";

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == Username && password == Password)
            {
                HttpContext.Session.SetString("HRAuth", "true");
                return RedirectToAction("ProcessClaims");
            }
            ViewBag.Error = "Invalid HR credentials";
            return View();
        }

        private bool IsHRAuthenticated() => HttpContext.Session.GetString("HRAuth") == "true";

        public IActionResult ProcessClaims()
        {
            if (!IsHRAuthenticated()) return RedirectToAction("Login");

            ViewBag.AllClaims = ClaimRepository.Claims;
            ViewBag.ApprovedClaims = ClaimRepository.Claims.Where(c => c.Status == "Approved").ToList();
            ViewBag.TotalApprovedAmount = ClaimRepository.Claims.Where(c => c.Status == "Approved").Sum(c => c.TotalAmount);
            return View();
        }

        [HttpPost]
        public IActionResult UpdateLecturerRate(int lecturerId, decimal newRate)
        {
            if (!IsHRAuthenticated()) return RedirectToAction("Login");
            // Simulate updating lecturer rate (extend with Lecturer model if needed)
            TempData["Success"] = $"Lecturer ID {lecturerId} rate updated to R{newRate}";
            return RedirectToAction("ProcessClaims");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("HRAuth");
            return RedirectToAction("Login");
        }
    }
}