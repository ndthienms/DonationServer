using CloudinaryDotNet.Actions;
using DonationAppDemo.DAL;
using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Helper;
using DonationAppDemo.Models;
using DonationAppDemo.Services.Interfaces;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace DonationAppDemo.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignDal _campaignDal;
        private readonly IRecipientService _recipientService;
        private readonly ICampaignParticipantService _participantService;
        private readonly INotificationService _notificationService;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CampaignService(ICampaignDal campaignDal,
            IRecipientService recipientService,
            ICampaignParticipantService participantService,
            INotificationService notificationService,
            IUtilitiesService utilitiesService,
            IHttpContextAccessor httpContextAccessor)
        {
            _campaignDal = campaignDal;
            _recipientService = recipientService;
            _participantService = participantService;
            _notificationService = notificationService;
            _utilitiesService = utilitiesService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<CampaignShortADto>?> GetListByAdmin(int pageIndex)
        {
            var campaigns = await _campaignDal.GetListByAdmin(pageIndex);
            return campaigns;
        }
        public async Task<List<CampaignShortADto>?> GetSearchedListByAdmin(int pageIndex, CampaignSearchADto search)
        {
            // Convert type
            if(search.StartDate != "" || search.EndDate != "")
            {
                if (search.EndDate == "" || search.StartDate == "")
                {
                    throw new Exception("Start date and End date can not be null if one of them is not null");
                }
            }
            else
            {
                //search.StartDate = DateTime.MinValue.ToString();
                //search.EndDate = DateTime.Now.ToString();

                search.StartDate = "";
                search.EndDate = "";
            }

            string? normalized = StringExtension.NormalizeString(search.Campaign);
            search.Campaign = normalized == null ? "" : normalized;
            normalized = StringExtension.NormalizeString(search.User);
            search.User = normalized == null ? "" : normalized;

            // Do search
            var campaigns = await _campaignDal.GetSearchedListByAdmin(pageIndex, search);
            return campaigns;
        }
        public async Task<List<CampaignShortBDto>?> GetSearchedListByUser(int pageIndex, CampaignSearchADto search)
        {
            // Convert type
            if (search.StartDate != "" || search.EndDate != "")
            {
                if (search.EndDate == "" || search.StartDate == "")
                {
                    throw new Exception("Start date and End date can not be null if one of them is not null");
                }
            }
            else
            {
                //search.StartDate = DateTime.MinValue.ToString();
                //search.EndDate = DateTime.Now.ToString();

                search.StartDate = "";
                search.EndDate = "";
            }

            string? normalized = StringExtension.NormalizeString(search.Campaign);
            search.Campaign = normalized == null ? "" : normalized;
            normalized = StringExtension.NormalizeString(search.User);
            search.User = normalized == null ? "" : normalized;

            // Do search
            var campaigns = await _campaignDal.GetSearchedListByUser(pageIndex, search);
            return campaigns;
        }
        public async Task<List<CampaignShortCDto>?> GetSearchedListByOrganiser(int pageIndex, CampaignSearchADto search)
        {
            // Convert type
            if (search.StartDate != "" || search.EndDate != "")
            {
                if (search.EndDate == "" || search.StartDate == "")
                {
                    throw new Exception("Start date and End date can not be null if one of them is not null");
                }
            }
            else
            {
                //search.StartDate = DateTime.MinValue.ToString();
                //search.EndDate = DateTime.Now.ToString();

                search.StartDate = "";
                search.EndDate = "";
            }

            string? normalized = StringExtension.NormalizeString(search.Campaign);
            search.Campaign = normalized == null ? "" : normalized;
            normalized = StringExtension.NormalizeString(search.User);
            search.User = normalized == null ? "" : normalized;

            // Do search
            var campaigns = await _campaignDal.GetSearchedListByOrganiser(pageIndex, search);
            return campaigns;
        }
        public async Task<CampaignDetailBDto?> GetById(int campaignId)
        {
            var campaign = await _campaignDal.GetById(campaignId);
            return campaign;
        }
        public async Task<CampaignShortCDto> Add(CampaignCUDto campaignCUDto)
        {
            // Get current user
            var handler = new JwtSecurityTokenHandler();
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadJwtToken(authHeader) as JwtSecurityToken;
            var currentUserId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.ToString();

            // Add cover image to cloudinary
            ImageUploadResult? coverImageResult = null;
            if (campaignCUDto.CoverSrc != null)
            {
                coverImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(campaignCUDto.CoverSrc);
                if (coverImageResult.Error != null)
                {
                    throw new Exception("Cannot upload certidication image");
                }
            }

            // Add campaign to db
            var campaign = await _campaignDal.Add(campaignCUDto, coverImageResult == null ? null : coverImageResult.PublicId, coverImageResult == null ? null : coverImageResult.SecureUrl.AbsoluteUri, Int32.Parse(currentUserId));

            var recipient = await _recipientService.GetById((int)campaignCUDto.RecipientId);

            var campaignDto = new CampaignShortCDto
            {
                Id = campaign.Id,
                Title = campaign.Title,
                Target = campaign.Target,
                StartDate = campaign.StartDate == null ? "?" : campaign.StartDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = campaign.EndDate == null ? "?" : campaign.EndDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Address = campaign.Address + ", " + campaign.City,
                Status = campaign.StatusCampaignId == 1 ? "Đang chuẩn bị" : campaign.StatusCampaignId == 2 ? "Đang tiến hành" : campaign.StatusCampaignId == 3 ? "Đã kết thúc" : null,
                UserId = recipient.Id,
                UserName = recipient.Name,
                UserAva = recipient.AvaSrc,
                Received = campaign.Received == false ? "Chưa nhận" : "Đã nhận",
                CreatedDate = campaign.CreatedDate == null ? "" : ((DateTime)campaign.CreatedDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Disabled = campaign.Disabled == false ? "Active" : "Disabled"
            };
            return campaignDto;
        }
        public async Task<CampaignShortCDto> Update(int campaignId, CampaignCUDto campaignCUDto)
        {
            // Get current user
            var handler = new JwtSecurityTokenHandler();
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadJwtToken(authHeader) as JwtSecurityToken;
            var currentUserId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.ToString();

            // Add cover image to cloudinary
            ImageUploadResult? coverImageResult = null;
            if (campaignCUDto.CoverSrc != null)
            {
                coverImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(campaignCUDto.CoverSrc);
                if (coverImageResult.Error != null)
                {
                    throw new Exception("Cannot upload certidication image");
                }
            }

            // Update campaign to db
            var campaign = await _campaignDal.Update(campaignId, campaignCUDto, coverImageResult == null ? null : coverImageResult.PublicId, coverImageResult == null ? null : coverImageResult.SecureUrl.AbsoluteUri, Int32.Parse(currentUserId));

            var recipient = await _recipientService.GetById((int)campaignCUDto.RecipientId);

            var campaignDto = new CampaignShortCDto
            {
                Id = campaign.Id,
                Title = campaign.Title,
                Target = campaign.Target,
                StartDate = campaign.StartDate == null ? "?" : campaign.StartDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = campaign.EndDate == null ? "?" : campaign.EndDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Address = campaign.Address + ", " + campaign.City,
                Status = campaign.StatusCampaignId == 1 ? "Đang chuẩn bị" : campaign.StatusCampaignId == 2 ? "Đang tiến hành" : campaign.StatusCampaignId == 3 ? "Đã kết thúc" : null,
                UserId = recipient.Id,
                UserName = recipient.Name,
                UserAva = recipient.AvaSrc,
                Received = campaign.Received == false ? "Chưa nhận" : "Đã nhận",
                CreatedDate = campaign.CreatedDate == null ? "" : ((DateTime)campaign.CreatedDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Disabled = campaign.Disabled == false ? "Active" : "Disabled"
            };

            // Notification
            List<int>? userIds = await _participantService.GetAllDonorIdByCampaignId(campaignId);
            var notification = new Notification
            {
                NotificationTitle = $"Cập nhật chiến dịch {campaign.Title}",
                NotificationText = $"Cập nhật thông tin chiến dịch {campaign.Title}",
                NotificationDate = DateTime.Now,
                IsRead = false,
                Marked = false,
                FromUserId = Int32.Parse(currentUserId),
                FromUserRole = "organiser",
                ToUserId = null,
                ToUserRole = "donor"
            };

            var notiAddResult = await _notificationService.AddList(userIds, notification);
            var noti = await _notificationService.SendMultipleNotifications(userIds, "donor", notification.NotificationTitle, notification.NotificationText);

            return campaignDto;
        }
        public async Task<bool> UpdateDisabledCampaign(int campaignId, bool disabled)
        {
            var result = await _campaignDal.UpdateDisabledCampaign(campaignId, disabled);
            if (!result)
            {
                throw new Exception("Failed to update disabled campaign");
            }

            return result;
        }
        /*private readonly ICampaignDal _campaignDal;
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

        public async Task<Campaign?> Get(int campaignId)
        {
            var campaign = await _campaignDal.Get(campaignId);
            if (campaign == null)
            {
                throw new Exception("Cannot find the campaign");
            }
            return campaign;
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
                Disabled = false,
                UpdatedDate = DateTime.Now,
                UpdatedBy = null,
            };
            await _campaignDal.Add(campaign);
            return campaign;
        }

        public async Task<Campaign> UpdateCampaign(CampaignDto campaignDto)
        {
            var campaign = await _campaignDal.Update(campaignDto);
            if (campaign == null)
            {
                throw new Exception("Cannot find the campaign");
            }
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

        public async Task<bool> ChangeStatusCampaign(int campaignId, int statusId)
        {
            var result = await _campaignDal.ChangeStatus(campaignId, statusId);
            if (!result)
            {
                throw new Exception("Cannot change Campaign's Status");
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
        public async Task<RateCampaign> UpdateRateCampaign(RateCampaignDto rateCampaignDto)
        {
            var rate = await _rateCampaignDal.CheckExistedRateCampaign(rateCampaignDto);
            if (!rate)
            {
                throw new Exception("The user has not rated this campaign.");
            }
            var result = await _rateCampaignDal.Update(rateCampaignDto);
            if (result == null) throw new Exception("Failed to update rate campaign.");
            return result;
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
        public async Task<bool> RemoveListImageCampaign(List<ImageCampaignDto> listImageCampaignDto)
        {
            var result = await _imageCampaignDal.RemoveListImages(listImageCampaignDto);
            if(result == false)
            {
                throw new Exception("Can not find the Image");
            }
            return result;
        }*/
    }
}