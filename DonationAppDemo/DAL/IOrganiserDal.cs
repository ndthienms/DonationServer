using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IOrganiserDal
    {
        Task<Organiser?> GetByPhoneNum(string phoneNum);
        Task<Organiser> Add(OrganiserDto organiserDto, string? certificationPublicId);
    }
}
