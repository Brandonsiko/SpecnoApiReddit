using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Likes :UniqueIdentifier
    {
        public int likes { get; set; }
        public int Dislikes { get; set; }
        public int LikesCount { get; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public Likes() { 
        LikesCount= likes + Dislikes;
        }
        //Relationship
        /*[ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        */
    }
}
