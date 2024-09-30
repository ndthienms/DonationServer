namespace DonationAppDemo.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public DateTime? DonationDate { get; set; }
        public decimal? Amount { get; set; }
        public int? DonorId { get; set; }
        public int? CampaignId { get; set; }
        public virtual Donor? Donor { get; set; }
        public virtual Campaign? Campaign { get; set; }
    }
}
