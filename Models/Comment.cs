using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        //Relationship

        /*[ForeignKey("Postpost")]
        public int PostpostId { get; set; }
        
        public Post PostPost { get; set; }*/
    }
}
