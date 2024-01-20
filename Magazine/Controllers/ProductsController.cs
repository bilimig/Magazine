using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MagazineContext _context;

        public ProductsController(MagazineContext context)
        {
            _context = context;

        }
        [HttpPost("AddNewProduct")]
        public IActionResult AddNewProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpGet("GetProduct/{product_id}")]
        public IActionResult GetProduct(int product_id)
        {
            var product = _context.Orders.Find(product_id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("DeleteProduct/{product}")]
        public Product DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();

            return product;
        }
        [HttpPut("UpdateProductAmount/{product_id}/{amount}")]
        public IActionResult UpdateProductAmount(int  product_id, int amount)
        {
            var product = _context.Products.Find(product_id);
            if (product == null)
            {
                return NotFound();
            }
            product.Amount = amount;
            _context.SaveChanges();

            return Ok(product);

        }

        [HttpGet("GetAllProduct")]
        public List<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }
        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            if (_context.Products.FirstOrDefault(r => r.Id == id) == null)
            {
                return NotFound();
            }

            return Ok(_context.Products.FirstOrDefault(r => r.Id == id));
        }

        [HttpGet("GetProductByFilter/{filter}")]
        public List<Product> GetProductByFilter(Expression<Func<Product, bool>> filter)
        {
            return _context.Products.Where(filter).ToList();
        }
        [HttpGet("UpdateProduct")]
        public Product UpdateProduct(Product cleaningHistory)
        {
            _context.Products.Update(cleaningHistory);
            _context.SaveChanges();
            return cleaningHistory;
        }
        [HttpDelete("DeleteProduc/{id}")]
        public void DeleteProduct(int id)
        {
            var cleaningHistory = _context.Products.FirstOrDefault(r => r.Id == id);
            if (cleaningHistory != null)
            {
                _context.Products.Remove(cleaningHistory);
                _context.SaveChanges();
            }
        }
        //dokonczyc jak cos brakuje

    }
}
