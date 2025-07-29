using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private readonly IAdPlatformService _service;

        public AdPlatformController(IAdPlatformService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var content = await reader.ReadToEndAsync();
                    await _service.LoadFromFile(content);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult Search(string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest("Локация не указана");

            var platforms = _service.SearchByLocation(location);
            return Ok(platforms);
        }
    }
}
