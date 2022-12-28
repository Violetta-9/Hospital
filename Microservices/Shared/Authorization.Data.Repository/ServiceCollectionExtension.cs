using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Data.Repository
{
    public static  class ServiceCollectionExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IReceptionistRepository, ReceptionistRepository>();
        }
    }
}
