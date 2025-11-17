using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;
using System.Linq;

namespace St10359529_POE_Prog6212.Controllers
{
    public class CoordinatorController : Controller
    {
        private const string ManagerUsername = "1234";
        private const string ManagerPassword = "1234";
        private const string SessionKey = "IsAuthenticated";

        // ========================================
        // 1. Login & Authentication
        // ========================================
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
                TempData["Success"] = "Welcome back, Programme Coordinator!";
                return RedirectToAction("PendingClaims");
            }

            ViewBag.Error = "Invalid username or password. Please try again.";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey);
            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }

        // ========================================
        // 2. Pending Claims with AUTOMATED VERIFICATION
        // ========================================
        [HttpGet]
        public IActionResult PendingClaims()
        {
            if (HttpContext.Session.GetString(SessionKey) != "true")
            {
                return RedirectToAction("Login");
            }

            var pendingClaims = ClaimRepository.Claims
                .Where(c => c.Status == "Pending")
                .ToList();

            // AUTOMATION: Run verification checks on all pending claims
            foreach (var claim in pendingClaims)
            {
                claim.Notes = VerifyClaimPolicy(claim);
            }

            return View(pendingClaims);
        }

        // ========================================
        // 3. Automated Policy Verification (Part 3 Core Feature)
        // ========================================
        private string VerifyClaimPolicy(Claim claim)
        {
            var warnings = new List<string>();

            // Policy 1: Maximum 40 hours per month
            if (claim.HoursWorked > 40)
            {
                warnings.Add("HIGH RISK: Hours exceed 40 (Overtime policy violation)");
            }

            // Policy 2: Maximum R450 hourly rate
            if (claim.HourlyRate > 450)
            {
                warnings.Add("HIGH RISK: Hourly rate exceeds R450 limit");
            }

            // Policy 3: Total claim > R15,000 requires manager approval
            if (claim.TotalAmount > 15000)
            {
                warnings.Add("VERY HIGH VALUE: Claim exceeds R15,000 – Requires Academic Manager approval");
            }

            // Policy 4: Suspiciously high hours + low rate (data entry error?)
            if (claim.HoursWorked > 35 && claim.HourlyRate < 100)
            {
                warnings.Add("WARNING: Possible data entry error – High hours with low rate");
            }

            return warnings.Any() ? string.Join(" | ", warnings) : "No issues detected";
        }

        // ========================================
        // 4. Claim Actions (Approve, Reject, Delete)
        // ========================================
        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Approved";
                TempData["Success"] = $"Claim ID {id} has been APPROVED successfully.";
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
                claim.Notes += " | REJECTED by Coordinator";
                TempData["Warning"] = $"Claim ID {id} has been REJECTED.";
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
                TempData["Info"] = $"Claim ID {id} has been moved to Deleted Claims.";
            }
            return RedirectToAction("PendingClaims");
        }

        // ========================================
        // 5. Status Views (Approved, Rejected, Deleted)
        // ========================================
        public IActionResult ApprovedClaims()
        {
            var approved = ClaimRepository.Claims.Where(c => c.Status == "Approved").ToList();
            ViewBag.PageTitle = "Approved Claims";
            return View("StatusView", approved);
        }

        public IActionResult RejectedClaims()
        {
            var rejected = ClaimRepository.Claims.Where(c => c.Status == "Rejected").ToList();
            ViewBag.PageTitle = "Rejected Claims";
            return View("StatusView", rejected);
        }

        public IActionResult DeletedClaims()
        {
            var deleted = ClaimRepository.DeletedClaims.ToList();
            ViewBag.PageTitle = "Deleted Claims";
            return View("StatusView", deleted);
        }
    }
}