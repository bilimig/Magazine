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
        [HttpPost("AddNewOrderStatus")]
        public IActionResult AddNewClient([FromBody] OrderStatusInput orderstatusinput)
        {
            if (ModelState.IsValid)
            {

                var orderstatus= new OrderStatus
                {
                   
                    Status = orderstatusinput.Status,

                };


              
                if (_context.OrderStatuses.Find(orderstatus.Id) == null)
                {
                    return BadRequest();
                }
                _context.OrderStatuses.Add(orderstatus);
                _context.SaveChangesAsync();

                return Ok(new { Message = "Order Status added successfully.", OrderStatusId = orderstatus.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
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
        [HttpGet("GetAllStatuses")]
        public IActionResult GetAllStatuses()
        {
            return Ok(_context.OrderStatuses.ToList());
        }
    }
}
