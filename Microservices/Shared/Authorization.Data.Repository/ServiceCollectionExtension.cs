using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Data.Repository;

public static class ServiceCollectionExtension
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
        services.AddScoped<IOfficeRepository, OfficeRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
    }
}