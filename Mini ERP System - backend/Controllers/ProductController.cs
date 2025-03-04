using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Data;
using Mini_ERP_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mini_ERP_System.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET : Get product by id (Accessible to Admin, Sales, Customer, and Purchase roles)
        [HttpGet]
        [Route("GetProductBy/{id}")]
        public async Task<IActionResult> GetProductBy(int id, [FromQuery] string role)
        {
            // Validate role
            var allowedRoles = new List<string> { "Admin", "Sales", "Customer", "Purchase" };
            if (!allowedRoles.Contains(role))
            {
                return Forbid(); // Return 403 Forbidden if the role is not allowed
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            return Ok(product);
        }

        // POST: Add a Product (Role-based authorization)
        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromQuery] string role, [FromBody] Product product)
        {
            if (!IsAuthorizedRole(role, new[] { "Admin" }))
                return Unauthorized(new { message = "Access denied" });

            if (product == null)
                return BadRequest(new { message = "Product data is required" });

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllProducts), new { id = product.ProductId }, product);
        }

        // PUT: Update a Product (Role-based authorization)
        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromQuery] string role, [FromBody] Product product)
        {
            if (!IsAuthorizedRole(role, new[] { "Admin", "Sales" }))
                return Unauthorized(new { message = "Access denied" });

            if (product == null || id <= 0 || id != product.ProductId)
                return BadRequest(new { message = "Invalid product data" });

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound(new { message = "Product not found" });

            // Update product properties
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product updated successfully" });
        }

        // DELETE: Delete a Product (Role-based authorization)
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromQuery] string role)
        {
            if (!IsAuthorizedRole(role, new[] { "Admin" }))
                return Unauthorized(new { message = "Access denied" });

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Product deleted successfully" });
        }

        // GET: Get All Products (Accessible to Admin, Sales, Customer, and Purchase roles)
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string role)
        {
            if (!IsAuthorizedRole(role, new[] { "Admin", "Sales", "Customer", "Purchase" }))
                return Unauthorized(new { message = "Access denied" });

            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // Role-based validation method
        private bool IsAuthorizedRole(string role, string[] allowedRoles)
        {
            return !string.IsNullOrEmpty(role) && allowedRoles.Contains(role);
        }
    }
}
