using System.ComponentModel.DataAnnotations.Schema;

namespace DonationAppDemo.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? ContentPost { get; set; }
        public DateTime? PostDate { get; set; }
        public bool? Disabled { get; set; }
        public int? UserId { get; set; }
        public string? UserRole { get; set; }
        [NotMapped]
        public virtual Admin? Admin { get; set; }
        [NotMapped]
        public ICollection<ImagePost>? ImagePosts { get; set;}
        [NotMapped]
        public ICollection<CommentPost>? CommentPosts { get; set; }
    }
}
