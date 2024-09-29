using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Course.Application.ActionResult;
using Course.Application.ActionResult.Extensions;
using Course.Application.Services.AppSettings;

namespace Course.Infrastructure.Authentication.JwtTokens;

public class ConfigureJwtBearerOptions(IAppSettings settings) : IConfigureNamedOptions<JwtBearerOptions>
{
    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
            return;

        byte[] key = Encoding.ASCII.GetBytes(settings.AuthSettings.ApiSecret);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new()
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsJsonAsync(
                        Result.Unauthorized().WithMessage("Доступ только авторизованным пользователям").PackAsApiResponse()
                    );
                }
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsJsonAsync(
                    Result.PermissionDenied().WithMessage("Нет доступа").PackAsApiResponse()
                );
            },
            OnMessageReceived = context =>
            {
                string? accessToken;
                if (context.Request.Path.ToString().Contains("server-messages"))
                {
                    accessToken = context.Request.Query["access_token"];
                }
                else
                {
                    accessToken = context.Request.Headers.Authorization
                        .FirstOrDefault()?
                        .Split(" ")[^1];
                }

                context.Token = accessToken;
                return Task.CompletedTask;
            },
        };
    }
}
