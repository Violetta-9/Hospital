using System.Text;
using Authorization.Data.EF.PostgreSQL;
using Authorization.Data.Repository;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Azure.Storage.Blobs;
using Documents.API.Application;
using Documents.API.Application.Configurations;
using Documents.API.Application.Services;
using Documents.API.Helpers;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services = builder.Services;
var configurationRoot = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>(true)
    .Build();
services.AddApplication();
services.AddRepository();
var blobSettings = services.Configure<BlobStorageSettings>(configurationRoot.GetSection(nameof(BlobStorageSettings)));
services.AddSingleton(blobSettings);
services.AddHospitalPostgreSql(configurationRoot.GetSection("ConnectionStrings:DefaultConnection").Value);
services.AddApplicationServices();
services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetSection("ConnectionStrings:BlobStorageConnection").Value));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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


services.AddProblemDetails(x =>
{
    x.Map<Exception>((context, exception) => CustomValidation<Exception>.CustomerDetails(exception));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseProblemDetails();
app.UseAuthorization();

app.MapControllers();

app.Run();