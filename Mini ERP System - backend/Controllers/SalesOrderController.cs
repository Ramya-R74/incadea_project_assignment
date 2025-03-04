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
    [Route("api/sales-orders")]
    public class SalesOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesOrderController(AppDbContext context)
        {
            _context = context;
        }

        // Create Sales Order (Admin & Sales Only)
        [HttpPost("{userRole}/create")]
        public async Task<IActionResult> CreateSalesOrder(string userRole, [FromBody] SalesOrder newOrder)
        {
            if (!IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Sales can create sales orders." });

            if (newOrder == null)
                return BadRequest(new { message = "Invalid sales order data" });

            _context.SalesOrders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesOrderById), new { id = newOrder.SalesOrderId }, newOrder);
        }

        // Get All Sales Orders (Admin & Sales Only)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSalesOrders([FromHeader(Name = "X-User-Role")] string userRole)
        {
            if (!IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Sales can view sales orders." });

            var salesOrders = await _context.SalesOrders.ToListAsync();
            return Ok(salesOrders);
        }

        //  Get Sales Order By ID (Admin & Sales Only)
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSalesOrderById(int id, [FromHeader(Name = "X-User-Role")] string userRole)
        {
            if (!IsAuthorized(userRole))
                return Unauthorized(new { message = "Access denied. Only Admin and Sales can view sales orders." });

            var salesOrder = await _context.SalesOrders.FindAsync(id);
            if (salesOrder == null)
                return NotFound(new { message = "Sales order not found" });

            return Ok(salesOrder);
        }

        // ✅ Helper Method: Check Role
        private bool IsAuthorized(string userRole)
        {
            return userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                   userRole.Equals("Sales", StringComparison.OrdinalIgnoreCase);
        }
    }
}

