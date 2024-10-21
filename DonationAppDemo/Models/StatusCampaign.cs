using System.ComponentModel.DataAnnotations.Schema;

namespace DonationAppDemo.Models
{
    public class StatusCampaign
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public ICollection<Campaign>? Campaigns { get; set; }
        [NotMapped]
        public ICollection<ImageCampaign>? ImageCampaigns { get; set; }
    }
}
