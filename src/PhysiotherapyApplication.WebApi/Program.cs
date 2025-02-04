using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using PhysiotherapyApplication.Application;
using PhysiotherapyApplication.Domain.Entities.IdentityModels;
using PhysiotherapyApplication.Domain.Options;
using PhysiotherapyApplication.Persistence;
using PhysiotherapyApplication.Persistence.Contexts;
using PhysiotherapyApplication.Persistence.Services.AuthService;
using PhysiotherapyApplication.WebApi.Common;
using PhysiotherapyApplication.WebApi.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Options Configuration
builder.Services.Configure<ConnectionStringOption>(builder.Configuration.GetSection(ConnectionStringOption.Key));

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection(CustomTokenOption.Key));

builder.Services.Configure<GoogleConfigurationOption>(builder.Configuration.GetSection(GoogleConfigurationOption.Key));
#endregion

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
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<PhysiotherapyApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Authentication Configurations
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Auidence,
            IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});


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
