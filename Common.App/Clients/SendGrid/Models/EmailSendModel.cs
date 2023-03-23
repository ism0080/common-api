using System.Collections.Generic;

namespace Common.App.Clients.SendGrid.Models;

public class EmailSendModel
{
  public List<string> Recipients { get; set; } = new List<string>();
  public string? ReplyTo { get; set; }
  public string? Subject { get; set; }
  public string? RecipientName { get; set; }
  public List<string> Body { get; set; } = new List<string>();
  public string? FromName { get; set; }
  public string? PreviewText { get; set; }
}
