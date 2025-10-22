namespace St10359529_POE_Prog6212.Models
{
    public static class ClaimRepository
    {
        public static List<Claim> Claims { get; } = new List<Claim>();
        public static List<Document> Documents { get; } = new List<Document>();
        public static int NextClaimId { get; set; } = 1;
        public static List<Claim> DeletedClaims { get; } = new List<Claim>(); // New list for deleted claims
        public static object Changes { get; set; }
    }
}