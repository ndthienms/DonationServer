using Twilio.Rest.Verify.V2.Service;
using Twilio;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Cryptography;
using DonationAppDemo.DTOs;
using DonationAppDemo.Helper;
using System;
using Twilio.TwiML.Voice;
using Twilio.Http;
//using DonationAppDemo.Models;
using Twilio.Jwt.AccessToken;
using DonationAppDemo.Services.Interfaces;

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
                    /*Transformation = new Transformation()
                        .Height(1517).Width(1024)*/
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
    }
}
