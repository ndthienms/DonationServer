namespace DonationAppDemo.Models
{
    public class Account
    {
        public int Id {  get; set; }
        public string PhoneNum { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } //admin, donor, organiser
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set;}
        public bool? disabled { get; set; } //if account signed up organiser role -> disabled == true (waiting for acceptance)
        public ICollection<Admin>? Admins { get; set; }
        public ICollection<Organiser>? Organisers { get; set; }
        public ICollection<Donor>? Donors { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<CommentPost>? CommentPosts { get; set; }
    }
}
