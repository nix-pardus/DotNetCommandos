namespace UI.Services;

public interface IApiService
{
    Task<T> GetAsync<T>(string endpoint, object request);
    Task PostAsync(string endpoint, object data);
    Task<bool> PutAsync(string endpoint, object data);
    Task<bool> DeleteAsync(string endpoint);
}
