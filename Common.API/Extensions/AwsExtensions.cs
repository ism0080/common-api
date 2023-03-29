using Amazon.DynamoDBv2;
using Amazon.SimpleNotificationService;
using Common.AWS.Clients.DynamoDB;
using Common.AWS.Clients.SNS;

namespace Common.API.Extensions;

public static class AwsExtensions
{
  public static IServiceCollection AddAwsClients(this IServiceCollection services, IConfiguration configuration)
  {
    if (configuration == null)
    {
      throw new ArgumentNullException(nameof(configuration));
    }

    services.AddDefaultAWSOptions(configuration.GetAWSOptions());

    // DynamoDB
    services.AddAWSService<IAmazonDynamoDB>();
    services.AddTransient<IDynamoDBClient, DynamoDBClient>();

    // SNS
    services.AddAWSService<IAmazonSimpleNotificationService>();
    services.AddTransient<ISNSClient, SNSClient>();

    return services;
  }
}
