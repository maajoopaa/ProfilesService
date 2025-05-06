using FluentValidation;
using Profiles.Application.Models;
using Profiles.Application.Services;
using Profiles.Application.Validators;
using Profiles.DataAccess.Repositories;

namespace Profiles.API
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDoctorsRepository,DoctorsRepository>();
            services.AddTransient<IPatientsRepository,PatientsRepository>();
            services.AddTransient<ISpecializationsRepository,SpecializationsRepository>();
            services.AddTransient<IReceptionistsRepository, ReceptionistsRepository>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<PatientRequest>, PatientRequestValidator>();
            services.AddTransient<IValidator<DoctorCreateRequest>, DoctorCreateRequestValidator>();
            services.AddTransient<IValidator<DoctorUpdateRequest>, DoctorUpdateRequestValidator>();
            services.AddTransient<IValidator<ReceptionistCreateRequest>, ReceptionistCreateRequestValidator>();
            services.AddTransient<IValidator<ReceptionistUpdateRequest>, ReceptionistUpdateRequestValidator>();
            services.AddTransient<IValidator<PaginationModel>, PaginationModelValidator>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IDoctorService,DoctorService>();
            services.AddTransient<IPatientService,PatientService>();
            services.AddTransient<IReceptionistService,ReceptionistService>();
            services.AddTransient<ISpecializaitonService,SpecializaitonService>();
        }
    }
}
