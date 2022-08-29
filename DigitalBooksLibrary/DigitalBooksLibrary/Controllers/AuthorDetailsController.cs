using DigitalBooksLibrary.DAL;
using DigitalBooksLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalBooksLibrary.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class AuthorDetailsController : ControllerBase
    {
        private readonly AuthorDBContext _usercontext;
        private readonly IAuthorService _authorService;

        public AuthorDetailsController(AuthorDBContext usersdbcontext,IAuthorService authorService)
        {
            _usercontext = usersdbcontext;
            _authorService = authorService;
        }
        /// <summary>
        /// Getting users from db.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDetails>>> GetUsers()
        {
            try
            {
                return await _usercontext.Users.ToListAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Getting users based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetails>> GetUserWithId(int id)
        {
            try
            {
                var user = await _usercontext.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// updating users based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AuthorDetails"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDetails>> ModifyUser(int id, AuthorDetails AuthorDetails)
        {
            if (id != AuthorDetails.User_Id)
            {
                return BadRequest();
            }

            _usercontext.Entry(AuthorDetails).State = EntityState.Modified;

            try
            {
                await _usercontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetUserWithId", new { id = AuthorDetails.User_Id }, AuthorDetails);
        }
        /// <summary>
        /// Adding users to database.
        /// </summary>
        /// <param name="AuthorDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorDetails>> AddUser(AuthorDetails AuthorDetails)
        {
            try
            {
                _usercontext.Users.Add(AuthorDetails);
                await _usercontext.SaveChangesAsync();

                return CreatedAtAction("GetUserWithId", new { id = AuthorDetails.User_Id }, AuthorDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Getting users details after login.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<List<AuthorDetails>> GetLoginUserDtls(UserLogin userLogin)
        {
            try
            {
                var userData = _authorService.UserLogin(userLogin);
                //await _usercontext.SaveChangesAsync();

                /*return CreatedAtAction("GetUserWithId", new { id = AuthorDetails.User_Id }, AuthorDetails)*/;
                return userData;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Getting all authors to display them in UI.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult<List<AuthorDetails>> GetAuthors()
        {
            try
            {
                var users = _authorService.GetAllAuthors();
                //await _usercontext.SaveChangesAsync();

                //return CreatedAtAction("GetUserWithId", new { id = AuthorDetails.User_Id }, AuthorDetails);
                return users.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Deleting user based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorDetails>> DeleteUser(int id)
        {
            try
            {
                var currentuser = await _usercontext.Users.FindAsync(id);
                if (currentuser == null)
                {
                    return NotFound();
                }

                _usercontext.Users.Remove(currentuser);
                await _usercontext.SaveChangesAsync();

                return currentuser;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Checking whether user exists o rnot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UserExists(int id)
        {
            try
            {
                return _usercontext.Users.Any(e => e.User_Id == id);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
