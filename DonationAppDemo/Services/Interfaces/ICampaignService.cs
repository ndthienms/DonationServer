using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<Campaign> CreateCampaign(CampaignDto campaignDto);
        Task<bool> DeleteCampaign(CampaignDto campaignDto);
        Task<RateCampaign> RateCampaign(RateCampaignDto rateCampaignDto);
        Task<List<ImageCampaign>> AddListImageCampaign(List<ImageCampaignDto> listImageCampaignDto);

    }
}
