using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecnoApiReddit.Data;
using SpecnoApiReddit.Models;

namespace SpecnoApiReddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecnoUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpecnoUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SpecnoUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecnoUser>>> GetSpecnoUsers()
        {
          if (_context.SpecnoUsers == null)
          {
              return NotFound();
          }
            return await _context.SpecnoUsers.ToListAsync();
        }

        // GET: api/SpecnoUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecnoUser>> GetSpecnoUser(int id)
        {
          if (_context.SpecnoUsers == null)
          {
              return NotFound();
          }
            var specnoUser = await _context.SpecnoUsers.FindAsync(id);
            var specnoPostDetails = await _context.SpecnoUsers
                    .Include(u => u.Posts)
                    .ThenInclude(p => p.Likes)
                    .Include(u => u.Posts)
                    .ThenInclude(p => p.Comments)
                    .FirstOrDefaultAsync(u => u.UserId == id);

            if (specnoUser == null)
            {
                return NotFound();
            }

            return specnoPostDetails;
        }

        // PUT: api/SpecnoUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecnoUser(int id, SpecnoUser specnoUser)
        {
            if (id != specnoUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(specnoUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecnoUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SpecnoUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpecnoUser>> PostSpecnoUser(SpecnoUser specnoUser)
        {
          if (_context.SpecnoUsers == null)
          {
              return Problem("Entity set 'ApplicationDbContext.SpecnoUsers'  is null.");
          }
            _context.SpecnoUsers.Add(specnoUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecnoUser", new { id = specnoUser.UserId }, specnoUser);
        }

        // DELETE: api/SpecnoUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecnoUser(int id)
        {
            if (_context.SpecnoUsers == null)
            {
                return NotFound();
            }
            var specnoUser = await _context.SpecnoUsers.FindAsync(id);
            if (specnoUser == null)
            {
                return NotFound();
            }

            _context.SpecnoUsers.Remove(specnoUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecnoUserExists(int id)
        {
            return (_context.SpecnoUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
