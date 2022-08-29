using AuthenticationAPI.Model;
using AuthenticationAPI.Services;
using DigitalBooksLibrary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly IAutheticationService _userService;
        public AutheticationController(IConfiguration configuration, IAutheticationService userService)
        {
            this._Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userService = userService;
        }
        public class AuthenticationBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        public class Users
        {
            public int User_Id { get; set; }
            public string? UserName { get; set; }
            public string? Password { get; set; }
            public string? User_Type { get; set; }
            public DateTime Created_On { get; set; }
            public string? EmailId { get; set; }
            public string? Mobile { get; set; }
        }
        private class RequestedUserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string User_Type { get; set; }
            public string EmailId { get; set; }
            public string Mobile { get; set; }
            public RequestedUserInfo(int userId, string userName, string user_Type, string emailId, string mobile)
            {
                UserId = userId;
                UserName = userName;
                User_Type = user_Type;
                EmailId = emailId;
                Mobile = mobile;
            }
        }
        //public class Claim
        //{
        //    public string sub { get; set; }
        //    public string given_name { get; set; }
        //    public string family_name { get; set; }
        //    public string city { get; set; }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Token will be generated if users got validated.</returns>
        [HttpPost("Authenticate")]
        public ActionResult<JsonResponse> AuthenticateUser(UserLogin userLogin)
        {
            try
            {
                var User = ValidateUserCrendentials(userLogin);
                if (User == null)
                {
                    return Unauthorized();
                }
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Configuration["Authentication:SecretForKey"]));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", User.UserId.ToString()));
                claimsForToken.Add(new Claim("UserName", User.UserName));
                claimsForToken.Add(new Claim("User_Type", User.User_Type));
                claimsForToken.Add(new Claim("EmailId", User.EmailId));
                claimsForToken.Add(new Claim("Mobile", User.Mobile));

                var jwtSecurityToken = new JwtSecurityToken(
                    _Configuration["Authentication:Issuer"],
                    _Configuration["Authentication:Audience"],
                    claimsForToken,
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(1),
                    signingCredentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                return Ok(new
                {
                    Token = tokenToReturn,
                    status = true,
                });
            }
            catch(Exception ex)
            {
                //throw new Exception(ex.Message);
                return BadRequest(new
                {
                    Token = string.Empty,
                    status = false,
                });
            }
        }
        /// <summary>
        /// Hitting service call to validate user.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private RequestedUserInfo ValidateUserCrendentials(UserLogin userLogin)
        {
            try
            {
                List<UsersDetails> list = _userService.UserLogin(userLogin);
                foreach (var item in list)
                {
                    //Userid = item.User_Id
                    return new RequestedUserInfo(item.User_Id, item.UserName, item.User_Type, item.EmailId, item.Mobile);

                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
