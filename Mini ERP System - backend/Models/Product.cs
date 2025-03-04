using System.ComponentModel.DataAnnotations;

namespace Mini_ERP_System.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]  
        public decimal Price { get; set; } = decimal.Zero;

        [Required]
        public int Stock { get; set; } = 0;
    }
}
