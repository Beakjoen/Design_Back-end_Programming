using System.ComponentModel.DataAnnotations;

namespace NguyenQuocHuy.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
