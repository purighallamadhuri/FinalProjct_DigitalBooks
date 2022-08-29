using AuthenticationAPI.DAL;
using AuthenticationAPI.Model;
using DigitalBooksLibrary.Model;

namespace AuthenticationAPI.Services
{
    public class AutheticationService : IAutheticationService
    {
        public AutheticationDbContext usersDbContext { get; set; }

        public AutheticationService(AutheticationDbContext addusersDbContext)
        {
            usersDbContext = addusersDbContext;
        }
        /// <summary>
        /// Checking and validating user here.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<UsersDetails> UserLogin(UserLogin userLogin)
        {
            try
            {
                var userData = usersDbContext.Users.Where(us => us.EmailId == userLogin.UserName && us.Password == userLogin.Password).ToList();
                return userData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
