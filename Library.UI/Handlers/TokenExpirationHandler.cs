using Microsoft.AspNetCore.Authentication;

public class TokenExpirationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenExpirationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && response.Headers.Contains("Token-Expired"))
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                await context.SignOutAsync("LibraryCookie");
                context.Response.Redirect("/Account/Login");
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        return response;
    }
}
