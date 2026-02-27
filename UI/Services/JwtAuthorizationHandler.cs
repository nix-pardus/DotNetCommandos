namespace UI.Services;

public class JwtAuthorizationHandler : DelegatingHandler
{
    private readonly IServiceProvider _serviceProvider;

    public JwtAuthorizationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
    {
        var authService = _serviceProvider.GetRequiredService<IAuthService>();
        var token = await authService.GetJwtTokenAsync();
        if(!string.IsNullOrEmpty(token))
        {
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(httpRequest, cancellationToken);

        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshed = await authService.RefreshTokenAsync();
            if (refreshed)
            {
                var newToken = await authService.GetJwtTokenAsync();
                httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newToken);
                response = await base.SendAsync(httpRequest, cancellationToken);
            }
        }

        return response;
    }
}
