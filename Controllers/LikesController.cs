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

        [HttpPost("posts/{postId}/likes")]
        public async Task<ActionResult<Likes>> AddLike(Likes likes,int postId)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            //Incrementing likes

            likes.PostId = postId;
            likes.UserId = post.UserId;
            likes.likes++;

            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("posts/{postId}/Dislike")]
        public async Task<ActionResult<Likes>> DisLike(Likes likes,int postId)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            var post = await _context.Posts.FindAsync(postId);
            if (post == null) 
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }


            //Decrementing likes

            likes.PostId = postId;
            likes.UserId= post.UserId;

            likes.Dislikes++;  
            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return NoContent();
        }       

        private bool LikesExists(int id)
        {
            return (_context.Likes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
