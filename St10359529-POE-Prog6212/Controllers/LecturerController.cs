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
        [ValidateAntiForgeryToken]
        public IActionResult SubmitClaim(Claim model, IFormFile? documentFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    model.TotalAmount = model.HoursWorked * model.HourlyRate;

                  
                    model.Id = ClaimRepository.NextClaimId++;
                    model.SubmissionDate = DateTime.Now;
                    model.Status = "Pending";
                    model.LecturerId = 1; 

                    // FILE UPLOAD (Safe & Professional)
                    if (documentFile != null && documentFile.Length > 0)
                    {
                        // Size check (max 5MB)
                        if (documentFile.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", "File size cannot exceed 5MB.");
                            return View(model);
                        }

                       
                        var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                        var ext = Path.GetExtension(documentFile.FileName).ToLowerInvariant();
                        if (!allowedExtensions.Contains(ext))
                        {
                            ModelState.AddModelError("", "Only .pdf, .docx, and .xlsx files are allowed.");
                            return View(model);
                        }

                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                        Directory.CreateDirectory(uploadsFolder);
                        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(documentFile.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            documentFile.CopyTo(stream);
                        }

                     
                        model.DocumentName = documentFile.FileName;      
                        model.DocumentPath = $"/uploads/{uniqueFileName}"; 
                    }

                    
                    ClaimRepository.Claims.Add(model);

                    
                    TempData["Success"] = $"Claim #{model.Id} submitted successfully! Total Amount: R{model.TotalAmount:F2}";

                  
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while submitting your claim. Please try again.");
                }
            }

           
            return View(model);
        }

      
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