using Magazine.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly MagazineContext _context;

        public OrderItemsController(MagazineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrderItem(OrderItem order_item) {
            var item = _context.OrderItems.Find(order_item);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }
        [HttpDelete]        
        public void DeleteOrderItem(int id)
        {
            var orderitem = _context.Orders.FirstOrDefault(r => r.Id == id);
            if (orderitem != null)
            {
                _context.Orders.Remove(orderitem);
                _context.SaveChanges();
            }

        }


    }
}
