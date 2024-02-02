using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Likes :UniqueIdentifier
    {
        public int LikesCount { get; private set; }
        public int likes { get; set; }
        public int Dislikes { get; set; }

        public int PostId { get; set; }
        public int UserId { get; set; }

        public Likes()
        {
            CalculateLikesCount();
        }

        public void CalculateLikesCount()
        {
            LikesCount = likes + Dislikes;
        }
        //Relationship
        /*[ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        */
    }
}
