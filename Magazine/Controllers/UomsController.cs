﻿using Magazine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magazine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UomsController : ControllerBase
    {
        private readonly MagazineContext _context;
        public UomsController(MagazineContext context)
        {
            _context = context;
        }

        [HttpGet("UomGetById/{oum_id}")]
        public IActionResult UomGetById(int oum_id) 
        {
            var uom = _context.Uoms.Find(oum_id);
            if(uom == null)
            {
                return NotFound();
            }

            return Ok(uom);
        }

        [HttpDelete("UomDelete{uom_id}")]
        public void UomDelete(int uom_id)
        {
            var uom = _context.Uoms.FirstOrDefault(r => r.Id == uom_id);
            if (uom != null)
            {
                _context.Uoms.Remove(uom);
                _context.SaveChanges();
            }
        }
        [HttpPost("UomAdd")]
        public IActionResult UomAdd([FromBody]Uom uom)
        {
            if (uom == null)
            {
                return BadRequest();
            }

            _context.Uoms.Add(uom);
            _context.SaveChanges();
            return Ok(uom);
        }



    }
}