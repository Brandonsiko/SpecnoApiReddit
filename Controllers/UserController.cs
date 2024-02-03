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


        //Getting all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecnoUser>>> GetUsers()
        {
            if (_context.SpecnoUsers == null)
            {
                return BadRequest("Invalid user data");
            }
            return await _context.SpecnoUsers.ToListAsync();
        }

        
        //Getting a user's details
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
                                         .Select(c => new Comment { CommentId = c.CommentId, CommentText = c.CommentText, PostId = c.PostId, UserId = c.UserId })
                                         .ToList(),
                Likes = _context.Likes.Where(l => l.UserId == id)
                                  .Select(l => new Likes { PostId = l.PostId, likes = l.likes, Dislikes = l.Dislikes })
                                  .ToList()
            };

            return Ok(userDto);
        }


        //Getting all posts a user has created
        [HttpGet("user/posts/{userid}")]
        public async Task<ActionResult<UserDetails>> GetUserPosts(int userid)
        {
            var user = _context.SpecnoUsers.Find(userid);
            var likes = _context.Likes.Find(userid);

            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            var UserDto =new UserDetails { 

                UserId = user.UserId,
                Username = user.Username,
                Posts = _context.Posts.Where(p => p.UserId == userid)
                                   .Select(p => new Post { PostId = p.PostId, Title = p.Title, Message = p.Message })
                                   .ToList(),
            };

            return Ok(UserDto);
        }


        //Getting posts that a user liked
        [HttpGet("user/posts/userliked/{userid}")]
        public async Task<ActionResult<UserDetails>> GetLikedPosts(int userid)
        {
            var User = _context.SpecnoUsers.Find(userid);

            if (User == null)
            {
                return BadRequest("no such user present");
            }
            var t = _context.Likes.Where(l => l.UserId == userid)
                                  .Select(l => new Likes { PostId = l.PostId, likes = l.likes, Dislikes = l.Dislikes })
                                  .ToList();



            var userdto = new UserDetails
            {
                UserId = User.UserId,
                Username = User.Username,
                Likes = _context.Likes.Where(l => l.UserId == userid)
                                  .Select(l => new Likes { PostId = l.PostId, likes = l.likes, Dislikes = l.Dislikes })
                                  .ToList()
            };

            return Ok(userdto);
        }


        //Getting posts created by a user
        [HttpGet("user/posts/userliked/{username}")]
        public async Task<ActionResult<UserDetails>> GetUsernamePosts(string usersname)
        {
            var User = _context.SpecnoUsers.Find(usersname);

            if (User == null)
            {
                return BadRequest("no such user present");
            }

            var userdto = new UserDetails
            {
                UserId = User.UserId,
                Username = User.Username,
                Posts = _context.Posts.Where(p => p.UserId == User.UserId)
                    .Select(p => new Post { PostId = p.PostId, Title = p.Title, Message = p.Message })
                    .ToList(),
            };

            return Ok(userdto);
        }


        //Creates new users
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

        //Deleting a User
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

        //Updating a user details
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
