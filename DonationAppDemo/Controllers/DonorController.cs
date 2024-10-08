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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _donorService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{donorId}")]
        public async Task<IActionResult> GetById([FromRoute]int donorId)
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
        [Route("Update/{donorId}")]
        public async Task<IActionResult> Update([FromRoute] int donorId, [FromBody] DonorDto donorDto)
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
        [Route("UpdateAva/{donorId}")]
        public async Task<IActionResult> UpdateAva([FromRoute] int donorId, [FromBody] IFormFile avaFile)
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
