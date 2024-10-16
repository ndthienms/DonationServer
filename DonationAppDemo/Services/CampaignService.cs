using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using DonationAppDemo.Services.Interfaces;

namespace DonationAppDemo.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignDal _campaignDal;
        private readonly IRateCampaignDal _rateCampaignDal;
        private readonly IImageCampaignDal _imageCampaignDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly ITransactionDal _transactionDal;

        public CampaignService(ICampaignDal campaignDal, IRateCampaignDal rateCampaignDal, IImageCampaignDal imageCampaignDal, IUtilitiesService utilitiesService, ITransactionDal transactionDal)
        {
            _campaignDal = campaignDal;
            _rateCampaignDal = rateCampaignDal;
            _imageCampaignDal = imageCampaignDal;
            _utilitiesService = utilitiesService;
            _transactionDal = transactionDal;
        }

        public async Task<Campaign> CreateCampaign(CampaignDto campaignDto)
        {
            string coverSrc = "";
            string coverSrcPublicId = "";
            if (campaignDto.CoverImage != null)
            {
                try
                {
                    // Add Campaign cover image to cloudinary
                    var uploadImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(campaignDto.CoverImage);
                    if (uploadImageResult.Error != null) throw new Exception("Cannot upload cover image on cloudinary");
                    coverSrc = uploadImageResult.SecureUrl.AbsoluteUri;
                    coverSrcPublicId = uploadImageResult.PublicId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while updating on database" + ex.Message);
                }
            }
            //Create Campaign and add to DTB through CampaignDal.Add
            var campaign = new Campaign()
            {
                Id = campaignDto.Id,
                Title = campaignDto.Title,
                Target = campaignDto.Target,
                Description = campaignDto.Description,
                Address = campaignDto.Address,
                TargetAmount = campaignDto.TargetAmount,
                Organiser = campaignDto.Organiser,
                CoverSrc = coverSrc,
                CoverSrcPublicId = coverSrcPublicId,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                Disabled = true,
                UpdatedDate = null,
                UpdatedBy = null,
            };
            await _campaignDal.Add(campaign);
            return campaign;
        }

        public async Task<bool> DeleteCampaign(CampaignDto campaignDto)
        {
            var result = await _transactionDal.CampaignRateImage(campaignDto);
            if (!result)
            {
                throw new Exception("Failed to do delete campaign");
            }
            return result;
        }

        public async Task<RateCampaign> RateCampaign(RateCampaignDto rateCampaignDto)
        {
            var result = await _rateCampaignDal.CheckExistedRateCampaign(rateCampaignDto);
            if (result)
            {
                throw new Exception("The user has rated this campaign.");
            }
            RateCampaign rateCampaign = new RateCampaign()
            {
                CampaignId = rateCampaignDto.CampaignId,
                Comment = rateCampaignDto.Content,
                Rate = rateCampaignDto.Rate,
                RatedDate = DateTime.Now,
                DonorId = rateCampaignDto.DonorId,
            };
            await _rateCampaignDal.Add(rateCampaign);
            return rateCampaign;
        }

        public async Task<List<ImageCampaign>> AddListImageCampaign(List<ImageCampaignDto> listImageCampaignDto)
        {
            List<ImageCampaign> imageCampaigns = new List<ImageCampaign>();
            if (listImageCampaignDto == null || listImageCampaignDto.Any()) throw new Exception("No images provided");
            foreach (var imageDto in listImageCampaignDto)
            {
                try
                {
                    var uploadImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(imageDto.ImageCampaign);
                    if (uploadImageResult.Error != null)
                        throw new Exception("Failed to upload image to Cloudinary");

                    // Create a new CampaignImage object and add it to the list
                    var imageCampaign = new ImageCampaign
                    {
                        CampaignId = imageDto.CampaignId,
                        ImageSrc = uploadImageResult.SecureUrl.AbsoluteUri,
                        ImageSrcPublicId = uploadImageResult.PublicId,
                        StatusCampaignId = imageDto.StatusCampaignId
                    };
                    imageCampaigns.Add(imageCampaign);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error uploading image: {ex.Message}");
                }
            }
            return imageCampaigns;
        }
    }
}