using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.App.Clients.SendGrid.EmailTemplates;
using Common.App.Clients.SendGrid.Models;
using Common.App.Common;
using Common.App.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Common.App.Clients.SendGrid;

public class SendGridEmailClient : ISendGridEmailClient
{
  private readonly ISendGridClient _client;
  private readonly SendGridConfig _config;

  public SendGridEmailClient(IOptions<SendGridConfig> config, ISendGridClient client)
  {
    _client = client;
    _config = config.Value;
  }

  public async Task<bool> Send(EmailTemplate template, EmailSendModel content, List<Attachment>? attachments = null)
  {
    var msg = new SendGridMessage()
    {
      From = new EmailAddress(_config.SenderEmail, _config.SenderName),
      Subject = content.Subject,
      ReplyTo = content.ReplyTo != null ? new EmailAddress(content.ReplyTo) : null,
      PlainTextContent = BuildText(content),
      HtmlContent = BuildHtml(template, content),
      Attachments = attachments
    };

    foreach (var email in content.Recipients)
    {
      msg.AddTo(new EmailAddress(email));
    }

    if (!content.Recipients.Any())
    {
      msg.AddTo(new EmailAddress(_config.DefaultRecipient));
    }

    msg.SetClickTracking(false, false);
    msg.SetOpenTracking(false);
    msg.SetGoogleAnalytics(false);
    msg.SetSubscriptionTracking(false);

    var response = await _client.SendEmailAsync(msg);

    return response.IsSuccessStatusCode;
  }

  protected static string BuildText(EmailSendModel content)
  {
    var bodyItems = content.Body.Select(b => $"{b}\n\n");
    var from = $"-\n{content.FromName}";
    return $"{string.Join(string.Empty, bodyItems)}{from}";
  }

  protected static string BuildHtml(EmailTemplate template, EmailSendModel content)
  {
    switch (template)
    {
      case EmailTemplate.ContactRequest:
        var contactReqTemplate = Template.CONTACT_REQUEST;
        var contactBodyItems = content.Body.Select(b => $"<p>{b}</p>");
        contactReqTemplate = contactReqTemplate.Replace("{{BODY}}", string.Join(string.Empty, contactBodyItems));
        return contactReqTemplate;

      default:
        throw new ArgumentOutOfRangeException($"Invalid template: {template}");
    }
  }
}