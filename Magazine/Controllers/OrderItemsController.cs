using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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


        [HttpPost("AddOrderItem")]
        public IActionResult AddOrderItem([FromBody]OrderItem item) 
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.OrderItems.Add(item);
            _context.SaveChanges();


            return Ok(item);
        }


        [HttpGet("GetOrderItems/{order_id}")]
        public IActionResult GetOrderItems(int order_id)
        {
            var order = _context.Orders.Find(order_id);

            if (order == null)
            {
                return NotFound();
            }

            var orderItems = order.OrderItems.ToList();

            return Ok(orderItems);
        }


        [HttpGet("GetOrderItem/{order_item}")]
        public IActionResult GetOrderItem(int order_item) {
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
