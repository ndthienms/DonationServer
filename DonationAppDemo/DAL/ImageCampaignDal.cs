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

        public async Task<ImageCampaign> Add(ImageCampaignDto imageCampaign)
        {
            var newImage = new ImageCampaign()
            {
                Id = imageCampaign.Id,
                ImageSrc = imageCampaign.ImageSrc,
                ImageSrcPublicId = imageCampaign.ImageSrcPublicId,
                CampaignId = imageCampaign.CampaignId,
                StatusCampaignId = imageCampaign.StatusCampaignId
            };
            _context.ImageCampaign.Add(newImage);
            await _context.SaveChangesAsync();
            return newImage;
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
    }
}