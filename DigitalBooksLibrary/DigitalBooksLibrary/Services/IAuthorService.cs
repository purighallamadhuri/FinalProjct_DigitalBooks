using DigitalBooksLibrary.DAL;
using DigitalBooksLibrary.Model;

namespace DigitalBooksLibrary
{
    public interface IAuthorService
    {
        AuthorDBContext usersDbContext { get; set; }

        string AddUsers(AuthorDetails AuthorDetails);
        string DeleteUser(AuthorDetails AuthorDetails);
        List<AuthorDetails> GetAllAuthors();
        string ModifyUser(AuthorDetails AuthorDetails);
        List<AuthorDetails> UserLogin(UserLogin userLogin);
    }
}