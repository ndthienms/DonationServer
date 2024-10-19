using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IDonorService
    {
        Task<List<Donor>> GetAll(int pageIndex);
        Task<List<Donor>> GetSearchedList(int pageIndex, string text);
        Task<Donor?> GetById(int donorId);
        Task<Donor> Update(int donorId, DonorDto donorDto);
        Task<Donor> UpdateAva(int donorId, IFormFile avaFile);
        Task<bool> BecomeDonor(SignUpDonorDto signUpDonorDto);
    }
}
