namespace DonationAppDemo.Services
{
    public interface IAccountService
    {
        Task<bool> Delete(string phoneNum);
        Task<bool> UpdateDisabledAccount(string phoneNum, bool disabled); //admin
        Task<bool> UpdateDisabledPersonalAccount(bool disabled); // self-user
    }
}
