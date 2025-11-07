using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using E_Club.Application.Interfaces.Common;
using E_Club.Application.Interfaces.Clubs;
using E_Club.Application.Interfaces.Auth;
using E_Club.WebAPI.Extensions.HealthCheck.Extension;
using E_Club.Application.Interfaces.Users;
using E_Club.Repositories.Users;
using E_Club.Services.Users;
using InfrastructureEFCore;
using DomainModels.Security;
using E_Club.Services.Common.Security;
using E_Club.Services.Common.Message;
using E_Club.Services.Auth;
using E_Club.Persistence.Repositories.Auth;
using E_Club.Services.Clubs;
using E_Club.Persistence.Repositories.Clubs;
using E_Club.Services.Common.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EClub - API Swagger", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "[token]"
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
            Array.Empty<string>()
        }
    });
});
#endregion

#region BearerToken Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
        {
            var now = DateTime.UtcNow;
            return (notBefore == null || notBefore <= now) && (expires == null || expires > now);
        }
    };
});
#endregion

#region Dépendances
builder.Services.AddSingleton<Token>();
builder.Services.AddScoped<IMessageToReturn, MessageToReturn>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IRedisCache, RedisCache>();
#endregion

#region SQL Configuration
var env = builder.Configuration["AspNetCore_Environment"];

// Local (MySQL)
if (env == "Development")
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseMySql(
            builder.Configuration.GetConnectionString("MySqlLocalApi"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlLocalApi"))
        )
    );
}
// OnrenderPreproduction (PostgreSQL)
else if (env == "Preproduction")
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlOnRenderPreprodApi")));
}

builder.Services.AddMemoryCache();
#endregion

#region Redis Configuration
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis");
    option.InstanceName = "EClub_";
});
#endregion

#region HealthCheck Configuration

var sp = builder.Services.BuildServiceProvider();
var passwordHasherBuilder = sp.GetRequiredService<IPasswordHasher>();

if (env == "Development")
{
    var hcBuilder = builder.Services.AddHealthChecks()
    .AddMySql(builder.Configuration.GetConnectionString("MySqlLocalHealthCheck")!, name: "api-0-service-mysql")
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!, name: "api-1-service-redis");

    hcBuilder.AddApiEndpointsHealthChecks("https://localhost:5123", passwordHasherBuilder);

    builder.Services.AddHealthChecksUI(settings =>
    {
        settings.SetEvaluationTimeInSeconds(15);
        settings.AddHealthCheckEndpoint("EClub - API Health", "/health");
    })
    .AddMySqlStorage(builder.Configuration.GetConnectionString("MySqlLocalHealthCheckUI")!);
}
#endregion

#region MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
#endregion

#region FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
#endregion

var app = builder.Build();

#region Insertion données redondantes après migration
using (var scope = app.Services.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    var cs = env == "Preproduction"
        ? configuration.GetConnectionString("PostgreSqlOnRenderPreprodApi")
        : configuration.GetConnectionString("MySqlLocalApi");

    if (string.IsNullOrWhiteSpace(cs))
        throw new InvalidOperationException(
            $"Aucune chaîne de connexion pour l'env '{env}'. " +
            $"Vérifie ConnectionStrings:PostgreSqlOnRenderPreprodApi / MySqlLocalApi."
        );

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    if (env == "Preproduction")
    {
        // PostgreSQL
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlOnRenderPreprodApi"));
    }
    else
    {
        // MySQL
        optionsBuilder.UseMySql(
            builder.Configuration.GetConnectionString("MySqlLocalApi"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlLocalApi")));
    }

    using var dbContext = new AppDbContext(optionsBuilder.Options);

    dbContext.Database.Migrate();
    DbSeeder.SeedDatas(dbContext, passwordHasherBuilder);
}
#endregion

#region HealthCheck Endpoints
if (env == "Development")
{
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        }
    });

    app.MapHealthChecksUI(options =>
    {
        options.UIPath = "/health-ui";
        options.ApiPath = "/health-ui-api";
    });
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseStaticFiles();

    app.MapGet("/", () => Results.Redirect("/index.html")).ExcludeFromDescription();
}

app.UseMiddleware<TokenRevocation>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();