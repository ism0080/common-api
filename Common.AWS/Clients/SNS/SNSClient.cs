using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace Common.AWS.Clients.SNS;

public class SNSClient : ISNSClient
{
  private readonly IAmazonSimpleNotificationService _client;

  public SNSClient(IAmazonSimpleNotificationService client)
  {
    _client = client;
  }

  public async Task<bool> PublishMessage(PublishRequest request)
  {
    var response = await _client.PublishAsync(request);

    return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
  }
}