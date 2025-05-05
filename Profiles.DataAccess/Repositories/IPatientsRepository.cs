using Profiles.Domain.Models;

namespace Profiles.DataAccess.Repositories
{
    public interface IPatientsRepository
    {
        Task<string?> AddAsync(Patient entity);
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetAsync(Guid id);
        Task<string?> RemoveAsync(Guid id);
        Task<string?> UpdateAsync(Patient entity);
    }
}