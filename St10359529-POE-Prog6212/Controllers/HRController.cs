using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;

namespace St10359529_POE_Prog6212.Controllers
{
    public class HRController : Controller
    {
        public IActionResult ProcessClaims()
        {
            var processedClaims = ClaimRepository.Claims.Where(c => c.Status == "Approved").ToList();
            var lecturers = ClaimRepository.Lecturers.ToList();
            ViewBag.ProcessedClaims = processedClaims;
            ViewBag.Lecturers = lecturers;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateLecturerRate(int lecturerId, decimal newRate)
        {
            var lecturer = ClaimRepository.Lecturers.FirstOrDefault(l => l.Id == lecturerId);
            if (lecturer != null)
            {
                lecturer.HourlyRate = newRate;
            }
            return RedirectToAction("ProcessClaims");
        }
    }
}