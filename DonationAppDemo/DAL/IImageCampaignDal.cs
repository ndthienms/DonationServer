using DonationAppDemo.Models;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public interface IImageCampaignDal
    {
        Task<ImageCampaign> Add(ImageCampaignDto imageCampaignDto);
        Task<bool> Remove(int imageId);
        Task<List<ImageCampaign>> GetById(int campaignId, int pageSize, int pageIndex);
    }
}
