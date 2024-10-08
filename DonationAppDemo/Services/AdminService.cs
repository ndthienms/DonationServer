using DonationAppDemo.DAL;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminDal _adminDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(IAdminDal adminDal,
            IUtilitiesService utilitiesService,
            IHttpContextAccessor httpContextAccessor)
        {
            _adminDal = adminDal;
            _utilitiesService = utilitiesService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Admin>> GetAll()
        {
            var admins = await _adminDal.GetAll();

            return admins;
        }
        public async Task<Admin?> GetById(int adminId)
        {
            var admins = await _adminDal.GetById(adminId);

            return admins;
        }
        public async Task<Admin> Update(int adminId, AdminDto adminDto)
        {
            var admins = await _adminDal.Update(adminId, adminDto);

            return admins;
        }
    }
}
