﻿using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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

        [HttpGet("GetOrderStatus/{order_id}")]
        public IActionResult GetOrderStatus(int order_id)
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
        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
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
        [HttpGet("GetOrdersByFilter/{filter}")]
        public List<Order> GetOrdersByFilter(Expression<Func<Order, bool>> filter)
        {
            return _context.Orders.Where(filter).ToList();
        }
        [HttpGet("UpdateOrder/{order}")]
        public Order UpdateOrderCleaningHistory(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }//zapytac czy git

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


    }
}