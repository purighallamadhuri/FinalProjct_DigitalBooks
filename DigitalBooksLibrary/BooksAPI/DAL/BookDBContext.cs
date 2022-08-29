using BooksAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.DAL
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {

        }
        public DbSet<BooksDetails> Books { get; set; }
    }
}
