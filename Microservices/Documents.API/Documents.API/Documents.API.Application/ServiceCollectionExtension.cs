using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Documents.API.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;

namespace Documents.API.Application
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

