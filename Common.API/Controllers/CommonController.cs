using Common.App.Models;
using Common.App.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Common.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommonController : ControllerBase
{
  private readonly ILogger<CommonController> _logger;
  private readonly IEmailService _emailService;

  public CommonController(ILogger<CommonController> logger, IEmailService emailService)
  {
    _logger = logger;
    _emailService = emailService;
  }

  // GET
  [HttpGet("helloWorld")]
  public IActionResult HelloWorld()
  {
    return Ok("Hello World");
  }

  // POST
  [HttpPost("contactRequest")]
  public async Task<IActionResult> SendEmail([FromBody] ContactRequestModel model)
  {
    var result = await _emailService.ContactRequest(model);
    return result ? Ok() : BadRequest();
  }

}
