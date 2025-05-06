using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Models;
using Profiles.Application.Services;
using Profiles.Domain.Models;

namespace Profiles.API.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost(""),Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] DoctorCreateRequest model)
        {
            var response = await _doctorService.CreateAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPut("{id:guid}"),Authorize]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] DoctorUpdateRequest model)
        {
            var response = await _doctorService.UpdateAsync(id, model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpDelete("{id:guid}"),Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _doctorService.DeleteAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _doctorService.GetAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetListAsync([FromBody] PaginationModel model)
        {
            var response = await _doctorService.GetListAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPatch("{id:guid}/change-status"),Authorize]
        public async Task<IActionResult> ChangeStatusAsync(Guid id, [FromBody] Status status)
        {
            var response = await _doctorService.ChangeStatusAsync(id, status);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
