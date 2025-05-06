using FluentValidation;
using Microsoft.Extensions.Configuration;
using Profiles.Application.Models;
using Profiles.DataAccess.Repositories;
using Profiles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly IValidator<PatientRequest> _validator;
        private readonly IValidator<PaginationModel> _pagValidator;

        public PatientService(IPatientsRepository patientsRepository, IValidator<PatientRequest> validator, IValidator<PaginationModel> pagValidator)
        {
            _patientsRepository = patientsRepository;
            _validator = validator;
            _pagValidator = pagValidator;
        }

        public async Task<ServiceResponse<Patient>> GetAsync(Guid id)
        {
            try
            {
                var patient = await _patientsRepository.GetAsync(id);

                if (patient is not null)
                {
                    return new ServiceResponse<Patient>
                    {
                        Success = true,
                        Data = patient
                    };
                }

                return new ServiceResponse<Patient>
                {
                    Error = "Patient was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Patient>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<Patient>>> GetListAsync(PaginationModel model)
        {
            try
            {
                await _pagValidator.ValidateAndThrowAsync(model);

                var patients = (await _patientsRepository.GetAllAsync())
                    .Skip((model.Page - 1) * model.PageSize)
                    .Take(model.PageSize)
                    .ToList();

                return new ServiceResponse<List<Patient>>
                {
                    Success = true,
                    Data = patients
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Patient>>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Patient>> AddAsync(Guid accountId, PatientRequest model)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(model);

                var patient = new Patient
                {
                    ImageUrl = model.ImageUrl,
                    FistName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    DateOfBirth = model.DateOfBirth,
                    AccountId = accountId
                };

                await _patientsRepository.AddAsync(patient);

                return new ServiceResponse<Patient>
                {
                    Success = true,
                    Data = patient
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Patient>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Patient>> UpdateAsync(Guid id, PatientRequest model)
        {
            try
            {
                await _validator.ValidateAndThrowAsync(model);

                var patient = await _patientsRepository.GetAsync(id);

                if (patient is not null)
                {
                    patient.ImageUrl = model.ImageUrl;
                    patient.FistName = model.FirstName;
                    patient.LastName = model.LastName;
                    patient.MiddleName = model.MiddleName;
                    patient.DateOfBirth = model.DateOfBirth;

                    await _patientsRepository.UpdateAsync(patient);

                    return new ServiceResponse<Patient>
                    {
                        Success = true,
                        Data = patient
                    };
                }

                return new ServiceResponse<Patient>
                {
                    Error = "Patient was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Patient>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                var patient = await _patientsRepository.GetAsync(id);

                if (patient is not null)
                {
                    await _patientsRepository.RemoveAsync(id);

                    return new ServiceResponse<Guid>
                    {
                        Success = true,
                        Data = id
                    };
                }

                return new ServiceResponse<Guid>
                {
                    Error = "Patient was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Guid>
                {
                    Error = ex.Message
                };
            }
        }
    }
}
