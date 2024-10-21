using DonationAppDemo.DAL.Interfaces;
using DonationAppDemo.DTOs;
using DonationAppDemo.Models;

namespace DonationAppDemo.DAL
{
    public class DonationDal : IDonationDal
    {
        private readonly DonationDbContext _context;

        public DonationDal(DonationDbContext context)
        {
            _context = context;
        }

        public async Task<Donation> Add(PaymentResponseDto responseDto)
        {
            var donation = new Donation()
            {
                DonationDate = responseDto.PaymentDate,
                Amount = responseDto.Amount,
                DonorId = responseDto.UserId,
                CampaignId = responseDto.CampaignId,
                PaymentMethodId = responseDto.PaymentMethodId,
                PaymentDescription = responseDto.PaymentDescription,
                PaymentOrderId = responseDto.PaymentOrderId,
                PaymentTransactionId = responseDto.PaymentTransactionId,
                PaymentToken = responseDto.PaymentToken,
                PaymentResponse = "sucess",
            };
            _context.Donation.Add(donation);
            await _context.SaveChangesAsync();
            return donation;
        }
    }
}
