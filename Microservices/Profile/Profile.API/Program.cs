using System.Text;
using Authorization.API.Client;
using Authorization.Data.EF.PostgreSQL;
using Authorization.Data.Repository;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Documents.API.Client;
using Hellang.Middleware.ProblemDetails;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Profile.API.Consumer;
using Profile.API.Helpers;
using Profile.Application;
using Profile.Application.Contracts.Internal;
using Profile.Application.Helpers;
using Profile.Application.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configurationRoot = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
services.AddApplication();
services.AddApplicationServices();
var uriSettings = services.Configure<BlobUrlHelpers>(configurationRoot.GetSection(nameof(BlobUrlHelpers)));
services.AddSingleton(uriSettings);
services.AddRepository();
services.AddAuthorizationApi(configurationRoot);
services.AddHospitalPostgreSql(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
services.AddDocumentsApi(configurationRoot);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var emailConfig = services.Configure<EmailSettings>(configurationRoot.GetSection(nameof(EmailSettings)));
services.AddSingleton(emailConfig);

services.AddIdentity<Account, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddEntityFrameworkStores<HospitalDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<Account>>(TokenOptions.DefaultProvider);


services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationRoot["JwtSettings:JwtSecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true
    };
});
services.AddProblemDetails(x =>
{
    x.Map<Exception>((context, exception) => CustomValidation<Exception>.CustomerDetails(exception));
});
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Description = "Please insert JWT token into field"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    c.EnableAnnotations();
});


builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(OfficeStatusChangedConsumer).Assembly);
    x.SetKebabCaseEndpointNameFormatter();

    x.AddBus(ctx => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        var serviceProvider = builder.Services.BuildServiceProvider();
        cfg.ReceiveEndpoint("office-status-change", e => { e.Consumer<OfficeStatusChangedConsumer>(serviceProvider); });
        cfg.ReceiveEndpoint("doctor-status-change",
            e => { e.Consumer<SpecializationStatusChangedConsumer>(serviceProvider); });
    }));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseProblemDetails();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();