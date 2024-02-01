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
    public class LikesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Likes>>> GetLikes()
        {
          if (_context.Likes == null)
          {
              return NotFound();
          }
            return await _context.Likes.ToListAsync();
        }

       

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("posts/{postId}/likes")]
        public async Task<ActionResult<Likes>> AddLike(Likes likes)
        {
          if (_context.Likes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
          }
            int currentlike = likes.likes;
            likes.likes=currentlike + 1;
            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("posts/{postId}/Dislike")]
        public async Task<ActionResult<Likes>> DisLike(Likes likes)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }
            int currentlike = likes.likes;
            likes.likes = currentlike - 1;
            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Likes/5
       

        private bool LikesExists(int id)
        {
            return (_context.Likes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
