using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using DonationAppDemo.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace DonationAppDemo.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorDal _donorDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DonorService(IDonorDal donorDal,
            IUtilitiesService utilitiesService,
            IHttpContextAccessor httpContextAccessor)
        {
            _donorDal = donorDal;
            _utilitiesService = utilitiesService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Donor>> GetAll()
        {
            var donors = await _donorDal.GetAll();

            return donors;
        }
        public async Task<Donor?> GetById(int donorId)
        {
            var donor = await _donorDal.GetById(donorId);
            
            return donor;
        }
        public async Task<Donor> Update(int donorId, DonorDto donorDto)
        {
            var donor = await _donorDal.Update(donorId, donorDto);

            return donor;
        }
        public async Task<Donor> UpdateAva(int donorId, IFormFile avaFile)
        {
            string imagePublicId = "";
            try
            {
                // Add avatar image to cloudinary
                var uploadImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(avaFile);
                if (uploadImageResult.Error != null)
                {
                    throw new Exception("Cannot upload avatar on cloudinary");
                }

                imagePublicId = uploadImageResult.PublicId;
                var donor = await _donorDal.UpdateAva(donorId, uploadImageResult.SecureUrl.AbsoluteUri, imagePublicId);

                return donor;
            }
            catch
            {
                await _utilitiesService.CloudinaryDeletePhotoAsync(imagePublicId);
                throw new Exception("Error while updating on database");
            }
        }
    }
}
