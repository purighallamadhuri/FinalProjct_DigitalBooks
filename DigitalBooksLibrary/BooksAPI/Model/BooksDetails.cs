using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Model
{
    public class BooksDetails
    {
        [Key]
        [Required]
        public string? Book_Id { get; set; }
        public string? Logo { get; set; }
        public string? Book_Title { get; set; }
        public string? Category { get; set; }
        public int? Price { get; set; }
        public int Author_Id { get; set; }
        public string? Publisher { get; set; }
        public DateTime Published_Date { get; set; }
        public string? Content { get; set; }
        public bool Active { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }


    }
    public class BookSearch
    {
        public string? Category { get; set; }
        public int Author_Id { get; set; }
        public string? Publisher { get; set; }
    }
    public class GetBooksByAuthorID
    {
        public int Author_Id { get; set; }
    }
}
