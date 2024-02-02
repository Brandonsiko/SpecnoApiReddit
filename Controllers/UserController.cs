using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SpecnoApiReddit.Data;
using SpecnoApiReddit.Models;

namespace SpecnoApiReddit.Controllers
{
    [Route("api/users")]
    [ApiController]
   public class UserController : ControllerBase
    { 
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecnoUser>>> GetUsers()
        {
            if (_context.SpecnoUsers == null)
            {
                return BadRequest("Invalid user data");
            }
            return await _context.SpecnoUsers.ToListAsync();
        }

       [HttpGet("{id}")]
       public async Task<ActionResult<UserDetails>> GetUsersDetails(int id)
       {
            var user = _context.SpecnoUsers.Find(id);
            if (_context.SpecnoUsers == null)
           {
               return BadRequest("Invalid user data");
           }
            var userDto = new UserDetails
            {
                UserId = user.UserId,
                Username = user.Username,
                Posts = _context.Posts.Where(p => p.UserId == id)
                                   .Select(p => new Post { PostId = p.PostId, Title = p.Title, Message = p.Message })
                                   .ToList(),
                Comments = _context.Comments.Where(c => c.UserId == id)
                                         .Select(c => new Comment { CommentId = c.CommentId, CommentText = c.CommentText })
                                         .ToList(),
                Likes = _context.Likes.Where(l => l.UserId == id)
                                  .Select(l => new Likes { PostId = l.PostId, likes = l.likes })
                                  .ToList()
            };

            return Ok(userDto);
       }



        [HttpPost]
       public async Task<ActionResult<SpecnoUser>> CreateUser(SpecnoUser specnoUser)
       {
           if (_context.SpecnoUsers == null)
           {
               return Problem("Entity set 'ApplicationDbContext.SpecnoUsers'  is null.");
           }
           if (specnoUser.Username == "string" || specnoUser.Password == "string")
           {
               return Problem("Username or password cannot be set to 'string'. Please enter proper values.");
           }

           
           _context.SpecnoUsers.Add(specnoUser);
           await _context.SaveChangesAsync();

           return CreatedAtAction(nameof(GetUsers), new { id = specnoUser.UserId }, specnoUser);
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult<SpecnoUser>> DeleteUser(int id)
       {
           if (_context.SpecnoUsers== null)
           {
               return BadRequest("User not found");
           }
           var user =await _context.SpecnoUsers.FindAsync(id);
           if (user == null)
           {
               return BadRequest("Not found");
           }
           _context.SpecnoUsers.Remove(user);
           await _context.SaveChangesAsync();
           return NoContent();
       }

       [HttpPut("{id}")]
       public async Task<ActionResult<SpecnoUser>> UpdateUser(int id, SpecnoUser specnoUser)
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
               if (!UserExists(id))
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

       private bool UserExists(int id)
       {
           return (_context.SpecnoUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
       }
    }
}
