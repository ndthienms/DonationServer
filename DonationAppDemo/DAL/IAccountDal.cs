using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public interface IAccountDal
    {
        Task<Account?> Get(string phoneNum);
        Task<Account> Add(AccountDto accountDto);
        Task<bool> UpdateDisabledAccount(string phoneNum, bool disabled);
        Task<bool> Delete(string phoneNum);
    }
}
