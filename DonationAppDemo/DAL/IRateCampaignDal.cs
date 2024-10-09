﻿using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL

{
    public interface IRateCampaignDal
    {
        Task<RateCampaign> Add(RateCampaignDto rateCampaignDto);
        Task<RateCampaign> Update(RateCampaignDto rateCampaignDto);
        Task<bool> Remove(RateCampaignDto rateCampaignDto);
    }
}
