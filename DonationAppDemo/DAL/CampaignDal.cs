using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class CampaignDal : ICampaignDal
    {
        private readonly DonationDbContext _context;
        public CampaignDal(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<Campaign> Add(Campaign campaign)
        {
            _context.Campaign.Add(campaign);
            await _context.SaveChangesAsync();
            return campaign;
        }
        public async Task<Campaign?> Get(int campaignId)
        {
            var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            return campaign;
        }
        public async Task<Campaign> Update(CampaignDto campaignDto)
        {
            var campaign = await _context.Campaign.Where(x => x.Id == campaignDto.Id).FirstOrDefaultAsync();
            if (campaign == null)
            {
                throw new KeyNotFoundException("Campaign not found");
            }
            campaign.Title = campaignDto.Title;
            campaign.Target = campaignDto.Target;
            campaign.Description = campaignDto.Description;
            campaign.Address = campaignDto.Address;
            campaign.TargetAmount = campaignDto.TargetAmount;
            campaign.UpdatedDate = DateTime.Now;
            _context.Campaign.Update(campaign);
            await _context.SaveChangesAsync();
            return campaign;
        }

        public async Task<bool> Remove(int campaignId)
        {
            var hasDonations = await (
                from cp in _context.Campaign
                join dt in _context.Donation
                on cp.Id equals dt.CampaignId
                where cp.Id == campaignId
                select dt.Id
                ).AnyAsync();
            if (hasDonations) return false;
            /*var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            if (campaign == null)
            {
                return false;
            }
            var hasDonations = await _context.Donation.AnyAsync(d => d.CampaignId == campaignId);

            if (hasDonations)
            {
                //Check if Campaign existed in the Donation table
                return false;
            }*/
            var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            if (campaign == null)
            {
                return false;
            }
            _context.Campaign.Remove(campaign);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeStatus(int campaignId, int statusId)
        {
            var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            if (campaign == null)
            {
                return false;
            }
            //set status campaign Id to value of "Finished"
            campaign.StatusCampaign.Id = statusId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}