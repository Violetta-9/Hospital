using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Office.Application.PipelineBehaviors;

namespace Office.Application;

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