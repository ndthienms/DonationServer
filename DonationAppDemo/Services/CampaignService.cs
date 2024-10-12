using DonationAppDemo.DAL;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignDal _campaignDal;
        private readonly IRateCampaignDal _rateDal;
        private readonly IImageCampaignDal _imageDal;

        public CampaignService(ICampaignDal campaignDal, IRateCampaignDal rateCampaignDal, IImageCampaignDal imageCampaignDal)
        {
            _campaignDal = campaignDal;
            _rateDal = rateCampaignDal;
            _imageDal = imageCampaignDal;
        }

        public Task<CampaignDal> CreateCampaign(CampaignDto campaignDto)
        {

        }
    }