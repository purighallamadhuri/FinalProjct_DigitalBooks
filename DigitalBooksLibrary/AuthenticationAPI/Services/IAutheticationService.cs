using AuthenticationAPI.DAL;
using AuthenticationAPI.Model;
using DigitalBooksLibrary.Model;

namespace AuthenticationAPI.Services
{
    public interface IAutheticationService
    {
        AutheticationDbContext usersDbContext { get; set; }

        List<UsersDetails> UserLogin(UserLogin userLogin);
    }
}