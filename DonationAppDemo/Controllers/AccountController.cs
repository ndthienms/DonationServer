using DonationAppDemo.Models;
using DonationAppDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody]string phoneNum)
        {
            try
            {
                var result = await _accountService.Delete(phoneNum);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDisabledAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> UpdateDisabledAccount([FromBody] string phoneNum, [FromBody] bool disabled)
        {
            try
            {
                var result = await _accountService.UpdateDisabledAccount(phoneNum, disabled);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateDisabledPersonalAccount")]
        public async Task<IActionResult> UpdateDisabledPersonalAccount([FromBody] bool disabled)
        {
            try
            {
                var result = await _accountService.UpdateDisabledPersonalAccount(disabled);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
