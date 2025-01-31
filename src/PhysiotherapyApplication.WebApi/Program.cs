using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhysiotherapyApplication.Application;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;
using PhysiotherapyApplication.Persistence;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.WebApi.Common;
using PhysiotherapyApplication.WebApi.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region Environment Configurations

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

#endregion

#region Custom Extensions Services
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationService();
#endregion

#region Identity Configurations
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PhysiotherapyApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Custom Middlewares

#endregion


builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});

// Closed .NET Default messages
builder.Services.Configure<ApiBehaviorOptions>(options=>options.SuppressModelStateInvalidFilter = true);



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseExceptionHandler(_ => { });

app.UseCustomExceptionHandler();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
