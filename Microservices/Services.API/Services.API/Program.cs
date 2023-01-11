using Authorization.Data.EF.PostgreSQL;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using Hellang.Middleware.ProblemDetails;
using Services.API.Application;
using Services.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var services=builder.Services;
services.AddApplication();
services.AddRepository();
services.AddHospitalPostgreSQL(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
