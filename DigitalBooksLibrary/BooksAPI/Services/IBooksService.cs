using BooksAPI.DAL;
using BooksAPI.Model;

namespace BooksAPI.Services
{
    public interface IBooksService
    {
        BookDBContext booksDbContext { get; set; }

        string AddBooks(BooksDetails booksDetails);
        string DeleteBook(BooksDetails booksDetails);
        List<BooksDetails> GetAllPublishers();
        List<BooksDetails> GetAuthorBooks(GetBooksByAuthorID getBooksByAuthorID);
        string ModifyBooks(BooksDetails booksDetails);
        List<BooksDetails> SearchBooksByCriteria(BookSearch booksDetails);
    }
}