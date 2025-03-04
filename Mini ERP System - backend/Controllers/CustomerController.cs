using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Data;
using Mini_ERP_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mini_ERP_System.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Method to Check Authorization Based on Role
        private bool IsAuthorized(string userRole, params string[] allowedRoles)
        {
            return allowedRoles.Contains(userRole);
        }

        // ✅ Customer Registration (Accessible to All)
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest(new { message = "Customer data is required" });

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        // ✅ Get Customer by ID (Only Admin, Sales, Customer, Purchase)
        [HttpGet]
        [Route("GetCustomer/{id}")]
        public async Task<IActionResult> GetCustomerById([FromHeader(Name = "UserRole")] string userRole, int id)
        {
            if (!IsAuthorized(userRole, "Admin", "Sales", "Customer", "Purchase"))
                return Unauthorized(new { message = "Access denied" });

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(customer);
        }

        // ✅ Update Customer (Only Admin)
        [HttpPut]
        [Route("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer([FromHeader(Name = "UserRole")] string userRole, int id, [FromBody] Customer updatedCustomer)
        {
            if (!IsAuthorized(userRole, "Admin", "Sales"))
                return Unauthorized(new { message = "Access denied" });

            if (updatedCustomer == null || id != updatedCustomer.CustomerId)
                return BadRequest(new { message = "Invalid customer data" });

            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null)
                return NotFound(new { message = "Customer not found" });

            // Update customer properties
            existingCustomer.CustomerName = updatedCustomer.CustomerName;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Phone = updatedCustomer.Phone;
            existingCustomer.Address = updatedCustomer.Address;

            _context.Customers.Update(existingCustomer);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Customer updated successfully" });
        }

        // ✅ Delete Customer (Only Admin)
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer([FromHeader(Name = "UserRole")] string userRole, int id)
        {
            if (!IsAuthorized(userRole, "Admin"))
                return Unauthorized(new { message = "Access denied" });

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Customer deleted successfully" });
        }

        // ✅ Get All Customers (Only Admin, Sales, Customer, Purchase)
        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomers()
        {
            //if (!IsAuthorized(userRole, "Admin", "Sales", "Customer", "Purchase"))
            //    return Unauthorized(new { message = "Access denied" });

            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }
    }
}
