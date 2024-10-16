namespace Contract_Monthly_Claim_System.Models2
{
    public class CreateClaim
    {
        public int claimID { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public double hours { get; set; }
        // public var document { get; set; }
        public double rate { get; set; }
    }
}
