using Magazine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly MagazineContext _context;

        public OrderStatusesController(MagazineContext context)
        {
            _context = context;
        }
        [HttpGet("GetOrderTypeById/{id}")]
        public IActionResult GetOrderStatusById(int id)
        {
            var orderStatus = _context.OrderStatuses.Find(id);
            if (orderStatus == null)
            {
                return NotFound();

            }
            return Ok(orderStatus);
        }
        [HttpPost("AddOrderType")]
        public IActionResult AddOrderStatus([FromBody] OrderStatus orderStatus)
        {
            if (orderStatus == null) { return BadRequest(); }

            _context.OrderStatuses.Add(orderStatus);
            _context.SaveChanges();
            return Ok(orderStatus);
        }
        [HttpDelete("DeleteOrderType{id}")]
        public void DeleteOrderStatus(int id)
        {
            var orderStatus = _context.OrderStatuses.FirstOrDefault(r => r.Id == id);
            if (orderStatus != null)
            {
                _context.OrderStatuses.Remove(orderStatus);
                _context.SaveChanges();
            }
        }
    }
}
