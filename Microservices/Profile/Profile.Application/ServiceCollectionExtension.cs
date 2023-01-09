using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Profile.Application.Command.Doctors.DeleteDoctor;
using Profile.Application.PipelineBehaviors;
using Profile.Application.Validators.Commands.Doctor;

namespace Profile.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
          
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(ServiceCollectionExtension).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
        }
    }
}
