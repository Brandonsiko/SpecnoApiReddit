using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Comment:UniqueIdentifier
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }
        //Relationship

    }
}
