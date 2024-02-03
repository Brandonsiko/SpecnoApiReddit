namespace SpecnoApiReddit.Models
{
    public class PostDetails
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime PostCreation { get; set; }

        public int UserId { get; set; }


        // Other properties as needed

        public ICollection<Likes> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
