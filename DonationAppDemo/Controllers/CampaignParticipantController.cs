using DonationAppDemo.Services;
using DonationAppDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignParticipantController : ControllerBase
    {
        private readonly ICampaignParticipantService _campaignParticipantService;

        public CampaignParticipantController(ICampaignParticipantService campaignParticipantService)
        {
            _campaignParticipantService = campaignParticipantService;
        }

        [HttpGet]
        [Route("CheckParticipated")]
        public async Task<IActionResult> CheckParticipated(int donorId, int campaignId)
        {
            try
            {
                var result = await _campaignParticipantService.CheckParticipated(donorId, campaignId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("JoinCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "donor")]
        public async Task<IActionResult> JoinCampaign([FromBody]int campaignId)
        {
            try
            {
                var result = await _campaignParticipantService.JoinCampaign(campaignId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("CancelCampaignPartipation/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "donor")]
        public async Task<IActionResult> CancelCampaignPartipation([FromRoute]int campaignId)
        {
            try
            {
                var result = await _campaignParticipantService.CancelCampaignPartipation(campaignId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
