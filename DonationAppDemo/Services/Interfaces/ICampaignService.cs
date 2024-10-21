using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<Campaign> CreateCampaign(CampaignDto campaignDto);
        Task<Campaign?> Get(int campaignId);
        Task<bool> DeleteCampaign(CampaignDto campaignDto);
        Task<RateCampaign> RateCampaign(RateCampaignDto rateCampaignDto);
        Task<List<ImageCampaign>> AddListImageCampaign(List<ImageCampaignDto> listImageCampaignDto);
        Task<Campaign> UpdateCampaign(CampaignDto campaignDto);
        Task<bool> ChangeStatusCampaign(int campaignId, int statusId);
        Task<RateCampaign> UpdateRateCampaign(RateCampaignDto rateCampaignDto);
        Task<bool> RemoveListImageCampaign(List<ImageCampaignDto> listImageCampaignDto);
    }
}
