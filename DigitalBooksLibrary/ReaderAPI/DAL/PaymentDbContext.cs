using Microsoft.EntityFrameworkCore;
using static ReaderAPI.Model.PaymentDetails;

namespace ReaderAPI.DAL
{
    public class PaymentDbContext :DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
        public DbSet<PayDetails> Payments { get; set; }
    }
}
