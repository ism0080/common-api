using Amazon.SimpleNotificationService.Model;

namespace Common.AWS.Clients.SNS;

public interface ISNSClient
{
  Task<bool> PublishMessage(PublishRequest request);
}