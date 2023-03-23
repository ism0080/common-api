namespace Common.App.Configuration;
public class SendGridConfig
{
  public string ApiKey { get; set; }
  public string SenderEmail { get; set; }
  public string SenderName { get; set; }
  public string DefaultRecipient { get; set; }
}
