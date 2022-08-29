using System;
using System.ComponentModel.DataAnnotations;

namespace AzurePayments.Model
{
    public class PaymentDetails
    {
        public class PayDetails
        {
            [Key]
            public string Payment_Id { get; set; }
            public string EmailId { get; set; }
            public int? User_Id { get; set; }
            public string Book_Id { get; set; }
            public DateTime Payment_Date { get; set; }
            public string Transaction_Id { get; set; }
            public string Payment_Mode { get; set; }
            public string BookName { get; set; }
            public int Price { get; set; }
        }
        public class GetPayDetails
        {
            public string? EmailId { get; set; }
        }
        public class Refundpayment
        {
            public string? Payment_Id { get; set; }
        }
        //private static Random random = new Random();

        //public static string RandomString(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    return new string(Enumerable.Repeat(chars, length)
        //        .Select(s => s[random.Next(s.Length)]).ToArray());
        //}
    }
}
