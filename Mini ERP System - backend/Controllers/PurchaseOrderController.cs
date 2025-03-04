using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Data;
using Mini_ERP_System.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mini_ERP_System.Controllers
{
    [ApiController]
    [Route("api/purchase-orders")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchaseOrderController(AppDbContext context)
        {
            _context = context;
        }

        // Add new purchase orders (Only Admin and Purchase)
        [HttpPost("CreateOrder/{userRole}")]
        public async Task<IActionResult> CreatePurchaseOrder(string userRole, [FromBody] PurchaseOrder newOrder)
        {
            if (string.IsNullOrEmpty(userRole) || !IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Purchase can create purchase orders." });

            if (newOrder == null)
                return BadRequest(new { message = "Invalid purchase order data" });

            _context.PurchaseOrders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = newOrder.PurchaseOrderId }, newOrder);
        }

        // Get All Purchase Orders (Admin & Purchase Only)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPurchaseOrders([FromHeader(Name = "Role")] string userRole)
        {
            if (!IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Purchase can view purchase orders." });

            var purchaseOrders = await _context.PurchaseOrders.ToListAsync();
            return Ok(purchaseOrders);
        }

        // ✅ Purchase Order By ID (Admin & Purchase Only)
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetPurchaseOrderById(int id, [FromHeader(Name = "Role")] string userRole)
        {
            if (!IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Purchase can view purchase orders." });

            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
                return NotFound(new { message = "Purchase order not found" });

            return Ok(purchaseOrder);
        }

        // ✅ Helper Method: Check Role
        private bool IsAuthorized(string userRole)
        {
            return userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                   userRole.Equals("Purchase", StringComparison.OrdinalIgnoreCase);
        }


    }
}
