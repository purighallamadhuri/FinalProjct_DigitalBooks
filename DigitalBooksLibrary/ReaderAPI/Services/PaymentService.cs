using ReaderAPI.DAL;
using System;
using static ReaderAPI.Model.PaymentDetails;

namespace ReaderAPI.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentDbContext paymentDbContext { get; set; }
        private static Random random = new Random();
        /// <summary>
        /// defining dbcontext in constructor.
        /// </summary>
        /// <param name="payDbContext"></param>
        public PaymentService(PaymentDbContext payDbContext)
        {
            paymentDbContext = payDbContext;
        }
        /// <summary>
        /// Generating random string for payment id.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Completing payment and adding in database.
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        public string CompletePayment(PayDetails paymentDetails)
        {
            try
            {
                paymentDetails.Payment_Id = RandomString(8);
                paymentDetails.Transaction_Id = RandomString(8);
                var list = paymentDbContext.Payments.ToList();
                var paymentData = paymentDbContext.Payments.Where(us => us.EmailId == paymentDetails.EmailId && us.Book_Id == paymentDetails.Book_Id).ToList();
                //return userData;
                if (paymentData.Count == 0)
                {
                    paymentDbContext.Payments.Add(paymentDetails);
                    paymentDbContext.SaveChanges();
                    return $"Payment done successfully {paymentDetails.Payment_Id}";
                }
                else
                {
                    return "This book was already bought by you!!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Updating payment details to database.
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        public string UpdatePayment(PayDetails paymentDetails)
        {
            try
            {
                var list = paymentDbContext.Payments.ToList();
                foreach (var u in list)
                {
                    int index = list.FindIndex(s => s.Payment_Id == paymentDetails.Payment_Id);
                    list[index].EmailId = paymentDetails.EmailId;
                    list[index].User_Id = paymentDetails.User_Id;
                    list[index].Book_Id = paymentDetails.Book_Id;
                    list[index].Payment_Date = paymentDetails.Payment_Date;
                    list[index].Transaction_Id = paymentDetails.Transaction_Id;
                    list[index].Payment_Mode = paymentDetails.Payment_Mode;

                    paymentDbContext.SaveChanges();
                }
                return "Payment details updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// getting all payments done by user.
        /// </summary>
        /// <param name="getPayDetails"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<PayDetails> GetAllPayments(GetPayDetails getPayDetails)
        {
            try
            {
                List<PayDetails> tbl = paymentDbContext.Payments.Where(e => e.EmailId == getPayDetails.EmailId).ToList();
                return tbl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
