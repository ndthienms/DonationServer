using DonationAppDemo.DTOs;
using DonationAppDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganiserController : ControllerBase
    {
        private readonly IOrganiserService _organiserService;

        public OrganiserController(IOrganiserService organiserService)
        {
            _organiserService = organiserService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById([FromBody]int organiserId)
        {
            try
            {
                var result = await _organiserService.GetById(organiserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]int organiserId, [FromBody]OrganiserDto organiserDto)
        {
            try
            {
                var result = await _organiserService.Update(organiserId, organiserDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateAva")]
        public async Task<IActionResult> UpdateAva([FromBody] int organiserId, [FromBody] IFormFile avaFile)
        {
            try
            {
                var result = await _organiserService.UpdateAva(organiserId, avaFile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateCertification")]
        public async Task<IActionResult> UpdateCertification([FromBody] int organiserId, [FromBody] IFormFile certificationFile)
        {
            try
            {
                var result = await _organiserService.UpdateCertification(organiserId, certificationFile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
