//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using AzurePayments.DAL;
//using AzurePayments.Model;

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using AzurePayments.Model;
using AzurePayments.DAL;
using System.Collections.Generic;
using System.Linq;
namespace AzurePayments
{
/// <summary>
/// Function to perform payment through azure function.
/// </summary>
    public static class Purchase
    {
        [FunctionName("CompletePayment")]
        public static List<string> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] PaymentDetails.PayDetails payDetails)
        {
           PaymentDbContext paymentDbContext = new PaymentDbContext();
            List<string> res = new List<string>();

            var paymentData = paymentDbContext.Payments.Where(us => us.EmailId == payDetails.EmailId && us.Book_Id == payDetails.Book_Id).ToList();
            if (paymentData.Count == 0)
            {
                paymentDbContext.Payments.Add(payDetails);
                paymentDbContext.SaveChanges();
                 res.Add($"Payment done successfully {payDetails.Payment_Id}");
            }
            else
            {
                 res.Add("This book was already bought by you!!");
            }
            return res;
        }
    }
}
