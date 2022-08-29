using AuthenticationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.DAL
{
    public class AutheticationDbContext:DbContext
    {
        public AutheticationDbContext(DbContextOptions<AutheticationDbContext> options) : base(options)
        {

        }
        public DbSet<UsersDetails> Users { get; set; }
    }
}
