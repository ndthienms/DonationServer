using DonationAppDemo.DAL;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DonationAppDemo.Services
{
    public class OrganiserService : IOrganiserService
    {
        private readonly IOrganiserDal _organiserDal;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUtilitiesService _utilitiesService;

        public OrganiserService(IOrganiserDal organiserDal,
            IHttpContextAccessor httpContextAccessor,
            IUtilitiesService utilitiesService)
        {
            _organiserDal = organiserDal;
            _httpContextAccessor = httpContextAccessor;
            _utilitiesService = utilitiesService;
        }
        public async Task<List<Organiser>> GetAll()
        {
            var organisers = await _organiserDal.GetAll();

            return organisers;
        }
        public async Task<Organiser?> GetById(int organiserId)
        {
            var organiser = await _organiserDal.GetById(organiserId);

            return organiser;
        }
        public async Task<Organiser> Update(int organiserId, OrganiserDto organiserDto)
        {
            var organiser = await _organiserDal.Update(organiserId, organiserDto);

            return organiser;
        }

        public async Task<Organiser> UpdateAva(int organiserId, IFormFile avaFile)
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
                var organiser = await _organiserDal.UpdateAva(organiserId, uploadImageResult.SecureUrl.AbsoluteUri, imagePublicId);

                return organiser;
            }
            catch
            {
                await _utilitiesService.CloudinaryDeletePhotoAsync(imagePublicId);
                throw new Exception("Error while updating on database");
            }
        }
        public async Task<Organiser> UpdateCertification(int organiserId, IFormFile certificationFile)
        {
            string imagePublicId = "";
            try
            {
                // Add avatar image to cloudinary
                var uploadImageResult = await _utilitiesService.CloudinaryUploadPhotoAsync(certificationFile);
                if (uploadImageResult.Error != null)
                {
                    throw new Exception("Cannot upload certification on cloudinary");
                }

                imagePublicId = uploadImageResult.PublicId;
                var organiser = await _organiserDal.UpdateCertification(organiserId, uploadImageResult.SecureUrl.AbsoluteUri, imagePublicId);

                return organiser;
            }
            catch
            {
                await _utilitiesService.CloudinaryDeletePhotoAsync(imagePublicId);
                throw new Exception("Error while updating on database");
            }
        }
    }
}
