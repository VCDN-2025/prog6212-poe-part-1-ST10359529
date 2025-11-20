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
                return RedirectToAction("PendingClaims");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }

        private bool IsManager() => HttpContext.Session.GetString("ManagerAuth") == "true";

        public IActionResult PendingClaims()
        {
            if (!IsManager()) return RedirectToAction("Login");

            var pending = ClaimRepository.Claims.Where(c => c.Status == "Pending").ToList();

            // AUTOMATION: High-risk flags 
            foreach (var c in pending)
            {
                c.Notes = "";
                if (c.HoursWorked > 40) c.Notes += "HIGH RISK: >40 hours | ";
                if (c.HourlyRate > 450) c.Notes += "HIGH RATE | ";
                if (c.TotalAmount > 15000) c.Notes += "HIGH VALUE | ";
            }
            return View(pending);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            if (!IsManager()) return RedirectToAction("Login");
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null) claim.Status = "Approved";
            TempData["Success"] = $"Claim #{id} Approved";
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            if (!IsManager()) return RedirectToAction("Login");
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null) claim.Status = "Rejected";
            TempData["Success"] = $"Claim #{id} Rejected";
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult DeleteClaim(int id)
        {
            if (!IsManager()) return RedirectToAction("Login");
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                ClaimRepository.Claims.Remove(claim);
                TempData["Success"] = $"Claim #{id} Deleted";
            }
            return RedirectToAction("PendingClaims");
        }

        public IActionResult ApprovedClaims()
        {
            if (!IsManager()) return RedirectToAction("Login");
            var approved = ClaimRepository.Claims.Where(c => c.Status == "Approved").ToList();
            ViewBag.Title = "Approved Claims";
            return View("StatusView", approved);
        }

        public IActionResult RejectedClaims()
        {
            if (!IsManager()) return RedirectToAction("Login");
            var rejected = ClaimRepository.Claims.Where(c => c.Status == "Rejected").ToList();
            ViewBag.Title = "Rejected Claims";
            return View("StatusView", rejected);

        }



        public IActionResult DeletedClaims()
        {
            if (!IsManager()) return RedirectToAction("Login");
            ViewBag.Title = "Deleted Claims";
            return View(ClaimRepository.DeletedClaims ?? new List<Claim>());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

  
}