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

        [HttpPost("posts/{userid}/{postId}/addlike")]
        public async Task<ActionResult<Likes>> AddLike(Likes likes,int postId,int userid)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            var post = await _context.Posts.FindAsync(postId);
            var user = await _context.SpecnoUsers.FindAsync(userid);
            if (post == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            if (user == null)
            {
                return Problem("No valid user is invalid");
            }

            //Incrementing likes

            likes.PostId = post.PostId;
            likes.UserId = user.UserId;
            likes.likes++;

            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return Ok(likes);
        }

        [HttpPost("posts/{userid}/{postId}/addDislike")]
        public async Task<ActionResult<Likes>> DisLike(Likes likes,int postId, int userid)
        {
            if (_context.Likes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }
            var user = await _context.SpecnoUsers.FindAsync(userid);
            var post = await _context.Posts.FindAsync(postId);
            if (post == null) 
            {
                return Problem("Entity set 'ApplicationDbContext.Likes'  is null.");
            }

            if (user == null)
            {
                return Problem("No valid user");
            }

            //Decrementing likes

            likes.PostId = post.PostId;
            likes.UserId= user.UserId;

            likes.Dislikes++;  
            _context.Likes.Add(likes);
            await _context.SaveChangesAsync();

            return Ok(likes);
        }       

        private bool LikesExists(int id)
        {
            return (_context.Likes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
