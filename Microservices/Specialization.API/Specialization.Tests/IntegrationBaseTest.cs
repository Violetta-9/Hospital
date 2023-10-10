using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using FluentAssertions.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Specialization.API.Application.PipelineBehaviors;
using Specialization.API.Controllers.Abstraction.Mediator;
using System.Reflection;
using Specialization.API.Application;
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

namespace Specialization.Tests
{
    public class IntegrationBaseTest
    {
        private IConfiguration Configuration { get; set; }
        private IServiceProvider Services { get; set; }

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
            serviceCollection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(ServiceCollectionExtension).Assembly)
                .ForEach(item => serviceCollection.AddScoped(item.InterfaceType, item.ValidatorType));
            serviceCollection.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration()
                {
                    Issuer = MockJwtTokens.Issuer
                };

                config.SigningKeys.Add(MockJwtTokens.SecurityKey);
                options.Configuration = config;
            });
            Services = serviceCollection.BuildServiceProvider();
            _mediator = Services.GetService<IMediator>();


        }
    }
}
public static class MockJwtTokens
{
    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
    private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
    private static readonly byte[] s_key = new byte[32];

    static MockJwtTokens()
    {
        s_rng.GetBytes(s_key);
        SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
    }
}
