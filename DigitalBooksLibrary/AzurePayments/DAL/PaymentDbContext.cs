using Microsoft.EntityFrameworkCore;
using static AzurePayments.Model.PaymentDetails;

namespace AzurePayments.DAL
{
    public class PaymentDbContext :DbContext
    {
        public PaymentDbContext()
        {
        }

        //public PaymentDbContext()
        //{
        //}

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
        public DbSet<PayDetails> Payments { get; set; }
    }
}
