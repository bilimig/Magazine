using Magazine.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        private readonly MagazineContext _context;
        public ContactDetailsController(MagazineContext context)
        {
            _context = context;
        }

        // GET: api/<ContactDetailsController>
        [HttpPost("AddNewContactDetails")]
        public IActionResult AddNewContactDetails([FromBody]ContactDetail contact_details)
        {
            if (contact_details == null)
            {
                return BadRequest();
            }
            _context.ContactDetails.Add(contact_details);
            _context.SaveChanges();
            return Ok(contact_details);
        }

        [HttpGet("ContactDetailsById/{client_id}")]
        public IActionResult UomGetById(int details_id)
        {
            var details = _context.ContactDetails.Find(details_id);
            if (details == null)
            {
                return NotFound();
            }

            return Ok(details);
        }

        [HttpGet("GetContactDetailsByClientId")]
        public IActionResult GetContactDetailsByClientId(int client_id)
        {
            var client = _context.Clients.Find(client_id);
            if (client == null)
            {
                return NotFound();
            }

            var details_id = client.DetailsId.Value;
            var contact_details = _context.ContactDetails.Find(details_id);
            if (contact_details == null)
            {
                return NotFound();

            }
            return Ok(contact_details);
        }

        [HttpDelete("DeleteContactDetails/{id}")]
        public void DeleteContactDetails(int id)
        {
            var deatails = _context.ContactDetails.FirstOrDefault(r => r.Id == id);
            if (deatails != null)
            {
                _context.ContactDetails.Remove(deatails);
                _context.SaveChanges();
            }
        }




    }
}
