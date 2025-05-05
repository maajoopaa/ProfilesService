using Profiles.Domain.Models;

namespace Profiles.DataAccess.Repositories
{
    public interface IReceptionistsRepository
    {
        Task<string> AddAsync(Receptionist entity);
        Task<List<Receptionist>> GetAllAsync();
        Task<Receptionist?> GetAsync(Guid id);
        Task<string> RemoveAsync(Guid id);
        Task<string> UpdateAsync(Receptionist entity);
    }
}