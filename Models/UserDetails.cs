namespace SpecnoApiReddit.Models
{
    public class UserDetails:UniqueIdentifier
    {
        public int UserId { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Likes> Likes { get; set; }
    }
}
