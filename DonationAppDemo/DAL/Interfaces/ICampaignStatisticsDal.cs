﻿using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface ICampaignStatisticsDal
    {
        Task<CampaignStatistics> Add(int campaignId, decimal total, string type);
        Task<CampaignStatistics> Update(int campaignId, decimal total, string type);
    }
}
