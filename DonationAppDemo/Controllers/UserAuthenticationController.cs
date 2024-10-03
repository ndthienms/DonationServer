using DonationAppDemo.DTOs;
using DonationAppDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }
        /*[HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp()
        {
            string accountSid = "ACf38b8a39f1aeca6bbe67529c1f2ea6e6";
            string authToken = "2e4394c1de32d1823354b3097cc62e47";

            TwilioClient.Init(accountSid, authToken);

            var verification = await VerificationResource.CreateAsync(
                to: "+84901151072",
                channel: "sms",
                pathServiceSid: "VAe2f25fec9727ec8fbd7c109146efe5da");
            return Ok(verification);
        }

        [HttpPost]
        [Route("verify")]
        public async Task<IActionResult> Verify(string otp)
        {
            string accountSid = "ACf38b8a39f1aeca6bbe67529c1f2ea6e6";
            string authToken = "2e4394c1de32d1823354b3097cc62e47";

            TwilioClient.Init(accountSid, authToken);

            var verificationCheck = await VerificationCheckResource.CreateAsync(
                to: "+84901151072", code: otp, pathServiceSid: "VAe2f25fec9727ec8fbd7c109146efe5da");

            //Console.WriteLine(verificationCheck.Status);
            return Ok(verificationCheck);
        }*/
        [HttpPost]
        [Route("CheckAccount")]
        public async Task<IActionResult> CheckExistedUser(string phoneNum)
        {
            try
            {
                var result = await _userAuthenticationService.CheckExistedUser(phoneNum);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignUpOrganiser")]
        public async Task<IActionResult> SignUpOrganiser(SignUpOrganiserDto signUpOrganiserDto)
        {
            try
            {
                var result = await _userAuthenticationService.SignUpOrganiser(signUpOrganiserDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignUpDonor")]
        public async Task<IActionResult> SignUpDonor(SignUpDonorDto signUpDonorDto)
        {
            try
            {
                var result = await _userAuthenticationService.SignUpDonor(signUpDonorDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            try
            {
                var result = await _userAuthenticationService.SignIn(signInDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
