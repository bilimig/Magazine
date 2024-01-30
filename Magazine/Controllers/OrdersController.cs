using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MagazineContext _context;
        public OrdersController(MagazineContext context)
        {
            _context = context;

        }

        

        [HttpGet("GetAllOrderandItemsByOrder/{order_id}")]
        public IActionResult GetAllOrderandItemsByOrder(int order_id)
        {
            var orderItems = _context.OrderItems.Where(item => item.OrderId == order_id).ToList();

            if (orderItems == null)
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        [HttpGet("GetOrderStatusByOrderId/{order_id}")]
        public IActionResult GetOrderStatusByOrderId(int order_id)
        {
            var current_order = _context.Orders.Find(order_id);
            if (current_order == null) { return NotFound(); }

            int status_id = current_order.StatusId.Value; 
            

            var current_status = _context.OrderStatuses.Find(status_id);

            if (current_status == null)
            {
                return NotFound();
            }

            return Ok(current_status);

        }

        [HttpGet("GetOrderTypeByOrderId/{order_id}")]
        public IActionResult GetOrderTypeByOrderId(int order_id)
        {
            var current_order = _context.Orders.Find(order_id);
            if (current_order == null) { return NotFound(); }

            int status_id = current_order.TypeId.Value;


            var current_type = _context.OrderTypes.Find(status_id);

            if (current_type == null)
            {
                return NotFound();
            }

            return Ok(current_type);

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

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()

        {          
            return Ok(_context.Orders.ToList());
        }

        [HttpGet("GetOrderById")]
        public IActionResult GetOrderById(int id)
        {
            if(_context.Orders.FirstOrDefault(r => r.Id == id) == null)
            {
                return NotFound();
            }
            
            return Ok(_context.Orders.FirstOrDefault(r => r.Id == id));
        }

        [HttpGet("GetOrderedOrdersByDate")]
        public IActionResult GetOrderedOrdersByDate()
        {

            List<Order> orders = _context.Orders
                .ToList();
            if (orders == null)
            {
                return BadRequest();
            }
            var sortedOrders = orders.OrderBy(order => order.Date).ToList();
            return Ok(sortedOrders);
        }


        [HttpPost("AddNewOrder")]
        public IActionResult AddNewOrder([FromBody] OrderInput orderinput)
        {
            if (ModelState.IsValid)
            {

                var order = new Order
                {
                    //Id = orderinput.Id,
                    UserId = orderinput.UserId,
                    ClientId = orderinput.ClientId,
                    Date = orderinput.Date,
                    StatusId = orderinput.StatusId,
                    TotalValue = orderinput.TotalValue,
                    TypeId = orderinput.TypeId,


                };
                if (order.UserId <= 0 || order.ClientId <= 0 || order.StatusId <= 0 || order.TypeId <= 0 || order.TotalValue < 0 || order.Date == null)
                {
                    return BadRequest();
                }


                if (_context.Clients.Find(order.ClientId) == null)
                {
                    return BadRequest();
                }
                if (_context.Users.Find(order.UserId) == null)
                {
                    return BadRequest();
                }
                if (_context.OrderStatuses.Find(order.StatusId) == null)
                {
                    return BadRequest();
                }
                if (_context.OrderTypes.Find(order.TypeId) == null)
                {
                    return BadRequest();
                }

                _context.Orders.Add(order);
                _context.SaveChanges();

                return Ok(new { Message = "Order added successfully.", OrderId = order.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
        public void DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(r => r.Id == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        [HttpPost("UpdateOrder/{order_id}")]
        public IActionResult UpdateClient([FromBody] OrderInput orderinput, int order_id)
        { 
            if (_context.Orders.Find(order_id) == null) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var order = _context.Orders.Find(order_id);
                {
                    //order.Id = orderinput.Id;
                    order.UserId = orderinput.UserId;
                    order.ClientId = orderinput.ClientId;
                    order.Date = orderinput.Date;
                    order.StatusId = orderinput.StatusId;
                    order.TotalValue = orderinput.TotalValue;
                    order.TypeId = orderinput.TypeId;


                };
                if (order.UserId <= 0 || order.ClientId <= 0 || order.StatusId <= 0 || order.TypeId <= 0 || order.TotalValue < 0 || order.Date == null)
                {
                    return BadRequest();
                }


                

                if (_context.Clients.Find(order.ClientId) == null)
                {
                    return BadRequest();
                }
                if (_context.Users.Find(order.UserId) == null)
                {
                    return BadRequest();
                }
                if (_context.OrderStatuses.Find(order.StatusId) == null)
                {
                    return BadRequest();
                }
                if (_context.OrderTypes.Find(order.TypeId) == null)
                {
                    return BadRequest();
                }

                _context.Orders.Add(order);
                _context.SaveChangesAsync();

                return Ok(new { Message = "Client added successfully.", OrderId = order.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpPut("ChangeOrderTotalValue/{orderid}/{totalAmount}")]
        public IActionResult ChangeOrderTotalValue(int orderid, decimal totalAmount)
        {
            var order = _context.Orders.Find(orderid);

            if (order == null)
            {
                return NotFound();
            }

            

            
            order.TotalValue = totalAmount;
            _context.SaveChanges();

            return Ok(order);
        }


    }
}
