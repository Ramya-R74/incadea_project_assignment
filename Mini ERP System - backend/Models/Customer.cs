using System.ComponentModel.DataAnnotations;

namespace Mini_ERP_System.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, MaxLength(200)]
        public string? CustomerName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }   

        public string? Address { get; set; } 

    }
}
