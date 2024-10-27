﻿using CloudinaryDotNet;
using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class CampaignStatisticsDal : ICampaignStatisticsDal
    {
        private readonly DonationDbContext _context;

        public CampaignStatisticsDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<CampaignStatistics> Add(int campaignId, decimal total, string type)
        {
            var campaignStatistics = new CampaignStatistics();
            if (type == "donation")
            {
                campaignStatistics = new CampaignStatistics()
                {
                    CampaignId = campaignId,
                    TotalDonationAmount = total,
                    TotalExpendedAmount = 0,
                    TotalTransferredAmount = 0,
                };
            }
            else if (type == "expense")
            {
                campaignStatistics = new CampaignStatistics()
                {
                    CampaignId = campaignId,
                    TotalDonationAmount = 0,
                    TotalExpendedAmount = total,
                    TotalTransferredAmount = 0,
                };
            }
            else
            {
                campaignStatistics = new CampaignStatistics()
                {
                    CampaignId = campaignId,
                    TotalDonationAmount = 0,
                    TotalExpendedAmount = 0,
                    TotalTransferredAmount = total,
                };
            }
            
            _context.CampaignStatistics.Add(campaignStatistics);
            await _context.SaveChangesAsync();
            return campaignStatistics;
        }

        public async Task<CampaignStatistics> Update(int campaignId, decimal total, string type)
        {
            var campaignStatistics = await _context.CampaignStatistics.Where(x => x.CampaignId == campaignId).FirstOrDefaultAsync();
            if (campaignStatistics == null)
            {
                campaignStatistics = await Add(campaignId, total, type);
                return campaignStatistics;
            }
            if (type == "donation")
            {
                campaignStatistics.TotalDonationAmount += total;
            }
            else if (type == "expense")
            {
                campaignStatistics.TotalExpendedAmount += total;
            }
            else
            {
                campaignStatistics.TotalTransferredAmount += total;
            }

            _context.CampaignStatistics.Update(campaignStatistics);
            await _context.SaveChangesAsync();
            return campaignStatistics;
        }
    }
}