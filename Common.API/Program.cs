using Common.API.Extensions;
using Common.App.Services;
using Common.App.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.ConfigureSettings(builder.Configuration);
services.AddSendGridClient(builder.Configuration);

services.AddSingleton<IEmailService, EmailService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
  });
  app.MapSwagger();
}

app.UseAuthorization();
app.UseApiKeyMiddleware(app.Environment.IsDevelopment());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");
app.Run();