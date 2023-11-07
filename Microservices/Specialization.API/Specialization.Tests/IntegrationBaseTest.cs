using Authorization.Data.Shared.DbContext;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Specialization.API.Application.PipelineBehaviors;
using Authorization.Data.Repository;
using Services.API.Client;
using ServiceCollectionExtension = Specialization.API.Application.ServiceCollectionExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using Services.API.Client.Abstraction;
using Services.API.Client.GeneratedClient;
using System;
using MassTransit;

namespace Specialization.Tests
{
    public class IntegrationBaseTest
    {
        private IConfiguration Configuration { get; set; }
        protected IServiceProvider Services { get; set; }

        protected IMediator _mediator;
        [SetUp]
        public void TestInitialize ()
        {
           
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Local.json", true)
                .Build();
            
            var serviceCollection = new ServiceCollection().AddSingleton(Configuration);
            serviceCollection.AddDbContext<HospitalDbContext>(options =>
            {
                options.UseInMemoryDatabase("SpecializationDb");
            });
            serviceCollection.AddMediatR(typeof(Specialization.API.Application.Command.CreateSpecialization.CreateSpecializationCommand).Assembly);
            serviceCollection.AddScoped<ISpecializationRepository, SpecializationRepository>();
            serviceCollection.AddServiceApi(Configuration);
            serviceCollection.AddScoped(x => new Mock<IServiceApiProxy>().Object);
            serviceCollection.AddScoped(x => new Mock<IPublishEndpoint>().Object);
            serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(ServiceCollectionExtension).Assembly)
                .ForEach(item => serviceCollection.AddScoped(item.InterfaceType, item.ValidatorType));
            Services = serviceCollection.BuildServiceProvider();
            _mediator = Services.GetService<IMediator>();
            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

                var newSpecialization = new Authorization.Data_Domain.Models.Specialization
                {
                   Title = "first",
                   IsActive=true
                };
                dbContext.Specializations.Add(newSpecialization);
                dbContext.SaveChanges();
            }
        }
    }



