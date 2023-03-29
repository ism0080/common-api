
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.API.Middleware;

public static class ResponseWriter
{
  public static Task WriteHealthResponse(HttpContext httpContext, HealthReport result)
  {
    httpContext.Response.ContentType = "application/json";

    var json = JsonConvert.SerializeObject(result, Formatting.Indented, new StringEnumConverter());
    return httpContext.Response.WriteAsync(json);
  }
}