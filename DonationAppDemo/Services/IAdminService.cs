using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAll(); // admin
        Task<Admin?> GetById(int adminId); // admin
        Task<Admin> Update(int adminId, AdminDto adminDto); // admin
    }
}
