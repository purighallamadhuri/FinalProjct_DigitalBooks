using BooksAPI.DAL;
using BooksAPI.Model;
using BooksAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class BooksDetailsController : ControllerBase
    {
        private readonly BookDBContext _usercontext;
        private readonly IBooksService _booksService;
        /// <summary>
        /// Declaring context db in the constructor.
        /// </summary>
        /// <param name="usersdbcontext"></param>
        /// <param name="booksService"></param>
        public BooksDetailsController(BookDBContext usersdbcontext, IBooksService booksService)
        {
            _usercontext = usersdbcontext;
            _booksService = booksService;
        }
        /// <summary>
        /// Getting all books from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksDetails>>> GetAllBooks()
        {
            try
            {
                return await _usercontext.Books.ToListAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Getting all author related books.
        /// </summary>
        /// <param name="getBooksByAuthorID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<BooksDetails>>> GetAuthorBooks(GetBooksByAuthorID getBooksByAuthorID)
        {
            try
            {
                //return await _usercontext.Books.ToListAsync();
                var booksData = _booksService.GetAuthorBooks(getBooksByAuthorID);
                return booksData;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Getting book details with bookid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<BooksDetails>> GetBookWithId(string id)
        {
            try
            {
                var book = await _usercontext.Books.FindAsync(id);

                if (book == null)
                {
                    return NotFound();
                }
                return book;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Adding book to database.
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BooksDetails>> AddBook(BooksDetails booksDetails)
        {
            try
            {
                booksDetails.Book_Id = $"{booksDetails.Author_Id}{booksDetails.Publisher.Replace(" ", "_")}{DateTime.Now.Ticks}";
                _usercontext.Books.Add(booksDetails);
                await _usercontext.SaveChangesAsync();

                return CreatedAtAction("GetBookWithId", new { id = booksDetails.Book_Id }, booksDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updating book to database.
        /// </summary>
        /// <param name="booksDetails"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<BooksDetails>> ModifyBook(BooksDetails booksDetails)
        {
            try
            {
                if (booksDetails.Book_Id == "")
                {
                    return BadRequest();
                }

                _usercontext.Entry(booksDetails).State = EntityState.Modified;

                try
                {
                    await _usercontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return CreatedAtAction("GetBookWithId", new { id = booksDetails.Book_Id }, booksDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// checking whether book exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BookExists(string id)
        {
            try
            {
                return _usercontext.Books.Any(e => e.Book_Id == id);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Deleting book in database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<BooksDetails>> DeleteBook(string id)
        {
            try
            {
                var currentuser = await _usercontext.Books.FindAsync(id);
                if (currentuser == null)
                {
                    return NotFound();
                }

                _usercontext.Books.Remove(currentuser);
                await _usercontext.SaveChangesAsync();

                return currentuser;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// getting all publishers from database to display in UI.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<BooksDetails>> GetAllPublishers()
        {
            try
            {
                var bookpublsrs = _booksService.GetAllPublishers();
                //await _usercontext.SaveChangesAsync();

                //return CreatedAtAction("GetUserWithId", new { id = AuthorDetails.User_Id }, AuthorDetails);
                return bookpublsrs.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Performing search based on criteria given by user.
        /// </summary>
        /// <param name="bookDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<List<BooksDetails>> SearchBookByCriteria(BookSearch bookDetails)
        {
            try
            {
                var currentBook = _booksService.SearchBooksByCriteria(bookDetails);
                if (currentBook != null)
                {
                    return currentBook;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
