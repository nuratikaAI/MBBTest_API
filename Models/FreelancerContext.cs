using Microsoft.EntityFrameworkCore;

namespace MBBTest.Models
{
    public class FreelancerContext : DbContext
    {

        public FreelancerContext(DbContextOptions<FreelancerContext> options)
            : base(options)
        {

        }

        public DbSet<Freelancer> Freelancers { get; set; } = null!;
    }
}
