using DigitalBooksLibrary.Model;
using Microsoft.EntityFrameworkCore;
namespace DigitalBooksLibrary.DAL
{
    public class AuthorDBContext:DbContext
    {
        public AuthorDBContext(DbContextOptions<AuthorDBContext> options) : base(options)
        {

        }
        public DbSet<AuthorDetails> Users { get; set; }
    }
}
