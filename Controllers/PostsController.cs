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
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var postdto = new PostDetails
            {
                PostId = post.Id,
                Title = post.Title,
                Message = post.Message,
                UserId = post.UserId,
                PostCreation = post.PostCreation,
                Likes = _context.Likes.Where(p => p.UserId == post.UserId)
                .Select(p => new Likes { PostId = p.PostId, likes = p.likes, Dislikes = p.Dislikes, UserId = p.UserId })
                .ToList(),
                Comments = _context.Comments.Where(p => p.UserId == post.UserId)
                .Select(p => new Comment { PostId = p.PostId,  CommentId= p.CommentId, CommentText = p.CommentText, UserId = p.UserId })
                .ToList(),
            };

            return  Ok(postdto);
        }



        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            var existingPost = await _context.Posts.FindAsync(id);


            if (post.Title != "string")
            {
                existingPost.Title = post.Title;
            }
            if (post.Message != "string")
            {
                existingPost.Title = post.Title;
            }
            _context.Entry(existingPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        [HttpPost("{userId}")]
        public async Task<ActionResult<Post>> SendPost(Post post,int userId)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var user= await _context.SpecnoUsers.FindAsync(userId);
            
            
            if (user == null)
            {
                return BadRequest("No User logged in");
            }
           
            post.UserId= userId;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
