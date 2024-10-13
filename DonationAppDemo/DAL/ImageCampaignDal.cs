using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAppDemo.DAL
{
    public class ImageCampaignDal : IImageCampaignDal
    {
        private readonly DonationDbContext _context;

        public ImageCampaignDal(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ImageCampaign>> AddImages(List<ImageCampaign> imageCamapaigns)
        {
            foreach(var imageCampaign in imageCamapaigns)
            {
                _context.Add(imageCampaign);
            }
            await _context.SaveChangesAsync();
            return imageCamapaigns;
        }

        public async Task<bool> Remove(int imageId)
        {
            var image = await _context.ImageCampaign.Where(x => x.Id == imageId).FirstOrDefaultAsync();
            if (image == null)
            {
                return false;
            }
            _context.ImageCampaign.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveByCampaignId(int campaignId)
        {
            List<ImageCampaign> listImageCampaign = await _context.ImageCampaign.Where(x => x.CampaignId == campaignId).ToListAsync();
            if (listImageCampaign == null)
            {
                return false;
            }
            foreach (var image in listImageCampaign)
            {
                _context.ImageCampaign.Remove(image);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ImageCampaign>> GetById(int campaignId, int pageSize, int pageIndex)
        {
            var campaignExists = await _context.Campaign.AnyAsync(c => c.Id == campaignId);

            if (!campaignExists)
            {
                return new List<ImageCampaign>();
            }
            // Fetch all image campaigns that match the given campaignId
            return await _context.ImageCampaign
                                    .Where(c => c.CampaignId == campaignId)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
        }
        public async Task<List<ImageCampaign>> GetAllById(int campaignId)
        {
            var campaignExists = await _context.Campaign.AnyAsync(c => c.Id == campaignId);

            if (!campaignExists)
            {
                return new List<ImageCampaign>();
            }
            // Fetch all image campaigns that match the given campaignId
            return await _context.ImageCampaign
                                    .Where(c => c.CampaignId == campaignId)
                                    .ToListAsync();
        }
    }
}