using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IAccountDal
    {
        Task<Account?> Get(string phoneNum);
        Task<Account> Add(AccountDto accountDto);
    }
}
