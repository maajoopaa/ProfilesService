using Profiles.Application.Models;
using Profiles.Domain.Models;

namespace Profiles.Application.Services
{
    public interface ISpecializaitonService
    {
        Task<ServiceResponse<Specialization>> GetAsync(Guid id);
        Task<ServiceResponse<List<Specialization>>> GetListAsync();
    }
}