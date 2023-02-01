﻿using System.Reflection;
using Documents.API.Application.PipelineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Documents.API.Application;

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