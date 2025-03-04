using System.ComponentModel.DataAnnotations;

namespace Mini_ERP_System.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required, MaxLength(200)]
        public string? SupplierName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

    }
}
