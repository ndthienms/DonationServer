namespace DonationAppDemo.Models
{
    public class CommentPost
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? AccountId { get; set; }
        public int? PostId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Post? Post { get; set; }
        public ICollection<ImageCommentPost>? ImageCommentPosts { get; set; }
    }
}
