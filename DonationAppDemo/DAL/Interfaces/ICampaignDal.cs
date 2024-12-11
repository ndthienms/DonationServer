using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface ICampaignDal
    {
        Task<List<CampaignShortADto>?> GetListByAdmin(int pageIndex);
        Task<List<CampaignShortADto>?> GetSearchedListByAdmin(int pageIndex, CampaignSearchADto search);
        Task<List<CampaignShortBDto>?> GetSearchedListByUser(int pageIndex, CampaignSearchADto search);
        Task<CampaignDetailBDto?> GetById(int campaignId);
        Task<bool> UpdateDisabledCampaign(int campaignId, bool disabled);
        /*Task<Campaign?> Get(int campaignId);
        Task<Campaign> Add(Campaign campaign);
        Task<Campaign> Update(CampaignDto campaignDto);
        Task<bool> Remove(int campaignId);
        Task<bool> ChangeStatus(int campaignId, int statusId);*/
    }
}
