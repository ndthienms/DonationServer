namespace DonationAppDemo.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Target { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Address { get; set; }
        //public int? District { get; set; } //optional
        //public int? City { get; set; } //optional
        public int? StatusCampaignId { get; set; }
        public decimal? TargetAmount { get; set; }
        public string? CoverSrc { get; set; }
        public string? CoverSrcPublicId { get; set; }
        public int? OrganiserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? Disabled { get; set; }
        public virtual StatusCampaign? StatusCampaign { get; set; }
        public virtual Organiser? Organiser { get; set; }
        public ICollection<ImageCampaign>? ImageCampaigns { get; set; }
        public ICollection<RateCampaign>? RateCampaigns { get; set;}
        public ICollection<Donation>? Donations { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
    }
}
