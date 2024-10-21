using DonationAppDemo.DTOs;
using DonationAppDemo.Services;
using DonationAppDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        //CAMPAIGN
        [HttpGet]
        [Route("Get/{campaignId}")]
        public async Task<IActionResult> Get([FromRoute] int campaignId)
        {
            try
            {
                var result = await _campaignService.Get(campaignId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "organiser")]
        public async Task<IActionResult> AddOrganiserAccount([FromBody] CampaignDto campaignDto)
        {
            try
            {
                var result = await _campaignService.CreateCampaign(campaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "organiser")]
        public async Task<IActionResult> Delete([FromBody] CampaignDto campaignDto)
        {
            try
            {
                var result = await _campaignService.DeleteCampaign(campaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "organiser")]
        public async Task<IActionResult> UpdateCampaign([FromBody] CampaignDto campaignDto)
        {
            try
            {
                var result = await _campaignService.UpdateCampaign(campaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody] int campaignId, int statusId)
        {
            try
            {
                var result = await _campaignService.ChangeStatusCampaign(campaignId, statusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //RATE CAMPAIGN
        [HttpPost]
        [Route("AddRateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<IActionResult> AddRateCampaign([FromBody] RateCampaignDto rateCampaignDto)
        {
            try
            {
                var result = await _campaignService.RateCampaign(rateCampaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateRateCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<IActionResult> UpdateRateCampaign([FromBody] RateCampaignDto rateCampaignDto)
        {
            try
            {
                var result = await _campaignService.UpdateRateCampaign(rateCampaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //IMAGECAMPAIGN
        [HttpPost]
        [Route("AddImagesCampaign")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "organiser")]
        public async Task<IActionResult> AddImagesCampaign([FromBody] List<ImageCampaignDto> listImageCampaignDto)
        {
            try
            {
                var result = await _campaignService.AddListImageCampaign(listImageCampaignDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("DeleteListImage")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "organiser")]
        public async Task<IActionResult> DeleteListImage([FromBody] List<ImageCampaignDto> imageCampaignDtos)
        {
            try
            {
                var result = await _campaignService.RemoveListImageCampaign(imageCampaignDtos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}