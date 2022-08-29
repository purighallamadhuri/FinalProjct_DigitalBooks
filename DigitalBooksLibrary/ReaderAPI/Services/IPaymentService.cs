using ReaderAPI.DAL;
using ReaderAPI.Model;

namespace ReaderAPI.Services
{
    public interface IPaymentService
    {
        PaymentDbContext paymentDbContext { get; set; }

        string CompletePayment(PaymentDetails.PayDetails paymentDetails);
        List<PaymentDetails.PayDetails> GetAllPayments(PaymentDetails.GetPayDetails getPayDetails);
        string UpdatePayment(PaymentDetails.PayDetails paymentDetails);
    }
}