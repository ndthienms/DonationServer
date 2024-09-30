namespace DonationAppDemo.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? PostDate { get; set; }
        public bool? disabled { get; set; }
        public int? AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public ICollection<ImagePost>? ImagePosts { get; set;}
        public ICollection<CommentPost>? CommentPosts { get; set; }
    }
}
