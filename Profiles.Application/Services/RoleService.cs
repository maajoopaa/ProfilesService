using Microsoft.Extensions.Configuration;
using Profiles.Application.Models;
using Profiles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public RoleService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task ChangeAccountRoleAsync(Guid id, UpdateAccountRoleRequest model)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{_config["Services:Authorization"]}/account/update-role/{id}", model);
        }
    }
}
