using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    public class CoordinatorController : Controller
    {
        private const string ManagerUsername = "1234";
        private const string ManagerPassword = "1234";
        private const string SessionKey = "IsAuthenticated";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == ManagerUsername && password == ManagerPassword)
            {
                HttpContext.Session.SetString(SessionKey, "true");
                return RedirectToAction("PendingClaims");
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult PendingClaims()
        {
            if (HttpContext.Session.GetString(SessionKey) != "true")
            {
                return RedirectToAction("Login");
            }
            var pending = ClaimRepository.Claims.Where(c => c.Status == "Pending").ToList();
            return View(pending);
        }

        public IActionResult ApprovedClaims()
        {
            var approved = ClaimRepository.Claims.Where(c => c.Status == "Approved").ToList();
            return View(approved);
        }

        public IActionResult RejectedClaims()
        {
            var rejected = ClaimRepository.Claims.Where(c => c.Status == "Rejected").ToList();
            return View(rejected);
        }

        public IActionResult DeletedClaims()
        {
            var deleted = ClaimRepository.DeletedClaims.ToList();
            return View(deleted);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Approved";
            }
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult DeleteClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                ClaimRepository.DeletedClaims.Add(claim);
                ClaimRepository.Claims.Remove(claim);
            }
            return RedirectToAction("PendingClaims");
        }
    }
}