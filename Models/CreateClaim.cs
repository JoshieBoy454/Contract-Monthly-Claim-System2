using System.ComponentModel.DataAnnotations.Schema;

namespace Contract_Monthly_Claim_System.Models2
{
    public class CreateClaim
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public double Hours { get; set; }
        public double Rate { get; set; }
        public string? Notes { get; set; }
        [NotMapped]
        public IFormFile? DocumentFile { get; set; }
        public string? Document { get; set; }
        public int Approval { get; set; }
    }
}
