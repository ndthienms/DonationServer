using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface ICampaignDal
    {
        Task<Campaign?> Get(int campaignId);
        Task<Campaign> Add(Campaign campaign);
        Task<Campaign> Update(CampaignDto campaignDto);
        Task<bool> Remove(int campaignId);
        Task<bool> ChangeStatus(int campaignId, int statusId);
    }
}
