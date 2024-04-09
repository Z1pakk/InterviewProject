using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

namespace InterviewProject.Server.Cookie
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public CustomCookieAuthenticationEvents()
        {
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            return Handler(StatusCodes.Status401Unauthorized)(context);
        }

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            return Handler(StatusCodes.Status403Forbidden)(context);
        }

        private Func<RedirectContext<CookieAuthenticationOptions>, Task> Handler(int statusCode)
        {
            return ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.Headers[HeaderNames.Location] = ctx.RedirectUri;
                    ctx.Response.StatusCode = statusCode;
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }

                return Task.CompletedTask;
            };
        }
    }
}
