using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly MagazineContext _context;
        public ClientsController(MagazineContext context)
        {
            _context = context;
        }

        // GET: api/<ContactDetailsController>
        [HttpPost("AddNewClient")]
        public IActionResult AddNewClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            _context.Clients.Add(client);
            _context.SaveChanges();
            return Ok(client);
        }

        [HttpGet("GetClientById/{client_id}")]
        public IActionResult GetClientById(int client_id)
        {
            var client = _context.Clients.Find(client_id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpDelete("DeleteClient/{id}")]
        public void DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(r => r.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }
        [HttpGet("GetClientsByFilter/{filter}")]
        public List<Client> GetClientsByFilter(Expression<Func<Client, bool>> filter)
        {
            return _context.Clients.Where(filter).ToList();
        }

    }
}
