﻿using DonationAppDemo.DTOs;
using DonationAppDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("CheckAccount")]
        public async Task<IActionResult> CheckExistedUser([FromBody]string phoneNum)
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
        public async Task<IActionResult> SignUpOrganiser([FromBody]SignUpOrganiserDto signUpOrganiserDto)
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
        public async Task<IActionResult> SignUpDonor([FromBody]SignUpDonorDto signUpDonorDto)
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
        public async Task<IActionResult> SignIn([FromBody]SignInDto signInDto)
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

        [HttpPut]
        [Route("UpdateApprovementOrganiser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> UpdateApprovementOrganiser([FromBody]string phoneNum, [FromBody]int organiserId)
        {
            try
            {
                var result = await _userAuthenticationService.UpdateApprovementOrganiser(phoneNum, organiserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
