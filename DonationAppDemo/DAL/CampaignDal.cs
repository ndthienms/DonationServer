using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class CampaignDal : ICampaignDal
    {
        private readonly DonationDbContext _context;

        public async Task<Campaign> Add(CampaignDto campaignDto)
        {
            var campaign = new Campaign()
            {
                Id = campaignDto.Id,
                Title = campaignDto.Title,
                Target = campaignDto.Target,
                Description = campaignDto.Description,
                Address = campaignDto.Address,
                TargetAmount = campaignDto.TargetAmount,
                Organiser = campaignDto.Organiser,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                disabled = true,
                UpdatedDate = null,
                UpdatedBy = null,
            };
            _context.Campaign.Add(campaign);
            await _context.SaveChangesAsync();
            return campaign;
        }

        public async Task<Campaign> Update(CampaignDto campaignDto)
        {
            var existingCampaign = await _context.Campaign.Where(x => x.Id == campaignDto.Id).FirstOrDefaultAsync();
            if (existingCampaign == null)
            {
                throw new KeyNotFoundException("Campaign not found");
            }
            existingCampaign.Title = campaignDto.Title;
            existingCampaign.Target = campaignDto.Target;
            existingCampaign.Description = campaignDto.Description;
            existingCampaign.Address = campaignDto.Address;
            existingCampaign.TargetAmount = campaignDto.TargetAmount;
            existingCampaign.UpdatedDate = DateTime.Now;
            _context.SaveChangesAsync();
            return existingCampaign;
        }

        public async Task<bool> Remove(int campaignId)
        {
            var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            if (campaign == null)
            {
                return false;
            }
            var hasDonations = await _context.Donation.AnyAsync(d => d.CampaignId == campaignId);

            if (hasDonations)
            {
                //Check if Campaign existed in the Donation table
                return false;
            }
            _context.Campaign.Remove(campaign);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Finish(int campaignId)
        {
            var campaign = await _context.Campaign.Where(x => x.Id == campaignId).FirstOrDefaultAsync();
            if (campaign == null)
            {
                return false;
            }
            //set status campaign Id to value of "Finished"
            campaign.StatusCampaign.Id = 1;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}