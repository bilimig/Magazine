using Magazine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpGet("GetAllClientsWithDetails")]
        public IActionResult GetAllClientsWithDetails()
        {
            return Ok(_context.Clients.Include(client => client.Details).ToList());

        }


        // GET: api/<ContactDetailsController>
        [HttpPost("AddNewClient")]
        public IActionResult AddNewClient([FromBody] ClientInput clientinput)
        {
            if (ModelState.IsValid)
            {
                
                var client = new Client
                {
                    
                    DetailsId = clientinput.DetailsId,

                };
                if( client.DetailsId <= 0) 
                {
                    return BadRequest();
                }


        

                if (_context.ContactDetails.Find(client.DetailsId) == null)
                {
                    return BadRequest();
                }
                _context.Clients.Add(client);
                _context.SaveChangesAsync();

                return Ok(new { Message = "Client added successfully.", ClientId = client.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
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
        [HttpGet("GetAllClients")]
        public IActionResult GetAllClients()
        {
            return Ok(_context.Clients.ToList());
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
        [HttpGet("GetOrderedClients")]
        public IActionResult GetOrderedClients()
        {

            List<Client> clients = _context.Clients
                .Include(client => client.Details)
                .ToList();
            if (clients == null)
            {
                return BadRequest();
            }
            var sortedClients = clients.OrderBy(client => client.Details?.Name, StringComparer.OrdinalIgnoreCase).ToList();
            return Ok(sortedClients);
        }

        [HttpGet("GetClientWithInformation/{client_id}")]
        public IActionResult GetClientWithInformation(int client_id)
        {
            var clientDetails = _context.Clients
         .Where(client => client.Id == client_id)
         .Select(client => new
         {
             client.Id,
             client.DetailsId,
             ContactDetails = client.Details != null ? new
             {
                 client.Details.Id,
                 client.Details.Name,
                 client.Details.SecondName,
                 client.Details.Phone,
                 client.Details.Address
             } : null,
         })
         .ToList();

            if (clientDetails == null || !clientDetails.Any())
            {
                return NotFound();
            }

            return Ok(clientDetails); ;
        }

        [HttpPost("UpdateClientContactDetails/{client_id}")]
        public IActionResult UpdateClient([FromBody] ContactDetailInput detailinput,int client_id)
        {
            var client = _context.Clients.Find(client_id);
            if (_context.Clients.Find(client_id) == null){ return NotFound(); }

            if (ModelState.IsValid)
            {
                var detail = _context.ContactDetails.Find(client.DetailsId);
                {
                    detail.Name = detailinput.Name;
                    detail.SecondName = detailinput.SecondName;
                    detail.Phone = detailinput.Phone;
                    detail.Address = detailinput.Address;
                };
             


           
                if (detail.Name == null || detail.SecondName == null || detail.Phone == null || detail.Address == null)
                {
                    return BadRequest();
                }

                _context.ContactDetails.Update(detail);
                _context.SaveChangesAsync();

                return Ok(new { Message = "Details added successfully.", DetailId = detail.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

    }
}
