using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services
{
    public interface IOrganiserService
    {
        Task<Organiser?> GetById(int organiserId);
        Task<Organiser> Update(int organiserId, OrganiserDto organiserDto);
        Task<Organiser> UpdateAva(int organiserId, IFormFile avaFile);
        Task<Organiser> UpdateCertification(int organiserId, IFormFile certificationFile);
    }
}
