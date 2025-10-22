using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Models;
using System.IO;

namespace St10359529_POE_Prog6212.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LecturerController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult SubmitClaim()
        {
            return View(new Claim());
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claim model, IFormFile documentFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = ClaimRepository.NextClaimId++;
                    model.TotalAmount = model.HoursWorked * model.HourlyRate;
                    model.SubmissionDate = DateTime.Now;
                    model.Status = "Pending";
                    model.LecturerId = 1; // Hardcoded for prototype

                    ClaimRepository.Claims.Add(model);

                    if (documentFile != null && documentFile.Length > 0)
                    {
                        if (documentFile.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", "File size exceeds 5MB.");
                            return View(model);
                        }
                        var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                        var ext = Path.GetExtension(documentFile.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(ext))
                        {
                            ModelState.AddModelError("", "Invalid file type.");
                            return View(model);
                        }

                        var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploads);
                        var filePath = Path.Combine(uploads, documentFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            documentFile.CopyTo(stream);
                        }

                        ClaimRepository.Documents.Add(new Document
                        {
                            Id = ClaimRepository.Documents.Count + 1,
                            ClaimId = model.Id,
                            FileName = documentFile.FileName,
                            FilePath = filePath
                        });
                    }

                    ViewData["Message"] = "Claim submitted successfully!";
                    return RedirectToAction("PendingClaims", "Coordinator");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(model);
        }

        public IActionResult TrackStatus()
        {
            var userClaims = ClaimRepository.Claims.Where(c => c.LecturerId == 1).ToList();
            return View(userClaims);
        }
    }
}