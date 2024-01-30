namespace SpecnoApiReddit.Models
{
    public class Post
    {
        public int id { get; set; }
        public string? Title { get; set; }
        public DateTime PostCreation { get; set; }
        public List<string> Pictures { get; set; }

        public List<Likes> MyLikes { get; set; }

        public Post() { 

            MyLikes= new List<Likes>(); 
        }
    }
}
