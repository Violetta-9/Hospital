using System.Text;
using Authorization.Application;
using Authorization.Application.Helpers;
using Authorization.Data.EF.PostgreSQL;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Authorization_API.Helpers;
using Authorization_API.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Hellang.Middleware.ProblemDetails;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
var services=builder.Services;
var configurationRoot = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddApplication();
services.Configure<JwtSettings>(configurationRoot.GetSection(nameof(JwtSettings)));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
services.AddHostedService<RoleHostedServices>();
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
services.AddHospitalPostgreSQL(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
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
    c.EnableAnnotations();
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
