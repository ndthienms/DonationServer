namespace DonationAppDemo.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? NotificationTitle { get; set; }
        public string? NotificationText { get; set;}
        public DateTime? NotificationDate { get; set; }
        public bool? NotificationType { get; set;} // unread == 0 / read == 1
        public string? AccountId { get; set; }
        public virtual Account? Account { get; set; }
    }
}
