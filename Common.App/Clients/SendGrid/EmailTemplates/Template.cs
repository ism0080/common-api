using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.App.Clients.SendGrid.EmailTemplates;
public class Template
{
  public const string CONTACT_REQUEST = @"
        {{BODY}}
    ";
}