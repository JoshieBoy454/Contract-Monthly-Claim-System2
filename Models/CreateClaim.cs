namespace Contract_Monthly_Claim_System.Models2
{
    public class CreateClaim
    {
        public string? name { get; set; }
        public string? surname { get; set; }
        public double hours { get; set; }
        public double rate { get; set; }
        public string? notes { get; set; }
        // public var document { get; set; }
        public int approval { get; set; }
    }
}
