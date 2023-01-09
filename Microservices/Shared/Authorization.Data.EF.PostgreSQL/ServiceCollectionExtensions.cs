using Authorization.Data.Shared.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Data.EF.PostgreSQL;

public static class ServiceCollectionExtensions
{
    public static void AddHospitalPostgreSQL(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<HospitalDbContext>(opt => opt.UseLazyLoadingProxies().UseNpgsql(connectionString));
    }
}