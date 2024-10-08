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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _organiserService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{organiserId}")]
        public async Task<IActionResult> GetById([FromRoute]int organiserId)
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
        [Route("Update/{organiserId}")]
        public async Task<IActionResult> Update([FromRoute] int organiserId, [FromBody]OrganiserDto organiserDto)
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
        [Route("UpdateAva/{organiserId}")]
        public async Task<IActionResult> UpdateAva([FromRoute] int organiserId, [FromBody] IFormFile avaFile)
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
        [Route("UpdateCertification/{organiserId}")]
        public async Task<IActionResult> UpdateCertification([FromRoute] int organiserId, [FromBody] IFormFile certificationFile)
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
