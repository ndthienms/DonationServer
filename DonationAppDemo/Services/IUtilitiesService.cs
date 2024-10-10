using CloudinaryDotNet.Actions;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services
{
    public interface IUtilitiesService
    {
        Task<string> TwilioSendCodeSms(string phoneNum);
        Task<bool?> TwilioVerifyCodeSms(string? code, string? phoneNum);
        Task<ImageUploadResult> CloudinaryUploadPhotoAsync(IFormFile photo);
        Task<DeletionResult> CloudinaryDeletePhotoAsync(string publicId);
    }
}
