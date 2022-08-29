using BooksAPI.DAL;
using BooksAPI.Model;

namespace BooksAPI.Services
{
    public class BooksService : IBooksService
    {
        public BookDBContext booksDbContext { get; set; }
        /// <summary>
        /// 
        /// Defining dbcontext in constructor.
        /// </summary>
        /// <param name="commonDbContext"></param>
        public BooksService(BookDBContext commonDbContext)
        {
            booksDbContext = commonDbContext;
        }
        /// <summary>
        /// Getting all publishers from db.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BooksDetails> GetAllPublishers()
        {
            try
            {
                var publishersData = booksDbContext.Books.Where(us => us.Publisher != "").ToList();
                return publishersData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Adiing books to database.
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        public string AddBooks(BooksDetails booksDetails)
        {
            try
            {
                var list = booksDbContext.Books.ToList();
                if (list.Count > 0)
                {
                    booksDbContext.Books.Add(booksDetails);
                    booksDbContext.SaveChanges();
                    return $"Book created successfully  - {booksDetails.Book_Title}";
                }
                else
                {
                    return "Please enter book details";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Updating book details to database.
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        public string ModifyBooks(BooksDetails booksDetails)
        {
            try
            {
                var list = booksDbContext.Books.ToList();
                foreach (var u in list)
                {
                    int index = list.FindIndex(s => s.Book_Id == booksDetails.Book_Id);
                    list[index].Logo = booksDetails.Logo;
                    list[index].Book_Title = booksDetails.Book_Title;
                    list[index].Category = booksDetails.Category;
                    list[index].Price = booksDetails.Price;
                    list[index].Author_Id = booksDetails.Author_Id;
                    list[index].Publisher = booksDetails.Publisher;
                    list[index].Published_Date = booksDetails.Published_Date;
                    list[index].Content = booksDetails.Content;
                    list[index].Active = booksDetails.Active;
                    list[index].Created_Date = booksDetails.Created_Date;
                    list[index].Modified_Date = booksDetails.Modified_Date;

                    booksDbContext.SaveChanges();
                }
                return "Books details updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// deleting book in database.
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        public string DeleteBook(BooksDetails booksDetails)
        {
            try
            {
                var list = booksDbContext.Books.ToList();
                int index = list.FindIndex(s => s.Book_Id == booksDetails.Book_Id);
                if (index > 0)
                {
                    booksDbContext.Books.Remove(booksDetails);
                    return "User deleted successfully";
                }
                else
                {
                    return "User deletion failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BooksDetails> SearchBooksByCriteria(BookSearch booksDetails)
        {
            try
            {
                var list = booksDbContext.Books.ToList();
                if (booksDetails.Publisher != "" && booksDetails.Category != "" && booksDetails.Author_Id > 0)
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Publisher == booksDetails.Publisher && x.Category == booksDetails.Category && x.Author_Id == booksDetails.Author_Id && x.Active == true).ToList();
                    return tbl;
                }
                else if (booksDetails.Category != "" && booksDetails.Author_Id > 0)
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Category == booksDetails.Category && x.Author_Id == booksDetails.Author_Id && x.Active == true).ToList();
                    return tbl;
                }
                else if (booksDetails.Publisher != "" && booksDetails.Category != "")
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Publisher == booksDetails.Publisher && x.Category == booksDetails.Category && x.Active == true).ToList();
                    return tbl;
                }
                else if (booksDetails.Publisher != "" && booksDetails.Author_Id > 0)
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Publisher == booksDetails.Publisher && x.Author_Id == booksDetails.Author_Id && x.Active == true).ToList();
                    return tbl;
                }
                else if (booksDetails.Publisher != "")
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Publisher == booksDetails.Publisher && x.Active == true).ToList();
                    return tbl;
                }
                else if (booksDetails.Category != "")
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Category == booksDetails.Category && x.Active == true).ToList();
                    return tbl;
                }
                else
                {
                    List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Author_Id == booksDetails.Author_Id && x.Active == true).ToList();
                    return tbl;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// getting particular author related books.
        /// </summary>
        /// <param name="getBooksByAuthorID"></param>
        /// <returns></returns>
        public List<BooksDetails> GetAuthorBooks(GetBooksByAuthorID getBooksByAuthorID)
        {
            var list = booksDbContext.Books.ToList();
            if (getBooksByAuthorID.Author_Id != 0)
            {
                List<BooksDetails> tbl = booksDbContext.Books.Where(x => x.Author_Id == getBooksByAuthorID.Author_Id).ToList();
                return tbl;
            }
            else
            {
                return list;
            }
        }
    }
}
