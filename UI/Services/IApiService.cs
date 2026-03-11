namespace UI.Services;

public interface IApiService
{
    Task<T> GetAsync<T>(string endpoint, object request);
    Task<T> GetFromJsonAsync<T>(string endpoint);
    Task PostAsync(string endpoint, object data);
    Task<bool> PutAsync(string endpoint, object data);
    Task<bool> DeleteAsync(string endpoint);
}
