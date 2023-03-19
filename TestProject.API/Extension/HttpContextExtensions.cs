using Microsoft.AspNetCore.Http.Features;

namespace TestProject.API.Extension
{
    public static class HttpContextExtensions
    {
        public static string GetPath(this HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
        }
    }
}
