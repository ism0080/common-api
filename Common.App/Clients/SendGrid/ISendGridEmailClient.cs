using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.App.Clients.SendGrid.Models;
using Common.App.Common;
using SendGrid.Helpers.Mail;

namespace Common.App.Clients.SendGrid;

public interface ISendGridEmailClient
{
  Task<bool> Send(EmailTemplate template, EmailSendModel content, List<Attachment>? attachments = null);
}