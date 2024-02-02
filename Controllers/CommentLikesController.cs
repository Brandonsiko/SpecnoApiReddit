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
        /*[HttpGet("{id}")]
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
        }*/



        [HttpPost("comment/{commentid}/CommentLikes")]
        public async Task<ActionResult<CommentLikes>> PostCommentLikes(CommentLikes commentLikes,int commentid)
        {
          if (_context.CommentLikes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.CommentLikes'  is null.");
          }
            var comment = await _context.Comments.FindAsync(commentid);

            if (comment==null)
            {
                return BadRequest("There is no such comment please check id properly");
            }

           commentLikes.likes++;
            commentLikes.PostId= comment.PostId; 
            commentLikes.UserId= comment.UserId;
            commentLikes.CommentId= comment.CommentId;

           _context.CommentLikes.Add(commentLikes);
            await _context.SaveChangesAsync();

            return Ok(commentLikes) ;
        }




        [HttpPost("commment/{commentid}/CommentDislikes")]
        public async Task<ActionResult<CommentLikes>> DislikeCommentLikes(CommentLikes commentLikes, int commentid)
        {
            if (_context.CommentLikes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CommentLikes'  is null.");
            }
            var comment = await _context.Comments.FindAsync(commentid);

            if (comment == null)
            {
                return BadRequest("There is no such post please check id properly");
            }

            commentLikes.Dislikes++;

            commentLikes.PostId = comment.PostId;
            commentLikes.UserId = comment.UserId;
            commentLikes.CommentId = comment.CommentId;
            _context.CommentLikes.Add(commentLikes);
            await _context.SaveChangesAsync();

            return Ok(commentLikes);
        }


       // DELETE: api/CommentLikes/5
       /*[HttpDelete("{id}")]
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
       }*/

       
    }
}
