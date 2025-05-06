using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Models;
using Profiles.Application.Services;

namespace Profiles.API.Controllers
{
    [ApiController]
    [Route("receptionist")]
    public class ReceptionistController : ControllerBase
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _receptionistService.GetAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetListAsync([FromBody] PaginationModel model)
        {
            var response = await _receptionistService.GetListAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] ReceptionistCreateRequest model)
        {
            var response = await _receptionistService.CreateAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ReceptionistUpdateRequest model)
        {
            var response = await _receptionistService.UpdateAsync(id, model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _receptionistService.DeleteAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
