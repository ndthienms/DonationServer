using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IOrganiserService
    {
        Task<List<OrganiserShortDto>> GetAll(int pageIndex);
        Task<List<OrganiserShortDto>> GetSearchedList(int pageIndex, string text);
        Task<List<Organiser>> GetAllUnCensored(int pageIndex);
        Task<Organiser?> GetById(int organiserId);
        Task<Organiser> Update(int organiserId, OrganiserDto organiserDto);
        Task<Organiser> UpdateAva(int organiserId, IFormFile avaFile);
        Task<Organiser> UpdateCertification(int organiserId, IFormFile certificationFile);
        Task<bool> BecomeOrganiser(SignUpOrganiserDto signUpOrganiserDto);
        Task<Organiser> UpdateApprovement(int organiserId); // admin
    }
}
