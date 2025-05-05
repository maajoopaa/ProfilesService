using FluentValidation;
using Microsoft.Extensions.Configuration;
using Profiles.Application.Models;
using Profiles.DataAccess.Repositories;
using Profiles.Domain.Models;
using System.Net.Http.Json;

namespace Profiles.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IValidator<DoctorCreateRequest> _createValidator;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IValidator<DoctorUpdateRequest> _updateValidator;
        private readonly IValidator<PaginationModel> _pagValidator;

        public DoctorService(IDoctorsRepository doctorsRepository, IValidator<DoctorCreateRequest> createValidator, HttpClient httpClient,
            IConfiguration config, IValidator<DoctorUpdateRequest> updateValidator, IValidator<PaginationModel> pagValidator)
        {
            _doctorsRepository = doctorsRepository;
            _createValidator = createValidator;
            _httpClient = httpClient;
            _config = config;
            _updateValidator = updateValidator;
            _pagValidator = pagValidator;
        }

        public async Task<ServiceResponse<List<Doctor>>> GetListAsync(PaginationModel model)
        {
            try
            {
                await _pagValidator.ValidateAndThrowAsync(model);

                var doctors = (await _doctorsRepository.GetAllAsync())
                    .Skip((model.Page - 1) * model.PageSize)
                    .Take(model.PageSize)
                    .ToList();

                return new ServiceResponse<List<Doctor>>
                {
                    Success = true,
                    Data = doctors
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Doctor>>
                {
                    Error = ex.Message
                };

            }
        }

        public async Task<ServiceResponse<Doctor>> GetAsync(Guid id)
        {
            try
            {
                var doctor = await _doctorsRepository.GetAsync(id);

                if (doctor is not null)
                {
                    return new ServiceResponse<Doctor>
                    {
                        Success = true,
                        Data = doctor
                    };
                }

                return new ServiceResponse<Doctor>
                {
                    Error = "Doctor was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Doctor>
                {
                    Error = ex.Message
                };

            }
        }

        public async Task<ServiceResponse<Doctor>> CreateAsync(DoctorCreateRequest model)
        {
            try
            {
                await _createValidator.ValidateAndThrowAsync(model);

                var password = PasswordGenerator.GeneratePassword();

                var response = await _httpClient.PostAsJsonAsync($"{_config["Services:Authorization"]}/account/register",
                    new RegisterRequest
                    {
                        Email = model.Email,
                        Password = password,
                        RepeatPassword = password
                    });

                if (response.IsSuccessStatusCode)
                {
                    var accountResponse = await response.Content.ReadFromJsonAsync<AccountResponse>();

                    if (accountResponse is not null)
                    {
                        var doctor = new Doctor
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            DateOfBirth = model.DateOfBirth,
                            SpecializationId = model.SpecializationId,
                            AccountId = accountResponse.Id,
                            OfficeId = model.OfficeId,
                            CareerStartYear = model.CareerStartYear,
                            Status = model.Status,
                        };

                        await _doctorsRepository.AddAsync(doctor);

                        MessageService.SendMessageAsync(model.Email, "Account is added",
                            $"Your password: {password}");

                        return new ServiceResponse<Doctor>
                        {
                            Success = true,
                            Data = doctor
                        };
                    }
                }

                return new ServiceResponse<Doctor>
                {
                    Error = await response.Content.ReadAsStringAsync()
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Doctor>
                {
                    Error = ex.Message
                };

            }
        }

        public async Task<ServiceResponse<Doctor>> UpdateAsync(Guid id, DoctorUpdateRequest model)
        {
            try
            {
                await _updateValidator.ValidateAndThrowAsync(model);

                var doctor = await _doctorsRepository.GetAsync(id);

                if (doctor is not null)
                {
                    doctor.ImageUrl = model.ImageUrl;
                    doctor.FirstName = model.FirstName;
                    doctor.LastName = model.LastName;
                    doctor.MiddleName = model.MiddleName;
                    doctor.DateOfBirth = model.DateOfBirth;
                    doctor.SpecializationId = model.SpecializationId;
                    doctor.OfficeId = model.OfficeId;
                    doctor.CareerStartYear = model.CareerStartYear;
                    doctor.Status = model.Status;

                    await _doctorsRepository.UpdateAsync(doctor);

                    return new ServiceResponse<Doctor>
                    {
                        Success = true,
                        Data = doctor
                    };

                }

                return new ServiceResponse<Doctor>
                {
                    Error = "Doctor was not found"
                };

            }
            catch (Exception ex)
            {
                return new ServiceResponse<Doctor>
                {
                    Error = ex.Message
                };

            }
        }

        public async Task<ServiceResponse<Guid>> DeleteAsync(Guid id)
        {
            try
            {

                var doctor = await _doctorsRepository.GetAsync(id);

                if (doctor is not null)
                {
                    await _doctorsRepository.RemoveAsync(id);

                    return new ServiceResponse<Guid>
                    {
                        Success = true,
                        Data = id
                    };

                }

                return new ServiceResponse<Guid>
                {
                    Error = "Doctor was not found"
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

        public async Task<ServiceResponse<Doctor>> ChangeStatusAsync(Guid id, Status status)
        {
            try
            {
                var doctor = await _doctorsRepository.GetAsync(id);

                if (doctor is not null)
                {
                    doctor.Status = status;

                    await _doctorsRepository.UpdateAsync(doctor);

                    return new ServiceResponse<Doctor>
                    {
                        Success = true,
                        Data = doctor
                    };
                }

                return new ServiceResponse<Doctor>
                {
                    Error = "Doctor was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Doctor>
                {
                    Error = ex.Message
                };

            }
        }
    }
}
