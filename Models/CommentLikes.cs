namespace SpecnoApiReddit.Models
{
    public class CommentLikes:UniqueIdentifier
    {
        public int likes { get; set; }
        public int Dislikes { get; set; }
        public int LikesCount { get; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        
    }
}
