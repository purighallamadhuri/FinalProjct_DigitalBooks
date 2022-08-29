using System.ComponentModel.DataAnnotations;
namespace DigitalBooksLibrary.Model
{
    public class AuthorDetails
    {
        [Key]
        [Required]
        public int User_Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? User_Type { get; set; }
        public DateTime Created_On { get; set; }
        public string? EmailId { get; set; }
        public string? Mobile { get; set; }
    }
    public class UserLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    public class JsonResponse
    {
        public string? Token { get; set; }
        public Boolean status { get; set; }
    }
}

