using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services
{
    public interface IDonorService
    {
        Task<Donor?> GetById(int donorId);
        Task<Donor> Update(int donorId, DonorDto donorDto);
        Task<Donor> UpdateAva(int donorId, IFormFile avaFile);
    }
}
