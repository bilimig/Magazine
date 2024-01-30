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

        [HttpGet("GetAllContactDetails")]
        public IActionResult GetAllContactDetails()
        {
            return Ok(_context.ContactDetails.ToList());
        }

        [HttpPost("AddNewClientContactDetails")]
        public IActionResult AddNewClientContactDetails([FromBody] ContactDetailInput detailinput)
        {
            if (ModelState.IsValid)
            {

                var detail = new ContactDetail
                {

                    Name = detailinput.Name,
                    SecondName = detailinput.SecondName,
                    Phone = detailinput.Phone,
                    Address = detailinput.Address,
                };

                if (detail.Name == null || detail.SecondName == null || detail.Phone == null || detail.Address == null)
         {
                    return BadRequest();
                }

                _context.ContactDetails.Add(detail);
                _context.SaveChanges();
                var client = new Client { DetailsId = detail.Id };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return Ok(new { Message = "Details added successfully.", DetailId = detail.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("AddNewUserContactDetails")]
        public IActionResult AddNewUserContactDetails([FromBody] ContactDetailInput detailinput)
        {
            if (ModelState.IsValid)
            {

                var detail = new ContactDetail
                {

                    Name = detailinput.Name,
                    SecondName = detailinput.SecondName,
                    Phone = detailinput.Phone,
                    Address = detailinput.Address,
                };

                if (detail.Name == null || detail.SecondName == null || detail.Phone == null || detail.Address == null)
         {
                    return BadRequest();
                }

                _context.ContactDetails.Add(detail);
                _context.SaveChanges();
                var client = new Client { DetailsId = detail.Id };

                _context.Clients.Add(client);
                _context.SaveChanges();

                return Ok(new { Message = "Details added successfully.", DetailId = detail.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }







        [HttpPost("AddNewContactDetails")]
        public IActionResult AddNewContactDetails([FromBody] ContactDetailInput detailinput)
        {
            if (ModelState.IsValid)
            {

                var detail = new ContactDetail
                {

                    Name = detailinput.Name,
                    SecondName = detailinput.SecondName,
                    Phone = detailinput.Phone,
                    Address = detailinput.Address,
                };
            
                if(detail.Name == null || detail.SecondName == null || detail.Phone == null || detail.Address == null)
                {
                    return BadRequest();
                }
                
                _context.ContactDetails.Add(detail);
                _context.SaveChanges();

                return Ok(new { Message = "Details added successfully.", DetailId = detail.Id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("GetContactDetailsById/{details_id}")]
        public IActionResult GetContactDetailsById(int details_id)
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

        //[HttpPost("UpdateContactDetails")]
        //public IActionResult UpdateContactDetails([FromBody] ContactDetail contactDetail)
        //{
        //    if (contactDetail == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (_context.Products.Find(contactDetail.Id) == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Products.Update(product);
        //    _context.SaveChanges();
        //    return Ok();
        //}




    }
}
