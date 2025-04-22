using System.ComponentModel.DataAnnotations;

namespace CampusNews.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Token { get; set; }
        public DateTime TokenExpiration { get; set; }

     
    }
}
