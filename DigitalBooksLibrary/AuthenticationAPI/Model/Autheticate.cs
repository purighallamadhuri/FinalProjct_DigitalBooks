using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Model
{
    public class Autheticate
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        
    }
    public class UsersDetails
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
}
