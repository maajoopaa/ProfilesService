using Profiles.Domain.Models;

namespace Profiles.DataAccess.Repositories
{
    public interface IDoctorsRepository
    {
        Task<string> AddAsync(Doctor entity);
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetAsync(Guid id);
        Task<string> RemoveAsync(Guid id);
        Task<string> UpdateAsync(Doctor entity);
    }
}