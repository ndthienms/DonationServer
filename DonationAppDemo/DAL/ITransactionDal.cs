using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public interface ITransactionDal
    {
        Task<bool> AccountOrganiser(AccountDto accountDto, OrganiserDto organiserDto, string? certificationPublicId);
        Task<bool> AccountDonor(AccountDto accountDto, DonorDto donorDto);
    }
}
