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

        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            if (_context.Products.FirstOrDefault(r => r.Id == id) == null)
            {
                return NotFound();
            }

            return Ok(_context.Products.FirstOrDefault(r => r.Id == id));
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

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpGet("GetOrderedProductsByAmount")]
        public IActionResult GetOrderedProductsByAmount()
        {

            List<Product> products = _context.Products
                .ToList();
            if (products == null)
            {
                return BadRequest();
            }
            var sortedProducts = products.OrderBy(product => product.Amount).ToList();
            return Ok(sortedProducts);
        }
        [HttpPost("AddNewProduct")]
        public IActionResult AddNewProduct([FromBody] ProductInput productinput)
        {
            if (ModelState.IsValid)
            {
                

                var product = new Product
                {
                
                    Name = productinput.Name,
                    UomId = productinput.UomId,
                    BaseUnit = productinput.BaseUnit,
                    Amount = productinput.Amount,

                };
                if (product.Amount <  0 || product.UomId <= 0 || product.BaseUnit == null || product.Name == null)
                {
                    return BadRequest();
                }


            

               

                var checkingProduct = _context.Products.Where(p => p.Name == product.Name && p.UomId == product.UomId && p.BaseUnit == product.BaseUnit).FirstOrDefault();

                if (checkingProduct != null)
                {
                    checkingProduct.Amount += productinput.Amount;
                }
                else
                {
                    _context.Products.Add(product);

                }

                
                _context.SaveChangesAsync();

                return Ok(new { Message = "Product added successfully.", Product = product.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public void DeleteProduct(int id)
        {
            var cleaningHistory = _context.Products.FirstOrDefault(r => r.Id == id);
            if (cleaningHistory != null)
            {
                _context.Products.Remove(cleaningHistory);
                _context.SaveChanges();
            }
        }
       

    }
}
