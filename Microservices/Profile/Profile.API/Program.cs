using Authorization.Data.EF.PostgreSQL;
using Authorization.Data.Shared.DbContext;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Profile.Application;

var builder = WebApplication.CreateBuilder(args);
var services=builder.Services;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddApplication();
services.AddHospitalPostgreSQL(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
