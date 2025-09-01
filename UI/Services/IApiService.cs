namespace UI.Services;

public interface IApiService
{
    Task<T> GetAsync<T>(string endpoint);
    Task<T> PostAsync<T>(string endpoint, object data);
    Task<bool> PutAsync(string endpoint, object data);
    Task<bool> DeleteAsync(string endpoint);
}
