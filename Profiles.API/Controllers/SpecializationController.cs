using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Models;
using Profiles.Application.Services;

namespace Profiles.API.Controllers
{
    [ApiController]
    [Route("specialization")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializaitonService _specializaitonService;

        public SpecializationController(ISpecializaitonService specializaitonService)
        {
            _specializaitonService = specializaitonService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _specializaitonService.GetAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListAsync()
        {
            var response = await _specializaitonService.GetListAsync();

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
