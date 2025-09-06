
using System.Net.Http.Json;

namespace UI.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiService> _logger;

    public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DELETE request to {endpoint}");
            return false;
        }
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        try
        {
            _logger.LogInformation($"GET request to: {endpoint}");
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in GET request to {endpoint}");
            throw;
        }
    }

    public async Task<T> PostAsync<T>(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in POST request to {endpoint}");
            throw;
        }
    }

    public async Task<bool> PutAsync(string endpoint, object data)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in PUT request to {endpoint}");
            return false;
        }
    }
}
