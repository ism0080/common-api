using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.App.Models;

namespace Common.App.Services.Interface;
public interface IEmailService
{
  Task<bool> ContactRequest(ContactRequestModel model);
}