

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class SpecnoUser:UniqueIdentifier
    {
        [Key]
        public int UserId { get; set; }

        // Username for the user (unique)
        public string Username { get; set; }

        // Password for the user (hashed and salted in a real-world scenario)
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        


    }
}
