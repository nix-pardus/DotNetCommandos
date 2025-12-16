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

        return await base.SendAsync(httpRequest, cancellationToken);
    }
}
