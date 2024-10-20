using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.DAL
{
    public class TransactionDal : ITransactionDal
    {
        private readonly DonationDbContext _context;
        private readonly IAccountDal _accountDal;
        private readonly IAdminDal _adminDal;
        private readonly IDonorDal _donorDal;
        private readonly IOrganiserDal _organiserDal;

        private readonly ICampaignDal _campaignDal;
        private readonly IImageCampaignDal _imageCampaignDal;
        private readonly IRateCampaignDal _rateCampaignDal;


        public TransactionDal(DonationDbContext context,
            //Account
            IAccountDal accountDal,
            IAdminDal adminDal,
            IDonorDal donorDal,
            IOrganiserDal organiserDal,
            //Campaign
            ICampaignDal campaignDal,
            IImageCampaignDal imageCampaignDal,
            IRateCampaignDal rateCampaignDal)
        {
            _context = context;
            //Account
            _accountDal = accountDal;
            _adminDal = adminDal;
            _donorDal = donorDal;
            _organiserDal = organiserDal;
            //Campaign
            _campaignDal = campaignDal;
            _imageCampaignDal = imageCampaignDal;
            _rateCampaignDal = rateCampaignDal;
        }

        // Account + Organiser
        public async Task<bool> SignUpOrganiser(AccountDto accountDto, OrganiserDto organiserDto, string? certificationPublicId)
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
        public async Task<bool> BecomeOrganiser(string phoneNum, string role, bool disabled, OrganiserDto organiserDto, string? certificationPublicId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.UpdateRole(phoneNum, role, disabled);
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
        public async Task<bool> DeleteUncensoredOrganiser(string phoneNum, int organiserId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.DeleteUncensoredOrganiser(phoneNum);
                    await _organiserDal.Delete(organiserId);

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

        // Account + Donor
        public async Task<bool> SignUpDonor(AccountDto accountDto, DonorDto donorDto)
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
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> BecomeDonor(string phoneNum, string role, bool disabled, DonorDto donorDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.UpdateRole(phoneNum, role, disabled);
                    await _donorDal.Add(donorDto);

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

        // Account + Admin
        public async Task<bool> AccountAdmin(AccountDto accountDto, AdminDto adminDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _accountDal.Add(accountDto);
                    await _adminDal.Add(adminDto);

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

        //Delete Campaign
        public async Task<bool> CampaignRateImage(CampaignDto campaignDto)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _campaignDal.Remove(campaignDto.Id);
                    await _rateCampaignDal.RemoveByCampaignId(campaignDto.Id);
                    await _imageCampaignDal.RemoveByCampaignId(campaignDto.Id);
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
    }
}
