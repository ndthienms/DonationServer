using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public interface ITransactionDal
    {
        Task<bool> AccountOrganiser(AccountDto accountDto, OrganiserDto organiserDto, string? certificationPublicId);
        Task<bool> UpdateAccountOrganiserApprovement(int organiserId, int adminId, string phoneNum);
        Task<bool> AccountDonor(AccountDto accountDto, DonorDto donorDto);
        Task<bool> AccountAdmin(AccountDto accountDto, AdminDto adminDto);
    }
}
