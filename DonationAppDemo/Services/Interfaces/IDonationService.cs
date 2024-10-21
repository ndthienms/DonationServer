using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IDonationService
    {
        Task<string> CreatePaymentUrl(HttpContext context, PaymentRequestDto request);
        Task<DonationDto> PaymentExecute(IQueryCollection collections);
    }
}
