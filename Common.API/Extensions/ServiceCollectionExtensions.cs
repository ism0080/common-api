using Common.App.Configuration;

namespace Common.API.Extensions;
public static class ServiceCollectionExtensions
{
  public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<SecurityConfig>(configuration.GetSection(nameof(SecurityConfig)));
  }
}