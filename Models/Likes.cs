namespace SpecnoApiReddit.Models
{
    public class Likes
    {
        public int id { get; set; }
        public int? likes { get; set; }
        public int Dislikes { get; set; }
        public int LikesCount { get; }
    }
}
