using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using Office.Application.Command.UpdateOffice;
using Office.Application.PipelineBehaviors;
using Office.Application.Validators.UpdateOffice;
using System;

namespace Office.Application
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddScoped<IValidator<UpdateOfficeCommand>, UpdateOfficeValidator>(); ;
       

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(ServiceCollectionExtension).Assembly)
                .ForEach(item => services.AddScoped(item.InterfaceType, item.ValidatorType));

        }
    }
}
