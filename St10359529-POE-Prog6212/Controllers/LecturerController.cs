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

        // GET: Show the claim submission form
        public IActionResult SubmitClaim()
        {
            return View(new Claim());
        }

        // POST: Process the submitted claim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitClaim(Claim model, IFormFile? documentFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // AUTOMATION: Auto-calculate total amount (Part 3 requirement)
                    model.TotalAmount = model.HoursWorked * model.HourlyRate;

                    // Set required fields
                    model.Id = ClaimRepository.NextClaimId++;
                    model.SubmissionDate = DateTime.Now;
                    model.Status = "Pending";
                    model.LecturerId = 1; // In real app: from session or login

                    // FILE UPLOAD (Safe & Professional)
                    if (documentFile != null && documentFile.Length > 0)
                    {
                        // Size check (max 5MB)
                        if (documentFile.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", "File size cannot exceed 5MB.");
                            return View(model);
                        }

                        // Extension check
                        var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                        var ext = Path.GetExtension(documentFile.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(ext))
                        {
                            ModelState.AddModelError("", "Only .pdf, .docx, and .xlsx files are allowed.");
                            return View(model);
                        }

                        // Save file with unique name to avoid conflicts
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);
                        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(documentFile.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            documentFile.CopyTo(stream);
                        }

                        // STORE FILE INFO DIRECTLY IN CLAIM MODEL (No extra Document class needed)
                        model.DocumentName = documentFile.FileName;      // Original name
                        model.DocumentPath = $"/uploads/{uniqueFileName}"; // URL path
                    }

                    // Add claim to in-memory repository
                    ClaimRepository.Claims.Add(model);

                    // SUCCESS MESSAGE + REDIRECT TO HOME (No more 404!)
                    TempData["Success"] = $"Claim #{model.Id} submitted successfully! Total Amount: R{model.TotalAmount:F2}";

                    // BEST UX: Back to beautiful home page
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while submitting your claim. Please try again.");
                }
            }

            // If validation fails → show form again with errors
            return View(model);
        }

        // Track all claims for the current lecturer
        public IActionResult TrackStatus()
        {
            var userClaims = ClaimRepository.Claims
                .Where(c => c.LecturerId == 1)
                .OrderByDescending(c => c.SubmissionDate)
                .ToList();

            return View(userClaims);
        }
    }
}