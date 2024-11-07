using System.ComponentModel.DataAnnotations.Schema;

namespace DonationAppDemo.Models
{
    public class Campaign
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? NormalizedTitle { get; set; }
        public string? Target { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Address { get; set; }
        //public int? District { get; set; } //optional
        public string? City { get; set; }
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
        [NotMapped]
        public virtual StatusCampaign? StatusCampaign { get; set; }
        [NotMapped]
        public virtual Organiser? Organiser { get; set; }
        [NotMapped]
        public ICollection<ImageCampaign>? ImageCampaigns { get; set; }
        [NotMapped]
        public ICollection<RateCampaign>? RateCampaigns { get; set;}
        [NotMapped]
        public ICollection<Donation>? Donations { get; set; }
        [NotMapped]
        public ICollection<Expense>? Expenses { get; set; }
        [NotMapped]
        public ICollection<Transference>? Transferences { get; set; }
        [NotMapped]
        public ICollection<CampaignParticipant>? CampaignParticipants { get; set; }
    }
}
