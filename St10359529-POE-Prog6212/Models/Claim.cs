namespace St10359529_POE_Prog6212.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        public string Name { get; set; } = string.Empty;  // New field
        public string Surname { get; set; } = string.Empty;  // New field
        public string ContactNumber { get; set; } = string.Empty;  // New field
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
}