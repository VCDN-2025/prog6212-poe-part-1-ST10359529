using System.ComponentModel.DataAnnotations;

namespace St10359529_POE_Prog6212.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters")]
        public string Surname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string ContactNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0.01, 500, ErrorMessage = "Hours must be > 0 and ≤ 500")]
        public decimal HoursWorked { get; set; }
        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(50, 500, ErrorMessage = "Rate must be R50–R500")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Total Amount (Auto-Calculated)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be > 0")]
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
        public string? DocumentName { get; set; }      
        public string? DocumentPath { get; set; }      
    }
}