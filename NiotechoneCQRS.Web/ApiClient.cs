namespace NiotechoneCQRS.Web;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse?> GetAsync<TResponse>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _httpClient.PostAsJsonAsync(url, data);
        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var response = await _httpClient.PutAsJsonAsync(url, data);
        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var response = await _httpClient.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }
}
