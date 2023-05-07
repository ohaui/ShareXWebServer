using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Data;

namespace ShareXWebClient.Middleware;

public class RedirectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RedirectMiddleware> _logger;

    public RedirectMiddleware(RequestDelegate next, ILogger<RedirectMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, ApplicationContext context)
    {
        var query = httpContext.Request.GetDisplayUrl();
        var link = await context.Links.FirstOrDefaultAsync(x => x.Value == query);

        if (link != default)
        {
            httpContext.Response.Redirect(link.ReferenceLink);
        }
        else
        {
            await _next.Invoke(httpContext);
        }
    }
}