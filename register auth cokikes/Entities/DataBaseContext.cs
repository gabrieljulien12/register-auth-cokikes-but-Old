using Microsoft.EntityFrameworkCore;

namespace register_auth_cokikes.Entities
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<user> AuthUser { get; set; }
    }
}
