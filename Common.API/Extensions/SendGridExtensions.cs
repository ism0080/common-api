using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;
using Common.App.Clients.SendGrid;
using Common.App.Configuration;

namespace Common.API.Extensions;

public static class SendGridExtensions
{
  public static IServiceCollection AddSendGridClient(this IServiceCollection services, IConfiguration configuration)
  {
    if (configuration == null)
    {
      throw new ArgumentNullException(nameof(configuration));
    }

    services.AddSendGrid(options => options.ApiKey = configuration[$"{nameof(SendGridConfig)}:ApiKey"]);
    services.AddTransient<ISendGridEmailClient, SendGridEmailClient>();
    services.Configure<SendGridConfig>(configuration.GetSection(nameof(SendGridConfig)));

    return services;
  }
}
