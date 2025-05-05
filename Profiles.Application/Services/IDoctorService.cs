using Profiles.Application.Models;
using Profiles.Domain.Models;

namespace Profiles.Application.Services
{
    public interface IDoctorService
    {
        Task<ServiceResponse<Doctor>> ChangeStatusAsync(Guid id, Status status);
        Task<ServiceResponse<Doctor>> CreateAsync(DoctorCreateRequest model);
        Task<ServiceResponse<Guid>> DeleteAsync(Guid id);
        Task<ServiceResponse<Doctor>> GetAsync(Guid id);
        Task<ServiceResponse<List<Doctor>>> GetListAsync(PaginationModel model);
        Task<ServiceResponse<Doctor>> UpdateAsync(Guid id, DoctorUpdateRequest model);
    }
}