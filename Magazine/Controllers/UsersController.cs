using Magazine.Models;
using Microsoft.AspNetCore.Mvc;

namespace Magazine.Controllers
{
    public class UsersController : Controller
    {
        private readonly MagazineContext _context;

        public UsersController(MagazineContext context)
        {
            _context = context;
        }
        [HttpGet("GetUserById/{user_id}")]
        public IActionResult GetUserById(int user_id)
        {
            var user = _context.Users.Find(user_id);
            if (user == null)
            {
                return NotFound();

            }
            return Ok(user);
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody]User user)
        {
            if (user == null) { return BadRequest(); }

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        [HttpDelete("DeleteUser{user_id}")]
        public void DeleteUser(int user_id)
        {
            var user = _context.Users.FirstOrDefault(r => r.Id == user_id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        [HttpGet("IsAdminById/{user_id}")]
        public IActionResult IsAdminById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) { return NotFound(); }

            if (user.IsAdmin == true)
            {
                return Ok(true);
            }
            return Ok(false);
        }

    }

}
