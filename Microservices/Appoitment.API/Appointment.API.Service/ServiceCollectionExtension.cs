using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;


namespace Appointment.API.Application.Service
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPdfService, PdfService>();
            QuestPDF.Settings.License = LicenseType.Community;
        }
    }
}
