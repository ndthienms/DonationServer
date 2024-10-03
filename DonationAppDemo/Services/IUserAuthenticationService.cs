using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services
{
    public interface IUserAuthenticationService
    {
        Task<string> CheckExistedUser(string phoneNum);
        Task<OrganiserDto> SignUpOrganiser(SignUpOrganiserDto signUpOrganiserDto);
        Task<DonorDto> SignUpDonor(SignUpDonorDto signUpDonorDto);
        Task<string> SignIn(SignInDto signInDto);
    }
}
