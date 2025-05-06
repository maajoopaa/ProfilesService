using Profiles.Application.Models;
using Profiles.Domain.Models;

namespace Profiles.Application.Services
{
    public interface IReceptionistService
    {
        Task<ServiceResponse<Receptionist>> CreateAsync(ReceptionistCreateRequest model);
        Task<ServiceResponse<Guid>> DeleteAsync(Guid id);
        Task<ServiceResponse<Receptionist>> GetAsync(Guid id);
        Task<ServiceResponse<List<Receptionist>>> GetListAsync(PaginationModel model);
        Task<ServiceResponse<Receptionist>> UpdateAsync(Guid id, ReceptionistUpdateRequest model);
    }
}