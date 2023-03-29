using System.Net;
using System.Text;
using Common.App.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Common.API.Middleware;

internal class ApiKeyMiddleware
{
  private readonly RequestDelegate _next;
  public static string ApiKeyHeaderKey => "X-API-KEY";
  private readonly bool _isDevelopmentEnvironment;
  private readonly string _apiKey;

  public ApiKeyMiddleware(RequestDelegate next, IOptions<SecurityConfig> securityConfigOptions, bool isDevelopmentEnvironment)
  {
    _next = next;
    _isDevelopmentEnvironment = isDevelopmentEnvironment;
    _apiKey = securityConfigOptions.Value?.ApiKey?.Trim() ?? throw new ArgumentException("ApiKey must be provided, but couldn't get from settings");
  }

  public async Task Invoke(HttpContext context)
  {
    var validationMessage = $"'{ApiKeyHeaderKey}' header is not present in request";
    var validApiKey = false;
    if (IsDevelopmentMode(_isDevelopmentEnvironment))
    {
      validApiKey = true;
    }
    else if (context.Request.Headers.ContainsKey(ApiKeyHeaderKey))
    {
      var key = context.Request.Headers[ApiKeyHeaderKey].ToString().Trim();
      if (key.Equals(_apiKey))
      {
        validApiKey = true;
      }
      else
      {
        validationMessage = $"Validation key provided is different from the expected Api Key";
      }
    }

    if (!validApiKey)
    {
      var errorMessageBytes = Encoding.UTF8.GetBytes($"Failed to validate api key. {validationMessage}");
      context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
      await context.Response.Body.WriteAsync(errorMessageBytes);
    }
    else
    {
      await _next.Invoke(context);
    }
  }

  private bool IsDevelopmentMode(bool isDevelopmentEnvironment) => isDevelopmentEnvironment && _apiKey.Equals(string.Empty);

}