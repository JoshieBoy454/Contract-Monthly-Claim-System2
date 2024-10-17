namespace Contract_Monthly_Claim_System.Models2
{
    public class CreateClaim
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public double Hours { get; set; }
        public double Rate { get; set; }
        public string? Notes { get; set; }
        public IFormFile? Document { get; set; }
        public int Approval { get; set; }
    }
}
