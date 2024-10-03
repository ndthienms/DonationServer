using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public class TransactionDal : ITransactionDal
    {
        private readonly DonationDbContext _context;
        private readonly IAccountDal _accountDal;
        private readonly IDonorDal _donorDal;
        private readonly IOrganiserDal _organiserDal;

        public TransactionDal(DonationDbContext context,
            IAccountDal accountDal,
            IDonorDal donorDal,
            IOrganiserDal organiserDal)
        {
            _context = context;
            _accountDal = accountDal;
            _donorDal = donorDal;
            _organiserDal = organiserDal;
        }
        public async Task<bool> AccountOrganiser(AccountDto accountDto, OrganiserDto organiserDto, string? certificationPublicId)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.Add(accountDto);
                    await _organiserDal.Add(organiserDto, certificationPublicId);

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> AccountDonor(AccountDto accountDto, DonorDto donorDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.Add(accountDto);
                    await _donorDal.Add(donorDto);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
