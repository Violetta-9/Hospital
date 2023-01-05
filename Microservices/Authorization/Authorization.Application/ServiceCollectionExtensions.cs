using System.Reflection;
using Authorization.Application.Command.User.Registration;
using Authorization.Application.PipelineBehaviors;
using Authorization.Application.Query.User;
using Authorization.Application.Validators.Command;
using Authorization.Application.Validators.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));
         
        }
    }
}
