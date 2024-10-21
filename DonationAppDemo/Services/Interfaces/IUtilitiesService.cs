using CloudinaryDotNet.Actions;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services.Interfaces
{
    public interface IUtilitiesService
    {
        Task<string> TwilioSendCodeSms(string phoneNum);
        Task<bool?> TwilioVerifyCodeSms(string? code, string? phoneNum);
        Task<ImageUploadResult> CloudinaryUploadPhotoAsync(IFormFile photo);
        Task<DeletionResult> CloudinaryDeletePhotoAsync(string publicId);
        Task<string> VnPayCreatePaymentUrl(HttpContext context, PaymentRequestDto requestDto);
        Task<PaymentResponseDto> VnPayPaymentExecute(IQueryCollection collections);
        Task<string> ZaloPayCreatePaymentUrl(PaymentRequestDto requestDto);
        Task<PaymentResponseDto> ZaloPayPaymentExecute(IQueryCollection collections);
    }
}
