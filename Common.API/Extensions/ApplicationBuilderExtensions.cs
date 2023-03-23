using Common.API.Middleware;

namespace Common.API.Extensions;
public static class ApplicationBuilderExtensions
{
  public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder, bool isDevelopmentEnvironment)
  {
    return builder.UseMiddleware<ApiKeyMiddleware>(isDevelopmentEnvironment);
  }
}