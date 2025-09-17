namespace St10359529_POE_Prog6212.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace CMCS.Models
    {
        public class UserRole
        {
            public int Id { get; set; }  

            [Required]
            public string UserId { get; set; } = string.Empty;

            [Required]
            public string Role { get; set; } = string.Empty;  
        }
    }
}
