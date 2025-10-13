using System.Text;
using KnowledgeHub.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KnowledgeHub.Api.Helpers;

public static class BuilderConfigurationHelper
{
    /// <summary>
    ///     Method that is used to contain logic for <see cref="ApplicationDbContext" /> configuration
    /// </summary>
    /// <param name="builder"> Application builder </param>
    public static WebApplicationBuilder ConfigureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
                .UseAsyncSeeding(async (context, _, ct) =>
                {
                    if (!ct.IsCancellationRequested)
                    {
                        await context.SeedAsync(ct);
                    }
                });
        });

        return builder;
    }

    /// <summary>
    ///     Adds Jwt token authentication and authorization to the app
    /// </summary>
    /// <param name="builder"> Application builder </param>
    public static WebApplicationBuilder ConfigureJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}