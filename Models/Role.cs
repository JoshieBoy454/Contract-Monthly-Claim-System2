using Azure.Identity;

namespace Contract_Monthly_Claim_System2.Models
{
    public class Role
    {
        public enum Roles
        {
            Lecturer,
            Admin,
            Hr,
            None
        }
        public string? username { get; set;}
        public string? password { get; set;}
    }
}
