using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IDonorDal
    {
        Task<Donor?> GetByPhoneNum(string phoneNum);
        Task<Donor> Add(DonorDto donorDto);
    }
}
