namespace St10359529_POE_Prog6212.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace CMCS.Models
    {
        public class Document
        {
            public int Id { get; set; } 

            [Required]
            public int ClaimId { get; set; }  

            [ForeignKey("ClaimId")]
            public Claim? Claim { get; set; } 

            [Required]
            public string FileName { get; set; } = string.Empty;

            [Required]
            public string FilePath { get; set; } = string.Empty; 

            [Required]
            public DateTime UploadDate { get; set; } = DateTime.Now;
        }
    }
}
