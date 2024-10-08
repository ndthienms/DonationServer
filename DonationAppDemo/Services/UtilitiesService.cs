using Twilio.Rest.Verify.V2.Service;
using Twilio;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Cryptography;
using DonationAppDemo.DTOs;

namespace DonationAppDemo.Services
{
    public class UtilitiesService : IUtilitiesService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;

        public UtilitiesService(IConfiguration config)
        {
            _config = config;
            Account account = new Account(
                _config.GetValue<string>("CloudinarySettings:CloudName"),
                _config.GetValue<string>("CloudinarySettings:ApiKey"),
                _config.GetValue<string>("CloudinarySettings:ApiSecret"));

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> TwilioSendCodeSms(string phoneNum)
        {
            string accountSid = _config.GetValue<string>("TwilioSettings:AccountSid");
            string authToken = _config.GetValue<string>("TwilioSettings:AuthToken");

            TwilioClient.Init(accountSid, authToken);

            var verification = await VerificationResource.CreateAsync(
                to: phoneNum,
                channel: "sms",
                pathServiceSid: _config.GetValue<string>("TwilioSettings:PathServiceSid"));

            return verification.Sid;
        }
        public async Task<bool?> TwilioVerifyCodeSms(string? code, string? phoneNum)
        {
            string accountSid = _config.GetValue<string>("TwilioSettings:AccountSid");
            string authToken = _config.GetValue<string>("TwilioSettings:AuthToken");

            TwilioClient.Init(accountSid, authToken);

            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: phoneNum, code: code, pathServiceSid: _config.GetValue<string>("TwilioSettings:PathServiceSid"));

            return verificationCheck.Valid;
        }
        public async Task<ImageUploadResult> CloudinaryUploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length > 0)
            {
                using var stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream),
                    Transformation = new Transformation()
                        .Height(1517).Width(1024)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }
        public async Task<DeletionResult> CloudinaryDeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
            return deleteResult;
        }
        public HashSaltDto HMACSHA512(string code)
        {
            byte[] codeHash, codeKey;
            using (var hmac = new HMACSHA512())
            {
                codeKey = hmac.Key;
                codeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));
            }
            return new HashSaltDto
            {
                hashedCode = codeHash,
                keyCode = codeKey
            };
        }
        public bool MatchCodeHash(string code, byte[] hashedCode, byte[] keyCode)
        {
            using (var hmac = new HMACSHA512(keyCode))
            {
                var codeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));
                for (int i = 0; i < codeHash.Length; i++)
                {
                    if (codeHash[i] != hashedCode[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
