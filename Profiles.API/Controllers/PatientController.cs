using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Application.Models;
using Profiles.Application.Services;
using System.Reflection;

namespace Profiles.API.Controllers
{
    [ApiController]
    [Route("patient"),Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _patientService.GetAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);    
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetListAsync([FromBody] PaginationModel model)
        {
            var response = await _patientService.GetListAsync(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPost("{accountId:guid}")]
        public async Task<IActionResult> CreateAsync(Guid accountId, [FromBody] PatientRequest model)
        {
            var response = await _patientService.AddAsync(accountId, model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PatientRequest model)
        {
            var response = await _patientService.UpdateAsync(id, model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _patientService.DeleteAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
