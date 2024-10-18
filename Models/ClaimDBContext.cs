using Contract_Monthly_Claim_System2.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract_Monthly_Claim_System2.Models
{
    public class ClaimDBContext : DbContext
    {
        public DbSet<CreateClaim> Claims { get; set; }

        public ClaimDBContext(DbContextOptions<ClaimDBContext> options) : base(options)
        {
            
        }
    }
}
