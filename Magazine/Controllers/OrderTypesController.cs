using Magazine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {
        private readonly MagazineContext _context;

        public OrderTypesController(MagazineContext context)
        {
            _context = context;
        }
        [HttpGet("GetOrderTypeById/{id}")]
        public IActionResult GetOrderTypeById(int id)
        {
            var order_type = _context.OrderTypes.Find(id);
            if (order_type == null)
            {
                return NotFound();

            }
            return Ok(order_type);
        }
        [HttpPost("AddNewOrderType")]
        public IActionResult AddNewOrderType([FromBody] OrderTypeInput ordertypeinput)
        {
            if (ModelState.IsValid)
            {

                var ordertype = new OrderType
                {
                 
                    Type = ordertypeinput.Type,

                };
            

                _context.OrderTypes.Add(ordertype);
                _context.SaveChangesAsync();

                return Ok(new { Message = "Client added successfully.", OrderTypeId = ordertype.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }





        [HttpDelete("DeleteOrderType{id}")]
        public void DeleteOrderTyper(int id)
        {
            var order_type = _context.OrderTypes.FirstOrDefault(r => r.Id == id);
            if (order_type != null)
            {
                _context.OrderTypes.Remove(order_type);
                _context.SaveChanges();
            }
        }

        [HttpGet("GetAllTypes")]
        public IActionResult GetAllTypes()
        {
            return Ok(_context.OrderTypes.ToList());
        }

    }
}
