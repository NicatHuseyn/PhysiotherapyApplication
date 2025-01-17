using Microsoft.AspNetCore.Identity;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;
using PhysiotherapyApplication.Persistence;
using PhysiotherapyApplication.Persistence.Contexts;

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
#endregion

#region Identity Configurations
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PhysiotherapyApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
