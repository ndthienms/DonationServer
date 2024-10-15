using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class RateCampaignDal: IRateCampaignDal
    {
        private readonly DonationDbContext _context;
        public RateCampaignDal(DonationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckExistedRateCampaign(RateCampaignDto rateCampaignDto)
        {
            //check if the donor rated campaign
            var existingRateCampaign =  await _context.RateCampaign.Where(x => x.CampaignId == rateCampaignDto.CampaignId && x.DonorId == rateCampaignDto.DonorId).FirstOrDefaultAsync();
            if (existingRateCampaign != null)
            {
                return true;
            }
            return false;
        }

        public async Task<RateCampaign> Add(RateCampaign rateCampaign)
        {
            _context.RateCampaign.Add(rateCampaign);
            await _context.SaveChangesAsync();
            return rateCampaign;
        }
        public async Task<RateCampaign> Update(RateCampaignDto rateCampaignDto)
        {
            //check if user has rated the campaign
            var existingRateCampaign = await _context.RateCampaign.Where(x => x.CampaignId == rateCampaignDto.CampaignId && x.DonorId == rateCampaignDto.DonorId).FirstOrDefaultAsync();
            if(existingRateCampaign != null)
            {
                existingRateCampaign.Rate = rateCampaignDto.Rate;
                existingRateCampaign.Content = rateCampaignDto.Content;
            }
            await _context.SaveChangesAsync();
            return existingRateCampaign;
        }

        public async Task<bool> RemoveByCampaignId(int campaignId)
        {
            List<RateCampaign> listRateCampaign = await _context.RateCampaign.Where(x => x.CampaignId == campaignId).ToListAsync();
            if (listRateCampaign == null)
            {
                return false;
            }
            foreach (var rate in listRateCampaign)
            {
                _context.RateCampaign.Remove(rate);
            }
            await _context.SaveChangesAsync();
            return true;
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
        public async Task<List<RateCampaign>> GetAllById(int campaignId)
        {
            var campaignExists = await _context.Campaign.AnyAsync(c => c.Id == campaignId);
            if (!campaignExists)
            {
                return new List<RateCampaign>();
            }
            // Fetch all image campaigns that match the given campaignId
            return await _context.RateCampaign
                                    .Where(x => x.CampaignId == campaignId)
                                    .ToListAsync();
        }
    }
}