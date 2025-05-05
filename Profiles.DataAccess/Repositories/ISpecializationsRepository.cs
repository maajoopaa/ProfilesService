using Profiles.Domain.Models;

namespace Profiles.DataAccess.Repositories
{
    public interface ISpecializationsRepository
    {
        Task<List<Specialization>> GetAllAsync();
        Task<Specialization?> GetAsync(Guid id);
    }
}