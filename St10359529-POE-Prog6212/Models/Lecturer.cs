namespace St10359529_POE_Prog6212.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace CMCS.Models
    {
        public class Lecturer
        {
            public int Id { get; set; }  

            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            [Range(0, 999.99)]
            public decimal HourlyRate { get; set; }  
        }
    }
}
