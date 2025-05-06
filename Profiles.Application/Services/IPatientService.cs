using Profiles.Application.Models;
using Profiles.Domain.Models;

namespace Profiles.Application.Services
{
    public interface IPatientService
    {
        Task<ServiceResponse<Patient>> AddAsync(Guid accountId, PatientRequest model);
        Task<ServiceResponse<Guid>> DeleteAsync(Guid id);
        Task<ServiceResponse<List<Patient>>> GetListAsync(PaginationModel model);
        Task<ServiceResponse<Patient>> GetAsync(Guid id);
        Task<ServiceResponse<Patient>> UpdateAsync(Guid id, PatientRequest model);
    }
}