using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokoWebAPI.DTO;
using TokoWebAPI.Models;

namespace TokoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TokoDatabaseContext _context;

        public ProductController(TokoDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          var allProduct = await _context.Products
                .Join (_context.Categories, product => product.CatId, category=> category.CategoryId,
                (product, category) => new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Stock = product.Stock,
                    Price = product.Price,
                    CatId = product.CatId,
                    Cat = category
                }).ToListAsync();
            return allProduct;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            //var product = await _context.Products.FindAsync(id);
            var product = await _context.Products
                .Join(_context.Categories, product => product.CatId, category => category.CategoryId,
                (product, category) => new Product
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Stock = product.Stock,
                    Price = product.Price,
                    CatId = product.CatId,
                    Cat = category
                }).Where(x=>x.ProductId == id).FirstAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, ProductDto productDto)
        {
            Product? product = _context.Products.FirstOrDefault(c => c.ProductId == id);
            if (product == null)
            {
                return BadRequest();
            }
            product.ProductName = productDto.ProductName;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.CatId = productDto.CatId;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto productDto)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'TokoDatabaseContext.Products'  is null.");
          }
            Product product = new Product();
            if (productDto != null)
            {
                product = new Product(productDto);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
