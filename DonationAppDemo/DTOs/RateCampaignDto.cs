using DonationAppDemo.Models;

namespace DonationAppDemo.DTOs
{
    public class RateCampaignDto
    {
        public int CampaignId { get; set; }
        public int DonorId { get; set; }
        public int? Rate { get; set; }
        public string? Content { get; set; }
        public DateTime? RatedDate { get; set; }
    }
}
