using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecnoApiReddit.Models
{
    public class Likes
    {
        [Key]
        public int id { get; set; }
        public int? likes { get; set; }
        public int Dislikes { get; set; }
        public int LikesCount { get; }


        //Relationship
        /*[ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        */
    }
}
