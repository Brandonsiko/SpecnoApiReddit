

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class SpecnoUser
    {
        [Key]
        public int User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Other properties as needed

        [ForeignKey("postId")]
        public int postId { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
