using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Mini_ERP_System.Models
{
    public class SalesOrder
    {
        [Key]
        public int SalesOrderId { get; set; } 

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int UserId { get; set; } // Salesperson

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key Relationships
        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public Customer? Customer { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product? Product { get; set; }

    }
}
