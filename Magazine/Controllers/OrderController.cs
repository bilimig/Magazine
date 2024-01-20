using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MagazineContext _context;

        public OrderController(MagazineContext context)
        {
            _context = context;

        }
        [HttpGet("GetOrder/{order_id}")]
        public IActionResult GetOrder(int order_id)
        {
            var order = _context.Orders.Find(order_id);
            if(order == null)
            {
                return NotFound();
            }

            return Ok(order); 
        }
        [HttpGet("GetOrderStatus/{status_id}")]
        public IActionResult GetOrderStatus(int status_id)
        {
            var current_status = _context.OrderStatuses.Find(status_id);

            if (current_status == null)
            {
                return NotFound();
            }

            return Ok(current_status);

        }

        [HttpPut("UpdateOrderStatus/{orderId}/{newStatusId}")]
        public IActionResult UpdateOrderStatus(int orderId, int newStatusId)
        {
            var order = _context.Orders.Find(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var newStatus = _context.OrderStatuses.Find(newStatusId);

            if (newStatus == null)
            {
                return BadRequest("Invalid status ID"); 
            }

            order.StatusId = newStatusId;
            _context.SaveChanges();

            return Ok(order);
        }
        [HttpDelete("DeleteOrder/{order}")]
        public Order DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();

            return order;
        }
        [HttpGet("GetAllOrderCleaningHistories")]
        public List<Order> GetAllOrderCleaningHistories()
        {
            return _context.Orders.ToList();
        }
        [HttpGet("GetOrderCleaningHistoryById")]
        public IActionResult GetOrderCleaningHistoryById(int id)
        {
            if(_context.Orders.FirstOrDefault(r => r.Id == id) == null)
            {
                return NotFound();
            }
            
            return Ok(_context.Orders.FirstOrDefault(r => r.Id == id));
        }
        [HttpGet("GetOrderCleaningHistoriesByFilter/{filter}")]
        public List<Order> GetOrderCleaningHistoriesByFilter(Expression<Func<Order, bool>> filter)
        {
            return _context.Orders.Where(filter).ToList();
        }
        [HttpGet("UpdateOrderCleaningHistory/{cleaningHistory}")]
        public Order UpdateOrderCleaningHistory(Order cleaningHistory)
        {
            _context.Orders.Update(cleaningHistory);
            _context.SaveChanges();
            return cleaningHistory;
        }
        [HttpDelete("DeleteOrderCleaningHistory/{id}")]
        public void DeleteOrderCleaningHistory(int id)
        {
            var cleaningHistory = _context.Orders.FirstOrDefault(r => r.Id == id);
            if (cleaningHistory != null)
            {
                _context.Orders.Remove(cleaningHistory);
                _context.SaveChanges();
            }
        }


    }
}
