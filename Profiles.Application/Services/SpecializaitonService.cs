using Profiles.Application.Models;
using Profiles.DataAccess.Repositories;
using Profiles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Services
{
    public class SpecializaitonService : ISpecializaitonService
    {
        private readonly ISpecializationsRepository _specializationsRepository;

        public SpecializaitonService(ISpecializationsRepository specializationsRepository)
        {
            _specializationsRepository = specializationsRepository;
        }

        public async Task<ServiceResponse<Specialization>> GetAsync(Guid id)
        {
            try
            {
                var specialization = await _specializationsRepository.GetAsync(id);

                if (specialization is not null)
                {
                    return new ServiceResponse<Specialization>
                    {
                        Success = true,
                        Data = specialization
                    };
                }

                return new ServiceResponse<Specialization>
                {
                    Error = "Spezialization was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Specialization>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<List<Specialization>>> GetListAsync()
        {
            try
            {
                var specializations = await _specializationsRepository.GetAllAsync();

                return new ServiceResponse<List<Specialization>>
                {
                    Success = true,
                    Data = specializations
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Specialization>>
                {
                    Error = ex.Message,
                };
            }
        }
    }
}
