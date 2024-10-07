using DonationAppDemo.DTOs;
using DonationAppDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById([FromBody]int donorId)
        {
            try
            {
                var result = await _donorService.GetById(donorId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] int donorId, [FromBody] DonorDto donorDto)
        {
            try
            {
                var result = await _donorService.Update(donorId, donorDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateAva")]
        public async Task<IActionResult> UpdateAva([FromBody] int donorId, [FromBody] IFormFile avaFile)
        {
            try
            {
                var result = await _donorService.UpdateAva(donorId, avaFile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
