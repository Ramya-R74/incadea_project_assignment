using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mini_ERP_System.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public int UserId { get; set; } // Purchase team member

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("SupplierId")]
        [JsonIgnore]
        public Supplier? Supplier { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
