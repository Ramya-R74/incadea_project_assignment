using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Data;
using Mini_ERP_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mini_ERP_System.Controllers
{
    [ApiController]
    [Route("api/suppliers")]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Helper method to check role authorization
        private bool IsAuthorized(string userRole, params string[] allowedRoles)
        {
            return allowedRoles.Contains(userRole);
        }

        // ✅ Create Supplier (Only Admin)
        [HttpPost]
        [Route("AddSuppliers")]
        public async Task<IActionResult> CreateSupplier([FromHeader(Name = "Role")] string userRole, [FromBody] Supplier supplier)
        {
            if (!IsAuthorized(userRole, "Admin"))
                return Unauthorized(new { message = "Access denied. Only Admin can create suppliers." });

            if (supplier == null)
                return BadRequest(new { message = "Supplier data is required" });

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.SupplierId }, supplier);
        }

        // ✅ Get Supplier by ID (Only Admin, Purchase)
        [HttpGet]
        [Route("GetSupplier/{id}")]
        public async Task<IActionResult> GetSupplierById([FromHeader(Name = "Role")] string userRole, int id)
        {
            if (!IsAuthorized(userRole, "Admin", "Purchase"))
                return Unauthorized(new { message = "Access denied." });

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = "Supplier not found" });

            return Ok(supplier);
        }

        // ✅ Update Supplier (Only Admin, Purchase)
        [HttpPut]
        [Route("UpdateSuppliers/{id}")]
        public async Task<IActionResult> UpdateSupplier([FromHeader(Name = "Role")] string userRole, int id, [FromBody] Supplier updatedSupplier)
        {
            if (!IsAuthorized(userRole, "Admin", "Purchase"))
                return Unauthorized(new { message = "Access denied." });

            if (updatedSupplier == null || id != updatedSupplier.SupplierId)
                return BadRequest(new { message = "Invalid supplier data" });

            var existingSupplier = await _context.Suppliers.FindAsync(id);
            if (existingSupplier == null)
                return NotFound(new { message = "Supplier not found" });

            // Update supplier properties
            existingSupplier.SupplierName = updatedSupplier.SupplierName;
            existingSupplier.Email = updatedSupplier.Email;
            existingSupplier.Phone = updatedSupplier.Phone;
            existingSupplier.Address = updatedSupplier.Address;

            _context.Suppliers.Update(existingSupplier);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Supplier updated successfully" });
        }

        // ✅ Delete Supplier (Only Admin, Purchase)
        [HttpDelete]
        [Route("DeleteSuppliers/{id}")]
        public async Task<IActionResult> DeleteSupplier([FromHeader(Name = "Role")] string userRole, int id)
        {
            if (!IsAuthorized(userRole, "Admin", "Purchase"))
                return Unauthorized(new { message = "Access denied." });

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound(new { message = "Supplier not found" });

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Supplier deleted successfully" });
        }

        // ✅ Get All Suppliers (Only Admin, Purchase)
        [HttpGet]
        [Route("GetAllSuppliers")]
        public async Task<IActionResult> GetAllSuppliers([FromHeader(Name = "Role")] string userRole)
        {
            if (!IsAuthorized(userRole, "Admin", "Purchase"))
                return Unauthorized(new { message = "Access denied." });

            var suppliers = await _context.Suppliers.ToListAsync();
            return Ok(suppliers);
        }
    }
}
