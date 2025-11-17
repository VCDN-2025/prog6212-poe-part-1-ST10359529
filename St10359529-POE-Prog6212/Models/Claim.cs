using System.ComponentModel.DataAnnotations;

namespace St10359529_POE_Prog6212.Models
{
    public class Claim
    {
        public int Id { get; set; }

        // Links claim to a specific lecturer (used in HR view and future enhancements)
        public int LecturerId { get; set; }

        // Lecturer personal details
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [Display(Name = "First Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters")]
        [Display(Name = "Surname")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        // Claim financial details with validation
        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0.01, 500, ErrorMessage = "Hours worked must be greater than 0 and reasonable")]
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(50.00, 500.00, ErrorMessage = "Hourly rate must be between R50 and R500")]
        [Display(Name = "Hourly Rate (R)")]
        public decimal HourlyRate { get; set; }

        // Auto-calculated field (jQuery will populate this before submission)
        [Display(Name = "Total Amount (R)")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal TotalAmount { get; set; }

        // Claim status
        [Required]
        public string Status { get; set; } = "Pending";  // Pending, Approved, Rejected

        // Audit fields
        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        // Used for automated warnings (e.g., "HIGH RISK: Hours exceed 40")
        [Display(Name = "System Notes / Warnings")]
        public string? Notes { get; set; }

        // Optional: Future-proof for file upload
        public string? SupportingDocumentPath { get; set; }
    }
}