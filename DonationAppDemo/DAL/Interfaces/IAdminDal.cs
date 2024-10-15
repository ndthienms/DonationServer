using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL.Interfaces
{
    public interface IAdminDal
    {
        Task<List<Admin>> GetAll();
        Task<Admin?> GetById(int id);
        Task<Admin?> GetByPhoneNum(string phoneNum);
        Task<Admin> Add(AdminDto adminDto);
        Task<Admin> Update(int adminId, AdminDto adminDto);
    }
}
