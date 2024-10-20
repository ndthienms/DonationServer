﻿using DonationAppDemo.Models;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public interface IImageCampaignDal
    {
        Task<List<ImageCampaign>> AddImages(List<ImageCampaign> imageCamapaigns);
        Task<bool> Remove(int imageId);
        Task<bool> RemoveByCampaignId(int campaignId);
        Task<List<ImageCampaign>> GetById(int campaignId, int pageIndex);
        Task<List<ImageCampaign>> GetAllById(int campaignId);
        Task<bool> RemoveListImages(List<ImageCampaignDto> imageCampaignDtos);
    }
}
