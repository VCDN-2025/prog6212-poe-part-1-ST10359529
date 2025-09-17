namespace St10359529_POE_Prog6212.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace CMCS.Models
    {
        public class Claim
        {
            public int Id { get; set; }  

            [Required]
            public int LecturerId { get; set; }  

            [ForeignKey("LecturerId")]
            public Lecturer? Lecturer { get; set; }  

            [Required]
            [Range(0, 999.99)]
            public decimal HoursWorked { get; set; }  

            [Required]
            public decimal TotalAmount { get; set; }  

            [Required]
            public string Status { get; set; } = "Pending";  

            [Required]
            public DateTime SubmissionDate { get; set; } = DateTime.Now;

            public string? Notes { get; set; } 
        }
    }
}
