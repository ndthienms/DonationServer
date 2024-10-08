using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.Services
{
    public interface IAccountService
    {
        Task<Account?> Get(string phoneNum);
        Task<bool> Delete(string phoneNum);
        Task<bool> DeletePersonalAccount();
        Task<bool> UpdateDisabledAccount(string phoneNum, bool disabled); // admin
        Task<bool> UpdateDisabledPersonalAccount(bool disabled); // self-user
        Task<OrganiserDto> AddOrganiserAccount(SignUpOrganiserDto signUpOrganiserDto); // admin
        Task<DonorDto> AddDonorAccount(SignUpDonorDto signUpDonorDto); // admin
    }
}
