using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Post:UniqueIdentifier
    {
        [Key]
        public int postId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime PostCreation { get; set; }


        // Other properties as needed

        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
