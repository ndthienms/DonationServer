using DonationAppDemo.DAL;
using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Services.Interfaces;

namespace DonationAppDemo.Services
{
    public class RecipientService : IRecipientService
    {
        private readonly IRecipientDal _recipientDal;
        private readonly ITransactionDal _transactionDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RecipientService(IRecipientDal recipientDal,
            ITransactionDal transactionDal,
            IUtilitiesService utilitiesService,
            IHttpContextAccessor httpContextAccessor)
        {
            _recipientDal = recipientDal;
            _transactionDal = transactionDal;
            _utilitiesService = utilitiesService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<UserDto>> GetAll(int pageIndex)
        {
            var donors = await _recipientDal.GetAll(pageIndex);

            return donors;
        }
        public async Task<List<UserDto>> GetSearchedList(int pageIndex, string text)
        {
            var donor = await _recipientDal.GetSearchedList(pageIndex, text);

            return donor;
        }
    }
}
