using Magazine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagazinesController : ControllerBase
    {
        private readonly MagazineContext _context;

        public MagazinesController(MagazineContext context)
        {
            _context = context;
        }
        [HttpGet("GetMagazineById/{id}")]
        public IActionResult GetMagazineById(int id)
        {
            var magazine = _context.Magazines.Find(id);
            if (magazine == null)
            {
                return NotFound();

            }
            return Ok(magazine);
        }
       
        [HttpDelete("DeleteMagazine{id}")]
        public void DeleteMagazine(int id)
        {
            var magazine = _context.Magazines.FirstOrDefault(r => r.Id == id);
            if (magazine != null)
            {
                _context.Magazines.Remove(magazine);
                _context.SaveChanges();
            }
        }
    }
}
