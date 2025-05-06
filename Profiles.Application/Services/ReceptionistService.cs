using FluentValidation;
using Microsoft.Extensions.Configuration;
using Profiles.Application.Models;
using Profiles.DataAccess.Repositories;
using Profiles.Domain.Models;
using System.Net.Http.Json;

namespace Profiles.Application.Services
{
    public class ReceptionistService : IReceptionistService
    {
        private readonly IReceptionistsRepository _receptionistsRepository;
        private readonly IValidator<ReceptionistCreateRequest> _createValidator;
        private readonly IValidator<ReceptionistUpdateRequest> _updateValidator;
        private readonly IValidator<PaginationModel> _pagValidator;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public ReceptionistService(IReceptionistsRepository receptionistsRepository, IValidator<ReceptionistCreateRequest> createValidator,
            IValidator<ReceptionistUpdateRequest> updateValidator, IValidator<PaginationModel> pagValidator, IConfiguration config,
            HttpClient httpClient)
        {
            _receptionistsRepository = receptionistsRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _pagValidator = pagValidator;
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<Receptionist>> GetAsync(Guid id)
        {
            try
            {
                var receptionist = await _receptionistsRepository.GetAsync(id);

                if (receptionist is not null)
                {
                    return new ServiceResponse<Receptionist>
                    {
                        Success = true,
                        Data = receptionist
                    };
                }

                return new ServiceResponse<Receptionist>
                {
                    Error = "Receptionist was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Receptionist>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<List<Receptionist>>> GetListAsync(PaginationModel model)
        {
            try
            {
                await _pagValidator.ValidateAndThrowAsync(model);

                var receptionists = (await _receptionistsRepository.GetAllAsync())
                    .Skip((model.Page - 1) * model.PageSize)
                    .Take(model.PageSize)
                    .ToList();

                return new ServiceResponse<List<Receptionist>>
                {
                    Success = true,
                    Data = receptionists
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Receptionist>>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Receptionist>> CreateAsync(ReceptionistCreateRequest model)
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
                        var receptionist = new Receptionist
                        {
                            ImageUrl = model.ImageUrl,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            AccountId = accountResponse.Id,
                            OfficeId = model.OfficeId
                        };

                        MessageService.SendMessageAsync(model.Email, "Account is added",
                            $"Your password: {password}");

                        return new ServiceResponse<Receptionist>
                        {
                            Success = true,
                            Data = receptionist
                        };
                    }
                }
                return new ServiceResponse<Receptionist>
                {
                    Error = await response.Content.ReadAsStringAsync()
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Receptionist>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Receptionist>> UpdateAsync(Guid id, ReceptionistUpdateRequest model)
        {
            try
            {
                await _updateValidator.ValidateAndThrowAsync(model);

                var receptionist = await _receptionistsRepository.GetAsync(id);

                if (receptionist is not null)
                {
                    receptionist.ImageUrl = model.ImageUrl;
                    receptionist.FirstName = model.FirstName;
                    receptionist.LastName = model.LastName;
                    receptionist.MiddleName = model.MiddleName;
                    receptionist.OfficeId = model.OfficeId;

                    await _receptionistsRepository.UpdateAsync(receptionist);

                    return new ServiceResponse<Receptionist>
                    {
                        Success = true,
                        Data = receptionist
                    };
                }

                return new ServiceResponse<Receptionist>
                {
                    Error = "Receptioninst was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Receptionist>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Guid>> DeleteAsync(Guid id)
        {
            try
            {

                var receptionist = await _receptionistsRepository.GetAsync(id);

                if (receptionist is not null)
                {
                    await _receptionistsRepository.RemoveAsync(id);

                    return new ServiceResponse<Guid>
                    {
                        Success = true,
                        Data = id
                    };

                }

                return new ServiceResponse<Guid>
                {
                    Error = "Receptionist was not found"
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
