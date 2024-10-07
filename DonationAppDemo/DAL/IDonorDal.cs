using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IDonorDal
    {
        Task<Donor?> GetById(int id);
        Task<Donor?> GetByPhoneNum(string phoneNum);
        Task<Donor> Add(DonorDto donorDto);
        Task<Donor> Update(int donorId, DonorDto donorDto);
        Task<Donor> UpdateAva(int donorId, string avaSrc, string avaSrcPublicId);
    }
}
