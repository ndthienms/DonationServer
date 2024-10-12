using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface ICampaignDal
    {
        Task<Campaign> Add(CampaignDto campaignDto);
        Task<Campaign> Update(CampaignDto campaignDto);
        Task<bool> Remove(int campaignId);
        Task<bool> Finish(int campaignId);

    }
}
