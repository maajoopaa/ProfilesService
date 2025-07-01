using Profiles.Application.Models;

namespace Profiles.Application.Services
{
    public interface IRoleService
    {
        Task ChangeAccountRoleAsync(Guid id, UpdateAccountRoleRequest model);
    }
}