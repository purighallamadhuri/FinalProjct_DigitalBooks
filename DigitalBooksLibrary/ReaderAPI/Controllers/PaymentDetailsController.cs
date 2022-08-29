using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReaderAPI.DAL;
using ReaderAPI.Services;
using System.Net.Http.Headers;
using static ReaderAPI.Model.PaymentDetails;

namespace ReaderAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDbContext _paymentcontext;
        private readonly IPaymentService _paymentService;
        /// <summary>
        /// declaring dbcontext in the constructor.
        /// </summary>
        /// <param name="paymentDbContext"></param>
        /// <param name="paymentService"></param>
        public PaymentDetailsController(PaymentDbContext paymentDbContext, IPaymentService paymentService)
        {
            _paymentcontext = paymentDbContext;
            _paymentService = paymentService;
        }
        /// <summary>
        /// getting all payments done.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayDetails>>> GetPaymentDetails()
        {
            try
            {
                return await _paymentcontext.Payments.ToListAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// getting particular payment details with id.,
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PayDetails>> GetPaymentDetailsWithId(int id)
        {
            try
            {
                var payment = await _paymentcontext.Payments.FindAsync(id);

                if (payment == null)
                {
                    return NotFound();
                }
                return payment;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Initiating Refund to the user.
        /// </summary>
        /// <param name="refundpayment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PayDetails>> RefundPayment(Refundpayment refundpayment)
        {
            try
            {
                var currentuser = await _paymentcontext.Payments.FindAsync(refundpayment.Payment_Id);
                if (currentuser == null)
                {
                    return NotFound();
                }
                if(currentuser.Payment_Date < DateTime.Now.AddDays(-1))
                {
                    return BadRequest();
                }
                else
                {
                    _paymentcontext.Payments.Remove(currentuser);
                    await _paymentcontext.SaveChangesAsync();
                    return currentuser;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updating payment details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PayDetails>> UpdatePaymentDetails(string id, PayDetails paymentDetails)
        {
            try
            {
                if (id != paymentDetails.Payment_Id)
                {
                    return BadRequest();
                }

                _paymentcontext.Entry(paymentDetails).State = EntityState.Modified;

                try
                {
                    await _paymentcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GetPayment(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction("GetPaymentDetailsWithId", new { id = paymentDetails.Payment_Id }, paymentDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static Random random = new Random();
        /// <summary>
        /// Generating random string for paymentid.
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
        /// Completing payment.
        /// </summary>
        /// <param name="paymentDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PayDetails>> CompletePayment(PayDetails paymentDetails)
        {
            try
            {
                //paymentDetails.Payment_Id = RandomString(8);
                //paymentDetails.Transaction_Id = RandomString(8);
                //_paymentcontext.Payments.Add(paymentDetails);
                //await _paymentcontext.SaveChangesAsync();
                string result = _paymentService.CompletePayment(paymentDetails);
                return Ok(new { result });


                //using (HttpClient client = new HttpClient())
                //{
                //    List<string> result = new List<string>();
                //    result.Add(await client.GetStringAsync(""));
                //    return Ok(new { result });
                //}

                //using (HttpClient client = new HttpClient())
                //{
                //    var json = JsonConvert.SerializeObject(paymentDetails, Formatting.Indented);

                //    var stringContent = new StringContent(json);

                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    List<string> result = new List<string>();
                //    result.Add(await client.PostAsync("", stringContent));
                //}

                //return CreatedAtAction("GetPaymentDetailsWithId", new { id = paymentDetails.Payment_Id }, paymentDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool GetPayment(string id)
        {
            try
            {
                return _paymentcontext.Payments.Any(e => e.Payment_Id == id);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Getting all payments of a user.
        /// </summary>
        /// <param name="getPayDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<List<PayDetails>> GetAllPayments(GetPayDetails getPayDetails)
        {
            try
            {
                var payments = _paymentService.GetAllPayments(getPayDetails);
                return payments;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
