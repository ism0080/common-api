using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.App.Clients.SendGrid;
using Common.App.Models;
using Common.App.Services.Interface;

namespace Common.App.Services;

public class EmailService : IEmailService
{
  private readonly ISendGridEmailClient _emailClient;

  public EmailService(ISendGridEmailClient emailClient)
  {
    _emailClient = emailClient;
  }

  public async Task<bool> ContactRequest(ContactRequestModel model)
  {
    var isSent = await _emailClient.Send(Common.EmailTemplate.ContactRequest, new Clients.SendGrid.Models.EmailSendModel
    {
      Recipients = new List<string>
      {
        "isaac.mackle@gmail.com"
      },
      ReplyTo = model.Email,
      Subject = "Website Contact Request | isaacmackle.com",
      Body = new()
      {
        $"Name: {model.Name}",
        $"Email: {model.Email}",
        $"Message: {model.Message}"
      },
      FromName = model.Name
    });

    return isSent;
  }
}