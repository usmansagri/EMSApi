using EMSApi.Modals;
using Microsoft.EntityFrameworkCore;

namespace EMSApi
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}
