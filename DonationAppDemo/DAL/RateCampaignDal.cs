using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class RateCampaignDal
    {
        private readonly DonationDbContext _context;
        public async Task<bool> CheckExistedRateCampaign(RateCampaignDto rateCampaignDto)
        {
            //check if the donor rated campaign
            var existingRateCampaign = _context.RateCampaign.Where(x => x.CampaignId == rateCampaignDto.CampaignId && x.DonorId == rateCampaignDto.DonorId).FirstOrDefault();
            if (existingRateCampaign != null)
            {
                return true;
            }
            return false;
        }

        public async Task<RateCampaign> Add(RateCampaignDto rateCampaignDto)
        {
            var rateCampaign = new RateCampaign()
            {
                CampaignId = rateCampaignDto.CampaignId,
                DonorId = rateCampaignDto.DonorId,
                Rate = rateCampaignDto.Rate,
                Content = rateCampaignDto.Content,
                RatedDate = DateTime.Now,
            };
            _context.RateCampaign.Add(rateCampaign);
            await _context.SaveChangesAsync();
            return rateCampaign;
        }
        public async Task<RateCampaign> Update(RateCampaignDto rateCampaignDto)
        {
            //check if user has rated the campaign
            var existingRateCampaign = _context.RateCampaign.Where(x => x.CampaignId == rateCampaignDto.CampaignId && x.DonorId == rateCampaignDto.DonorId).FirstOrDefault();
            existingRateCampaign.Rate = rateCampaignDto.Rate;
            existingRateCampaign.Content = rateCampaignDto.Content;
            await _context.SaveChangesAsync();
            return existingRateCampaign;
        }

        public async Task<List<RateCampaign>> GetById(int campaignId, int pageSize, int pageIndex)
        {
            var campaignExists = await _context.Campaign.AnyAsync(c => c.Id == campaignId);

            if (!campaignExists)
            {
                return new List<RateCampaign>();
            }
            // Fetch all image campaigns that match the given campaignId
            return await _context.RateCampaign
                                    .Where(x => x.CampaignId == campaignId)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        }
    }
}