using System.ComponentModel.DataAnnotations;

namespace Mini_ERP_System.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
