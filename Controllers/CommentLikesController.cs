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
    public class CommentLikesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentLikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CommentLikes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentLikes>>> GetCommentLikes()
        {
          if (_context.CommentLikes == null)
          {
              return NotFound();
          }
            return await _context.CommentLikes.ToListAsync();
        }
        
        // GET: api/CommentLikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentLikes>> GetCommentLikes(int id)
        {
          if (_context.CommentLikes == null)
          {
              return NotFound();
          }
            var commentLikes = await _context.CommentLikes.FindAsync(id);

            if (commentLikes == null)
            {
                return NotFound();
            }

            return commentLikes;
        }







        [HttpPost("posts/{postId}/CommentLikes")]
        public async Task<ActionResult<CommentLikes>> PostCommentLikes(CommentLikes commentLikes,int postid)
        {
          if (_context.CommentLikes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CommentLikes'  is null.");
          }
            var post = _context.Posts.FindAsync(postid);

            if (post==null)
            {
                return BadRequest("There is no such post please check id properly");
            }

           commentLikes.likes++;
           _context.CommentLikes.Add(commentLikes);
            await _context.SaveChangesAsync();

            return NoContent() ;
        }




        [HttpPost("posts/{postId}/CommentDislikes")]
        public async Task<ActionResult<CommentLikes>> DislikeCommentLikes(CommentLikes commentLikes, int postid)
        {
            if (_context.CommentLikes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CommentLikes'  is null.");
            }
            var post = _context.Posts.FindAsync(postid);

            if (post == null)
            {
                return BadRequest("There is no such post please check id properly");
            }

            commentLikes.likes--;
            _context.CommentLikes.Add(commentLikes);
            await _context.SaveChangesAsync();

            return NoContent();
        }


       // DELETE: api/CommentLikes/5
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteCommentLikes(int id)
       {
           if (_context.CommentLikes == null)
           {
               return NotFound();
           }
           var commentLikes = await _context.CommentLikes.FindAsync(id);
           if (commentLikes == null)
           {
               return NotFound();
           }

           _context.CommentLikes.Remove(commentLikes);
           await _context.SaveChangesAsync();

           return NoContent();
       }

       private bool CommentLikesExists(int id)
       {
           return (_context.CommentLikes?.Any(e => e.Id == id)).GetValueOrDefault();
       }
    }
}
