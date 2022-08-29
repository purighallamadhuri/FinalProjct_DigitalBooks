using DigitalBooksLibrary.DAL;
using DigitalBooksLibrary.Model;

namespace DigitalBooksLibrary
{
    public class AuthorService : IAuthorService
    {
        public AuthorDBContext usersDbContext { get; set; }
        /// <summary>
        /// Configuring DBContext in Constructor.
        /// </summary>
        /// <param name="addusersDbContext"></param>
        public AuthorService(AuthorDBContext addusersDbContext)
        {
            usersDbContext = addusersDbContext;
        }
        /// <summary>
        /// Validating user and logging him.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<AuthorDetails> UserLogin(UserLogin userLogin)
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
        /// <summary>
        /// Service to get all authors.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<AuthorDetails> GetAllAuthors()
        {
            try
            {
                var userData = usersDbContext.Users.Where(us => us.User_Type == "Author").ToList();
                return userData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Service to add useres.
        /// </summary>
        /// <param name="AuthorDetails"></param>
        /// <returns></returns>
        public string AddUsers(AuthorDetails AuthorDetails)
        {
            try
            {
                var list = usersDbContext.Users.ToList();
                if (list.Count > 0)
                {
                    usersDbContext.Users.Add(AuthorDetails);
                    usersDbContext.SaveChanges();
                    return $"User added successfully {AuthorDetails.UserName}";
                }
                else
                {
                    return "Please add user";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Service to modify users.
        /// </summary>
        /// <param name="AuthorDetails"></param>
        /// <returns></returns>
        public string ModifyUser(AuthorDetails AuthorDetails)
        {
            try
            {
                var list = usersDbContext.Users.ToList();
                foreach (var u in list)
                {
                    int index = list.FindIndex(s => s.User_Id == AuthorDetails.User_Id);
                    list[index].UserName = AuthorDetails.UserName;
                    list[index].Password = AuthorDetails.Password;
                    list[index].User_Type = AuthorDetails.User_Type;
                    list[index].Created_On = AuthorDetails.Created_On;
                    list[index].EmailId = AuthorDetails.EmailId;
                    list[index].Mobile = AuthorDetails.Mobile;

                    usersDbContext.SaveChanges();
                }
                return "User details updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Service to delete user.
        /// </summary>
        /// <param name="AuthorDetails"></param>
        /// <returns></returns>
        public string DeleteUser(AuthorDetails AuthorDetails)
        {
            try
            {
                var list = usersDbContext.Users.ToList();
                int index = list.FindIndex(s => s.User_Id == AuthorDetails.User_Id);
                if (index > 0)
                {
                    usersDbContext.Users.Remove(AuthorDetails);
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
    }
}
