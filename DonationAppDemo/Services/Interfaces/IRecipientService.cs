using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IRecipientService
    {
        Task<List<UserDto>> GetAll(int pageIndex);
        Task<List<UserDto>> GetSearchedList(int pageIndex, string text);
    }
}
