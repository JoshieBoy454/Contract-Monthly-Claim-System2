using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Contract_Monthly_Claim_System2.Models
{
    public class Role
    {
        public enum Roles
        {
            Lecturer,
            Admin,
            HR,
            None
        }
        public string? username { get; set;}
        public string? password { get; set;}

        /*private Role.Roles _role = Role.Roles.None;*/ // to set None as the default role
        public Roles UserRole { get; set; } = Role.Roles.None;
        public Role()
        {
            UserRole = Role.Roles.None; 
        }
    }
}
