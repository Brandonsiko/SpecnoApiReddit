namespace SpecnoApiReddit.Models
{
    public class SpecnoUser
    {
        public int User_ID { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }

        public List<Post> MyPosts { get; set; }

        public SpecnoUser()
        {
            MyPosts = new List<Post>();
        }
    }
}
