using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;
using DonationAppDemo.Services.Interfaces;

namespace DonationAppDemo.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationDal _donationDal;
        private readonly ITransactionDal _transactionDal;
        private readonly IUtilitiesService _utilitiesService;
        private readonly IDonorService _donorService;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DonationService(IDonationDal donationDal,
            ITransactionDal transactionDal,
            IUtilitiesService utilitiesService,
            IDonorService donorService,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor)
        {
            _donationDal = donationDal;
            _transactionDal = transactionDal;
            _utilitiesService = utilitiesService;
            _donorService = donorService;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> CreatePaymentUrl(HttpContext context, PaymentRequestDto request)
        {
            if (request.PaymentMethod == "vnpay")
            {
                return await _utilitiesService.VnPayCreatePaymentUrl(context, request);
            }
            else if (request.PaymentMethod == "zalopay")
            {
                return await _utilitiesService.ZaloPayCreatePaymentUrl(request);
            }
            else
            {
                throw new Exception($"{request.PaymentMethod} gateway is not supported");
            }
        }
        public async Task<DonationDto> PaymentExecute(IQueryCollection collections)
        {
            // Determine payment gateway
            string paymentMethod = "";
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.Equals("appid"))
                {
                    if(value == _config.GetValue<string>("ZaloPaySettings:AppId"))
                    {
                        paymentMethod = "zalopay";
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    paymentMethod = "vnpay";
                    break;
                }
            }

            PaymentResponseDto resultPayment = new PaymentResponseDto();
            if (paymentMethod == "vnpay")
            {
                resultPayment = await _utilitiesService.VnPayPaymentExecute(collections);
            }
            else if (paymentMethod == "zalopay")
            {
                resultPayment = await _utilitiesService.ZaloPayPaymentExecute(collections);
            }
            else
            {
                throw new Exception($"{paymentMethod} gateway is not supported");
            }

            // Check payment result
            if (resultPayment.PaymentResponse == false)
            {
                throw new Exception("Payment failed");
            }

            // Add to donation entity and update campaign statistics entity
            var transactionResult = await _transactionDal.AddDonation(resultPayment);
            if (transactionResult == null)
            {
                throw new Exception("Failed to update database");
            }

            // Get information of donor
            Donor? donor = await _donorService.GetById(resultPayment.UserId);
            if (donor == null)
            {
                throw new Exception($"Not found donor id {resultPayment.UserId}");
            }

            DonationDto donation = new DonationDto()
            {
                CampaignId = resultPayment.CampaignId,
                CampaignName = null,
                DonorId = resultPayment.UserId,
                DonorName = donor.Name,
                DonorAvaSrc = donor.AvaSrc,
                DonationDate = resultPayment.PaymentDate,
                Amount = resultPayment.Amount,
                CampaignDonationTotal = transactionResult.TotalDonationAmount
            };

            return donation;
        }
    }
}
